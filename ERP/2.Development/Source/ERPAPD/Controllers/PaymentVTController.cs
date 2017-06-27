using ERPAPD.Helpers;
using ERPAPD.Models;
//using HtmlAgilityPack;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace ERPAPD.Controllers
{
    public class PaymentVTController : Controller
    {
        //
        // GET: /PaymentVT/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignedData()
        {
            return View(new Dictionary<string, string>());
        }


        [HttpPost]
        public ActionResult Signed(DC_SignedVietinRequest signData)
        {
            Dictionary<string, string> FD = (from x in signData.GetType().GetProperties() select x).ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(signData, null) == null ? "" : x.GetGetMethod().Invoke(signData, null).ToString()));
            var request = string.Join("&", FD.Select(s => s.Key + "=" + s.Value));

            //string URL = ConfigurationManager.AppSettings["signedLink"].ToString().Trim();
            //System.Net.WebRequest webRequest = System.Net.WebRequest.Create(URL);
            //webRequest.Method = "POST";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            //string postData = request; //you form data in get format 
            //byte[] postArray = Encoding.UTF8.GetBytes(postData);
            //Stream reqStream = webRequest.GetRequestStream();
            //reqStream.Write(postArray, 0, postArray.Length);
            //reqStream.Close();
            ////HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            //string result = string.Empty;

            //Stream dataStream = webRequest.GetRequestStream();

            //using (StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            //{
            //    result = sr.ReadToEnd();
            //    sr.Close();
            //}

            //
            //string result = string.Empty;
            //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ConfigurationManager.AppSettings["signedLink"].ToString().Trim());
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";
            //req.CookieContainer = new CookieContainer();
            //req.Accept = "*/*";
            //ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            //byte[] postByteArray = Encoding.UTF8.GetBytes(request);
            //req.ContentLength = postByteArray.Length;
            //Stream postStream = req.GetRequestStream();
            //postStream.Write(postByteArray, 0, postByteArray.Length);
            //postStream.Close();

            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            //if (resp.StatusCode == HttpStatusCode.OK)
            //{
            //    StreamReader sr = new StreamReader(resp.GetResponseStream());
            //    result = sr.ReadToEnd();
            //}

            //StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream());
            //string result = sr.ReadToEnd();

            //WebClient wc = new WebClient();
            //wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            //string result = wc.UploadString(ConfigurationManager.AppSettings["signedLink"].ToString().Trim(), request);

            //string result = Helpers.RestfulClient.POSTStream(ConfigurationManager.AppSettings["signedLink"].ToString().Trim(), request);

            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(result);
            //var response = new Dictionary<string, object>();
            //var fieldNames = typeof(DC_SignedVietinReponse).GetProperties()
            //                .Select(field => field.Name)
            //                .ToList();
            //foreach (HtmlNode input in doc.DocumentNode.SelectNodes("//input"))
            //{
            //    var id = input.Attributes["id"] != null ? input.Attributes["id"].Value : "";
            //    string value = input.Attributes["value"] != null ? input.Attributes["value"].Value : "";

            //    if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(value) && fieldNames.Contains(id))
            //    {
            //        response.Add(id, value);
            //    }
            //}

            //var data = ObjectExtensions.ToObject<DC_SignedVietinReponse>(response);

            //WebClient wc = new WebClient();
            //wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            //string HtmlResult = wc.UploadString(ConfigurationManager.AppSettings["signedLink"].ToString().Trim(), request);


            //Dictionary<string, string> FD = (from x in signData.GetType().GetProperties() select x).ToDictionary(x => x.Name, x => (x.GetGetMethod().Invoke(signData, null) == null ? "" : x.GetGetMethod().Invoke(signData, null).ToString()));
            //var request = string.Join("&", FD.Select(s => s.Key + "=" + s.Value));

            //var data = RestfulClient.POST(ConfigurationManager.AppSettings["signedLink"].ToString().Trim(), request);

            //        HttpWebRequest httpRequest =
            //(HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["signedLink"].ToString().Trim());

            //        httpRequest.Method = "POST";
            //        httpRequest.ContentType = "application/x-www-form-urlencoded";

            //        byte[] bytedata = Encoding.UTF8.GetBytes(request);
            //        httpRequest.ContentLength = bytedata.Length;

            //        Stream requestStream = httpRequest.GetRequestStream();
            //        requestStream.Write(bytedata, 0, bytedata.Length);
            //        requestStream.Close();


            //        HttpWebResponse httpWebResponse =
            //        (HttpWebResponse)httpRequest.GetResponse();
            //        Stream responseStream = httpWebResponse.GetResponseStream();

            //        StringBuilder sb = new StringBuilder();

            //        using (StreamReader reader =
            //        new StreamReader(responseStream, System.Text.Encoding.UTF8))
            //        {
            //            string line;
            //            while ((line = reader.ReadLine()) != null)
            //            {
            //                sb.Append(line);
            //            }
            //        }

            //        string result = sb.ToString();

            string result = RestfulClient.PostRequest(request, ConfigurationManager.AppSettings["signedLink"].ToString().Trim());

            return View();
        }

        [HttpPost]
        public ActionResult SignedData(FormCollection form)
        {
            var paramsArray = new Dictionary<string, string>();
            foreach (var item in form.AllKeys)
            {
                paramsArray[item] = form[item];
            }
            var sign = Helpers.PaySign.sign(paramsArray);
            paramsArray.Add("signature", sign);
            return View(paramsArray);
        }

        public ActionResult Callback(string req_reference_number)
        {

            return View();
        }
    }
}