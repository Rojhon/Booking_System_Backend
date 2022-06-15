using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int TrackingId { get; set; }
        public int OfficeId { get; set; }
        public int ServiceID { get; set; }
        public int StatusId { get; set; }
        public string UserNote { get; set; }
        public string OfficeNote { get; set; }
        public string LinkToFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}