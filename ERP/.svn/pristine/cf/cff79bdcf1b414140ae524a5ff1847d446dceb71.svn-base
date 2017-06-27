using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ERPAPD.Helpers
{
    public static class PaySign
    {
        private const string SECRET_KEY = "88faefdddedc43379674113b8df428abfd6e6b40903c46c6929fd0c9c06967eee7c311358b1e4158b81722bfcf5836a9722e57eb4ca04e0c97f8609d2b4c54a770a8a37923dd4be28ff707b2675eb68d1ded06c847fd45d2bcf25550f0cc950896c30f42500744fab9639cce232f708e153168bc6d084b4a9fe18e7e1fd1040e";
        public static string sign(IDictionary<string, string> paramsArray)
        {
            return sign(buildDataToSign(paramsArray), SECRET_KEY);
        }
        private static string sign(string data, string secretKey)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }
        private static string buildDataToSign(IDictionary<string, string> paramsArray)
        {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            List<string> datatoSign = new List<string>();

            foreach (var signedFieldName in signedFieldNames)
            {
                datatoSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return commaSeparate(datatoSign);
        }

        private static string commaSeparate(List<string> datatoSign)
        {
            var data = String.Join(",", datatoSign.Select(s => s));
            return data;
        }
    }
}
