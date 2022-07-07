using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using BookingSystem.Models.Authentications;
using Newtonsoft.Json;

namespace BookingSystem.Helper
{
    public class AuthManager
    {
        private static string connectionString = Constants.ConnectionString;

        public static string NewToken(AuthenticationModel authenticationModel, dynamic userData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "INSERT INTO Authentications (Token, UserId, RoleId) Values(@Token, @UserId, @RoleId)";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.AddWithValue("@Token", authenticationModel.Token);
                    command.Parameters.AddWithValue("@UserId", authenticationModel.UserId);
                    command.Parameters.AddWithValue("@RoleId", authenticationModel.RoleId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                string objStringify = JsonConvert.SerializeObject(userData);
                string accessToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(objStringify));

                return $"Success&{authenticationModel.Token}&{accessToken}";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error";
            }
        }

        public static bool UserTokenExist(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM Authentications Where UserId=@UserId";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        connection.Close();
                        return true;
                    }
                    else
                    {
                        connection.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public static bool DeleteUserToken(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Authentications Where UserId=@UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public static bool VerifyToken(string token)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM Authentications Where Token=@Token";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Token", token);
                    SqlDataReader reader = command.ExecuteReader();

                    DateTime expiredAt = new DateTime();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            expiredAt = Convert.ToDateTime(reader["ExpiredAt"]);
                        }

                        if(Validate.DateTimeExpired(DateTime.Now, expiredAt))
                        {
                            Debug.WriteLine("Token Found but already Expired.");
                            // Then Delete the Token in Database
                            connection.Close();
                            return false;
                        }
                        else
                        {
                            Debug.WriteLine("Token is Valid.");
                            connection.Close();
                            return true;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Token Not Found");
                        connection.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public static bool VerifyRole(string token)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Authentications.Id, Authentications.Token, Roles.Name FROM Authentications JOIN Roles ON Authentications.RoleId = Roles.Id Where Token=@Token";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Token", token);
                    SqlDataReader reader = command.ExecuteReader();

                    string roleName = "";

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roleName = Convert.ToString(reader["Name"]);
                        }

                        if(roleName == "Admin")
                        {
                            Debug.WriteLine("Role is Admin");
                            return true;
                        }

                        Debug.WriteLine("Role is Officer");
                        return false;
                    }
                    else
                    {
                        Debug.WriteLine("Token Not Found");
                        connection.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}