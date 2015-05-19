using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.Processor.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Core.Processor.Extentions;
using XMIS.Report.Core.Processor.Condition.Helper;
using XMIS.Report.Core.Processor.Condition.Provider;
using XMIS.Report.Core.Processor.Condition.ResultFilter;

namespace XMIS.Report.Core.Processor.Condition
{
    public class ConditionController : IConditionController
    {
        private readonly Dictionary<string, Func<ServiceDescriptorBase, bool>> conditionDictionary = new Dictionary<string, Func<ServiceDescriptorBase, bool>>();

        private readonly PeriodConditionProvider periodConditionProvider = new PeriodConditionProvider();

        private readonly List<IConditionHelper> conditionHelperCollection = new List<IConditionHelper>();

        private readonly Dictionary<string, IConditionProvider> conditionProviderCollection = new Dictionary<string, IConditionProvider>();

        public ConditionController()
        {
            // providers used for parameterized conditions
            this.conditionProviderCollection.Add("regions", new RegionConditionProvider());
            this.conditionProviderCollection.Add("ages", new AgeConditionProvider());


            //// helpers used for simple conditions processing
            this.conditionHelperCollection.Add(new PatientConditionHelper());


            // initialize condition collection
            this.InitConditionCollection();
        }

        public Func<ServiceDescriptorBase, bool> GetConditionFunction(string condition, string patientType, DateTime startDate, DateTime endDate)
        {
            List<Func<ServiceDescriptorBase, bool>> funcList = new List<Func<ServiceDescriptorBase, bool>>();

            Func<ServiceDescriptorBase, bool> periodFunc = this.periodConditionProvider.GetFunction(patientType, startDate, endDate);

            funcList.Add(periodFunc);

            if (!string.IsNullOrEmpty(condition))
            {
                string[] conditionParts = condition.Split(',');

                foreach (string part in conditionParts)
                {
                    if (part.Contains(':'))
                    {
                        string[] partUnits = part.Split(':');

                        string command = partUnits[0].Trim();
                        string value = partUnits[1].Trim();

                        if (!command.Equals(string.Empty) && !value.Equals(string.Empty))
                        {
                            IConditionProvider conditionProvider;
                            if (this.conditionProviderCollection.TryGetValue(command, out conditionProvider))
                            {
                                Func<ServiceDescriptorBase, bool> resultFunc = conditionProvider.GetFunction(value);
                                funcList.Add(resultFunc);
                            }
                        }
                    }
                    else
                    {
                        Func<ServiceDescriptorBase, bool> resultFunc;
                        if (this.conditionDictionary.TryGetValue(part.Trim(), out resultFunc))
                        {
                            funcList.Add(resultFunc);
                        }
                    }
                }
            }
            else
            {
                funcList.Add(s => true);
            }

            Func<ServiceDescriptorBase, bool> resultFunction = LinqHelper.CombineWithAnd(funcList);

            return resultFunction;
        }

        private void InitConditionCollection()
        {
            try
            {
                foreach (IConditionHelper conditionHelper in this.conditionHelperCollection)
                {
                    conditionHelper.Init(this.conditionDictionary);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
