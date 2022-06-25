using System;
using System.Collections.Generic;
using BookingSystem.Models.Users;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookingSystem.Data.User
{
    public class LoginDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string UpdateOne(UserModel UserModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "Update Requests Set Email=@Email, Password=@Password UpdatedAt=GETDATE() Where UserNumber=@UserNumber";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

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
        public List<UserModel> FindOne(string Email)
        {
            List<UserModel> userList = new List<UserModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM Users Where Email=@Email";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Email", Email);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.Password = Convert.ToString(reader["Password"]);
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
    }
}