using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GizaWebsite.Components.HomePage
{
    public class HomeController : Controller
    {
        //
        // GET: /Web/

        public ActionResult Index()
        {
            return View("~/Components/HomePage/Home.cshtml");
        }
        public ActionResult getHomePageContent()
        {
               try
                {
                    CMSEntities m_context = new CMSEntities();
                    m_context.Configuration.ProxyCreationEnabled = false;
                    string content = (from rows in m_context.HOMEPAGE_CONTENT select rows.HTML_CONTENT).FirstOrDefault();
                    return Json(new object[] { content }, JsonRequestBehavior.AllowGet);

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
