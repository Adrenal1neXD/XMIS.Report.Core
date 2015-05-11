using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain.Default;
using XMIS.Report.Domain;
using XMIS.Report.Transform;
using XMIS.Report.Core.DAL;
using System.Data.SqlClient;
using System.Data;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = new DataAccessManager<SqlConnection>();
            conn.Connect(@"WIN-Q2I6UCG0G3J\SQLSERVER");

            var data = conn.DoQuery("Select * from [XMISDB].[dbo].[Person]");

            var service = new ServiceDescriptorTransformer();
            List<ServiceDescriptorBase> sd = new List<ServiceDescriptorBase>();

            foreach(DataRow row in data.AsEnumerable())
                sd.Add(service.Transform(row));
        }
    }
}
