using System;
using System.IO;
using System.Net;
using System.Text;

namespace ERPAPD.Helpers
{
    public class RestfulClient
    {
        public static string POST(string url, string queries)
        {
            return Request(url, queries, Method.POST);
        }

        public static string POSTStream(string url, string queries)
        {
            return RequestStream(url, queries, Method.POST);
        }

        public static string GET(string url, string queries)
        {
            return Request(url, queries, Method.GET);
        }

        public static string RequestStream(string url, string queries, Method method)
        {
            try
            {
                int timeout = 15000;

                HttpWebRequest request = null;

                request = (HttpWebRequest)WebRequest.Create(url);

                byte[] objBytes = Encoding.UTF8.GetBytes(queries);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = objBytes.Length;
                request.Timeout = timeout;
                request.UserAgent = Guid.NewGuid().ToString();
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);         //allows for validation of SSL certificates 
                using (Stream post = request.GetRequestStream())
                {
                    post.Write(objBytes, 0, objBytes.Length);
                    post.Close();
                }

                string result = string.Empty;
                StreamReader data = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
                result = data.ReadToEnd();
                data.Close();
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //using (StreamReader data = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8))
                //{
                //    result = data.ReadToEnd();
                //    data.Close();
                //}

                return result;
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new System.TimeoutException();
            }
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
                    request.ContentType = "multipart/form-data";
                    request.ContentLength = objBytes.Length;
                    request.Timeout = timeout;
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
                //throw ex;
                throw new System.TimeoutException();
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

        //
        // ClientData = It's the data we want to pass to the URL
        // PostURL = The actual URL we need to pass the data to
        public static string PostRequest(string ClientData, string PostURL)
        {

            string responseFromServer = string.Empty;

            try
            {

                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(PostURL);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.


                byte[] byteArray = Encoding.UTF8.GetBytes(ClientData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();

                WebHeaderCollection myWebHeaderCollection = response.Headers;

                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();

                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();

                }


                response.Close();



                return responseFromServer;
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    WebHeaderCollection myWebHeaderCollection = httpResponse.Headers;

                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        Console.WriteLine(streamReader.ReadToEnd());
                }
                return responseFromServer;
            }

        }

    }

    public enum Method
    {
        GET = 1,
        POST = 2
    }
}