using BIBIAM.Core.Data.Providers;
using System.Configuration;

namespace ERP_API
{
    public static class AllConstant
    {
        public static string KeyAPI = SqlHelper.GetMd5Hash(ConfigurationManager.AppSettings["KeyAPI"].ToString().Trim());
        public static string Url = ConfigurationManager.AppSettings["Url"].ToString().Trim();
        public static string MCCDBName = ConfigurationManager.AppSettings["MCCDBName"].ToString().Trim();
        public static string MCCConnectionString = ConfigurationManager.AppSettings["MCCAPDConnection"].ToString().Trim();
        public static string ERPConnectionString = ConfigurationManager.AppSettings["ERPAPDConnection"].ToString().Trim();
    }
}