using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    public class File
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public string Data { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
    }
}