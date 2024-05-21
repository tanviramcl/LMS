using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Text;

/// <summary>
/// Summary description for DropDownList
/// </summary>
public class DropDownList
{
    CommonGateway commonGatewayObj = new CommonGateway();
    
	public DropDownList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetFundName()
    {
        DataTable dtFundName = new DataTable();

        StringBuilder sbMst = new StringBuilder();
        StringBuilder sbOrderBy = new StringBuilder();
        sbOrderBy.Append("");

        sbMst.Append(" SELECT     FUND.*    FROM         INVEST.FUND  ");
        sbMst.Append(" WHERE     IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_CD NOT IN('6','7')  ");
        sbOrderBy.Append(" ORDER BY FUND.F_CD ");

        sbMst.Append(sbOrderBy.ToString());
        dtFundName = commonGatewayObj.Select(sbMst.ToString());

      
        return dtFundName;
    }
    public DataTable FundNameDropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME,F_TYPE, F_CD FROM INVEST.FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL    ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }
    public DataTable FundNameDropDownListOpenEnd()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME,F_TYPE, F_CD FROM INVEST.FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL  AND F_TYPE='OPEN END'  ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }
    public DataTable FundNameDropDownListCLOSEEND()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME,F_TYPE, F_CD FROM INVEST.FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_TYPE='CLOSE END'  ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }
    public DataTable CLOSEENDFundNameDropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT F_NAME, F_CD FROM INVEST.FUND WHERE IS_F_CLOSE IS NULL AND BOID IS NOT NULL AND F_TYPE='CLOSE END' ORDER BY F_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("F_NAME", typeof(string));
        dtFundNameDropDownList.Columns.Add("F_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["F_NAME"] = "--Click Here to Select--";
        dr["F_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["F_NAME"] = dtFundName.Rows[loop]["F_NAME"].ToString();
            dr["F_CD"] = Convert.ToInt32(dtFundName.Rows[loop]["F_CD"]);
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }

    public DataTable UNIT_FundNameDropDownList()//For All Funds
    {
        DataTable dtFundName = commonGatewayObj.Select("SELECT FUND_NM, FUND_CD,FUND_CD_INVEST FROM UNIT.FUND_INFO  ORDER BY FUND_CD");
        DataTable dtFundNameDropDownList = new DataTable();
        dtFundNameDropDownList.Columns.Add("FUND_NM", typeof(string));
        dtFundNameDropDownList.Columns.Add("FUND_CD", typeof(string));
        DataRow dr = dtFundNameDropDownList.NewRow();
        dr["FUND_NM"] = "--Click Here to Select--";
        dr["FUND_CD"] = "0";
        dtFundNameDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtFundName.Rows.Count; loop++)
        {
            dr = dtFundNameDropDownList.NewRow();
            dr["FUND_NM"] = dtFundName.Rows[loop]["FUND_NM"].ToString();
            dr["FUND_CD"] = dtFundName.Rows[loop]["FUND_CD"].ToString();
            dtFundNameDropDownList.Rows.Add(dr);
        }
        return dtFundNameDropDownList;
    }

    public DataTable HowlaDateDropDownList()//Get Howla Date from invest.fund_trans_hb Table
    {
        DataTable dtHowlaDate = commonGatewayObj.Select("SELECT DISTINCT VCH_DT    FROM   INVEST.FUND_TRANS_HB  ORDER BY VCH_DT DESC");
        DataTable dtHowlaDateDropDownList = new DataTable();
        dtHowlaDateDropDownList.Columns.Add("Howla_Date", typeof(string));
        dtHowlaDateDropDownList.Columns.Add("VCH_DT", typeof(string));
        DataRow dr = dtHowlaDateDropDownList.NewRow();
        dr["Howla_Date"] = "--Select--";
        dr["VCH_DT"] = "0";
        dtHowlaDateDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtHowlaDate.Rows.Count; loop++)
        {
            dr = dtHowlaDateDropDownList.NewRow();
            dr["Howla_Date"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["VCH_DT"]).ToString("dd-MMM-yyyy");
            dr["VCH_DT"] = Convert.ToDateTime(dtHowlaDate.Rows[loop]["VCH_DT"]).ToString("dd-MMM-yyyy");
            dtHowlaDateDropDownList.Rows.Add(dr);
        }
        return dtHowlaDateDropDownList;
    }
    public DataTable ServiceNameDropDownList()//For All Funds
    {
        DataTable dtService = commonGatewayObj.Select("SELECT  SERVICE_ID, SERVICE_NAME FROM CUS_SERVICE");
        DataTable dtServiceList = new DataTable();
        dtServiceList.Columns.Add("SERVICE_NAME", typeof(string));
        dtServiceList.Columns.Add("SERVICE_ID", typeof(string));
        DataRow dr = dtServiceList.NewRow();
        dr["SERVICE_NAME"] = "--Click Here to Select--";
        dr["SERVICE_ID"] = "0";
        dtServiceList.Rows.Add(dr);
        for (int loop = 0; loop < dtService.Rows.Count; loop++)
        {
            dr = dtServiceList.NewRow();
            dr["SERVICE_NAME"] = dtService.Rows[loop]["SERVICE_NAME"].ToString();
            dr["SERVICE_ID"] = Convert.ToInt32(dtService.Rows[loop]["SERVICE_ID"]);
            dtServiceList.Rows.Add(dr);
        }
        return dtServiceList;
    }

    public DataTable TEST_MAIL_DROP_DOWNLIST()//For All Funds
    {
        
        DataTable dtServiceList = new DataTable();
        dtServiceList.Columns.Add("EMAIL", typeof(string));
        DataRow dr = dtServiceList.NewRow();
        dr["EMAIL"] = "tnvraiub@gmail.com";
        dtServiceList.Rows.Add(dr);

        //DataRow dr1 = dtServiceList.NewRow();
        //dr1["EMAIL"] = "tanvirjob786@gmail.com";
        //dtServiceList.Rows.Add(dr1);

        DataRow dr2 = dtServiceList.NewRow();
        dr2["EMAIL"] = "sakha5413@gmail.com";
        dtServiceList.Rows.Add(dr2);

        DataRow dr3 = dtServiceList.NewRow();
        dr3["EMAIL"] = "jagonnath.amcl.com.18@gmail.com";
        dtServiceList.Rows.Add(dr3);

        DataRow dr4 = dtServiceList.NewRow();
        dr4["EMAIL"] = "tanvir@icbamcl.com.bd";
        dtServiceList.Rows.Add(dr4);

        DataRow dr5 = dtServiceList.NewRow();
        dr5["EMAIL"] = "jogonnathbd87@gmail.com";
        dtServiceList.Rows.Add(dr5);

        DataRow dr6 = dtServiceList.NewRow();
        dr6["EMAIL"] = "jogonnathbd0808@gmail.com";
        dtServiceList.Rows.Add(dr6);

        DataRow dr7 = dtServiceList.NewRow();
        dr7["EMAIL"] = "fulbabuamcl@gmail.com";
        dtServiceList.Rows.Add(dr7);
        return dtServiceList;
    }

    public DataTable SUBServiceNameDropDownList()//For All Funds
    {
        DataTable dtService = commonGatewayObj.Select("SELECT  SERVICE_SUB_ID, SERVICE_SUB_NAME FROM SERVICE_SUB");
        DataTable dtServiceList = new DataTable();
        dtServiceList.Columns.Add("SERVICE_SUB_NAME", typeof(string));
        dtServiceList.Columns.Add("SERVICE_SUB_ID", typeof(string));
        DataRow dr = dtServiceList.NewRow();
        dr["SERVICE_SUB_NAME"] = "--Click Here to Select--";
        dr["SERVICE_SUB_ID"] = "0";
        dtServiceList.Rows.Add(dr);
        for (int loop = 0; loop < dtService.Rows.Count; loop++)
        {
            dr = dtServiceList.NewRow();
            dr["SERVICE_SUB_NAME"] = dtService.Rows[loop]["SERVICE_SUB_NAME"].ToString();
            dr["SERVICE_SUB_ID"] = Convert.ToInt32(dtService.Rows[loop]["SERVICE_SUB_ID"]);
            dtServiceList.Rows.Add(dr);
        }
        return dtServiceList;
    }

    public DataTable SUBServiceNameDropDownList(string serviceId )//For All Funds
    {

        DataTable dtsubService = commonGatewayObj.Select("SELECT  * FROM SERVICE_SUB  WHERE SERVICE_ID = " + serviceId + "");
        DataTable dtsubServiceList = new DataTable();
        dtsubServiceList.Columns.Add("SERVICE_SUB_NAME", typeof(string));
        dtsubServiceList.Columns.Add("SERVICE_SUB_ID", typeof(string));
        DataRow dr = dtsubServiceList.NewRow();
        dr["SERVICE_SUB_NAME"] = "--Click Here to Select--";
        dr["SERVICE_SUB_ID"] = "0";
        dtsubServiceList.Rows.Add(dr);
        for (int loop = 0; loop < dtsubService.Rows.Count; loop++)
        {
            dr = dtsubServiceList.NewRow();
            dr["SERVICE_SUB_NAME"] = dtsubService.Rows[loop]["SERVICE_SUB_NAME"].ToString();
            dr["SERVICE_SUB_ID"] = Convert.ToInt32(dtsubService.Rows[loop]["SERVICE_SUB_ID"]);
            dtsubServiceList.Rows.Add(dr);
        }
        return dtsubServiceList;
    }

    public DataTable Get_DepsrtmentList()
    {

        DataTable dtDepartmentList = commonGatewayObj.Select("Select * from DEPARTMENT order by DEPARTMENT_ID asc");
        DataTable DepartmentList = new DataTable();
        DepartmentList.Columns.Add("DEPARTMENT_NAME", typeof(string));
        DepartmentList.Columns.Add("DEPARTMENT_ID", typeof(string));
        DataRow dr = DepartmentList.NewRow();
        dr["DEPARTMENT_NAME"] = "--Click Here to Select--";
        dr["DEPARTMENT_ID"] = "0";
        DepartmentList.Rows.Add(dr);
        for (int loop = 0; loop < dtDepartmentList.Rows.Count; loop++)
        {
            dr = DepartmentList.NewRow();
            dr["DEPARTMENT_NAME"] = dtDepartmentList.Rows[loop]["DEPARTMENT_NAME"].ToString();
            dr["DEPARTMENT_ID"] = dtDepartmentList.Rows[loop]["DEPARTMENT_ID"];
            DepartmentList.Rows.Add(dr);
        }
        return DepartmentList;
    }
    public DataTable Get_DESIGNATIONList()
    {

        DataTable dtDESIGNATIONList = commonGatewayObj.Select("Select * from DESIGNATION order by DESIGNATION_ID asc");
        DataTable DESIGNATIONList = new DataTable();
        DESIGNATIONList.Columns.Add("DESIGNATION_NAME", typeof(string));
        DESIGNATIONList.Columns.Add("DESIGNATION_ID", typeof(string));
        DataRow dr = DESIGNATIONList.NewRow();
        dr["DESIGNATION_NAME"] = "--Click Here to Select--";
        dr["DESIGNATION_ID"] = "0";
        DESIGNATIONList.Rows.Add(dr);
        for (int loop = 0; loop < dtDESIGNATIONList.Rows.Count; loop++)
        {
            dr = DESIGNATIONList.NewRow();
            dr["DESIGNATION_NAME"] = dtDESIGNATIONList.Rows[loop]["DESIGNATION_NAME"].ToString();
            dr["DESIGNATION_ID"] = dtDESIGNATIONList.Rows[loop]["DESIGNATION_ID"];
            DESIGNATIONList.Rows.Add(dr);
        }
        return DESIGNATIONList;
    }
    public DataTable Get_EmployeeList()
    {

        DataTable dtEmpList = commonGatewayObj.Select("Select * from EMP_INFO order by EMP_ID asc");
        DataTable EmpList = new DataTable();
        EmpList.Columns.Add("EMPLOYEE_NAME", typeof(string));
        EmpList.Columns.Add("EMP_ID", typeof(string));
        DataRow dr = EmpList.NewRow();
        dr["EMPLOYEE_NAME"] = "--Click Here to Select--";
        dr["EMP_ID"] = "0";
        EmpList.Rows.Add(dr);
        for (int loop = 0; loop < dtEmpList.Rows.Count; loop++)
        {
            dr = EmpList.NewRow();
            dr["EMPLOYEE_NAME"] = dtEmpList.Rows[loop]["EMPLOYEE_NAME"].ToString();
            dr["EMP_ID"] = dtEmpList.Rows[loop]["EMP_ID"];
            EmpList.Rows.Add(dr);
        }
        return EmpList;
    }
    public DataTable Get_ComplainList()
    {

        DataTable dtComplainList = commonGatewayObj.Select("Select * from COMPLAIN_REGISTER order by COMPLAIN_ID asc");
        DataTable ComplainList = new DataTable();
        ComplainList.Columns.Add("COMPLAIN_SUBJECT", typeof(string));
        ComplainList.Columns.Add("COMPLAIN_ID", typeof(string));
        DataRow dr = ComplainList.NewRow();
        dr["COMPLAIN_SUBJECT"] = "--Click Here to Select--";
        dr["COMPLAIN_ID"] = "0";
        ComplainList.Rows.Add(dr);
        for (int loop = 0; loop < dtComplainList.Rows.Count; loop++)
        {
            dr = ComplainList.NewRow();
            dr["COMPLAIN_SUBJECT"] = dtComplainList.Rows[loop]["COMPLAIN_SUBJECT"].ToString();
            dr["COMPLAIN_ID"] = dtComplainList.Rows[loop]["COMPLAIN_ID"];
            ComplainList.Rows.Add(dr);
        }
        return ComplainList;
    }
    public DataTable Get_ComplainCloseList()
    {

        DataTable dtComplainList = commonGatewayObj.Select("Select a.*,b.COMPLAIN_ACTION_ID,b.COMMENTS,b.ASSIGN_BY,b.ASSIGN_DATE,b.CLOSE_DATE,b.EMP_ID from COMPLAIN_REGISTER  a  INNER JOIN COMPLAIN_ACTION b ON a.COMPLAIN_ID = b.COMPLAIN_ID order by a.COMPLAIN_ID asc");
        DataTable ComplainList = new DataTable();
        ComplainList.Columns.Add("COMPLAIN_SUBJECT", typeof(string));
        ComplainList.Columns.Add("COMPLAIN_ID", typeof(string));
        DataRow dr = ComplainList.NewRow();
        dr["COMPLAIN_SUBJECT"] = "--Click Here to Select--";
        dr["COMPLAIN_ID"] = "0";
        ComplainList.Rows.Add(dr);
        for (int loop = 0; loop < dtComplainList.Rows.Count; loop++)
        {
            dr = ComplainList.NewRow();
            dr["COMPLAIN_SUBJECT"] = dtComplainList.Rows[loop]["COMPLAIN_SUBJECT"].ToString();
            dr["COMPLAIN_ID"] = dtComplainList.Rows[loop]["COMPLAIN_ID"];
            ComplainList.Rows.Add(dr);
        }
        return ComplainList;
    }



    public DataTable BRANCHNameDropDownList()
    {

        DataTable dtsubService = commonGatewayObj.Select("SELECT  BR_CD as BRANCH_ID,BR_NM as BRANCH_NAME FROM UNIT.BRANCH_INFO ORDER BY BR_CD ");
        DataTable dtsubServiceList = new DataTable();
        dtsubServiceList.Columns.Add("BRANCH_NAME", typeof(string));
        dtsubServiceList.Columns.Add("BRANCH_ID", typeof(string));
        DataRow dr = dtsubServiceList.NewRow();
        dr["BRANCH_NAME"] = "--Click Here to Select--";
        dr["BRANCH_ID"] = "0";
        dtsubServiceList.Rows.Add(dr);
        for (int loop = 0; loop < dtsubService.Rows.Count; loop++)
        {
            dr = dtsubServiceList.NewRow();
            dr["BRANCH_NAME"] = dtsubService.Rows[loop]["BRANCH_NAME"].ToString();
            dr["BRANCH_ID"] = dtsubService.Rows[loop]["BRANCH_ID"];
            dtsubServiceList.Rows.Add(dr);
        }
        return dtsubServiceList;
    }

    public DataTable GetUserINFOByID(string reg_no,string BO,string Folio, string branchNo, string fund )
    {
        //string strdESFromempQuery = "SELECT U_MASTER.*,U_JHOLDER.* FROM U_JHOLDER RIGHT OUTER JOIN U_MASTER ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR "+
        //    "AND U_JHOLDER.REG_NO = U_MASTER.REG_NO WHERE U_MASTER.REG_BK='" + fund + "' AND U_MASTER.REG_BR='" + branchNo + "'";
        // DataTable dtFromUserList = commonGatewayObj.Select(strdESFromempQuery);
        DataTable dtRegInfo = new DataTable();
        StringBuilder sbMaseter = new StringBuilder();
        sbMaseter.Append("SELECT U_MASTER.*,U_JHOLDER.* FROM U_JHOLDER RIGHT OUTER JOIN U_MASTER ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO WHERE U_MASTER.REG_BK='" +fund + "' AND U_MASTER.REG_BR='" + branchNo + "'");
        bool flag = true;
        if (reg_no != "")
        {
            sbMaseter.Append(" AND U_MASTER.REG_NO=" + Convert.ToInt32(reg_no));
        }
        else if (BO != "")
        {
            sbMaseter.Append(" AND U_MASTER.BO='" + BO + "'");
        }
        else if (Folio != "")
        {
            sbMaseter.Append(" AND U_MASTER.FOLIO_NO='" + Folio + "'");
        }
        else
        {
            flag = false;
        }
        if (flag)
        {
            dtRegInfo = this.commonGatewayObj.Select(sbMaseter.ToString());
        }





        return dtRegInfo;
    }

    public DataTable GetUserINF(string reg_no, string branchNo, string fund)
    {
        //string strdESFromempQuery = "SELECT U_MASTER.*,U_JHOLDER.* FROM U_JHOLDER RIGHT OUTER JOIN U_MASTER ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR "+
        //    "AND U_JHOLDER.REG_NO = U_MASTER.REG_NO WHERE U_MASTER.REG_BK='" + fund + "' AND U_MASTER.REG_BR='" + branchNo + "'";
        // DataTable dtFromUserList = commonGatewayObj.Select(strdESFromempQuery);
        DataTable dtRegInfo = new DataTable();
        StringBuilder sbMaseter = new StringBuilder();
        sbMaseter.Append("SELECT U_MASTER.*,U_JHOLDER.* FROM U_JHOLDER RIGHT OUTER JOIN U_MASTER ON U_JHOLDER.REG_BK = U_MASTER.REG_BK AND U_JHOLDER.REG_BR = U_MASTER.REG_BR AND U_JHOLDER.REG_NO = U_MASTER.REG_NO WHERE U_MASTER.REG_BK='" + fund + "' AND U_MASTER.REG_BR='" + branchNo + "'");
        bool flag = true;
        if (reg_no != "")
        {
            sbMaseter.Append(" AND U_MASTER.REG_NO=" + Convert.ToInt32(reg_no));
        }
        //else if (unitRegObj.BO != "")
        //{
        //    sbMaseter.Append(" AND U_MASTER.BO='" + unitRegObj.BO + "'");
        //}
        //else if (unitRegObj.Folio != "")
        //{
        //    sbMaseter.Append(" AND U_MASTER.FOLIO_NO='" + unitRegObj.Folio + "'");
        //}
        else
        {
            flag = false;
        }
        if (flag)
        {
            dtRegInfo = this.commonGatewayObj.Select(sbMaseter.ToString());
        }





        return dtRegInfo;
    }


    //........................................//

    public DataTable dtFillBloodGroupName()
    {
        DataTable dtBloodGroupName = commonGatewayObj.Select("SELECT BG_CD , BG_NAME FROM BLOODGROUP ORDER BY BG_NAME");
        DataTable dtBloodGroupNameDropDown = new DataTable();
        dtBloodGroupNameDropDown.Columns.Add("BG_CD", typeof(string));
        dtBloodGroupNameDropDown.Columns.Add("BG_NAME", typeof(string));

        DataRow drBloodGroupNameDropDown = dtBloodGroupNameDropDown.NewRow();
        drBloodGroupNameDropDown["BG_NAME"] = "--Select Blood Group--- ";
        drBloodGroupNameDropDown["BG_CD"] = "0";
        dtBloodGroupNameDropDown.Rows.Add(drBloodGroupNameDropDown);
        for (int loop = 0; loop < dtBloodGroupName.Rows.Count; loop++)
        {
            drBloodGroupNameDropDown = dtBloodGroupNameDropDown.NewRow();
            drBloodGroupNameDropDown["BG_NAME"] = dtBloodGroupName.Rows[loop]["BG_NAME"].ToString();
            drBloodGroupNameDropDown["BG_CD"] = dtBloodGroupName.Rows[loop]["BG_CD"].ToString();
            dtBloodGroupNameDropDown.Rows.Add(drBloodGroupNameDropDown);
        }

        return dtBloodGroupNameDropDown;
    }



    public DataTable dtFillDistrictName()
    {
        DataTable dtDistrictName = commonGatewayObj.Select("SELECT DISTRICTCODE , DISTRICTNAME FROM DISTRICT ORDER BY DISTRICTNAME");
        DataTable dtDistrictNameDropDown = new DataTable();
        dtDistrictNameDropDown.Columns.Add("DISTRICTCODE", typeof(string));
        dtDistrictNameDropDown.Columns.Add("DISTRICTNAME", typeof(string));

        DataRow drDistrictNameDropDown = dtDistrictNameDropDown.NewRow();
        drDistrictNameDropDown["DISTRICTNAME"] = "--Select District Name--- ";
        drDistrictNameDropDown["DISTRICTCODE"] = "0";
        dtDistrictNameDropDown.Rows.Add(drDistrictNameDropDown);
        for (int loop = 0; loop < dtDistrictName.Rows.Count; loop++)
        {
            drDistrictNameDropDown = dtDistrictNameDropDown.NewRow();
            drDistrictNameDropDown["DISTRICTNAME"] = dtDistrictName.Rows[loop]["DISTRICTNAME"].ToString();
            drDistrictNameDropDown["DISTRICTCODE"] = dtDistrictName.Rows[loop]["DISTRICTCODE"].ToString();
            dtDistrictNameDropDown.Rows.Add(drDistrictNameDropDown);
        }

        return dtDistrictNameDropDown;
    }

    public DataTable dtFillDepartmentName()
    {
        DataTable dtDepartmentName = commonGatewayObj.Select("SELECT DEPARTMENTCODE , DEPARTMENTNAME FROM DEPARTMENT ORDER BY DEPARTMENTNAME");
        DataTable dtDepartmentNameDropDown = new DataTable();
        dtDepartmentNameDropDown.Columns.Add("DEPARTMENTCODE", typeof(string));
        dtDepartmentNameDropDown.Columns.Add("DEPARTMENTNAME", typeof(string));

        DataRow drDepartmentNameDropDown = dtDepartmentNameDropDown.NewRow();
        drDepartmentNameDropDown["DEPARTMENTNAME"] = "--Select Department Name--- ";
        drDepartmentNameDropDown["DEPARTMENTCODE"] = "0";
        dtDepartmentNameDropDown.Rows.Add(drDepartmentNameDropDown);
        for (int loop = 0; loop < dtDepartmentName.Rows.Count; loop++)
        {
            drDepartmentNameDropDown = dtDepartmentNameDropDown.NewRow();
            drDepartmentNameDropDown["DEPARTMENTNAME"] = dtDepartmentName.Rows[loop]["DEPARTMENTNAME"].ToString();
            drDepartmentNameDropDown["DEPARTMENTCODE"] = dtDepartmentName.Rows[loop]["DEPARTMENTCODE"].ToString();
            dtDepartmentNameDropDown.Rows.Add(drDepartmentNameDropDown);
        }

        return dtDepartmentNameDropDown;
    }

    public DataTable dtFillDesignationName()
    {
        DataTable dtDesignationName = commonGatewayObj.Select("SELECT DESIGNATIONCODE ,DESIGNATIONNAME FROM DESIGNATION ORDER BY DESIGNATIONNAME");
        DataTable dtDesignationNameDropDown = new DataTable();
        dtDesignationNameDropDown.Columns.Add("DESIGNATIONCODE", typeof(string));
        dtDesignationNameDropDown.Columns.Add("DESIGNATIONNAME", typeof(string));

        DataRow drDesignationNameDropDown = dtDesignationNameDropDown.NewRow();
        drDesignationNameDropDown["DESIGNATIONNAME"] = "--Select Designation Name--- ";
        drDesignationNameDropDown["DESIGNATIONCODE"] = "0";
        dtDesignationNameDropDown.Rows.Add(drDesignationNameDropDown);
        for (int loop = 0; loop < dtDesignationName.Rows.Count; loop++)
        {
            drDesignationNameDropDown = dtDesignationNameDropDown.NewRow();
            drDesignationNameDropDown["DESIGNATIONNAME"] = dtDesignationName.Rows[loop]["DESIGNATIONNAME"].ToString();
            drDesignationNameDropDown["DESIGNATIONCODE"] = dtDesignationName.Rows[loop]["DESIGNATIONCODE"].ToString();
            dtDesignationNameDropDown.Rows.Add(drDesignationNameDropDown);
        }

        return dtDesignationNameDropDown;
    }

    public DataTable dtFillUpozilaName(int districtCode)
    {
        DataTable dtUpozilaName = commonGatewayObj.Select("SELECT UPOZILACODE , UPOZILANAME FROM UPOZILA where DISTRICTCODE=" + districtCode + " ORDER BY UPOZILANAME");
        DataTable dtUpozilakNameDropDown = new DataTable();
        dtUpozilakNameDropDown.Columns.Add("UPOZILACODE", typeof(string));
        dtUpozilakNameDropDown.Columns.Add("UPOZILANAME", typeof(string));

        DataRow drUpozilaNameDropDown = dtUpozilakNameDropDown.NewRow();
        drUpozilaNameDropDown["UPOZILANAME"] = "--Select Upozila--- ";
        drUpozilaNameDropDown["UPOZILACODE"] = "0";
        dtUpozilakNameDropDown.Rows.Add(drUpozilaNameDropDown);
        for (int loop = 0; loop < dtUpozilaName.Rows.Count; loop++)
        {
            drUpozilaNameDropDown = dtUpozilakNameDropDown.NewRow();
            drUpozilaNameDropDown["UPOZILANAME"] = dtUpozilaName.Rows[loop]["UPOZILANAME"].ToString();
            drUpozilaNameDropDown["UPOZILACODE"] = dtUpozilaName.Rows[loop]["UPOZILACODE"].ToString();
            dtUpozilakNameDropDown.Rows.Add(drUpozilaNameDropDown);
        }

        return dtUpozilakNameDropDown;
    }

    public DataTable dtFillEduSubject()
    {
        DataTable dtEduSubject = commonGatewayObj.Select("SELECT ID, SUBJECT_NM FROM EDU_SUBJECT ORDER BY SUBJECT_NM");
        DataTable dtEduSubjectDropDown = new DataTable();
        dtEduSubjectDropDown.Columns.Add("ID", typeof(string));
        dtEduSubjectDropDown.Columns.Add("SUBJECT_NM", typeof(string));

        DataRow drEduSubjectDropDown = dtEduSubjectDropDown.NewRow();
        drEduSubjectDropDown["SUBJECT_NM"] = "--Select Subject--- ";
        drEduSubjectDropDown["ID"] = "0";
        dtEduSubjectDropDown.Rows.Add(drEduSubjectDropDown);
        for (int loop = 0; loop < dtEduSubject.Rows.Count; loop++)
        {
            drEduSubjectDropDown = dtEduSubjectDropDown.NewRow();
            drEduSubjectDropDown["SUBJECT_NM"] = dtEduSubject.Rows[loop]["SUBJECT_NM"].ToString();
            drEduSubjectDropDown["ID"] = dtEduSubject.Rows[loop]["ID"].ToString();
            dtEduSubjectDropDown.Rows.Add(drEduSubjectDropDown);
        }

        return dtEduSubjectDropDown;
    }
    public DataTable dtFillDesignation()
    {
        DataTable dtDesignationName = commonGatewayObj.Select("SELECT ID ,NAME FROM EMP_DESIGNATION ORDER BY NAME");
        DataTable dtDesignationNameDropDown = new DataTable();
        dtDesignationNameDropDown.Columns.Add("ID", typeof(string));
        dtDesignationNameDropDown.Columns.Add("NAME", typeof(string));

        DataRow drDesignationNameDropDown = dtDesignationNameDropDown.NewRow();
        drDesignationNameDropDown["NAME"] = "--Select Designation Name--- ";
        drDesignationNameDropDown["ID"] = "0";
        dtDesignationNameDropDown.Rows.Add(drDesignationNameDropDown);
        for (int loop = 0; loop < dtDesignationName.Rows.Count; loop++)
        {
            drDesignationNameDropDown = dtDesignationNameDropDown.NewRow();
            drDesignationNameDropDown["NAME"] = dtDesignationName.Rows[loop]["NAME"].ToString();
            drDesignationNameDropDown["ID"] = dtDesignationName.Rows[loop]["ID"].ToString();
            dtDesignationNameDropDown.Rows.Add(drDesignationNameDropDown);
        }

        return dtDesignationNameDropDown;
    }

    public DataTable MonthlyBankAdviceDropDownList()
    {

        DataTable dtMonthOfBankAdvice = commonGatewayObj.Select("SELECT  CAL_DATE, TO_CHAR(CAL_DATE, 'MONTH,YYYY') AS MONTH_OF_BANK_ADVICE FROM AMCL_EMP_SALARY GROUP BY CAL_DATE ORDER BY CAL_DATE DESC");
        DataTable dtMonthOfBankAdviceDropDownList = new DataTable();
        dtMonthOfBankAdviceDropDownList.Columns.Add("MONTH_OF_BANK_ADVICE", typeof(string));
        dtMonthOfBankAdviceDropDownList.Columns.Add("CAL_DATE", typeof(string));
        DataRow dr = dtMonthOfBankAdviceDropDownList.NewRow();
        dr["MONTH_OF_BANK_ADVICE"] = "--Select--";
        dr["CAL_DATE"] = "0";
        dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtMonthOfBankAdvice.Rows.Count; loop++)
        {
            dr = dtMonthOfBankAdviceDropDownList.NewRow();
            dr["MONTH_OF_BANK_ADVICE"] = dtMonthOfBankAdvice.Rows[loop]["MONTH_OF_BANK_ADVICE"].ToString();
            dr["CAL_DATE"] = Convert.ToString(dtMonthOfBankAdvice.Rows[loop]["CAL_DATE"]);
            dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        }
        return dtMonthOfBankAdviceDropDownList;
    }

    public DataTable PLoanEmployeeList()
    {
        DataTable dtPLoanEmpList = commonGatewayObj.Select("SELECT  EMP_INFO.ID, EMP_INFO.NAME  FROM  EMP_INFO, P_LOAN_INFO WHERE EMP_INFO.ID = P_LOAN_INFO.EMP_ID AND (P_LOAN_INFO.IS_ACTIVE = 'Y')");
        DataTable dtEmpIdList = new DataTable();
        dtEmpIdList.Columns.Add("NAME", typeof(string));
        dtEmpIdList.Columns.Add("ID", typeof(string));
        DataRow dr = dtEmpIdList.NewRow();
        dr["NAME"] = "--Select--";
        dr["ID"] = "0";
        dtEmpIdList.Rows.Add(dr);
        for (int loop = 0; loop < dtPLoanEmpList.Rows.Count; loop++)
        {
            dr = dtEmpIdList.NewRow();
            dr["NAME"] = dtPLoanEmpList.Rows[loop]["NAME"].ToString();
            dr["ID"] = Convert.ToString(dtPLoanEmpList.Rows[loop]["ID"]);
            dtEmpIdList.Rows.Add(dr);
        }
        return dtEmpIdList;
    }


    public DataTable EmpDropdownList()
    {

        DataTable dtEmpList = commonGatewayObj.Select("SELECT ID, NAME as EMPNAME FROM  EMP_INFO WHERE (VALID = 'Y') ORDER BY RANK, SENIORITY");
        DataTable dtEmpIdList = new DataTable();
        dtEmpIdList.Columns.Add("EMPNAME", typeof(string));
        dtEmpIdList.Columns.Add("ID", typeof(string));
        DataRow dr = dtEmpIdList.NewRow();
        dr["EMPNAME"] = "--Select--";
        dr["ID"] = "0";
        dtEmpIdList.Rows.Add(dr);
        for (int loop = 0; loop < dtEmpList.Rows.Count; loop++)
        {
            dr = dtEmpIdList.NewRow();
            dr["EMPNAME"] = dtEmpList.Rows[loop]["EMPNAME"].ToString();
            dr["ID"] = Convert.ToString(dtEmpList.Rows[loop]["ID"]);
            dtEmpIdList.Rows.Add(dr);
        }
        return dtEmpIdList;
    }

    public DataTable monthOfDeductionDropDownList()
    {
        DataTable dtMonthOfBankAdvice = commonGatewayObj.Select("SELECT  CAL_DATE, TO_CHAR(CAL_DATE, 'MONTH,YYYY') AS MONTH_OF_BANK_ADVICE FROM AMCL_EMP_SALARY GROUP BY CAL_DATE ORDER BY CAL_DATE DESC");
        DataTable dtMonthOfBankAdviceDropDownList = new DataTable();
        dtMonthOfBankAdviceDropDownList.Columns.Add("MONTH_OF_BANK_ADVICE", typeof(string));
        dtMonthOfBankAdviceDropDownList.Columns.Add("CAL_DATE", typeof(string));
        DataRow dr = dtMonthOfBankAdviceDropDownList.NewRow();
        dr["MONTH_OF_BANK_ADVICE"] = "--Select--";
        dr["CAL_DATE"] = "0";
        dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        for (int loop = 0; loop < dtMonthOfBankAdvice.Rows.Count; loop++)
        {
            dr = dtMonthOfBankAdviceDropDownList.NewRow();
            dr["MONTH_OF_BANK_ADVICE"] = dtMonthOfBankAdvice.Rows[loop]["MONTH_OF_BANK_ADVICE"].ToString();
            dr["CAL_DATE"] = Convert.ToString(dtMonthOfBankAdvice.Rows[loop]["CAL_DATE"]);
            dtMonthOfBankAdviceDropDownList.Rows.Add(dr);
        }
        return dtMonthOfBankAdviceDropDownList;
    }

    public object dtFY()
    {
        DataTable dtList = commonGatewayObj.Select("SELECT DISTINCT FY FROM PF_BASIC ORDER BY FY DESC");
        DataTable dtDropDown = new DataTable();
        dtDropDown.Columns.Add("FY", typeof(string));
        dtDropDown.Columns.Add("FY_VALUE", typeof(string));

        DataRow drdtDropDown = dtDropDown.NewRow();

        drdtDropDown["FY"] = "--Select FY--- ";
        drdtDropDown["FY_VALUE"] = "0";
        dtDropDown.Rows.Add(drdtDropDown);
        for (int loop = 0; loop < dtList.Rows.Count; loop++)
        {
            drdtDropDown = dtDropDown.NewRow();
            drdtDropDown["FY"] = dtList.Rows[loop]["FY"].ToString();
            drdtDropDown["FY_VALUE"] = dtList.Rows[loop]["FY"].ToString();
            dtDropDown.Rows.Add(drdtDropDown);
        }
        return dtDropDown;
    }

}
