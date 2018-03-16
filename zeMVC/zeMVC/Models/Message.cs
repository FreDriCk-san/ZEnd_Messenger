using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zeMVC.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Emoji { get; set; }
        public DateTime SendTime { get; set; }

        public ICollection<Chat> Chat { get; set; }

    }
}
