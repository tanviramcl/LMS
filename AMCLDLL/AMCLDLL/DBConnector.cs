using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMCLDLL;


namespace AMCLDLL
{
    class DBConnector
    {
        private string connectionString = null;

        private OracleConnection sqlConn = null;

        public OracleConnection GetConnection
        {
            get
            {
                return this.sqlConn;
            }
        }

        public DBConnector()
        {
            try
            {
                this.connectionString = ConfigReader.SecurityAnalysis.ToString();
                this.sqlConn = new OracleConnection(this.connectionString);
            }
            catch (Exception exceptionObj)
            {
                throw exceptionObj;
            }
        }
    }
}
