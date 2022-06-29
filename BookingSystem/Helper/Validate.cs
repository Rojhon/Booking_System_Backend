using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookingSystem.Helper
{
    public class Validate
    {
        public static DateTime? Date(SqlDataReader reader, string name)
        {
            var value = reader[name];
            if (value == DBNull.Value) return null;
            else return Convert.ToDateTime(reader[name]);
        }
    }
}