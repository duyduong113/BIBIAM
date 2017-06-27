using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OpenPop.Pop3;
using OpenPop.Mime;
using ERPAPD.Models;
using ServiceStack.OrmLite;
using System.IO;
using Hangfire;
using ERPAPD.Helpers;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace ERPAPD.Controllers
{
    public class CronEmailController : Controller
    {
        //
        // GET: /CronEmail/
        public ActionResult Index()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                //dbConn.DropTables(typeof(DC_Telesales_Xlite));
                //const bool overwrite = false;
                //dbConn.CreateTables(overwrite, typeof(DC_Telesales_Xlite));
                return View();
            }
        }

        public void Cron(string expression)
        {
            //"*/5 * * *" every 5mins
            RecurringJob.AddOrUpdate("CronEmail", () => CronEmail(), expression, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }

        public void CronXliteHistory(string expression)
        {
            //"*/5 * * *" every 5mins
            RecurringJob.AddOrUpdate("CronXliteHistory", () => XliteHistory(), expression, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }

        public void XliteHistory()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var lastDate = dbConn.GetScalar<DateTime>("SELECT TOP 1 CallDate FROM DC_Telesales_Xlite Order By Id DESC").ToString("yyyy-MM-dd HH:mm:ss");
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var data = RestfulClient.POST(ConfigurationManager.AppSettings["XliteAPIHistory"].ToString().Trim() + "?action=outbound&date_from=" + lastDate + "&date_to=" + currentDate + "&user=deca_report&password=" + GetMd5Hash("1811HoaSao@@"), "");
                DC_Telesales_Xlite_API listHistory = new DC_Telesales_Xlite_API();
                if (!String.IsNullOrEmpty(data))
                {
                    JavaScriptSerializer objJavascript = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                    listHistory = objJavascript.Deserialize<DC_Telesales_Xlite_API>(data);
                    listHistory.data.ForEach(s => { s.CreatedAt = DateTime.Now; s.CreatedBy = "cronXLite"; });
                    foreach (var item in listHistory.data)
                    {
                        var exist = dbConn.Select<DC_Telesales_Xlite_API>("SELECT * FROM DC_Telesales_Xlite where RecordingFile = {0}", item.recordingfile);
                        if (exist.Count() == 0)
                        {
                            dbConn.Insert(item);
                        }
                    }
                }

            }
        }

        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public void CronEmail()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                //get list email seen
                var seenUids = dbConn.GetFirstColumn<string>("SELECT Uid FROM Deca_Email_SeenUids");
                //new list email
                var data = new List<Email>();

                Pop3Client pop3Client = new Pop3Client();
                pop3Client.Connect("pop.gmail.com", 995, true);
                pop3Client.Authenticate("cskh@deca.vn", "umycvtjuhcndujnh", AuthenticationMethod.UsernameAndPassword);
                //seenUUIDs
                //get list uuid
                List<string> uids = pop3Client.GetMessageUids();

                // All the new messages not seen by the POP3 client
                for (int i = 0; i < uids.Count; i++)
                {
                    string currentUidOnServer = uids[i];
                    if (!seenUids.Contains(currentUidOnServer))
                    {
                        // We have not seen this message before.
                        // Download it and add this new uid to seen uids

                        // the uids list is in messageNumber order - meaning that the first
                        // uid in the list has messageNumber of 1, and the second has 
                        // messageNumber 2. Therefore we can fetch the message using
                        // i + 1 since messageNumber should be in range [1, messageCount]
                        Message unseenMessage = pop3Client.GetMessage(i + 1);

                        Email email = new Email()
                        {
                            MessageNumber = i,
                            Subject = unseenMessage.Headers.Subject,
                            DateSent = unseenMessage.Headers.DateSent,
                            From = string.Format("<a href = 'mailto:{1}'>{0}</a>", unseenMessage.Headers.From.DisplayName, unseenMessage.Headers.From.Address),
                        };
                        MessagePart body = unseenMessage.FindFirstHtmlVersion();
                        body = unseenMessage.FindFirstPlainTextVersion();
                        if (body != null)
                        {
                            email.Body = body.GetBodyAsText();
                        }
                        List<MessagePart> attachments = unseenMessage.FindAllAttachments();

                        email.Attachments = attachments;

                        data.Add(email);

                        // Add the uid to the seen uids, as it has now been seen
                        //seenUids.Add(currentUidOnServer);
                        dbConn.Insert(new Deca_Email_SeenUids { Uid = uids[i], CreatedAt = DateTime.Now });
                    }

                    //insert ticket
                }
                var checktype = Deca_RT_Type.GetDeca_RT_Type("TIC0004");
                foreach (var item in data)
                {
                    var w = new Deca_RT_Ticket();
                    w.Status = "New";
                    w.TypeID = checktype.TypeID;
                    w.Title = item.Subject;
                    w.Detail = item.Body;
                    w.Requestor = "cronSystemFromEmail";
                    w.Assignee = "";
                    w.preAssignee = "";
                    w.Owner = checktype.Owner;
                    w.OrganizationID = "";
                    w.CustomerID = item.From;
                    w.Priority = "TPRI002";
                    w.Impact = "TIMP002";
                    w.RowCreatedUser = "cronSystemFromEmail";
                    w.RowCreatedTime = DateTime.Now;
                    var id = w.Save();
                    if (id != "")
                    {
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = "Add New Ticket";
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = "cronSystemFromEmail";
                        savelog.Save();
                    }

                    foreach (var attachment in item.Attachments)
                    {
                        string pathForSaving = Server.MapPath("~/UploadFileTicket/" + id);
                        if (this.CreateFolderIfNeeded(pathForSaving))
                        {
                            var filePath = Path.Combine(pathForSaving, attachment.FileName);
                            FileStream Stream = new FileStream(filePath, FileMode.Create);
                            BinaryWriter BinaryStream = new BinaryWriter(Stream);
                            BinaryStream.Write(attachment.Body);
                            BinaryStream.Close();
                        }
                    }
                }
            }
        }

        private bool CreateFolderIfNeeded(string path)
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
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}