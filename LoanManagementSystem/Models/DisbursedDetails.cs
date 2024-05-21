using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class DisbursedDetails
    {
        [DataType(DataType.Date)]
        public DateTime DISBURSED_DATE_FORM { get; set; }
        [DataType(DataType.Date)]
        public DateTime DISBURSED_DATE_TO { get; set; }
        public string LOAN_STATUS { get; set; }
    }
}