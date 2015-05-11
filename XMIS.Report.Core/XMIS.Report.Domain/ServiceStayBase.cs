using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class ServiceStayBase
    {
        private StayDescriptorBase plannedStay;
        private StayDescriptorBase factStay;
        private int divergence;
        private int divergenceDays;

        public StayDescriptorBase PlannedStay
        {
            get { return plannedStay; }
            set { plannedStay = value; }
        }

        public StayDescriptorBase FactStay
        {
            get { return factStay; }
            set { factStay = value; }
        }

        public int Divergence
        {
            get { return divergence; }
            set { divergence = value; }
        }

        public int DivergenceDays
        {
            get { return divergenceDays; }
            set { divergenceDays = value; }
        }
    }
}
