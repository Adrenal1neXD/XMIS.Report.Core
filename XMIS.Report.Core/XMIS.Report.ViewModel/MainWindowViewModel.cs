using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using XMIS.Report.Core.BLL;
using XMIS.Report.Model;
using XMIS.Report.Domain;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Model.Extentions;

namespace XMIS.Report.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        public List<CheckedListItem<string>> FormNameCollection { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DstPath { get; set; }
        public ICommand SaveComm { get; set; }
        #endregion

        #region Local
        private readonly IDataConfiguration config;
        private readonly IReportController reportController;
        #endregion

        public MainWindowViewModel(IDataConfiguration config)
        {
            this.config = config;
            this.reportController = new ReportController(config);
            this.FromDate = DateTime.Now;
            this.ToDate = DateTime.Now;
            this.DstPath = this.config.DstPath;
            this.SaveComm = new Command(arg => SaveClickMethod());

            this.InitFormList();
        }

        #region Methods
        private void InitFormList()
        {
            this.FormNameCollection = new List<CheckedListItem<string>>();
            for (int i = 0; i < 100; i++)
                this.FormNameCollection.Add(new CheckedListItem<string>(string.Format("form {0}", i)));
        }

        private void SaveClickMethod()
        {
            foreach (string item in this.FormNameCollection.GetChecked())
                this.reportController.CreateReport(this.DstPath, item, this.FromDate, this.ToDate);
        }

        #endregion
    }
}
