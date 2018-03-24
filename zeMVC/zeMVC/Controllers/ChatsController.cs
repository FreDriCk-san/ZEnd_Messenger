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
    public class ChatsController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: Chats
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = await db.Chats.Select(c => new { Id = c.Id, Creator = c.Creator, Name = c.Name, Avatar = c.Avatar }).ToListAsync();

            return result;
        }

        // POST: Chats/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Chats chats)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid && !db.Users.Any(c => c.Id == chats.Creator))
            {
                db.Chats.Add(chats);
                await db.SaveChangesAsync();
                result.Data = chats;
                return result;
            }

            result.Data = null;

            return result;
        }

        // POST: Chats/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Chats chats)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Entry(chats).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = chats;
                return result;
            }

            result.Data = false;

            return result;
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int chatId, string login, string password)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = false;

            var chats = await db.Chats.FirstOrDefaultAsync(g => g.Id == chatId && db.Users.FirstOrDefaultAsync(h => h.Login == login && h.Password == password).Id == g.Creator);

            if (null != chats)
            {
                db.Chats.Remove(chats);
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
