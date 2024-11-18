using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTeAspMVC.Daos;
using YTeAspMVC.Models;

namespace YTeAspMVC.Controllers
{
    public class AuthencationController : Controller
    {
        UserDao userDao = new UserDao();
        NumberDao numberDao = new NumberDao();
        BookingDao bookingDao = new BookingDao();
        // GET: Authencation
        public ActionResult Login()
        {
            Session.Add("Active", "Login");
            return View();
        }

        public ActionResult Singup()
        {
            Session.Add("Active", "Singup");
            return View();
        }

        public ActionResult Information(string msg)
        {
            User user = (User)Session["USER"];
            ViewBag.mess = msg;
            ViewBag.User = userDao.getById(user.IdUser);
            return View();
        }

        public ActionResult HistoryNumber()
        {
            User user = (User)Session["USER"];
            ViewBag.List = numberDao.GetNumberByUser(user.IdUser);
            return View();
        }

        public ActionResult Print(int id)
        {
            ViewBag.Number = numberDao.GetById(id);
            return View();
        }

        public ActionResult HistoryBooking(string mess)
        {
            User user = (User)Session["USER"];
            ViewBag.Msg = mess;
            ViewBag.List = bookingDao.GetBookingByUser(user.IdUser);
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            bool checkLogin = userDao.checkLogin(user.Email, user.Password);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(user.Email);
                Session.Add("USER", userInformation);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.mess = "Error";
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult Singup(User user)
        {
            
            bool checkExistUserName = userDao.checkExistEmail(user.Email);
            if (checkExistUserName)
            {
                ViewBag.mess = "ErrorExist";
                return View("Singup");
            }
            else
            {
                user.IdRole = 1;
                user.Status = 1;
                user.Created = DateTime.Now.ToString();
                userDao.Add(user);
                ViewBag.mess = "Success";
                return View("Singup");
            }
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            userDao.Update(user);
            return RedirectToAction("Information", new { msg = "Success" });
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult NumberOnline()
        {
            User user = (User)Session["USER"];
            bool check = numberDao.CheckUserNumberDay(user.IdUser);
            if (!check)
            {
                return RedirectToAction("Index", "Home", new { mess = "2" });
            }
            else {
                int numberInt = numberDao.GetNumberToday();
                var numberAdd = new Number()
                {
                    NumberInt = numberInt + 1,
                    Day = DateTime.Now.Date.ToString(),
                    IdUser = user.IdUser
                };
                numberDao.Add(numberAdd);
                return RedirectToAction("HistoryNumber", "Authencation", new { mess = "1" });
            }
          
        }
    }
}