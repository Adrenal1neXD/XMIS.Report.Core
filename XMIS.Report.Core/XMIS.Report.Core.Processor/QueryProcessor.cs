using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.Processor.Contract;
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

        public List<ServiceDescriptorBase> DoQuery(Func<ServiceDescriptorBase, bool> func)
        {
            return this.serviceCollection.Where(func).ToList();
        }

        public int GetCount(Func<ServiceDescriptorBase, bool> func)
        {
            return this.serviceCollection.Where(func).ToList().Count;
        }
    }
}
