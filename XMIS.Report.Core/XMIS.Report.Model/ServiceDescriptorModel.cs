using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMIS.Report.Domain;

namespace XMIS.Report.Model
{
    public class ServiceDescriptorModel : ModelBase
    {
        #region Fields

        private ServiceDescriptorBase serviceDescriptor;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set first name.
        /// </summary>
        public ServiceDescriptorBase ServiceDescriptor
        {
            get { return this.serviceDescriptor; }
            set
            {
                if (this.serviceDescriptor != value)
                {
                    this.serviceDescriptor = value;
                    OnPropertyChanged("ServiceDescriptor");
                }
            }
        }

        #endregion
    }
}
