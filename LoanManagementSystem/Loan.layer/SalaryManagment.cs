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
    public class SalaryManagment
    {
        CommonGateway CommonGatewayObj = new CommonGateway();
        LoanManagment lmg = new LoanManagment();
        public List<SalaryDetails> empSalaryDelails_LIST { get; set; }
        public SalaryManagment()
        {
            ShowSalary();
        }


        public DataTable SalaryProcessGenarate(string FromDate, string Todate, string CalDate)
        {
            double area, gtwa, npwa, dd, pp, wa, basic, hrnt, gt, ma, hm, ca, ppn, td, pcc, pco, sppco, np, hba, cmcl, asl, osl, sul, stb, empbf, rstmp, gip, intx, others, da, cea, emt, utly, tlp, pfl, tempad, ta;


            DataTable dtamcl_emp_salary = new DataTable();

            dtamcl_emp_salary.Columns.Add("AUTOID", typeof(int));
            dtamcl_emp_salary.Columns.Add("ID", typeof(string));
            dtamcl_emp_salary.Columns.Add("BASIC", typeof(double));
            dtamcl_emp_salary.Columns.Add("HOUSE_RENT", typeof(double));
            dtamcl_emp_salary.Columns.Add("HB_ADV", typeof(double));
            dtamcl_emp_salary.Columns.Add("MED_ALLOW", typeof(double));
            dtamcl_emp_salary.Columns.Add("HOUSE_MAINT", typeof(string));
            dtamcl_emp_salary.Columns.Add("CONV_ALLOW", typeof(double));
            dtamcl_emp_salary.Columns.Add("PF_PN_COR", typeof(double));
            dtamcl_emp_salary.Columns.Add("PENSION_CONTR_COR", typeof(double));
            dtamcl_emp_salary.Columns.Add("PF_CONTR_COR", typeof(double));
            dtamcl_emp_salary.Columns.Add("PF_CONTR_OWN", typeof(double));
            dtamcl_emp_salary.Columns.Add("PF_LOAN", typeof(double));
            dtamcl_emp_salary.Columns.Add("CAR_MOTOR_LOAN", typeof(double));
            dtamcl_emp_salary.Columns.Add("ASSOC_LOAN", typeof(string));
            dtamcl_emp_salary.Columns.Add("OFF_ASS_SUB", typeof(double));
            dtamcl_emp_salary.Columns.Add("STAFF_UNION_SUB", typeof(double));
            dtamcl_emp_salary.Columns.Add("STAFF_BUS", typeof(double));
            dtamcl_emp_salary.Columns.Add("ENTERTAINMENT", typeof(double));
            dtamcl_emp_salary.Columns.Add("EMP_BEN_FUND", typeof(string));
            dtamcl_emp_salary.Columns.Add("WASH_ALLOWS", typeof(double));
            dtamcl_emp_salary.Columns.Add("DEDUCT", typeof(double));
            dtamcl_emp_salary.Columns.Add("P_PAY", typeof(double));
            dtamcl_emp_salary.Columns.Add("UTILITIES", typeof(string));
            dtamcl_emp_salary.Columns.Add("NEWSPAPER", typeof(double));
            dtamcl_emp_salary.Columns.Add("REV_STAMP", typeof(double));
            dtamcl_emp_salary.Columns.Add("TELEPHONE", typeof(double));
            dtamcl_emp_salary.Columns.Add("INCOME_TAX", typeof(double));
            dtamcl_emp_salary.Columns.Add("PREVIOUS_INCOME_TAX", typeof(double));
            dtamcl_emp_salary.Columns.Add("GI_PREM", typeof(string));
            dtamcl_emp_salary.Columns.Add("OTHERS", typeof(double));
            dtamcl_emp_salary.Columns.Add("TEMP_ADVANCE", typeof(double));
            dtamcl_emp_salary.Columns.Add("DISBURSED_ID", typeof(int));
            dtamcl_emp_salary.Columns.Add("ADVANCE_INIT_ID", typeof(int));
            dtamcl_emp_salary.Columns.Add("ADVANCE_SCHEDULE_ID", typeof(int));
            dtamcl_emp_salary.Columns.Add("DEP_ALLOW", typeof(double));
            dtamcl_emp_salary.Columns.Add("CHILD_EDU_ALLOW", typeof(string));
            dtamcl_emp_salary.Columns.Add("AREA", typeof(double));
            dtamcl_emp_salary.Columns.Add("TOTAL_DEDUCT", typeof(double));
            dtamcl_emp_salary.Columns.Add("GROSS_TOTAL", typeof(double));
            dtamcl_emp_salary.Columns.Add("GROSS_TOTAL_WITH_ALLOW", typeof(double));
            dtamcl_emp_salary.Columns.Add("NET_PAYABLE", typeof(double));
            dtamcl_emp_salary.Columns.Add("NET_PAYABLE_WITH_ALLOW", typeof(double));
            dtamcl_emp_salary.Columns.Add("NET_ALLOWANCE", typeof(double));
            dtamcl_emp_salary.Columns.Add("CAL_DATE", typeof(string));

            DataRow dramcl_emp_salary;

            DataTable dt = new DataTable();





            //DateTime FmDt = Convert.ToDateTime(FromDate);
            //DateTime Tdate = Convert.ToDateTime(Todate);
            //DateTime ClDt = Convert.ToDateTime(CalDate);

            //var FromDateFinal = FmDt.ToString("dd/MM/yyyy");   // "05/21/2014 22:09:28"
            //                                                   //FromDateFinal = FromDateFinal.ToString("MM/dd/yy");

            //var TodateFinal = Tdate.ToString("dd/MM/yyyy");

            //var CalDateFinal = ClDt.ToString("dd/MM/yyyy");


            //try
            //{
            //    DateTime d1 = Convert.ToDateTime(FromDate);
            //    DateTime d2 = Convert.ToDateTime(Todate);
            //    DateTime d3 = Convert.ToDateTime(CalDate);
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.ToString()+"from:"+FromDate+"toDate:"+Todate);
            //}
            

            


            dt = getSalaryDetails(FromDate, Todate);
            long AUTOID=CommonGatewayObj.GetMaxNo("AMCL_EMP_SALARY", "AUTOID") + 1;

            if (dt.Rows.Count > 0)
            {
                int i;

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    try
                    {
                        //Hashtable htSalaryInfo = new Hashtable();
                        dramcl_emp_salary = dtamcl_emp_salary.NewRow();

                        dramcl_emp_salary["AUTOID"] = AUTOID + i;
                        dramcl_emp_salary["ID"] = dt.Rows[i]["ID"].ToString();
                        dramcl_emp_salary["BASIC"] = dt.Rows[i]["BASIC"].ToString();

                        basic = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString());

                        char pntn = Convert.ToChar(dt.Rows[i]["ISCOMP"]);
                        char hbl = Convert.ToChar(dt.Rows[i]["HBLOAN"]);

                        if (pntn == 'Y')
                        {
                            if (basic >= 35501)
                            {
                                if ((basic <= 39000))
                                {

                                    hrnt = (basic * 0.05) + (19500 - (basic * 0.05));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 16001 && basic <= 35500)
                            {
                                if ((basic <= 17455))
                                {

                                    hrnt = (basic * 0.55) + (9600 - (basic * 0.55));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.55;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 9701 && basic <= 16000)
                            {
                                if ((basic <= 10667))
                                {

                                    hrnt = (basic * 0.60) + (6400 - (basic * 0.60));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.60;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else
                            {
                                if ((basic <= 8616))
                                {


                                    hrnt = (basic * 0.65) + (5600 - (basic * 0.65));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.65;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                        }
                        else if (pntn == 'N' && hbl == 'N')
                        {
                            if (basic >= 35501)
                            {
                                if ((basic <= 39000))
                                {

                                    hrnt = (basic * 0.05) + (19500 - (basic * 0.05));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 16001 && basic <= 35500)
                            {
                                if ((basic <= 17455))
                                {

                                    hrnt = (basic * 0.55) + (9600 - (basic * 0.55));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.55;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 9701 && basic <= 16000)
                            {
                                if ((basic <= 10667))
                                {

                                    hrnt = (basic * 0.60) + (6400 - (basic * 0.60));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.60;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else
                            {
                                if ((basic <= 8616))
                                {


                                    hrnt = (basic * 0.65) + (5600 - (basic * 0.65));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.65;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 0.00;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                        }
                        else
                        {
                            if (basic >= 35501)
                            {
                                if ((basic <= 39000))
                                {

                                    hrnt = (basic * 0.05) + (19500 - (basic * 0.05));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.05) + (19500 - (basic * 0.05));
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else if ((basic > 39000) && (dt.Rows[i]["ID"].ToString() == "IAMCL109"))
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = 10160;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else if ((basic > 39000) && (dt.Rows[i]["ID"].ToString() == "IAMCL659"))
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.50) + 13950;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else if ((basic > 39000) && (dt.Rows[i]["ID"].ToString() == "IAMCL666"))
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.50) + 5100;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.50;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = basic * 0.50;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 16001 && basic <= 35500)
                            {
                                if ((basic <= 17455))
                                {

                                    hrnt = (basic * 0.55) + (9600 - (basic * 0.55));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.55) + (9600 - (basic * 0.55));
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.55;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = basic * 0.55;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else if (basic >= 9701 && basic <= 16000)
                            {
                                if ((basic <= 10667))
                                {

                                    hrnt = (basic * 0.60) + (6400 - (basic * 0.60));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.60) + (6400 - (basic * 0.60));
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.60;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = basic * 0.60;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                            else
                            {
                                if ((basic <= 8616))
                                {


                                    hrnt = (basic * 0.65) + (5600 - (basic * 0.65));
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = (basic * 0.65) + (5600 - (basic * 0.65));
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                                else
                                {

                                    hrnt = basic * 0.65;
                                    dramcl_emp_salary["HOUSE_RENT"] = hrnt;
                                    hba = basic * 0.65;
                                    dramcl_emp_salary["HB_ADV"] = hba;
                                }
                            }
                        }

                        ma = dt.Rows[i]["MED_ALLOW"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["MED_ALLOW"].ToString());
                        dramcl_emp_salary["MED_ALLOW"] = ma;

                        hm = dt.Rows[i]["HOUSE_MAINT"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["HOUSE_MAINT"].ToString());

                        dramcl_emp_salary["HOUSE_MAINT"] = hm;

                        ca = dt.Rows[i]["CONV_ALLOW"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["CONV_ALLOW"].ToString());

                        dramcl_emp_salary["CONV_ALLOW"] = ca;


                        pcc = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.0833;

                        pco = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.10;

                        if ((dt.Rows[i]["ID"].ToString() == "IAMCL922") || (dt.Rows[i]["ID"].ToString() == "IAMCL920") || (dt.Rows[i]["ID"].ToString() == "IAMCL658") || (dt.Rows[i]["ID"].ToString() == "IAMCL659") || (dt.Rows[i]["ID"].ToString() == "IAMCL311") || (dt.Rows[i]["ID"].ToString() == "IAMCL109") || (dt.Rows[i]["ID"].ToString() == "IAMCL662") || (dt.Rows[i]["ID"].ToString() == "IAMCL663") || (dt.Rows[i]["ID"].ToString() == "IAMCL666") || (dt.Rows[i]["ID"].ToString() == "IAMCL920") || (dt.Rows[i]["ID"].ToString() == "IAMCL924") || (dt.Rows[i]["ID"].ToString() == "IAMCL926") || (dt.Rows[i]["ID"].ToString() == "IAMCL667") || (dt.Rows[i]["ID"].ToString() == "IAMCL666") || (dt.Rows[i]["ID"].ToString() == "IAMCL663") || (dt.Rows[i]["ID"].ToString() == "IAMCL931"))
                        {
                            sppco = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.25;
                        }
                        else if ((dt.Rows[i]["ID"].ToString() == "IAMCL923") || (dt.Rows[i]["ID"].ToString() == "IAMCL664") || (dt.Rows[i]["ID"].ToString() == "IAMCL927") || (dt.Rows[i]["ID"].ToString() == "IAMCL921") || (dt.Rows[i]["ID"].ToString() == "IAMCL929") || (dt.Rows[i]["ID"].ToString() == "IAMCL918"))
                        {
                            sppco = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.10;
                        }
                        else
                        {
                            sppco = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.20;
                        }


                        ppn = Convert.ToDouble(dt.Rows[i]["BASIC"].ToString()) * 0.32;

                        //char pntn = Convert.ToChar(dt.Rows[i]["ISCOMP"]);

                        char spco = Convert.ToChar(dt.Rows[i]["SPF_CONTR_OWN"]);

                        if (pntn == 'N' && spco == 'Y')
                        {
                            dramcl_emp_salary["PF_PN_COR"] = ppn;
                            dramcl_emp_salary["PENSION_CONTR_COR"] = ppn;
                            dramcl_emp_salary["PF_CONTR_COR"] = 0.0;
                            dramcl_emp_salary["PF_CONTR_OWN"] = sppco;

                          
                        }
                        else if (pntn == 'N')
                        {
                            dramcl_emp_salary["PF_PN_COR"] = ppn;
                            dramcl_emp_salary["PENSION_CONTR_COR"] = ppn;
                            dramcl_emp_salary["PF_CONTR_COR"] = 0.0;
                            dramcl_emp_salary["PF_CONTR_OWN"] = pco;

                        }
                        else
                        {
                            dramcl_emp_salary["PF_PN_COR"] = pcc;
                            dramcl_emp_salary["PENSION_CONTR_COR"] = 0.0;
                            dramcl_emp_salary["PF_CONTR_COR"] = pcc;
                            dramcl_emp_salary["PF_CONTR_OWN"] = pco;

                        }
                        pfl = dt.Rows[i]["PF_LOAN"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["PF_LOAN"].ToString());
                        dramcl_emp_salary["PF_LOAN"] = pfl;


                        cmcl = dt.Rows[i]["CAR_MOTOR_LOAN"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["CAR_MOTOR_LOAN"].ToString());
                        dramcl_emp_salary["CAR_MOTOR_LOAN"] = cmcl;

                        asl = dt.Rows[i]["ASSOC_LOAN"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["ASSOC_LOAN"].ToString());
                        dramcl_emp_salary["ASSOC_LOAN"] = asl;


                        osl = dt.Rows[i]["OFF_ASS_SUB"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["OFF_ASS_SUB"].ToString());
                        dramcl_emp_salary["OFF_ASS_SUB"] = osl;



                        sul = dt.Rows[i]["STAFF_UNION_SUB"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["STAFF_UNION_SUB"].ToString());
                        dramcl_emp_salary["STAFF_UNION_SUB"] = sul;


                        stb = dt.Rows[i]["STAFF_BUS"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["STAFF_BUS"].ToString());
                        dramcl_emp_salary["STAFF_BUS"] = stb;



                        emt = dt.Rows[i]["ENTERTAINMENT"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["ENTERTAINMENT"].ToString());

                        dramcl_emp_salary["ENTERTAINMENT"] = emt;

                        if (pntn == 'N')
                        {
                            empbf = 100;

                            dramcl_emp_salary["EMP_BEN_FUND"] = empbf;

                        }
                        else
                        {

                            empbf = basic * .01; // 1% effected from 21 mar, 2019
                            dramcl_emp_salary["EMP_BEN_FUND"] = empbf;

                        }
                        wa = dt.Rows[i]["WASH_ALLOWS"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["WASH_ALLOWS"]);
                        dramcl_emp_salary["WASH_ALLOWS"] = wa;


                        dd = dt.Rows[i]["DEDUCT"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["DEDUCT"]);
                        dramcl_emp_salary["DEDUCT"] = dd;

                        pp = dt.Rows[i]["P_PAY"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["P_PAY"]);
                        dramcl_emp_salary["P_PAY"] = pp;


                        utly = dt.Rows[i]["UTILITIES"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["UTILITIES"].ToString());
                        dramcl_emp_salary["UTILITIES"] = utly;


                        np = dt.Rows[i]["NEWSPAPER"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["NEWSPAPER"].ToString());
                        dramcl_emp_salary["NEWSPAPER"] = np;


                        rstmp = dt.Rows[i]["REV_STAMP"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["REV_STAMP"].ToString());
                        dramcl_emp_salary["REV_STAMP"] = rstmp;


                        tlp = dt.Rows[i]["TELEPHONE"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["TELEPHONE"].ToString());
                        dramcl_emp_salary["TELEPHONE"] = tlp;


                        DataTable dtEmp = getEmpType(dt.Rows[i]["ID"].ToString());

                        //..................................................new block.................
                        //..............................................................................


                        double  monthlyIncomeTax, PreviousIncomeTax;

                        monthlyIncomeTax = dt.Rows[i]["INCOME_TAX"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["INCOME_TAX"].ToString());
                        dramcl_emp_salary["INCOME_TAX"] = monthlyIncomeTax;

                        PreviousIncomeTax = dt.Rows[i]["PREVIOUS_INCOME_TAX"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["PREVIOUS_INCOME_TAX"].ToString());
                        dramcl_emp_salary["PREVIOUS_INCOME_TAX"] = PreviousIncomeTax;





                        gip = dt.Rows[i]["GI_PREM"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["GI_PREM"].ToString());
                        dramcl_emp_salary["GI_PREM"] = gip;


                        others = dt.Rows[i]["OTHERS"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["OTHERS"].ToString());
                        dramcl_emp_salary["OTHERS"] = others;

                        //..........................................AdvanceBlock....................................................

                        tempad = dt.Rows[i]["TEMP_ADVANCE"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["TEMP_ADVANCE"].ToString());
                        List<Advance_initialization_details> advanceInitializationDetails_LIST = lmg.AdvanceInitializationDetails_LIST.FindAll(itm => itm.EMPLOYEE_ID == dt.Rows[i]["ID"].ToString() & itm.LOAN_STATUS == "Active" & itm.ADVANCE_INIT_DET_ACTIVATE == "Y" & itm.SHEDULE_GENARATE == "Y"); ;
                        foreach (Advance_initialization_details advINtDEt in advanceInitializationDetails_LIST)
                        {
                            try
                            {
                                DataTable dtShedule = lmg.Get_Shedule(advINtDEt.DISBURSED_ID, CalDate);
                                if (dtShedule.Rows.Count > 0 && dtShedule != null)
                                {
                                    tempad = Convert.ToDouble(dtShedule.Rows[0]["INSTALLMENT_AMT"].ToString());
                                    dramcl_emp_salary["DISBURSED_ID"] = advINtDEt.DISBURSED_ID;
                                    dramcl_emp_salary["ADVANCE_INIT_ID"] = advINtDEt.ADVANCE_INIT_ID;
                                    dramcl_emp_salary["ADVANCE_SCHEDULE_ID"] = Convert.ToInt32(dtShedule.Rows[0]["ADVANCE_SCHEDULE_ID"].ToString());

                                    string strupdateAdvShedule = "update ADVANCE_SCHEDULE set PAID='Y'  where ADVANCE_INIT_DET_ID='" + advINtDEt.DISBURSED_ID + "'and ADVANCE_INIT__ID='" + advINtDEt.ADVANCE_INIT_ID + "' and ADVANCE_SCHEDULE_ID='" + Convert.ToInt32(dtShedule.Rows[0]["ADVANCE_SCHEDULE_ID"].ToString()) + "'";
                                    int strupdateAdvSheduleNumOfRows = CommonGatewayObj.ExecuteNonQuery(strupdateAdvShedule);
                                }
                                else
                                {
                                    dramcl_emp_salary["DISBURSED_ID"] = 0;
                                    dramcl_emp_salary["ADVANCE_INIT_ID"] = 0;
                                    dramcl_emp_salary["ADVANCE_SCHEDULE_ID"] = 0;
                                }

                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.ToString() + CalDate);
                            }
                          
                        }
                        if (advanceInitializationDetails_LIST.Count == 0)
                        {
                            dramcl_emp_salary["DISBURSED_ID"] = 0;
                            dramcl_emp_salary["ADVANCE_INIT_ID"] = 0;
                            dramcl_emp_salary["ADVANCE_SCHEDULE_ID"] = 0;

                        }
                        dramcl_emp_salary["TEMP_ADVANCE"] = tempad;

                        da = dt.Rows[i]["DEP_ALLOW"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["DEP_ALLOW"].ToString());
                        dramcl_emp_salary["DEP_ALLOW"] = da;


                        cea = dt.Rows[i]["CHILD_EDU_ALLOW"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["CHILD_EDU_ALLOW"].ToString());

                        dramcl_emp_salary["CHILD_EDU_ALLOW"] = cea;

                        area = dt.Rows[i]["AREA"] is DBNull ? 0.0 : Convert.ToDouble(dt.Rows[i]["AREA"].ToString());

                        dramcl_emp_salary["AREA"] = area;

                        ta = emt + utly + tlp + hm;

                        if (pntn == 'N' && spco == 'Y')
                        {
                            td = (ppn + hba + cmcl + asl + osl + sul + stb + empbf + np + rstmp + gip + monthlyIncomeTax + PreviousIncomeTax + dd + tempad + pfl + sppco);
                        }

                        else if (pntn == 'N')
                        {
                            td = (ppn + hba + cmcl + asl + osl + sul + stb + empbf + np + rstmp + gip + monthlyIncomeTax + PreviousIncomeTax + dd + tempad + pfl + pco);
                        }
                        else
                        {
                            td = (pco + pcc + hba + cmcl + stb + empbf + np + rstmp + gip + monthlyIncomeTax + PreviousIncomeTax + dd + tempad + pfl);
                        }

                        dramcl_emp_salary["TOTAL_DEDUCT"] = td;
                        if (pntn == 'N')
                        {
                            gt = (basic + hrnt + ppn + da + cea + ma + ca + pp + others + area);
                            gtwa = (basic + hrnt + ppn + da + cea + ma + ca + pp + others + ta + area);
                        }
                        else
                        {
                            gt = (basic + hrnt + pcc + da + cea + ma + ca + pp + others + wa + area);
                            gtwa = (basic + hrnt + pcc + da + cea + ma + ca + wa + pp + others + ta + area);
                        }

                        dramcl_emp_salary["GROSS_TOTAL"] = gt;
                        dramcl_emp_salary["GROSS_TOTAL_WITH_ALLOW"] = gtwa;

                        np = gt - td;
                        npwa = gtwa - td;

                        dramcl_emp_salary["NET_PAYABLE"] = np;
                        dramcl_emp_salary["NET_PAYABLE_WITH_ALLOW"] = npwa;


                        dramcl_emp_salary["NET_ALLOWANCE"] = ta;
                        try
                        {
                            DateTime dtCaldate = DateTime.ParseExact(CalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            dramcl_emp_salary["CAL_DATE"] = dtCaldate;
                        }
                        catch(Exception exe)
                        {
                            throw new Exception(exe.ToString()+"calDate:"+CalDate);
                        }

                        dtamcl_emp_salary.Rows.Add(dramcl_emp_salary);
                    }

                    catch (Exception ex)
                    {
                        
                        CommonGatewayObj.RollbackTransaction();
                        throw ex;
                    }
                }
            }

            return dtamcl_emp_salary;
        }


      



    

        public string SaveSalary(SalaryDetails salDet)
        {
            string mess = "Success";

            try
            {
                CommonGatewayObj.BeginTransaction();


                Hashtable htSalDet = new Hashtable();


                htSalDet.Add("AUTOID", salDet.AUTOID);
                htSalDet.Add("ID", salDet.ID.ToString());
                htSalDet.Add("BASIC", salDet.BASIC.ToString());
                htSalDet.Add("HOUSE_RENT", salDet.HOUSE_RENT.ToString());
                htSalDet.Add("HB_ADV", salDet.HB_ADV.ToString());
                htSalDet.Add("MED_ALLOW", salDet.MED_ALLOW.ToString());
                htSalDet.Add("HOUSE_MAINT", salDet.HOUSE_MAINT.ToString());
                htSalDet.Add("CONV_ALLOW", salDet.CONV_ALLOW.ToString());
                htSalDet.Add("PF_PN_COR", salDet.PF_PN_COR.ToString());
                htSalDet.Add("PENSION_CONTR_COR", salDet.PENSION_CONTR_COR.ToString());
                htSalDet.Add("PF_CONTR_COR", salDet.PF_CONTR_COR.ToString());
                htSalDet.Add("PF_CONTR_OWN", salDet.PF_CONTR_OWN.ToString());
                htSalDet.Add("PF_LOAN", salDet.PF_LOAN.ToString());
                htSalDet.Add("CAR_MOTOR_LOAN", salDet.CAR_MOTOR_LOAN.ToString());
                htSalDet.Add("ASSOC_LOAN", salDet.ASSOC_LOAN.ToString());
                htSalDet.Add("OFF_ASS_SUB", salDet.OFF_ASS_SUB.ToString());
                htSalDet.Add("STAFF_UNION_SUB", salDet.STAFF_UNION_SUB.ToString());
                htSalDet.Add("STAFF_BUS", salDet.STAFF_BUS.ToString());
                htSalDet.Add("ENTERTAINMENT", salDet.ENTERTAINMENT.ToString());
                htSalDet.Add("EMP_BEN_FUND", salDet.EMP_BEN_FUND);
                htSalDet.Add("WASH_ALLOWS", salDet.WASH_ALLOWS.ToString());
                htSalDet.Add("DEDUCT", salDet.DEDUCT.ToString());
                htSalDet.Add("P_PAY",salDet.P_PAY.ToString());
                htSalDet.Add("UTILITIES",salDet.UTILITIES.ToString());
                htSalDet.Add("NEWSPAPER", salDet.NEWSPAPER.ToString());
                htSalDet.Add("REV_STAMP", salDet.REV_STAMP.ToString());
                htSalDet.Add("TELEPHONE", salDet.TELEPHONE.ToString());
                htSalDet.Add("INCOME_TAX", salDet.INCOME_TAX.ToString());
                htSalDet.Add("PREVIOUS_INCOME_TAX",salDet.PREVIOUS_INCOME_TAX.ToString());
                htSalDet.Add("GI_PREM", salDet.GI_PREM.ToString());
                htSalDet.Add("OTHERS", salDet.OTHERS);
                htSalDet.Add("TEMP_ADVANCE", salDet.TEMP_ADVANCE.ToString());
                htSalDet.Add("DISBURSED_ID", salDet.DISBURSED_ID.ToString());
                htSalDet.Add("ADVANCE_INIT_ID", salDet.ADVANCE_INIT_ID.ToString());
                htSalDet.Add("ADVANCE_SCHEDULE_ID", salDet.ADVANCE_SCHEDULE_ID.ToString());
                htSalDet.Add("DEP_ALLOW", salDet.DEP_ALLOW.ToString());
                htSalDet.Add("CHILD_EDU_ALLOW", salDet.CHILD_EDU_ALLOW.ToString());
                htSalDet.Add("AREA", salDet.AREA.ToString());
                htSalDet.Add("TOTAL_DEDUCT", salDet.TOTAL_DEDUCT.ToString());
                htSalDet.Add("GROSS_TOTAL", salDet.GROSS_TOTAL.ToString());
                htSalDet.Add("GROSS_TOTAL_WITH_ALLOW",salDet.GROSS_TOTAL_WITH_ALLOW.ToString());
                htSalDet.Add("NET_PAYABLE", salDet.NET_PAYABLE.ToString());
                htSalDet.Add("NET_PAYABLE_WITH_ALLOW", salDet.NET_PAYABLE_WITH_ALLOW.ToString());
                htSalDet.Add("NET_ALLOWANCE", salDet.NET_ALLOWANCE.ToString());
                htSalDet.Add("CAL_DATE", salDet.CAL_DATE.ToString());
               
                CommonGatewayObj.Insert(htSalDet, "AMCL_EMP_SALARY");


                CommonGatewayObj.CommitTransaction();
                mess = "Success";
            }
            catch (Exception e)
            {
                CommonGatewayObj.RollbackTransaction();
                mess = "error";
                throw e;
            }

            return mess;

        }


        public bool IsDuplicateSalaryProcess(string calDate)
        {
            try
            {
                DateTime dtcalDate = DateTime.ParseExact(calDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                try
                {

                }
                catch(Exception ep)
                {
                    throw new Exception(ep.ToString() + "SQL:" + "SELECT * FROM AMCL_EMP_SALARY WHERE CAL_DATE='" + dtcalDate + "'");
                }
                DataTable dtDuplicateSalaryProcess = CommonGatewayObj.Select("SELECT * FROM AMCL_EMP_SALARY WHERE CAL_DATE=TO_DATE('" + dtcalDate + "', 'DD-MM-YYYY')");
                if (dtDuplicateSalaryProcess.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable getSalaryDetails(string p1, string p2)
        {
            DataTable dtSalaryinfo = new DataTable();
            var sql = "";
            try
            {
                StringBuilder sb = new StringBuilder();



                sb.Append(" SELECT        EMP_INFO.ID, EMP_INFO.NAME, EMP_INFO.BASIC, AMCL_EMP_SALARY.PF_LOAN, AMCL_EMP_SALARY.HB_ADV, AMCL_EMP_SALARY.CAR_MOTOR_LOAN, ");
                sb.Append(" AMCL_EMP_SALARY.ASSOC_LOAN, AMCL_EMP_SALARY.OFF_ASS_SUB, AMCL_EMP_SALARY.STAFF_UNION_SUB, AMCL_EMP_SALARY.STAFF_BUS, AMCL_EMP_SALARY.MED_ALLOW,");
                sb.Append(" AMCL_EMP_SALARY.NEWSPAPER, AMCL_EMP_SALARY.REV_STAMP, AMCL_EMP_SALARY.OTHERS, AMCL_EMP_SALARY.GI_PREM, AMCL_EMP_SALARY.CONV_ALLOW,");
                sb.Append(" AMCL_EMP_SALARY.TEMP_ADVANCE, AMCL_EMP_SALARY.DEP_ALLOW, AMCL_EMP_SALARY.CHILD_EDU_ALLOW, AMCL_EMP_SALARY.INCOME_TAX, AMCL_EMP_SALARY.PREVIOUS_INCOME_TAX,");
                sb.Append(" AMCL_EMP_SALARY.WASH_ALLOWS, AMCL_EMP_SALARY.CAR_MAINT_ALLOW,AMCL_EMP_SALARY.ASSO_SUB_LOAN,AMCL_EMP_SALARY.DEDUCT,AMCL_EMP_SALARY.P_PAY, GRADE_RLT_ALLOWS.HOUSE_MAINT,");
                sb.Append(" GRADE_RLT_ALLOWS.UTILITIES, GRADE_RLT_ALLOWS.TELEPHONE,GRADE_RLT_ALLOWS.ENTERTAINMENT,EMP_INFO.SPF_CONTR_OWN,EMP_INFO.ISCOMP,EMP_INFO.HBLOAN, AMCL_EMP_SALARY.AREA");
                sb.Append(" FROM            EMP_INFO INNER JOIN ");
                sb.Append(" AMCL_EMP_SALARY ON EMP_INFO.ID = AMCL_EMP_SALARY.ID INNER JOIN ");
                sb.Append(" GRADE_RLT_ALLOWS ON EMP_INFO.GRADE = GRADE_RLT_ALLOWS.GRADE ");
                sb.Append(" WHERE   AMCL_EMP_SALARY.CAL_DATE BETWEEN TO_DATE('"+p1+ "', 'DD-MM-YYYY')  AND TO_DATE('" + p2 + "', 'DD-MM-YYYY') AND EMP_INFO.VALID='Y'  ");
                sb.Append("    ORDER BY EMP_INFO.ID");

                sql = sb.ToString();

                dtSalaryinfo = CommonGatewayObj.Select(sb.ToString());
                return dtSalaryinfo;

            }
            catch (Exception ex)
            {
                throw new Exception("[[["+sql+"]]]"+ ex+"form Date"+ p1+"---"+ "to Date"+p2);
            }

        }

        public void ShowSalary()
        {
            string Query = "Select * From AMCL_EMP_SALARY Where CAL_DATE =(select max(CAL_DATE) from AMCL_EMP_SALARY) ";

            DataTable dt_AMCL_EMP_SALARY = this.CommonGatewayObj.Select(Query);

            empSalaryDelails_LIST = DataTableToList.DataTableToListConvert<Models.SalaryDetails>(dt_AMCL_EMP_SALARY);


        }

        internal DataTable getEmpType(string id)
        {
            DataTable dtSalaryinfo = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                //DateTime caldate = Convert.ToDateTime(p2);
                sb.Append(" SELECT  ID, NAME, SEX,RANK ");
                sb.Append(" FROM EMP_INFO ");
                sb.Append(" WHERE   ID = '" + id + "' ");
                dtSalaryinfo = CommonGatewayObj.Select(sb.ToString());
                return dtSalaryinfo;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}