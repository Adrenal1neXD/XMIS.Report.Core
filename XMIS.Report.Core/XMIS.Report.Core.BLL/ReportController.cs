﻿using System;
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
using XMIS.Report.Core.Processor.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Default;
using XMIS.Report.Core.Processor.Condition;
using XMIS.Report.Core.Processor;
using XMIS.Report.Core.BLL.Extentions;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.BLL.FormFactory;
using XMIS.Report.Core.BLL.FormFactory.Forms;
using System.Data.OleDb;
using System.Data.Common;


namespace XMIS.Report.Core.BLL
{
    public class ReportController : IReportController
    {
        private readonly IDataConfiguration config;
        private readonly IDbManager dataAccessManager;
        private readonly List<ServiceDescriptorBase> descriptorCollection;
        
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
            var dbdata = this.dataAccessManager.DoQuery("Select * from [XMISDB].[dbo].[Person]");
            this.descriptorCollection = this.GetTransformedCollection(dbdata);
            //
        }

        public void CreateReport(string path, string formName, DateTime fromDate, DateTime toDate)
        {
            FormFactory.FormFactory factory = this.GetFactory(formName);
            if (factory == null)
                return;
            var x = factory.GetApp(this.descriptorCollection, fromDate, toDate);

            x.SaveAs(string.Format(@"{0}\{1}", this.config.DstPath, formName));
        }

        private FormFactory.FormFactory GetFactory(string formName)
        {
            //
            switch(formName)
            {
                case "form 7":
                    return new Form7x();
                default:
                    return null;
            }
        }

        private string HandleValue(string value)
        {
            //var result = this.GetDictionary(value);

            //if (result == null)
            //    return null;

            //object condition;
            //object type;
            //object from;
            //object to;
            //object res;

            //result.TryGetValue("condition", out condition);
            //result.TryGetValue("type", out type);
            //result.TryGetValue("from", out from);
            //result.TryGetValue("to", out to);
            //result.TryGetValue("result", out res);

            //var func = this.conditionController.GetConditionFunction(
            //        (string)condition,
            //        (string)type,
            //        (DateTime)from,
            //        (DateTime)to);

            //IQueryProcessor query = new QueryProcessor(descriptorCollection);
            //var queryRes = query.DoQuery(func);

            //if ((res as string) == "count")
            //    return queryRes.Count.ToString();

            //var resComm = "age";
            //var resAction = "count";
            //if ((res as string).Contains(':'))
            //{
            //    resComm = (res as string).Split(':')[0];
            //    resAction = (res as string).Split(':')[1];
            //}

            //Func<ServiceDescriptorBase, string> resultFunc;
            //this.resultDictionary.TryGetValue(resComm, out resultFunc);


            //if (resultFunc == null)
            //    return string.Empty;

            //string finalResult = string.Empty;

            //switch(resAction)
            //{
            //    case "sum":
            //        int cnt = 0;
            //        for (int i = 0; i < queryRes.Count; i++)
            //            cnt += Convert.ToInt32(resultFunc(queryRes[i]));
            //        finalResult = cnt.ToString();
            //            break;
            //    case "count":
            //            finalResult = queryRes.Count.ToString();
            //            break;
            //    case "value":
            //        for (int i = 0; i < queryRes.Count; i++)
            //            finalResult += " " + resultFunc(queryRes[i]);
            //        break;
            //}

            //return finalResult;
            return "";
        }

        //private Dictionary<string, object> GetDictionary(string srcStr)
        //{
        //    var src = srcStr.Trim('$');
        //    DateTime intervalFrom;
        //    DateTime intervalTo;

        //    try 
        //    { 
        //        intervalFrom = DateTime.Parse(src.FindFromDate());
        //        intervalTo = DateTime.Parse(src.FindToDate());
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        throw new Exception("Wrong DateTime format", ex);
        //    }

        //    string condition = src.FindCondition();
        //    string type = src.FindType();
        //    string res = src.FindResult();

        //    if (intervalFrom != null 
        //        && intervalTo != null
        //        && condition != null
        //        && res != null
        //        && type != null)
        //    {
        //        Dictionary<string, object> result = new Dictionary<string, object>();
        //        result.Add("from", intervalFrom);
        //        result.Add("to", intervalTo);
        //        result.Add("condition", condition);
        //        result.Add("type", type);
        //        result.Add("result", res);

        //        return result;
        //    }

        //    return null;
        //}

        private List<ServiceDescriptorBase> GetTransformedCollection(DataTable data)
        {
            var service = new ServiceDescriptorTransformer();
            List<ServiceDescriptorBase> descriptorCollection = new List<ServiceDescriptorBase>();

            foreach (DataRow row in data.AsEnumerable())
                descriptorCollection.Add(service.Transform(row));

            return descriptorCollection;
        }
    }
}
