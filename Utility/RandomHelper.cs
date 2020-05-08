using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 随机相关
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static RandomHelper Instance { get; } = new RandomHelper();

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="min">最小数值</param>
        /// <param name="max">最大数值</param>
        /// <returns></returns>
        public int GetNumber(int min, int max)
        {
            var r = new Random();
            var result = r.Next(min, max);

            return result;
        }
        /// <summary>
        /// 获取随机数（字母和数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetString(int length)
        {
            if (length == 0) return "";
            var listStr = new List<string>();

            var r = new Random();
            System.Text.ASCIIEncoding ascii = new ASCIIEncoding();
            for (var i = 0; i < length; i++)
            {
                var number = r.Next(0, 3);
                switch (number)
                {
                    case 0: number = r.Next(48, 58); break;//0-9
                    case 1: number = r.Next(65, 91); break;//A-Z
                    default: number = r.Next(97, 123); break;//a-z
                }
                byte[] byteArray = new byte[] { (byte)number };
                listStr.Add(ascii.GetString(byteArray));
            }

            return string.Join("", listStr);
        }
    }
}
