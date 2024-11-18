using YTeAspMVC.Daos;
using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers.Admin
{
    public class AdminPostController : Controller
    {
        PostDao postDao = new PostDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = postDao.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Post post)
        {
            var file = Request.Files["file"];
            string reName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            post.Image = reName;
            post.Status = 1;
            post.Created = DateTime.Now.ToString();
            postDao.Add(post);
            return RedirectToAction("Index", new { msg = "1" });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Post post)
        {
            string reName = "";
            var objCourse = postDao.GetPost(post.IdPost);
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
            post.Image = reName;
            postDao.Update(post);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(Post post)
        {

            postDao.Delete(post.IdPost);
            return RedirectToAction("Index", new { msg = "1" });
            
        }
    }
}