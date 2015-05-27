using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using XMIS.Report.ViewModel;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.DAL;

namespace XMIS.Report.View
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataConfiguration config = new DataConfiguration();

        public App()
        {
            
            var mw = new MainWindow
            {
                DataContext = new MainWindowViewModel(config)
            };

            mw.Show();
        }
    }
}
