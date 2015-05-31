using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Transform.Contract
{
    public interface IDescriptorTransformer<T>
    {
        T Transform(DataRow dataRow);
    }
}
