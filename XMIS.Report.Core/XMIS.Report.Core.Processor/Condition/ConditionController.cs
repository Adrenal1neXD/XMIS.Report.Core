using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Core.Processor.Extentions;

namespace XMIS.Report.Core.Processor.Condition
{
    public class ConditionController : IConditionController
    {
        private readonly Dictionary<string, Func<ServiceDescriptorBase, bool>> conditionDictionary = new Dictionary<string, Func<ServiceDescriptorBase, bool>>();

        private readonly Dictionary<string, Func<ServiceDescriptorBase, object>> groupConditionDictionary = new Dictionary<string, Func<ServiceDescriptorBase, object>>();

        private readonly PeriodConditionProvider periodConditionProvider = new PeriodConditionProvider();

        private readonly List<IConditionHelper> conditionHelperCollection = new List<IConditionHelper>();

        private readonly Dictionary<string, IConditionProvider> conditionProviderCollection =
            new Dictionary<string, IConditionProvider>();

        public ConditionController()
        {
            // providers used for parameterized conditions
            //this.conditionProviderCollection.Add("icd_pattern", new IcdConditionProvider());
            //this.conditionProviderCollection.Add("operations", new OperationConditionProvider());
            //this.conditionProviderCollection.Add("surgeons", new SurgeonConditionProvider());
            this.conditionProviderCollection.Add("regions", new RegionConditionProvider());
            //this.conditionProviderCollection.Add("operationDirection", new OperationDirectionConditionProvider());
            //this.conditionProviderCollection.Add("operationClass", new OperationGroupConditionProvider());
            this.conditionProviderCollection.Add("ages", new AgeConditionProvider());

            //// helpers used for simple conditions processing
            this.conditionHelperCollection.Add(new PatientConditionHelper());
            //this.conditionHelperCollection.Add(new StayDivergenceConditionHelper());
            //this.conditionHelperCollection.Add(new OperationConditionHelper());
            //this.conditionHelperCollection.Add(new OperationLengthDivergenceConditionHelper());

            // initialize condition collections
            this.InitConditionCollection();
            this.InitGroupConditionCollection();
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

        public Func<ServiceDescriptorBase, object> GetGroupFunction(string groupType)
        {
            Func<ServiceDescriptorBase, object> resultFunc;

            if (!this.groupConditionDictionary.TryGetValue(groupType, out resultFunc))
            {
                resultFunc = c => c.Patient.Age;
            }

            return resultFunc;
        }

        private void InitGroupConditionCollection()
        {
            //this.groupConditionDictionary.Add("diagnosis", c => c.Diagnosis.FinalDiagnosis.IcdCode);
            //this.groupConditionDictionary.Add("operation", c => c.Operation.FactOperation.MainOperation.OperationCode);
            //this.groupConditionDictionary.Add("surgeon", c => c.Operation.FactOperation.SurgeonId);

            //this.groupConditionDictionary.Add("xdiagnosis-operation",
            //                        c => new { c.Diagnosis.FinalDiagnosis.IcdCode, c.Operation.FactOperation.MainOperation.OperationStandardId });

            //this.groupConditionDictionary.Add("diagnosis-operation",
            //            c => c.Diagnosis.FinalDiagnosis.IcdCode + " - " + c.Operation.FactOperation.MainOperation.OperationStandardId);

            this.groupConditionDictionary.Add("group", c => c.Patient.AgeGroup);
            this.groupConditionDictionary.Add("sub", c => c.Patient.AgeSubGroup);
            this.groupConditionDictionary.Add("raw", c => c.Patient.Age);

            this.groupConditionDictionary.Add("month_in", c => c.InDate.Month);
            this.groupConditionDictionary.Add("month_out", c => c.OutDate.Month);

            this.groupConditionDictionary.Add("dow_in", c => (int)c.InDate.DayOfWeek + 1);
            this.groupConditionDictionary.Add("dow_out", c => (int)c.OutDate.DayOfWeek + 1);

            this.groupConditionDictionary.Add("serv_id", c => c.Id);
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
