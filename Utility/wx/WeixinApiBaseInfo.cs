using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 微信初始化参数
    /// </summary>
    public class WeixinApiBaseInfo
    {
        /// <summary>
        /// 微信AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string PayMchId { get; set; }
        /// <summary>
        /// APIv3 私钥
        /// </summary>
        public string PayApiKey { get; set; }
        /// <summary>
        /// 商户证书版本号
        /// </summary>
        public string PayCertKeySerialno { get; set; }
        /// <summary>
        /// 私钥apiclient_key.pem路径
        /// </summary>
        public string PayPrivateKeyPath { get; set; }
        /// <summary>
        /// 平台证书wechatpay_3371210A5785C9A4E2D081E6E7752FC79AD3A397.pem的版本号
        /// </summary>
        public string PayPlatKeySerialno { get; set; }
        /// <summary>
        /// 平台证书路径
        /// </summary>
        public string PayPlatKeyPath { get; set; }
    }
}
