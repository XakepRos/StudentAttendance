using StudentAttendance.Models;
using StudentAttendance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendance.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Default
        public virtual ActionResult Index()
        {
            try
            {
                //var data = db.Informations.Select(x => new StdInformationViewModel()
                //var d = db.Informations.ToList();
                 var data = db.Informations.Include(x=>x.CourseDetails).ToList().Select(x => new StdInformationViewModel()
                 {
                    Id = x.Id,
                    StdName = x.StdName,
                    DOB = x.DOB,
                    Address = x.Address,
                     Age = x.Age,
                     //Age = x.CalculateAge(x.DOB),
                     Mobile = x.Mobile,
                    Email = x.Email,
                    Gender = x.Gender,
                    Food = x.Food,
                    Description = x.Description,
                    CourseId = x.CourseId,
                     //Employee = t.EmployeeDetails == null ? null : t.EmployeeDetails.Name,
                     Course = x.CourseDetails == null?null:x.CourseDetails.CourseName
                }).ToList();

                var std = data.Select(t => new StdInformationViewModel()
                {
                    Id = t.Id,
                    StdName = t.StdName,
                    DOB = t.DOB,
                    Address = t.Address,
                    Age = t.Age,
                    Mobile = t.Mobile,
                    Email = t.Email,
                    Gender = t.Gender,
                    Food = t.Food,
                    Description = t.Description,
                    CourseId = t.CourseId,
                    Course=t.Course
                });
                return View(std);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private int CalculateAge(DateTime dateOfBirth)
        //{
        //    if (dateOfBirth == null)
        //    {
        //        return 0;
        //    }
        //    var today = DateTime.Today;
        //    var age = today.Year - dateOfBirth.Year;
        //    if (dateOfBirth.Date > today.AddYears(-age)) age--;
        //    return age;
        //}

        [HttpGet]
        public virtual ActionResult CreateInformation()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName");
            return PartialView(new StdInformationViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateInformation(StdInformationViewModel model)
        {
            try
            {
               
                Information entity = new Information();
                entity.StdName = model.StdName;
                entity.DOB = model.DOB;
                entity.Address = model.Address;
                entity.Age = model.Age;
                entity.Mobile = model.Mobile;
                entity.Email = model.Email;
                entity.Gender = model.Gender;
                entity.Food = model.Food;
                entity.Description = model.Description;
                entity.CourseId = model.CourseId;
                //entity.Course = model.Course;
                var result = db.Informations.Add(entity);
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
        public virtual ActionResult EditInformation(int id)
        {
            var information = db.Informations.Find(id);
            var model = new StdInformationViewModel();

            model.Id = information.Id;
            model.StdName = information.StdName;
            model.Address = information.Address;
            model.DOB = information.DOB;
            model.Age = information.Age;
            model.Mobile = information.Mobile;
            model.Email = information.Email;
            model.Gender = information.Gender;
            model.Food = information.Food;
            model.Description = information.Description;
            model.CourseId = information.CourseId;
           
            return PartialView(model);          
        }

        [HttpPost]
        public virtual JsonResult EditInformation(StdInformationViewModel model)
        {
            try
            {
                var entity = db.Informations.Find(model.Id);
                if (entity == null)
                {
                    return Json(new { success = false, message = "Not Found" }, JsonRequestBehavior.AllowGet);
                }

                entity.Id = model.Id;
                entity.StdName = model.StdName;
                entity.Address = model.Address;
                entity.DOB = model.DOB;
                entity.Age = model.Age;
                entity.Mobile = model.Mobile;
                entity.Email = model.Email;
                entity.Gender = model.Gender;
                entity.Food = model.Food;
                entity.Description = model.Description;
                entity.CourseId = model.CourseId;
                
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public virtual JsonResult DeleteInformation(int id)
        {
            try
            {
                Information del = db.Informations.Find(id);
                db.Informations.Remove(del);
                db.SaveChanges();
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}