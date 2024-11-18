using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTeAspMVC.Controllers
{
    public class ModulesController : Controller
    {
        // GET: PublicModules
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult NavigationBar()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult MainSlider()
        {
            return PartialView();
        }
        
    }
}