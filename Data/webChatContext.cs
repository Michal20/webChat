using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webChat.ViewModels;
using webChat.Models;

namespace webChat.Data
{
    public class webChatContext : DbContext
    {
        public webChatContext (DbContextOptions<webChatContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        //public DbSet<Message> Message { get; set; }
        public DbSet<Conversation> Conversation { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Conversation>()
                .HasKey(x => new { x.UserId, x.id });
        }
    }
}
