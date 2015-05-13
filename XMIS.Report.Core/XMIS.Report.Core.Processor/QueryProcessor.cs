using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Contract;
using XMIS.Report.Domain;

namespace XMIS.Report.Core.Processor
{
    public class QueryProcessor : IQueryProcessor
    {
        private IList<ServiceDescriptorBase> serviceCollection = new List<ServiceDescriptorBase>();

        public QueryProcessor(IList<ServiceDescriptorBase> serviceDescriptorCollection)
        {
            this.serviceCollection = serviceDescriptorCollection;
        }

        public IList<ServiceDescriptorBase> ServiceCollection
        {
            get { return this.serviceCollection; }
            set { this.serviceCollection = value; }
        }

        public Dictionary<Object, double> DoQuery(Func<ServiceDescriptorBase, bool> func, Func<ServiceDescriptorBase, Object> groupFunc)
        {
            Dictionary<Object, double> resultDictionary = new Dictionary<Object, double>();
            IOrderedEnumerable<KeyValuePair<Object, double>> queryResult = null;

            queryResult =
                this.serviceCollection.Where(func)
                    .GroupBy(groupFunc)
                    .Select(group => new KeyValuePair<Object, double>(group.Key, group.Count()))
                    .OrderBy(c => c.Key);

            if (queryResult != null)
            {
                resultDictionary = queryResult.ToDictionary(ob => ob.Key, ob => ob.Value);
            }

            return resultDictionary;
        }

        public int DoCountQuery(Func<ServiceDescriptorBase, bool> func)
        {
            return this.serviceCollection.Where(s => func(s) == true).Count();
        }

        public double GetCount(Func<ServiceDescriptorBase, bool> func)
        {
            int queryResult = this.serviceCollection.Where(func).ToList().Count;

            return (double)queryResult;
        }
    }
}
