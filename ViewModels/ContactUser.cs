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
    public class ContactUser
    {
        [Required, MinLength(5), Column(TypeName = "VARCHAR(20)")]
        public string UserName { get; set; }

        [Required, MinLength(5), Column(TypeName = "VARCHAR(20)")]
        public string NickName { get; set; }
        [Required]
        public string Server { get; set; }
    }
}
