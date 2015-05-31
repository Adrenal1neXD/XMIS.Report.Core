using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public enum BedStatus
    {
        Broken, 
        Busy,
        WritenOff,
        Free
    }

    public class BedBase
    {
        public int BedId { get; set; }
        public BedStatus  Status { get; set; }
        public PatientDescriptorBase Patient { get; set; }
    }
}
