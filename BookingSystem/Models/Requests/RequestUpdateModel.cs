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
        [Required]
        public string TrackingId { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string UserNote { get; set; }
        public string OfficeNote { get; set; }

        public string NewFileData { get; set; }
        public string NewFilePath { get; set; }
        public int NewFileSize { get; set; }
        public string NewFileExtension { get; set; }
        public string NewFileName { get; set; }

        [Required]
        public bool IsFileNew { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}