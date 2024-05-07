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

        public ActionResult SaveNoticeInfo(NoticeModel model)
        {
            try
            {
                return Json(new { Message = new NoticeModel().SaveNoticeInfo(model) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}