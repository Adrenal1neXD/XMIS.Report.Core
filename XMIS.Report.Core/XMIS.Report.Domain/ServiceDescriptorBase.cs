using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain
{
    public class ServiceDescriptorBase
    {
        private Guid id;
        private int hospitalizationId;
        private DateTime inDate;
        private DateTime outDate;
        private int days;
        private int type;
        private int processed = 0;

        /// <summary>
        /// The patient.
        /// </summary>
        private PatientDescriptorBase patient;

        /// <summary>
        /// The operation.
        /// </summary>
        //private ServiceOperationBase operation;

        /// <summary>
        /// The stay.
        /// </summary>
        private ServiceStayBase stay;
        //private ServiceExpensesBase expenses;
        //private DiagnosisDescriptorBase diagnosis;

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

        public int HospitalizationId
        {
            get { return hospitalizationId; }
            set { hospitalizationId = value; }
        }

        public PatientDescriptorBase Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        //public ServiceOperationBase Operation
        //{
        //    get { return operation; }
        //    set { operation = value; }
        //}

        public ServiceStayBase Stay
        {
            get { return stay; }
            set { stay = value; }
        }

        //public ServiceExpensesBase Expenses
        //{
        //    get { return expenses; }
        //    set { expenses = value; }
        //}

        //public DiagnosisDescriptorBase Diagnosis
        //{
        //    get { return diagnosis; }
        //    set { diagnosis = value; }
        //}

        public int Days
        {
            get { return days; }
            set { days = value; }
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Processed
        {
            get { return processed; }
            set { processed = value; }
        }
    }
}
