using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class HttpHelper
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <returns></returns>
        public static string Get(string Url
            , string ContentType = "application/json; charset=utf-8"
            , string Accept = ""
            , string UserAgent = ""
            , Dictionary<string, string> dicHeaderKeyValue = null
            )
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = ContentType;
            if (!string.IsNullOrEmpty(Accept)) request.Accept = Accept;
            if (!string.IsNullOrEmpty(UserAgent)) request.UserAgent = UserAgent;
            if (dicHeaderKeyValue != null && dicHeaderKeyValue.Count > 0)
            {
                foreach (var kv in dicHeaderKeyValue)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
        public static HttpWebResponse GetResponse(string Url
            , string ContentType = "application/json; charset=utf-8"
            , string Accept = ""
            , string UserAgent = ""
            , Dictionary<string, string> dicHeaderKeyValue = null
            )
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);
            request.ContentType = ContentType;
            if (!string.IsNullOrEmpty(Accept)) request.Accept = Accept;
            if (!string.IsNullOrEmpty(UserAgent)) request.UserAgent = UserAgent;
            if (dicHeaderKeyValue != null && dicHeaderKeyValue.Count > 0)
            {
                foreach (var kv in dicHeaderKeyValue)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            var response = (HttpWebResponse)request.GetResponse();

            return response;
        }


        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="Data">请求数据（JSON或者XML需要直接转换成string类型）</param>
        /// <param name="ContentType">文件类型：application/x-www-form-urlencoded; charset=utf-8 | multipart/form-data; charset=utf-8 | application/json; charset=utf-8 | text/xml; charset=utf-8</param>
        /// <param name="cer">证书</param>
        /// <returns></returns>
        public static string Post(string Url, string Data
            , string ContentType = "application/json"
            , X509Certificate2 cer = null
            , string Accept = ""
            , string UserAgent = ""
            , Dictionary<string,string> dicHeaderKeyValue = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);

            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            var PostData = Encoding.UTF8.GetBytes(Data);

            if(!string.IsNullOrEmpty(Accept)) request.Accept = Accept;
            if (!string.IsNullOrEmpty(UserAgent)) request.UserAgent = UserAgent;
            if (dicHeaderKeyValue!=null && dicHeaderKeyValue.Count > 0)
            {
                foreach (var kv in dicHeaderKeyValue)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            request.Method = "POST";
            request.ContentType = ContentType;
            request.ContentLength = PostData.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(PostData, 0, PostData.Length);
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
        public static HttpWebResponse PostResponse(string Url, string Data
            , string ContentType = "application/json"
            , X509Certificate2 cer = null
            , string Accept = ""
            , string UserAgent = ""
            , Dictionary<string, string> dicHeaderKeyValue = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(Url);

            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            var PostData = Encoding.UTF8.GetBytes(Data);

            if (!string.IsNullOrEmpty(Accept)) request.Accept = Accept;
            if (!string.IsNullOrEmpty(UserAgent)) request.UserAgent = UserAgent;
            if (dicHeaderKeyValue != null && dicHeaderKeyValue.Count > 0)
            {
                foreach (var kv in dicHeaderKeyValue)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }
            request.Method = "POST";
            request.ContentType = ContentType;
            request.ContentLength = PostData.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(PostData, 0, PostData.Length);
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            return response;
        }

        /// <summary>
        /// 生成证书类型
        /// </summary>
        /// <param name="path">证书存储路径</param>
        /// <param name="password">证书密码</param>
        /// <returns></returns>
        public static X509Certificate2 CreateCert(string path, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //X509Certificate cer = new X509Certificate(cert, password);
            X509Certificate2 cer = new X509Certificate2(path, password, X509KeyStorageFlags.MachineKeySet);

            return cer;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
    }
}
