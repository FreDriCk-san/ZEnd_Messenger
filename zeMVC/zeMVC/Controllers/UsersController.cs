using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zeMVC.Data;
using zeMVC.Models;

namespace zeMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly MessangerContext _context;

        public UsersController(MessangerContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(s => s.Chat)
                    .ThenInclude(e => e.Messages)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Mail,Password,Login")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_context.Users.Any(p => p.Login == user.Login))
                    {
                        ModelState.AddModelError("", "Login already exists.");
                    }
                    else
                    {
                        _context.Add(user);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch (DbUpdateException exception)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again. " +
                "ERROR: " + exception);
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users.SingleOrDefaultAsync(s => s.UserId == id);
            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                s => s.Login, s => s.Password, s => s.Mail, s => s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again. " +
                    "ERROR: " + exception);
                }
            }
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again.";
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
