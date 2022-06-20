using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    // Wala na to, pang ready nalang kung gagamitin.
    public class RequestStatusModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}