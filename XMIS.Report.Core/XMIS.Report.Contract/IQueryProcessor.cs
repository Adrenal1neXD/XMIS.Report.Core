using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Contract
{
    public interface IQueryProcessor
    {
        Dictionary<object, double> DoQuery(
            Func<ServiceDescriptorBase, bool> func, Func<ServiceDescriptorBase, object> groupFunc);

        double GetCount(Func<ServiceDescriptorBase, bool> func);
    }
}
