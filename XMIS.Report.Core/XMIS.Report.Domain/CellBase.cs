using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class CellBase
    {
        public int RowIdx { get; set; }
        public int ColumnIdx { get; set; }
        public string Value { get; set; }
    }
}
