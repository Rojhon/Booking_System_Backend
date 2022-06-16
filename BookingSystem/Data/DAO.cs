using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookingSystem.Data
{
    public class DAO<T> where T : class, new()
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void InsertOne(string sqlQuery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public List<T> GetAll(string sqlQuery)
        {
            List<T> returnList = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dynamic testModel = new T();
                        testModel.Id = reader.GetInt32(0);
                        testModel.Name = reader.GetString(1);

                        returnList.Add(testModel);
                    }
                }
            }

            return returnList;
        }

        public void DeleteById(string sqlQuery)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            }
        }
    }
}