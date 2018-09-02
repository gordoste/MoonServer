using MoonServer.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class HoldSetupsController : BaseController
    {
        // GET: HoldSetups
        public async Task<ActionResult> Index()
        {
            return View(await db.HoldSetups.ToListAsync());
        }

        // GET: HoldSetups/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldSetup holdSetup = await db.HoldSetups.FindAsync(id);
            if (holdSetup == null)
            {
                return HttpNotFound();
            }
            return View(holdSetup);
        }

        // GET: HoldSetups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoldSetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] HoldSetup holdSetup)
        {
            if (ModelState.IsValid)
            {
                db.HoldSetups.Add(holdSetup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(holdSetup);
        }

        // GET: HoldSetups/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldSetup holdSetup = await db.HoldSetups.FindAsync(id);
            if (holdSetup == null)
            {
                return HttpNotFound();
            }
            return View(holdSetup);
        }

        // POST: HoldSetups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] HoldSetup holdSetup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holdSetup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(holdSetup);
        }

        // GET: HoldSetups/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldSetup holdSetup = await db.HoldSetups.FindAsync(id);
            if (holdSetup == null)
            {
                return HttpNotFound();
            }
            return View(holdSetup);
        }

        // POST: HoldSetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            HoldSetup holdSetup = await db.HoldSetups.FindAsync(id);
            db.HoldSetups.Remove(holdSetup);
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
