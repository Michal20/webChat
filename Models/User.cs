using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace webChat.Models
{
    public class User
    {
        public User() : base()
        {
            Conversations = new List<Conversation>();
        }
        [Key]
        [Required, MinLength(5), Column(TypeName = "VARCHAR(20)")]
        public string UserName { get; set; }

        [Required, MinLength(5), Column(TypeName = "VARCHAR(20)")]
        public string NickName { get; set; }
        
        [Required, MinLength(5), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Column(TypeName = "VARCHAR(200)")]
        public string ProfilePicture { get; set; }

        public ICollection<Conversation> Conversations { get; set; }

    }
}
