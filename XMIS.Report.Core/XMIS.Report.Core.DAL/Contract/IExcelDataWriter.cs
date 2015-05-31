using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace XMIS.Report.Core.DAL.Contract
{
    public interface IExcelDataWriter
    {
        void CreateCell(string value = "", int hcolspan = 1, int vcolspan = 1);
        void MoveRight(int times);
        void NextRow();
        void AddBorder(int row, int column, CellBorder border = null);
        void AddBorders(int row, int columnStart, int len, CellBorder border = null);
        void AddBorder(CellBorder border = null);
        int RowIdx { get; }
        int ColumnIdx { get; }
        int MaxRowIdx { get; }
        int MaxColumnIdx { get; }
        void FormatText(int row, int col, int deg);
        void FormatText(int row, int col, bool bold, bool italic, bool underline);
        void FormatText(int row, int col, XlHAlign halign, XlVAlign valign);
        void SetValue(int row, int col, string value);
    }
}
