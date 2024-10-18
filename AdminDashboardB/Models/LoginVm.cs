using System.ComponentModel.DataAnnotations;

namespace AdminDashboardB.Models
{
    public class LoginVm
    {
       [Required(ErrorMessage = "Wrong Email ")]

        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
