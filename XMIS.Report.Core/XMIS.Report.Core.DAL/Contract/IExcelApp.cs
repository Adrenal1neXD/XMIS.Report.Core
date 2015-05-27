using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL.Contract
{
    public interface IExcelApp : IApp
    {
        Range Cells { get; }
        void SaveAs(string path);
    }
}
