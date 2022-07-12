using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Helper
{
    public class Constants
    {
        //This connection string is for Local Sql Server
        public const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookingSystemDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //This connection string is for Microsoft Sql Server
        //public const string ConnectionString = @"Data Source=DESKTOP-KHLERAQ\MSSQLSERVER01;Initial Catalog=BookingSystemDatabase;Integrated Security=True";

        //Edit for FilePath
        public static string Path = HttpContext.Current.Server.MapPath("~/Files"); //Path

        public const string LocalPath = @"c:\BookingSystemFiles";
        

    }
}