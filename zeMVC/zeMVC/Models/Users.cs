using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace zeMVC.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(15)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        public string Avatar { get; set; }

    }
}