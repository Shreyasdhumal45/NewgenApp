using System.ComponentModel.DataAnnotations;

namespace NewgenApp.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display( Name="remember me?")]
        public bool RememberMe { get; set; }


    }
}
