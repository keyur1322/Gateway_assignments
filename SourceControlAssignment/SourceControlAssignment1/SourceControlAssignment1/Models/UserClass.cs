using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SourceControlAssignment1.Models
{
    public class UserClass
    {


        [Required]
        [Display(Name = "Enter your Username")]
        [StringLength(maximumLength: 8, MinimumLength = 4, ErrorMessage = "Please enter your username character between 4 and 7 character")]
        public string Username { get; set; }


        [Required]
        [Display(Name = "Enter your Age")]
        [Range(18, 90, ErrorMessage = "Please User age should be between 18 to 90")]
        public int Age { get; set; }


        [Required]
        [Display(Name = "Enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter your email address properly")]
        public String Email { get; set; }


        [Required]
        [Display(Name = "Enter your mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Please enter valid mobile number")]
        public string Phoneno { get; set; }


        [Required]
        [Display(Name = "Enter your password")]
        [DataType(DataType.Password)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please your password must have contain one digit, lowercase and uppercase")]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Repassword { get; set; }


        [Display(Name = "Enter your gender")]
        [Required(ErrorMessage = "Please enter your gender")]
        public string Gender { get; set; }


        [Required]
        [Display(Name = "Choose terms & condition")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept Terms & condition for regular update")]
        public bool Termsandcondition { get; set; }
    }
}