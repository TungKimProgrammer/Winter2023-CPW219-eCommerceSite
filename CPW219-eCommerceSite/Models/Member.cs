﻿using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Member
    {
        [Key] 
        public int MemberId { get; set; }

        public string Email { get; set; }   

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name ="Confirm Email")]
        public string ConfirmedEmail { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name ="Confirm Password")]
        public string ConfirmedPassword { get; set; }
    }
}
