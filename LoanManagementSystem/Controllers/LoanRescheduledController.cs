using LoanManagementSystem.Loan.layer;
using LoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanManagementSystem.Controllers
{
    public class LoanRescheduledController : Controller
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        LoanManagment lmg = new LoanManagment();
        // GET: LoanRescheduled
        public ActionResult Index()
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.ADVANCE_INIT_DET_ACTIVATE == "Y" & itm.LOAN_STATUS == "Active" & itm.SHEDULE_GENARATE == "Y" & itm.RESCHEDULED == "Y" );
            ViewBag.advINTDet = advanceInitializationDetails_LIST;

            DataTable dt = lmg.Reshedule();


            return View();
        }
    }
}