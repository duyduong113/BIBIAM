using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using ERPAPD.Models;
using System.Dynamic;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;


namespace ERPAPD.Controllers
{
    [Authorize]
    public class CRMSurveyFormController : CustomController
    {
        //
        // GET: /SurveyManagement/
        public ActionResult Index()
        {
            
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                    ViewBag.listSurveyCategory = dbConn.Select<CRM_Survey_Management>(@"SELECT * FROM CRM_Survey_Management WHERE [Active] = 1 AND [Status] <> N'Kết thúc' AND (StartDate < GETDATE() AND GETDATE() < EndDate ) ");
                    return View();
               
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
      
        //Used
        [HttpPost]
        public ActionResult GetListCustomerBySurvey(int ID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<CRM_Survey_Management_Customer>("SurveyManagementID='" + ID + "' AND IsDone=0");
                return Json(new { success = true, data = data });
            }
        }
        //Used
        [HttpPost]
        public ActionResult GetDetailCustomerBySurvey(int ID, string CustomerID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<CRM_Survey_Management_Customer>("Id='" + ID + "'");
                return Json(new { success = true, data = data });
            }
        }
        //Used
        [HttpPost]
        public ActionResult SaveSurvey(CRM_Survey_Management_Proceeded model)
        {
            using (var dbConn = OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
            {
                if (ModelState.IsValid && asset.Update)
                {
                    try
                    {
                        //current survey management:
                        var currentSurvey = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + model.SurveyManagementID);
                        var write = new CRM_Survey_Management_Proceeded();
                        string query = @"SELECT q.Id, q.CategoryID,q.Type, q.QuestionID,q.[Description] FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + model.SurveyManagementID + " Order By mq.SortOrder ASC";
                        var listQuestion = dbConn.Select<CRM_Survey_Question>(query);
                        int count = 0;
                        foreach (var item in listQuestion)
                        {
                            if (item.Type == "Multi choice")
                            {
                                item.results = CRM_Survey_Question.GetQuestionList(item.QuestionID);
                                write.SurveyManagementID = model.SurveyManagementID;
                                write.Source = model.Source;
                                write.QuestionID = item.QuestionID;
                                write.QuestionType = item.Type;
                                write.AnswerID = !string.IsNullOrEmpty(Request[item.QuestionID + item.QuestionID]) ? Request[item.QuestionID + item.QuestionID] : "";
                                //get list answer description
                                if (write.AnswerID == "")
                                {
                                    count++;
                                    continue;
                                    //throw new Exception("Chọn ít nhất một trả lời cho câu '" + item.Description + "'");
                                }
                                write.Answer = "";
                                var listAnswer = dbConn.Select<CRM_Survey_AnswerList>("QuestionID={0}", write.QuestionID);
                                //save maping vao cau tra loi
                                for (int i = 0; i < write.AnswerID.Split(',').Length; i++)
                                {
                                    write.Answer += listAnswer.FirstOrDefault(a => a.AnswerID == write.AnswerID.Split(',')[i].Trim()).AnswerDescription + ", ";
                                }
                                write.Answer = write.Answer.Trim();
                                write.Answer = write.Answer.Substring(0, write.Answer.Length - 1);
                                write.CustomerID = model.CustomerID;
                                write.CustomerName = model.CustomerName;
                                write.Phone = model.Phone;
                                write.PhysicalID = model.PhysicalID;
                                write.CustomerRank = model.CustomerRank;
                                write.Merchant = model.Merchant;
                                write.OrderID = model.OrderID;
                                write.Carrier = model.Carrier;


                                write.CreatedAt = DateTime.Now;
                                write.CreatedBy = currentUser.UserName;
                                write.UpdatedAt = DateTime.Parse("1900-01-01");
                                write.UpdatedBy = "";
                                dbConn.Insert(write);
                                write.Id = int.Parse(dbConn.GetLastInsertId().ToString());
                                foreach (var answer in item.results.Where(a => write.AnswerID.Contains(a.AnswerID)))
                                {
                                    if (answer.Answer == "Yes")
                                    {
                                        CRM_Survey_Management_Proceeded_Answer pa = new CRM_Survey_Management_Proceeded_Answer();
                                        pa.ProceededID = write.Id;
                                        pa.AnswerID = answer.AnswerID;
                                        pa.Answer = !string.IsNullOrEmpty(Request["Answer_" + item.QuestionID + answer.AnswerID]) ? Request["Answer_" + item.QuestionID + answer.AnswerID].Trim() : "";
                                        dbConn.Insert(pa);
                                    }
                                }
                            }
                            else if (item.Type == "Single choice")
                            {
                                write.SurveyManagementID = model.SurveyManagementID;
                                write.Source = model.Source;
                                write.QuestionID = item.QuestionID;
                                write.QuestionType = item.Type;
                                write.AnswerID = !string.IsNullOrEmpty(Request[item.QuestionID + item.QuestionID]) ? Request[item.QuestionID + item.QuestionID] : "";
                                if (write.AnswerID == "")
                                {
                                    count++;
                                    continue;
                                    //throw new Exception("Chọn trả lời cho câu '" + item.Description + "'");
                                }

                                var listAnswer = dbConn.FirstOrDefault<CRM_Survey_AnswerList>("AnswerID={0}", write.AnswerID);
                                //save maping vao cau tra loi
                                write.Answer = !string.IsNullOrEmpty(Request["Answer_" + item.QuestionID + write.AnswerID]) ? listAnswer.AnswerDescription + " - " + Request["Answer_" + item.QuestionID + write.AnswerID].Trim() : listAnswer.AnswerDescription;
                                write.CustomerID = model.CustomerID;
                                write.CustomerName = model.CustomerName;
                                write.Phone = model.Phone;
                                write.PhysicalID = model.PhysicalID;
                                write.CustomerRank = model.CustomerRank;
                                write.Merchant = model.Merchant;
                                write.OrderID = model.OrderID;
                                write.Carrier = model.Carrier;

                                write.CreatedAt = DateTime.Now;
                                write.CreatedBy = currentUser.UserName;
                                write.UpdatedAt = DateTime.Parse("1900-01-01");
                                write.UpdatedBy = "";
                                dbConn.Insert(write);
                            }
                            else
                            {
                                write.SurveyManagementID = model.SurveyManagementID;
                                write.Source = model.Source;
                                write.QuestionID = item.QuestionID;
                                write.QuestionType = item.Type;
                                write.AnswerID = !string.IsNullOrEmpty(Request[item.QuestionID + item.QuestionID]) ? Request[item.QuestionID + item.QuestionID] : "";
                                write.Answer = !string.IsNullOrWhiteSpace(Request["Answer_" + item.QuestionID]) ? Request["Answer_" + item.QuestionID].Trim() : "";
                                if (write.Answer == "")
                                {
                                    count++;
                                    continue;
                                    //throw new Exception("Chọn trả lời cho câu '" + item.Description + "'");
                                }
                                write.CustomerID = model.CustomerID;
                                write.CustomerName = model.CustomerName;
                                write.Phone = model.Phone;
                                write.PhysicalID = model.PhysicalID;
                                write.CustomerRank = model.CustomerRank;
                                write.Merchant = model.Merchant;
                                write.OrderID = model.OrderID;
                                write.Carrier = model.Carrier;

                                write.CreatedAt = DateTime.Now;
                                write.CreatedBy = currentUser.UserName;
                                write.UpdatedAt = DateTime.Parse("1900-01-01");
                                write.UpdatedBy = "";
                                dbConn.Insert(write);
                            }

                        }
                        if (count >= listQuestion.Count)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, message = "Phải trả lời ít nhất một câu hỏi để lưu kết quả survey. Bạn có thể bỏ qua khách hàng này bằng cách click vào 'Bỏ qua'" });
                        }
                        //update survey management.
                        currentSurvey.Actual = currentSurvey.Actual + 1;
                        if (currentSurvey.Target > 0)
                            currentSurvey.Percent = ((double)currentSurvey.Actual / (double)currentSurvey.Target) * 100;
                        currentSurvey.Status = "Đang thực hiện";
                        dbConn.Update(currentSurvey);
                        //update nguoi lam neu source la surveycustomer
                        //if (model.Source == "SurveyCustomer")
                        //    dbConn.Update<CRM_Survey_Management_Customer>(set: "IsDone=1", where: "Phone='" + model.Phone + "'");
                        dbConn.Update<CRM_Survey_Management_Customer>(set: "IsDone=1", where: "Phone='" + model.Phone + "'");
                        dbTrans.Commit();
                        return Json(new { success = true, });

                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
                else
                {
                    if (!asset.Update) return Json(new { error = true, message = "Không có quyền cập nhật." });
                    string message = "Thông tin khách hàng còn thiếu. Cập nhật thông tin khách hàng để lưu khảo sát (Tên - ĐT)";
                    return Json(new { success = false, message = message });
                }
            }

        }
        //Used
        public ActionResult GetQuestionByManagementID(int ManagementID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    //get current survey:
                    var management = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + ManagementID);
                    if (management.Status == "Kết thúc")
                    {
                        return Json(new { success = false, alert = "Khảo sát này đã kết thúc." });
                    }
                    if (management.StartDate < DateTime.Now && DateTime.Now < management.EndDate)
                    {
                        string query = @"SELECT q.Id, q.CategoryID,q.Type, q.QuestionID,q.[Description] FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + ManagementID + " Order By mq.SortOrder ASC";
                        var data = dbConn.Select<CRM_Survey_Question>(query);
                        foreach (var item in data)
                        {
                            item.results = CRM_Survey_Question.GetQuestionList(item.QuestionID);
                        }
                        return Json(new { success = true, data = data });
                    }
                    else
                    {
                        return Json(new { success = false, alert = "Thời điểm hiện tại không nằm trong thời gian làm survey." });
                    }
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền survey" });
                }
            }

        }
        //Used
        public ActionResult LoadNextSurveyCustomer(int ManagementID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var nextCustomer = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("SELECT * FROM CRM_Survey_Management_Customer WHERE SurveyManagementID = {0} AND IsDone=0 AND Id NOT IN (SELECT ManagementCustomerID FROM CRM_Survey_Management_Skip WHERE SurveyManagementID = {0})", ManagementID);
                if (nextCustomer == null)
                {
                    return Json(new { success = false, message = "Không có khách hàng trong danh sách hoặc tất cả khách hàng đã làm survey." });
                }
                else
                {
                    return Json(new { success = true, data = nextCustomer });
                }
            }
        }
        [HttpPost]
        public ActionResult EndSurvey(string data)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listdata)
                        {
                            var current = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + item);
                            if (current.Status == "Đang thực hiện")
                            {
                                current.Status = "Kết thúc";
                                current.UpdatedAt = DateTime.Now;
                                current.UpdatedBy = currentUser.UserName;
                                dbConn.Update(current);
                            }
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Không có quyền kết thúc." });
                }

        }
        //Result by Customer block
        public PartialViewResult PartialResultByCustomer(string ManagementID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                //tra ra danh sach header.
                List<ExpandoObject> result = new List<ExpandoObject>();
                //2 cot dau tien
                dynamic Ngaylam = new ExpandoObject();
                Ngaylam.Name = "CreatedAt";
                Ngaylam.Title = "Ngày làm";
                result.Add(Ngaylam);
                dynamic khach = new ExpandoObject();
                khach.Name = "CustomerInfo";
                khach.Title = "Thông tin khách hàng";
                result.Add(khach);
                //lay danh sach cau hoi, bind thanh header.
                string query = @"SELECT mq.Id, q.QuestionID, q.[Description] FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + ManagementID + " ORDER BY mq.SortOrder";
                var listQuestion = dbConn.Select<CRM_Survey_Management_Question>(query);
                foreach (var question in listQuestion)
                {
                    dynamic dynamicQuestion = new ExpandoObject();
                    dynamicQuestion.Name = question.QuestionID;
                    dynamicQuestion.Title = question.Description;
                    result.Add(dynamicQuestion);
                }
                return PartialView("_GridResultByCustomer", result);
            }
        }
       
        //Used
        [HttpPost]
        public ActionResult SkipSurvey(CRM_Survey_Management_Skip model)
        {
            if (asset.Update && ModelState.IsValid)
            {
                try
                {
                    using (var dbConn = OrmliteConnection.openConn())
                    {
                        model.ManagementCustomerID = int.Parse(Request["ManagementCustomerIDSkip"]);
                        model.SurveyManagementID = int.Parse(Request["SurveyManagementIDSkip"]);
                        model.CreatedAt = DateTime.Now;
                        model.CreatedBy = currentUser.UserName;
                        dbConn.Insert(model);
                        return Json(new { success = true, message = "" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                if (!asset.Update)
                {
                    return Json(new { success = false, message = "Bạn không có quyền làm khảo sát, hãy liên lạc với 1080 hoặc trưởng nhóm để được cấp quyền." });
                }
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { success = false, message = message.Remove(message.Length - 1) });
            }
        }
    }
}