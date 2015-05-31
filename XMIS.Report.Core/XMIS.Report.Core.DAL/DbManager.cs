using System;
using System.Data;
using System.Data.Common;

using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.DAL
{
    public class DbManager : IDbManager
    {
        private string connectionStringPattern = @"password='';user id='';Data Source='{0}';Integrated Security=True";
        private IDbConnection connection;
        private Type connType;

        public DbManager(IDbConnection conn)
        {
            this.connection = conn;
            this.connType = conn.GetType();
        }

        public DbManager()
        {
            this.connType = typeof(DbConnection);
        }

        public /*async*/ void Connect(string directory)
        {
            if (this.Connected)
                throw new Exception("Already connected to database");

            try
            {
                var connectionString = string.Format(this.connectionStringPattern, directory);
                if (directory == string.Empty)
                    throw new Exception("Directory argument of db connection is empty");

                if (this.connection == null)
                    try
                    {
                        this.connection = Activator.CreateInstance(this.connType, connectionString) as IDbConnection;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Can't create connection", ex);
                    }
                else
                    this.connection.ConnectionString = connectionString;

                /*await*/ this.connection.Open/*Async*/();
            }
            catch (DbException ex)
            {
                //server is off
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                //wrong connection string args
                throw new Exception(ex.Message, ex);
            }

            if (this.connection == null)
                throw new FormatException("DB is not connected.");
        }

        public void Disconnect()
        {
            if (this.Connected)
                this.connection.Close();
        }

        public bool Connected
        {
            get 
            { 
                return this.connection != null 
                ? this.connection.State == ConnectionState.Open
                || this.connection.State == ConnectionState.Fetching
                || this.connection.State == ConnectionState.Executing
                || this.connection.State == ConnectionState.Connecting
                : false; 
            }
        }

        public /*async*/ DataTable DoQuery(string query)
        {
            if (!this.Connected)
                throw new Exception("Database is not connected");

            var command = this.connection.CreateCommand();
            command.CommandText = query;

            try
            {
                var reader = command.ExecuteReader();
                if (reader == null)
                    return null;
                var dataTable = new DataTable();
                dataTable.Load(reader);
                reader.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
