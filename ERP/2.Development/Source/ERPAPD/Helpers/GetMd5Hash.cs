using System.Security.Cryptography;
using System.Text;

namespace ERPAPD.Helpers
{
    public class GetMd5Hash
    {
        public static string Generate(string pass)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pass));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}