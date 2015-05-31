using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace XMIS.Report.Core.DAL.Tests
{
    [TestFixture]
    public class ExcelAppTests
    {
        [TestCase]
        public void ExcelApp_Cells()
        {
            var app = new ExcelApp();
            var cells = app.Cells;
            Assert.That(cells != null);
        }

        [TestCase]
        public void ExcelApp_CellsEdit()
        {
            var app = new ExcelApp();
            var cells = app.Cells;
            ((Range)app.Cells[1, 1]).Value2 = "test";

            Assert.AreEqual(((Range)app.Cells[1, 1]).Value2, "test");
        }

        [TestCase(@"", Result = false)]
        [TestCase("path", Result = true)]
        [TestCase(@"D:\Work\1\test1", Result = true)]
        [TestCase(@"D:\Work\1\test2.xls", Result = true)]
        public bool ExcelApp_SaveAs(string path)
        {
            var app = new ExcelApp();
            app.SaveAs(path);
            if (path.IndexOf(".xls") > 1)
                return File.Exists(path);
            else
                return File.Exists(path + ".xls");
        }

        [TestCase(@"D:\Work\1\test3.xls")]
        public void ExcelApp_SaveAsCheckValues(string path)
        {
            var app = new ExcelApp();
            ((Range)app.Cells[1, 1]).Value2 = "test";
            ((Range)app.Cells[2, 1]).Value2 = "----";
            app.SaveAs(path);
            var app2 = new ExcelApp(path);

            Assert.That(((Range)app2.Cells[1, 1]).Value2 == "test" && ((Range)app2.Cells[2, 1]).Value2 == "----");
        }
    }
}
