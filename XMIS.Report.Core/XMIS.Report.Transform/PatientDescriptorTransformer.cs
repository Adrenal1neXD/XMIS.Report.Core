using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using XMIS.Report.Domain;
using XMIS.Report.Transform.Contract;
using XMIS.Report.Domain.Default;
using XMIS.Report.Transform.Extentions;

namespace XMIS.Report.Transform
{
    public class PatientDescriptorTransformer : IServiceDescriptorUnitTransformer
    {
        public ServiceDescriptorBase Transform(ServiceDescriptorBase serviceDescriptor, DataRow dataRow)
        {
            PatientDescriptorBase patientDescriptor = new PatientDescriptor();
            patientDescriptor.Dob = dataRow["pac_born"].ToString().ToDateTime();
            patientDescriptor.RegionId = Convert.ToInt32(dataRow["oblcode"].ToString());
            patientDescriptor.Gender = Convert.ToInt32(dataRow["pac_sex"]);

            serviceDescriptor.Patient = patientDescriptor;
            return serviceDescriptor;
        }
    }
}
