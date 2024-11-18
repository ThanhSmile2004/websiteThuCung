using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminServiceController : Controller
    {
        ServiceDao serviceDao = new ServiceDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = serviceDao.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Service service)
        {
            var file = Request.Files["file"];
            string reName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            service.Image = reName;
            service.Status = 1;
            service.Created = DateTime.Now.ToString();
            serviceDao.Add(service);
            return RedirectToAction("Index", new { msg = "1" });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Service service)
        {
            string reName = "";
            var objCourse = serviceDao.GetService(service.IdService);
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
            service.Image = reName;
            serviceDao.Update(service);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(Service service)
        {
            
            serviceDao.Delete(service.IdService);
            return RedirectToAction("Index", new { msg = "1" });
            
        }
    }
}