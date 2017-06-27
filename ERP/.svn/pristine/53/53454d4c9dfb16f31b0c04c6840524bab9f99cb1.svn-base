using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MCC.Helpers
{
    public class checkValid
    {
        public bool email(string email = "")
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool phoneV(string phone = "")
        {
            string rgx = @"(\(\d{3}\)\d{3}-\d{4})|(\d{10})";
            if (Regex.IsMatch(phone, rgx))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}