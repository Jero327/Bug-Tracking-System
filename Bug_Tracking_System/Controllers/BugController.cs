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
        public async Task<IActionResult> Index(int count)
        {
            var bugs = from m in _context.Bug.Include(b => b.Developer).Include(b => b.Project).Include(b => b.SubProject).Include(b => b.TestCase).Include(b => b.TestManager).Include(b => b.Tester) select m;

            count = bugs.Count();

            ViewData["count"] = count;
            
            return View(await bugs.ToListAsync());
        }

        // GET: Bug Report
        public async Task<IActionResult> BugReport(string bugProject, string bugSubProject, string bugTester, string bugDeveloper, string searchString, string BugStatus, int count, string Rating, string start_date, string end_date, string close_start_date, string close_end_date)
        {
            IQueryable<string> projectQuery = from m in _context.Bug
                                    orderby m.Project.ProjectName
                                    select m.Project.ProjectName;

            IQueryable<string> subprojectQuery = from m in _context.Bug
                                    orderby m.SubProject.SubProjectName
                                    select m.SubProject.SubProjectName;

            IQueryable<string> testerQuery = from m in _context.Bug
                                    orderby m.Tester.UserName
                                    select m.Tester.UserName;

            IQueryable<string> developerQuery = from m in _context.Bug
                                    orderby m.Developer.UserName
                                    select m.Developer.UserName;
            
            var bugs = from m in _context.Bug.Include(b => b.Developer).Include(b => b.Project).Include(b => b.SubProject).Include(b => b.TestCase).Include(b => b.TestManager).Include(b => b.Tester) select m;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                bugs = bugs.Where(s => s.BugName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(bugProject))
            {
                bugs = bugs.Where(s => s.Project.ProjectName == bugProject);
            }

            if (!String.IsNullOrEmpty(bugSubProject))
            {
                bugs = bugs.Where(s => s.SubProject.SubProjectName == bugSubProject);
            }

            if (!String.IsNullOrEmpty(bugTester))
            {
                bugs = bugs.Where(s => s.Tester.UserName == bugTester);
            }

            if (!String.IsNullOrEmpty(bugDeveloper))
            {
                bugs = bugs.Where(s => s.Developer.UserName == bugDeveloper);
            }

            if (!String.IsNullOrEmpty(BugStatus))
            {
                bugs = bugs.Where(s => s.BugStatus == ((BugStatus)Enum.Parse(typeof(BugStatus), BugStatus, false)));
            }

            if (!String.IsNullOrEmpty(Rating))
            {
                if (Rating=="1")
                { bugs = bugs.Where(s => (s.Severity*s.Priority)>0 && (s.Severity*s.Priority)<6); }
                if (Rating=="6")
                { bugs = bugs.Where(s => (s.Severity*s.Priority)>5 && (s.Severity*s.Priority)<11); }
                if (Rating=="11")
                { bugs = bugs.Where(s => (s.Severity*s.Priority)>10 && (s.Severity*s.Priority)<16); }
                if (Rating=="16")
                { bugs = bugs.Where(s => (s.Severity*s.Priority)>15 && (s.Severity*s.Priority)<21); }
                if (Rating=="21")
                { bugs = bugs.Where(s => (s.Severity*s.Priority)>20 && (s.Severity*s.Priority)<26); }
            }

            if (!String.IsNullOrEmpty(start_date))
            {
                if (!String.IsNullOrEmpty(end_date))
                {
                    bugs = bugs.Where(s => s.CreateTime>=Convert.ToDateTime(start_date) && s.CreateTime<=Convert.ToDateTime(end_date));
                }
            }

            if (!String.IsNullOrEmpty(close_start_date))
            {
                if (!String.IsNullOrEmpty(close_end_date))
                {
                    bugs = bugs.Where(s => s.CloseTime>=Convert.ToDateTime(close_start_date) && s.CloseTime<=Convert.ToDateTime(close_end_date));
                }
            }

            var bugVM = new BugViewModel
            {
                Project = new SelectList(await projectQuery.Distinct().ToListAsync()),
                SubProject = new SelectList(await subprojectQuery.Distinct().ToListAsync()),
                Tester = new SelectList(await testerQuery.Distinct().ToListAsync()),
                Developer = new SelectList(await developerQuery.Distinct().ToListAsync()),

                Bugs = await bugs.ToListAsync()
            };

            count = bugs.Count();

            ViewData["count"] = count;
            
            return View(bugVM);
        }

        // GET: Bug chart
        public IActionResult chart()
        {
            return View();
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
        public async Task<IActionResult> Create([Bind("Id,ProjectId,SubProjectId,TestCaseId,BugStatus,Severity,Priority,TesterId,TestManagerId,DeveloperId,BugName,Comment,Rating,CreateTime,ModifyTime,CloseTime")] Bug bug)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,SubProjectId,TestCaseId,BugStatus,Severity,Priority,TesterId,TestManagerId,DeveloperId,BugName,Comment,Rating,CreateTime,ModifyTime,CloseTime")] Bug bug)
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
