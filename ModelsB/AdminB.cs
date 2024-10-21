using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB
{
    public class AdminB
    {
        public int AdminMAFPId { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string PasswordHash { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(25)]
        public string Country { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
/*
        public string Role { get; set; } // e.g., SuperAdmin, Manager, etc.
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
*/
    }
}
