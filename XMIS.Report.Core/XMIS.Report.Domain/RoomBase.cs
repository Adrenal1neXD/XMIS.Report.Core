using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class RoomBase
    {
        public List<BedBase> Beds { get; set; }
        public int RoomId { get; set; }
        public int Gender { get; set; }
    }
}
