using AMCLBL;
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
    public class LoansSheduleController: Controller
    {
        LoanManagment lmg = new LoanManagment();
        // GET: LoansShedule
        public ActionResult Index()
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.ADVANCE_INIT_DET_ACTIVATE == "Y" & itm.LOAN_STATUS == "Active" & itm.SHEDULE_GENARATE == null);
            ViewBag.advINTDet = advanceInitializationDetails_LIST;
            return View();
        }

        public ActionResult LoanBalance()
        {
            
            return View();
        }

        public ActionResult LoanRecovery()
        {

            return View();
        }

        public ActionResult GetDisbursedAndPrincipalBalance()
        {

            return View();
        }


        public ActionResult GeneratedSheduleReport()
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.SHEDULE_GENARATE == "Y");
            ViewBag.advINTDet = advanceInitializationDetails_LIST;
            return View();
        }
        public ActionResult PaymentStatusReport()
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.SHEDULE_GENARATE == "Y");
            ViewBag.advINTDet = advanceInitializationDetails_LIST;
            return View();
        }

        public JsonResult GetAdvIntializationDetailstById(int Id)
        {
            Advance_initialization_details model = lmg.AdvanceInitializationDetails_LIST.Where(x => x.DISBURSED_ID == Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAdvIntializationDetails(String Id)
        {
            List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.LOAN_ID == Id);

            return Json(advanceInitializationDetails_LIST, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerateShedule(string Id)
        {
            Advance_initialization_details advanceInitializationDetails = lmg.AdvanceInitializationDetails_LIST.Find(itm => itm.LOAN_ID == Id);


            DataTable dtadvShedule = lmg.GenarateAdvanceShedule(advanceInitializationDetails);

            List<Advance_schedule> advanceShedule_LIST = new List<Advance_schedule>();

            foreach (DataRow dr in dtadvShedule.Rows)
            {
                Advance_schedule advShe = new Advance_schedule();
                advShe.ADVANCE_SCHEDULE_ID = Convert.ToInt32(dr["ADVANCE_SCHEDULE_ID"]);
                advShe.SCHEDULE_DATE = dr["SCHEDULE_DATE"].ToString();
                advShe.INSTALLMENT_AMT = Convert.ToDouble(dr["INSTALLMENT_AMT"]);
                advShe.INTEREST_RATE = Convert.ToDouble(dr["INTEREST_RATE"]); 
                advShe.INTEREST_PAID = Convert.ToDouble(dr["INTEREST_PAID"]);
                advShe.CUMULATIVE_INTEREST = Convert.ToDouble(dr["CUMULATIVE_INTEREST"]);
                advShe.PRINCIPAL_PAID = Convert.ToDouble(dr["PRINCIPAL_PAID"]);
                advShe.TYPE = dr["TYPE"].ToString();

                if (Convert.ToDouble(dr["INTEREST_PAID"]) == 0)
                {
                    advShe.BALANCE = 0;
                }
                else
                {
                    advShe.BALANCE = Convert.ToDouble(dr["BALANCE"]);
                }
                advShe.BALANCEWITHINTEREST = Convert.ToDouble(dr["BALANCE"]) + Convert.ToDouble(dr["CUMULATIVE_INTEREST"]);
                advShe.ADVANCE_INIT_DET_ID = advanceInitializationDetails.DISBURSED_ID;
                advShe.ADVANCE_INIT__ID = advanceInitializationDetails.ADVANCE_INIT_ID;
                advanceShedule_LIST.Add(advShe);
            }

            return Json(advanceShedule_LIST, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveAdvanceShedule(string Id)
        {
            Advance_initialization_details advanceInitializationDetails = lmg.AdvanceInitializationDetails_LIST.Find(itm => itm.LOAN_ID == Id);
            string mess = "";

            DataTable dtadvSheduleGenarated = lmg.GenarateAdvanceShedule(advanceInitializationDetails);

            DataTable dtShedule = lmg.Get_Shedule_ID(Id);

            if (dtShedule.Rows.Count > 0)
            {
                mess = "Shedule Already Genarated";
            }
            else
            {
                DataTable dtadvShedule = lmg.GenarateAdvanceShedule(advanceInitializationDetails);

                List<Advance_schedule> advanceShedule_LIST = new List<Advance_schedule>();

                foreach (DataRow dr in dtadvShedule.Rows)
                {
                    Advance_schedule advShe = new Advance_schedule();
                    advShe.ADVANCE_SCHEDULE_ID = Convert.ToInt32(dr["ADVANCE_SCHEDULE_ID"]);
                    advShe.SCHEDULE_DATE = dr["SCHEDULE_DATE"].ToString();
                    advShe.INSTALLMENT_AMT = Convert.ToDouble(dr["INSTALLMENT_AMT"]);
                    advShe.INTEREST_RATE = Convert.ToDouble(dr["INTEREST_RATE"].ToString());
                    advShe.INTEREST_PAID = Convert.ToDouble(dr["INTEREST_PAID"]);
                    advShe.CUMULATIVE_INTEREST = Convert.ToDouble(dr["CUMULATIVE_INTEREST"]);
                    advShe.PRINCIPAL_PAID = Convert.ToDouble(dr["PRINCIPAL_PAID"]);
                    if (Convert.ToDouble(dr["INTEREST_PAID"]) == 0)
                    {
                        advShe.BALANCE = 0;
                    }
                    else
                    {
                        advShe.BALANCE = Convert.ToDouble(dr["BALANCE"]);
                    }
                   
                    advShe.TYPE = dr["TYPE"].ToString();
                    advShe.ADVANCE_INIT_DET_ID = advanceInitializationDetails.DISBURSED_ID;
                    advShe.ADVANCE_INIT__ID = advanceInitializationDetails.ADVANCE_INIT_ID;
                    advanceShedule_LIST.Add(advShe);
                }
                foreach (Advance_schedule SheduleAdvance in advanceShedule_LIST)
                {
                    mess = lmg.SaveSheduleAdvance(SheduleAdvance);
                }

                DataTable dtTotalIntAndPrincipal = lmg.Get_TotalInterestAndPrincipal(advanceInitializationDetails.ADVANCE_INIT_ID, advanceInitializationDetails.DISBURSED_ID);
                Decimal INTEREST_PAID = 0;
                Decimal PRINCIPAL_PAID = 0;
                if (dtTotalIntAndPrincipal.Rows.Count > 0)
                {
                    INTEREST_PAID = Convert.ToDecimal(dtTotalIntAndPrincipal.Rows[0]["INTEREST_PAID"]);
                    advanceInitializationDetails.TOTAL_INTEREST_AMT = Convert.ToDouble(INTEREST_PAID);
                    PRINCIPAL_PAID = Convert.ToDecimal(dtTotalIntAndPrincipal.Rows[0]["PRINCIPAL_PAID"]);
                    advanceInitializationDetails.TOTAL_PRINCIPAL_AMT = Convert.ToDouble(PRINCIPAL_PAID);
                    advanceInitializationDetails.SCHEDULE_GENARATE_DATE = DateTime.Now;
                    advanceInitializationDetails.SHEDULE_GENARATE = "Y";


                }

                mess = lmg.UpdateInitializationDetails(advanceInitializationDetails);

            }

            return Json(mess, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult ExportAdvanceShedule(string LOAN_ID)
        {
           DataSet ds = new DataSet();

            DataTable dtGetDisburserID = lmg.Get_DISBURSED_ID(LOAN_ID);
            DataTable dt_INTEREST_RATE = lmg.Get_INTEREST_RATE_DISBURSED_ID(dtGetDisburserID.Rows[0]["DISBURSED_ID"].ToString());
            string INTEREST_RATE = "";
            if (dt_INTEREST_RATE.Rows.Count > 0)
            {
                for (int i = 0; i < dt_INTEREST_RATE.Rows.Count; i++)
                {
                    INTEREST_RATE = INTEREST_RATE +","+dt_INTEREST_RATE.Rows[i]["INTEREST_RATE"].ToString();
                }
                    
            }
           
            DataTable dtSheduleWithEmpInfo = lmg.Get_AllinfoByDISBURSED_ID(dtGetDisburserID.Rows[0]["DISBURSED_ID"].ToString());
            DataTable dtshedule = lmg.GetShedule(dtGetDisburserID.Rows[0]["DISBURSED_ID"].ToString());
            dtSheduleWithEmpInfo.TableName = "dtCopyForMainRpt";
            dtshedule.TableName = "dtCopyForSubRpt";
            ds.Tables.Add(dtSheduleWithEmpInfo.Copy());
            ds.Tables.Add(dtshedule.Copy());


          //  ds.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\dtAdvanceShedulewithInt_final.xsd");

            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "crtAdvanceShedule.rpt"));

            rd.SetDataSource(ds);

            rd.SetParameterValue("@INTEREST_RATE", INTEREST_RATE.TrimStart(','));

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "AdvanceShedule.pdf");
        }
        [HttpPost]
        public ActionResult ExportPaymentStatus(string LOAN_ID, string DISBURSED_DATE)
        {

            string AsonDate= Convert.ToDateTime(DISBURSED_DATE).ToString("dd-MMM-yyyy");
            DataTable dtAdvancIntDetails = lmg.Get_DISBURSED_ID(LOAN_ID);
            DataTable dtSheduleWithEmpInfo = lmg.Get_AllinfoByDISBURSED_ID(dtAdvancIntDetails.Rows[0]["DISBURSED_ID"].ToString());
            DataTable dtpaymentStaus = lmg.GetPaymentStaus(dtAdvancIntDetails.Rows[0]["DISBURSED_ID"].ToString(), AsonDate);
            dtpaymentStaus.TableName = "dtPaymentStatus";
            string ADVANCE_ID = "";
            string EmpName = "";
            string DESIG = "";
            string BDATE = "";
            string DISBURSED_AMT = "";
            string INT_RATE = "";
            string TIMES_OF_LOAN = "";
            string EMPLOYEE_ID = "";
            string LOAN_STATUS = "";
            string EMPLOYEE_JOIN_DATE = "";
            string EMPLOYEE_RETIRE_DATE = "";
            string DISBURSED_DT = "";
            string NO_OF_INSTALLMENT = "";
            if (dtSheduleWithEmpInfo.Rows.Count > 0)
            {
                ADVANCE_ID = dtSheduleWithEmpInfo.Rows[0]["ADVANCE_ID"].ToString();
                EmpName = dtSheduleWithEmpInfo.Rows[0]["NAME"].ToString();
                DESIG = dtSheduleWithEmpInfo.Rows[0]["DESIG"].ToString();
                BDATE = dtSheduleWithEmpInfo.Rows[0]["BDATE"].ToString(); 
                DISBURSED_AMT = dtSheduleWithEmpInfo.Rows[0]["DISBURSED_AMT"].ToString(); 
                INT_RATE = dtSheduleWithEmpInfo.Rows[0]["INT_RATE"].ToString();
                TIMES_OF_LOAN = dtSheduleWithEmpInfo.Rows[0]["TIMES_OF_LOAN"].ToString();
                EMPLOYEE_ID = dtSheduleWithEmpInfo.Rows[0]["EMPLOYEE_ID"].ToString();
                LOAN_STATUS= dtSheduleWithEmpInfo.Rows[0]["LOAN_STATUS"].ToString();
                EMPLOYEE_JOIN_DATE = dtSheduleWithEmpInfo.Rows[0]["EMPLOYEE_JOIN_DATE"].ToString();
                EMPLOYEE_RETIRE_DATE = dtSheduleWithEmpInfo.Rows[0]["EMPLOYEE_RETIRE_DATE"].ToString();
                DISBURSED_DT = dtSheduleWithEmpInfo.Rows[0]["DISBURSED_DATE"].ToString(); 
                NO_OF_INSTALLMENT = dtSheduleWithEmpInfo.Rows[0]["NO_OF_INSTALLMENT"].ToString();
            }

           // dtpaymentStaus.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\dtPaymentStatus.xsd");


            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "crtpaymentStatus.rpt"));

            rd.SetDataSource(dtpaymentStaus);

            rd.SetParameterValue("@DisburshDate", Convert.ToDateTime(DISBURSED_DATE).ToShortDateString());
            rd.SetParameterValue("@ADVANCE_ID", ADVANCE_ID);
            rd.SetParameterValue("@EmpName", EmpName);
            rd.SetParameterValue("@DESIG", DESIG);
            rd.SetParameterValue("@BDATE", BDATE);
            rd.SetParameterValue("@DISBURSED_AMT", DISBURSED_AMT);
            rd.SetParameterValue("@INT_RATE", INT_RATE);
            rd.SetParameterValue("@LOAN_STATUS", LOAN_STATUS);
            rd.SetParameterValue("@TIMES_OF_LOAN", TIMES_OF_LOAN);
            rd.SetParameterValue("@EMPLOYEE_ID", EMPLOYEE_ID);
            rd.SetParameterValue("@EMPLOYEE_JOIN_DATE", Convert.ToDateTime(EMPLOYEE_JOIN_DATE).ToShortDateString());
            rd.SetParameterValue("@EMPLOYEE_RETIRE_DATE", Convert.ToDateTime(EMPLOYEE_RETIRE_DATE).ToShortDateString());
            rd.SetParameterValue("@DISBURSED_DT", Convert.ToDateTime(DISBURSED_DT).ToShortDateString());
            rd.SetParameterValue("@NO_OF_INSTALLMENT", NO_OF_INSTALLMENT);
       

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "PaymentStatus.pdf");
        }

        [HttpPost]
        public ActionResult ExportLoanBalance(string DISBURSED_DATE)
        {
            string AsonDate = Convert.ToDateTime(DISBURSED_DATE).ToString("dd-MMM-yyyy");
            DataTable dtLoanStaus = lmg.GetLoanBalance(AsonDate);



            DataTable dtLoanbalanceFinal = new DataTable();
            dtLoanbalanceFinal.Columns.Add("DISBURSED_ID", typeof(int));
            dtLoanbalanceFinal.Columns.Add("DISBURSED_AMT", typeof(double));
            dtLoanbalanceFinal.Columns.Add("DISBURSED_DATE", typeof(string));
            dtLoanbalanceFinal.Columns.Add("LOAN_STATUS", typeof(string));
            dtLoanbalanceFinal.Columns.Add("CLOSE_DATE", typeof(string));
            dtLoanbalanceFinal.Columns.Add("TOTAL_INTEREST_AMT", typeof(double));
            dtLoanbalanceFinal.Columns.Add("NAME", typeof(string));
            dtLoanbalanceFinal.Columns.Add("DESIGNATION", typeof(string));
            dtLoanbalanceFinal.Columns.Add("PRINCIPAL", typeof(double));
            dtLoanbalanceFinal.Columns.Add("INTEREST", typeof(double));

            DataRow drLoanbalanceFinal;

            if (dtLoanStaus.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtLoanStaus.Rows.Count; loop++)
                {
                    double DISBURSED_AMT = Convert.ToDouble(dtLoanStaus.Rows[loop]["DISBURSED_AMT"].ToString());
                    double INTEREST = Convert.ToDouble(dtLoanStaus.Rows[loop]["INTEREST"].ToString());
                    double PRINCIPAL = Convert.ToDouble(dtLoanStaus.Rows[loop]["PRINCIPAL"].ToString());
                    DateTime Closedate;

                    if (dtLoanStaus.Rows[loop]["CLOSE_DATE"] == DBNull.Value)
                    {
                        //Do something
                        Closedate = Convert.ToDateTime("01-Jan-2099");

                    }
                    else
                    {
                        //Do something
                        Closedate = Convert.ToDateTime(dtLoanStaus.Rows[loop]["CLOSE_DATE"].ToString());
                    }

                    drLoanbalanceFinal = dtLoanbalanceFinal.NewRow();
                    if (Convert.ToDateTime(DISBURSED_DATE) <= Closedate)
                    {

                        if (PRINCIPAL != DISBURSED_AMT + INTEREST)
                        {
                           

                            drLoanbalanceFinal["DISBURSED_ID"] = dtLoanStaus.Rows[loop]["DISBURSED_ID"].ToString();
                            drLoanbalanceFinal["DISBURSED_DATE"] = Convert.ToDateTime(dtLoanStaus.Rows[loop]["DISBURSED_DATE"]).ToString("dd/MMM/yyyy");
                            drLoanbalanceFinal["LOAN_STATUS"] = dtLoanStaus.Rows[loop]["LOAN_STATUS"].ToString();
                            drLoanbalanceFinal["DISBURSED_AMT"] = dtLoanStaus.Rows[loop]["DISBURSED_AMT"].ToString();
                            drLoanbalanceFinal["TOTAL_INTEREST_AMT"] = dtLoanStaus.Rows[loop]["TOTAL_INTEREST_AMT"].ToString();
                            drLoanbalanceFinal["NAME"] = dtLoanStaus.Rows[loop]["NAME"].ToString();
                            drLoanbalanceFinal["DESIGNATION"] = dtLoanStaus.Rows[loop]["DESIGNATION"].ToString();


                            if (DISBURSED_AMT < PRINCIPAL)
                            {
                                double amount = PRINCIPAL - DISBURSED_AMT;
                                double interest_amt = INTEREST - amount;
                                drLoanbalanceFinal["PRINCIPAL"] = 0;
                                drLoanbalanceFinal["INTEREST"] = interest_amt;
                            }
                            else
                            {
                                drLoanbalanceFinal["PRINCIPAL"] = DISBURSED_AMT - PRINCIPAL;
                                drLoanbalanceFinal["INTEREST"] = dtLoanStaus.Rows[loop]["INTEREST"].ToString();

                            }



                        }
                        else
                        {
                            drLoanbalanceFinal["DISBURSED_ID"] = dtLoanStaus.Rows[loop]["DISBURSED_ID"].ToString();
                            drLoanbalanceFinal["DISBURSED_DATE"] = Convert.ToDateTime(dtLoanStaus.Rows[loop]["DISBURSED_DATE"]).ToString("dd/MMM/yyyy");
                            drLoanbalanceFinal["LOAN_STATUS"] = dtLoanStaus.Rows[loop]["LOAN_STATUS"].ToString();
                            drLoanbalanceFinal["DISBURSED_AMT"] = dtLoanStaus.Rows[loop]["DISBURSED_AMT"].ToString();
                            drLoanbalanceFinal["TOTAL_INTEREST_AMT"] = dtLoanStaus.Rows[loop]["TOTAL_INTEREST_AMT"].ToString();
                            drLoanbalanceFinal["NAME"] = dtLoanStaus.Rows[loop]["NAME"].ToString();
                            drLoanbalanceFinal["DESIGNATION"] = dtLoanStaus.Rows[loop]["DESIGNATION"].ToString();
                            if (Convert.ToDouble(dtLoanStaus.Rows[loop]["INTEREST"].ToString()) == Convert.ToDouble(dtLoanStaus.Rows[loop]["TOTAL_INTEREST_AMT"].ToString()))
                            {
                                drLoanbalanceFinal["PRINCIPAL"] = 0;
                                drLoanbalanceFinal["INTEREST"] = 0;

                            }
                            else
                            {
                                drLoanbalanceFinal["PRINCIPAL"] = dtLoanStaus.Rows[loop]["PRINCIPAL"].ToString();
                                drLoanbalanceFinal["INTEREST"] = dtLoanStaus.Rows[loop]["INTEREST"].ToString();
                            }
                            
                           


                        }
                        dtLoanbalanceFinal.Rows.Add(drLoanbalanceFinal);

                    }
                   

                }

           
            }

            dtLoanbalanceFinal.TableName = "LoanbalanceFinal";
         //   dtLoanbalanceFinal.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\dtLoanbalanceFinalxsd.xsd");
            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "crtLoanbalance.rpt"));
            rd.SetDataSource(dtLoanbalanceFinal);
            rd.SetParameterValue("@AsonDate", AsonDate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Loanbalance.pdf");

        }

        [HttpPost]
        public ActionResult ExportRecoveryStatus(string DISBURSED_DATE_FORM, string DISBURSED_DATE_TO)
        {
            string FORM_DATE = Convert.ToDateTime(DISBURSED_DATE_FORM).ToString("dd-MMM-yyyy");
            string TO_DATE = Convert.ToDateTime(DISBURSED_DATE_TO).ToString("dd-MMM-yyyy");
            DataTable dtDisburshedId = lmg.GetDisburshedId(FORM_DATE, TO_DATE);


         


            DataTable dtLoanRecoveryFinal = new DataTable();
            dtLoanRecoveryFinal.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtLoanRecoveryFinal.Columns.Add("DESIGNATION", typeof(string));
            dtLoanRecoveryFinal.Columns.Add("PRINCIPAL", typeof(double));
            dtLoanRecoveryFinal.Columns.Add("INTEREST", typeof(double));
          

          

            DataRow drLoanbalanceFinal;
            if (dtDisburshedId.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtDisburshedId.Rows.Count; loop++)
                {
                    drLoanbalanceFinal = dtLoanRecoveryFinal.NewRow();

                    DateTime Closedate;

                    if (dtDisburshedId.Rows[loop]["CLOSE_DATE"] == DBNull.Value)
                    {
                        //Do something
                        Closedate = Convert.ToDateTime("01-Jan-2099");

                    }
                    else
                    {
                        //Do something
                        Closedate = Convert.ToDateTime(dtDisburshedId.Rows[loop]["CLOSE_DATE"].ToString());
                    }




                    DataTable dtShedule = lmg.GetSheduleBYDisburshedIdandDate(dtDisburshedId.Rows[loop]["ADVANCE_INIT_DET_ID"].ToString(),FORM_DATE,TO_DATE);
                    if (dtShedule.Rows.Count > 0)
                    {
                        drLoanbalanceFinal["EMPLOYEE_NAME"] = dtDisburshedId.Rows[loop]["NAME"].ToString();
                        drLoanbalanceFinal["DESIGNATION"] = dtDisburshedId.Rows[loop]["DESIGNATION"].ToString();
                        Double total_principal = 0;
                        Double total_interest = 0;
                        for (int i = 0; i < dtShedule.Rows.Count; i++)
                        {
                            DateTime SheduleDate = Convert.ToDateTime(dtShedule.Rows[i]["SCHEDULE_DATE"].ToString());


                            if (Convert.ToDateTime(SheduleDate) <= Closedate)
                            {
                                if (dtShedule.Rows[i]["TYPE"].ToString() == "P")
                                {

                                    if (Convert.ToDouble(dtShedule.Rows[i]["INSTALLMENT_AMT"].ToString()) > Convert.ToDouble(dtShedule.Rows[i]["BALANCE"].ToString()))
                                    {
                                        if (Convert.ToDouble(dtShedule.Rows[i]["BALANCE"].ToString()) == 0)
                                        {
                                            total_principal = Convert.ToDouble(dtShedule.Rows[i]["PRINCIPAL_PAID"].ToString()) + Convert.ToDouble(dtShedule.Rows[i]["INTEREST_PAID"].ToString());
                                        }
                                        else
                                        {
                                            total_principal = total_principal + Convert.ToDouble(dtShedule.Rows[i]["PRINCIPAL_PAID"].ToString());
                                            total_interest = total_interest + Convert.ToDouble(dtShedule.Rows[i]["INTEREST_PAID"].ToString());
                                        }
                                       
                                    }
                                    else
                                    {
                                        total_principal = total_principal + Convert.ToDouble(dtShedule.Rows[i]["INSTALLMENT_AMT"].ToString());
                                    }


                                }
                                else if (dtShedule.Rows[i]["TYPE"].ToString() == "I")
                                {
                                    total_interest = total_interest + Convert.ToDouble(dtShedule.Rows[i]["INSTALLMENT_AMT"].ToString());
                                }
                            }

                           
                            drLoanbalanceFinal["PRINCIPAL"] = total_principal;
                            drLoanbalanceFinal["INTEREST"] = total_interest;
                        }
                       
                    }

                    dtLoanRecoveryFinal.Rows.Add(drLoanbalanceFinal);
                }
            }

            dtLoanRecoveryFinal.TableName = "LoanRecoveryFinal";
         //   dtLoanRecoveryFinal.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\dtLoanRecoveryFinalxsd.xsd");

            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "crtLoanRecoveryFinal.rpt"));
            rd.SetDataSource(dtLoanRecoveryFinal);
            rd.SetParameterValue("@FormDate", FORM_DATE);
            rd.SetParameterValue("@ToDate", TO_DATE);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "LoanRecoveryFinal.pdf");



           
        }



        [HttpPost]
        public ActionResult ExportGetDisbursedAndInterestBalance(string DISBURSED_DATE_FORM,string DISBURSED_DATE_TO, string LOAN_STATUS)
        {
            string FORM_DATE = Convert.ToDateTime(DISBURSED_DATE_FORM).ToString("dd-MMM-yyyy");
            string TO_DATE = Convert.ToDateTime(DISBURSED_DATE_TO).ToString("dd-MMM-yyyy");

            DataTable dtLoanBalanceFinal = lmg.GetDisbursedAndInterestBalance(FORM_DATE, TO_DATE, LOAN_STATUS);


            DataTable dtLoanBalance = new DataTable();
            dtLoanBalance.Columns.Add("DISBURSED_ID", typeof(int));
            dtLoanBalance.Columns.Add("NAME", typeof(string));
            dtLoanBalance.Columns.Add("DESIG_ID", typeof(string));
            dtLoanBalance.Columns.Add("DISBURSED_DATE", typeof(string));
            dtLoanBalance.Columns.Add("CLOSE_DATE", typeof(string));
            dtLoanBalance.Columns.Add("TotalInstallment", typeof(double));
            dtLoanBalance.Columns.Add("DESIGNATION", typeof(string));

            DataRow drInterestFinal;
            if (dtLoanBalanceFinal.Rows.Count > 0)
            {
                for (int loop = 0; loop < dtLoanBalanceFinal.Rows.Count; loop++)
                {
                    drInterestFinal = dtLoanBalance.NewRow();

                    DateTime Closedate;

                    if (dtLoanBalanceFinal.Rows[loop]["CLOSE_DATE"] == DBNull.Value)
                    {
                        //Do something
                        Closedate = Convert.ToDateTime("01-Jan-2099");

                    }
                    else
                    {
                        //Do something
                        Closedate = Convert.ToDateTime(dtLoanBalanceFinal.Rows[loop]["CLOSE_DATE"].ToString());
                    }

                    DateTime SheduleDate = Convert.ToDateTime(dtLoanBalanceFinal.Rows[loop]["SCHEDULE_DATE"].ToString());

                    if (Convert.ToDateTime(SheduleDate) <= Closedate)
                    {
                        drInterestFinal["DISBURSED_ID"] = dtLoanBalanceFinal.Rows[loop]["DISBURSED_ID"].ToString();
                        drInterestFinal["NAME"] = dtLoanBalanceFinal.Rows[loop]["NAME"].ToString();
                        drInterestFinal["DESIG_ID"] = dtLoanBalanceFinal.Rows[loop]["DESIG_ID"].ToString();
                        drInterestFinal["DISBURSED_DATE"] = Convert.ToDateTime(dtLoanBalanceFinal.Rows[loop]["DISBURSED_DATE"]).ToString("dd-MMM-yyyy");
                        drInterestFinal["TotalInstallment"] = dtLoanBalanceFinal.Rows[loop]["TotalInstallment"].ToString();

                        dtLoanBalance.Rows.Add(drInterestFinal);


                    }


                }

            }



            string text = "";
            if (LOAN_STATUS == "I")
            {
                text = "Interest";
            }
            else if (LOAN_STATUS == "P")
            {
                text = "Loan Disbursed";
            }

            dtLoanBalance.TableName = "DisbursedAndInterestBalance";
           // dtLoanBalance.WriteXmlSchema(@"D:\Development\LoanManagementSystem\LoanManagementSystem\Reports\dtDisbursedAndInterestBalancexsd.xsd");

            ReportDocument rd = new ReportDocument();
            rd.Load(System.IO.Path.Combine(Server.MapPath("~/Reports"), "crtDisbursedAndInterestBalance.rpt"));
            rd.SetDataSource(dtLoanBalance);
            rd.SetParameterValue("@FormDate", FORM_DATE);
            rd.SetParameterValue("@ToDate", TO_DATE);
            rd.SetParameterValue("@LOAN_STATUS", text);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "DisbursedAndInterestBalance.pdf");


        }


    }
}