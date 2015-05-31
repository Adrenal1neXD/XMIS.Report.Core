using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.Processor.Contract;
using XMIS.Report.Domain;

namespace XMIS.Report.Core.Processor
{
    public class QueryProcessor<T> : IQueryProcessor<T>
    {
        private IList<T> collection = new List<T>();

        public QueryProcessor(IList<T> descriptorCollection)
        {
            this.collection = descriptorCollection;
        }

        public IList<T> Collection
        {
            get { return this.collection; }
            set { this.collection = value; }
        }

        public List<T> DoQuery(Func<T, bool> func)
        {
            return this.collection.Where(func).ToList();
        }

        public int GetCount(Func<T, bool> func)
        {
            return this.collection.Where(func).ToList().Count;
        }
    }
}
