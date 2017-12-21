using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GizaWebsite.Components.Common
{
    public class DashboardController : Controller
    {
        // GET: /Web/

        public ActionResult Index()
        {
            return View("~/Components/Dashboard/dashboard.cshtml");
        }

        public ActionResult Homepage()
        {
            return View("~/Components/Dashboard/Homepage.cshtml");
        }
             
        public ActionResult getLookups()
        {

            try
            {
                 CMSEntities m_context = new CMSEntities();
                 m_context.Configuration.ProxyCreationEnabled = false;

                var sending_entity_types =
                    from row in m_context.ENTITY_TYPE
                    select new 
                      {
                          text = row.NAME_AR
                      };
                var status_types = (from row in m_context.STATUS
                                    select new 
                      {
                          text = row.NAME_AR
                      });
                var document_types = (from row in m_context.DOCUMENT_TYPE
                                                     select new 
                      {
                          text = row.NAME_AR
                      });
                var topic_types = (from row in m_context.TOPIC_TYPE
                                                   select new 
                      {
                          text = row.NAME_AR
                      });
                var adding_entity_types = (from row in m_context.ADDING_ENTITY_TYPE
                                                          select new 
                      {
                          text = row.NAME_AR
                      });
                var c_type = (from row in m_context.C_TYPE
                             select new
                             {
                                 text = row.NAME_AR
                             });
                var importance = (from row in m_context.IMPORTANCEs
                                  select new
                                  {
                                      text = row.NAME_AR
                                  });
                var closets = (from row in m_context.CLOSETs
                                  select new
                                  {
                                      text = row.NAME_AR
                                  });
                var shelves = (from row in m_context.SHELves
                                  select new
                                  {
                                      text = row.NAME_AR
                                  });
                return Json(new object[]{
                               sending_entity_types.ToList() , 
                               status_types.ToList(), 
                               document_types.ToList() ,  
                               topic_types.ToList(),
                               adding_entity_types.ToList(),
                               c_type.ToList(),
                               importance.ToList(),
                               closets.ToList(),
                               shelves.ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveSendingEntityTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.ENTITY_TYPE.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.ENTITY_TYPE.Add(new ENTITY_TYPE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                           ENTITY_TYPE s = (ENTITY_TYPE)m_context.ENTITY_TYPE.Where(a => a.NAME_AR == item).First();
                            m_context.ENTITY_TYPE.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current  = (from row in m_context.ENTITY_TYPE select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current  }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveStatus(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.STATUS.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.STATUS.Add(new STATUS { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            STATUS s = (STATUS)m_context.STATUS.Where(a => a.NAME_AR == item).First();
                            m_context.STATUS.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
               
                var currentStatus = (from row in m_context.STATUS select row.NAME_AR).ToList();
                return Json(new object[]{failedToDelete,currentStatus}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveDocumentTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.DOCUMENT_TYPE.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.DOCUMENT_TYPE.Add(new DOCUMENT_TYPE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            DOCUMENT_TYPE s = (DOCUMENT_TYPE)m_context.DOCUMENT_TYPE.Where(a => a.NAME_AR == item).First();
                            m_context.DOCUMENT_TYPE.Remove(s);
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                m_context.SaveChanges();
                var current = (from row in m_context.DOCUMENT_TYPE select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveTopicTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.TOPIC_TYPE.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.TOPIC_TYPE.Add(new TOPIC_TYPE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            TOPIC_TYPE s = (TOPIC_TYPE)m_context.TOPIC_TYPE.Where(a => a.NAME_AR == item).First();
                            m_context.TOPIC_TYPE.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.TOPIC_TYPE select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveAddingEntityTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.ADDING_ENTITY_TYPE.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.ADDING_ENTITY_TYPE.Add(new ADDING_ENTITY_TYPE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            ADDING_ENTITY_TYPE s = (ADDING_ENTITY_TYPE)m_context.ADDING_ENTITY_TYPE.Where(a => a.NAME_AR == item).First();
                            m_context.ADDING_ENTITY_TYPE.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.ADDING_ENTITY_TYPE select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveCTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.C_TYPE.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.C_TYPE.Add(new C_TYPE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            C_TYPE s = (C_TYPE)m_context.C_TYPE.Where(a => a.NAME_AR == item).First();
                            m_context.C_TYPE.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.C_TYPE select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveImportanceTypes(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.IMPORTANCEs.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.IMPORTANCEs.Add(new IMPORTANCE { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            IMPORTANCE s = (IMPORTANCE)m_context.IMPORTANCEs.Where(a => a.NAME_AR == item).First();
                            m_context.IMPORTANCEs.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.IMPORTANCEs select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult saveClosets(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.CLOSETs.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.CLOSETs.Add(new CLOSET { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            CLOSET s = (CLOSET)m_context.CLOSETs.Where(a => a.NAME_AR == item).First();
                            m_context.CLOSETs.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.CLOSETs select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult saveShelves(List<string> toBeDeleted, List<string> toBeAdded)
        {
            try
            {
                CMSEntities m_context = new CMSEntities();
                m_context.Configuration.ProxyCreationEnabled = false;
                List<string> failedToDelete = new List<string>();
                if (toBeAdded != null)
                {
                    foreach (var item in toBeAdded)
                    {
                        var query = m_context.SHELves.Where(a => a.NAME_AR == item);
                        if (!query.Select(q => q.NAME_AR).Contains(item))
                            m_context.SHELves.Add(new SHELF { NAME_AR = item });
                    }
                    m_context.SaveChanges();
                }
                if (toBeDeleted != null)
                {
                    foreach (var item in toBeDeleted)
                    {
                        try
                        {
                            SHELF s = (SHELF)m_context.SHELves.Where(a => a.NAME_AR == item).First();
                            m_context.SHELves.Remove(s);
                            m_context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            failedToDelete.Add(item);
                        }
                    }
                }
                var current = (from row in m_context.SHELves select row.NAME_AR).ToList();
                return Json(new object[] { failedToDelete, current }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult saveHomePageContent(string htmlContent)
        {
            if (!string.IsNullOrEmpty(htmlContent))
            {
                try
                {
                      CMSEntities m_context = new CMSEntities();
                      m_context.Configuration.ProxyCreationEnabled = false;
                      var query = from rows in m_context.HOMEPAGE_CONTENT select rows;
                      foreach (var item in query)
                          m_context.HOMEPAGE_CONTENT.Remove(item);
                      m_context.HOMEPAGE_CONTENT.Add(new HOMEPAGE_CONTENT { HTML_CONTENT = htmlContent });
                      m_context.SaveChanges();
                      return Json(new object[] { 1 }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json(new object[] { 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(new object[] { 0 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getHomePageContent()
        {
            HomePage.HomeController c = new HomePage.HomeController();
            return c.getHomePageContent();
        }
    }
}
