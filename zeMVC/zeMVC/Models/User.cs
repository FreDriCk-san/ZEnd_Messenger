using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zeMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(15, ErrorMessage = "Login cannot be longer than 15 and less 5 characters.", MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z""'\s-]*$", ErrorMessage = "Use english characters A-z")]
        public string Login { get; set; }
        [StringLength(20, ErrorMessage = "Password cannot be longer than 20 and less 5 characters", MinimumLength = 5)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Avatar { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Chat> Chat { get; set; }
    }
}
