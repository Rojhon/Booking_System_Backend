using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using BookingSystem.Helper;
using BookingSystem.Models.Requests;

namespace BookingSystem.Controllers.Request
{
    public class RequestSqlParser
    {
        public void GetOneAggregated (SqlDataReader reader, dynamic requestModel)
        {
            requestModel.Id = Convert.ToInt32(reader["Id"]);
            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
            requestModel.Office = Convert.ToString(reader["Office"]);
            requestModel.Service = Convert.ToString(reader["Service"]);
            requestModel.Status = Convert.ToString(reader["Status"]);
            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
            //requestModel.FileData = Convert.ToString(fileBase64);
            requestModel.FileName = Convert.ToString(reader["FileName"]);
            requestModel.FileSize = Convert.ToInt32(reader["FileSize"]);
            requestModel.FileExtension = Convert.ToString(reader["FileExtension"]);
            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
        }

        public void GetOne(SqlDataReader reader, dynamic requestModel)
        {
            requestModel.Id = Convert.ToInt32(reader["Id"]);
            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
            requestModel.OfficeId = Convert.ToInt32(reader["OfficeId"]);
            requestModel.ServiceId = Convert.ToInt32(reader["ServiceId"]);
            requestModel.StatusId = Convert.ToInt32(reader["StatusId"]);
            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
            //requestModel.FileData = Convert.ToString(fileBase64);
            requestModel.FileName = Convert.ToString(reader["FileName"]);
            requestModel.FileSize = Convert.ToInt32(reader["FileSize"]);
            requestModel.FileExtension = Convert.ToString(reader["FileExtension"]);
            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
        }

        public void GetListAggregated(SqlDataReader reader, List<dynamic> returnList)
        {
            RequestAggregatedToAllModel requestModel = new RequestAggregatedToAllModel();
            requestModel.Id = Convert.ToInt32(reader["Id"]);
            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
            requestModel.Office = Convert.ToString(reader["Office"]);
            requestModel.Service = Convert.ToString(reader["Service"]);
            requestModel.Status = Convert.ToString(reader["Status"]);
            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
            //requestModel.FileData = Convert.ToString(reader["FileData"]);
            requestModel.FileName = Convert.ToString(reader["FileName"]);
            requestModel.FileSize = Convert.ToInt32(reader["FileSize"]);
            requestModel.FileExtension = Convert.ToString(reader["FileExtension"]);
            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
            returnList.Add(requestModel);
        }

        public void GetList(SqlDataReader reader, List<dynamic> returnList)
        {
            RequestModel requestModel = new RequestModel();
            requestModel.Id = Convert.ToInt32(reader["Id"]);
            requestModel.TrackingId = Convert.ToString(reader["TrackingId"]);
            requestModel.OfficeId = Convert.ToInt32(reader["OfficeId"]);
            requestModel.ServiceId = Convert.ToInt32(reader["ServiceId"]);
            requestModel.StatusId = Convert.ToInt32(reader["StatusId"]);
            requestModel.UserNote = Convert.ToString(reader["UserNote"]);
            requestModel.OfficeNote = Convert.ToString(reader["OfficeNote"]);
            //requestModel.FileData = Convert.ToString(reader["FileData"]);
            requestModel.FileName = Convert.ToString(reader["FileName"]);
            requestModel.FileSize = Convert.ToInt32(reader["FileSize"]);
            requestModel.FileExtension = Convert.ToString(reader["FileExtension"]);
            requestModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            requestModel.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            requestModel.FinishedAt = Validate.Date(reader, "FinishedAt");
            returnList.Add(requestModel);
        }

    }
}