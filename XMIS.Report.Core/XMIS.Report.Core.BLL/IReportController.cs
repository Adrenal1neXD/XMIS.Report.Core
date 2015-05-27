using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.BLL
{
    public interface IReportController
    {
        void CreateReport(string path, string formName, DateTime fromDate, DateTime toDate);
    }
}
