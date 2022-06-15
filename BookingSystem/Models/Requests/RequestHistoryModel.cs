using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    public class RequestHistoryModel
    {
        public int Id { get; set; }
        public string History { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}