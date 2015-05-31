using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Practices.Unity;

using XMIS.Report.Core.DAL;
using XMIS.Report.Transform;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Default;
using XMIS.Report.Core.BLL.Extentions;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.BLL.FormFactory;
using XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts;
using System.Data.OleDb;
using System.Data.Common;


namespace XMIS.Report.Core.BLL
{
    public class ReportController : IReportController
    {
        private readonly IDataConfiguration config;
        private readonly IDbManager dataAccessManager;
        private readonly List<DepartmentDescriptorBase> descriptorCollection;
        
        public ReportController(IDataConfiguration config)
        {
            this.config = config;

            IUnityContainer container = new UnityContainer()
                .RegisterType<IDbConnection, OleDbConnection>(new InjectionConstructor())
                .RegisterType<IDbConnection, SqlConnection>(new InjectionConstructor())
                .RegisterType<DbManager>();

            //sql conn
            try
            { 
                this.dataAccessManager = container.Resolve<DbManager>();
                this.dataAccessManager.Connect(this.config.DBConnectionPath);
            }
            catch (Exception ex)
            {

            }
            //

            //to do sql helper
            var dbdata = this.dataAccessManager.DoQuery("Select * from MyPerson");
            this.descriptorCollection = this.GetTransformedCollection(dbdata);
            //
        }

        public void CreateReport(string path, string formName, DateTime fromDate, DateTime toDate)
        {
            var factory = this.GetFactory(formName);
            if (factory == null)
                return;
            var x = factory.GetApp(this.descriptorCollection, fromDate, toDate);

            x.SaveAs(string.Format(@"{0}\{1}", this.config.DstPath, formName));
        }

        private dynamic GetFactory(string formName)
        {
            switch(formName)
            {
                case "form 7":
                    return new AppFactory<Form7_MakeText, Form7_MakeData, Form7_MakeBorders, Form7_MakeFormat>(18);
                case "form 7x":
                    return new AppFactory<Form7x_MakeText, Form7x_MakeData, Form7x_MakeBorders, Form7x_MakeFormat>(29);
                case "form 16x":
                    return new AppFactory<Form16x_MakeText, Form16x_MakeData, Form16x_MakeBorders, Form16x_MakeFormat>(23);
                default:
                    return null;
            }
        }

        private List<DepartmentDescriptorBase> GetTransformedCollection(DataTable data)
        {
            
            List<DepartmentDescriptorBase> descriptorCollection = new List<DepartmentDescriptorBase>();

            int[] depatmentIdxs = new int[] { 4, 5, 77 };

            foreach (int idx in depatmentIdxs)
                descriptorCollection.Add(new DepartmentDescriptorTransform(idx).Transform(data));
            
            return descriptorCollection;
        }
    }
}
