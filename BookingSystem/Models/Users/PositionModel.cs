using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Users
{
    public class PositionModel
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}