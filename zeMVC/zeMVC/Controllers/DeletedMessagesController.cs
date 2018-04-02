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
    public class DeletedMessagesController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: DeletedMessages
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var deletedMessages = await db.DeletedMessages.Select(d => new { Id = d.Id, MessageId = d.MessageId, UserId = d.UserId }).ToListAsync();
            result.Data = deletedMessages;

            return result;
        }

        //// GET: DeletedMessages/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DeletedMessages deletedMessages = await db.DeletedMessages.FindAsync(id);
        //    if (deletedMessages == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(deletedMessages);
        //}

        //// GET: DeletedMessages/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MessageId = new SelectList(db.Messages, "Id", "Text");
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
        //    return View();
        //}

        // POST: DeletedMessages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DeletedMessages deletedMessages)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid && 
                db.UsersInChat.Any(d => d.UserId == deletedMessages.UserId && db.Messages.SingleOrDefault(m => m.Id == deletedMessages.MessageId).ChatId == d.ChatId)
                )
            {
                db.DeletedMessages.Add(deletedMessages);
                await db.SaveChangesAsync();
                result.Data = deletedMessages;
                return result;
            }

            result.Data = false;

            return result;
        }

        //// GET: DeletedMessages/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DeletedMessages deletedMessages = await db.DeletedMessages.FindAsync(id);
        //    if (deletedMessages == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MessageId = new SelectList(db.Messages, "Id", "Text", deletedMessages.MessageId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", deletedMessages.UserId);
        //    return View(deletedMessages);
        //}

        // POST: DeletedMessages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeletedMessages deletedMessages)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Entry(deletedMessages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = deletedMessages;
                return result;
            }

            result.Data = false;

            return result;
        }

        //// GET: DeletedMessages/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DeletedMessages deletedMessages = await db.DeletedMessages.FindAsync(id);
        //    if (deletedMessages == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(deletedMessages);
        //}

        // POST: DeletedMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            DeletedMessages deletedMessages = await db.DeletedMessages.FindAsync(id);
            if (null != deletedMessages)
            {
                db.DeletedMessages.Remove(deletedMessages);
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
