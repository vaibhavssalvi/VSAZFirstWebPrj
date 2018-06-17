using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using MyStudentDAL.Models;

namespace MyFirstWeb.Controllers
{
    public class CreateController : Controller
    {
        //private Model dbCntxt = new Model();
        MyWebStudentContext dbCntxt = new MyWebStudentContext();

        // GET: Create/Create
        public ActionResult Create()
        {
            ViewBag.Rating = new SelectList(dbCntxt.Ratings, "Id", "Name");
            return View();
        }

        // POST: Create/Create
        [HttpPost]
        public ActionResult Create(Students objStdnt)
        {
            try
            {
                //dbCntxt.Students.Add(objStdnt);
                //dbCntxt.SaveChanges();
                //return RedirectToAction("Home");
                if (ModelState.IsValid)
                {
                    dbCntxt.Students.Add(objStdnt);
                    dbCntxt.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Rating = new SelectList(dbCntxt.Ratings, "Id", "Name", objStdnt.Rating);
                return View(objStdnt);
            }
            catch
            {
                return View();
            }
        }


    }
}
