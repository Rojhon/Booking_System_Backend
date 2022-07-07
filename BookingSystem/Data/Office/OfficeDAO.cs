using System;
using System.Collections.Generic;
using BookingSystem.Models.Offices;
using System.Data.SqlClient;
using System.Diagnostics;
using BookingSystem.Helper;
using System.Dynamic;

namespace BookingSystem.Data.Office
{
    public class OfficeDAO
    {
        private string connectionString = Constants.ConnectionString;

        public string InsertOne(OfficeModel officeModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "INSERT INTO Offices (Name) Values(@Name)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        command.Parameters.AddWithValue("@Name", officeModel.Name);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

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

        public dynamic FindOne(string Id)
        {
            dynamic officeModel = new ExpandoObject();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Offices Where Id=@Id";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            officeModel.Id = Convert.ToInt32(reader["Id"]);
                            officeModel.Name = Convert.ToString(reader["Name"]);
                            officeModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            officeModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);

                        }
                    }

                    connection.Close();
                }

                return officeModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new { };
            }
        }

        public string UpdateOne(OfficeModel officeModel, bool isModelValidate)
        {
            if (isModelValidate)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "Update Offices Set Name=@Name, UpdatedAt=GETDATE() Where Id=@Id";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Id", officeModel.Id);
                        command.Parameters.AddWithValue("@Name", officeModel.Name);
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

        public string DeleteOne(string Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Offices Where Id=@Id";
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

        public List<OfficeModel> GetAll()
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

    }
}