using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcPart.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MvcPart.Controllers
{
    public class PartsController : Controller
    {
        private readonly MvcPartContext _context;
        public IList<Part> Parts { get; set; }

        public PartsController(MvcPartContext context)
        {
            _context = context;
        }

        // GET: Parts
        public async Task<IActionResult> Index(string searchString)
        {
            var parts = from m in _context.Part
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(s => s.UsedFor.Contains(searchString));
            }

            return View(await parts.ToListAsync());
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // GET: Parts/Create
        public IActionResult Create()
        {
            return View();
        }

        public IList<Project> Projects { get; private set; }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Now,UsedFor,Flagged,Color,TextColor")] Part part)
        {
            var inThere = false;

            if (ModelState.IsValid)
            {
                Projects = await _context.Project.AsNoTracking().ToListAsync();

                if (Projects == null)
                {
                    ViewData["Message"] = "There are currently no projects.";
                    return View(part);
                }
                else
                {
                    for (int i = 0; i < Projects.Count; i++)
                    {
                        if (Projects[i].Name == part.UsedFor)
                            inThere = true;
                    }
                }

                if (inThere == false)
                {
                    ViewData["Message"] = $"Project {part.UsedFor} is not a current project";
                    return View(part);
                }

                part.Now = DateTime.Now;

                _context.Add(part);
                await _context.SaveChangesAsync();
                ViewData["Message"] = $"Part { part.Name} added!";
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        [Authorize(Roles="Manager")]
        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        [Authorize(Roles = "Manager")]
        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Now,UsedFor,Flagged,Color,TextColor")] Part part)
        {
            if (id != part.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Projects = await _context.Project.AsNoTracking().ToListAsync();

                    var inThere = false;

                    for (int i = 0; i < Projects.Count; i++)
                    {
                        if (Projects[i].Name == part.UsedFor)
                            inThere = true;
                    }


                    if (inThere == false)
                    {
                        ViewData["Message"] = $"Project {part.UsedFor} is not a current project";
                        return View(part);
                    }
                    else
                    {
                        _context.Update(part);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        [Authorize(Roles = "Manager")]
        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        [Authorize(Roles = "Manager")]
        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Part.FindAsync(id);
            _context.Part.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(int id)
        {
            return _context.Part.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Flag(int? id)
        {
            var part = await _context.Part.FindAsync(id);

            if (part != null)
            {
                part.Flagged = "Y";
                part.Color = "red";
                part.TextColor = "white";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Unflag(int? id)
        {
            var part = await _context.Part.FindAsync(id);

            if (part != null)
            {
                part.Flagged = "N";
                part.Color = "white";
                part.TextColor = "black";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
