using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisePoll.Services.ViewModels
{
    public class AuthLoginViewModel
    {

        [Required(ErrorMessage = "Email is required or already used")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool StayLog { get; set;}
    }
}
