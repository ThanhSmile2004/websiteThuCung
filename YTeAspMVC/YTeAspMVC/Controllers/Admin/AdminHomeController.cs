using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            User user = (User)Session["ADMIN"];
            Doctor doctor = (Doctor)Session["DOCTOR"];
            if (user == null && doctor == null)
            {
                return RedirectToAction("Login", "AdminAuthentication");
            }
            else
            {
                return View();
            }
        }


    }
}