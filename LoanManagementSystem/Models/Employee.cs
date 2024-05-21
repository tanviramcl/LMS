using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanManagementSystem.Models
{
    public class Employee
    {
        public string ID { get; set; }
        public string RANK { get; set; }
        public string NAME { get; set; }
        public string ADDR { get; set; }
        public string ADDR2 { get; set; }
        public string SEX { get; set; }
        public String BDATE { get; set; }
        public String TEL { get; set; }
        [DataType(DataType.Date)]
        public DateTime JDATE { get; set; }
        public decimal BASIC { get; set; }
        public String SCHEME { get; set; }
        public string ISCOMP { get; set; }
        public String PFLOAN { get; set; }
        public String HBLOAN { get; set; }
        public String CMCLOAN { get; set; }
        public String ASSOLOAN { get; set; }
        public String BKACNO { get; set; }
        public String BKNAME { get; set; }
        public String OLD_BASIC { get; set; }
        public String VALID { get; set; }
        public String TEMP_ADVANCE { get; set; }
        public int SENIORITY { get; set; }
        public int ICB_ID { get; set; }
        public String OLD_BKACNO { get; set; }
        public String BINC { get; set; }
        public int NO_CHILD { get; set; }
        public int MACHIN_ID { get; set; }
        public int DESIG_ID { get; set; }
        public int DEPT_ID { get; set; }
        public string EMP_ID { get; set; }
        public int GRADE { get; set; }
        public int AUTOID { get; set; }


    }
}