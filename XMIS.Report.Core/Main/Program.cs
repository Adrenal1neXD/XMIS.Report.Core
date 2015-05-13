using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain.Default;
using XMIS.Report.Domain;
using XMIS.Report.Transform;
using XMIS.Report.Core.DAL;
using XMIS.Report.Core.Processor;
using XMIS.Report.Core.Processor.Condition;
using XMIS.Report.Core.BLL;
using System.Data.SqlClient;
using System.Data;
using XMIS.Report.Core.BLL.Extentions;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = new ReportController();
            rc.WriteDataToXlsFile("form7x.xls");
        }
    }
}
