using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Core.Processor.Contract
{
    public interface IQueryProcessor
    {
        //Dictionary<object, double> DoQuery(
        //    Func<ServiceDescriptorBase, bool> func);

        List<ServiceDescriptorBase> DoQuery(Func<ServiceDescriptorBase, bool> func);

        //Dictionary<object, double> DoQuery(
        //    Func<ServiceDescriptorBase, bool> func, Func<ServiceDescriptorBase, Object> groupFunc, Func<ServiceDescriptorBase, string> resultFunc);

        double GetCount(Func<ServiceDescriptorBase, bool> func);
    }
}
