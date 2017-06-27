using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CMS.Helpers
{
    public class RestfulClient
    {
        public static string POST(string url, string queries)
        {
            return Request(url, queries, Method.POST);
        }


        public static string POSTNormal<T>(string url, T obj)
        {
            NameValueCollection data = new NameValueCollection();
            obj.GetType().GetProperties()
                .ToList()
                .ForEach(pi => data.Add(pi.Name, pi.GetValue(obj, null).ToString()));

            //var request = "date_start =" + date_start + "&date_end =" + date_end + "&hash =" + Helpers.GetMd5Hash.Generate(date_start + date_end + "tvn112015");
            WebClient webClient = new WebClient();
            byte[] responseBytes = webClient.UploadValues(url, "POST", data);
            string response = Encoding.UTF8.GetString(responseBytes);
            return response;
        }

        public static string POSTNormalT<T>(string url, T obj)
        {
            NameValueCollection data = new NameValueCollection();
            obj.GetType().GetProperties()
                .ToList()
                .ForEach(pi => data.Add(pi.Name, pi.GetValue(obj, null).ToString()));

            //var request = "date_start =" + date_start + "&date_end =" + date_end + "&hash =" + Helpers.GetMd5Hash.Generate(date_start + date_end + "tvn112015");
            WebClient webClient = new WebClient();
            byte[] responseBytes = webClient.UploadValues(url, "POST", data);
            string response = Encoding.UTF8.GetString(responseBytes);
            return response;
        }


        public static string GET(string url, string queries)
        {
            return Request(url, queries, Method.GET);
        }

        public static string Request(string url, string queries, Method method)
        {
            try
            {
                int timeout = 15000;

                HttpWebRequest request = null;

                if (method == Method.GET)
                {
                    if (url.Contains("?"))
                        url = string.Format("{0}&{1}", url, queries);
                    else
                        url = string.Format("{0}?{1}", url, queries);

                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.Timeout = timeout;
                }
                else if (method == Method.POST)
                {
                    request = (HttpWebRequest)WebRequest.Create(url);

                    byte[] objBytes = Encoding.UTF8.GetBytes(queries);

                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = objBytes.Length;
                    request.Timeout = timeout;
                    request.ReadWriteTimeout = timeout;
                    ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);         //allows for validation of SSL certificates 
                    using (Stream post = request.GetRequestStream())
                    {
                        post.Write(objBytes, 0, objBytes.Length);
                        post.Close();
                    }
                }

                string result = string.Empty;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader data = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = data.ReadToEnd();
                    data.Close();
                }

                response.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new System.TimeoutException();
            }
        }

        public static string Request(string url)
        {
            try
            {
                int timeout = 30000;
                HttpWebRequest request;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = timeout;
                string result;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var data = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = data.ReadToEnd();
                    data.Close();
                }
                response.Close();
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (var reader = new StreamReader(resp.GetResponseStream()))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                    return null;
                }
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum Method
    {
        GET = 1,
        POST = 2
    }
}