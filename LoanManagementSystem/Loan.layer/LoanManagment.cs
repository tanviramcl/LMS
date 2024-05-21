using AMCLBL;
using LoanManagementSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace LoanManagementSystem.Loan.layer
{
    public class LoanManagment
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        public List<AdvanceInitialization> AdvanceInitialization_LIST { get; set; }
        public List<AdvanceType> AdvanceType_LIST { get; set; }
        public List<Advance_initialization_details> AdvanceInitializationDetails_LIST { get; set; }
        public List<AdvanceInitialization> AdvanceInitialization_active_LIST { get; set; }
        public List<EarlySatlement> earlySatlement_LIST { get; set; }


        public LoanManagment()
        {
            Get_All_AdvanceInitialization();
            Get_All_Advance_Type();
            Get_All_AdvanceInitialization_details();
            Get_All_AdvanceInitializationWithActive();
            Get_All_EARLY_SATALEMENT();


        }

        public void Get_All_AdvanceInitialization()
        {
            string Query = " Select ADVANCE_INIT_ID, ADVANVE_INIT_NAME, EMPLOYEE_JOIN_DATE, EMPLOYEE_RETIRE_DATE, EMPLOYEE_BASIC, BANK_ACCOUNT, ADVANVE_ACTIVATE, ADVANCE_ID, EMPLOYEE_ID, AVD_INT_DATE,OFFICE_ID From ADVANCE_INITIALIZATION order by ADVANCE_INIT_ID DESC ";

            DataTable dt_avvance_int = this.commonGatewayObj.Select(Query);

            AdvanceInitialization_LIST = DataTableToList.DataTableToListConvert<Models.AdvanceInitialization>(dt_avvance_int);


        }

        public void Get_All_EARLY_SATALEMENT()
        {
            string Query = " SELECT * from ADVANCE_EARLY_SATALEMENT ";

            DataTable dt_earlySatalement= this.commonGatewayObj.Select(Query);

            earlySatlement_LIST = DataTableToList.DataTableToListConvert<Models.EarlySatlement>(dt_earlySatalement);


        }

        public void Get_All_AdvanceInitializationWithActive()
        {
            string Query = " Select ADVANCE_INIT_ID, ADVANVE_INIT_NAME, EMPLOYEE_JOIN_DATE, EMPLOYEE_RETIRE_DATE, EMPLOYEE_BASIC, BANK_ACCOUNT, ADVANVE_ACTIVATE, ADVANCE_ID, EMPLOYEE_ID, AVD_INT_DATE,OFFICE_ID From ADVANCE_INITIALIZATION where ADVANVE_ACTIVATE='Y' order by ADVANCE_INIT_ID DESC ";

            DataTable dt_avvance_int = this.commonGatewayObj.Select(Query);

            AdvanceInitialization_active_LIST = DataTableToList.DataTableToListConvert<Models.AdvanceInitialization>(dt_avvance_int);


        }



        public void Get_All_AdvanceInitializationDetailsWithActive()
        {
            string Query = " Select ADVANCE_INIT_ID, ADVANVE_INIT_NAME, EMPLOYEE_JOIN_DATE, EMPLOYEE_RETIRE_DATE, EMPLOYEE_BASIC, BANK_ACCOUNT, ADVANVE_ACTIVATE, ADVANCE_ID, EMPLOYEE_ID, AVD_INT_DATE,OFFICE_ID From ADVANCE_INITIALIZATION where ADVANVE_ACTIVATE='Y' order by ADVANCE_INIT_ID DESC ";

            DataTable dt_avvance_int = this.commonGatewayObj.Select(Query);

            AdvanceInitialization_active_LIST = DataTableToList.DataTableToListConvert<Models.AdvanceInitialization>(dt_avvance_int);


        }


        public DataTable Get_All_AdvanceInitializationByID(string ID)
        {
            string Query = " Select ADVANCE_INIT_ID, ADVANVE_INIT_NAME, EMPLOYEE_JOIN_DATE, EMPLOYEE_RETIRE_DATE, EMPLOYEE_BASIC, BANK_ACCOUNT, ADVANVE_ACTIVATE, ADVANCE_ID, EMPLOYEE_ID, AVD_INT_DATE,OFFICE_ID From ADVANCE_INITIALIZATION where ADVANCE_INIT_ID='" + ID+ "' order by ADVANCE_INIT_ID DESC ";

            DataTable dt_avvance_int = this.commonGatewayObj.Select(Query);

            return dt_avvance_int;
        }

        public DataTable Get_All_EMPLOYEE_INFOMATION_ByID(string ID)
        {
            string Query = " SELECT  * FROM EMP_INFO WHERE ID='"+ ID + "'";

            DataTable dt_Employee_info = this.commonGatewayObj.Select(Query);

            return dt_Employee_info;
        }

        public DataTable Get_Shedule(int ID,string CalDate)
        {
           
            DateTime caldate;
            try
            {

              caldate = DateTime.ParseExact(CalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
              //caldate = Convert.ToDateTime(CalDate);
            }
            catch (Exception exe)
            {
                throw new Exception(exe.ToString() + "calDate:" + CalDate);
            }
           

            var firstDayOfMonth = new DateTime(caldate.Year, caldate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            string Query = " SELECT ID, ADVANCE_SCHEDULE_ID, SCHEDULE_DATE,  INSTALLMENT_AMT, INTEREST_RATE, INTEREST_PAID, CUMULATIVE_INTEREST, PRINCIPAL_PAID, BALANCE,"+     
                            "ADVANCE_INIT_DET_ID, ADVANCE_INIT__ID, TYPE,  PAID FROM INVEST.ADVANCE_SCHEDULE Where ADVANCE_INIT_DET_ID = "+ID+ " AND SCHEDULE_DATE  between  TO_DATE('" + firstDayOfMonth.ToString("dd/MM/yyyy") + "', 'DD-MM-YYYY') and  TO_DATE('" + lastDayOfMonth.ToString("dd/MM/yyyy") + "', 'DD-MM-YYYY') ";

           
            DataTable dt_avvance_Shedule = this.commonGatewayObj.Select(Query);

            return dt_avvance_Shedule;
        }

        public void Get_All_AdvanceInitialization_details()
        {
            string Query = " select * from (select a.*,B.ADVANVE_INIT_NAME,B.AVD_INT_DATE,EMPLOYEE_JOIN_DATE, B.EMPLOYEE_RETIRE_DATE, b.EMPLOYEE_BASIC, b.BANK_ACCOUNT,b.ADVANCE_ID"+
   " from(SELECT DISBURSED_ID, DISBURSED_AMT, DISBURSED_DATE, INSTALLMENT_AMT, TOTAL_PRINCIPAL_AMT, TOTAL_INTEREST_AMT," +
   "  INT_RATE, INT_RATE_PREVIOUS, NO_OF_INSTALLMENT, NO_OF_YEAR, TIMES_OF_LOAN, LOAN_STATUS, ADVANCE_INIT_DET_ACTIVATE,"+
   " ADVANCE_INIT_ID, ADVANVE_INIT_DET_DATE, EMPLOYEE_ID,SCHEDULE_GENARATE_DATE, SHEDULE_GENARATE,RESCHEDULED,LOAN_ID,CLOSE_DATE,CLOSE_TYPE FROM ADVANCE_INITIALIZATION_DETAILS) a inner join ADVANCE_INITIALIZATION b" +
    " on A.ADVANCE_INIT_ID = B.ADVANCE_INIT_ID ) c inner join EMP_INFO d on c.EMPLOYEE_ID=d.ID order by DISBURSED_ID DESC    ";

            DataTable dt_avvance_int_details = this.commonGatewayObj.Select(Query);

            AdvanceInitializationDetails_LIST = DataTableToList.DataTableToListConvert<Models.Advance_initialization_details>(dt_avvance_int_details);


        }

        public DataTable Get_Shedule_ID(string LOAN_ID)
        {

            string Query = "select *  from ADVANCE_SCHEDULE Where ADVANCE_INIT_DET_ID = "+ LOAN_ID + " ";
                          

            DataTable dt_avvance_Shedule = this.commonGatewayObj.Select(Query);

            return dt_avvance_Shedule;
        }

        public DataTable GenarateAdvanceShedule(Advance_initialization_details AvdIntDet)
        {
            double amount = AvdIntDet.DISBURSED_AMT;
            double interest_rate = AvdIntDet.INT_RATE;
            double installment = AvdIntDet.INSTALLMENT_AMT;
            int period = AvdIntDet.NO_OF_YEAR;
            int NO_OF_INSTALLMENT = AvdIntDet.NO_OF_INSTALLMENT;
            double balance = 0;
            double interest = 0;
            string SCHEDULE_DATE = "";
            double principal = 0;
            double Cumulative_Interest = 0;

            DateTime start_date = Convert.ToDateTime(AvdIntDet.DISBURSED_DATE);



            DataRow dr;
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("ADVANCE_SCHEDULE_ID", typeof(int));
            dt.Columns.Add("SCHEDULE_DATE", typeof(string));
            dt.Columns.Add("INTEREST_RATE", typeof(double));
            dt.Columns.Add("INSTALLMENT_AMT", typeof(double));
            dt.Columns.Add("INTEREST_PAID", typeof(double));
            dt.Columns.Add("CUMULATIVE_INTEREST", typeof(double));
            dt.Columns.Add("PRINCIPAL_PAID", typeof(double));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("BALANCE", typeof(double));



            for (int i = 1; i <= NO_OF_INSTALLMENT ; i++)
            {
                dr = dt.NewRow();

                if (balance >= 0 && installment > 0)
                {
                    dr["ADVANCE_SCHEDULE_ID"] = i;
                    dr["SCHEDULE_DATE"] = start_date.AddMonths(i).ToString("dd-MMM-yyyy");
                    SCHEDULE_DATE = start_date.AddMonths(i).ToString("dd-MMM-yyyy");

                    DataTable dtIntRate = GetInterestRate(SCHEDULE_DATE);
                    if (dtIntRate.Rows.Count > 0)
                    {
                        interest_rate = Convert.ToDouble(dtIntRate.Rows[0]["INT_RATE"]);
                        dr["INTEREST_RATE"] = interest_rate;
                    }
                    else
                    {
                        interest_rate = AvdIntDet.INT_RATE;
                        dr["INTEREST_RATE"] = interest_rate;
                    }
                  
                    dr["INSTALLMENT_AMT"] = installment;
                   
                    if (i == 1)
                    {
                        interest = amount * ((interest_rate / 100) / 12);
                        balance = amount - installment;
                        Cumulative_Interest = interest;
                        principal = installment - interest;
                        dr["BALANCE"] = balance;
                        dr["INTEREST_PAID"] = interest;
                        dr["TYPE"] = "P";
                        dr["CUMULATIVE_INTEREST"] = Cumulative_Interest;
                        dr["PRINCIPAL_PAID"] = principal;
                    }
                    else
                    {
                        
                        if (balance > 0 && interest > 0)
                        {


                            if (balance < installment)
                            {
                                double total_balance = balance;
                                balance = total_balance + Cumulative_Interest;

                                Cumulative_Interest = 0;
                                interest = 0;
                                balance = balance - installment;
                                dr["BALANCE"] = balance;
                                principal = installment;
                                dr["INTEREST_PAID"] = interest;
                                dr["TYPE"] = "I";
                                dr["CUMULATIVE_INTEREST"] = Cumulative_Interest;
                                dr["PRINCIPAL_PAID"] = principal;

                               
                            }
                            else
                            {
                               
                                interest = balance * ((interest_rate / 100) / 12);
                                balance = balance - installment;
                                Cumulative_Interest = Cumulative_Interest + interest;
                                principal = installment - interest;
                                dr["BALANCE"] = balance;
                                dr["INTEREST_PAID"] = interest;
                                dr["TYPE"] = "P";
                                dr["CUMULATIVE_INTEREST"] = Cumulative_Interest;
                                dr["PRINCIPAL_PAID"] = principal;

                            }

                        }
                        else
                        {

                            if (balance == 0)
                            {
                                balance = Cumulative_Interest;
                            }
                            if (principal > 0)
                            {
                                double total_balance = balance;
                                Cumulative_Interest = 0;
                                interest = 0;
                                balance = total_balance - installment;
                                dr["BALANCE"] = balance;
                                principal = installment;
                                dr["INTEREST_PAID"] = interest;
                                dr["TYPE"] = "I";
                                dr["CUMULATIVE_INTEREST"] = Cumulative_Interest;
                                dr["PRINCIPAL_PAID"] = principal;

                                if (total_balance < installment)
                                {

                                    dr["INSTALLMENT_AMT"] = total_balance;
                                    dr["PRINCIPAL_PAID"] = total_balance;
                                    dr["BALANCE"] = 0;
                                    Cumulative_Interest = 0;
                                    installment = 0;


                                }
                            }



                        }


                    }

                    dt.Rows.Add(dr);
                }

            }

            return dt;

        }




        public DataTable Reshedule()
        {

            string Query = " select * from ADVANCE_SCHEDULE where ADVANCE_INIT__ID =32 and  ADVANCE_SCHEDULE_ID=   (select max(ADVANCE_SCHEDULE_ID) AS ADVANCE_SCHEDULE_ID  from ADVANCE_SCHEDULE where  PAID='Y') ";

            DataTable dt_avvanceShedule = this.commonGatewayObj.Select(Query);
            
            double interest_rate = 7;
            double amount = Convert.ToDouble(dt_avvanceShedule.Rows[0]["BALANCE"]);
            double installment = 10000;
            int NO_OF_INSTALLMENT = 125;

            double balance = Convert.ToDouble(dt_avvanceShedule.Rows[0]["BALANCE"]);
            double interest = Convert.ToDouble(dt_avvanceShedule.Rows[0]["INTEREST_PAID"]);

            double principal = Convert.ToDouble(dt_avvanceShedule.Rows[0]["PRINCIPAL_PAID"]); 
            double Cumulative_Interest = Convert.ToDouble(dt_avvanceShedule.Rows[0]["CUMULATIVE_INTEREST"]); 
            DateTime start_date = Convert.ToDateTime(dt_avvanceShedule.Rows[0]["SCHEDULE_DATE"]);
            DateTime reshudledate = start_date;

            DataRow dr;
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("ADVANCE_SCHEDULE_ID", typeof(int));
            dt.Columns.Add("SCHEDULE_DATE", typeof(string));
            dt.Columns.Add("INSTALLMENT_AMT", typeof(double));
            dt.Columns.Add("INTEREST_PAID", typeof(double));
            dt.Columns.Add("CUMULATIVE_INTEREST", typeof(double));
            dt.Columns.Add("PRINCIPAL_PAID", typeof(double));
            dt.Columns.Add("TYPE", typeof(string));
            dt.Columns.Add("BALANCE", typeof(double));

               int j = 1;
                for (int i = Convert.ToInt32(dt_avvanceShedule.Rows[0]["ADVANCE_SCHEDULE_ID"]) + 1; i <= NO_OF_INSTALLMENT; i++)
                {
                   
                    dr = dt.NewRow();
                    dr["ADVANCE_SCHEDULE_ID"] = i;
                    dr["SCHEDULE_DATE"] = Convert.ToDateTime(reshudledate).AddMonths(j).ToString("dd-MMM-yyyy");
                    dr["INSTALLMENT_AMT"] = installment;

                    if (balance > 0 && interest > 0)
                    {
                        balance = balance - installment;
                        interest = balance * ((interest_rate / 100) / 12);
                        Cumulative_Interest = Cumulative_Interest + interest;
                        principal = installment - interest;
                        dr["BALANCE"] = balance;
                        dr["INTEREST_PAID"] = interest;
                        dr["TYPE"] = "P";
                        dr["CUMULATIVE_INTEREST"] = Cumulative_Interest;
                        dr["PRINCIPAL_PAID"] = principal;
                    }
                    else
                    {

                        if (balance == 0)
                        {
                            balance = Cumulative_Interest;
                        }
                        if (principal > 0 )
                        {
                            if (installment == 0)
                            {
                                break;
                            }
                            else
                            {
                                Cumulative_Interest = 0;
                                balance = balance - installment;
                                dr["BALANCE"] = balance;
                                principal = installment;
                                dr["INTEREST_PAID"] = 0;
                                dr["TYPE"] = "I";
                                dr["CUMULATIVE_INTEREST"] = 0;
                                dr["PRINCIPAL_PAID"] = principal;

                                if (balance < installment)
                                {
                                    dr["INSTALLMENT_AMT"] = balance;
                                    dr["PRINCIPAL_PAID"] = balance;
                                    dr["BALANCE"] = 0;
                                    Cumulative_Interest = 0;
                                    installment = 0;


                                }

                            }
                           
                        }

                    }
                    dt.Rows.Add(dr);
                    j++;
                }
           



           return dt;
        }

        public DataTable GetInterestRate(string SCHEDULE_DATE)
        {
            string Query = "Select * from (SELECT Min(INT_ID) AS ID FROM ADVANCE_INTEREST where INT_EFFECTIVE_TO_DATE  >  '"+ SCHEDULE_DATE + "' ) tab1 inner join ADVANCE_INTEREST Tab2 on tab1.ID=tab2.INT_ID ";
            DataTable dtInterestRate = this.commonGatewayObj.Select(Query);
            return dtInterestRate;
        }
        public DataTable GetShedule(string ADVANCE_INIT_DET_ID)
        {
            string Query = "select * from ADVANCE_SCHEDULE where ADVANCE_INIT_DET_ID= " + ADVANCE_INIT_DET_ID + " order by ADVANCE_SCHEDULE_ID ASC ";
            DataTable dtAdvShedule = this.commonGatewayObj.Select(Query);
            return dtAdvShedule;
        }
         public DataTable GetDisburshedId(string DISBURSED_DATE_FORM, string DISBURSED_DATE_TO)
        {
            string Query = "  select tab1.*,TAB2.NAME as DESIGNATION  from (select C.ADVANCE_INIT_DET_ID,CLOSE_DATE,C.EMPLOYEE_ID,D.NAME,D.DESIG_ID from (Select ADVANCE_INIT_DET_ID,EMPLOYEE_ID,CLOSE_DATE from   ( select distinct (ADVANCE_INIT_DET_ID)  from ADVANCE_SCHEDULE where  SCHEDULE_DATE between " +
                "  '"+ DISBURSED_DATE_FORM + "' and '"+ DISBURSED_DATE_TO + "' order by ADVANCE_INIT_DET_ID asc ) a inner join ADVANCE_INITIALIZATION_DETAILS b on a.ADVANCE_INIT_DET_ID=B.DISBURSED_ID    order by DISBURSED_DATE asc) C " +
                "  inner join EMP_INFO D on C.EMPLOYEE_ID=D.ID   ) tab1 inner join  EMP_DESIGNATION tab2 on tab1.DESIG_ID=tab2.ID order by ADVANCE_INIT_DET_ID asc ";
            DataTable dtGetDisburshedId = this.commonGatewayObj.Select(Query);
            return dtGetDisburshedId;
        }
        public DataTable GetSheduleBYDisburshedIdandDate(string ADVANCE_INIT_DET_ID, string DISBURSED_DATE_FORM, string DISBURSED_DATE_TO)
        {
            string Query = "   select *  from ADVANCE_SCHEDULE where ADVANCE_INIT_DET_ID="+ ADVANCE_INIT_DET_ID + " and SCHEDULE_DATE between TO_DATE('" + DISBURSED_DATE_FORM + "', 'DD-MM-YYYY') and TO_DATE('" + DISBURSED_DATE_TO + "', 'DD-MM-YYYY') order by ADVANCE_SCHEDULE_ID asc ";
            DataTable dtGetDisburshedId = this.commonGatewayObj.Select(Query);
            return dtGetDisburshedId;
        }
        public DataTable GetPaymentStaus(string ADVANCE_INIT_DET_ID,string DISBURSED_DATE)
        {
            string Query = "select ADVANCE_INIT_DET_ID,totalPrincipal,TotalInterest,NoOfInstallment,(TOTAL_PRINCIPAL_AMT- totalPrincipal) As PrincipalDue,"+
                            "(TOTAL_INTEREST_AMT - TotalInterest) As InterestDue,(NO_OF_INSTALLMENT - NoOfInstallment) AS InstallmentDue "+
                              "from(select ADVANCE_INIT_DET_ID, sum(INSTALLMENT_AMT) AS totalPrincipal, sum(INTEREST_PAID) AS TotalInterest, "+
                                " count(ADVANCE_SCHEDULE_ID) as NoOfInstallment from ADVANCE_SCHEDULE where ADVANCE_INIT_DET_ID = "+ ADVANCE_INIT_DET_ID + " and paid is not null and SCHEDULE_DATE <= '"+ DISBURSED_DATE + "' group by ADVANCE_INIT_DET_ID "+
                                    ") tab1 inner join ADVANCE_INITIALIZATION_DETAILS tab2 ON tab1.ADVANCE_INIT_DET_ID = tab2.DISBURSED_ID";
            DataTable dtPaymentStaus = this.commonGatewayObj.Select(Query);
            return dtPaymentStaus;
        }
        public DataTable GetLoanBalance(string DISBURSED_DATE)
        {
            string Query = " select tab1.*,TAB2.NAME as DESIGNATION from ( select C.DISBURSED_ID,C.DISBURSED_AMT,C.DISBURSED_DATE,C.CLOSE_DATE,C.LOAN_STATUS,C.TOTAL_INTEREST_AMT,D.NAME,D.DESIG_ID,C.Principal,C.Interest from " +
                            " (select DISBURSED_ID,DISBURSED_DATE,LOAN_STATUS,CLOSE_DATE,EMPLOYEE_ID, DISBURSED_AMT,TOTAL_INTEREST_AMT,decode(totalPrincipal,null,DISBURSED_AMT,totalPrincipal) AS Principal,decode(TotalInterest,null,0,TotalInterest)AS Interest from " +
                              "  ( select ADVANCE_INIT_DET_ID, sum(INSTALLMENT_AMT) AS totalPrincipal, sum(INTEREST_PAID) AS TotalInterest, count(ADVANCE_SCHEDULE_ID) as NoOfInstallment " +
                                "   from ADVANCE_SCHEDULE where  paid is not null and SCHEDULE_DATE <= '"+ DISBURSED_DATE + "' group by ADVANCE_INIT_DET_ID) a  full  outer join  (SELECT * FROM ADVANCE_INITIALIZATION_DETAILS Where DISBURSED_DATE  <= '"+ DISBURSED_DATE + "' ) b " +
                                    "   on a.ADVANCE_INIT_DET_ID=b.DISBURSED_ID ) C inner join  EMP_INFO D on C.EMPLOYEE_ID=D.ID order by DISBURSED_DATE asc ) tab1 inner join EMP_DESIGNATION tab2 on tab1.DESIG_ID=tab2.ID  ";
            DataTable dtLoanStaus = this.commonGatewayObj.Select(Query);
            return dtLoanStaus;
        }


        public DataTable GetDisbursedAndInterestBalance(string FORM_DATE, string TO_DATE,string LOAN_STATUS)
        {
            string Query = "";
            if (LOAN_STATUS == "I")
            {
                Query = "  select tab1.*,TAB2.NAME as DESIGNATION from ( select C.DISBURSED_ID,D.NAME,D.DESIG_ID,CLOSE_DATE,SCHEDULE_DATE,DISBURSED_DATE, C.TotalInstallment from  ( select DISBURSED_ID, " +
                "  DISBURSED_DATE,CLOSE_DATE,SCHEDULE_DATE,EMPLOYEE_ID, TotalInstallment from   (  select ADVANCE_INIT_DET_ID, sum(INTEREST_PAID) AS TotalInstallment,MAX(SCHEDULE_DATE) AS SCHEDULE_DATE " +
                  "   from ADVANCE_SCHEDULE where  SCHEDULE_DATE between '" + FORM_DATE + "' and '" + TO_DATE + "'  group by ADVANCE_INIT_DET_ID " +
                    "     ) a inner join ADVANCE_INITIALIZATION_DETAILS b    on a.ADVANCE_INIT_DET_ID=b.DISBURSED_ID  ) C inner join  EMP_INFO D on " +
                        "  C.EMPLOYEE_ID=D.ID order by DISBURSED_DATE asc ) tab1 inner join EMP_DESIGNATION tab2 on tab1.DESIG_ID=tab2.ID  where TotalInstallment > 0  ";

            }
            else if (LOAN_STATUS == "P")
            {
                Query = "   select  tab1.DISBURSED_ID,tab1.NAME,tab1.DESIG_ID,tab1.DISBURSED_DATE,tab1.DISBURSED_AMT AS TotalInstallment ,TAB2.NAME as DESIGNATION from    (   " +
               "    select a.*,e.Name,E.DESIG_ID from ( SELECT  DISBURSED_ID ,EMPLOYEE_ID,DISBURSED_DATE,  DISBURSED_AMT FROM  " +
                 "     ADVANCE_INITIALIZATION_DETAILS where  DISBURSED_DATE between '"+ FORM_DATE + "' and '"+ TO_DATE + "'    ) a inner join  EMP_INFO e on   " +
                   "         a.EMPLOYEE_ID=e.ID order by DISBURSED_DATE Desc  ) tab1 inner join EMP_DESIGNATION tab2 on tab1.DESIG_ID=tab2.ID  ";

            }

            DataTable dtLoanStaus = this.commonGatewayObj.Select(Query);
            return dtLoanStaus;
        }

        public DataTable Get_AllinfoByDISBURSED_ID(string LOAN_ID)
        {
            string Query = "  select tab1.*,TAB2.NAME AS DESIG from  ((select * from (select * from (SELECT DISBURSED_ID, DISBURSED_AMT, DISBURSED_DATE,   INSTALLMENT_AMT, TOTAL_PRINCIPAL_AMT, TOTAL_INTEREST_AMT,  " +
                         " INT_RATE, INT_RATE_PREVIOUS, NO_OF_INSTALLMENT,    NO_OF_YEAR, TIMES_OF_LOAN, LOAN_STATUS,   ADVANCE_INIT_DET_ACTIVATE, ADVANCE_INIT_ID, ADVANVE_INIT_DET_DATE,  EMPLOYEE_ID AS ID, SCHEDULE_GENARATE_DATE, SHEDULE_GENARATE, " +
                          " RESCHEDULED FROM ADVANCE_INITIALIZATION_DETAILS where DISBURSED_ID=" + LOAN_ID + ") a inner join ADVANCE_INITIALIZATION b on a.ADVANCE_INIT_ID=b.ADVANCE_INIT_ID) d inner join EMP_INFO e on d.ID=e.ID )) tab1 inner join EMP_DESIGNATION tab2  on tab1.DESIG_ID = tab2.ID ";

            DataTable dtSheduleWithEmpInfo = this.commonGatewayObj.Select(Query);
            return dtSheduleWithEmpInfo;
        }


        public DataTable Get_DISBURSED_ID(string LOAN_ID)
        {
            string Query = " SELECT  * from ADVANCE_INITIALIZATION_DETAILS Where LOAN_ID='" + LOAN_ID + "' ";        
            DataTable dtDISBURSED_ID = this.commonGatewayObj.Select(Query);
            return dtDISBURSED_ID;
        }
        public DataTable Get_DISBURSED_With_DATE(int ADVANCE_INIT_ID, string DISBURSED_DATE)
        {
            string Query = " SELECT  * from ADVANCE_INITIALIZATION_DETAILS Where ADVANCE_INIT_ID=" + ADVANCE_INIT_ID + " and DISBURSED_DATE=TO_DATE('" + DISBURSED_DATE + "', 'DD-MM-YYYY') ";
            DataTable dtadvanceIntDetils = this.commonGatewayObj.Select(Query);
            return dtadvanceIntDetils;
        }

        public DataTable Get_INTEREST_RATE_DISBURSED_ID(string ADVANCE_INIT_DET_ID)
        {
            string Query = " SELECT distinct(INTEREST_RATE)  from ADVANCE_SCHEDULE Where ADVANCE_INIT_DET_ID='" + ADVANCE_INIT_DET_ID + "' ";
            DataTable dtINTEREST_RATE = this.commonGatewayObj.Select(Query);
            return dtINTEREST_RATE;
        }


        public string SaveInitialization(List<AdvanceInitialization> AvdInt)
        {
            string mess = "";
            try
            {
                this.commonGatewayObj.BeginTransaction();

                this.commonGatewayObj._INSERT(AvdInt, "ADVANCE_INITIALIZATION");

                this.commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                this.commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
                
            }

            return mess;
        }


        public string UpdateAdvanceInitializationDetails(Advance_initialization_details AvdIntDet)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvIntDet = new Hashtable();



                htAdvIntDet.Add("DISBURSED_AMT", AvdIntDet.DISBURSED_AMT.ToString());
                htAdvIntDet.Add("DISBURSED_DATE", AvdIntDet.DISBURSED_DATE.ToString());
                htAdvIntDet.Add("INSTALLMENT_AMT", AvdIntDet.INSTALLMENT_AMT.ToString());
                htAdvIntDet.Add("INT_RATE", AvdIntDet.INT_RATE.ToString());
                htAdvIntDet.Add("NO_OF_YEAR", AvdIntDet.NO_OF_YEAR.ToString());
                htAdvIntDet.Add("NO_OF_INSTALLMENT", AvdIntDet.NO_OF_INSTALLMENT.ToString());
                htAdvIntDet.Add("TIMES_OF_LOAN", AvdIntDet.TIMES_OF_LOAN.ToString());
                htAdvIntDet.Add("LOAN_STATUS", AvdIntDet.LOAN_STATUS.ToString());
                htAdvIntDet.Add("CLOSE_DATE", AvdIntDet.CLOSE_DATE.ToString());



                commonGatewayObj.Update(htAdvIntDet, "ADVANCE_INITIALIZATION_DETAILS", "DISBURSED_ID = " + AvdIntDet.DISBURSED_ID + "");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;
        }

        public string UpdateInitializationDetails(Advance_initialization_details AvdIntDet)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvIntDet = new Hashtable();

               
                htAdvIntDet.Add("TOTAL_PRINCIPAL_AMT", AvdIntDet.TOTAL_PRINCIPAL_AMT.ToString());
                htAdvIntDet.Add("TOTAL_INTEREST_AMT", AvdIntDet.TOTAL_INTEREST_AMT.ToString());
                htAdvIntDet.Add("SCHEDULE_GENARATE_DATE", AvdIntDet.SCHEDULE_GENARATE_DATE.ToString());
                htAdvIntDet.Add("SHEDULE_GENARATE", AvdIntDet.SHEDULE_GENARATE.ToString());
               
         
                
                commonGatewayObj.Update(htAdvIntDet, "ADVANCE_INITIALIZATION_DETAILS", "DISBURSED_ID = " + AvdIntDet.DISBURSED_ID + "and ADVANCE_INIT_ID = '" + AvdIntDet.ADVANCE_INIT_ID + "'");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;
        }

        public string UpdateEarlySatlement(Advance_initialization_details AvdIntDet)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvIntDet = new Hashtable();


                htAdvIntDet.Add("ADVANCE_INIT_DET_ACTIVATE", AvdIntDet.ADVANCE_INIT_DET_ACTIVATE.ToString());
                htAdvIntDet.Add("LOAN_STATUS", AvdIntDet.LOAN_STATUS.ToString());
                htAdvIntDet.Add("CLOSE_TYPE", AvdIntDet.CLOSE_TYPE.ToString());
                htAdvIntDet.Add("CLOSE_DATE", AvdIntDet.CLOSE_DATE.ToString());



                commonGatewayObj.Update(htAdvIntDet, "ADVANCE_INITIALIZATION_DETAILS", "DISBURSED_ID = " + AvdIntDet.DISBURSED_ID + "and ADVANCE_INIT_ID = '" + AvdIntDet.ADVANCE_INIT_ID + "'");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;
        }


        public DataTable Get_TotalInterestAndPrincipal(int ADVANCE_INIT__ID,int ADVANCE_INIT_DET_ID)
        {
            string Query = "select sum(INTEREST_PAID) As INTEREST_PAID  ,SUM(PRINCIPAL_PAID) As PRINCIPAL_PAID from (SELECT "+
                         " ADVANCE_SCHEDULE_ID, SCHEDULE_DATE, INSTALLMENT_AMT, INTEREST_RATE, INTEREST_PAID,"+ 
                          " CUMULATIVE_INTEREST, PRINCIPAL_PAID, BALANCE,  ADVANCE_INIT_DET_ID, ADVANCE_INIT__ID, TYPE "+
                        " FROM INVEST.ADVANCE_SCHEDULE) Where ADVANCE_INIT__ID = "+ ADVANCE_INIT__ID + " and ADVANCE_INIT_DET_ID = "+ ADVANCE_INIT_DET_ID + " ";
            DataTable dttotalIntAndPrincipal = this.commonGatewayObj.Select(Query);
            return dttotalIntAndPrincipal;
        }


        public int Get_Adv_shedule_ID()
        {
            int ID;
            string Query = "Select MAX(ID)+1 as ID  from ADVANCE_SCHEDULE";
            DataTable dtAdVID = this.commonGatewayObj.Select(Query);

            if (dtAdVID.Rows[0]["ID"].ToString() == null)
            {
                ID = 1;
            }
            else
            {
                ID = Convert.ToInt32(dtAdVID.Rows[0]["ID"].ToString());
            }



            return ID;
        }
        public int Get_ADV_INT_ID()
        {
            int ID;
            string Query = "Select MAX(ADVANCE_INIT_ID)+1 as ID  from ADVANCE_INITIALIZATION";
            DataTable dtAdVID = this.commonGatewayObj.Select(Query);

            if (dtAdVID.Rows[0]["ID"].ToString() == null)
            {
                ID = 1;
            }
            else
            {
                ID = Convert.ToInt32(dtAdVID.Rows[0]["ID"].ToString());
            }



            return ID;
        }

        

        public DataTable Get_ADV_INT_DISBURSED_ID()
        {
            string Query = "Select MAX(DISBURSED_ID)+1 as DISBURSED_ID  from ADVANCE_INITIALIZATION_DETAILS";
            DataTable dtAdVID = this.commonGatewayObj.Select(Query);
            return dtAdVID;
        }
        public DataTable Get_ADVANCE_EARLY_SATALEMENT_ID()
        {
            string Query = "Select MAX(ADVANCE_EARLY_SATALEMENT_ID)+1 as ADVANCE_EARLY_SATALEMENT_ID  from ADVANCE_EARLY_SATALEMENT";
            DataTable dtAdVID = this.commonGatewayObj.Select(Query);
            return dtAdVID;
        }

        public string Initialization(List<AdvanceInitialization> AvdInt)
        {
                string mess = "";
                try
                {
                    commonGatewayObj.BeginTransaction();


                    Hashtable htAdvInt = new Hashtable();

                    foreach (AdvanceInitialization aVdInt in AvdInt)
                    {

                        htAdvInt.Add("ADVANCE_INIT_ID", aVdInt.ADVANCE_INIT_ID.ToString());
                        htAdvInt.Add("ADVANVE_INIT_NAME", aVdInt.ADVANVE_INIT_NAME.ToString());
                        htAdvInt.Add("EMPLOYEE_JOIN_DATE", aVdInt.EMPLOYEE_JOIN_DATE.ToString());
                        htAdvInt.Add("EMPLOYEE_RETIRE_DATE", aVdInt.EMPLOYEE_RETIRE_DATE.ToString());
                        htAdvInt.Add("ADVANVE_ACTIVATE", aVdInt.ADVANVE_ACTIVATE.ToString());
                        htAdvInt.Add("EMPLOYEE_BASIC", aVdInt.EMPLOYEE_BASIC.ToString());
                        htAdvInt.Add("BANK_ACCOUNT", aVdInt.BANK_ACCOUNT.ToString());
                        htAdvInt.Add("EMPLOYEE_ID", aVdInt.EMPLOYEE_ID.ToString());
                        htAdvInt.Add("AVD_INT_DATE", aVdInt.AVD_INT_DATE.ToString());
                        htAdvInt.Add("ADVANCE_ID", aVdInt.ADVANCE_ID.ToString());
                        htAdvInt.Add("OFFICE_ID", aVdInt.OFFICE_ID.ToString());
                }

                    commonGatewayObj.Insert(htAdvInt, "ADVANCE_INITIALIZATION");


                    commonGatewayObj.CommitTransaction();
                    mess = "Success";
                }
                catch (Exception e)
                {
                    commonGatewayObj.RollbackTransaction();
                    mess = "error";
                    throw e;
                }

                return mess;

            }



        public string SaveSheduleAdvance(Advance_schedule aVdShedule)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvShedule = new Hashtable();
                int Id = Get_Adv_shedule_ID();

                htAdvShedule.Add("ID", Id);
                htAdvShedule.Add("ADVANCE_SCHEDULE_ID", aVdShedule.ADVANCE_SCHEDULE_ID.ToString());
                htAdvShedule.Add("SCHEDULE_DATE", aVdShedule.SCHEDULE_DATE.ToString());
                htAdvShedule.Add("INSTALLMENT_AMT", Convert.ToDouble(aVdShedule.INSTALLMENT_AMT.ToString()));
                htAdvShedule.Add("INTEREST_RATE", Convert.ToDouble(aVdShedule.INTEREST_RATE.ToString()));
                htAdvShedule.Add("INTEREST_PAID", Convert.ToDouble(aVdShedule.INTEREST_PAID.ToString()));
                htAdvShedule.Add("CUMULATIVE_INTEREST", Convert.ToDouble(aVdShedule.CUMULATIVE_INTEREST.ToString()));
                htAdvShedule.Add("PRINCIPAL_PAID", Convert.ToDouble(aVdShedule.PRINCIPAL_PAID.ToString()));
                htAdvShedule.Add("TYPE", aVdShedule.TYPE.ToString());
                htAdvShedule.Add("BALANCE", Convert.ToDouble(aVdShedule.BALANCE.ToString()));
                htAdvShedule.Add("ADVANCE_INIT_DET_ID", Convert.ToInt32(aVdShedule.ADVANCE_INIT_DET_ID.ToString()));
                htAdvShedule.Add("ADVANCE_INIT__ID", Convert.ToInt32(aVdShedule.ADVANCE_INIT__ID.ToString()));
               

                commonGatewayObj.Insert(htAdvShedule, "ADVANCE_SCHEDULE");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;

        }

        public string Initialization_DISBURSE(List<Advance_initialization_details> AvdIntDetails)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvIntDet = new Hashtable();

                foreach (Advance_initialization_details aVdIntDet in AvdIntDetails)
                {
                    htAdvIntDet.Add("DISBURSED_ID", aVdIntDet.DISBURSED_ID.ToString());
                    htAdvIntDet.Add("DISBURSED_AMT", aVdIntDet.DISBURSED_AMT.ToString());
                    htAdvIntDet.Add("DISBURSED_DATE", aVdIntDet.DISBURSED_DATE.ToString());
                    htAdvIntDet.Add("INSTALLMENT_AMT", aVdIntDet.INSTALLMENT_AMT.ToString());
                    htAdvIntDet.Add("INT_RATE", aVdIntDet.INT_RATE.ToString());
                    htAdvIntDet.Add("NO_OF_INSTALLMENT", aVdIntDet.NO_OF_INSTALLMENT.ToString());
                    htAdvIntDet.Add("TIMES_OF_LOAN", aVdIntDet.TIMES_OF_LOAN.ToString());
                    htAdvIntDet.Add("NO_OF_YEAR", aVdIntDet.NO_OF_YEAR.ToString());
                    htAdvIntDet.Add("LOAN_STATUS", aVdIntDet.LOAN_STATUS.ToString());
                    htAdvIntDet.Add("ADVANCE_INIT_DET_ACTIVATE", aVdIntDet.ADVANCE_INIT_DET_ACTIVATE.ToString());
                    htAdvIntDet.Add("ADVANCE_INIT_ID", aVdIntDet.ADVANCE_INIT_ID.ToString());
                    htAdvIntDet.Add("ADVANVE_INIT_DET_DATE", aVdIntDet.ADVANVE_INIT_DET_DATE.ToString());
                    htAdvIntDet.Add("EMPLOYEE_ID", aVdIntDet.EMPLOYEE_ID.ToString());
                    htAdvIntDet.Add("LOAN_ID", aVdIntDet.LOAN_ID.ToString());
                }

                commonGatewayObj.Insert(htAdvIntDet, "ADVANCE_INITIALIZATION_DETAILS");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;

        }



        public string AddEarlySatlement(List<EarlySatlement> EarlySatlementDetails)
        {
            string mess = "";
            try
            {
                commonGatewayObj.BeginTransaction();


                Hashtable htAdvIntDet = new Hashtable();

                foreach (EarlySatlement earlySatlement in EarlySatlementDetails)
                {
                    htAdvIntDet.Add("ADVANCE_EARLY_SATALEMENT_ID", earlySatlement.ADVANCE_EARLY_SATALEMENT_ID.ToString());
                    htAdvIntDet.Add("DISBURSED_ID", earlySatlement.DISBURSED_ID.ToString());
                    htAdvIntDet.Add("DUE_PRINCIPAL", earlySatlement.DUE_PRINCIPAL.ToString());
                    htAdvIntDet.Add("DUE_INTEREST", earlySatlement.DUE_INTEREST.ToString());
                    htAdvIntDet.Add("EMPLOYEE_ID", earlySatlement.EMPLOYEE_ID.ToString());
                    htAdvIntDet.Add("EARLY_SATALEMENT_DATE", earlySatlement.EARLY_SATALEMENT_DATE.ToString());
                    htAdvIntDet.Add("LOAN_ID", earlySatlement.LOAN_ID.ToString());
                    htAdvIntDet.Add("CHECK_NO", earlySatlement.CHECK_NO.ToString());

                }

                commonGatewayObj.Insert(htAdvIntDet, "ADVANCE_EARLY_SATALEMENT");


                commonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                commonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;

        }


        public void Get_All_Advance_Type()
    {
        string Query = "SELECT  ADVANCE_ID, ADVANCE_NAME FROM ADVANCE_TYPE";

        DataTable dt_avD_list = commonGatewayObj.Select(Query);

        AdvanceType_LIST = DataTableToList.DataTableToListConvert<AdvanceType>(dt_avD_list);


    }

}
}