using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.G04.Core.Dtos.Auth
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Display name is required")]
        public string DisplayName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(
    @"^\+201[0125]\d{8}$",
    ErrorMessage = "Phone number must be a valid Egyptian mobile number starting with +20."
)]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

    
    }
}
