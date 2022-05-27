using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
namespace webChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public bool sent { get; set; }
        public string content { get; set; }
        public DateTime created { get; set; }
    }
}
