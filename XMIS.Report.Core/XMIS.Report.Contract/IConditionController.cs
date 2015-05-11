using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Contract
{
    public interface IConditionController
    {
        Func<ServiceDescriptorBase, bool> GetConditionFunction(
            string condition, string patientType, DateTime startDate, DateTime endDate);

        Func<ServiceDescriptorBase, object> GetGroupFunction(string groupType);
    }
}
