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
    public class BugController : Controller
    {
        private readonly BugContext _context;

        public BugController(BugContext context)
        {
            _context = context;
        }

        // GET: Bug
        public async Task<IActionResult> Index(string searchString, string BugStatus)
        {
            var bugs = from m in _context.Bug.Include(b => b.Developer).Include(b => b.Project).Include(b => b.SubProject).Include(b => b.TestCase).Include(b => b.TestManager).Include(b => b.Tester) select m;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                bugs = bugs.Where(s => s.BugName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(BugStatus))
            {
                bugs = bugs.Where(s => s.BugStatus == ((BugStatus)Enum.Parse(typeof(BugStatus), BugStatus, false)));
            }
            
            return View(await bugs.ToListAsync());
        }

        // GET: Bug/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.Developer)
                .Include(b => b.Project)
                .Include(b => b.SubProject)
                .Include(b => b.TestCase)
                .Include(b => b.TestManager)
                .Include(b => b.Tester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bug/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Set<User>(), "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName");
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName");
            ViewData["TestCaseId"] = new SelectList(_context.Set<TestCase>(), "Id", "TestCaseName");
            ViewData["TestManagerId"] = new SelectList(_context.Set<User>(), "Id", "UserName");
            ViewData["TesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName");
            return View();
        }

        // POST: Bug/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,SubProjectId,TestCaseId,BugStatus,Severity,Priority,TesterId,TestManagerId,DeveloperId,BugName,Comment,Image,CreateTime,ModifyTime,CloseTime")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", bug.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", bug.SubProjectId);
            ViewData["TestCaseId"] = new SelectList(_context.Set<TestCase>(), "Id", "TestCaseName", bug.TestCaseId);
            ViewData["TestManagerId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TestManagerId);
            ViewData["TesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TesterId);
            return View(bug);
        }

        // GET: Bug/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", bug.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", bug.SubProjectId);
            ViewData["TestCaseId"] = new SelectList(_context.Set<TestCase>(), "Id", "TestCaseName", bug.TestCaseId);
            ViewData["TestManagerId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TestManagerId);
            ViewData["TesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TesterId);
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,SubProjectId,TestCaseId,BugStatus,Severity,Priority,TesterId,TestManagerId,DeveloperId,BugName,Comment,Image,CreateTime,ModifyTime,CloseTime")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
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
            ViewData["DeveloperId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.DeveloperId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", bug.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", bug.SubProjectId);
            ViewData["TestCaseId"] = new SelectList(_context.Set<TestCase>(), "Id", "TestCaseName", bug.TestCaseId);
            ViewData["TestManagerId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TestManagerId);
            ViewData["TesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", bug.TesterId);
            return View(bug);
        }

        // GET: Bug/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.Developer)
                .Include(b => b.Project)
                .Include(b => b.SubProject)
                .Include(b => b.TestCase)
                .Include(b => b.TestManager)
                .Include(b => b.Tester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bug/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bug.FindAsync(id);
            _context.Bug.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bug.Any(e => e.Id == id);
        }
    }
}
