using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class Advance_schedule
    {
        public int ADVANCE_SCHEDULE_ID { get; set; }
        public string SCHEDULE_DATE { get; set; }
        public double INSTALLMENT_AMT { get; set; }
        public double INTEREST_RATE { get; set; }
        public double INTEREST_PAID { get; set; }
        public double CUMULATIVE_INTEREST { get; set; }
        public double PRINCIPAL_PAID { get; set; }
        public double BALANCE { get; set; }
        public double BALANCEWITHINTEREST { get; set; }
        public string TYPE { get; set; }
        public int ADVANCE_INIT_DET_ID { get; set; }
        public int ADVANCE_INIT__ID { get; set; }
       
    }
}