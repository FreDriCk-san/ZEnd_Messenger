using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace zeMVC.Models
{
    public class MessangerContext : DbContext
    {

        public MessangerContext() : base("name=ZEnd_DB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<zeMVC.Models.Chats> Chats { get; set; }

        public System.Data.Entity.DbSet<zeMVC.Models.Messages> Messages { get; set; }

        public System.Data.Entity.DbSet<zeMVC.Models.Users> Users { get; set; }
    }
}