using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

using XMIS.Report.Domain;
using XMIS.Report.Core.Processor.Contract;
using XMIS.Report.Core.Processor;
using XMIS.Report.Core.Processor.Condition;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.DAL;

namespace XMIS.Report.Core.BLL.FormFactory
{
    public abstract class FormFactory
    {
        protected readonly IConditionController conditionController;
        protected readonly IExcelApp excelApp;
        protected readonly IExcelDataWriter dataWriter;
        protected int tableWidth;
        protected IQueryProcessor queryProcessor;
        protected DateTime fromDate;
        protected DateTime toDate;

        public FormFactory()
        {
            this.conditionController = new ConditionController();
            this.excelApp = new ExcelApp();
            this.dataWriter = new ExcelDataWriter(this.excelApp.Cells);
        }

        public abstract IExcelApp GetApp(List<ServiceDescriptorBase> serv, DateTime fromDate, DateTime toDate);
    }
}
