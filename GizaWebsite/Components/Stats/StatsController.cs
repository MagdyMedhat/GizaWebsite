using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GizaWebsite.Components.HomePage
{
    public class StatsController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Index()
        {
            return View("~/Components/Stats/Stats.cshtml");
        }
        public ActionResult getStats()
        {
               try
                {
                    CMSEntities m_context = new CMSEntities();
                    m_context.Configuration.ProxyCreationEnabled = false;
                    string numberOfOUTCorrespondences = (from rows in m_context.CORRESPONDENCE_OUT select rows).Count().ToString();
                    string numberOfINCorrespondences = (from rows in m_context.CORRESPONDENCE_IN select rows).Count().ToString();
                    string numberOfStatuses = (from rows in m_context.STATUS select rows).Count().ToString();
                    string numberOfImportanceLevels = (from rows in m_context.IMPORTANCEs select rows).Count().ToString();
                    int relatedIN = (from rows in m_context.CORRESPONDENCE_IN where rows.RELATED_CORRESPONDENCE_ID != null select rows.RELATED_CORRESPONDENCE_ID).Count();
                    int relatedOut = (from rows in m_context.CORRESPONDENCE_OUT where rows.RELATED_CORRESPONDENCE_ID != null select rows.RELATED_CORRESPONDENCE_ID).Count();
                    string numberOfRelatedCorrespondeces = (relatedIN + relatedOut).ToString();
                    return Json(new object[] { numberOfOUTCorrespondences, numberOfINCorrespondences, numberOfStatuses, numberOfImportanceLevels, numberOfRelatedCorrespondeces }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json(new object[] { 0 }, JsonRequestBehavior.AllowGet);
                }
           
        }

        public ActionResult Noaccess()
        {
            return View("~/Components/Common/Error_noaccess.cshtml");
        }
    }
}
