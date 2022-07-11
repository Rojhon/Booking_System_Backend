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
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Users (FirstName, LastName, RoleId, OfficeId, Email, Password) Values(@FirstName, @LastName, @RoleId, @OfficeId, @Email, @Password)";
                    string password = Hash.HashString(userModel.Password);

                    SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Users.Email=@Email" , con);
                    check_User_Name.Parameters.AddWithValue("@Email", userModel.Email);
                    int UserExist = (int)check_User_Name.ExecuteScalar();

                    if(UserExist > 0)
                    {
                        Debug.WriteLine("Email exist");
                       //Username exist
                    }
                    else
                    {
                        Debug.WriteLine("Email exist");
                       //Username doesn't exist.
                    }

                    SqlCommand command = new SqlCommand(sqlQuery, con);

                    command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", userModel.LastName);
                    command.Parameters.AddWithValue("@RoleId", userModel.RoleId);
                    command.Parameters.AddWithValue("@OfficeId", userModel.OfficeId);
                    command.Parameters.AddWithValue("@Email", userModel.Email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                    con.Close();
                }

                    return "Success";
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
                    string query = "Update Users Set FirstName=@FirstName, LastName=@LastName, RoleId=@RoleId, Email=@Email, Password=@Password, UpdatedAt=GETDATE() Where Id=@Id";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", userModel.LastName);
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
                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.OfficeId = Convert.ToString(reader["OfficeId"]);
                            userModel.RoleId = Convert.ToString(reader["RoleId"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
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
                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Office = Convert.ToString(reader["Office"]);
                            userModel.Role = Convert.ToString(reader["Role"]);
                            userModel.Email = Convert.ToString(reader["Email"]);
                            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
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

        public string DeleteOne(string Id)
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

                return "Deleted";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
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
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.RoleId = Convert.ToInt32(reader["RoleId"]);
                            userModel.OfficeId = Convert.ToInt32(reader["OfficeId"]);
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
                Debug.WriteLine(e);
                return returnList;
            }
        }

        public List<UserAggregatedModel> GetAllAggregated()
        {
            List<UserAggregatedModel> returnList = new List<UserAggregatedModel>();

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
                            UserAggregatedModel userModel = new UserAggregatedModel();

                            userModel.Id = Convert.ToInt32(reader["Id"]);
                            userModel.FirstName = Convert.ToString(reader["FirstName"]);
                            userModel.LastName = Convert.ToString(reader["LastName"]);
                            userModel.Role = Convert.ToString(reader["Role"]);
                            userModel.Office = Convert.ToString(reader["Office"]);
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