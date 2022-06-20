using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingSystem.Models.Requests;

namespace BookingSystem.Models.Requests
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string TrackingId { get; set; }
        public int OfficeId { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; }
        public string UserNote { get; set; }
        public string OfficeNote { get; set; }
        public string FileData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}