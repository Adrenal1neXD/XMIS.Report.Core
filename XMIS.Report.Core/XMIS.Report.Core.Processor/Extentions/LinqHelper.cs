using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Core.Processor.Extentions
{
    public static class LinqHelper
    {
        public static Func<T, bool> CombineWithAnd<T>(IList<Func<T, bool>> filters)
        {
            return x =>
            {
                foreach (var filter in filters)
                {
                    if (!filter(x))
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        public static Func<T, bool> CombineWithOr<T>(IEnumerable<Func<T, bool>> filters)
        {
            return x =>
            {
                foreach (var filter in filters)
                {
                    if (filter(x))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        public static IEnumerable<IGrouping<object, TElement>> GroupByMany<TElement>(
        this IEnumerable<TElement> elements, params string[] groupSelectors)
        {//
            var selectors = new List<Func<TElement, object>>(groupSelectors.Length);
            //selectors.AddRange(groupSelectors.Select(selector => DynamicExpression.ParseLambda(typeof(TElement), typeof(object), selector)).Select(l => (Func<TElement, object>)l.Compile()));
            return elements.GroupByMany(selectors.ToArray());
        }

        public static IEnumerable<IGrouping<object, TElement>> GroupByMany<TElement>(
                this IEnumerable<TElement> elements, params Func<TElement, object>[] groupSelectors)
        {
            if (groupSelectors.Length > 0)
            {
                Func<TElement, object> selector = groupSelectors.First();
                return elements.GroupBy(selector);
            }
            return null;
        }

    }
}
