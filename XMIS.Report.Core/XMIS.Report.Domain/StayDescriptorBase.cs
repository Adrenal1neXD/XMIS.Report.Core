using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class StayDescriptorBase
    {

        /// <summary>
        /// The number of bed days.
        /// </summary>
        private int bedDay;

        private int bedTypeId = 0;

        private int roomId = 0;

        private int roomTypeId = 0;

        /// <summary>
        /// The date of hospitalization.
        /// </summary>
        private DateTime inDate;

        /// <summary>
        /// The date of discharge.
        /// </summary>
        private DateTime outDate;

        public int BedDay
        {
            get { return bedDay; }
            set { bedDay = value; }
        }

        public DateTime InDate
        {
            get { return inDate; }
            set { inDate = value; }
        }

        public DateTime OutDate
        {
            get { return outDate; }
            set { outDate = value; }
        }
    }
}
