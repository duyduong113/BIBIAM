using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Data.Providers
{
    public static class Utility
    {
        public static string ConvertUTF(string s)
        {
            if (String.IsNullOrEmpty(s))
                return string.Empty;
            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
            //string to utf
            byte[] utf = System.Text.Encoding.UTF8.GetBytes(s);
            //utf to string
            string s2 = System.Text.Encoding.UTF8.GetString(utf);
            return s2;
        }

    }
}
