using BookingSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookingSystem.Data.User
{
    public class UserSqlParser
    {
        
        public static void GetOneAggregated(SqlDataReader reader, dynamic userModel)
        {
            userModel.Id = Convert.ToInt32(reader["Id"]);
            userModel.FirstName = Convert.ToString(reader["FirstName"]);
            userModel.LastName = Convert.ToString(reader["LastName"]);
            userModel.Role = Convert.ToString(reader["Role"]);
            userModel.Office = Convert.ToString(reader["Office"]);
            userModel.Email = Convert.ToString(reader["Email"]);
            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
        }

        public static void GetOne(SqlDataReader reader, dynamic userModel)
        {
            userModel.Id = Convert.ToInt32(reader["Id"]);
            userModel.FirstName = Convert.ToString(reader["FirstName"]);
            userModel.LastName = Convert.ToString(reader["LastName"]);
            userModel.RoleId = Convert.ToString(reader["RoleId"]);
            userModel.OfficeId = Convert.ToString(reader["OfficeId"]);
            userModel.Email = Convert.ToString(reader["Email"]);
            userModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            userModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
        }

        public static void GetListAggregated(SqlDataReader reader, List<dynamic> returnList)
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

        public static void GetList(SqlDataReader reader, List<dynamic> returnList)
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
}