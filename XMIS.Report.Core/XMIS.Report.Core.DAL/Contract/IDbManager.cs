using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL.Contract
{
    public interface IDbManager
    {
        void Connect(string directory);
        bool Connected { get; }
        DataTable DoQuery(string query);
        void Disconnect();
    }
}
