using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloudCommerceProApiClient
{
    /// <summary>
    /// Produces Hash from Public Key to be passed with all requests
    /// </summary>
    public static class ApiSecurityStatic
    {

        #region Enctryption functions

        /// <summary>
        /// Creates security Hash string to pass to API
        /// </summary>
        /// <param name="data">your plaint text api password</param>
        /// <param name="_publicKey">your xml public key as string</param>
        /// <returns>string</returns>
        public static string Encrypt(string data, string _publicKey)
        {
            UTF8Encoding _encoder = new UTF8Encoding();
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }


        #endregion



    }
}
