using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class DepartmentBase
    {
        public string DepertmantType { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmenID { get; set; }
        public List<ServiceDescriptorBase> Services { get; set; }
        public List<RoomBase> Rooms { get; set; }
    }
}
