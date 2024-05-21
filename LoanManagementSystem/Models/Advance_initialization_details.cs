using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class Advance_initialization_details
    {
        public int DISBURSED_ID { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public string NAME { get; set; }
        public string RANK { get; set; }
        public double DISBURSED_AMT { get; set; }
        [DataType(DataType.Date)]
        public DateTime DISBURSED_DATE { get; set; }
        public double INSTALLMENT_AMT { get; set; }
        public double TOTAL_PRINCIPAL_AMT { get; set; }
        public double TOTAL_INTEREST_AMT { get; set; }
        public double INT_RATE { get; set; }
        public double INT_RATE_PREVIOUS { get; set; }
        public int NO_OF_INSTALLMENT { get; set; }
        public int NO_OF_YEAR { get; set; }
        public string TIMES_OF_LOAN { get; set; }
        public string LOAN_STATUS { get; set; }
        public string ADVANCE_INIT_DET_ACTIVATE { get; set; }
        public int ADVANCE_INIT_ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime ADVANVE_INIT_DET_DATE { get; set; }
        [DataType(DataType.Date)]
        public DateTime SCHEDULE_GENARATE_DATE { get; set; }
        public string SHEDULE_GENARATE { get; set; }
        public string RESCHEDULED { get; set; }
        public string LOAN_ID { get; set; }
        public string CLOSE_TYPE { get; set; }
        [DataType(DataType.Date)]
        public DateTime CLOSE_DATE { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime DISBURSED_DATE_FORM { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime DISBURSED_DATE_TO { get; set; }


    }
}