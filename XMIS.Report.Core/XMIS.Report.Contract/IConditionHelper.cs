using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Contract
{
    public interface IConditionHelper
    {
        void Init(Dictionary<string, Func<ServiceDescriptorBase, bool>> conditionDictionary);
    }
}
