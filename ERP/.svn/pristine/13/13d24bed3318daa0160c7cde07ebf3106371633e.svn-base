using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using MCC.Models;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using OfficeOpenXml;

namespace MCC.Helpers
{
    public class SendMail
    {
        public void Send(List<string> mailTos, List<string> mailCCs, string subject, string content, string displayName = "")
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var configEmail = dbConn.Single<SMTP_Configuration>("active = 1 and isDefault = 1");
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    if (mailTos.Count() > 0)
                    {
                        foreach (var item in mailTos)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                mail.To.Add(item);
                            }
                        }
                    }
                    if (mailCCs.Count() > 0)
                    {
                        foreach (var item in mailTos)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                mail.CC.Add(item);
                            }
                        }
                    }
                    var listBccs = dbConn.Scalar<string>(" SELECT Value FROM Parameter WHERE ParameterName = 'MailAdmin'");
                    foreach (var item in listBccs.Split(';'))
                    {
                        mail.Bcc.Add(item);
                    }
                    mail.Subject = subject;
                    mail.From = new MailAddress(configEmail.email, !string.IsNullOrEmpty(displayName) ? displayName : "Thietbinhanh.com");
                    mail.Body = HttpUtility.HtmlDecode(content);
                    mail.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Port = configEmail.port;
                    smtp.Host = configEmail.host;
                    smtp.EnableSsl = configEmail.enableSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential(configEmail.email, configEmail.password);
                    //ServicePointManager.ServerCertificateValidationCallback =
                    //    delegate(object s, X509Certificate certificate,
                    //             X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    //    { return true; };
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                     
                }
        }
        }

        public void Send(string mailTos, string mailCCs, string subject, string content,string displayName)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var configEmail = dbConn.Single<SMTP_Configuration>("active = 1 and isDefault = 1");
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(mailTos);
                if (!string.IsNullOrEmpty(mailCCs))
                {
                    mail.CC.Add(mailCCs);
                }
                var listBccs = dbConn.Scalar<string>(" SELECT Value FROM Parameter WHERE ParameterName = 'MailAdmin'");
                foreach (var item in listBccs.Split(';'))
                {
                    mail.Bcc.Add(item);
                }
                mail.Body = HttpUtility.HtmlDecode(content);
                mail.Subject = subject;
                mail.From = new MailAddress(configEmail.email, !string.IsNullOrEmpty(displayName) ? displayName : "Thietbinhanh.com");
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Port = configEmail.port;              
                smtp.EnableSsl = configEmail.enableSsl;
                smtp.Host = configEmail.host;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(configEmail.email, configEmail.password);
                //ServicePointManager.ServerCertificateValidationCallback =
                //    delegate(object s, X509Certificate certificate,
                //             X509Chain chain, SslPolicyErrors sslPolicyErrors)
                //    { return true; };
                    smtp.Send(mail);
            }
        }


        private void SendDailyHtmlFormattedEmail(string body, string saleemail, string leademail, string contractno)
        {
            using (MailMessage mail = new MailMessage())
            {
                
            }
        }

        private void SendDailyHtmlFormattedEmailConfirm(string body, string saleemail, string leademail, string contractno)
        {
            using (MailMessage mail = new MailMessage())
            {
               
            }
        }

        //public void sendMailBooking(tw_Contract_List sendcontract, tw_Contract_Product_Meta sendproduct, tw_User saleinfo, tw_VINManagement sendvin, tw_BusinessGroup leaderinfo, string engineNo)
        //{
        //    //var body = string.Empty;
        //    //string template = string.Empty;
        //    //using (var dbConn = Helpers.OrmliteConnection.openConn())
        //    //{
        //    //    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/bookingCar.html")))
        //    //    {
        //    //        template = reader.ReadToEnd();
        //    //    }

        //    //    template = template.Replace("{fullname}", sendcontract.fullname != null ? sendcontract.fullname : "");
        //    //    template = template.Replace("{phone}", sendcontract.phone != null ? sendcontract.phone : "");
        //    //    template = template.Replace("{contractnumber}", sendcontract.contractnumber != null ? sendcontract.contractnumber : "");
        //    //    template = template.Replace("{contractday}", sendcontract.contractday.ToString("dd/MM/yyyy") != null ? sendcontract.contractday.ToString("dd/MM/yyyy") : "");
        //    //    template = template.Replace("{VIN}", sendproduct.code != null ? sendproduct.code : "");
        //    //    template = template.Replace("{contractinvoicedate}", sendproduct.invoicedate.Value.ToString("dd/MM/yyyy") != null ? sendproduct.invoicedate.Value.ToString("dd/MM/yyyy") : "");
        //    //    template = template.Replace("{carname}", sendproduct.name != null ? sendproduct.name : "");
        //    //    template = template.Replace("{warehousename}", sendvin.warehouseName != null ? sendvin.warehouseName : "");

        //    //    template = template.Replace("{salename}", saleinfo.fullName != null ? saleinfo.fullName : "");
        //    //    template = template.Replace("{EngineNo}", engineNo != null ? engineNo : "");

        //    //    this.SendDailyHtmlFormattedEmail(template, saleinfo.email, leaderinfo.email, sendcontract.contractnumber);
        //    //}
        //}

        //public void sendMailConfirm(tw_Contract_List sendcontract, tw_Contract_Product_Meta sendproduct, tw_User saleinfo, tw_BusinessGroup leaderinfo)
        //{
        //    //var body = string.Empty;
        //    //string template = string.Empty;
        //    //using (var dbConn = Helpers.OrmliteConnection.openConn())
        //    //{
        //    //    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/confirmContract.html")))
        //    //    {
        //    //        template = reader.ReadToEnd();
        //    //    }

        //    //    template = template.Replace("{fullname}", sendcontract.fullname != null ? sendcontract.fullname : "");
        //    //    template = template.Replace("{phone}", sendcontract.phone != null ? sendcontract.phone : "");
        //    //    template = template.Replace("{contractnumber}", sendcontract.contractnumber != null ? sendcontract.contractnumber : "");
        //    //    template = template.Replace("{contractday}", sendcontract.contractday.ToString("dd/MM/yyyy") != null ? sendcontract.contractday.ToString("dd/MM/yyyy") : "");
        //    //    template = template.Replace("{carname}", sendproduct.name != null ? sendproduct.name : "");
        //    //    template = template.Replace("{salename}", saleinfo.fullName != null ? saleinfo.fullName : "");

        //    //    this.SendDailyHtmlFormattedEmailConfirm(template, saleinfo.email, leaderinfo.email, sendcontract.contractnumber);
        //    //}
        //}
        
    }
}