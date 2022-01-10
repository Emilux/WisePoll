using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisePoll.Services.ViewModels
{
    public class AuthRegisterViewModel
    {
        private const string V = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

        [Required(ErrorMessage = "Email is required or already used")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Pseudo is required")]
        [MinLength(5, ErrorMessage = "Pseudo with a minimum length of 5 character")]
        public string Pseudo { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(V, ErrorMessage = "Password with minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        [MinLength(8, ErrorMessage = "Password with a minimum length of 8 character")]
        [MaxLength(50, ErrorMessage = "Password with a maximum length of 50 character")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and password confirm do not same")]
        public string ConfirmPassword { get; set; }
    }
}
