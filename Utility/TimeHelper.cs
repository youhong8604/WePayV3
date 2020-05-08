using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 时间操作工具
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }
        /// <summary>
        /// 获取UTC时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetUtcTimeStamp()
        {
            return GetTimeStamp(DateTime.Now.ToUniversalTime());
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime dt)
        {
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
