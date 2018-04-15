using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using zeMVC.Models;

namespace zeMVC.Controllers
{
    public class MessagesController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = await db.Messages.Select(m => new { Id = m.Id, Text = m.Text, Date = m.SentDate, ChatId = m.ChatId, UserId = m.UserId, IsRead = m.IsRead }).ToListAsync();

            return result;
        }

        // POST: Messages/ChatMessages/2
        public async Task<ActionResult> ChatMessages(int? ChatId, int? UserId, int? Start, int? Count)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = null;

            if (!ChatId.HasValue || !UserId.HasValue)
            {
                return result;
            }

            var messages = await db.Messages.Select(e => new { Id = e.Id, Text = e.Text, Date = e.SentDate, ChatId = e.ChatId, UserId = e.UserId, IsReaded = e.IsRead }).Where(z => z.ChatId == ChatId && !db.DeletedMessages.Any(e => e.UserId == UserId && e.MessageId == z.Id)).ToListAsync();

            if (messages != null)
            {

                if (!Start.HasValue || Start.Value < 0)
                {
                    Start = 0;
                }

                if (Start.Value >= messages.Count)
                {
                    return result;
                }

                if (!Count.HasValue || Count.Value <= 0)
                {
                    Count = messages.Count;
                }

                Count = Math.Min(Math.Min(Count.Value, messages.Count - Start.Value), 100);
                Start = messages.Count - Count - Start;
                messages = messages.GetRange(Start.Value, Count.Value);
                messages.Reverse();
                result.Data = messages;
            }

            return result;
        }


        // POST: Messages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Messages messages)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)                                          //&& !db.Users.Any(u => u.Id == messages.UserId))
            {
                messages.SentDate = DateTime.Now;
                db.Messages.Add(messages);
                await db.SaveChangesAsync();
                result.Data = messages;
                return result;
            }

            result.Data = false;
            return result;
        }

        // POST: Messages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Messages messages)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = messages;
                return result;
            }

            result.Data = false;

            return result;
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int chatId, string login, string password)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Messages messages = await db.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (null != messages)
            {
                db.Messages.Remove(messages);
                await db.SaveChangesAsync();
                result.Data = true;
                return result;
            }

            result.Data = false;

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
