using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Domain;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts
{
    public abstract class PartMaker
    {
        public abstract void Do(IExcelDataWriter writer);
    }
}
