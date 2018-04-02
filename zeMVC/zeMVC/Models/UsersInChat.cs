using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace zeMVC.Models
{
    public class UsersInChat
    {
        public int Id { get; set; }

        [Required, ForeignKey("Chat"), Index("User_In_Chat", IsUnique = true, Order = 1)]
        public int ChatId { get; set; }
        public virtual Chats Chat { get; set; }

        [Required, ForeignKey("User"), Index("User_In_Chat", IsUnique = true, Order = 2)]
        public int UserId { get; set; }
        public virtual Users User { get; set; }

    }
}