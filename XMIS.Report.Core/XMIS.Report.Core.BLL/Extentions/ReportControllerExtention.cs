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
            return FindValue(src, "from_date");
        }

        public static string FindToDate(this string src)
        {
            return FindValue(src, "to_date");
        }

        public static string FindCondition(this string src)
        {
            return FindValue(src, "condition");
        }

        public static string FindType(this string src)
        {
            return FindValue(src, "type");
        }

        public static string FindGroup(this string src)
        {
            return FindValue(src, "group");
        }

        private static string FindValue(string src, string valName)
        {
            if (src.Contains('@'))
            {
                var tokens = src.Split(',');
                
                foreach (string substr in tokens)
                {
                    string[] tmp = substr.Trim().Split('@');
                    if (tmp != null && tmp.Length == 2)
                    {
                        if (tmp[0] == valName)
                            return tmp[1];
                    }
                }
            }
            return null;
        }
    }
}
