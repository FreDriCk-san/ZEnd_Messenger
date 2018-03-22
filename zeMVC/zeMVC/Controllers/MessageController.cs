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
    [Route("api/Message")]
    public class MessageController : Controller
    {

        private readonly MessangerContext context;

        public MessageController(MessangerContext _context)
        {
            context = _context;

            if (context.Messages.Count() == 0)
            {
                context.Messages.Add(new Message { Text = "Hello world!", Emoji = "", SendTime = DateTime.Parse("00:00:00") });
                context.SaveChanges();
            }

        }

        [HttpGet]
        public IEnumerable<Message> GetMessages()
        {
            return context.Messages.ToList();
        }

        [HttpGet("{id}", Name = "GetMessages")]
        public IActionResult GetById(int id)
        {
            var item = context.Messages.FirstOrDefault(t => t.MessageId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //Uses response "HTTP POST. 201" to create
        [HttpPost]
        public IActionResult Create([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            context.Messages.Add(message);
            context.SaveChanges();

            return CreatedAtRoute("GetMessage", new { id = message.MessageId }, message);
        }

        //Uses response "HTTP PUT. 204" to update
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Message message)
        {
            if (message == null || message.MessageId != id)
            {
                return BadRequest();
            }

            var upd = context.Messages.FirstOrDefault(t => t.MessageId == id);
            if (upd == null)
            {
                return NotFound();
            }

            upd.Text = message.Text;
            upd.Emoji = message.Emoji;
            upd.SendTime = message.SendTime;

            context.Messages.Update(upd);
            context.SaveChanges();
            return new NoContentResult();
        }


        //Uses response "HTTP DELETE. 204" to delete
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = context.Messages.FirstOrDefault(t => t.MessageId == id);
            if (del == null)
            {
                return NotFound();
            }

            context.Messages.Remove(del);
            context.SaveChanges();
            return new NoContentResult();
        }
    }
}