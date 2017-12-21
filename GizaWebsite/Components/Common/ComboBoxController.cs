using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GizaWebsite.Components.Common
{
    public class ComboBoxController : Controller
    {
        CMSEntities m_context = new CMSEntities();

        public ComboBoxController()
        {
            m_context.Configuration.ProxyCreationEnabled = false;
        }

        public ActionResult GetAddingEntity()
        {
            var query = from table in m_context.ADDING_ENTITY_TYPE select new { id = table.ID, name = table.NAME_AR};
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCorrespondenceType()
        {
            var query = from table in m_context.C_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocumentType()
        {
            var query = from table in m_context.DOCUMENT_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImportance()
        {
            var query = from table in m_context.IMPORTANCEs select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSavedDocumentType()
        {
            var query = from table in m_context.DOCUMENT_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSendingEntityType()
        {

            var query = from table in m_context.ENTITY_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStatus()
        {
            var query = from table in m_context.STATUS select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopicType()
        {
            var query = from table in m_context.TOPIC_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShelves()
        {
            var query = from table in m_context.SHELves select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetClosets()
        {
            var query = from table in m_context.CLOSETs select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComplaintTypes()
        {
            var query = from table in m_context.COMPLAINT_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReplyTypes()
        {
            var query = from table in m_context.REPLY_TYPE select new { id = table.ID, name = table.NAME_AR };
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetIncomingCorrespondeceEntities()
        //{
        //    var query = from c in m_context.CORRESPONDENCEs
        //                join addingEnt in m_context.ADDING_ENTITY_TYPE on
        //                c.ADDING_ENTITY_TYPE equals addingEnt.ID
        //                join topic in m_context.TOPIC_TYPE on
        //                c.TOPIC_TYPE equals topic.ID
        //                join status in m_context.STATUS on
        //                c.STATUS equals status.ID
        //                select new
        //                {
        //                    number = c.CORRESPONDENCE_NO,
        //                    addingEntity = addingEnt.NAME_AR,
        //                    topicType = topic.NAME_AR,
        //                    status = status,
        //                    date = c.INCOMING_DATE
        //                };
        //    return Json(query, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetOutgoingCorrespondenceEntities()
        //{
        //    var query = from c in m_context.CORRESPONDENCEs
        //                join sendingEnt in m_context.SENDING_ENTITY_TYPE on
        //                c.SENDING_ENTITY_TYPE equals sendingEnt.ID
        //                join topic in m_context.TOPIC_TYPE on
        //                c.TOPIC_TYPE equals topic.ID
        //                join status in m_context.STATUS on
        //                c.STATUS equals status.ID
        //                select new
        //                {
        //                    number = c.CORRESPONDENCE_NO,
        //                    sendingEntity = sendingEnt.NAME_AR,
        //                    topicType = topic.NAME_AR,
        //                    status = status,
        //                    date = c.TO_OFFICE_RECEIPT_DATE
        //                };
        //    return Json(query, JsonRequestBehavior.AllowGet);
        //}

    }
}
