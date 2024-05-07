using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eGym.Models;

namespace EGym.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult RegistationIndex()
        {
            return View();
        }
        public ActionResult RegistationListIndex()
        {
            return View();
        }
        public ActionResult SaveRegistration(RegistrationModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0;i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];
                }
                return Json(new { Message = (new RegistrationModel().SaveRegistration(fb, model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetRegistration()
        {
            try
            {
                return Json(new { Message = new RegistrationModel().GetRegistration() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMemberDetails(int Reg_Id)
        {
            try
            {
                return Json(new { Message = new RegistrationModel().GetMemberDetails(Reg_Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteMember(int Reg_Id)
        {
            try
            {
                return Json(new { Message = (new RegistrationModel().DeleteMember(Reg_Id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Renival()
        {
            try
            {
                return Json(new { model = (new RegistrationModel().sendRenival()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCountMember()
        {
            try
            {
                return Json(new { Message = (new RegistrationModel().GetCountMember()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}