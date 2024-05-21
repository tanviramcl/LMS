using CrystalDecisions.CrystalReports.Engine;
using LoanManagementSystem.Loan.layer;
using LoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanManagementSystem.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        private ReportDocument rdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Employee> allEmployee = new List<Employee>();
            allEmployee = emplyeeManagment.Employee_LIST;


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(allEmployee);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

            //dt.TableName = "EmployeeList";
            //dt.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\xsdEmployeeList.xsd");


            if (dt.Rows.Count > 0 || dt.Rows.Count > 0)
            {
                string Path = Server.MapPath("crtEmployeeList.rpt");
                rdoc.Load(Path);
         
                rdoc.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rdoc;
                rdoc = ReportFactory.GetReport(rdoc.GetType());


            }
            else
            {
                Response.Write("No Data Found");
            }

        }
    }
}