using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Transform;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Default;
using XMIS.Report.Transform.Extenstions;
using XMIS.Report.Contract;

namespace XMIS.Report.Transform
{
    public class PatientDescriptorTransformer : IDescriptorTransformer
    {
        public dynamic Transform(DataRow dataRow)
        {
            PatientDescriptorBase patientDescriptor = new PatientDescriptor();
            patientDescriptor.Dob = dataRow["pac_born"].ToString().ToDateTime();
            patientDescriptor.RegionId = Convert.ToInt32(dataRow["oblcode"].ToString());
            patientDescriptor.Gender = Convert.ToInt32(dataRow["pac_sex"]);

            return patientDescriptor;
        }
    }
}
