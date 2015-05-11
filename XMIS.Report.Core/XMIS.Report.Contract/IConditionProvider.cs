using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Contract
{
    public interface IConditionProvider
    {
        Func<ServiceDescriptorBase, bool> GetFunction(string condition);
    }
}
