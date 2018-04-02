using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace zeMVC.Models
{
    public class DeletedMessages
    {
        public int Id { get; set; }

        [Required, ForeignKey("User"), Index("User_Message", IsUnique = true, Order = 1)]
        public int UserId { get; set; }
        public virtual Users User { get; set; }

        [Required, ForeignKey("Message"), Index("User_Message", IsUnique = true, Order = 2)]
        public int MessageId { get; set; }
        public virtual Messages Message { get; set; }

    }
}