using System.Collections.Generic;
using System.Data;

namespace XMIS.Report.Core.DAL.Extentions
{
    public static class ExcelManagerExtention
    {
        public static DataTable ToDataTable(this List<string[]> src)
        {
            if (src == null)
                return null;

            var dataTable = new System.Data.DataTable();

            for (int i = 0; i < src[0].Length; i++)
                dataTable.Columns.Add(new DataColumn());

            foreach (string[] s in src)
                dataTable.Rows.Add(s);

            return dataTable;
        }
    }
}
