using StudentAttendance.Models;
using StudentAttendance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendance.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            try
            {
               // var students = db.Attendances.Include(e => e.StdInformations).Select(t => new AttendanceViewModel()
                  var students = db.Attendances.ToList().Select(t => new AttendanceViewModel()
                  {
                    Id = t.Id,
                    StdId = t.StdId,
                    CreatedDate = t.CreatedDate,
                    Information = t.StdInformations == null ? null : t.StdInformations.StdName,
                    InTime = t.InTime,
                    OutTime = t.OutTime
                }).ToList();
                var emp = students.ToList();
                return View(students);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance student = db.Attendances.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            AttendanceViewModel model = new AttendanceViewModel();
            model.Id = student.Id;
            model.InTime = student.InTime;
            model.OutTime = student.OutTime;
            model.StdId = student.StdId;
            model.Information = student.StdInformations == null ? null : student.StdInformations.StdName;
            model.CreatedDate = student.CreatedDate;
            return View(model);
        }

        // GET: Attendances/Create
        public ActionResult CreateAttendance()
        {
            ViewBag.StdId = new SelectList(db.Informations, "Id", "StdName");
            //ViewBag.EmpId = new SelectList(db.Employees, "Id", "Name");
            return PartialView();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateAttendance(AttendanceViewModel student)
        {
            try
            {
                Attendance model = new Attendance();
                model.InTime = student.InTime;
                model.OutTime = student.OutTime;
                model.Id = student.Id;
                model.StdId = student.StdId;
                model.CreatedDate = DateTime.Now;
                db.Attendances.Add(model);
                db.SaveChanges();


                return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


            //ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", employee.DesignationId);
            //return View(employee);
        }

        // GET: Attendances/Edit/5
        public ActionResult EditAttendance(int? id)
        {
            if (id == null)
            {
                return Json(new { success = true, message = "Not find" }, JsonRequestBehavior.AllowGet);
            }
            Attendance student = db.Attendances.Find(id);
            if (student == null)
            {
                return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
            }
            AttendanceViewModel model = new AttendanceViewModel();
            model.InTime = student.InTime;
            model.OutTime = student.OutTime;
            model.Id = student.Id;
            model.StdId = student.StdId;

            ViewBag.StdId = new SelectList(db.Informations, "Id", "StdName", student.StdId);
            return PartialView(model);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditAttendance(Attendance student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = db.Attendances.Find(student.Id);
                    if (entity == null)
                    {
                        return Json(new { success = false, message = "Not found" }, JsonRequestBehavior.AllowGet);
                    }
                    entity.InTime = student.InTime;
                    entity.OutTime = student.OutTime;
                    entity.Id = student.Id;
                    entity.StdId = student.StdId;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = "Not Saved" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance student = db.Attendances.Find(id);
            db.Attendances.Remove(student);
            db.SaveChanges();

            return Json(new { success = true, message = "Deleted" }, JsonRequestBehavior.AllowGet);
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