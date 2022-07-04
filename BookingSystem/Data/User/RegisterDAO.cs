using System;
using System.Collections.Generic;
using BookingSystem.Models.Users;
using System.Data.SqlClient;
using BookingSystem.Helper;
using System.Linq;
using System.Web;

namespace BookingSystem.Data.User
{
    public class RegisterDAO
    {
        private string connectionString = Constants.ConnectionString;

        public string InsertOne(UserModel UserModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Users (UserNumber, FirstName, LastName, Position, Email, Password, CreatedAt) Values(@UserNumber, @FirstName, @LastName, @Position, @Email, @Password, GETDATE())";
                    string password = Hash.HashString(UserModel.Password);

                    SqlCommand command = new SqlCommand(sqlQuery, con);

                    command.Parameters.AddWithValue("@UserNumber", UserModel.UserNumber);
                    command.Parameters.AddWithValue("@FirstName", UserModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", UserModel.LastName);
                    command.Parameters.AddWithValue("@Position", UserModel.Position);
                    command.Parameters.AddWithValue("@Email", UserModel.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                    con.Close();
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
                    con.Open();
                    string sqlQuery = "Update Requests Set UserNumber=@UserNumber, FirstName=@FirstName, LastName=@LastName, Position=@Position, Email=@Email, Password=@Password UpdatedAt=GETDATE() Where UserNumber=@UserNumber";
                    string password = Hash.HashString(UserModel.Password);

                    SqlCommand command = new SqlCommand(sqlQuery, con);

                    command.Parameters.AddWithValue("@UserNumber", UserModel.UserNumber);
                    command.Parameters.AddWithValue("@FirstName", UserModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", UserModel.LastName);
                    command.Parameters.AddWithValue("@Position", UserModel.Position);
                    command.Parameters.AddWithValue("@Email", UserModel.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                    con.Close();
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Users Where UserNumber=@UserNumber";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@UserNumber", UserNumber);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();
                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.UserNumber = Convert.ToString(reader["UserNumber"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Position = Convert.ToString(reader["Position"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            userList.Add(userModel);
                        }
                    }

                    con.Close();
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "Delete from Users Where UserNumber=@UserNumber";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@UserNumber", UserNumber);
                    command.ExecuteNonQuery();
                    con.Close();
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Users";

                    SqlCommand command = new SqlCommand(sqlQuery, con);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();

                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.UserNumber = Convert.ToString(reader["UserNumber"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Position = Convert.ToString(reader["Position"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
                            returnList.Add(userModel);
                        }
                    }

                    con.Close();
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