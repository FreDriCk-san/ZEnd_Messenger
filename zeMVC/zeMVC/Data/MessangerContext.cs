using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zeMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace zeMVC.Data
{
    public class MessangerContext : DbContext
    {

        public MessangerContext(DbContextOptions<MessangerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Chat>().ToTable("Chat");
        }
    }
}
