using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class AdvanceInitialization
    {
        public int ADVANCE_INIT_ID { get; set; }
        public string ADVANVE_INIT_NAME { get; set; }
        [DataType(DataType.Date)]
        public DateTime EMPLOYEE_JOIN_DATE { get; set; }
        [DataType(DataType.Date)]
        public DateTime EMPLOYEE_RETIRE_DATE { get; set; }
        public string EMPLOYEE_BASIC { get; set; }
        public String ADVANVE_ACTIVATE { get; set; }
        public int ADVANCE_ID { get; set; }
        public string BANK_ACCOUNT { get; set; }
        public String EMPLOYEE_ID { get; set; }
        public String OFFICE_ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime AVD_INT_DATE { get; set; }

    }
}