using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Core.DAL;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace XMIS.Report.Core.BLL
{
    public class ReportControl
    {
        private readonly DataConfiguration config = new DataConfiguration();
        private IDataAccessManager dam;

        public ReportControl()
        {
            //getting type of db connection
            if (this.config.DbConnectionType == typeof(SqlConnection))
                this.dam = new DataAccessManager<SqlConnection>();
            else
                this.dam = new DataAccessManager<OleDbConnection>();

            try
            {
                //connecting to db
                this.dam.Connect(this.config.DBConnectionPath);
            }
            catch (Exception ex)
            {

            }
        }

        public void WriteToExcel()
        {
            //write data to excel file
        }

        public void ReadData()
        {
            //get data from db
            var data = this.dam.DoQuery(string.Format("select * from {0}", "Person"));
        }

        public void ShowData()
        {  
            //open window

        }
    }
}
