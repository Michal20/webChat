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
    public class AddMessage
    {
        [Required]
        public string from { get; set; }

        [Required]
        public string to { get; set; }
        [Required]
        public string content { get; set; }

    }
    public class Content
    {
        [Required]
        public string content { get; set; }
    }
}
