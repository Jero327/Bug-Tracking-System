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
    public class TestCaseController : Controller
    {
        private readonly TestCaseContext _context;

        public TestCaseController(TestCaseContext context)
        {
            _context = context;
        }

        // GET: TestCase
        public async Task<IActionResult> Index(string searchString)
        {
            var testCases = from m in _context.TestCase.Include(t => t.CaseTester).Include(t => t.Project).Include(t => t.SubProject) select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                testCases = testCases.Where(s => s.TestCaseName.Contains(searchString));
            }
            
            return View(await testCases.ToListAsync());
        }

        // GET: TestCase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase
                .Include(t => t.CaseTester)
                .Include(t => t.Project)
                .Include(t => t.SubProject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCase == null)
            {
                return NotFound();
            }

            return View(testCase);
        }

        // GET: TestCase/Create
        public IActionResult Create()
        {
            ViewData["CaseTesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName");
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName");
            return View();
        }

        // POST: TestCase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,SubProjectId,CaseTesterId,TestCaseName,Comment,Status,Priority,Image,CreateTime,ModifyTime,CloseTime")] TestCase testCase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testCase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseTesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", testCase.CaseTesterId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", testCase.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", testCase.SubProjectId);
            return View(testCase);
        }

        // GET: TestCase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase.FindAsync(id);
            if (testCase == null)
            {
                return NotFound();
            }
            ViewData["CaseTesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", testCase.CaseTesterId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", testCase.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", testCase.SubProjectId);
            return View(testCase);
        }

        // POST: TestCase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,SubProjectId,CaseTesterId,TestCaseName,Comment,Status,Priority,Image,CreateTime,ModifyTime,CloseTime")] TestCase testCase)
        {
            if (id != testCase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestCaseExists(testCase.Id))
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
            ViewData["CaseTesterId"] = new SelectList(_context.Set<User>(), "Id", "UserName", testCase.CaseTesterId);
            ViewData["ProjectId"] = new SelectList(_context.Set<Project>(), "Id", "ProjectName", testCase.ProjectId);
            ViewData["SubProjectId"] = new SelectList(_context.Set<SubProject>(), "Id", "SubProjectName", testCase.SubProjectId);
            return View(testCase);
        }

        // GET: TestCase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase
                .Include(t => t.CaseTester)
                .Include(t => t.Project)
                .Include(t => t.SubProject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCase == null)
            {
                return NotFound();
            }

            return View(testCase);
        }

        // POST: TestCase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testCase = await _context.TestCase.FindAsync(id);
            _context.TestCase.Remove(testCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestCaseExists(int id)
        {
            return _context.TestCase.Any(e => e.Id == id);
        }
    }
}
