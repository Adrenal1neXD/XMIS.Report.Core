using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Core.BLL;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var cntrl = new ReportControl();
            cntrl.ReadData();
        }
    }
}
