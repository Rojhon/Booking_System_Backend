using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingSystem.Models.Requests
{
    public class RequestUpdateModel
    {
        [Required]
        public int Id { get; set; }
        public string TrackingId { get; set; }
        public int OfficeId { get; set; }
        public int ServiceId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string UserNote { get; set; }
        [Required]
        public string OfficeNote { get; set; }

        public string NewFileData { get; set; }
        public string NewFilePath { get; set; }
        public int NewFileSize { get; set; }
        public string NewFileExtension { get; set; }
        public string NewFileName { get; set; }

        public bool IsFileNew { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}