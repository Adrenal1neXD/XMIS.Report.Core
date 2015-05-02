using System.Data;
using System.Data.Common;


namespace XMIS.Report.Core.DAL
{
    public interface IDataAccessManager
    {
        void Connect(string directory);
        bool Connected { get; }
        DataTable DoQuery(string query);
        void Disconnect();
    }
}
