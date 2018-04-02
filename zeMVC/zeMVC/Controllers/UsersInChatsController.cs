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
    public class UsersInChatsController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: UsersInChats
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var usersInChats = await db.UsersInChat.Select(u => new { Id = u.Id, UserId = u.UserId, ChatId = u.ChatId }).ToListAsync();
            result.Data = usersInChats;

            return result;
        }

        //// GET: UsersInChats/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UsersInChat usersInChat = await db.UsersInChat.FindAsync(id);
        //    if (usersInChat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usersInChat);
        //}

        //// GET: UsersInChats/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ChatId = new SelectList(db.Chats, "Id", "Name");
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
        //    return View();
        //}

        // POST: UsersInChats/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsersInChat usersInChat)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.UsersInChat.Add(usersInChat);
                await db.SaveChangesAsync();
                result.Data = usersInChat;
                return result;
            }

            result.Data = false;

            return result;
        }

        //// GET: UsersInChats/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UsersInChat usersInChat = await db.UsersInChat.FindAsync(id);
        //    if (usersInChat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ChatId = new SelectList(db.Chats, "Id", "Name", usersInChat.ChatId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "Name", usersInChat.UserId);
        //    return View(usersInChat);
        //}

        // POST: UsersInChats/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsersInChat usersInChat)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Entry(usersInChat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = usersInChat;
                return result;
            }

            result.Data = false;

            return result;
        }

        //// GET: UsersInChats/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UsersInChat usersInChat = await db.UsersInChat.FindAsync(id);
        //    if (usersInChat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usersInChat);
        //}

        // POST: UsersInChats/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string login, string password, int? chatId, int? userId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = false;

            if (!chatId.HasValue || !userId.HasValue || null == login || null == password)
            {
                return result;
            }

            UsersInChat usersInChat = await db.UsersInChat.FirstOrDefaultAsync(
                u => u.ChatId == chatId && (
                db.Users.FirstOrDefault(f => f.Login == login && f.Password == password).Id == db.Chats.FirstOrDefault(c => c.Id == chatId).Creator) || (db.Users.FirstOrDefault(l => l.Login == login && l.Password == password).Id == userId)
                );

            if (null != usersInChat)
            {
                db.UsersInChat.Remove(usersInChat);
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
