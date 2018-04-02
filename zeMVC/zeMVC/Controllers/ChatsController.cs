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


        // GET: Chats/FindPublics
        public async Task<ActionResult> FindPublics(string name, int? start, int? count)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = null;

            if (null == name)
            {
                return result;
            }

            var publics = await db.Chats.Where(s => s.Type.ToLower() == "public" && s.Name.ToLower().Contains(name.ToLower())).Select(s => new { Id = s.Id, Creator = s.Creator, Name = s.Name, Type = s.Type, Avatar = s.Avatar }).ToListAsync();

            if (publics != null)
            {

                if (!start.HasValue || start.Value < 0)
                {
                    start = 0;
                }

                if (start.Value >= publics.Count)
                {
                    return result;
                }

                if (!count.HasValue || count.Value <= 0)
                {
                    count = publics.Count;
                }

                count = Math.Min(Math.Min(count.Value, publics.Count - start.Value), 100);
                result.Data = publics.GetRange(start.Value, count.Value);
            }

            return result;
        }


        // GET: Chats/UserChats
        public async Task<ActionResult> UserChats(int? UserId, int? Start, int? Count)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = null;

            if (!UserId.HasValue)
            {
                return result;
            }

            var chats = await db.Chats.Select(p => new { Id = p.Id, Creator = p.Creator, Name = p.Name, Type = p.Type, Avatar = p.Avatar }).Where(p => db.UsersInChat.Any(element => element.UserId == UserId && p.Id == element.ChatId)).ToListAsync();

            if (chats != null)
            {

                chats.Sort((first, second) =>
                {
                    var a = db.Messages.Where(e => e.ChatId == first.Id).ToList().LastOrDefault();
                    var b = db.Messages.Where(e => e.ChatId == second.Id).ToList().LastOrDefault();

                    if (a == b)
                    {
                        return 0;
                    }

                    if (null == a)
                    {
                        return 1;
                    }

                    else if (null == b)
                    {
                        return -1;
                    }

                    if (a.SentDate > b.SentDate)
                    {
                        return -1;
                    }

                    return 1;
                });
                
                if (!Start.HasValue || Start.Value < 0)
                {
                    Start = 0;
                }

                if (Start.Value >= chats.Count)
                {
                    return result;
                }

                if (!Count.HasValue || Count.Value <= 0)
                {
                    Count = chats.Count;
                }

                Count = Math.Min(Math.Min(Count.Value, chats.Count - Start.Value), 100);
                result.Data = chats.GetRange(Start.Value, Count.Value);
            }

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

            var chats = await db.Chats.FirstOrDefaultAsync(g => g.Id == chatId && db.Users.FirstOrDefault(h => h.Login == login && h.Password == password).Id == g.Creator);

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
