using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL
{
    public class PatientDescriptorDataManager
    {
//        /// <summary>
//        /// The SQL data helper.
//        /// </summary>
//        private IDataAccessManager dataAccessManager;

//        /// <summary>
//        /// The data configuration.
//        /// </summary>
//        private readonly DataConfiguration dataConfiguration = new DataConfiguration();

//        /// <summary>
//        /// The service descriptor transformer.
//        /// </summary>
//        private readonly ServiceDescriptorTransformer serviceDescriptorTransformer = new ServiceDescriptorTransformer();

//        /// <summary>
//        /// Initializes a new instance of the <see cref="ServiceDescriptorDataManager"/> class.
//        /// </summary>
//        public PatientDescriptorDataManager()
//        {
//            this.dataConfiguration.ReadConfiguration();
            
//            //getting type of db connection
//            if (this.dataConfiguration.DbConnectionType == typeof(SqlConnection))
//                this.dataAccessManager = new DataAccessManager<SqlConnection>();
//            else
//                this.dataAccessManager = new DataAccessManager<OleDbConnection>();
//        }

//        /// <summary>
//        /// The get service descriptor collection.
//        /// </summary>
//        /// <param name="condition">
//        /// The condition.
//        /// </param>
//        /// <returns>
//        /// The <see>
//        ///       <cref>List</cref>
//        ///     </see> .
//        /// </returns>
//        public IList<ServiceDescriptorBase> GetServiceDescriptorCollection(string condition)
//        {
//            IList<ServiceDescriptorBase> serviceDescriptorCollection;
//            DataTable serviceDataTable = this.GetData(this.dataConfiguration.Path, condition);
//            serviceDescriptorCollection = this.TransformToServiceCollection(serviceDataTable);

//            return serviceDescriptorCollection;
//        }

//        /// <summary>
//        /// The get service collection.
//        /// </summary>
//        /// <param name="directoryName">
//        /// The directory name.
//        /// </param>
//        /// <param name="suffix">
//        /// The suffix.
//        /// </param>
//        /// <returns>
//        /// The <see cref="IList"/>.
//        /// </returns>
//        /// <exception cref="Exception">
//        /// </exception>
//        public IList<ServiceDescriptorBase> TransformToServiceCollection(DataTable serviceDataTable)
//        {
//            IList<ServiceDescriptorBase> serviceCollection = new List<ServiceDescriptorBase>();

//            try
//            {
//                foreach (DataRow dataRow in serviceDataTable.Rows)
//                {
//                    ServiceDescriptorBase serviceDescriptor = this.serviceDescriptorTransformer.Transform(dataRow);
//                    serviceCollection.Add(serviceDescriptor);
//                }

//                return serviceCollection;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        /// <summary>
//        /// The get data.
//        /// </summary>
//        /// <param name="directoryName">
//        /// The directory name.
//        /// </param>
//        /// <param name="suffix">
//        /// The suffix.
//        /// </param>
//        /// <returns>
//        /// The <see cref="DataTable"/>.
//        /// </returns>
//        private DataTable GetData(string directoryName, string suffix)
//        {
//            // string additional = " and opdict.u1code = 1";
//            string command1 =
//                @"select  a.nib, a.tnib, d.d_kmkb10, a.pac_fio, a.pac_oper, a.pac_sex, a.pos_d, a.out_d, a.pac_born, a.d_posplan, a.t_posplan, 
//                                a.d_outplan, str(a.m_wight, 7,2) as m_weight, str(a.m_hight, 7, 2) as m_height, a.oblcode,
//                                o.xcode, o.koddop, o.dateoper, o.time_began, o.lenoper, o.nsicode, o.typeop, o.simcode1, o.simcode2, o.simcode3,
//                                op.kdocplan, op.d_operplan, op.operlenght, op.codeopplan, op.typeop as ptypeop, opdict.u1code as u1, 
//                                opdict.u2code as u2,opdict.diffctcode as diffctcode,opdict.u1name,opdict.u2name, opdict.u2grcode from so_pos a, diagnosis d, cenoper o, cenoperpl op, p_operu2 opdict
//                                where o.nib = a.nib and d.nib = a.nib and d.type = 2 and !empty(pos_d) and 
//                                      !empty(pac_born) and op.nib = a.nib and !EMPTY(a.dateoper) and 
//                                       opdict.u2code = op.codeopplan {0}";

//            string command3 =
//               @"select  opdict.u1code as u1, opdict.u2code as u2,opdict.u1name,opdict.u2name
//                                from p_operu2 opdict {0}";


//            DataTable queryTable = this.dataAccessManager.DoQuery(directoryName, String.Format(command1, suffix));
//            queryTable.TableName = "query";

//            //To save intermediate data use queryTable.WriteXml("C:\\_s\\1.xml");

//            //queryTable.WriteXml("C:\\_s\\1.xml");

//            return queryTable;
//        }
    }
}
