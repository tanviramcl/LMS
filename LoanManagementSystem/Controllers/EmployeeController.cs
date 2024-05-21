using CrystalDecisions.CrystalReports.Engine;
using LoanManagementSystem.Loan.layer;
using LoanManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LoanManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();


        // GET: Employee
        public ActionResult Index()
        {
            List<Models.Employee> empList = emplyeeManagment.Employee_LIST;
            ViewBag.EmpLst = empList;

            return View();
        }



        public JsonResult GetEmployeeList()
        {
            List<Models.Employee> empList = emplyeeManagment.Employee_LIST;
            ViewBag.EmpLst = empList;

            return Json(empList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeetById(int AUTOID)
        {
            Employee model = emplyeeManagment.Employee_LIST.Where(x => x.AUTOID == AUTOID).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


      

        public JsonResult SaveDataInDatabase(Employee model)
        {
            var result = false;
            try
            {
                if (model.AUTOID > 0)
                {
                    Employee emp = emplyeeManagment.Employee_LIST.SingleOrDefault(x => x.AUTOID == model.AUTOID && x.BKACNO == model.BKACNO);
                    emp.TEMP_ADVANCE = model.TEMP_ADVANCE;
                    emp.BASIC = model.BASIC;
                    emp.BKACNO = model.BKACNO;
                    emp.VALID = model.VALID;
                    //Stu.Email = model.Email;
                    //Stu.DepartmentId = model.DepartmentId;
                    //db.SaveChanges();
                    string mess = emplyeeManagment.SaveEmployee(emp);
                    if (mess == "Success")
                    {
                        result = true;
                    }
                }
                else
                {
                    //tblStudent Stu = new tblStudent();
                    //Stu.StudentName = model.StudentName;
                    //Stu.Email = model.Email;
                    //Stu.DepartmentId = model.DepartmentId;
                    //Stu.IsDeleted = false;
                    //db.tblStudents.Add(Stu);
                    //db.SaveChanges();
                    //result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExportEmployees()
        {
            List<Employee> allEmployee = new List<Employee>();
            allEmployee = emplyeeManagment.Employee_LIST;


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(allEmployee);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

          //   dt.TableName = "testEmployeeList";
            // dt.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\testEmployeeList.xsd");


            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "CrystalReport.rpt"));

            rd.SetDataSource(allEmployee);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EmployeeList.pdf");
            

        }


    }
}