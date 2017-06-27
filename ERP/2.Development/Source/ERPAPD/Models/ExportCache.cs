using System;
using DevTrends.MvcDonutCaching;
using System.Web.Mvc;
using System.IO;
using NPOI.HSSF.UserModel;
using OfficeOpenXml;

namespace ERPAPD.Models
{
    public class ExportCache
    {
        public static string FilePath(string filename)
        {
            var location = System.Web.HttpContext.Current.Server.MapPath("~/ExportCache");
            CreateFolderIfNeeded(location);
            return location + "/" + filename;
        }
        public static bool CachResult(HSSFWorkbook workbook, string filename)
        {
            string cacheKey =System.Web.HttpContext.Current.User.Identity.Name +  System.Web.HttpContext.Current.Request.RawUrl;
            var OutputCacheManager = new OutputCacheManager();
            CacheItem item = OutputCacheManager.GetItem(cacheKey);
            if (item != null && item.ContentType == "StartRequest")
            {
                FileStream fileStream = new FileStream(FilePath(filename), FileMode.Create, FileAccess.Write);
                workbook.Write(fileStream);
                fileStream.Close();
                item.Content = filename;
                item.ContentType = "FinishRequest";

                OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                return false;
            }
            else
            {
                FileStream fileStream = new FileStream(FilePath(filename), FileMode.Create, FileAccess.Write);
                workbook.Write(fileStream);
                fileStream.Close();
                item.Content = filename;
                item.ContentType = "FinishRequest";
                OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                return true;
            }
        }
        public static bool CachResult(ExcelPackage workbook, string filename)
        {
            string cacheKey = System.Web.HttpContext.Current.User.Identity.Name + System.Web.HttpContext.Current.Request.RawUrl;
            var OutputCacheManager = new OutputCacheManager();
            CacheItem item = OutputCacheManager.GetItem(cacheKey);
            if (item != null && item.ContentType == "StartRequest")
            {
                FileStream fileStream = new FileStream(FilePath(filename), FileMode.Create, FileAccess.Write);
                workbook.SaveAs(fileStream);
                fileStream.Close();
                item.Content = filename;
                item.ContentType = "FinishRequest";

                OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                return false;
            }
            else
            {
                FileStream fileStream = new FileStream(FilePath(filename), FileMode.Create, FileAccess.Write);
                workbook.SaveAs(fileStream);
                fileStream.Close();
                item.Content = filename;
                item.ContentType = "FinishRequest";
                OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                return true;
            }
        }
        private static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    result = false;
                }
            }
            return result;
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ExportCacheAttribute : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        // option = 0 : cache file for 30 minitu
        // option = 1 : cache file only when excuting 
        public int option ;
        public ExportCacheAttribute(int i)
        {
            this.option = i;
        }
        public ExportCacheAttribute()
        {
            this.option = 0;
        }
        public void OnException(ExceptionContext filterContext)
        {
            string cacheKey = System.Web.HttpContext.Current.User.Identity.Name + System.Web.HttpContext.Current.Request.RawUrl;
            var OutputCacheManager = new OutputCacheManager();
            CacheItem item = OutputCacheManager.GetItem(cacheKey);
            if (item != null)
            {
                item.ContentType = "RequestError";
                OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name))
            {
                ContentResult result = new ContentResult {Content = "",ContentType = ""};
                filterContext.Result = result;
            }
            string cacheKey = System.Web.HttpContext.Current.User.Identity.Name  + System.Web.HttpContext.Current.Request.RawUrl;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var OutputCacheManager = new OutputCacheManager();
                CacheItem item = OutputCacheManager.GetItem(cacheKey);
                if (item == null  )
                {
                    item = new CacheItem { Content = "", ContentType = "StartRequest" };
                    OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                }
                else if(item.ContentType == "RequestError" || (option == 1 && item.ContentType == "FinishRequest")){
                    item.Content = "";
                    item.ContentType = "StartRequest";
                    OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                }
                else if (item.ContentType == "FinishRequest")
                {
                    FilePathResult result = new FilePathResult(ExportCache.FilePath(item.Content), "application/vnd.ms-excel");
                    result.FileDownloadName = item.Content;
                    filterContext.Result = result;
                }
                else
                {
                    int current = 0;
                    if (!string.IsNullOrEmpty(item.Content))
                    {
                        current = int.Parse(item.Content) + 1;
                    }
                    item.Content = current.ToString();
                    OutputCacheManager.AddItem(cacheKey, item, DateTime.UtcNow.AddSeconds(1800));
                    while (item.ContentType == "StartRequest" && item.Content == current.ToString())
                    {
                        System.Threading.Thread.Sleep(5000);
                        item = OutputCacheManager.GetItem(cacheKey);
                    }

                    if (item.ContentType == "FinishRequest" )
                    {
                        FilePathResult result = new FilePathResult(ExportCache.FilePath(item.Content), "application/vnd.ms-excel");
                        result.FileDownloadName = item.Content;
                        filterContext.Result = result;
                    }
                    else if (item.ContentType == "RequestError")
                    {
                        ContentResult result = new ContentResult
                        {
                            Content = "Error when excute request",
                            ContentType = item.ContentType
                        };
                        filterContext.Result = result;
                    }
                    else
                    {
                        ContentResult result = new ContentResult
                        {
                            Content = "",
                            ContentType = item.ContentType
                        };
                        filterContext.Result = result;
                    }
                }
             
            }
        }


    }
}