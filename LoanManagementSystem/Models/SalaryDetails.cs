using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class SalaryDetails
    {
        public int AUTOID { get; set; }
        public string ID { get; set; }
        public double BASIC { get; set; }
        public double HOUSE_RENT { get; set; }
        public double HB_ADV { get; set; }
        public double MED_ALLOW { get; set; }
        public double HOUSE_MAINT { get; set; }
        public double CONV_ALLOW { get; set; }
        public double PF_PN_COR { get; set; }
        public double PENSION_CONTR_COR { get; set; }
        public double PF_CONTR_COR { get; set; }
        public double PF_CONTR_OWN { get; set; }
        public double PF_LOAN { get; set; }
        public double CAR_MOTOR_LOAN { get; set; }
        public double ASSOC_LOAN { get; set; }
        public double OFF_ASS_SUB { get; set; }
        public double STAFF_UNION_SUB { get; set; }
        public double STAFF_BUS { get; set; }
        public double ENTERTAINMENT { get; set; }
        public double EMP_BEN_FUND { get; set; }
        public double WASH_ALLOWS { get; set; }
        public double DEDUCT { get; set; }
        public double P_PAY { get; set; }
        public double UTILITIES { get; set; }
        public double NEWSPAPER { get; set; }
        public double REV_STAMP { get; set; }
        public double TELEPHONE { get; set; }
        public double INCOME_TAX { get; set; }
        public double PREVIOUS_INCOME_TAX { get; set; }
        public double GI_PREM { get; set; }
        public double OTHERS { get; set; }
        public double TEMP_ADVANCE { get; set; }
        public int DISBURSED_ID { get; set; }
        public int ADVANCE_INIT_ID { get; set; }
        public int ADVANCE_SCHEDULE_ID { get; set; }
        public double DEP_ALLOW { get; set; }
        public double CHILD_EDU_ALLOW { get; set; }
        public double AREA { get; set; }
        public double TOTAL_DEDUCT { get; set; }
        public double GROSS_TOTAL { get; set; }
        public double GROSS_TOTAL_WITH_ALLOW { get; set; }
        public double NET_PAYABLE { get; set; }
        public double NET_PAYABLE_WITH_ALLOW { get; set; }
        public double NET_ALLOWANCE { get; set; }
        [DataType(DataType.Date)]
        public DateTime CAL_DATE { get; set; }
    }
}