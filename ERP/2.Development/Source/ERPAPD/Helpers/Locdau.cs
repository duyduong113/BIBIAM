using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ERPAPD.Helpers
{
    public static class Locdau
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string tmp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return Regex.Replace(tmp, "[^a-zA-Z0-9_ -]+", "", RegexOptions.Compiled).Replace(",", "").Replace(" ", "-").Replace("--", "-").Replace("--", "-").ToLower();
        }

        public static bool EmailTests(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
              + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
              + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if (regex.IsMatch(email))
            {
                return true;
            }
            else
                return false;
        }

        public static string ConvertFileName(string FileName)
        {
            return FileName.Replace("+", "-").Replace("&", "-");
        }
    }
}