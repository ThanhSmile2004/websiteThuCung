using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminAuthenticationController : Controller
    {
        UserDao userDao = new UserDao();
        DoctorDao doctorDao = new DoctorDao();
        // GET: AdminAuthentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult LoginDoctor()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            User user = new User()
            {
                Email = form["email"],
                Password = form["password"]
            };
            bool checkLogin = userDao.checkLogin(user.Email, user.Password);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(user.Email);

                if (userInformation.IdRole == 1)
                {
                    ViewBag.mess = "Bạn không có quyền truy cập vào trang quản trị";
                    return View("Login");
                }
                else
                {
                    Session.Add("ADMIN", userInformation);
                    return RedirectToAction("Index", "AdminHome");
                }

            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult LoginDoctor(FormCollection form)
        {
            Doctor user = new Doctor()
            {
                Email = form["email"],
                Password = form["password"]
            };
            bool checkLogin = doctorDao.checkLogin(user.Email, user.Password);
            if (checkLogin)
            {
                var userInformation = doctorDao.getUserByEmail(user.Email);
                Session.Add("DOCTOR", userInformation);
                return RedirectToAction("Index", "AdminHome");

            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            Session.Remove("ADMIN");
            Session.Remove("DOCTOR");
            return Redirect("/AdminHome/Index");
        }
    }
}