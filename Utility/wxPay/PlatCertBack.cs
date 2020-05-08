using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// 请求平台证书返回值Body
    /// </summary>
    public class PlatCertBack
    {
        /// <summary>
        /// 证书列表
        /// </summary>
        public List<PlatCertBack_Cert> data { get; set; }
    }
    /// <summary>
    /// 证书
    /// </summary>
    public class PlatCertBack_Cert
    {
        /// <summary>
        /// 证书编号
        /// </summary>
        public string serial_no { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime effective_time { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime expire_time { get; set; }
        /// <summary>
        /// 证书信息
        /// </summary>
        public Encrypt_Certificate encrypt_certificate { get; set; }
    }
    /// <summary>
    /// 证书信息
    /// </summary>
    public class Encrypt_Certificate
    {
        /// <summary>
        /// 加密算法
        /// </summary>
        public string algorithm { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }
        /// <summary>
        /// 附加数据包
        /// </summary>
        public string associated_data { get; set; }
        /// <summary>
        /// 证书密文
        /// </summary>
        public string ciphertext { get; set; }
    }
}
