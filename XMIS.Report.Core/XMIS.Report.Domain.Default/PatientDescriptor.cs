using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Domain;

namespace XMIS.Report.Domain.Default
{
    public class PatientDescriptor : PatientDescriptorBase
    {
        public override DateTime Dob
        {
            get
            {
                return base.Dob;
            }
            set
            {
                base.Dob = value; 
                DateTime today = DateTime.Today;
                int age = today.Year - Dob.Year;
                if (Dob > today.AddYears(-age)) age--;
                this.Age = age;

                var goupMod = this.Age / 10f;
                this.AgeGroup = (int)Math.Ceiling(goupMod) - 1;
                this.AgeSubGroup = ((goupMod - (int)goupMod) <= .5) 
                    ? (int)goupMod + 1 
                    : (int)goupMod + 2;
            }
        }
    }
}
