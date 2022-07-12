using System;
using System.Collections.Generic;
using BookingSystem.Models.Requests;
using BookingSystem.Models.Offices;
using BookingSystem.Models.Services;
using System.Data.SqlClient;
using System.Diagnostics;
using BookingSystem.Helper;
using System.IO;
using System.Web;
using System.Dynamic;
using BookingSystem.Controllers.Request;

namespace BookingSystem.Data.Request
{
    public class RequestDAO
    {
        public RequestSqlParser requestSqlParser = new RequestSqlParser();

        private string connectionString = Constants.ConnectionString;
        public string path = Constants.Path;
        public string localPath = Constants.LocalPath;

        public bool SaveFile(string fileBase64, string fileName, string path)
        {
            //Check if directory exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            //set the file path
            string filePath = Path.Combine(path, fileName);
            byte[] fileBytes = Convert.FromBase64String(fileBase64);

            File.WriteAllBytes(filePath, fileBytes);

            return true;
        }

        public string InsertOne(RequestModel requestModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    DateTime currentDate = DateTime.Now;
                    string trackingId = $"{Generate.RandomString(9)}-{Convert.ToString(currentDate.Ticks)}-{Convert.ToString(requestModel.OfficeId)}-{Convert.ToString(requestModel.ServiceId)}";
                    string fileName = $"{trackingId}-{requestModel.FileName}";
                    string fullFileName = fileName + requestModel.FileExtension;
                    string filePath = Path.Combine(path, fileName);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO Requests (TrackingId, OfficeId, ServiceId, UserNote, FileName, FilePath, FileSize, FileExtension) Values(@TrackingId, @OfficeId, @ServiceId, @UserNote, @FileName, @FilePath, @FileSize, @FileExtension)";

                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        command.Parameters.AddWithValue("@TrackingId", trackingId);
                        command.Parameters.AddWithValue("@OfficeId", requestModel.OfficeId);
                        command.Parameters.AddWithValue("@ServiceId", requestModel.ServiceId);
                        command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);
                        command.Parameters.AddWithValue("@FileName", fileName);
                        command.Parameters.AddWithValue("@FilePath", filePath);
                        command.Parameters.AddWithValue("@FileSize", requestModel.FileSize);
                        command.Parameters.AddWithValue("@FileExtension", requestModel.FileExtension);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    SaveFile(requestModel.FileData, fullFileName, path);
                    SaveFile(requestModel.FileData, fullFileName, localPath);

                    return trackingId;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return "Error";
                }
            }
            else return "The data sent are missing some field/s.";
        }

        public dynamic FindOne(string trackingId, bool isAggregated)
        {
            try
            {
                dynamic requestModel = new ExpandoObject();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "";

                    if (isAggregated == true)
                    {
                        sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Requests.FileSize, Requests.FileExtension, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                            "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                            "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                            "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                            "WHERE TrackingId=@TrackingId " +
                            "GROUP BY Status " +
                            "ORDER BY Requests.CreatedAt";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM Requests WHERE TrackingId=@TrackingId ORDER BY CreatedAt";
                    }

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (isAggregated == true)
                            {
                                requestSqlParser.GetOneAggregated(reader, requestModel);
                            }
                            else
                            {
                                requestSqlParser.GetOne(reader, requestModel);
                            }

                        }
                    }

                    connection.Close();
                }

                return requestModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new { };
            }
        }

        //This is for future function getFile
        //var fileName = $"{trackingId}.pdf";
        //var filePath = Path.Combine(path, fileName);
        //Byte[] fileByte = File.ReadAllBytes(filePath);
        //var fileBase64 = Convert.ToBase64String(fileByte);

        //Debug.WriteLine(fileByte);
        //Debug.WriteLine(fileBase64);

        public string UpdateOne(RequestUpdateModel requestModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "Update Requests Set OfficeNote=@OfficeNote, StatusId=@StatusId, UpdatedAt=GETDATE() Where Id=@Id";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Id", requestModel.Id);
                        command.Parameters.AddWithValue("@OfficeNote", requestModel.OfficeNote);
                        command.Parameters.AddWithValue("@StatusId", requestModel.StatusId);

                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    return "Updated";
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return "Error";
                }
            }
            else return "The data sent are missing some field/s.";
        }

        public string DeleteOne(string trackingId)
        {
            try
            {
                dynamic oldRequest = FindOne(trackingId, false);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Requests Where TrackingId=@TrackingId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                var fileName = $"{oldRequest.FileName}{oldRequest.FileExtension}";
                Debug.WriteLine(fileName);
                File.Delete(Path.Combine(path, fileName));
                File.Delete(Path.Combine(localPath, fileName));

                return "Deleted";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error";
            }
        }

        public dynamic GetByStatus(int statusId, bool isAggregated)
        {
            List<dynamic> returnList = new List<dynamic>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "";

                    if (isAggregated == true)
                    {
                        sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Requests.FileSize, Requests.FileExtension, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                                    "WHERE StatusId = @StatusID " +
                                    "ORDER BY Requests.CreatedAt";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM Requests WHERE StatusId = @StatusID ORDER BY CreatedAt";
                    }


                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@StatusID", statusId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (isAggregated == true)
                            {
                                requestSqlParser.GetListAggregated(reader, returnList);
                            }
                            else
                            {
                                requestSqlParser.GetList(reader, returnList);
                            }
                        }
                    }
                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }

        public dynamic GetByOffice(int officeId, bool isAggregated)
        {
            List<dynamic> returnList = new List<dynamic>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "";

                    if (isAggregated == true)
                    {
                        sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Requests.FileSize, Requests.FileExtension, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                                    "WHERE OfficeId = @OfficeId " +
                                    "ORDER BY Requests.CreatedAt";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM Requests WHERE OfficeId = @OfficeId ORDER BY CreatedAt";
                    }


                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@OfficeId", officeId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (isAggregated == true)
                            {
                                requestSqlParser.GetListAggregated(reader, returnList);
                            }
                            else
                            {
                                requestSqlParser.GetList(reader, returnList);
                            }
                        }
                    }


                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }

        public dynamic GetByService(int serviceId, bool isAggregated)
        {
            List<dynamic> returnList = new List<dynamic>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "";

                    if (isAggregated == true)
                    {
                        sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Requests.FileSize, Requests.FileExtension, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                                    "WHERE ServiceId = @ServiceId " +
                                    "ORDER BY Requests.CreatedAt";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM Requests WHERE ServiceId = @ServiceId ORDER BY CreatedAt";
                    }

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@ServiceId", serviceId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (isAggregated == true)
                            {
                                requestSqlParser.GetListAggregated(reader, returnList);
                            }
                            else
                            {
                                requestSqlParser.GetList(reader, returnList);
                            }
                        }
                    }


                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }

        public dynamic GetAll(bool isAggregated)
        {
            List<dynamic> returnList = new List<dynamic>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "";

                    if (isAggregated == true)
                    {
                        sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Requests.FileSize, Requests.FileExtension, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                            "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                            "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                            "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                            "ORDER BY Requests.CreatedAt";
                    }
                    else
                    {
                        sqlQuery = "SELECT * FROM Requests ORDER BY CreatedAt";
                    }

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            if (isAggregated == true)
                            {
                                requestSqlParser.GetListAggregated(reader, returnList);
                            }
                            else
                            {
                                requestSqlParser.GetList(reader, returnList);
                            }

                        }
                    }


                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }
    }
}