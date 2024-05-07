using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGym.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult EventIndex()
        {
            return View();
        }
    }
}