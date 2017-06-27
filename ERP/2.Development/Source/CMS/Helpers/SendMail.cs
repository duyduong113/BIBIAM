using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using CMS.Models;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using OfficeOpenXml;

namespace CMS.Helpers
{
    public class SendMail
    {
        //public static string email = "realestate@twin.vn";
        //public static string password = "F0reverDM$";
        //public static string host = "smtp.gmail.com";
        //public static int port = 587;
        //public static bool enableSsl = true;

        public static string email = "ctv.tunguyen02@gmail.com";
        public static string password = "Ctvseo@2016_ab";
        public static string host = "smtp.gmail.com";
        public static int port = 587;
        public static bool enableSsl = true;

        //public void Send(List<string> mailTos, List<string> mailCCs, string subject, string content)
        //{
        //    using (var dbConn = Helpers.OrmliteConnection.openConn())
        //    {
        //        var configEmail = dbConn.Single<tw_SMTP_Configuration>("active = 1 and isDefault = 1");
        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //        if (mailTos.Count() > 0)
        //        {
        //            foreach (var item in mailTos)
        //            {
        //                mail.To.Add(item);
        //            }
        //        }

        //        if (mailCCs.Count() > 0)
        //        {
        //            foreach (var item in mailTos)
        //            {
        //                mail.CC.Add(item);
        //            }
        //        }

        //        var listBccs = dbConn.Scalar<string>(" SELECT Value FROM Parameter WHERE ParameterName = 'MailAdmin'");
        //        foreach (var item in listBccs.Split(';'))
        //        {
        //            mail.Bcc.Add(item);
        //        }


        //        mail.Subject = subject;
        //        mail.From = new MailAddress(configEmail.email);
        //        mail.Body = HttpUtility.HtmlDecode(content);
        //        mail.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //        smtp.Port = 587;
        //        smtp.Host = configEmail.host;
        //        smtp.EnableSsl = configEmail.enableSsl;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new System.Net.NetworkCredential(configEmail.email, configEmail.password);

        //        ServicePointManager.ServerCertificateValidationCallback =
        //            delegate(object s, X509Certificate certificate,
        //                     X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //            { return true; };

        //        smtp.Send(mail);
        //    }
        //}

        //public void Send(string subject, string content)
        //{
        //    using (var dbConn = Helpers.OrmliteConnection.openConn())
        //    {
        //        var configEmail = dbConn.Single<tw_SMTP_Configuration>("active = 1 and isDefault = 1");
        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        //        var listTos = dbConn.Scalar<string>(" SELECT Value FROM Parameter WHERE ParameterName = 'MailAdmin'");
        //        foreach (var item in listTos.Split(';'))
        //        {
        //            mail.To.Add(item);
        //        }

        //        mail.Subject = subject;
        //        mail.From = new MailAddress(configEmail.email);
        //        mail.Body = HttpUtility.HtmlDecode(content);
        //        mail.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //        smtp.Port = 587;
        //        smtp.Host = configEmail.host;
        //        smtp.EnableSsl = configEmail.enableSsl;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new System.Net.NetworkCredential(configEmail.email, configEmail.password);

        //        ServicePointManager.ServerCertificateValidationCallback =
        //            delegate(object s, X509Certificate certificate,
        //                     X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //            { return true; };

        //        smtp.Send(mail);
        //    }
        //}


        private void SendDailyHtmlFormattedEmail(string body, string saleemail, string leademail, string contractno)
        {
            using (MailMessage mail = new MailMessage())
            {
                if (!String.IsNullOrEmpty(leademail))
                {
                    mail.To.Add(saleemail);
                    mail.CC.Add(leademail);
                }
                else
                {
                    mail.To.Add(saleemail);
                }

                mail.Subject = "Booking cho số hợp đồng " + contractno;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.From = new MailAddress(email);
                mail.Bcc.Add("dangbm@twin.vn,tunha@twin.vn");
                mail.Body = HttpUtility.HtmlDecode(body);

                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Port = port;
                smtp.Host = host;
                smtp.EnableSsl = enableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(email, password);
                smtp.Send(mail);
            }
        }

        private void SendDailyHtmlFormattedEmailConfirm(string body, string saleemail, string leademail, string contractno)
        {
            using (MailMessage mail = new MailMessage())
            {
                if (!String.IsNullOrEmpty(leademail))
                {
                    mail.To.Add(saleemail);
                    mail.CC.Add(leademail);
                }
                else
                {
                    mail.To.Add(saleemail);
                }

                mail.Subject = "Xác nhận cho hợp đồng " + contractno;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.From = new MailAddress(email);
                mail.Bcc.Add("dangbm@twin.vn,tunha@twin.vn");
                mail.Body = HttpUtility.HtmlDecode(body);

                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Port = port;
                smtp.Host = host;
                smtp.EnableSsl = enableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(email, password);
                smtp.Send(mail);
            }
        }

   
        //private void SendDailyHtmlFormattedEmail(string body, string brokerEmail, string dispatcherEmail, string fileLocation)
        //{
        //    using (MailMessage mail = new MailMessage())
        //    {

        //        if (!String.IsNullOrEmpty(brokerEmail))
        //        {
        //            mail.To.Add(brokerEmail);
        //            mail.CC.Add(dispatcherEmail);
        //        }
        //        else
        //        {
        //            mail.To.Add(dispatcherEmail);
        //        }

        //        mail.Subject = "Phân bổ khách hàng mới";
        //        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //        mail.From = new MailAddress(email);
        //        mail.Bcc.Add("tiennd@twin.vn,tunha@twin.vn");
        //        mail.Body = HttpUtility.HtmlDecode(body);

        //        System.Net.Mail.Attachment attachment;
        //        attachment = new System.Net.Mail.Attachment(fileLocation);
        //        mail.Attachments.Add(attachment);

        //        mail.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //        smtp.Port = port;
        //        smtp.Host = host;
        //        smtp.EnableSsl = enableSsl;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //        smtp.Credentials = new System.Net.NetworkCredential(email, password);
        //        smtp.Send(mail);
        //    }
        //    //if (!String.IsNullOrEmpty(brokerEmail))
        //    //{
        //    //    mail.To.Add(brokerEmail);
        //    //    mail.CC.Add(dispatcherEmail);
        //    //}
        //    //else
        //    //{
        //    //    mail.To.Add(dispatcherEmail);
        //    //}

        //    //mail.Subject = "Phân bổ khách hàng mới";
        //    //mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //    //mail.From = new MailAddress(email);
        //    //mail.Bcc.Add("tiennd@twin.vn,hao@urv.vn,tunh@twin.vn");
        //    //mail.Body = HttpUtility.HtmlDecode(body);

        //    //System.Net.Mail.Attachment attachment;
        //    //attachment = new System.Net.Mail.Attachment(fileLocation);
        //    //mail.Attachments.Add(attachment);

        //    //mail.IsBodyHtml = true;
        //    //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //    //smtp.Port = 587;
        //    //smtp.Host = host;
        //    //smtp.EnableSsl = enableSsl;
        //    //smtp.UseDefaultCredentials = false;
        //    //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //    //smtp.Credentials = new System.Net.NetworkCredential(email, password);
        //    //smtp.Send(mail);
        //}
    }
}