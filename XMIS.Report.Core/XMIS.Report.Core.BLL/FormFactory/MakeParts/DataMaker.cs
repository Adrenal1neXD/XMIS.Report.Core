using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts
{
    public abstract class DataMaker
    {
        public abstract List<Func<int>> GetDataFunc(DepartmentDescriptorBase descr, DateTime fromDate, DateTime toDate);
    }
}
