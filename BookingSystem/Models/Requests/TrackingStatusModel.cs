using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    public class TrackingStatusModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string TrackingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}