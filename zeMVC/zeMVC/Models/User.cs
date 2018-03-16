using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zeMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Avatar { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Chat> Chat { get; set; }
    }
}
