using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.DAL;
using XMIS.Report.Transform;
using XMIS.Report.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Default;
using XMIS.Report.Core.Processor.Condition;
using XMIS.Report.Core.Processor;
using XMIS.Report.Core.BLL.Extentions;
using System.IO;

namespace XMIS.Report.Core.BLL
{
    public class ReportController
    {
        private readonly DataConfiguration config = new DataConfiguration();
        private readonly ExcelAccessManager excelAccessManager = new ExcelAccessManager();
        private readonly IConditionController conditionController = new ConditionController();
        private readonly IDataAccessManager dataAccessManager;
        private readonly List<ServiceDescriptorBase> descriptorCollection;
        
        public ReportController()
        {
            //config.ReadConfiguration();

            //sql conn
            this.dataAccessManager = new DataAccessManager<SqlConnection>();
            this.dataAccessManager.Connect(this.config.DBConnectionPath);
            //

            //to do sql helper
            var dbdata = this.dataAccessManager.DoQuery("Select * from [XMISDB].[dbo].[Person]");
            this.descriptorCollection = this.GetTransformedCollection(dbdata);
            //
        }

        public void WriteDataToXlsFile(string fileName)
        {
            this.excelAccessManager.OpenXlsFile(this.config.SrcPath + '\\' + fileName);
            var data = this.excelAccessManager.ReadFormXlsFile();

            var commCells = this.GetCells(data);
            for (int i = 0; i < commCells.Count; i++)
            {
                var val = this.HandleValue(commCells[i].Value);
                if (val.GetType() == typeof(Dictionary<object,double>))
                {
                    //grouped
                }
                else
                {
                    //cnt
                    this.excelAccessManager.WriteToXlsFile(commCells[i].RowIdx, commCells[i].ColumnIdx, val.ToString());
                }
            }

            var shortPath = string.Format(@"{0}\{1}", this.config.DstPath, DateTime.Today.ToShortDateString());
            Directory.CreateDirectory(shortPath);
            var savePath = string.Format(@"{0}\{1}", shortPath, fileName);
            this.excelAccessManager.SaveXlsFileAs(savePath);
        }

        //
        private List<CellBase> GetCells(DataTable data)
        {
            var result = new List<CellBase>();

            for (int rowIdx = 0; rowIdx < data.Rows.Count; rowIdx++)
                for (int colIdx = 0; colIdx < data.Columns.Count; colIdx++)
                {
                    string cellValue = data.Rows[rowIdx][colIdx].ToString();
                    if (cellValue != null && cellValue.Length > 0 && cellValue[0] == '$')
                        result.Add(new CellBase { Value = cellValue.Trim('$', ' '), RowIdx = rowIdx, ColumnIdx = colIdx });
                }

            return result;
        }

        private dynamic HandleValue(string value)
        {
            var result = this.GetDictionary(value);

            if (result == null)
                return null;

            object condition;
            object type;
            object from;
            object to;
            object group;

            result.TryGetValue("condition", out condition);
            result.TryGetValue("type", out type);
            result.TryGetValue("from", out from);
            result.TryGetValue("to", out to);
            result.TryGetValue("group", out group);

            var func = this.conditionController.GetConditionFunction(
                    (string)condition,
                    (string)type,
                    (DateTime)from,
                    (DateTime)to);

            var query = new QueryProcessor(descriptorCollection);

            if (group != null)
            {
                var groupResult = this.conditionController.GetGroupFunction((string)group);
                return query.DoQuery(func, groupResult);
            }
            else
                return query.DoCountQuery(func);
        }

        private Dictionary<string, object> GetDictionary(string srcStr)
        {
            var src = srcStr.Trim('$');
            DateTime intervalFrom;
            DateTime intervalTo;

            try 
            { 
                intervalFrom = DateTime.Parse(src.FindFromDate());
                intervalTo = DateTime.Parse(src.FindToDate());
            }
            catch (ArgumentNullException ex)
            {
                // если не находит дату то кидает екс. ИСПРАВИТЬ
                throw new Exception("Wrong DateTime format", ex);
            }

            string condition = src.FindCondition();
            string type = src.FindType();
            string group = src.FindGroup();

            if (intervalFrom != null 
                && intervalTo != null
                && condition != null
                && type != null)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("from", intervalFrom);
                result.Add("to", intervalTo);
                result.Add("condition", condition);
                result.Add("type", type);
                result.Add("group", group);

                return result;
            }

            return null;
        }

        private List<ServiceDescriptorBase> GetTransformedCollection(DataTable data)
        {
            var service = new ServiceDescriptorTransformer();
            List<ServiceDescriptorBase> descriptorCollection = new List<ServiceDescriptorBase>();

            foreach (DataRow row in data.AsEnumerable())
                descriptorCollection.Add(service.Transform(row));

            return descriptorCollection;
        }

    }
}
