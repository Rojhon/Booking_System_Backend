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

namespace BookingSystem.Data.Request
{
    public class RequestDAO
    {
        private string connectionString = Constants.ConnectionString;
        public string path = HttpContext.Current.Server.MapPath("~/Files"); //Path
        public string localPath = @"c:\BookingSystemFiles";

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
                    string filePath = Path.Combine(path, fileName);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO Requests (TrackingId, OfficeId, ServiceId, UserNote, FileName, FilePath) Values(@TrackingId, @OfficeId, @ServiceId, @UserNote, @FileName, @FilePath)";

                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        command.Parameters.AddWithValue("@TrackingId", trackingId);
                        command.Parameters.AddWithValue("@OfficeId", requestModel.OfficeId);
                        command.Parameters.AddWithValue("@ServiceId", requestModel.ServiceId);
                        command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);
                        command.Parameters.AddWithValue("@FileName", fileName);
                        command.Parameters.AddWithValue("@FilePath", filePath);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    //SaveFile(requestModel.FileData, fileName, path);
                    //SaveFile(requestModel.FileData, fileName, localPath);

                    return "Success";
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return "Error";
                }
            }
            else return "The data sent are missing some field/s.";
        }

        public List<RequestModel> FindOne(string trackingId)
        {
            List<RequestModel> requestList = new List<RequestModel>();
            try
            {
                var fileName = $"{trackingId}.pdf";
                var filePath = Path.Combine(path, fileName);
                Byte[] fileByte = File.ReadAllBytes(filePath);
                var fileBase64 = Convert.ToBase64String(fileByte);

                Debug.WriteLine(fileByte);
                Debug.WriteLine(fileBase64);


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Requests Where TrackingId=@TrackingId";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestModel requestModel = new RequestModel();
                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.OfficeId = Convert.ToInt32(reader["OfficeId"]);
                            requestModel.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                            requestModel.StatusId = Convert.ToInt32(reader["StatusId"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            requestModel.FileData = Convert.ToString(fileBase64);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            requestList.Add(requestModel);
                        }
                    }

                    connection.Close();
                }

                return requestList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return requestList;
            }
        }

        public string UpdateOne(RequestModel requestModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "Update Requests Set UserNote=@UserNote, OfficeNote=@OfficeNote, Status=@Status, UpdatedAt=GETDATE() Where TrackingId=@TrackingId";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@TrackingId", requestModel.TrackingId);
                        command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Requests Where TrackingId=@TrackingId ORDER BY CreatedAt";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                var fileName = $"{trackingId}.pdf";
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

        public List<RequestAggregatedToAllModel> GetAllAggregated()
        {
            List<RequestAggregatedToAllModel> returnList = new List<RequestAggregatedToAllModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                        "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                        "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                        "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                        "ORDER BY Requests.CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestAggregatedToAllModel requestModel = new RequestAggregatedToAllModel();
                            Debug.WriteLine(reader["Id"]);
                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.Office = Convert.ToString(reader["Office"]);
                            requestModel.Service = Convert.ToString(reader["Service"]);
                            requestModel.Status = Convert.ToString(reader["Status"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            //requestModel.FileData = Convert.ToString(reader["FileData"]);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            returnList.Add(requestModel);
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

        public List<RequestAggregatedToAllModel> GetByStatus(int statusId)
        {
            List<RequestAggregatedToAllModel> returnList = new List<RequestAggregatedToAllModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                    "WHERE StatusId = @StatusID "+
                    "ORDER BY Requests.CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@StatusID", statusId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestAggregatedToAllModel requestModel = new RequestAggregatedToAllModel();
                            Debug.WriteLine(reader["Id"]);
                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.Office = Convert.ToString(reader["Office"]);
                            requestModel.Service = Convert.ToString(reader["Service"]);
                            requestModel.Status = Convert.ToString(reader["Status"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            //requestModel.FileData = Convert.ToString(reader["FileData"]);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            returnList.Add(requestModel);
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

        public List<RequestAggregatedToAllModel> GetByOffice(int officeId)
        {
            List<RequestAggregatedToAllModel> returnList = new List<RequestAggregatedToAllModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                    "WHERE OfficeId = @OfficeId " +
                    "ORDER BY Requests.CreatedAt";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@OfficeId", officeId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestAggregatedToAllModel requestModel = new RequestAggregatedToAllModel();
                            Debug.WriteLine(reader["Id"]);
                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.Office = Convert.ToString(reader["Office"]);
                            requestModel.Service = Convert.ToString(reader["Service"]);
                            requestModel.Status = Convert.ToString(reader["Status"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            //requestModel.FileData = Convert.ToString(reader["FileData"]);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            returnList.Add(requestModel);
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

        public List<RequestAggregatedToAllModel> GetByService(int serviceId)
        {
            List<RequestAggregatedToAllModel> returnList = new List<RequestAggregatedToAllModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Requests.Id, Requests.TrackingId, Requests.UserNote, Requests.OfficeNote, Requests.FileName, Offices.Name as Office, Services.Name as Service, Statuses.Name as Status, Requests.CreatedAt, Requests.UpdatedAt, Requests.FinishedAt " +
                    "FROM Requests LEFT JOIN Statuses ON Requests.StatusId=Statuses.Id " +
                    "LEFT JOIN Offices ON Requests.OfficeId=Offices.Id " +
                    "LEFT JOIN  Services ON Requests.ServiceId=Services.Id " +
                    "WHERE ServiceId = @ServiceId " +
                    "ORDER BY Requests.CreatedAt";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@ServiceId", serviceId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestAggregatedToAllModel requestModel = new RequestAggregatedToAllModel();
                            Debug.WriteLine(reader["Id"]);
                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.Office = Convert.ToString(reader["Office"]);
                            requestModel.Service = Convert.ToString(reader["Service"]);
                            requestModel.Status = Convert.ToString(reader["Status"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            //requestModel.FileData = Convert.ToString(reader["FileData"]);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            returnList.Add(requestModel);
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

        public List<RequestModel> GetAll()
        {
            List<RequestModel> returnList = new List<RequestModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Requests ORDER BY CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RequestModel requestModel = new RequestModel();

                            requestModel.Id = Convert.ToInt32(reader["Id"]);
                            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
                            requestModel.OfficeId = Convert.ToInt32(reader["OfficeId"]);
                            requestModel.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                            requestModel.StatusId = Convert.ToInt32(reader["StatusId"]);
                            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
                            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
                            //requestModel.FileData = Convert.ToString(reader["FileData"]);
                            requestModel.FileName = Convert.ToString(reader["FileName"]);
                            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
                            returnList.Add(requestModel);
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