using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace XMIS.Report.Core.DAL
{
    public class DataConfiguration
    {
        private string dbConnectionPath= @"WIN-Q2I6UCG0G3J\XMISSQLSERVER";
        private string srcPath = @"D:\Work\1\res";
        private string dstPath = @"D:\Work\1\dst";
        private Type dbConnectionType = typeof(SqlConnection);

        public string DBConnectionPath
        {
            get
            {
                return this.dbConnectionPath;
            }
            set
            {
                this.dbConnectionPath = value;
            }
        }
        public string SrcPath
        {
            get
            {
                return this.srcPath;
            }
            set
            {
                this.srcPath = value;
            }
        }
        public string DstPath
        {
            get
            {
                return this.dstPath;
            }
            set
            {
                this.dstPath = value;
            }
        }
        public Type DbConnectionType
        {
            get
            {
                return this.dbConnectionType;
            }
            set
            {
                this.dbConnectionType = value;
            }
        }

        public void ReadConfiguration()
        {
            try
            {
                this.DBConnectionPath = ConfigurationManager.AppSettings["DbConnectionPath"].Trim();
                this.SrcPath = ConfigurationManager.AppSettings["SrcFilesPath"].Trim();
                this.DstPath = ConfigurationManager.AppSettings["DstFilesPath"].Trim();

                if ((this.DbConnectionType = Type.GetType(ConfigurationManager.AppSettings["DbConnectionType"].Trim())) == null)
                    this.DbConnectionType = typeof(SqlConnection);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find configuration. Check your app.config file.", ex);
            }
        }
    }
}
