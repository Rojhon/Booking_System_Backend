using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Service
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceFee { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}