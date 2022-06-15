using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BookingSystem.Models.Test;

namespace BookingSystem.Data.Test
{
    public class TestDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Create(TestModel testModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "INSERT INTO Test (Name) Values(@Name)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@Name", testModel.Name);
                command.ExecuteNonQuery();
            }
        }

        public List<TestModel> GetAll()
        {
            List<TestModel> returnList = new List<TestModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Test";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TestModel testModel = new TestModel();
                        testModel.Id = reader.GetInt32(0);
                        testModel.Name = reader.GetString(1);

                        returnList.Add(testModel);
                    }
                }
            }

            return returnList;
        }
    }
}