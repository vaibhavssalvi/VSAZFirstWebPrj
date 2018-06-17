using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using MyStudentDAL.Models;

namespace MyFirstWeb.Controllers
{
    public class HomeController : Controller
    {
        // Create an instance of DatabaseContext class
        //Model db = new Model();
        MyWebStudentContext db = new MyWebStudentContext();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult All()
        {
            List<Students> model = db.Students.ToList();
            return PartialView("_Student", model);
        }

        // Return Top3 students
        public PartialViewResult Top3()
        {
            List<Students> model = db.Students.OrderByDescending(x => x.TotalMarks).Take(3).ToList();
            return PartialView("_Student", model);
        }

        // Return Bottom3 students
        public PartialViewResult Bottom3()
        {
            List<Students> model = db.Students.OrderBy(x => x.TotalMarks).Take(3).ToList();
            return PartialView("_Student", model);
        }

        public ActionResult Top()
        {
            var Students = db.Students.Include(s => s.Ratings)
                                        .Where(s => s.Ratings.Name == "Top" && (s.TotalMarks > 750 && s.TotalMarks < 890))                                 
                                        .OrderBy(s => s.Name);
            return View(Students.ToList());
            //List<Student> model = db.Students.OrderByDescending(x => x.TotalMarks).Take(3).ToList();
            //return View();
        }

    }
}
