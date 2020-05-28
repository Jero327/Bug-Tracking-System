using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bug_Tracking_System.Data;
using Bug_Tracking_System.Models;

namespace Bug_Tracking_System.Controllers
{
    public class SubProjectController : Controller
    {
        private readonly SubProjectContext _context;

        public SubProjectController(SubProjectContext context)
        {
            _context = context;
        }

        // GET: SubProject
        public async Task<IActionResult> Index()
        {
            var subProjectContext = _context.SubProject.Include(s => s.Project);
            return View(await subProjectContext.ToListAsync());
        }

        // GET: SubProject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProject
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // GET: SubProject/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName");
            return View();
        }

        // POST: SubProject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubProjectName,Description,CteateTime,FinishTime,ProjectId")] SubProject subProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", subProject.ProjectId);
            return View(subProject);
        }

        // GET: SubProject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProject.FindAsync(id);
            if (subProject == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", subProject.ProjectId);
            return View(subProject);
        }

        // POST: SubProject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubProjectName,Description,CteateTime,FinishTime,ProjectId")] SubProject subProject)
        {
            if (id != subProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubProjectExists(subProject.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", subProject.ProjectId);
            return View(subProject);
        }

        // GET: SubProject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProject
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // POST: SubProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subProject = await _context.SubProject.FindAsync(id);
            _context.SubProject.Remove(subProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubProjectExists(int id)
        {
            return _context.SubProject.Any(e => e.Id == id);
        }
    }
}
