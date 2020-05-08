using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// RSA加密操作类
    /// </summary>
    public class RsaHelper
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="message">明文</param>
        /// <param name="keyString">秘钥</param>
        /// <returns></returns>
        public static string Signature(string message,string keyString)
        {
            byte[] keyData = Convert.FromBase64String(keyString);
            //byte[] keyData = System.Text.Encoding.UTF8.GetBytes(keyString);

            using (CngKey cngkey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            {
                using (RSACng rsa = new RSACng(cngkey))
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
                }
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="message">验签消息</param>
        /// <param name="signature">要验证签名</param>
        /// <param name="certPath">秘钥路径</param>
        /// <returns></returns>
        public static bool VerifySignature(string message,string signature,string certPath)
        {
            string base64X509Cert = CertHelper.GetKeyStringFromCert(certPath);
            base64X509Cert = base64X509Cert.Replace("-----BEGIN CERTIFICATE-----", "");
            base64X509Cert = base64X509Cert.Replace("-----END CERTIFICATE-----", "");
            base64X509Cert = base64X509Cert.Replace("\n", "");
            var derCert = Convert.FromBase64String(base64X509Cert);
            var x509 = new X509Certificate2(derCert);
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509.PublicKey.Key;
            string publicKey = rsa.ToXmlString(false);

            //验签
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(message);
            var sha256 = new SHA256CryptoServiceProvider();
            byte[] rgbHash = sha256.ComputeHash(bt);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(publicKey);
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
            deformatter.SetHashAlgorithm("SHA256");
            byte[] rgbSignature = Convert.FromBase64String(signature);

            return deformatter.VerifySignature(rgbHash, rgbSignature);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string Encrypt(string text, byte[] publicKey)
        {
            using (var x509 = new X509Certificate2(publicKey))
            {
                using (var rsa = (RSACryptoServiceProvider)x509.PublicKey.Key)
                {
                    var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(text), true);

                    return Convert.ToBase64String(buff);
                }
            }
        }
    }
}
