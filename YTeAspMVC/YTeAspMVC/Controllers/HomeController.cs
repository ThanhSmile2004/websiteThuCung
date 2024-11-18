using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTeAspMVC.Daos;
using YTeAspMVC.Models;

namespace YTeAspMVC.Controllers
{
    public class HomeController : Controller
    {
        DoctorDao doctorDao = new DoctorDao();
        ServiceDao serviceDao = new ServiceDao();
        PostDao postDao = new PostDao();
        NumberDao numberDao = new NumberDao();
        public ActionResult Index(string mess)
        {
            ViewBag.Doctor = doctorDao.GetTop3();
            ViewBag.Service = serviceDao.GetTop3();
            ViewBag.Post = postDao.GetTop3();
            ViewBag.Msg = mess;
            Session.Add("Active", "Home");
            return View();
        }

        public ActionResult Advise()
        {
            Session.Add("Active", "Advice");
            return View();
        }

        [HttpPost]
        public ActionResult Advise(Advise advise)
        {

            advise.Created = DateTime.Now.ToString();
            postDao.AddAdvise(advise);
            ViewBag.Msg = "1";
            return View("Advise");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        //public JsonResult RemoveNumberOnline()
        //{
        //    List<Number> numbers = numberDao.GetNumber();
        //    for(int i = 0; i< numbers.Count; i++)
        //    {
        //        if(DateTime.Now.Date > DateTime.Parse(numbers[i].Day).Date)
        //        {
        //            numberDao.Remove(numbers[i].IdNumber);
        //        }
        //    }
        //    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult RemoveNumberOnline()
        {
            List<Number> numbers = numberDao.GetNumber();
            for (int i = 0; i < numbers.Count; i++)
            {
                DateTime dateValue;
                // Sử dụng DateTime.TryParse để kiểm tra xem chuỗi có hợp lệ không
                if (DateTime.TryParse(numbers[i].Day, out dateValue))
                {
                    if (DateTime.Now.Date > dateValue.Date)
                    {
                        numberDao.Remove(numbers[i].IdNumber);
                    }
                }
                else
                {
                    // Xử lý trường hợp ngày không hợp lệ
                    return Json(new { Status = false, Message = "Invalid date format for number: " + numbers[i].IdNumber }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}