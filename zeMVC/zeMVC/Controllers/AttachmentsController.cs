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
    public class AttachmentsController : Controller
    {
        private MessangerContext db = new MessangerContext();

        // GET: Attachments
        public async Task<ActionResult> Index()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var attachments = await db.Attachments.Select(a => new { Id = a.Id, Link = a.Link, FileSize = a.FileSize, CDNId = a.CDNId, MessageId = a.MessageId }).ToListAsync();
            result.Data = attachments;

            return result;
        }

        //// GET: Attachments/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Attachments attachments = await db.Attachments.FindAsync(id);
        //    if (attachments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(attachments);
        //}

        //// GET: Attachments/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MessageId = new SelectList(db.Messages, "Id", "Text");
        //    return View();
        //}

        // POST: Attachments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Attachments attachments)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Attachments.Add(attachments);
                await db.SaveChangesAsync();
                result.Data = attachments;
                return result;
            }

            result.Data = false;
            return result;
        }

        //// GET: Attachments/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Attachments attachments = await db.Attachments.FindAsync(id);
        //    if (attachments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MessageId = new SelectList(db.Messages, "Id", "Text", attachments.MessageId);
        //    return View(attachments);
        //}

        // POST: Attachments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Attachments attachments)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (ModelState.IsValid)
            {
                db.Entry(attachments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                result.Data = attachments;
                return result;
            }

            result.Data = false;
            return result;
        }

        //// GET: Attachments/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Attachments attachments = await db.Attachments.FindAsync(id);
        //    if (attachments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(attachments);
        //}

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            Attachments attachments = await db.Attachments.FindAsync(id);
            if (null != attachments)
            {
                db.Attachments.Remove(attachments);
                await db.SaveChangesAsync();
                result.Data = true;
                return result;
            }

            result.Data = false;
            return result;
        }

        public async Task<ActionResult> MessageAttachments(int? messageId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = null;

            if (!messageId.HasValue)
            {
                return result;
            }

            var attachments = await db.Attachments.Select(a => new { Id = a.Id, Link = a.Link, FileSize = a.FileSize, CDNId = a.CDNId, MessageId = a.MessageId }).Where(a => a.MessageId == messageId).ToListAsync();
            if (null != attachments)
            {
                result.Data = attachments;
            }

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
