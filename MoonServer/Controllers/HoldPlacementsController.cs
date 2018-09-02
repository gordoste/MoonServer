using MoonServer.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class HoldPlacementsController : BaseController
    {
        // GET: HoldPlacements
        public async Task<ActionResult> Index()
        {
            var holdPlacements = db.HoldPlacements.Include(h => h.Hold).Include(h => h.Position);
            return View(await holdPlacements.ToListAsync());
        }

        // GET: HoldPlacements/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldPlacement holdPlacement = await db.HoldPlacements.FindAsync(id);
            if (holdPlacement == null)
            {
                return HttpNotFound();
            }
            return View(holdPlacement);
        }

        // GET: HoldPlacements/Create
        public ActionResult Create()
        {
            ViewBag.HoldId = new SelectList(db.Holds, "Id", "Name");
            ViewBag.PositionId = new SelectList(db.Positions, "Id", "Name");
            return View();
        }

        // POST: HoldPlacements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,HoldId,PositionId,Orientation")] HoldPlacement holdPlacement)
        {
            if (ModelState.IsValid)
            {
                db.HoldPlacements.Add(holdPlacement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HoldId = new SelectList(db.Holds, "Id", "Name", holdPlacement.HoldId);
            ViewBag.PositionId = new SelectList(db.Positions, "Id", "Name", holdPlacement.PositionId);
            return View(holdPlacement);
        }

        // GET: HoldPlacements/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldPlacement holdPlacement = await db.HoldPlacements.FindAsync(id);
            if (holdPlacement == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoldId = new SelectList(db.Holds, "Id", "Name", holdPlacement.HoldId);
            ViewBag.PositionId = new SelectList(db.Positions, "Id", "Name", holdPlacement.PositionId);
            return View(holdPlacement);
        }

        // POST: HoldPlacements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,HoldId,PositionId,Orientation")] HoldPlacement holdPlacement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holdPlacement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HoldId = new SelectList(db.Holds, "Id", "Name", holdPlacement.HoldId);
            ViewBag.PositionId = new SelectList(db.Positions, "Id", "Name", holdPlacement.PositionId);
            return View(holdPlacement);
        }

        // GET: HoldPlacements/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldPlacement holdPlacement = await db.HoldPlacements.FindAsync(id);
            if (holdPlacement == null)
            {
                return HttpNotFound();
            }
            return View(holdPlacement);
        }

        // POST: HoldPlacements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            HoldPlacement holdPlacement = await db.HoldPlacements.FindAsync(id);
            db.HoldPlacements.Remove(holdPlacement);
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
