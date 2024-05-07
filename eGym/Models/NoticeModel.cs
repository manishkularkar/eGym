using eGym.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eGym.Models
{
    public class NoticeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<bool> isActive { get; set; }

        public string SaveNoticeInfo(NoticeModel model)
        {
            string msg = "Data Save Successfully";

            GymEntities Db = new GymEntities();


            {
                var infoData = new tblNotice()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Createdate = model.Createdate,
                    isActive = model.isActive,
                };

                Db.tblNotices.Add(infoData);
                Db.SaveChanges();
                return msg;
            }
        }

    }
}