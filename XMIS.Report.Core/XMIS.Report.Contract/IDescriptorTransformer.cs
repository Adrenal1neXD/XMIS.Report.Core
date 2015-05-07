using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Contract
{
    public interface IDescriptorTransformer
    {
        dynamic Transform(DataRow dataRow);
    }
}
