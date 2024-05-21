using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class Advance_rescheduled_init
    {
        public int RESCHEDULED_ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime RESCHEDULED_DATE { get; set; }
        public double INT_RATE { get; set; }
        public double INT_RATE_PREVIOUS { get; set; }
        public double INSTALLMENT_AMT { get; set; }
        public double TOTAL_PRINCIPAL_AMT_PAID { get; set; }
        public double TOTAL_INTEREST_AMT_PAID { get; set; }
        public int NO_OF_INSTALLMENT { get; set; }
        [DataType(DataType.Date)]
        public DateTime RE_SCHEDULE_GENARATE_DATE { get; set; }
        public string RE_SHEDULE_GENARATE { get; set; }
        public int DISBURSED_ID { get; set; }


    }
}