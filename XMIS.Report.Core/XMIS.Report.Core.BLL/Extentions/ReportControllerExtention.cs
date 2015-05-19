using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.BLL.Extentions
{
    public static class ReportControllerExtention
    {
        public static string FindFromDate(this string src)
        {
            var res = FindValue(src, "from_date");
            return (res == "any" 
                ? new DateTime().ToShortDateString() 
                : res == string.Empty 
                ? new DateTime().ToShortDateString() 
                : res) 
                ?? new DateTime().ToShortDateString();
        }

        public static string FindToDate(this string src)
        {
            var res = FindValue(src, "to_date");
            return (res == "today" 
                ? DateTime.Today.ToShortDateString() 
                : res == string.Empty 
                ? DateTime.Today.ToShortDateString()
                : res) 
                ?? DateTime.Today.ToShortDateString();
        }

        public static string FindCondition(this string src)
        {
            return FindValue(src, "condition");
        }

        public static string FindType(this string src)
        {
            return FindValue(src, "type");
        }

        public static string FindResult(this string src)
        {
            return FindValue(src, "result");
        }

        private static string FindValue(string src, string valName)
        {
            if (src.Contains('@'))
            {
                var tokens = src.Split(',');
                
                foreach (string substr in tokens)
                {
                    string[] tmp = substr.Trim().Split('@');
                    if (tmp != null)
                    {
                        if (tmp.Count() > 1)
                        {
                            if (tmp[0] == valName)
                                return tmp[1];
                        }
                        else
                            return null;
                    }
                }
            }
            return null;
        }
    }
}
