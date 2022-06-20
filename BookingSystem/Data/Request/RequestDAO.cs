using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingSystem.Models;
using BookingSystem.Models.Requests;
using BookingSystem.Models.Offices;
using BookingSystem.Models.Services;
using System.Data.SqlClient;

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
                    string sqlQuery = "INSERT INTO Requests (OfficeId, ServiceId, UserNote) Values(@OfficeId, @ServiceId, @UserNote)";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.AddWithValue("@OfficeId", requestModel.OfficeId);
                    command.Parameters.AddWithValue("@ServiceId", requestModel.ServiceId);
                    command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);

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

        public RequestModel FindOne(string trackingId)
        {
            RequestModel requestModel = new RequestModel();

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
                        while (reader.Read())
                        {
                            requestModel.Id = reader.GetInt32(0);
                            requestModel.TrackingId = reader.GetString(1);
                            requestModel.OfficeId = reader.GetInt32(2);
                            requestModel.ServiceId = reader.GetInt32(3);
                            requestModel.Status = reader.GetString(4);
                            requestModel.UserNote = reader.GetString(5);
                            requestModel.CreatedAt = reader.GetDateTime(8);
                            requestModel.UpdatedAt = reader.GetDateTime(9);
                        }
                    }

                    connection.Close();
                }

                return requestModel;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return requestModel;
            }
        }

        public string UpdateOne(RequestModel requestModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Update Requests Set UserNote=@UserNote, Status=@Status Where TrackingId=@TrackingId";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@TrackingId", requestModel.TrackingId);
                    command.Parameters.AddWithValue("@UserNote", requestModel.UserNote);
                    command.Parameters.AddWithValue("@Status", requestModel.Status);
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

                            officeModel.Id = reader.GetInt32(0);
                            officeModel.Name = reader.GetString(1);

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

                            serviceModel.Id = reader.GetInt32(0);
                            serviceModel.Name = reader.GetString(1);
                            serviceModel.Fee = reader.GetDecimal(2);
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

                            requestModel.Id = reader.GetInt32(0);
                            requestModel.TrackingId = reader.GetString(1);
                            requestModel.OfficeId = reader.GetInt32(2);
                            requestModel.ServiceId = reader.GetInt32(3);
                            requestModel.Status = reader.GetString(4);
                            requestModel.UserNote = reader.GetString(5);
                            requestModel.OfficeNote = reader.GetString(6);
                            requestModel.CreatedAt = reader.GetDateTime(8);
                            requestModel.UpdatedAt = reader.GetDateTime(9);
                            requestModel.FinishedAt = reader.GetDateTime(10);

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