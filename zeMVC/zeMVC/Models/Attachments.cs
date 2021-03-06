﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace zeMVC.Models
{
    public class Attachments
    {
        public int Id { get; set; }

        [Required]
        public string Link;

        public long FileSize { get; set; }

        public long CDNId { get; set; }

        [Required, ForeignKey("Message")]
        public int MessageId { get; set; }
        public virtual Messages Message { get; set; }
    }
}