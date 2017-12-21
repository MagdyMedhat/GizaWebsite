using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GizaWebsite.Components.Correspondence
{
    public class CorrespondenceINController : Controller
    {
        CMSEntities m_context = new CMSEntities();

        public CorrespondenceINController()
        {
            m_context.Configuration.ProxyCreationEnabled = false;
        }

        /*********************************** VIEWs ***************************************/
        public ActionResult index()
        {
            return View("~/Components/CorrespondenceIN/Correspondence.cshtml");
        }

        public ActionResult create()
        {
            return View("~/Components/CorrespondenceIN/CreateCorrespondence.cshtml");
        }

        public ActionResult update()
        {
            return View("~/Components/CorrespondenceIN/UpdateCorrespondence.cshtml");
        }

        public ActionResult open()
        {
            return View("~/Components/CorrespondenceIN/OpenCorrespondence.cshtml");
        }

        /*********************************** APIs ***************************************/


        private List<int> getLinkage(int? relatedCorrID)
        {
            List<int> correspondenceChain = new List<int>();
            while (relatedCorrID != null)
            {
                if(m_context.CORRESPONDENCE_IN.Select(c=>c.ID==relatedCorrID)!=null)
                correspondenceChain.Add(Convert.ToInt32(relatedCorrID));
                var ret = (from c in m_context.CORRESPONDENCE_IN where c.ID == relatedCorrID select c).FirstOrDefault();
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
            var C = (from c in m_context.CORRESPONDENCE_IN where c.ID == id select c).FirstOrDefault();

            //[0] correspondence object
            objects.Add(C);
              var directory = new DirectoryInfo(Server.MapPath("~/Uploads/correspondenceIN/" + C.ID.ToString()));

            //[1] files
            if (directory.Exists)
            {
                var files = directory.GetFiles("*.pdf");
                var fileNames = from f in files
                                select new
                                {
                                    fileName = f.Name,
                                    directory = "/Uploads/correspondenceIN/" + C.ID.ToString() + "/" + f.Name
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

        public int createCorrespondence(CORRESPONDENCE_IN c)
        {
            try
            {
                m_context.CORRESPONDENCE_IN.Add(c);
                m_context.SaveChanges();
                return c.ID;
            }
            catch (Exception ex)
            {
                //logging
                return 0;
            }
        }

        public int updateCorrespondence(CORRESPONDENCE_IN c)
        {
            try
            {
                var original = m_context.CORRESPONDENCE_IN.Find(c.ID);
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
                var c = new CORRESPONDENCE_IN { ID = id };
                m_context.CORRESPONDENCE_IN.Attach(c);
                m_context.CORRESPONDENCE_IN.Remove(c);
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

            //infere the type IQueryable<CORRESPONDENCE>
            //var searchQuery = from c in m_context.CORRESPONDENCE_IN.Include(c => c.IMPORTANCE) select c;
            var searchQuery = from c in m_context.CORRESPONDENCE_IN select c;

            if (!string.IsNullOrEmpty(keyword))
            {
                searchQuery = searchQuery.Where(c => c.CORRESPONDENCE_NO.Contains(keyword)
                                                || c.TOPIC.Contains(keyword)
                                                || c.STATUS.NAME_AR.Contains(keyword)
                                                || c.IMPORTANCE.NAME_AR.Contains(keyword)
                                                );
            }

            var result = from c in searchQuery
                         select new
                         {
                             id = c.ID,
                             topic = c.TOPIC,
                             number = c.CORRESPONDENCE_NO,
                             date = c.TO_OFFICE_RECEIPT_DATE,
                             importance = c.IMPORTANCE.NAME_AR,
                             status = c.STATUS.NAME_AR,
                             has_links =c.RELATED_CORRESPONDENCE_ID,
                         };

            int count = result.Count();
            int pageSize = 10;
            int startIndex = (Convert.ToInt32(pageNumber) - 1) * pageSize;

            int skip = startIndex <= 0 ? 0 : startIndex;
            result = (from c in result
                      orderby c.id descending
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
                        var path = Server.MapPath("~/Uploads/correspondenceIN/" + id.ToString() + "/");
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
            List<int> corrs = (from c in m_context.CORRESPONDENCE_IN where c.CORRESPONDENCE_NO ==CorrNo select c.ID).ToList();
            return Json(corrs, JsonRequestBehavior.AllowGet);

        }
    }
}
