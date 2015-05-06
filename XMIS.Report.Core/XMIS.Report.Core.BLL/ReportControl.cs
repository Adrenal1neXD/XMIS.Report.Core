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
            string[][] results = new string[actionCollection.Count][];
            for (int i = 0; i < actionCollection.Count; i++)
            {
                var args = CommandParser.CheckParams(actionCollection[i].Value, results);
                var result = this.StartAction(actionCollection[i].Value.Key, args);

                if (result != null)
                {
                    if (actionCollection[i].Value.Key == ActionName.Get)
                        Console.WriteLine(result.ToString());//its out result
                    results[i] = result;
                }
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

                case ActionName.Array:
                    return args;

                case ActionName.Sum:
                    int res = 0;
                    if (args == null)
                        return null;
                    for (int i = 0; i < args.Length; i++)
                    {
                        try
                        { 
                            res += Convert.ToInt32(args[i]);
                        }
                        catch (InvalidCastException ex)
                        {
                            continue;
                        }
                    }
                    return res;

                    //for plus just arr1.add(arr2) then args/2
                default:
                    return null;
            }
        }
    }
}
