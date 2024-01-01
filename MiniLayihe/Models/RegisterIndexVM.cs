using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiniLayihe.Models
{
	public class RegisterIndexVM
	{
        [Required(ErrorMessage = "Enter your Name")]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Enter correct Email adress")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Enter correct Phone Number")]
        [RegularExpression("^\\(?([0-9]{2})\\)?[-.●]?([0-9]{3})[-.●]?([0-9]{2})?([0-9]{2})$")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string? ConfirmPassword { get; set; }
    }
}

