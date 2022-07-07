using System;
using System.Collections.Generic;
using BookingSystem.Models.Services;
using System.Data.SqlClient;
using BookingSystem.Helper;
using System.Dynamic;

namespace BookingSystem.Data.Service
{
    public class ServiceDAO
    {
        private string connectionString = Constants.ConnectionString;

        public string InsertOne(ServiceModel serviceModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO Services (Name,Fee) Values(@Name, @Fee)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        command.Parameters.AddWithValue("@Name", serviceModel.Name);
                        command.Parameters.AddWithValue("@Fee", serviceModel.Fee);
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
            else return "The data sent are missing some field/s.";
        }

        public dynamic FindOne(string Id)
        {
            dynamic serviceModel = new ExpandoObject();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Services Where Id=@Id";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            serviceModel.Id = Convert.ToInt32(reader["Id"]);
                            serviceModel.Name = Convert.ToString(reader["Name"]);
                            serviceModel.Fee = Convert.ToDecimal(reader["Fee"]);
                            serviceModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            serviceModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                        }
                    }

                    connection.Close();
                }

                return serviceModel;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return new { };
            }
        }

        public string UpdateOne(ServiceModel serviceModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "Update Services Set Name=@Name, Fee=@Fee, UpdatedAt=GETDATE() Where Id=@Id";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Id", serviceModel.Id);
                        command.Parameters.AddWithValue("@Name", serviceModel.Name);
                        command.Parameters.AddWithValue("@Fee", serviceModel.Fee);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    return "Updated";
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return "Error";
                }
            }
            else return "The data sent are missing some field/s.";
        }

        public string DeleteOne(string Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Services Where Id=@Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", Id);
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

        public List<ServiceModel> GetAll()
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
                            serviceModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            serviceModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
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

    }
}