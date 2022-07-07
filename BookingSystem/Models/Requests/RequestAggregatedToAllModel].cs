using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookingSystem.Models.Requests;

namespace BookingSystem.Models.Requests
{
    public class RequestAggregatedToAllModel
    {
        public int Id { get; set; }
        public string TrackingId { get; set; }
        public string Office { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string UserNote { get; set; }
        public string OfficeNote { get; set; }
        public string FileData { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileExtension { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}