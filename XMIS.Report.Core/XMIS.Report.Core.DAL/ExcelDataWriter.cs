using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Core.DAL.Contract; 

namespace XMIS.Report.Core.DAL
{
    public class ExcelDataWriter : IExcelDataWriter
    {
        private Range cells;
        private int rowIdx = 1;
        private int colIdx = 0;

        public ExcelDataWriter(Range cells)
        {
            this.cells = cells;
        }

        public void NextRow()
        {
            this.rowIdx++;
            this.colIdx = 0;
        }

        public void CreateCell(string value = "", int hcolspan = 1, int vcolspan = 1)
        {
            this.colIdx++;
            ((Range)cells[colIdx][rowIdx]).Value2 = value;
            if ((hcolspan|vcolspan) > 1)
            {
                cells.Range[cells[colIdx][rowIdx], cells[colIdx + hcolspan - 1][rowIdx + vcolspan - 1]].Merge();
                this.colIdx += hcolspan - 1;
            }
        }

        public void MoveRight(int times)
        {
            //for (int i = 0; i < times; i++)
            //    this.CreateCell();
            this.colIdx += times;
        }

        public void AddBorder(int row, int column, CellBorder border = null)
        {
            var _border = new CellBorder();
            _border.ToDefaultAll();
            if (border != null)
                _border = border;

            if (border.IsZero())
                return;

            ((Range)cells[row, column]).Borders.LineStyle = XlLineStyle.xlContinuous;
            if (_border.Left > 0)
                ((Range)cells[row, column]).Borders[XlBordersIndex.xlEdgeLeft].Weight = _border.Left;
            if (_border.Right > 0)
                ((Range)cells[row, column]).Borders[XlBordersIndex.xlEdgeRight].Weight = _border.Right;
            if (_border.Top > 0)
                ((Range)cells[row, column]).Borders[XlBordersIndex.xlEdgeTop].Weight = _border.Top;
            if (_border.Bottom > 0)
                ((Range)cells[row, column]).Borders[XlBordersIndex.xlEdgeBottom].Weight = _border.Bottom;
        }

        public void AddBorders(int row, int col, int len, CellBorder border = null)
        {
            var _border = new CellBorder();
            _border.ToDefaultAll();
            if (border != null)
                _border = border;

            for (int i = 0; i < len; i++)
                this.AddBorder(row, col + i, _border);
        }

        public void AddBorder(CellBorder border = null)
        {
            var _border = new CellBorder();
            _border.ToDefaultAll();
            if (border != null)
                _border = border;
            
            this.AddBorder(this.rowIdx, this.colIdx, _border);
        }

        public int RowIdx
        {
            get
            {
                return this.rowIdx;
            }
        }

        public int ColumnIdx
        {
            get
            {
                return this.colIdx;
            }
        }

        public void FormatText(int row, int col, int deg)
        {
            ((Range)cells[col][row]).Orientation = deg;
        }

        public void FormatText(int row, int col, bool bold, bool italic, bool underline)
        {
            ((Range)cells[col][row]).Font.Bold = bold;
            ((Range)cells[col][row]).Font.Italic = italic;
            ((Range)cells[col][row]).Font.Underline = underline;
        }

        public void FormatText(int row, int col, XlHAlign halign, XlVAlign valign)
        {
            ((Range)cells[col][row]).HorizontalAlignment = halign;
            ((Range)cells[col][row]).VerticalAlignment = valign;
        }

    }
}
