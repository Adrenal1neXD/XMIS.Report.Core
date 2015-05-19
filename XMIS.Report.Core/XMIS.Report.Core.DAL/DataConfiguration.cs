using System;
using System.Configuration;

using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.DAL
{
    public class DataConfiguration : IDataConfiguration
    {
        private string dbConnectionPath= @"WIN-Q2I6UCG0G3J\SQLSERVER";
        private string srcPath = @"D:\Work\1\res";
        private string dstPath = @"D:\Work\1\dst";

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

        public void ReadConfiguration()
        {
            try
            {
                this.DBConnectionPath = this.RemoveLastSlash(ConfigurationManager.AppSettings["DbConnectionPath"].Trim());
                this.SrcPath = this.RemoveLastSlash(ConfigurationManager.AppSettings["SrcFilesPath"].Trim());
                this.DstPath = this.RemoveLastSlash(ConfigurationManager.AppSettings["DstFilesPath"].Trim());
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find configuration. Check your app.config file.", ex);
            }
        }

        private string RemoveLastSlash(string path)
        {
            if (path[path.Length - 1] == '\\')
                return path.Remove(path.Length - 1, 1);
            else
                return path;
        }
    }
}
