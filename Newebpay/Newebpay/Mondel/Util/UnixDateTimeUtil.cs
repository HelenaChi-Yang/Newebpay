using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newebpay.Mondel.Util
{
    public class UnixDateTimeUtil
    {
        //Unix起始時間
        private static DateTime BaseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// 轉換C#的DateTime格式為UNIX時戳格式(從 Unix 纪元到當前時間的秒數)
        /// </summary>
        /// <returns>UNIX時戳格式</returns>
        public static string GetUNIX()
        {
            Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp.ToString();
        }
    }
}
