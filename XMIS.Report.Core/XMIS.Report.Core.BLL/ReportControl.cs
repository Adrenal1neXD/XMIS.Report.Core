using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Core.DAL;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace XMIS.Report.Core.BLL
{
    public class ReportControl
    {
        private readonly DataConfiguration config = new DataConfiguration();
        private IDataAccessManager dam;
        private ExcelAccessManager eam = new ExcelAccessManager();

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
            DataTable xlsdata = null;
                 
            //--get data from excel 
            //open excel
            try 
            { 
                this.eam.OpenXmlFile(this.config.SrcPath + "form16x.xls");
                xlsdata = this.eam.ReadFormXmlFile();
            }
            catch (Exception ex)
            {
            }
             
            //send datatable to ActionCenter and get actionList & params for it
            var ac = new ActionCenter();
            var actionCollection = ac.Handle(xlsdata);
            object result = null;
            foreach (KeyValuePair<ActionName, string[]> action in actionCollection)
            {
                //Key - name, Value - params
                result = this.StartAction(action.Key, action.Value);
                if (result != null)
                    //////////////////////////////////////////
                    Console.WriteLine(result);
            }
        }

        private dynamic StartAction(ActionName action, string[] args)
        {
            //returnes result of action
            switch (action)
            {
                case ActionName.Select:
                    //--get data from db
                    string column = "*";
                    if (args.Length >= 2)
                    column = args[1];
                    DataTable dbdata = this.dam.DoQuery(string.Format("select {1} from {0}", args[0], column));
                    if (dbdata == null)
                        return null;
                    var result = (from dataRow in dbdata.AsEnumerable()
                                  from obj in dataRow.ItemArray
                                  select obj.ToString()).ToList();
                    return result.ToArray();

                case ActionName.Max:
                    return null;

                case ActionName.Sum:
                    return null;

                default:
                    return null;
            }
        }
    }
}
