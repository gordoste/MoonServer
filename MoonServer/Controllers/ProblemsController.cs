using MoonServer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class ProblemsController : BaseController
    {
        // GET: Problems
        //public async Task<ActionResult> Index()
        public ActionResult Index()
        {
            //    var problems = db.Problems.Include(p => p.Grade).Include(p => p.HoldSetup);
            //    return View(await problems.ToListAsync());
            return View(new List<Problem>());
        }

        // GET: Problems/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Problem problem = await db.Problems.FindAsync(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            return View(problem);
        }

        // GET: Problems/Create
        public ActionResult Create()
        {
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "EuroName");
            ViewBag.HoldSetupId = new SelectList(db.HoldSetups, "Id", "Name");
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MoonID,Name,IsBenchmark,GradeId,HoldSetupId,Repeats,DateAdded")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                db.Problems.Add(problem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GradeId = new SelectList(db.Grades, "Id", "EuroName", problem.GradeId);
            ViewBag.HoldSetupId = new SelectList(db.HoldSetups, "Id", "Name", problem.HoldSetupId);
            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Problem problem = await db.Problems.FindAsync(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "EuroName", problem.GradeId);
            ViewBag.HoldSetupId = new SelectList(db.HoldSetups, "Id", "Name", problem.HoldSetupId);
            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MoonID,Name,IsBenchmark,GradeId,HoldSetupId,Repeats,DateAdded")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(problem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "EuroName", problem.GradeId);
            ViewBag.HoldSetupId = new SelectList(db.HoldSetups, "Id", "Name", problem.HoldSetupId);
            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Problem problem = await db.Problems.FindAsync(id);
            if (problem == null)
            {
                return HttpNotFound();
            }
            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Problem problem = await db.Problems.FindAsync(id);
            db.Problems.Remove(problem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
