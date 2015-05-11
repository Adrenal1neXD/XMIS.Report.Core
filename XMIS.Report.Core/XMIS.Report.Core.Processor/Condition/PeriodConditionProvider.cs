using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Core.Processor.Condition
{
    public class PeriodConditionProvider
    {
        public Func<ServiceDescriptorBase, bool> GetFunction(string patientType, DateTime startDate, DateTime finalDate)
        {
            Func<ServiceDescriptorBase, bool> resultFunc;

            if (patientType == "in")
            {
                resultFunc = (s => (s.InDate >= startDate && s.InDate <= finalDate));
            }
            else
            {
                resultFunc = (s => (s.OutDate >= startDate && s.OutDate <= finalDate));
            }

            return resultFunc;
        }
    }
}
