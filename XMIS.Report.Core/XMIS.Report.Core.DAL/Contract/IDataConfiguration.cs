using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL.Contract
{
    public interface IDataConfiguration
    {
        string DBConnectionPath { get; set; }
        string SrcPath { get; set; }
        string DstPath { get; set; }

        void ReadConfiguration();
    }
}
