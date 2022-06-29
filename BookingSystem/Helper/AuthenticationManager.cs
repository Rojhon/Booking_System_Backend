using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BookingSystem.Models.Authentications;

namespace BookingSystem.Helper
{
    public class AuthenticationManager
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string NewToken(AuthenticationModel authenticationModel)
        {
            //Delete existing token if already login
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

                return "Success";
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return "Error";
            }
        }

        public bool VerifyToken(string token)
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

                    //Check if expired
                    connection.Close();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }
    }
}