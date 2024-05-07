using eGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGym.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult ScheduleIndex()
        {
            return View();
        }
    }
}