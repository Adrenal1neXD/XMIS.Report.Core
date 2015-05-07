using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Transform.Extenstions
{
        /// <summary>
    /// The transform extension.
    /// </summary>
    public static class TransformExtension
    {
        /// <summary>
        /// The to date time.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="minutes">
        /// The minutes.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Invalid date time format for : [date] [minutes]
        /// </exception>
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

        /// <summary>
        /// The get time minutes from legacy fox format of numbers separated by comma[12.00].
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <returns>
        /// A number of minutes <see cref="int"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Invalid time string : [time]. 
        /// </exception>
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

        /// <summary>
        /// The to number.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The number <see cref="int"/>.
        /// </returns>
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
