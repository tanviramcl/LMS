using AMCLBL;
using LoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;


namespace LoanManagementSystem.Loan.layer
{
    
    public class EmployeeManagment
    {
        CommonGateway commonGatewayObj = new CommonGateway();
        DataTableToList dtnew = new DataTableToList();
        public List<Models.Employee> Employee_LIST { get; set; }

        public EmployeeManagment()
        {
            Get_All_employee();
        }
        public void Get_All_employee()
        {
            string Query = " SELECT * FROM EMP_INFO  order by ID,AUTOID,RANK,SENIORITY ";

            DataTable dtallemployee = this.commonGatewayObj.Select(Query);

            Employee_LIST = DataTableToList.DataTableToListConvert<Models.Employee>(dtallemployee);

            
        }

        public string SaveEmployee(Employee employee)
        {
            string mess = "Success";
            StringBuilder sqlMaster = new StringBuilder();
            sqlMaster.Append("UPDATE EMP_INFO SET TEMP_ADVANCE = '" + employee.TEMP_ADVANCE + "',VALID='"+employee.VALID+ "',BASIC='"+employee.BASIC+ "' ,BKACNO='"+ employee.BKACNO + "' WHERE ID = '" + employee.ID + "' and AUTOID="+ employee.AUTOID+"");

            try
            {
                this.commonGatewayObj.ExecuteNonQuery(sqlMaster.ToString());
            }
            catch (Exception e)
            {
                mess = "error";
            }
            
            return mess;
        }




    }
}