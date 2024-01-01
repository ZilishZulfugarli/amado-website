using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MiniLayihe.Entities
{
	public class AppUser : IdentityUser
	{
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Country { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UserCreateTime { get; set; } = DateTime.Now;
    }
}

