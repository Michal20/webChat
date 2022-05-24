using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace webChat.ViewModels
{
    public class RegisterUser
    {   [Key]
        [Required, MinLength(5), MaxLength(20)]
        public string UserName { get; set; }
        [Required, MinLength(5), MaxLength(20)]
        public string NickName { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage="Password must contain: Minimum 8 characters atleast 1 Alphabet and 1 Number")]
        [Required, MinLength(8), MaxLength(10), DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password"), DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? ProfileImage { get; set; }
    }
}
