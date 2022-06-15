using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models
{
    public class OfficeModel
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}