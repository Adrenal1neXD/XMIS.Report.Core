using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.Processor.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Core.Processor.Extentions;

namespace XMIS.Report.Core.Processor.Condition.ResultFilter
{
    public class AgeConditionProvider : IConditionProvider
    {
        public Func<ServiceDescriptorBase, bool> GetFunction(string condition)
        {
            Func<ServiceDescriptorBase, bool> func = null;

            string[] surgeonList = condition.Split(';');
            foreach (string operation in surgeonList.Where(s => s.Trim() != string.Empty))
            {
                Func<ServiceDescriptorBase, bool> __func = null;

                if (operation.StartsWith("!"))
                {
                    // ! means "not"  
                    string _operation = operation.Replace("!", string.Empty);
                    __func = c => c.Patient.Age != Convert.ToInt32(_operation);
                }
                else if (operation.StartsWith("<"))
                {
                    string _operation = operation.Replace("<", string.Empty);
                    __func = c => c.Patient.Age < Convert.ToInt32(_operation);
                }
                else if (operation.StartsWith(">"))
                {
                    string _operation = operation.Replace(">", string.Empty);
                    __func = c => c.Patient.Age > Convert.ToInt32(_operation);
                }
                else
                {
                    __func = c => c.Patient.Age == Convert.ToInt32(operation);
                }

                if (func == null)
                {
                    func = __func;
                }
                else
                {
                    func = LinqHelper.CombineWithOr(new List<Func<ServiceDescriptorBase, bool>>() { func, __func });
                }
            }

            return func;
        }
    }
}
