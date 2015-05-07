using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class PatientDescriptorBase
    {
        private int age;
        private DateTime dob;
        private int ageGroup;
        private int ageSubGroup;
        private int gender;

        private int regionId;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int AgeGroup
        {
            get { return ageGroup; }
            set { ageGroup = value; }
        }

        public virtual DateTime Dob
        {
            get { return dob; }
            set { dob = value;}
        }

        public int Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int AgeSubGroup
        {
            get { return ageSubGroup; }
            set { ageSubGroup = value; }
        }

        public int RegionId
        {
            get
            {
                return this.regionId;
            }
            set
            {
                this.regionId = value;
            }
        }
    }
}
