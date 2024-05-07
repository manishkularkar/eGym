using eGym.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace eGym.Models
{
    public class RegistrationModel
    {
        public int Reg_Id { get; set; }
        public string Full_Name { get; set; }
        public string Mobile { get; set; }
        public string Photo { get; set; }
        public Nullable<decimal> Plans { get; set; }
        public string StartDate { get; set; }
        public string DueDate { get; set; }
        public string Address { get; set; }
        public decimal TotalIncome { get; set; }
        public int TotalMember { get; set; } 
        public int TodayTotalMember { get; set; } 
        public int MonthTotalMember { get; set; } 
        public int YearTotalMember { get; set; }
        public int TodayTotalReniwal { get; private set; }

        public int TotalReniwal { get; set; }
        public decimal TodayTotalIncome { get; private set; }
        public decimal MonthTotalIncome { get; private set; }
        public decimal YearTotalIncome { get; private set; }
        public int MonthTotalReniwal { get; set; }
        public int YearTotalReniwal { get; private set; }

        public string SaveRegistration(HttpPostedFileBase fb, RegistrationModel model)
        {
            string msg = "";
            GymEntities db = new GymEntities();
            string filePath = "";
            string FileName = "";
            string sysFileName = "";

            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/Pages/image");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                FileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/Pages/image") + "/" + sysFileName;
                }
            }
            var checkreg = db.tblRegs.Where(p => p.Mobile == model.Mobile).FirstOrDefault();

            if (model.Reg_Id == 0)
            {
                if (checkreg == null)
                {
                    var regdata = new tblReg()
                    {

                        Full_Name = model.Full_Name,
                        Mobile = model.Mobile,
                        Photo = sysFileName,
                        Plans = model.Plans,
                        StartDate = Convert.ToDateTime(model.StartDate),
                        DueDate = Convert.ToDateTime(model.DueDate),
                        Address = model.Address,
                    };
                    db.tblRegs.Add(regdata);
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Member Is Already Exist";
                    //return msg;
                }

            }
            else
            {
                var getdata = db.tblRegs.Where(p => p.Reg_Id == model.Reg_Id).FirstOrDefault();

                if (getdata != null)
                {
                    getdata.Full_Name = model.Full_Name;
                    getdata.Mobile = model.Mobile;
                    getdata.Photo = sysFileName;
                    getdata.Plans = model.Plans;
                    getdata.StartDate = Convert.ToDateTime(model.StartDate);
                    getdata.DueDate = Convert.ToDateTime(model.DueDate);
                    getdata.Address = model.Address;
                };
                db.SaveChanges();
                msg = "Updated  Successfully";
            }
            return msg;

        }

        public List<RegistrationModel> GetRegistration()
        {

            GymEntities db = new GymEntities();
            List<RegistrationModel> lstReg = new List<RegistrationModel>();
            var RegData = db.tblRegs.ToList();
            if (RegData != null)
            {
                foreach (var Reg in RegData)
                {
                    lstReg.Add(new RegistrationModel()
                    {
                        Reg_Id = Reg.Reg_Id,
                        Full_Name = Reg.Full_Name,
                        Mobile = Reg.Mobile,
                        Photo = Reg.Photo,
                        Plans = Reg.Plans,
                        StartDate = Reg.StartDate.ToShortDateString(),
                        DueDate = Reg.DueDate.ToShortDateString(),
                        Address = Reg.Address,

                    });
                }
            }
            return lstReg;
        }
        public RegistrationModel GetMemberDetails(int Reg_Id)
        {
            GymEntities db = new GymEntities();
            RegistrationModel model = new RegistrationModel();
            try
            {
                var getUser = db.tblRegs.Where(p => p.Reg_Id == Reg_Id).FirstOrDefault();
                if (getUser != null)
                {
                    model.Reg_Id = getUser.Reg_Id;
                    model.Full_Name = getUser.Full_Name;
                    model.Mobile = getUser.Mobile;
                    model.Photo = getUser.Photo;
                    model.Plans = getUser.Plans;
                    model.StartDate = getUser.StartDate.ToShortDateString();
                    model.DueDate = getUser.DueDate.ToShortDateString();
                    model.Address = getUser.Address;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
        public string DeleteMember(int Reg_Id)
        {
            string msg = "";
            GymEntities Db = new GymEntities();
            var DeleteRegister = Db.tblRegs.Where(p => p.Reg_Id == Reg_Id).FirstOrDefault();
            if (DeleteRegister != null)
            {
                Db.tblRegs.Remove(DeleteRegister);
            };
            Db.SaveChanges();
            msg = "Member Removed";
            return msg;
        }

        public List<RegistrationModel> sendRenival()
        {
            try
            {
             
                DateTime dudate = Convert.ToDateTime(DateTime.Now.AddDays(-4));
                GymEntities Db = new GymEntities();
                List<RegistrationModel> SendRenival = new List<RegistrationModel>();
                if (dudate <= Convert.ToDateTime(DateTime.Now.AddDays(-4)))
                {
                    var getRenival = Db.tblRegs.Where(p => p.DueDate >= dudate && p.DueDate <= DateTime.Now).ToList();
                    if (getRenival != null)
                    {
                        foreach (var Renival in getRenival)
                        {
                            SendRenival.Add(new RegistrationModel()
                            {
                                Reg_Id = Renival.Reg_Id,
                                Full_Name = Renival.Full_Name,
                                Mobile = Renival.Mobile,
                                Photo = Renival.Photo,
                                Plans = Renival.Plans,
                                StartDate = Renival.StartDate.ToShortDateString(),
                                DueDate = Renival.DueDate.ToShortDateString(),
                                Address = Renival.Address,

   
                            });

                            string Mobile = Renival.Mobile;
                            string sAPIKey = "6525215a34a72";
                            string sNumber = Mobile;
                            string sMessage = "Dear"+Renival.Full_Name+"Your Plan Has been expired  On "+Renival.DueDate+"Please Do Reniwal If Your Plan is Reniwal Please Ignore This Message Thanks And Regard From Revolution Fitness Gym";
                            string sSenderId = "6560B7938A1F5";
                            //string sChannel = "trans";
                            //string sRoute = "8";
                            string sURL = "https://cloud.connects.digital/api/send?number=91" + sNumber + "&type=text&message=" + sMessage + "&instance_id=" + sSenderId + "&access_token=" + sAPIKey + "";
                            string sResponse = GetResponse(sURL);
                        }
                    }
                    return SendRenival;
                }
                else
                {
                    return SendRenival;
                }
              
                
               
            }
            catch (Exception)
            {
               // return SendRenival;
                throw;
                
            }
           

        }
        public static string GetResponse(string sURL)
         {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public RegistrationModel GetCountMember()
        {
            GymEntities db = new GymEntities();
            DateTime today = Convert.ToDateTime(DateTime.Today);
            DateTime dudate = Convert.ToDateTime(DateTime.Now.AddDays(-10));
            RegistrationModel model = new RegistrationModel();
            try
            {
                model.MonthTotalMember = 0;

             
                model.TotalIncome = (from emp in db.tblRegs.ToList()
                                     select emp).Sum(e => Convert.ToDecimal(e.Plans));
               model.TotalMember = (from member in db.tblRegs.ToList()select member).Count();
                model.TodayTotalMember = db.tblRegs.Where(p => p.StartDate >= DateTime.Today).Count();
                model.MonthTotalMember = db.tblRegs.Where(p => p.StartDate.Month >= (DateTime.Now.Month)).Count();
                model.YearTotalMember = db.tblRegs.Where(p => p.StartDate.Year >= (DateTime.Now.Year)).Count();
                model.TodayTotalReniwal = db.tblRegs.Where(p => p.DueDate >= today && p.DueDate <= DateTime.Today).Count();
                model.MonthTotalReniwal = db.tblRegs.Where(p => p.DueDate.Month >=DateTime.Now.Month && p.DueDate.Month <= DateTime.Now.Month).Count();
                model.YearTotalReniwal = db.tblRegs.Where(p => p.DueDate.Year >=DateTime.Now.Year && p.DueDate.Year <= DateTime.Now.Year).Count();
                model.TotalReniwal = db.tblRegs.Where(p => p.DueDate >= dudate && p.DueDate <= DateTime.Now).Count();
                model.TodayTotalIncome = (from emp in db.tblRegs.Where(p=>p.StartDate>=DateTime.Today).ToList()
                                     select emp).Sum(e => Convert.ToDecimal(e.Plans));
                model.MonthTotalIncome = (from emp in db.tblRegs.Where(p => p.StartDate.Month >= DateTime.Now.Month).ToList()
                                          select emp).Sum(e => Convert.ToDecimal(e.Plans));
                model.YearTotalIncome = (from emp in db.tblRegs.Where(p => p.StartDate.Year >= DateTime.Now.Year).ToList()
                                          select emp).Sum(e => Convert.ToDecimal(e.Plans));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
    }
}