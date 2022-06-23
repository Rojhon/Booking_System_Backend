using System;
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

    }
}