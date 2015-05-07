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
    public class DataBaseReaderTests
    {
        [TestCase("", ExpectedException = typeof(InvalidOperationException))]
        [TestCase("ssss", ExpectedException = typeof(ArgumentException))]
        [TestCase(@"Data Source=WIN-Q2I6UCG0G3J\SQQLSERVER;Integrated Security=True", ExpectedException = typeof(Exception))]
        public void Connect_WrongConnectionString(string str)
        {
            var dbr = new DataAccessManager<SqlConnection>();
            dbr.Connect(str);
        }
    }
}
