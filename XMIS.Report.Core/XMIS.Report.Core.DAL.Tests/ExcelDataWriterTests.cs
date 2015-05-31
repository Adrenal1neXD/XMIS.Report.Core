using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XMIS.Report.Core.DAL;

namespace XMIS.Report.Core.DAL.Tests
{
    [TestFixture]
    public class ExcelDataWriterTests
    {
        [TestCase]
        public void tst()
        {
            var app = new ExcelApp();
            var edw = new ExcelDataWriter(app.Cells);

        }
    }
}
