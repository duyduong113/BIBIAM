using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ERPAPD.Helpers
{
    public class convertToUnSign3
    {
        public static string Init(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string tmp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return Regex.Replace(tmp, "[^a-zA-Z0-9_ -]+", "", RegexOptions.Compiled);
        }
        public static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);


            //Get your key from config file to open the lock!
            string key = "D0nt_Cr@ck-M3_pL34S3.jUs";

            //if hashing was not implemented get the byte code of the key
            keyArray = UTF8Encoding.UTF8.GetBytes(key);


            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
    public class convertNumber
    {
       public static double currencyToNumber(string currenncy)
        {
            string strNumber = !string.IsNullOrEmpty(currenncy) ? currenncy.Replace(",", "") : "0";
            double number = double.Parse(strNumber);
            return number;
        }
    }

}