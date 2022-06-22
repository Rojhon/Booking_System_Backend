using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingSystem.Models;
using BookingSystem.Models.Requests;
using BookingSystem.Models.Offices;
using BookingSystem.Models.Services;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Data.SqlTypes;
using BookingSystem.Helper;

namespace BookingSystem.Data.Request
{
    public class RequestDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string InsertOne(RequestModel requestModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO Requests (OfficeId, ServiceId, UserNote, FileData, FileName) Values(@OfficeId, @ServiceId, @UserNote, @FileData, @FileName)";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.AddWithValue("@OfficeId", requestModel.OfficeId);
                    command.Parameters.AddWithValue("@ServiceId", requestModel.ServiceId);
                    command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);
                    command.Parameters.AddWithValue("@FileData", requestModel.FileData);
                    command.Parameters.AddWithValue("@FileName", requestModel.FileName);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                    return "Success";
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Error";
            }
        }

        public List<RequestModel> FindOne(string trackingId)
        {
            List<RequestModel> requestList = new List<RequestModel>();
            Debug.WriteLine(trackingId);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Requests Where TrackingId=@TrackingId";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Debug.WriteLine("not null");
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
                            requestModel.FileData = Convert.ToString(reader["FileData"]);
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
                System.Diagnostics.Debug.WriteLine(e);
                return requestList;
            }
        }

        public string UpdateOne(RequestModel requestModel)
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
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Error";
            }
        }

        public string DeleteOne(string trackingId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Requests Where TrackingId=@TrackingId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingId", trackingId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return "Deleted";
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Error";
            }
        }

        public List<OfficeModel> GetOffices()
        {
            List<OfficeModel> returnList = new List<OfficeModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Offices";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            OfficeModel officeModel = new OfficeModel();

                            officeModel.Id = Convert.ToInt32(reader["Id"]);
                            officeModel.Name = Convert.ToString(reader["Name"]);

                            returnList.Add(officeModel);
                        }
                    }

                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return returnList;
            }
        }

        public List<ServiceModel> GetServices()
        {
            List<ServiceModel> returnList = new List<ServiceModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Services";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServiceModel serviceModel = new ServiceModel();

                            serviceModel.Id = Convert.ToInt32(reader["Id"]);
                            serviceModel.Name = Convert.ToString(reader["Name"]);
                            serviceModel.Fee = Convert.ToDecimal(reader["Fee"]);
                            returnList.Add(serviceModel);
                        }
                    }

                    connection.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
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
                    string sqlQuery = "SELECT * FROM Requests";

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
                            requestModel.FileData = Convert.ToString(reader["FileData"]);
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
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return returnList;
            }
        }


        // It's working but the code is a mess.
        //public void Create(ModelHandler modelHandler)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Query
        //        string officeQuery = "INSERT INTO Offices (Name) Values(@OfficeName); SELECT CAST(scope_identity() AS int)";
        //        string serviceQuery = "INSERT INTO Services (Name, Fee) Values(@ServiceName, @ServiceFee); SELECT CAST(scope_identity() AS int)";
        //        string statusQuery = "INSERT INTO RequestStatuses (Type) Values(@StatusType); SELECT CAST(scope_identity() AS int)";
        //        string requestQuery = "INSERT INTO Requests " +
        //            "(OfficeId, ServiceId, StatusId, UserNote) " +
        //            "Values(@RequestOfficeId," +
        //            " @RequestServiceId," +
        //            " @RequestStatusId," +
        //            " @RequestUserNote); SELECT CAST(scope_identity() AS int)";

        //        // Commands
        //        SqlCommand officeCommand = new SqlCommand(officeQuery, connection);
        //        SqlCommand serviceCommand = new SqlCommand(serviceQuery, connection);
        //        SqlCommand statusCommand = new SqlCommand(statusQuery, connection);
        //        SqlCommand requestCommand = new SqlCommand(requestQuery, connection);

        //        // Ids


        //        // Parameter
        //        officeCommand.Parameters.AddWithValue("@OfficeName", modelHandler.officeModel.Name);

        //        serviceCommand.Parameters.AddWithValue("@ServiceName", modelHandler.serviceModel.Name);
        //        serviceCommand.Parameters.AddWithValue("@ServiceFee", modelHandler.serviceModel.Fee);

        //        statusCommand.Parameters.AddWithValue("@StatusType", modelHandler.requestStatusModel.Type);

        //        int officeId = int.Parse(officeCommand.ExecuteScalar().ToString());
        //        int serviceId = int.Parse(serviceCommand.ExecuteScalar().ToString());
        //        int statusId = int.Parse(statusCommand.ExecuteScalar().ToString());

        //        requestCommand.Parameters.AddWithValue("@RequestOfficeId", officeId);
        //        requestCommand.Parameters.AddWithValue("@RequestServiceId", serviceId);
        //        requestCommand.Parameters.AddWithValue("@RequestStatusId", statusId);
        //        requestCommand.Parameters.AddWithValue("@RequestUserNote", modelHandler.requestModel.UserNote);

        //        // Execute
        //        officeCommand.ExecuteNonQuery();
        //        serviceCommand.ExecuteNonQuery();
        //        statusCommand.ExecuteNonQuery();
        //        requestCommand.ExecuteNonQuery();
        //    }
        //}
    }
}