using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 微信支付相关
    /// </summary>
    public partial class WeixinHelper
    {
        /// <summary>
        /// 下载平台证书
        /// </summary>
        public void DownloadPlatCertificates()
        {
            var mchId = ApiInfo.PayMchId;
            var serial_no = ApiInfo.PayCertKeySerialno;
            var method = "GET";
            var path = "/v3/certificates";
            var timeStamp = TimeHelper.GetUtcTimeStamp();
            var nonce = RandomHelper.Instance.GetString(32);
            var body = "";
            var msg = $"{method}\n{path}\n{timeStamp}\n{nonce}\n{body}\n";

            //用私钥签名
            string privateKey = "", sign = "";//私钥，签名
            try
            {
                privateKey = CertHelper.GetKeyStringFromCert(ApiInfo.PayPrivateKeyPath);
            }
            catch (Exception ex)
            {
                throw new Exception("请证书中获取私钥失败，错误原因：" + ex.Message, ex);
            }
            try
            {
                sign = RsaHelper.Signature(msg, privateKey);
            }
            catch (Exception ex)
            {
                throw new Exception("生成请求签名失败，错误原因：" + ex.Message, ex);
            }

            //配置HTTP头
            var dicHeader = new Dictionary<string, string>();
            var Authorization = $"WECHATPAY2-SHA256-RSA2048 mchid=\"{mchId}\",nonce_str=\"{nonce}\",signature=\"{sign}\",timestamp=\"{timeStamp}\",serial_no=\"{serial_no}\"";
            dicHeader.Add("Authorization", Authorization);
            var accept = "application/json";
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";

            //执行请求
            var url = "https://api.mch.weixin.qq.com/v3/certificates";
            try
            {
                var response = HttpHelper.GetResponse(
                    Url: url,
                    Accept: accept,
                    UserAgent: userAgent,
                    dicHeaderKeyValue: dicHeader);

                //获取返回数据
                var request_Id = response.Headers["Request-ID"];
                var Wechatpay_Nonce = response.Headers["Wechatpay-Nonce"];//随机数
                var Wechatpay_Signature = response.Headers["Wechatpay-Signature"];//签名
                var Wechatpay_Timestamp = response.Headers["Wechatpay-Timestamp"];//时间戳
                var Wechatpay_Serial = response.Headers["Wechatpay-Serial"];//证书版本号
                var sBack = new StreamReader((response.GetResponseStream())).ReadToEnd();//获取返回的body

                //提取返回中最新的证书
                var backCert = Newtonsoft.Json.JsonConvert.DeserializeObject<PlatCertBack>(sBack);
                var newCert = backCert.data.OrderByDescending(x => x.expire_time).First();

                //更新证书
                if (newCert.serial_no != ApiInfo.PayPlatKeySerialno)//保存的证书编号和最新的证书编号不同
                {
                    try
                    {
                        var newCertStr = AesHelper.GcmDecrypt(
                            newCert.encrypt_certificate.associated_data,
                            newCert.encrypt_certificate.nonce,
                            newCert.encrypt_certificate.ciphertext,
                            ApiInfo.PayApiKey
                            );
                        //保存证书
                        var filePath = ApiInfo.PayPlatKeyPath.Replace(ApiInfo.PayPlatKeySerialno, newCert.serial_no);
                        CertHelper.SaveKeyToCert(newCertStr, filePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("保存新证书的时候出错，错误原因为：" + ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("请求证书失败，错误原因：" + ex.Message, ex);
            }
        }
    }
}
