using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int OfficeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.!@#$%&'*+/=?^_`{|}~-]).{8,100}$")]
        public string Password { get; set; }
        //public string Verified { get; set; }
        //public string Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}