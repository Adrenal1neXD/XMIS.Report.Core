using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.DAL
{
    public class ExcelApp : IExcelApp
    {
        private Application App { get; set; }
        private Workbook WorkBook { get; set; }
        private Worksheet WorkSheet { get; set; }

        public Range Cells
        {
            get
            {
                return this.WorkSheet.Cells;
            }
        }

        public ExcelApp()
        {
            //create xls app
            this.App = new Application();
            this.App.SheetsInNewWorkbook = 1;
            this.WorkBook = this.App.Workbooks.Add(System.Reflection.Missing.Value);
            this.WorkSheet = this.WorkBook.ActiveSheet as Worksheet;
        }

        public ExcelApp(string path)
        {
            //create xls app
            this.App = new Application();
            if (!this.validPath(path))
                throw new FileNotFoundException(path);
            var localPath = this.checkToXlsEnding(path);
            try
            {
                this.WorkBook = App.Workbooks.Open(path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                this.WorkSheet = (Worksheet)WorkBook.ActiveSheet;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                this.releaseAllObjects();
                throw new FileNotFoundException(ex.Message, ex);
            }

            if (this.WorkSheet == null)
            {
                this.releaseAllObjects();
                throw new FileLoadException();
            }
        }

        public void SaveAs(string path)
        {
            if (path == string.Empty)
                return;
            if (this.validValues() && this.validPath(path))
                if (!path.Contains('\\'))
                    this.WorkBook.SaveAs(this.checkToXlsEnding(Directory.GetCurrentDirectory() + @"\" + path));
                else
                    this.WorkBook.SaveAs(this.checkToXlsEnding(path));
        }

        ~ExcelApp()
        {
            //var misValue = System.Reflection.Missing.Value;
            //this.WorkBook.Close(true, misValue, misValue);
            //this.App.Quit();

            this.releaseAllObjects();
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
                if (this.WorkSheet != null)
                    releaseObject(this.WorkSheet);
                if (this.WorkBook != null)
                    releaseObject(this.WorkBook);
                if (this.App != null)
                    releaseObject(this.App);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }

        private string checkToXlsEnding(string src)
        {
            if (src.IndexOf(".xls") > 0)
                return src;
            else
                return src + ".xls";
        }

        private bool validPath(string path)
        {
            bool result = true;
            if (path == string.Empty)
                result = false;

            var supPath = path.Split('\\');
            if (supPath.Length < 2)
                return true;
            var dir = "";
            for (int i = 0; i < supPath.Length - 1; i++)
                dir += supPath[i] + '\\';
            dir = dir.Remove(dir.Length - 1);
            result = Directory.Exists(dir);

            return result;
        }

        private bool validValues()
        {
            return this.App != null && this.WorkBook != null && this.WorkSheet != null;
        }
    }
}
