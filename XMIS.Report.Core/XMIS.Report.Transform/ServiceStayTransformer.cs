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
    public class ServiceStayTransformer : IServiceDescriptorUnitTransformer
    {
        public ServiceDescriptorBase Transform(ServiceDescriptorBase serviceDescriptor, DataRow dataRow)
        {
            serviceDescriptor.Stay = this.GetServiceStay(dataRow);

            return serviceDescriptor;
        }

        private ServiceStayBase GetServiceStay(DataRow dataRow)
        {
            ServiceStayBase stay = new ServiceStay();
            stay.PlannedStay = this.GetPlannedStay(dataRow);
            stay.FactStay = this.GetFactStay(dataRow);
            stay.Divergence = stay.PlannedStay.BedDay != stay.FactStay.BedDay ? 1 : 0;
            stay.DivergenceDays = stay.FactStay.BedDay - stay.PlannedStay.BedDay;
            return stay;
        }

        private StayDescriptorBase GetPlannedStay(DataRow dataRow)
        {
            try
            {
                StayDescriptorBase stayDescriptor = new StayDescriptor();

                stayDescriptor.OutDate = dataRow["d_outplan"].ToString().ToDateTime(12 * 60);
                stayDescriptor.InDate = dataRow["d_posplan"].ToString().ToDateTime(12 * 60);

                stayDescriptor.BedDay = stayDescriptor.OutDate.Subtract(stayDescriptor.InDate).Days;

                return stayDescriptor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StayDescriptorBase GetFactStay(DataRow dataRow)
        {
            try
            {
                StayDescriptorBase stayDescriptor = new StayDescriptor();

                stayDescriptor.OutDate = dataRow["out_d"].ToString().ToDateTime(12 * 60);
                stayDescriptor.InDate = dataRow["pos_d"].ToString().ToDateTime(12 * 60);

                stayDescriptor.BedDay = stayDescriptor.OutDate.Subtract(stayDescriptor.InDate).Days;

                return stayDescriptor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
