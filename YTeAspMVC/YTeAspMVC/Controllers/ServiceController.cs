using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTeAspMVC.Daos;

namespace YTeAspMVC.Controllers
{
    public class ServiceController : Controller
    {
        ServiceDao serviceDao = new ServiceDao();
        // GET: Service
        public ActionResult Index()
        {
            ViewBag.List = serviceDao.GetAll();
            Session.Add("Active", "Service");
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Service = serviceDao.GetService(id);
            return View();
        }
    }
}