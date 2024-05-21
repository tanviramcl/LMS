using LoanManagementSystem.Loan.layer;
using LoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LoanManagementSystem.Controllers
{
    public class SalaryController : Controller
    {
        SalaryManagment spmg = new SalaryManagment();
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult ShowSalary()
        {
           List<SalaryDetails> empSalarY = spmg.empSalaryDelails_LIST;
            ViewBag.empSalary_LIST = empSalarY;

            return View();
        }
        [HttpPost]

        [AllowAnonymous]
        public JsonResult Process(String SalaryDet)
        {
            string msg="";
            List<SalaryDetails> empSalary_LIST = new List<SalaryDetails>();
            List<Salary> salDet = new JavaScriptSerializer().Deserialize<List<Salary>>(SalaryDet);
            foreach (Salary sal in salDet)
            {
                DataTable dtsalalryProcess = spmg.SalaryProcessGenarate(sal.FromDate.ToString("dd/MM/yyyy"), sal.ToDate.ToString("dd/MM/yyyy"), sal.CaLDate.ToString("dd/MM/yyyy"));

                foreach (DataRow dr in dtsalalryProcess.Rows)
                {
                    SalaryDetails empSalary = new SalaryDetails();

                    empSalary.AUTOID = Convert.ToInt32(dr["AUTOID"]);
                    empSalary.ID = dr["ID"].ToString();
                    empSalary.BASIC = Convert.ToDouble(dr["BASIC"]);
                    empSalary.HOUSE_RENT = Convert.ToDouble(dr["HOUSE_RENT"].ToString());
                    empSalary.HB_ADV = Convert.ToDouble(dr["HB_ADV"]);
                    empSalary.HOUSE_MAINT = Convert.ToDouble(dr["HOUSE_MAINT"]);
                    empSalary.MED_ALLOW = Convert.ToDouble(dr["MED_ALLOW"]);
                    empSalary.CONV_ALLOW = Convert.ToDouble(dr["CONV_ALLOW"]);
                    empSalary.PF_PN_COR = Convert.ToDouble(dr["PF_PN_COR"].ToString());
                    empSalary.PENSION_CONTR_COR = Convert.ToDouble(dr["PENSION_CONTR_COR"].ToString());
                    empSalary.PF_CONTR_COR = Convert.ToDouble(dr["PF_CONTR_COR"].ToString());
                    empSalary.PF_CONTR_OWN = Convert.ToDouble(dr["PF_CONTR_OWN"]);
                    empSalary.PF_LOAN = Convert.ToDouble(dr["PF_LOAN"]);
                    empSalary.CAR_MOTOR_LOAN = Convert.ToDouble(dr["CAR_MOTOR_LOAN"]);
                    empSalary.ASSOC_LOAN = Convert.ToDouble(dr["ASSOC_LOAN"]);
                    empSalary.OFF_ASS_SUB = Convert.ToDouble(dr["OFF_ASS_SUB"]);
                    empSalary.STAFF_UNION_SUB = Convert.ToDouble(dr["STAFF_UNION_SUB"]);
                    empSalary.STAFF_BUS = Convert.ToDouble(dr["STAFF_BUS"]);
                    empSalary.ENTERTAINMENT = Convert.ToDouble(dr["ENTERTAINMENT"]);
                    empSalary.EMP_BEN_FUND = Convert.ToDouble(dr["EMP_BEN_FUND"]);
                    empSalary.WASH_ALLOWS = Convert.ToDouble(dr["WASH_ALLOWS"]);
                    empSalary.DEDUCT = Convert.ToDouble(dr["DEDUCT"]);
                    empSalary.P_PAY = Convert.ToDouble(dr["P_PAY"]);
                    empSalary.UTILITIES = Convert.ToDouble(dr["UTILITIES"]);
                    empSalary.NEWSPAPER = Convert.ToDouble(dr["NEWSPAPER"]);
                    empSalary.REV_STAMP = Convert.ToDouble(dr["REV_STAMP"]);
                    empSalary.TELEPHONE = Convert.ToDouble(dr["TELEPHONE"]);
                    empSalary.INCOME_TAX = Convert.ToDouble(dr["INCOME_TAX"]);
                    empSalary.PREVIOUS_INCOME_TAX = Convert.ToDouble(dr["PREVIOUS_INCOME_TAX"]);
                    empSalary.GI_PREM = Convert.ToDouble(dr["GI_PREM"]);
                    empSalary.OTHERS = Convert.ToDouble(dr["OTHERS"]);
                    empSalary.TEMP_ADVANCE = Convert.ToDouble(dr["TEMP_ADVANCE"]);
                    empSalary.DISBURSED_ID = Convert.ToInt32(dr["DISBURSED_ID"]);
                    empSalary.ADVANCE_INIT_ID = Convert.ToInt32(dr["ADVANCE_INIT_ID"]);
                    empSalary.ADVANCE_SCHEDULE_ID = Convert.ToInt32(dr["ADVANCE_SCHEDULE_ID"]);
                    empSalary.DEP_ALLOW = Convert.ToDouble(dr["DEP_ALLOW"]);
                    empSalary.CHILD_EDU_ALLOW = Convert.ToDouble(dr["CHILD_EDU_ALLOW"]);
                    empSalary.AREA = Convert.ToDouble(dr["AREA"]);
                    empSalary.TOTAL_DEDUCT = Convert.ToDouble(dr["TOTAL_DEDUCT"]);
                    empSalary.GROSS_TOTAL = Convert.ToDouble(dr["GROSS_TOTAL"]);
                    empSalary.GROSS_TOTAL_WITH_ALLOW = Convert.ToDouble(dr["GROSS_TOTAL_WITH_ALLOW"]);
                    empSalary.NET_PAYABLE = Convert.ToDouble(dr["NET_PAYABLE"]);
                    empSalary.NET_PAYABLE_WITH_ALLOW = Convert.ToDouble(dr["NET_PAYABLE_WITH_ALLOW"]);
                    empSalary.NET_ALLOWANCE = Convert.ToDouble(dr["NET_ALLOWANCE"]);
                    empSalary.CAL_DATE = Convert.ToDateTime(dr["CAL_DATE"].ToString()); 
                    empSalary_LIST.Add(empSalary);


                }

                foreach (SalaryDetails saldet in empSalary_LIST)
                {
                    msg = spmg.SaveSalary(saldet);
                }

               
            }

           return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}