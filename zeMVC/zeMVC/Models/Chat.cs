using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zeMVC.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }

        public User Users { get; set; }
        public Message Messages { get; set; }

    }
}
