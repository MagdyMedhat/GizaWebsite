using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GizaWebsite.Components.Employee
{
    public class EmployeeController : Controller
    {
        //views

        CMSEntities m_context = new CMSEntities();

        public EmployeeController()
        {
            m_context.Configuration.ProxyCreationEnabled = false;
        }
        public ActionResult Index()
        {
            return View("~/Components/Employee/Employee.cshtml");
        }

        public ActionResult Create()
        {
            return View("~/Components/Employee/CreateEmployee.cshtml");
        }

        public ActionResult Open()
        {
            return View("~/Components/Employee/OpenEmployee.cshtml");
        }

        public ActionResult Update()
        {
            return View("~/Components/Employee/UpdateEmployee.cshtml");
        }



        //APIs

        public ActionResult GetEmployee(int id)
        {
            var retObjects = new List<object>();

            //[0] employee object
            var emp = m_context.EMPLOYEEs.Find(id);
            retObjects.Add(emp);

            var directory = new DirectoryInfo(Server.MapPath("~/Uploads/employee/" + emp.ID.ToString()));

            //[1] files
            if (directory.Exists)
            {
                var files = directory.GetFiles("*.pdf");
                var fileNames = from f in files
                                select new
                                {
                                    fileName = f.Name,
                                    directory = "/Uploads/employee/" + emp.ID.ToString() + "/" + f.Name
                                };
                retObjects.Add(fileNames);
            }
            else
            {
                retObjects.Add(null);
            }

            return Json(retObjects, JsonRequestBehavior.AllowGet);
        }

        public int? CreateEmployee(EMPLOYEE e)
        {
            try
            {
                m_context.EMPLOYEEs.Add(e);
                m_context.SaveChanges();
                return e.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateEmployee(EMPLOYEE e)
        {
            try
            {
                var original = m_context.EMPLOYEEs.Find(e.ID);
                if (original != null)
                {
                    m_context.Entry(original).CurrentValues.SetValues(e);
                    m_context.SaveChanges();
                    return e.ID;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var e = m_context.EMPLOYEEs.Find(id);
                m_context.EMPLOYEEs.Attach(e);
                m_context.EMPLOYEEs.Remove(e);
                m_context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ActionResult search(string keyword, string pageNumber)
        {
            var searchQuery = from e in m_context.EMPLOYEEs select e;

            if (!string.IsNullOrEmpty(keyword))
            {
                searchQuery = searchQuery.Where(e => e.DEPARTMENT.Contains(keyword)
                                                || e.NATIONAL_ID.Contains(keyword)
                                                || e.NAME.Contains(keyword)
                                                || e.DEPARTMENT.Contains(keyword)
                                                //|| e.FILE_NUMBER == Convert.ToInt32(keyword)
                                                //|| e.INSURANCE_NUMBER == Convert.ToInt32(keyword)
                                                //|| e.DATE_HIRED == Convert.ToDateTime(keyword).Date
                                                );
            }

            var result = from e in searchQuery
                         select new
                         {
                             id = e.ID,
                             fileNumber = e.FILE_NUMBER,
                             nationalID = e.NATIONAL_ID,
                             name = e.NAME,
                             department = e.DEPARTMENT,
                             hireDate = e.DATE_HIRED,
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
                        var path = Server.MapPath("~/Uploads/employee/" + id.ToString() + "/");
                        var filePath = Path.Combine(path, fileName);
                        Directory.CreateDirectory(path);
                        file.SaveAs(filePath);
                        //var SR = new StreamReader(file.InputStream, System.Text.Encoding.ASCII);


                        //var buffer = "";


                        //    buffer = SR.ReadToEnd();


                        //SR.Close();
                        //var SW = new StreamWriter(filePath, false);
                        //SW.Write(buffer);
                        //SW.Close();



                    }
                }
            }

            return Json("uploaded", JsonRequestBehavior.AllowGet);
        }

    }
}
