using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;

namespace XMIS.Report.Core.DAL
{
    public class ExcelAccessManager
    {
        private Application xlApp;
        private Workbook xlWorkBook;
        private Worksheet xlWorkSheet;
        private Dictionary<int, int> yOffset = new Dictionary<int, int>();

        ~ExcelAccessManager()
        {
            var misValue = System.Reflection.Missing.Value;
            this.xlWorkBook.Close(true, misValue, misValue);
            this.xlApp.Quit();

            this.releaseAllObjects();
        }

        public void OpenXlsFile(string fullFilePath)
        {
            if (this.xlApp != null)
                this.releaseAllObjects();
            string path = fullFilePath;
            if (!this.checkToXlsEnding(fullFilePath))
                path += ".xls";
            this.xlApp = new Application();
            try
            { 
                this.xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                this.xlWorkSheet = (Worksheet)xlWorkBook.ActiveSheet;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.releaseAllObjects();
                throw new FileNotFoundException(ex.Message, ex);
            }

            if (this.xlWorkSheet == null)
            {
                this.releaseAllObjects();
                throw new FileLoadException("err");
            }
        }

        public System.Data.DataTable ReadFormXlsFile()
        {
            this.yOffset.Clear();
            if (this.xlWorkSheet == null)
                throw new Exception("File is not opened");

            var range = this.xlWorkSheet.UsedRange;
            List<string[]> items = new List<string[]>();
            bool trigger = false;
            int offStart = 1;
            int offEnd = 0;
            for (int rCnt = 1; rCnt <= range.Rows.Count; rCnt++)
            {
                string[] arr = new string[range.Columns.Count];
                trigger = false;
                for (int cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                {
                    string str = Convert.ToString((range.Cells[rCnt, cCnt] as Range).Value2);
                    if (str != null)
                    {
                        arr[cCnt - 1] = str;
                        trigger = true;
                    }
                }


                if (trigger)
                {
                    if (offEnd == rCnt - 1 && rCnt - 1 != 0)
                    { 
                        this.yOffset.Add(offStart, offEnd);
                        offEnd = 0;
                    }
                    items.Add(arr);
                    offStart = rCnt;
                }
                else
                    offEnd = rCnt;
                    
            }
            
            if (offEnd != 0)
                this.yOffset.Add(offStart, offEnd);

            return this.ToDataTable(items);
        }

        private System.Data.DataTable ToDataTable(List<string[]> src)
        {
            if (src == null)
                return null;

            var dataTable = new System.Data.DataTable();

            for (int i = 0; i < src[0].Length; i++)
                dataTable.Columns.Add(new DataColumn());

            foreach(string[] s in src)
                dataTable.Rows.Add(s);

            return dataTable;
        }

        public void WriteToXlsFile(int rCnt, int cCnt, string data)
        {
            var offs = from y in this.yOffset
                    where y.Key < rCnt
                    select y.Value - y.Key;
            var off = 0;
            foreach (int i in offs)
                off += i;

            this.xlWorkSheet.UsedRange.Cells[rCnt + off + 1, cCnt + 1].Value2 = data;
        }

        public void WriteRowToXlsFile(int rCnt, string[] data)
        {
            if (this.xlWorkSheet == null)
                throw new Exception("File is not opened");

            var range = this.xlWorkSheet.UsedRange;
            for (int cCnt = 1; (rCnt <= range.Rows.Count) || (cCnt < data.Length + 1); rCnt++)
                this.xlWorkSheet.UsedRange.Value2 = data[cCnt - 1];
        }

        public void SaveXlsFileAs(string fullFilePath)
        {
            if (this.xlWorkBook != null)
                if (checkToXlsEnding(fullFilePath))
                    this.xlWorkBook.SaveAs(fullFilePath);
                else
                    this.xlWorkBook.SaveAs(fullFilePath + ".xls");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void releaseAllObjects()
        {
            try
            {
                if (this.xlWorkSheet != null)
                    releaseObject(this.xlWorkSheet);
                if (this.xlWorkBook != null)
                    releaseObject(this.xlWorkBook);
                if (this.xlApp != null)
                    releaseObject(this.xlApp);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }

        private bool checkToXlsEnding(string src)
        {
            return src.IndexOf(".xls") > 0;
        }
    }
}
