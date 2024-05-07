using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGym.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult NoticeIndex()
        {
            return View();
        }
    }
}