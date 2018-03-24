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
    public class UsersController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = await db.Users.Select(t => new { Id = t.Id, Name = t.Name, Avatar = t.Avatar }).ToListAsync();

            return result;
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = null;

            if (id == null)
            {
                return result;
            }

            var users = await db.Users.Select(t => new { Id = t.Id, Name = t.Name, Avatar = t.Avatar }).FirstOrDefaultAsync(f => f.Id == id);
            result.Data = users;

            return result;
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Users users)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                result.Data = true;
                return result;
            }
            result.Data = false;

            return result;
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Users users, string oldPassword)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var password = oldPassword == null ? users.Password : oldPassword;

            if (ModelState.IsValid && db.Users.Any(a => a.Login == users.Login && a.Password == password))
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = true;
                return result;
            }
            result.Data = false;

            return result;
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Users users = await db.Users.FindAsync(id);
            if (null != users)
            {
                db.Users.Remove(users);
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
