using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BookingSystem.Models.Requests;

namespace BookingSystem.Models.Requests
{
    public class RequestModel
    {
        public int Id { get; set; }
        public string TrackingId { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public int StatusId { get; set; }
        public string UserNote { get; set; }
        public string OfficeNote { get; set; }
        [Required]
        public string FileData { get; set; }
        //[Required]
        public string FilePath { get; set; }
        [Range(0, 5242880)]
        public int FileSize { get; set; }
        [Required]
        public string FileExtension { get; set; }
        [Required]
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}