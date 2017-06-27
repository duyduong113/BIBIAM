using System.Text;

namespace ERPAPD.Helpers
{
    public static class ConvertANSIToUTF8
    {
        public static string Convert(string s)
        {
            //Convert sang unicode
            var result = string.Empty;
            if (!s.Contains("UnicodeConverted"))
            {
                byte[] bytes = Encoding.Default.GetBytes(s);
                result = Encoding.UTF8.GetString(bytes);
            }
            else result = s;
            return result;

        }
    }
}