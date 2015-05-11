using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Default;
using XMIS.Report.Transform.Extentions;

namespace XMIS.Report.Transform
{
    public class ServiceDescriptorTransformer : IServiceDescriptorTransformer
    {
         /// <summary>
        /// The transformer collection organized pipeline. Each transformer implements the interface.
        /// </summary>
        private List<IServiceDescriptorUnitTransformer> transformerPipeline = new List<IServiceDescriptorUnitTransformer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDescriptorTransformer"/> class.
        /// </summary>
        public ServiceDescriptorTransformer()
        {
            this.transformerPipeline.Add(new ServiceStayTransformer());
            this.transformerPipeline.Add(new PatientDescriptorTransformer());
        }

        /// <summary>
        /// The transform.
        /// </summary>
        /// <param name="dataRow">
        /// The data row.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceDescriptorBase"/>.
        /// </returns>
        public ServiceDescriptorBase Transform(DataRow dataRow)
        {
            try
            {
                ServiceDescriptorBase serviceDescriptor = new ServiceDescriptor();
                serviceDescriptor.HospitalizationId = dataRow["nib"].ToString().ToNumber();
                serviceDescriptor.InDate = dataRow["pos_d"].ToString().ToDateTime();
                serviceDescriptor.OutDate = dataRow["out_d"].ToString().ToDateTime();

                foreach (IServiceDescriptorUnitTransformer serviceDescriptorUnitTransformer in this.transformerPipeline)
                {
                    serviceDescriptorUnitTransformer.Transform(serviceDescriptor, dataRow);
                }

                return serviceDescriptor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
