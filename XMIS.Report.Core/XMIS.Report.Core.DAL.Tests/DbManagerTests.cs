using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XMIS.Report.Core.DAL;
using System.Data.SqlClient;

namespace XMIS.Report.Core.DAL.Tests
{
    [TestFixture]
    public class DbManagerTests
    {
        /// <summary>
        /// CONNECT_STRING
        /// </summary>
        /// <param name="str"></param>
        /// 
        [TestCase("", ExpectedException = typeof(InvalidOperationException))]
        [TestCase("ssss", ExpectedException = typeof(ArgumentException))]
        [TestCase(@"Data Source=WIN-Q2I6UCG0G3J\SQQLSERVER;Integrated Security=True", ExpectedException = typeof(Exception))]
        public void Connect_WrongConnectionString(string str)
        {
            var dbr = new DbManager();
            dbr.Connect(str);
        }

        [TestCase(@"Data Source=WIN-Q2I6UCG0G3J\SQLSERVER;Integrated Security=True")]
        public void Connect_NormalString(string str)
        {
            var dbr = new DbManager();
            Assert.DoesNotThrow(() => dbr.Connect(str));
        }

        [TestCase]
        public void Connect_AfterDisconnect()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager();
            dbr.Connect(cs);
            dbr.Disconnect();
            Assert.DoesNotThrow(() => dbr.Connect(cs));
        }

        [TestCase(ExpectedException = typeof(Exception), ExpectedMessage = "Already connected to database")]
        public void Connect_DoubleConnect()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager();
            dbr.Connect(cs);
            dbr.Connect(cs);
        }

        /// <summary>
        /// DISCONNECT
        /// </summary>
        /// 
        [TestCase]
        public void Disconnect_Connected()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager();
            dbr.Connect(cs);

            Assert.DoesNotThrow(() => dbr.Disconnect());
        }

        [TestCase]
        public void Disconnect_NotConnected()
        {
            var dbr = new DbManager();
            dbr.Disconnect();
        }

        /// <summary>
        /// 
        /// </summary>
       [TestCase]
        public void d()
        {
           var dbm = new DbManager(new SqlConnection());
           dbm.Connect(ConnectionString);
           dbm.DoQuery(@"create table test(////)");
           dbm.DoQuery(@"insert int [XMISDB].[dbo].[test] values(///)");
           var res = dbm.DoQuery(@"select * from [XMISDB].[dbo].[test]");

           if (res != null)
           {
               Assert.IsTrue(res.Columns.Count == 0
                   && res.Rows.Count == 0
                   && res.Rows[0][0] == "ccc");

           }

           Assert.NotNull(res);
        }

        private string ConnectionString = @"Data Source=WIN-Q2I6UCG0G3J\SQLSERVER;Integrated Security=True";
    }
}
