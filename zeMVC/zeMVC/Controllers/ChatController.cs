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
    [Route("api/Chat")]
    public class ChatController : Controller
    {

        private readonly MessangerContext context;

        public ChatController(MessangerContext _context)
        {
            context = _context;

            if (context.Chats.Count() == 0)
            {
                context.Chats.Add(new Chat { UserId = 0, MessageId = 0 });
                context.SaveChanges();
            }

        }

        [HttpGet]
        public IEnumerable<Chat> GetChats()
        {
            return context.Chats.ToList();
        }

        [HttpGet("{id}", Name = "GetChats")]
        public IActionResult GetById(int id)
        {
            var item = context.Chats.FirstOrDefault(t => t.ChatId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //Uses response "HTTP POST. 201" to create
        [HttpPost]
        public IActionResult Create([FromBody] Chat chat)
        {
            if (chat == null)
            {
                return BadRequest();
            }

            context.Chats.Add(chat);
            context.SaveChanges();

            return CreatedAtRoute("GetChat", new { id = chat.ChatId }, chat);
        }

        //Uses response "HTTP PUT. 204" to update
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Chat chat)
        {
            if (chat == null || chat.ChatId != id)
            {
                return BadRequest();
            }

            var upd = context.Chats.FirstOrDefault(t => t.ChatId == id);
            if (upd == null)
            {
                return NotFound();
            }

            upd.UserId = chat.UserId;
            upd.MessageId = chat.MessageId;

            context.Chats.Update(upd);
            context.SaveChanges();
            return new NoContentResult();
        }


        //Uses response "HTTP DELETE. 204" to delete
        //Do not forget to put "../@@param" to the end of URL (@@param == index)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = context.Chats.FirstOrDefault(t => t.ChatId == id);
            if (del == null)
            {
                return NotFound();
            }

            context.Chats.Remove(del);
            context.SaveChanges();
            return new NoContentResult();
        }
    }
}