using MoonServer.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class HoldsController : BaseController
    {
        // GET: Holds
        public async Task<ActionResult> Index()
        {
            return View(await db.Holds.ToListAsync());
        }

        // GET: Holds/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = await db.Holds.FindAsync(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            return View(hold);
        }

        // GET: Holds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Holdset,Name")] Hold hold)
        {
            if (ModelState.IsValid)
            {
                db.Holds.Add(hold);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hold);
        }

        // GET: Holds/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = await db.Holds.FindAsync(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            return View(hold);
        }

        // POST: Holds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Holdset,Name")] Hold hold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hold).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hold);
        }

        // GET: Holds/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hold hold = await db.Holds.FindAsync(id);
            if (hold == null)
            {
                return HttpNotFound();
            }
            return View(hold);
        }

        // POST: Holds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Hold hold = await db.Holds.FindAsync(id);
            db.Holds.Remove(hold);
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
