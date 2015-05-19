using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;
using XMIS.Report.Core.Processor.Contract;

namespace XMIS.Report.Core.Processor.Condition.Helper
{ 
    public class PatientConditionHelper : IConditionHelper
    {
        public void Init(Dictionary<string, Func<ServiceDescriptorBase, bool>> conditionDictionary)
        {
            conditionDictionary.Add("m", s => s.Patient.Gender == 1);
            conditionDictionary.Add("f", s => s.Patient.Gender == 2);
            conditionDictionary.Add("city", s => s.Patient.IsVillager == false);
            conditionDictionary.Add("village", s => s.Patient.IsVillager == true);
            conditionDictionary.Add("dead", s => s.Patient.IsAlive == false);
            conditionDictionary.Add("alive", s => s.Patient.IsAlive == true);
        }
    }
}
