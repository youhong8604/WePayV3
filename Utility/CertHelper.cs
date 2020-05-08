using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 证书操作类
    /// </summary>
    public class CertHelper
    {

        /// <summary>
        /// 获取秘钥字符串
        /// </summary>
        /// <param name="keyPath">私钥文件路径，例如：C:\\inetpub\\cert\\apiclient_key.pem</param>
        /// <returns></returns>
        public static string GetKeyStringFromCert(string keyPath)
        {
            var key = "";
            using (FileStream fs = File.Open(keyPath, FileMode.Open, FileAccess.Read))
            {
                StreamReader objsr = new StreamReader(fs, Encoding.UTF8);
                key = objsr.ReadToEnd();
            }
            key = key.Replace("-----BEGIN PRIVATE KEY-----", "")
                .Replace("-----END PRIVATE KEY-----", "")
                .Replace("\n", "");
            return key;
        }
        /// <summary>
        /// 保存证书
        /// </summary>
        /// <param name="keyStr">key字符串</param>
        /// <param name="certPath">保存路径，...pem</param>
        public static void SaveKeyToCert(string keyStr,string certPath)
        {
            StreamWriter sw = new System.IO.StreamWriter(certPath, true);
            sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);
            sw.WriteLine(keyStr);
            sw.Flush();
            sw.Close();
        }
    }
}
