using System;
using System.Data;
using System.Data.Common;

using XMIS.Report.Contract;

namespace XMIS.Report.Core.DAL
{
    public class DataAccessManager<T> : IDataAccessManager where T : DbConnection
    {
        private string connectionStringPattern = @"password='';user id='';Data Source='{0}';Integrated Security=True";
        private T connection;

        public /*async*/ void Connect(string directory)
        {
            if (this.Connected)
                throw new Exception("Already connected to database");

            try
            {
                var connectionString = string.Format(this.connectionStringPattern, directory);
                this.connection = (T)Activator.CreateInstance(typeof(T),connectionString);
                /*await*/ this.connection.Open/*Async*/();
            }
            catch (ArgumentException ex)
            {
                //wrong args of connection string
                throw new ArgumentException(ex.Message, ex);
            }
            catch (DbException ex)
            {
                //server is off
                throw new Exception(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                //connection string is null or empty
                throw new InvalidOperationException(ex.Message, ex);
            }

            if (this.connection == null)
                throw new FormatException("DB is not connected.");
        }

        public void Disconnect()
        {
            if (this.Connected)
                this.connection.Close();
                this.connection = null;
        }

        public bool Connected
        {
            get 
            { 
                return this.connection != null 
                ? this.connection.State == ConnectionState.Open 
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
