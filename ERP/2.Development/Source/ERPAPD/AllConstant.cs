using ERPAPD.Helpers;
using System.Configuration;

namespace ERPAPD
{
    public static class AllConstant
    {
        public static string KeyAPI = SqlHelper.GetMd5Hash(ConfigurationManager.AppSettings["KeyAPI"].ToString().Trim());
        public static string UriAPI = ConfigurationManager.AppSettings["UriAPI"].ToString().Trim();
    }
}