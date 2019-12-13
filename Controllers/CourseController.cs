using StudentAttendance.Models;
using StudentAttendance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendance.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Default
        public virtual ActionResult Index()
        {
            var data = db.Courses.Select(x => new CourseViewModel()
            {
              Id = x.Id,
              CourseName = x.CourseName
            }).ToList();
            return View(data);
        }

        [HttpGet]
        public virtual ActionResult CreateCourse()
        {
            return PartialView(new CourseViewModel());
        } 

        [HttpPost]
        public virtual ActionResult CreateCourse(CourseViewModel model)
        {
            try
            {
                Course entity = new Course();
                entity.CourseName = model.CourseName;
                var result = db.Courses.Add(entity);
                db.SaveChanges();
                // return RedirectToAction("Index");
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public virtual ActionResult EditCourse(int id)
        {
            var course = db.Courses.Find(id);
            var model = new CourseViewModel();
            model.Id = course.Id;
            model.CourseName = course.CourseName;
            return PartialView(model);
            
        }

        [HttpPost]
        public virtual JsonResult EditCourse(CourseViewModel model)
        {
            try
            {
                var entity = db.Courses.Find(model.Id);
                if(entity==null)
                {
                    return Json(new { success = false, message = "Not Found" }, JsonRequestBehavior.AllowGet);
                }
                entity.CourseName = model.CourseName;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual JsonResult DeleteCourse(int id)
        {
            try
            {
                Course del = db.Courses.Find(id);
                db.Courses.Remove(del);
                db.SaveChanges();
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}