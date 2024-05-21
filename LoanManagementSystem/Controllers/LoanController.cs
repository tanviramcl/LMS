using AMCLBL;
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
    public class LoanController : Controller
    {
        EmployeeManagment emplyeeManagment = new EmployeeManagment();
        LoanManagment lmg = new LoanManagment(); 
        // GET: Loan
        public ActionResult Index()
        {

            List<Models.Employee> empList = emplyeeManagment.Employee_LIST;
            ViewBag.EmpLst = empList;
            

            return View();
        }

        public ActionResult AdvanceInitializationDetails()
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST;
            ViewBag.advanceInitializationDetails_LIST = advanceInitializationDetails_LIST;

            List<AdvanceInitialization> AdvanceInitialization_list = lmg.AdvanceInitialization_active_LIST;

            ViewBag.advType = AdvanceInitialization_list;

            DataTable dtDISBURSED_ID = lmg.Get_ADV_INT_DISBURSED_ID();

            ViewBag.DISBURSED_ID = dtDISBURSED_ID.Rows[0]["DISBURSED_ID"].ToString();

            return View();
        }

        public JsonResult GetAdvIntialization(String Id)
        {

           DataTable  dtadvanceInitialization =lmg.Get_All_AdvanceInitializationByID(Id);
            DataTable dtDISBURSED_ID = lmg.Get_ADV_INT_DISBURSED_ID();
            string ID = dtDISBURSED_ID.Rows[0]["DISBURSED_ID"].ToString();
            List<AdvanceInitialization> AdvanceInitialization_list = new List<AdvanceInitialization>();
            foreach (DataRow dr in dtadvanceInitialization.Rows)
            {
                AdvanceInitialization advInt = new AdvanceInitialization();
                advInt.EMPLOYEE_ID = dr["EMPLOYEE_ID"].ToString();
                advInt.OFFICE_ID = dr["OFFICE_ID"].ToString()+"-"+ ID;

                AdvanceInitialization_list.Add(advInt);
            }
            return Json(AdvanceInitialization_list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEarlySatlementInfo(String Id)
        {

            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.LOAN_ID == Id);


            return Json(advanceInitializationDetails_LIST, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Get_EMPLOYEE_OFFICE_ID(String Id)
        {

            DataTable dtEmpInfo = lmg.Get_All_EMPLOYEE_INFOMATION_ByID(Id);

            List<Employee> EmpInfo_list = new List<Employee>();
            foreach (DataRow dr in dtEmpInfo.Rows)
            {
                Employee emp = new Employee();
                emp.EMP_ID = dr["EMP_ID"].ToString();
                EmpInfo_list.Add(emp);
            }
            return Json(EmpInfo_list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdvanceInitialization()
        {

            List<AdvanceInitialization> AdvanceInitialization_list = lmg.AdvanceInitialization_LIST;
            List<Models.Employee> empList = emplyeeManagment.Employee_LIST;
            List<AdvanceType> advType = lmg.AdvanceType_LIST;


            ViewBag.AdvanceInitialization_list = AdvanceInitialization_list;
            ViewBag.EmpLst = empList;
            ViewBag.advType = advType;
            
            return View();
        }

        public ActionResult CloseAdvance()
        {
            return View();
        }
        public ActionResult EarlySatlementList()
        {

            List<EarlySatlement> earlySatlementListn_list = lmg.earlySatlement_LIST;

            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.SHEDULE_GENARATE == "Y");

            ViewBag.earlySatlementListn_list = earlySatlementListn_list;

            ViewBag.advINTDet = advanceInitializationDetails_LIST;


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddAdvanceInitialization(String AvdInt)
         {
            //AdvanceInitialization jsul = new JavaScriptSerializer().Deserialize<AdvanceInitialization>(AvdInt);
            List<AdvanceInitialization> jsul = new JavaScriptSerializer().Deserialize<List<AdvanceInitialization>>(AvdInt);
            int ID = lmg.Get_ADV_INT_ID();
          

            foreach (AdvanceInitialization adbInt in jsul)
            {
                adbInt.ADVANCE_INIT_ID = ID;
                adbInt.ADVANVE_ACTIVATE = "Y";
                adbInt.AVD_INT_DATE =DateTime.Now;
            }

            string mess = lmg.Initialization(jsul);


            return Json(mess, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult Test(String AvdInt)
        {
         


            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddAdvanceInitializationDetails(String AvdIntDetails)
        {
            //AdvanceInitialization jsul = new JavaScriptSerializer().Deserialize<AdvanceInitialization>(AvdInt);
            List<Advance_initialization_details> jsul = new JavaScriptSerializer().Deserialize<List<Advance_initialization_details>>(AvdIntDetails);
            string mess = "";
            foreach (Advance_initialization_details adbIntDet in jsul)
            {
                int DISBURSED_ID = adbIntDet.DISBURSED_ID;
                string ID = "";
                int ADVANCE_INIT_ID = adbIntDet.ADVANCE_INIT_ID;
                string DISBURSED_DATE = Convert.ToDateTime(adbIntDet.DISBURSED_DATE).ToString("dd/MM/yyyy");
                DataTable dtAdvanceIntDetails = lmg.Get_DISBURSED_With_DATE(ADVANCE_INIT_ID, DISBURSED_DATE);


                
                if (dtAdvanceIntDetails?.Rows?.Count == 0)
                {
                    if (DISBURSED_ID == 0)
                    {
                        DataTable dtDISBURSED_ID = lmg.Get_ADV_INT_DISBURSED_ID();
                        ID = dtDISBURSED_ID.Rows[0]["DISBURSED_ID"].ToString();
                        adbIntDet.DISBURSED_ID = Convert.ToInt32(ID);
                        adbIntDet.ADVANCE_INIT_DET_ACTIVATE = "Y";
                        adbIntDet.ADVANVE_INIT_DET_DATE = DateTime.Now;
                        mess = lmg.Initialization_DISBURSE(jsul);
                    }
                    else
                    {
                        mess = lmg.UpdateAdvanceInitializationDetails(adbIntDet);
                    }

                }
                else
                {
                    mess = lmg.UpdateAdvanceInitializationDetails(adbIntDet);
                }

            }
            return Json(mess, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UpdateAdvanceInitializationDetails(String AvdIntDetails)
        {

            string mess = "";

            //AdvanceInitialization jsul = new JavaScriptSerializer().Deserialize<AdvanceInitialization>(AvdInt);
            List<Advance_initialization_details> jsul = new JavaScriptSerializer().Deserialize<List<Advance_initialization_details>>(AvdIntDetails);
           
            foreach (Advance_initialization_details adbIntDet in jsul)
            {

               

            }

          


            return Json(mess, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddEarlySatlement(String erlyStlDetails)
        {
            //AdvanceInitialization jsul = new JavaScriptSerializer().Deserialize<AdvanceInitialization>(AvdInt);
            List<EarlySatlement> jsul = new JavaScriptSerializer().Deserialize<List<EarlySatlement>>(erlyStlDetails);
            DataTable dtADVANCE_EARLY_SATALEMENT_ID = lmg.Get_ADVANCE_EARLY_SATALEMENT_ID();
            string ID = dtADVANCE_EARLY_SATALEMENT_ID.Rows[0]["ADVANCE_EARLY_SATALEMENT_ID"].ToString();

            foreach (EarlySatlement earlySatlement in jsul)
            {
                earlySatlement.ADVANCE_EARLY_SATALEMENT_ID = Convert.ToInt32(ID);
                
            }

            string mess = lmg.AddEarlySatlement(jsul);

            if (mess == "Success")
            {
                foreach (EarlySatlement earlySatlement in jsul)
                {
                    Advance_initialization_details advanceInitializationDetails = lmg.AdvanceInitializationDetails_LIST.Find(itm => itm.DISBURSED_ID == Convert.ToInt32(earlySatlement.DISBURSED_ID));
                    advanceInitializationDetails.ADVANCE_INIT_DET_ACTIVATE = "C";
                    advanceInitializationDetails.LOAN_STATUS = "CLOSE";
                    advanceInitializationDetails.CLOSE_TYPE = "EARLY SATLEMENT";
                    advanceInitializationDetails.CLOSE_DATE = earlySatlement.EARLY_SATALEMENT_DATE;
                    mess = lmg.UpdateEarlySatlement(advanceInitializationDetails);

                }
                

            }


            return Json(mess, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetEmployeeRecord(string ID)
        {
            List<Models.Employee> empList = emplyeeManagment.Employee_LIST;
            var employeeModel = new Models.Employee();
            var emp = empList.Where(e => e.ID == ID).FirstOrDefault();
            //Set default emp records 
            employeeModel.AUTOID = emp.AUTOID;
            employeeModel.ID = emp.ID;
            employeeModel.NAME = emp.NAME;
            employeeModel.BASIC = emp.BASIC;
            employeeModel.BKACNO = emp.BKACNO;
            employeeModel.TEMP_ADVANCE = emp.TEMP_ADVANCE;
            employeeModel.JDATE= emp.JDATE;
            employeeModel.BDATE = emp.BDATE;

            return PartialView("_EmployeePartial", employeeModel);
        }
    }
}