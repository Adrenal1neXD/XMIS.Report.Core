using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Core.BLL;
using XMIS.Report.Core.DAL;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            new ReportControl().ReadData();
        }
    }
}
