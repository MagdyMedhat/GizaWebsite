using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GizaWebsite.Components.Correspondence
{
    public class CorrespondenceOUTController : Controller
    {
        CMSEntities m_context = new CMSEntities();

        public CorrespondenceOUTController()
        {
            m_context.Configuration.ProxyCreationEnabled = false;
        }

        /*********************************** VIEWs ***************************************/
        public ActionResult index()
        {
            return View("~/Components/CorrespondenceOUT/Correspondence.cshtml");
        }

        public ActionResult create()
        {
            return View("~/Components/CorrespondenceOUT/CreateCorrespondence.cshtml");
        }

        public ActionResult update()
        {
            return View("~/Components/CorrespondenceOUT/UpdateCorrespondence.cshtml");
        }

        public ActionResult open()
        {
            return View("~/Components/CorrespondenceOUT/OpenCorrespondence.cshtml");
        }

        /*********************************** APIs ***************************************/


        private List<int> getLinkage(int? relatedCorrID)
        {
            List<int> correspondenceChain = new List<int>();
            while (relatedCorrID != null)
            {
                if (m_context.CORRESPONDENCE_OUT.Select(c => c.ID == relatedCorrID) != null)
                    correspondenceChain.Add(Convert.ToInt32(relatedCorrID));
                var ret = (from c in m_context.CORRESPONDENCE_OUT where c.ID == relatedCorrID select c).FirstOrDefault();
                if (ret != null)
                    relatedCorrID = ret.RELATED_CORRESPONDENCE_ID;
                else
                    break;
            }
            return correspondenceChain;
        }
        public ActionResult openCorrespondence(int id)
        {
            var objects = new List<Object>();
            var C = (from c in m_context.CORRESPONDENCE_OUT where c.ID == id select c).FirstOrDefault();

            //[0] correspondence object
            objects.Add(C);
            var directory = new DirectoryInfo(Server.MapPath("~/Uploads/correspondenceOUT/" + C.ID.ToString()));

            //[1] files
            if (directory.Exists)
            {
                var files = directory.GetFiles("*.pdf");
                var fileNames = from f in files
                                select new
                                {
                                    fileName = f.Name,
                                    directory = "/Uploads/correspondenceOUT/" + C.ID.ToString() + "/" + f.Name
                                };
                objects.Add(fileNames);
            }
            else
            {
                objects.Add(null);
            }

            //[2] linkage
            if (C.RELATED_CORRESPONDENCE_ID != null)
            {
                objects.Add(this.getLinkage(C.RELATED_CORRESPONDENCE_ID));
            }
            else
            {
                objects.Add(null);
            }

            return Json(objects, JsonRequestBehavior.AllowGet);
        }

        public int createCorrespondence(CORRESPONDENCE_OUT c)
        {
            try
            {
                m_context.CORRESPONDENCE_OUT.Add(c);
                m_context.SaveChanges();
                return c.ID;
            }
            catch (Exception ex)
            {
                //logging
                return 0;
            }
        }

        public int updateCorrespondence(CORRESPONDENCE_OUT c)
        {
            try
            {
                var original = m_context.CORRESPONDENCE_OUT.Find(c.ID);
                if (original != null)
                {
                    m_context.Entry(original).CurrentValues.SetValues(c);
                    m_context.SaveChanges();
                    return c.ID;
                }
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool deleteCorrespondence(int id)
        {
            try
            {
                var c = new CORRESPONDENCE_OUT { ID = id };
                m_context.CORRESPONDENCE_OUT.Attach(c);
                m_context.CORRESPONDENCE_OUT.Remove(c);
                m_context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                //logging;
                return false;
            }

        }

        public ActionResult SearchCorrespondence(string keyword, string correspondenceType, string pageNumber)
        {

            var searchQuery = from c in m_context.CORRESPONDENCE_OUT select c;

            if (!string.IsNullOrEmpty(keyword))
            {
                searchQuery = searchQuery.Where(c => c.SENT_TO_ENTITY_NAME.Contains(keyword)
                                                || c.OFFICE_ORGANIZATION.Contains(keyword)
                                                || c.RECIEVER_NAME.Contains(keyword)
                                                );
            }

            var result = from c in searchQuery
                         select new
                         {
                             ID = c.ID,
                             //to be fixed
                             NUMBER = c.ID,
                             RECIEPT_DATE=c.RECIEPT_DATE,
                             ARCHIVE_OUT_NUMBER=c.ARCHIVE_OUT_NUMBER,
                             ARCHIVE_OUT_NUMBER_DATE=c.ARCHIVE_OUT_NUMBER_DATE,
                             COMPLAINT_NAME=c.COMPLAINT_NAME,
                             COMPLAINT_TYPE=c.COMPLAINT_TYPE,
                             DOCUMENT_TYPE=c.DOCUMENT_TYPE,
                             FOLLOWUP_ENTITY_TYPE=c.ENTITY_TYPE,
                             FROM_OFFICE_SENT_DATE=c.FROM_OFFICE_SENT_DATE,
                             FROM_OFFICE_SENT_NUMBER=c.FROM_OFFICE_SENT_NUMBER,
                             NO_OF_ATTACHMENTS = c.NO_OF_ATTACHMENTS,
                             NOTES = c.NOTES,
                             OFFICE_ORGANIZATION = c.OFFICE_ORGANIZATION,
                             RECIEVER_NAME=c.RECIEVER_NAME,
                             REPLIES=c.REPLIES,
                             REPLY_DATE=c.REPLY_DATE,
                             REPLY_DURATION=c.REPLY_DURATION,
                             REPLY_TYPE=c.REPLY_TYPE, 
                             SENT_TO_ENTITY_NAME=c.SENT_TO_ENTITY_NAME,
                             has_links =c.RELATED_CORRESPONDENCE_ID,
                         };

            int count = result.Count();
            int pageSize = 10;
            int startIndex = (Convert.ToInt32(pageNumber) - 1) * pageSize;

            int skip = startIndex <= 0 ? 0 : startIndex;
            result = (from c in result
                      orderby c.ID descending
                      select c).Skip(skip).Take(pageSize);

            return Json(new object[] { result, count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult uploadFile(int id)
        {
            if (Request.Files.Count > 0)
            {
                //var file = Request.Files[0];                
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        //System.IO.File.ReadAllBytes(
                        var path = Server.MapPath("~/Uploads/correspondenceOUT/" + id.ToString() + "/");
                        var filePath = Path.Combine(path, fileName);
                        Directory.CreateDirectory(path);
                        file.SaveAs(filePath);
                    }
                }
            }

            return Json("uploaded", JsonRequestBehavior.AllowGet);
        }
        public ActionResult getCorrespondenceWithNumber(string CorrNo)
        {
            List<int> corrs = (from c in m_context.CORRESPONDENCE_IN where c.CORRESPONDENCE_NO == CorrNo select c.ID).ToList();
            return Json(corrs, JsonRequestBehavior.AllowGet);

        }
    }
}
