using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Transform.Extentions
{
    public static class TransformExtentions
    {
        public static DateTime ToDateTime(this string date, int minutes)
        {
            try
            {
                DateTime result = DateTime.Parse(date);
                result = new DateTime(result.Year, result.Month, result.Day);
                result = result.AddMinutes(minutes);

                return result;
            }
            catch (Exception ex)
            {
                //Logger.Log.Error(date + " : " + ex.Message, ex);
                throw ex;
            }
        }

        public static DateTime ToDateTime(this string date)
        {
            return date.ToDateTime(0);
        }

        public static int ToMinutes(this string time)
        {
            try
            {
                int result = 0;
                if (time.Trim() == "." || time.Trim() == String.Empty)
                {
                    time = "0.0";
                }

                string[] timePair = time.Split('.');

                result = timePair[0].ToNumber() * 60 + timePair[1].ToNumber();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int ToNumber(this string s)
        {
            int result = 0;
            try
            {
                if (s.Trim() != string.Empty)
                {
                    result = Convert.ToInt32(s);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
