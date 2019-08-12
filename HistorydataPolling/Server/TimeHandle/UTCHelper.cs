using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisplayHelper
{
    /// <summary>
    /// UTC时间转换帮助类
    /// </summary>
    public class UTCHelper
    {
        /// <summary>
        /// DateTime类型时间转化为UTC时间（int类型）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeToInt(DateTime time) 
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToUniversalTime(new DateTime(1970,1,1));
            intResult = (time - startTime).TotalSeconds;
            return (int)intResult;
        }

        /// <summary>
        /// UTC时间转化为
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static DateTime ConvertIntToDateTime(double utc)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToUniversalTime(new DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(utc);
            startTime = startTime.AddHours(8);
            return startTime;
        }

    }
}
