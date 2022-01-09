﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisePoll.Services.ViewModels
{
    public class AuthRegisterViewModel
    {

        [Required(ErrorMessage = "Email is required or already used")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Pseudo is required")]
        [MinLength(5, ErrorMessage = "Pseudo with a minimum length of 5 character")]
        public string Pseudo { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password with a minimum length of '8 character")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and password confirm do not same")]
        public string ConfirmPassword { get; set; }
    }
}
