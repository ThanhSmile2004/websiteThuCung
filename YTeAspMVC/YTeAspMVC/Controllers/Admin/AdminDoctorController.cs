using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminDoctorController : Controller
    {
        DoctorDao doctorDao = new DoctorDao();
        BookingDao bookingDao = new BookingDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = doctorDao.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Doctor doctor)
        {
            var file = Request.Files["file"];
            string reName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            doctor.Image = reName;
            doctor.Status = 1;
            doctorDao.Add(doctor);
            return RedirectToAction("Index", new { msg = "1" });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Doctor doctor)
        {
            string reName = "";
            var objCourse = doctorDao.getDoctor(doctor.IdDoctor);
            var file = Request.Files["file"];
            if (file.FileName == "")
            {
                reName = objCourse.Image;
            }
            else
            {
                reName = DateTime.Now.Ticks.ToString() + file.FileName;
                file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            }
            doctor.Image = reName;
            doctorDao.Update(doctor);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(Doctor doctor)
        {
            var check = bookingDao.GetBookingByDoctor(doctor.IdDoctor);
            if (check.Count == 0)
            {
                doctorDao.Delete(doctor.IdDoctor);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}