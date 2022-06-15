using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BookingSystem.Models.Test;
using System.Data;

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

        public void Delete(int Id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Delete from Test Where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(TestModel testModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Update Test Set Name=@Name Where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", testModel.Id);
                cmd.Parameters.AddWithValue("@Name", testModel.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public TestModel Details(int Id)
        {
            TestModel returnList = new TestModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "Select * from Test Where Id=@Id";
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        TestModel testModel = new TestModel();
                        testModel.Id = reader.GetInt32(0);
                        testModel.Name = reader.GetString(1);

                        returnList = testModel;
                    }
                }
            }

            return returnList;
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