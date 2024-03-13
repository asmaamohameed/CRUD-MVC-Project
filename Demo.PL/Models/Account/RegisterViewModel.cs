﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [MinLength(5, ErrorMessage="MinLenght is 5")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
