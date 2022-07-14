using System;
using System.Collections.Generic;
using BookingSystem.Models.Users;
using BookingSystem.Models.Authentications;
using System.Data.SqlClient;
using BookingSystem.Helper;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace BookingSystem.Data.User
{
    public class UserDAO
    {
        private string connectionString = Constants.ConnectionString;

        public string InsertOne(UserModel userModel)
        {
            string msg = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Users (FirstName, LastName, RoleId, OfficeId, Email, Password) Values(@FirstName, @LastName, @RoleId, @OfficeId, @Email, @Password)";
                    string password = Hash.HashString(userModel.Password);
                    

                    SqlCommand check_User_Name = new SqlCommand("SELECT * FROM Users WHERE Users.Email=@Email ORDER BY Users.CreatedAt", con);
                    check_User_Name.Parameters.AddWithValue("@Email", userModel.Email);
                    int UserExist = Convert.ToInt32(check_User_Name.ExecuteScalar() ?? 0);

                    if (UserExist > 0)
                    {
                        Debug.WriteLine("Email already exist");
                        Debug.WriteLine(UserExist);
                        msg = "Email already exist";
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand(sqlQuery, con);

                        command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                        command.Parameters.AddWithValue("@LastName", userModel.LastName);
                        command.Parameters.AddWithValue("@RoleId", userModel.RoleId);
                        command.Parameters.AddWithValue("@OfficeId", userModel.RoleId);
                        command.Parameters.AddWithValue("@Email", userModel.Email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                        Debug.WriteLine("Email does not exist");
                        msg = "Success";
                    }

                    con.Close();
                }
                return msg;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error";
            }
        }

        public string UpdateOne(UserModel userModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string password = Hash.HashString(userModel.Password);
                    string query = "Update Users Set FirstName=@FirstName, LastName=@LastName, RoleId=@RoleId, OfficeId=@OfficeId, Email=@Email, Password=@Password, UpdatedAt=GETDATE() Where Id=@Id";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", userModel.LastName);
                    command.Parameters.AddWithValue("@OfficeId", userModel.OfficeId);
                    command.Parameters.AddWithValue("@RoleId", userModel.RoleId);
                    command.Parameters.AddWithValue("@Email", userModel.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Id", userModel.Id);
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

        public dynamic FindOne(int Id)
        {
            try
            {
                dynamic userModel = new ExpandoObject();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Users WHERE Users.Id=@Id ORDER BY Users.CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    Debug.WriteLine(Id);
                    command.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserSqlParser.GetOne(reader, userModel);
                        }
                    }

                    con.Close();
                }

                return userModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new { };
            }
        }

        public dynamic FindOneAggregated(int Id)
        {
            try
            {
                dynamic userModel = new ExpandoObject();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Users.Id, Users.FirstName, Users.LastName, Users.Email, Roles.Name as Role, Offices.Name as Office, Users.CreatedAt, Users.UpdatedAt FROM Users LEFT JOIN Roles ON Users.RoleId = Roles.Id LEFT JOIN Offices ON Users.OfficeId = Offices.Id WHERE Users.Id=@Id ORDER BY Users.CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    Debug.WriteLine(Id);
                    command.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserSqlParser.GetOneAggregated(reader, userModel);
                        }
                    }

                    con.Close();
                }

                return userModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new { };
            }
        }

        public string DeleteOne(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "Delete from Users Where Id=@Id";
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                    con.Close();
                }

                AuthManager.DeleteUserToken(Id);

                return "Deleted";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error";
            }
        }

        public List<dynamic> GetAll()
        {
            List<dynamic> returnList = new List<dynamic>();

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
                            UserSqlParser.GetList(reader, returnList);
                        }
                    }

                    con.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }

        public List<dynamic> GetAllAggregated()
        {
            List<dynamic> returnList = new List<dynamic>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Users.Id, Users.FirstName, Users.LastName, Users.Email, Roles.Name as Role, Offices.Name as Office, Users.CreatedAt, Users.UpdatedAt FROM Users LEFT JOIN Roles ON Users.RoleId = Roles.Id LEFT JOIN Offices ON Users.OfficeId = Offices.Id ORDER BY Users.CreatedAt";

                    SqlCommand command = new SqlCommand(sqlQuery, con);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserSqlParser.GetListAggregated(reader, returnList);
                        }
                    }

                    con.Close();
                }

                return returnList;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return returnList;
            }
        }

        //Login

        public string SignIn(UserModel userModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Users.Id as UserId, Users.FirstName, Users.LastName, Users.Email, Users.Password, Users.OfficeId, Roles.Id as RoleId, Roles.Name as Role " +
                        "FROM Users LEFT JOIN Roles " +
                        "ON Users.RoleId = Roles.Id WHERE Users.Email = @Email";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Email", userModel.Email);
                    SqlDataReader reader = command.ExecuteReader();

                    string response = "";

                    int userId = 0, roleId = 0;
                    string password = "";
                    string userPass = Hash.HashString(userModel.Password);

                    dynamic userData = new ExpandoObject();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            password = Convert.ToString(reader["Password"]);
                            userId = Convert.ToInt32(reader["UserId"]);
                            roleId = Convert.ToInt32(reader["RoleId"]);

                            userData.UserId = Convert.ToInt32(reader["UserId"]);
                            userData.FirstName = Convert.ToString(reader["FirstName"]);
                            userData.LastName = Convert.ToString(reader["LastName"]);
                            userData.Email = Convert.ToString(reader["Email"]);
                            userData.Role = Convert.ToString(reader["Role"]);
                            userData.OfficeId = Convert.ToString(reader["OfficeId"]);
                        }

                        if (password == userPass)
                        {
                            // Credentials Valid
                            userData.Authorized = true;
                            userData.ExpiredAt = DateTime.Now.AddDays(2);

                            AuthenticationModel authenticationModel = new AuthenticationModel();
                            authenticationModel.Token = Generate.Token();
                            authenticationModel.UserId = userId;
                            authenticationModel.RoleId = roleId;

                            // If user have already token, Delete the existing token and Generate a new one
                            if (AuthManager.UserTokenExist(userId) && AuthManager.DeleteUserToken(userId))
                            {
                                Debug.WriteLine("User have already token, Delete the existing token and Generate a new one");
                                return AuthManager.NewToken(authenticationModel, userData);
                            }
                            // No token exist, Generate Token
                            else if(!AuthManager.UserTokenExist(userId))
                            {
                                Debug.WriteLine("No token exist, Generate Token");
                                return AuthManager.NewToken(authenticationModel, userData);
                            }
                            else
                            {
                                Debug.WriteLine("User have already token, but failed to delete the token");
                                return "Error";
                            }
                        }
                        else
                        {
                            response = "Incorrect Password";
                        }
                    }
                    else
                    {
                        response = "Email not found";
                    }
                    connection.Close();
                    return response;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Error";

            }
        }

        public string SignOut(UserModel userModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "Delete from Authentications Where UserId=@UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userModel.Id);
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
    }
}