using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPart.Models;

namespace MvcPart.Controllers
{
    public class CalculationsController : Controller
    {
        private readonly MvcPartContext _context;
        public IList<Part> Parts { get; set; }
        public IList<Project> Projects { get; set; }

        public CalculationsController(MvcPartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var count = 0;

            Parts = await _context.Part.AsNoTracking().ToListAsync();
            Projects = await _context.Project.AsNoTracking().ToListAsync();



            return View();
        }
    }
}