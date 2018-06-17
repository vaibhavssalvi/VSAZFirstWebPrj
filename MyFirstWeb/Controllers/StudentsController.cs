using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyStudentDAL.Models;

namespace MyFirstWeb.Controllers
{
    public class StudentsController : Controller
    {
        //private Model db = new Model();
        MyWebStudentContext db = new MyWebStudentContext();
        
        // GET: Students
        public async Task<ActionResult> Index()
        {
            //var students = db.Students.Include(s => s.Ratings);
            return View(await db.Students.Include(rtng => rtng.Ratings).ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = await db.Students.Include(rtng => rtng.Ratings).FirstOrDefaultAsync(stu => stu.Id == Id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Rating = new SelectList(db.Ratings, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TotalMarks,Rating")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Rating = new SelectList(db.Ratings, "Id", "Name", students.Rating);
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = await db.Students.FindAsync(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rating = new SelectList(db.Ratings, "Id", "Name", students.Rating);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TotalMarks,Rating")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Rating = new SelectList(db.Ratings, "Id", "Name", students.Rating);

            return View(students);
            //return RedirectToAction("Index", "Students");
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? Id)
        {
            if (Id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Students students = await db.Students.Include(rtng => rtng.Ratings).FirstOrDefaultAsync(stu => stu.Id == Id);
            if (students == null) return HttpNotFound();
            //return View(students);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        // GET PARTIAL: Students/Delete/5
        public ActionResult ConfirmDelete(int? Id)
        {
            if (Id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Students students = db.Students.Include(rtng => rtng.Ratings).FirstOrDefault(stu => stu.Id == Id);
            //ViewBag.StudentObj = students;
            if (students == null) return HttpNotFound();
            return PartialView("_Partial1", students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Students students = await db.Students.FindAsync(id);
            db.Students.Remove(students);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            //return Json(true, JsonRequestBehavior.AllowGet);
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
