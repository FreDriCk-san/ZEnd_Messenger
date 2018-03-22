using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zeMVC.Data;
using zeMVC.Models;

namespace zeMVC.Controllers
{
    [Produces("application/json")]
    [Route("api/Users2")]
    public class Users2Controller : Controller
    {

        private readonly MessangerContext context;

        public Users2Controller(MessangerContext _context)
        {
            context = _context;

            if (context.Users.Count() == 0)
            {
                context.Users.Add(new User { Login = "Sen", Password = "Pai", Name = "SeniCh", Mail = "sen@gmail.com", Avatar = "", EnrollmentDate = DateTime.Parse("11-12-2018") });
                context.SaveChanges();
            }

        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(int id)
        {
            var item = context.Users.FirstOrDefault(t => t.UserId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //Uses response "HTTP POST. 201" to create
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            context.Users.Add(user);
            context.SaveChanges();

            return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
        }

        //Uses response "HTTP PUT. 204" to update
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest();
            }

            var upd = context.Users.FirstOrDefault(t => t.UserId == id);
            if (upd == null)
            {
                return NotFound();
            }

            upd.Login = user.Login;
            upd.Password = user.Password;
            upd.Name = user.Name;
            upd.Mail = user.Mail;
            upd.Avatar = user.Avatar;
            upd.EnrollmentDate = user.EnrollmentDate;

            context.Users.Update(upd);
            context.SaveChanges();
            return new NoContentResult();
        }


        //Uses response "HTTP DELETE. 204" to delete
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = context.Users.FirstOrDefault(t => t.UserId == id);
            if (del == null)
            {
                return NotFound();
            }

            context.Users.Remove(del);
            context.SaveChanges();
            return new NoContentResult();
        }
    }
}