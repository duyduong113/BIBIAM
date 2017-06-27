using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class FAQController : CustomController
    {
        //
        // GET: /FAQ/
        public ActionResult Index()
        {
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.Assets = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult FAQ_Read(string filter)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Deca_Topic>();
                if (String.IsNullOrEmpty(filter))
                {
                    string select = @"select distinct t.* from Deca_Topic t
                                       LEFT JOIN Deca_FAQ f
                                        on t.Id = f.TopicId WHERE f.Published=1";

                    data = dbConn.Select<Deca_Topic>(select);
                    foreach (var item in data)
                    {
                        item.ListFAQ = dbConn.Select<Deca_FAQ>("TopicId={0} AND Published=1", item.Id);
                    }
                }
                else
                {
                    string select = @"select distinct t.* from Deca_Topic t
                                        JOIN Deca_FAQ f
                                        on t.Id = f.TopicId
                                        WHERE f.Published=1 AND ( f.[Question] COLLATE Latin1_General_CI_AI LIKE '%" + filter + "%' OR f.[Name] COLLATE Latin1_General_CI_AI LIKE '%" + filter + "%')";
                    data = dbConn.Select<Deca_Topic>(select);
                    foreach (var item in data)
                    {
                        item.ListFAQ = dbConn.Select<Deca_FAQ>("TopicId={0} AND ([Question] COLLATE Latin1_General_CI_AI LIKE '%" + filter + "%' OR [Name] COLLATE Latin1_General_CI_AI LIKE '%" + filter + "%') AND Published=1 ", item.Id);
                    }
                    //data = dbConn.Select<Deca_Topic>().Where(s => s.ListFAQ.Count() > 0 && (s.ListFAQ.Where(f => f.Name != null && Helpers.convertToUnSign3.Init(f.Name.ToLower()).Contains(Helpers.convertToUnSign3.Init(filter.ToLower()))).Count() > 0 || s.ListFAQ.Where(f => f.Question != null && Helpers.convertToUnSign3.Init(f.Question.ToLower()).Contains(Helpers.convertToUnSign3.Init(filter.ToLower()))).Count() > 0)).ToList();
                }

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult View(int Id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Deca_FAQ_Rating>("FAQId={0} AND UserName={1}", Id, currentUser.UserName);
                    if (data != null)
                    {
                        if (!data.View)
                        {
                            data.View = true;
                            data.UpdatedAt = DateTime.Now;
                            data.UpdatedBy = currentUser.UserName;
                            dbConn.Update(data);

                            var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                            quest.View += 1;
                            quest.UpdatedAt = DateTime.Now;
                            quest.UpdatedBy = currentUser.UserName;
                            dbConn.Update(quest);
                        }
                    }
                    else
                    {
                        var newdata = new Deca_FAQ_Rating();
                        newdata.FAQId = Id;
                        newdata.View = true;
                        newdata.UserName = currentUser.UserName;
                        newdata.CreatedAt = DateTime.Now;
                        newdata.CreatedBy = currentUser.UserName;
                        dbConn.Insert(newdata);

                        var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                        quest.View += 1;
                        quest.UpdatedAt = DateTime.Now;
                        quest.UpdatedBy = currentUser.UserName;
                        dbConn.Update(quest);
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult Like(int Id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //lay like hien tai
                    var data = dbConn.SingleOrDefault<Deca_FAQ_Rating>("FAQId={0} AND UserName={1}", Id, currentUser.UserName);
                    //update
                    if (data != null)
                    {
                        //chua like thi like
                        if (!data.Like)
                        {
                            data.Like = true;
                            data.UpdatedAt = DateTime.Now;
                            data.UpdatedBy = currentUser.UserName;
                            dbConn.Update(data);

                            var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                            quest.Like += 1;
                            quest.UpdatedAt = DateTime.Now;
                            quest.UpdatedBy = currentUser.UserName;
                            dbConn.Update(quest);
                        }
                        //da like, unlike
                        else
                        {
                            data.Like = false;
                            data.UpdatedAt = DateTime.Now;
                            data.UpdatedBy = currentUser.UserName;
                            dbConn.Update(data);

                            var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                            quest.Like -= 1;
                            quest.UpdatedAt = DateTime.Now;
                            quest.UpdatedBy = currentUser.UserName;
                            dbConn.Update(quest);
                        }
                    }
                    else
                    {
                        //add like
                        var newdata = new Deca_FAQ_Rating();
                        newdata.FAQId = Id;
                        newdata.Like = true;
                        newdata.UserName = currentUser.UserName;
                        newdata.CreatedAt = DateTime.Now;
                        newdata.CreatedBy = currentUser.UserName;
                        dbConn.Insert(newdata);

                        var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                        quest.Like += 1;
                        quest.UpdatedAt = DateTime.Now;
                        quest.UpdatedBy = currentUser.UserName;
                        dbConn.Update(quest);
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult Unlike(int Id)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<Deca_FAQ_Rating>("FAQId={0} AND UserName={1}", Id, currentUser.UserName);
                    if (data != null)
                    {
                        if (!data.Unlike)
                        {
                            data.Unlike = true;
                            data.UpdatedAt = DateTime.Now;
                            data.UpdatedBy = currentUser.UserName;
                            dbConn.Update(data);

                            var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                            quest.Unlike += 1;
                            quest.UpdatedAt = DateTime.Now;
                            quest.UpdatedBy = currentUser.UserName;
                            dbConn.Update(quest);
                        }
                        else
                        {
                            data.Unlike = false;
                            data.UpdatedAt = DateTime.Now;
                            data.UpdatedBy = currentUser.UserName;
                            dbConn.Update(data);

                            var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                            quest.Unlike -= 1;
                            quest.UpdatedAt = DateTime.Now;
                            quest.UpdatedBy = currentUser.UserName;
                            dbConn.Update(quest);
                        }
                    }
                    else
                    {
                        var newdata = new Deca_FAQ_Rating();
                        newdata.FAQId = Id;
                        newdata.Unlike = true;
                        newdata.UserName = currentUser.UserName;
                        newdata.CreatedAt = DateTime.Now;
                        newdata.CreatedBy = currentUser.UserName;
                        dbConn.Insert(newdata);

                        var quest = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
                        quest.Unlike += 1;
                        quest.UpdatedAt = DateTime.Now;
                        quest.UpdatedBy = currentUser.UserName;
                        dbConn.Update(quest);
                    }
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
        [ValidateInput(false)]
        public ActionResult Comment(int Id, string content)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var comment = new Deca_FAQ_Comment();
                    comment.UserName = currentUser.UserName;
                    comment.QuestionId = Id;
                    comment.Content = content;
                    comment.CreatedAt = DateTime.Now;
                    comment.CreatedBy = currentUser.UserName;
                    dbConn.Insert(comment);
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult GetTopic()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Deca_Topic>();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateQuestion(int TopicId, string Name, string Question)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var faq = new Deca_FAQ();
                    faq.TopicId = TopicId;
                    faq.Question = Question;
                    faq.CreatedAt = DateTime.Now;
                    faq.CreatedBy = currentUser.UserName;
                    dbConn.Insert(faq);
                    return Json(new { success = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }
    }
}