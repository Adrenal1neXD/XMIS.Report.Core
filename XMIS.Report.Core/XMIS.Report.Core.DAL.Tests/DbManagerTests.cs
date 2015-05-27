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
        [TestCase("", ExpectedException = typeof(Exception))]
        [TestCase("ssss", ExpectedException = typeof(Exception))]
        [TestCase(@"WIN-Q2I6UCG0G3J\SQQLSERVER", ExpectedException = typeof(Exception))]
        public void Connect_SqlConn_WrongConnectionString(string str)
        {
            var dbr = new DbManager(new SqlConnection());
            dbr.Connect(str);
            dbr.Disconnect();
        }

        [TestCase(@"WIN-Q2I6UCG0G3J\SQLSERVER")]
        public void Connect_SqlConn_NormalString(string str)
        {
            var dbr = new DbManager(new SqlConnection());
            Assert.DoesNotThrow(() => dbr.Connect(str));
        }

        [TestCase]
        public void Connect_SqlConn_AfterDisconnect()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager(new SqlConnection());
            dbr.Connect(cs);
            dbr.Disconnect();
            Assert.DoesNotThrow(() => dbr.Connect(cs));
        }

        [TestCase(ExpectedException = typeof(Exception), ExpectedMessage = "Already connected to database")]
        public void Connect_SqlConn_DoubleConnect()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager(new SqlConnection());
            dbr.Connect(cs);
            dbr.Connect(cs);
            dbr.Disconnect();
        }

        /// <summary>
        /// DISCONNECT
        /// </summary>
        /// 
        [TestCase]
        public void Disconnect_Connected()
        {
            var cs = this.ConnectionString;
            var dbr = new DbManager(new SqlConnection());
            dbr.Connect(cs);

            Assert.DoesNotThrow(() => dbr.Disconnect());
        }

        [TestCase]
        public void Disconnect_NotConnected()
        {
            var dbr = new DbManager(new SqlConnection());
            Assert.DoesNotThrow(() => dbr.Disconnect());
        }

        /// <summary>
        /// 
        /// </summary>
       [TestCase]
        public void DoQuery_SqlConn_NormalResult()
        {
           var dbm = new DbManager(new SqlConnection());
           dbm.Connect(ConnectionString);
           dbm.DoQuery(@"create table test(id int primary key, name varchar(255), age int)");
           dbm.DoQuery(@"insert into [XMISDB].[dbo].[test] values(1, 'Name1', 22)");
           dbm.DoQuery(@"insert into [XMISDB].[dbo].[test] values(2, 'Name2', 43)");
           dbm.DoQuery(@"insert into [XMISDB].[dbo].[test] values(3, 'Name3', 15)");
           var res = dbm.DoQuery(@"select * from [XMISDB].[dbo].[test]");

           if (res != null)
           {
               var assertResult = res.Columns.Count == 0
                                    && res.Rows.Count == 0
                                    && res.Rows[1][0] == "2"
                                    && res.Rows[1][2] == "Name2"
                                    && res.Rows[1][3] == "15";


               dbm.DoQuery(@"drop table [XMISDB].[dbo].[test]");
               dbm.Disconnect();
               Assert.IsTrue(assertResult);

           }

           Assert.NotNull(res);
        }

        private string ConnectionString = @"WIN-Q2I6UCG0G3J\SQLSERVER";
    }
}
