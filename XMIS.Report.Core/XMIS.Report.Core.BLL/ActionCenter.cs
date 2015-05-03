using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.BLL
{
    public enum ActionName
    {
        Plus,
        Minus,
        Multiply,
        Devide,
        Sum,
        Array,
        None,
        Select,
        Min,
        Max
    }

    struct DataTableCommand
    {
        public string Value { get; set; }
        public int RowIdx { get; set; }
        public int ColumnIdx { get; set; }
    }

    public class ActionCenter
    {
        public dynamic Handle(DataTable param)
        {
            var comm = this.SelectCommands(param);
            if (comm == null || comm.Count == 0)
                throw new ArgumentException("Can't parse args"); 
            var realComms = this.TryParse(comm);
            if (realComms == null || realComms.Count == 0)
                throw new ArgumentException("Can't parse args");

            return realComms;
        }

        private List<DataTableCommand> SelectCommands(DataTable xlsdata)
        {
            //select {cell value/rowIdx/colIdx} of excel file where first char = '$'
            var arr = (from dataRow in xlsdata.AsEnumerable()
                       from obj in dataRow.ItemArray
                       where obj.ToString() != string.Empty && obj.ToString()[0] == '$'
                       select new DataTableCommand
                       {
                           Value = obj.ToString(),
                           RowIdx = xlsdata.AsEnumerable().ToList().IndexOf(dataRow),
                           ColumnIdx = dataRow.ItemArray.ToList().IndexOf(obj)
                       }).ToList();

            return arr.Count > 1 ? arr : null;
        }

        private List<KeyValuePair<ActionName, string[]>> TryParse(List<DataTableCommand> arr)
        {
            //parse data
            List<KeyValuePair<ActionName, string[]>> parseResult = new List<KeyValuePair<ActionName, string[]>>();
            var priorityCollection = new List<string>();
            for (int i = 0; i < arr.Count; i++)
                CommandParser.GetPriorityQueue(arr[i].Value, priorityCollection);
            foreach (string s in priorityCollection)
                parseResult.Add(CommandParser.Parse(s));

            return parseResult.Count > 1 ? parseResult : null;
        }
    }
}
