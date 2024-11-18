using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminBookingController : Controller
    {
        BookingDao bookingDao = new BookingDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = bookingDao.GetAll();
            return View();
        }

        public ActionResult History()
        {
            ViewBag.List = bookingDao.GetHistory();
            return View();
        }

        public ActionResult Doctor(string msg)
        {
            Doctor doctor = (Doctor)Session["DOCTOR"];
            ViewBag.Msg = msg;
            ViewBag.List = bookingDao.GetBookingByDoctor(doctor.IdDoctor);
            return View();
        }

        public ActionResult Approve(Booking booking)
        {
            bookingDao.Approve(booking.IdBooking);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Complete(Booking booking)
        {
            bookingDao.Complete(booking.IdBooking);
            return RedirectToAction("Doctor", new { msg = "1" });
        }
    }
}