﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Data.User
{
    public class RegisterDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string InsertOne(UserModel UserModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO Users (UserNumber, FirstName, LastName, Position, Email, Password, CreatedAt) Values(@UserNumber, @FirstName, @LastName, @Position, @Email, @Password, GETDATE())";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.AddWithValue("@UserNumber", UserModel.UserNumber);
                    command.Parameters.AddWithValue("@FirstName", UserModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", UserModel.LastName);
                    command.Parameters.AddWithValue("@Position", UserModel.Position);
                    command.Parameters.AddWithValue("@Email", UserModel.Email);
                    command.Parameters.AddWithValue("@Password", UserModel.Password);
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
        public string UpdateOne(UserModel UserModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Update Requests Set UserNumber=@UserNumber, FirstName=@FirstName, LastName=@LastName, Position=@Position, Email=@Email, Password=@Password UpdatedAt=GETDATE() Where UserNumber=@UserNumber";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.AddWithValue("@UserNumber", UserModel.UserNumber);
                    command.Parameters.AddWithValue("@FirstName", UserModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", UserModel.LastName);
                    command.Parameters.AddWithValue("@Position", UserModel.Position);
                    command.Parameters.AddWithValue("@Email", UserModel.Email);
                    command.Parameters.AddWithValue("@Password", UserModel.Password);
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
        public List<UserModel> FindOne(string UserNumber)
        {
            List<UserModel> userList = new List<UserModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Users Where UserNumber=@UserNumber";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@UserNumber", UserNumber);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();
                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.UserNumber = Convert.ToDecimal(reader["UserNumber"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Position = Convert.ToString(reader["Position"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            userList.Add(userModel);
                        }
                    }

                    connection.Close();
                }

                return userList;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return userList;
            }
        }
        public string DeleteOne(string UserNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Users Where UserNumber=@UserNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserNumber", UserNumber);
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
        public List<UserModel> GetAll()
        {
            List<UserModel> returnList = new List<UserModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Users";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();

                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.UserNumber = Convert.ToDecimal(reader["UserNumber"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Position = Convert.ToString(reader["Position"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            returnList.Add(userModel);
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