using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using System.Dynamic;
using OfficeOpenXml;
using ERPAPD.Helpers;
using Hangfire;
using ServiceStack.OrmLite;


namespace ERPAPD.Controllers
{
    [Authorize]
    public class CRMSurveySettingController : CustomController
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
                    ViewBag.Category = dbConn.Select<CRM_Survey_Category>().OrderBy(a => a.Id);
                    ViewBag.ListGroup = dbConn.Select<Groups>();
                    ViewBag.listUser = dbConn.Select<Users>("");
                  //  ViewBag.listDepartment = dbConn.Select<Deca_Department>("Active = 1");
                    ViewBag.listSurveyCategory = dbConn.Select<CRM_Survey_Management>("Active = 1");
                    return View();
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
        }
        [HttpPost]
        public ActionResult GetListSurvey(int ID)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.Select<CRM_Survey_Management>("Id={0}", ID);
                    return Json(new { success = true, data = data });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public ActionResult SurveyManagement_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Survey_Management>(request));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SurveyManagement_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Management> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                var checkColumName = dbConn.Select<CRM_Survey_Management>("[Title] = {0}", item.Title).FirstOrDefault();
                                if (checkColumName != null)
                                {
                                    ModelState.AddModelError("", "Tiêu đề đã có");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                item.Detail = !string.IsNullOrEmpty(item.Detail) ? item.Detail : "";
                                item.Actual = 0;
                                item.Percent = 0;
                                item.QuestionCount = 0;
                                item.StartDate = DateTime.Parse(item.StartDate.ToString());
                                item.EndDate = DateTime.Parse(item.EndDate.ToString());
                                item.CreatedAt = DateTime.Now;
                                item.CreatedBy = currentUser.UserName;
                                item.UpdatedAt = DateTime.Parse("1900-01-01");
                                item.UpdatedBy = "";
                                item.Status = "Mới";
                                dbConn.Insert(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền tạo");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request));
                }
        }
        public ActionResult SurveyManagement_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  
            IEnumerable<CRM_Survey_Management> list)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                item.Detail = !string.IsNullOrEmpty(item.Detail) ? item.Detail : "";
                                item.Active = item.Active;
                                item.UpdatedAt = DateTime.Now;
                                item.UpdatedBy = currentUser.UserName;
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "You don't have permission to update record");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult ActionUser_Read([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Survey_Management_User>(request, "SurveyManagementID=" + SurveyManagementID));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Question_Read([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string query = @"SELECT DISTINCT q.Id, q.QuestionID,q.[Description] 
                                    FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + SurveyManagementID;
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_Survey_Management_Question>(request, query, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Question_ReadFilter([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {  
                string query="";
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    if (int.Parse(SurveyManagementID) == 0)
                    {
                        //Không lấy data
                        query = @"Select * from CRM_Survey_Question where 1= 0";
                    }
                    else
                    {
                        query = @"SELECT * FROM ( SELECT q.Id, q.CategoryID, q.Type, q.QuestionID,q.[Description]
								 FROM CRM_Survey_Question q
								 WHERE q.QuestionID NOT IN ( 
								    SELECT QuestionID FROM CRM_Survey_Management_Question mq
									WHERE mq.SurveyManagementID = " + SurveyManagementID + ")) A ";
                    }
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_Survey_Management_Question>(request, query, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Customer_Read([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Survey_Management_Customer>(request, "SurveyManagementID=" + SurveyManagementID));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Customer_Read_Filter([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string query = "";
                    if (int.Parse(SurveyManagementID) == 0)
                    {
                        //Không lấy data
                        query = @"Select * from ERPAPD_Customer where 1= 0";
                    }
                    else
                    {
                        query = @"SELECT * FROM ( SELECT   c.RowID,c.CustomerID,c.CustomerName,c.Phone
                                    FROM ERPAPD_Customer c
                                    WHERE c.CustomerID NOT IN (
                                    SELECT CustomerID FROM CRM_Survey_Management_Customer mc
                                    WHERE mc.SurveyManagementID =" + SurveyManagementID + ")) A ";
                    }
                    return Json(KendoApplyFilter.KendoDataByQuery<ERPAPD_Customer>(request, query, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Customer_Create_Filter(int SurveyManagementID, string CustomerID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        var current = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + SurveyManagementID);
                        if (current.Status == "Kết thúc")
                        {
                            return Json(new { success = false, message = "Khảo sát đã kết thúc" });
                        }
                        // string[] separators = { "@@" };
                        // var listdata = CustomerID.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        //foreach (var item in listdata)
                        //{
                            var checkColumName = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("CustomerID='" + CustomerID + "' AND SurveyManagementID=" + SurveyManagementID);
                            if (checkColumName != null)
                            {
                                dbTrans.Rollback();
                                return Json(new { success = false, message = "Khách hàng số điện thoại: " + checkColumName.Phone + " có thông tin trùng với khách hàng khác trong survey này." });
                            }
                            var currentCus = dbConn.FirstOrDefault<ERPAPD_Customer>("CustomerID='" + CustomerID+"'");
                            var scum = new CRM_Survey_Management_Customer();
                            scum.IsDone = false;
                            scum.SurveyManagementID = SurveyManagementID;
                            scum.Name = currentCus.CustomerName;
                            scum.Phone = currentCus.Phone;
                            scum.OrderID = "";
                            scum.PhysicalID = "";
                            scum.Merchant = "";
                            scum.CustomerID = CustomerID;
                            dbConn.Insert(scum);
                        //}
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {    
                        dbTrans.Rollback();
                        return Json(new { success = false, message = e.Message });
                       
                    }
                    return Json(new { success = true });
                }
                else
                { 
                    dbTrans.Rollback();
                    return Json(new { success = false, message = "Không có quyền tạo" });
                }
        }
        public ActionResult QuestionResult_Read([DataSourceRequest] DataSourceRequest request, string SurveyManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    string query = @"SELECT  q.Id,q.QuestionID,q.[Description],mq.SurveyManagementID,q.Type,mq.SortOrder FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + SurveyManagementID;
                    return Json(KendoApplyFilter.KendoDataByQuery<CRM_Survey_Management_Question>(request, query, ""));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SurveyListStatisticsDetail_Read([DataSourceRequest] DataSourceRequest request, string QuestionID, int ManagementID)
        {
            if (asset.View)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //khai bao list tra ra
                    List<CRM_Survey_Management> Result = new List<CRM_Survey_Management>();
                    //lay cau hoi hien tai
                    var cQuestion = dbConn.FirstOrDefault<CRM_Survey_Question>("QuestionID={0}", QuestionID);
                    //check neu la free text
                    if (cQuestion.Type == "Free text")
                    {
                        //lay tat ca cau tra loi ra.
                        var allFreetext = dbConn.Select<CRM_Survey_Management_Proceeded>("QuestionID={0} AND SurveyManagementID={1}", QuestionID, ManagementID);
                        int index = 0;
                        foreach (var freetext in allFreetext)
                        {
                            CRM_Survey_Management management = new CRM_Survey_Management();
                            management.Detail = freetext.Answer;
                            management.Title = freetext.CustomerName;
                            Result.Insert(index, management);
                            index++;
                        }
                    }
                    else
                    {
                        //danh sach cau tra loi 
                        var listAnswer = dbConn.Select<CRM_Survey_AnswerList>("QuestionID={0}", QuestionID);
                        //total so nguoi tra loi cau nay
                        double totalAnswer = dbConn.Count<CRM_Survey_Management_Proceeded>(a => a.QuestionID == QuestionID && a.SurveyManagementID == ManagementID);
                        //voi moi cau tra loi, tao mot dong trong result
                        int index = 0;
                        foreach (var answer in listAnswer)
                        {
                            CRM_Survey_Management management = new CRM_Survey_Management();
                            management.Detail = answer.AnswerDescription;
                            management.Actual = (int)dbConn.Count<CRM_Survey_Management_Proceeded>(a => a.AnswerID.Contains(answer.AnswerID));
                            management.Percent = ((double)management.Actual / totalAnswer) * 100;
                            Result.Insert(index, management);
                            index++;
                        }
                    }
                    return Json(Result.ToDataSourceResult(request));


                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
//        public ActionResult CustomerSearchRead([DataSourceRequest] DataSourceRequest request, string ManagementID, string Source, string Keywork)
//        {
//            if (asset.View)
//            {
//                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
//                {
//                    if (Source == "SurveyCustomer")
//                    {
//                        string query = @"SELECT Id, CustomerID, Name AS Fullname,Phone,PhysicalID, Email
//                                        FROM	CRM_Survey_Management_Customer 
//                                        WHERE	SurveyManagementID =" + ManagementID + @" AND 
//                                                IsDone = 0 AND  (
//		                                        Name COLLATE Latin1_General_CI_AI LIKE '%" + Keywork + @"%' OR
//		                                        Phone LIKE '%" + Keywork + @"%' OR
//		                                        Merchant LIKE '%" + Keywork + @"%' OR
//		                                        CustomerRank LIKE '%" + Keywork + "%')";
//                        return Json(KendoApplyFilter.KendoDataByQuery<Deca_Customer_Index>(request, query, ""));
//                    }
//                    else
//                    {
//                        string customWhere = @" Phone<>'' AND CONTAINS([Meta],'""*" + Keywork + @"*""')";
//                        return Json(KendoApplyFilter.KendoData<Deca_Customer_Index>(request, customWhere));
//                    }
//                }
//            }
//            else
//            {
//                return RedirectToAction("NoAccessRights", "Error");
//            }
//        }
        public ActionResult DeleteSurveyManagement(string data)
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
                            dbConn.Delete<CRM_Survey_Management>(where: "Id={0} AND [Status] = N'Mới'".Params(item));
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền xóa." });
                }
        }
        public ActionResult GetQuestionByCategory(string CategoryID)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            var data = dbConn.Select<CRM_Survey_Question>("Active =1 AND CategoryID = '" + CategoryID + "'");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteQuestion(string data, int id)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Delete)
                {
                    try
                    {
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        var currentSuvey = dbConn.FirstOrDefault<CRM_Survey_Management>("Id={0}", id);
                        if (currentSuvey.Status != "Mới")
                        {
                            return Json(new { success = false, alert = "Khảo sát không thể cập nhật khi trạng thái khác Mới" });
                        }
                        foreach (var item in listdata)
                        {
                            var checkexits = dbConn.FirstOrDefault<CRM_Survey_Management_Question>("QuestionID= {0} AND SurveyManagementID= {1}", item, id);
                            if (checkexits != null)
                            {
                                dbConn.Delete(checkexits);
                                currentSuvey.QuestionCount = currentSuvey.QuestionCount - 1;
                            }
                        }
                        currentSuvey.UpdatedAt = DateTime.Now;
                        currentSuvey.UpdatedBy = currentUser.UserName;
                        dbConn.Update(currentSuvey);
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền cập nhật." });
                }
        }
        public ActionResult SaveQuestion(CRM_Survey_Management_Question model)
        {
            if (asset.Update && ModelState.IsValid)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        //lay survey hien tai
                        var currentSurvey = dbConn.FirstOrDefault<CRM_Survey_Management>("Id={0}", model.SurveyManagementID);
                        if (currentSurvey.Status != "Mới")
                        {
                            return Json(new { success = false, message = "Khảo sát không thể cập nhật khi trạng thái khác Mới" });
                        }
                        //get current question cua survey nay
                        var currentQuestion = dbConn.Select<CRM_Survey_Management_Question>("SurveyManagementID={0}", model.SurveyManagementID);
                        //check question neu la all
                        if (model.QuestionID=="All")
                        {
                            //lay tat ca question trong category
                            var questionlist = dbConn.Select<CRM_Survey_Question>("CategoryID={0}", model.CategoryID);
                            //save question chua co
                            if (questionlist != null)
                            {
                                foreach (var question in questionlist)
                                {
                                    //neu cau hoi nay chua co trong danh sach cau hoi thi add vao.
                                    if (currentQuestion.Count(a => a.QuestionID == question.QuestionID) < 1)
                                    {
                                        var newQuestion = new CRM_Survey_Management_Question();
                                        newQuestion.QuestionID = question.QuestionID;
                                        newQuestion.SortOrder = model.SortOrder;
                                        newQuestion.SurveyManagementID = model.SurveyManagementID;
                                        dbConn.Insert(newQuestion);
                                        //update so luong cau hoi
                                        currentSurvey.QuestionCount = currentSurvey.QuestionCount + 1;
                                    }
                                }
                            }
                        }
                        else //1 question.
                        {
                            if (currentQuestion.Count(a => a.QuestionID == model.QuestionID) < 1)
                            {
                                var newQuestion = new CRM_Survey_Management_Question();
                                newQuestion.QuestionID = model.QuestionID;
                                newQuestion.SortOrder = model.SortOrder;
                                newQuestion.SurveyManagementID = model.SurveyManagementID;
                                dbConn.Insert(newQuestion);
                                //update so luong cau hoi
                                currentSurvey.QuestionCount = currentSurvey.QuestionCount + 1;
                            }
                        }
                        //update survey
                        currentSurvey.UpdatedAt = DateTime.Now;
                        currentSurvey.UpdatedBy = currentUser.UserName;
                        dbConn.Update(currentSurvey);
                        dbTrans.Commit();
                        return Json(new { success = true, message = "success" });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                    //save lai neu question chua co 
                }
            }
            else
            {
                if (!asset.Update) return Json(new { error = true, message = "Không có quyền cập nhật" });
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { error = true, message = message });
            }
        }
        public ActionResult Customer_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Management_Customer> list, int SurveyManagementID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Create)
                {
                    try
                    {
                        var current = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + SurveyManagementID);
                        if (current.Status == "Kết thúc")
                        {
                            ModelState.AddModelError("error", "Khảo sát đã kết thúc");
                            return Json(list.ToDataSourceResult(request, ModelState));
                        }
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                var checkColumName = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("([Phone] = {0} OR ([PhysicalID]<>'' AND [PhysicalID]={1})) AND SurveyManagementID={2}", item.Phone, item.PhysicalID, SurveyManagementID);
                                if (checkColumName != null)
                                {
                                    dbTrans.Rollback();
                                    ModelState.AddModelError("", "Khách hàng số điện thoại " + item.Phone + " có thông tin trùng với khách hàng khác trong survey này. ");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                item.IsDone = false;
                                item.SurveyManagementID = SurveyManagementID;
                                dbConn.Insert(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền tạo");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult Customer_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<CRM_Survey_Management_Customer> list, int SurveyManagementID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                if (asset.Update)
                {
                    try
                    {
                        if (list != null && ModelState.IsValid)
                        {
                            foreach (var item in list)
                            {
                                if (item.IsDone)
                                {
                                    ModelState.AddModelError("error", "Không thể cập nhật thông tin khách hàng đã làm khảo sát");
                                    dbTrans.Rollback();
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                var checkColumName = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("([Phone] = {0} OR ([PhysicalID]<>'' AND [PhysicalID]={1})) AND SurveyManagementID={2} AND Id <>{3}", item.Phone, item.PhysicalID, SurveyManagementID, item.Id);
                                if (checkColumName != null)
                                {
                                    dbTrans.Rollback();
                                    ModelState.AddModelError("", "Khách hàng số điện thoại " + item.Phone + " có thông tin trùng với khách hàng khác trong survey này. ");
                                    return Json(list.ToDataSourceResult(request, ModelState));
                                }
                                dbConn.Update(item);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("error", e.Message);
                        dbTrans.Rollback();
                        return Json(list.ToDataSourceResult(request, ModelState));
                    }
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền cập nhật");
                    dbTrans.Rollback();
                    return Json(list.ToDataSourceResult(request, ModelState));
                }
        }
        public ActionResult DeleteCustomer(string data, int id)
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
                            var checkexits = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("Id = {0} AND SurveyManagementID= {1}", item, id);
                            if (checkexits.IsDone)
                            {
                                dbTrans.Rollback();
                                return Json(new { success = false, alert = "Không thể xóa khách hàng đã làm khảo sát" });
                            }
                            if (checkexits != null && checkexits.IsDone == false)
                            {
                                dbConn.Delete(checkexits);
                            }
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền cập nhật." });
                }
        }
        public ActionResult ImportCustomer(string SurveyManagementID)
        {
            try
            {

                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                        using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                        {
                            var current = dbConn.FirstOrDefault<CRM_Survey_Management>("Id=" + SurveyManagementID);
                            if (current.Status == "Kết thúc")
                            {
                                return Json(new { success = false, error = "Khảo sát đã kết thúc." });
                            }
                            string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                            string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                            if (System.IO.File.Exists(fileLocation))
                                System.IO.File.Delete(fileLocation);

                            Request.Files["FileUpload"].SaveAs(fileLocation);

                            var rownumber = 2;
                            var total = 0;
                            FileInfo fileInfo = new FileInfo(fileLocation);
                            var excelPkg = new ExcelPackage(fileInfo);
                            FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyCustomer.xlsx"));
                            template.CopyTo(errorFileLocation);
                            FileInfo _fileInfo = new FileInfo(errorFileLocation);
                            var _excelPkg = new ExcelPackage(_fileInfo);
                            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DC_SurveyCustomer"];
                            ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DC_SurveyCustomer"];
                            int totalRows = oSheet.Dimension.End.Row;
                            for (int i = 2; i <= totalRows; i++)
                            {
                                string Id = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                                string OrderID = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                                string Name = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                                string Phone = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                                string CustomerRank = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                                string Merchant = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                                string Carrier = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                                try
                                {
                                    var check = string.IsNullOrWhiteSpace(Id) ? null : dbConn.Select<CRM_Survey_Management_Customer>("Id =" + Id).FirstOrDefault();
                                    if (String.IsNullOrWhiteSpace(Name))
                                    {
                                        eSheet.Cells[rownumber, 1].Value = Id;
                                        eSheet.Cells[rownumber, 2].Value = OrderID;
                                        eSheet.Cells[rownumber, 3].Value = Name;
                                        eSheet.Cells[rownumber, 4].Value = Phone;
                                        eSheet.Cells[rownumber, 5].Value = CustomerRank;
                                        eSheet.Cells[rownumber, 6].Value = Merchant;
                                        eSheet.Cells[rownumber, 7].Value = Carrier;
                                        eSheet.Cells[rownumber, 8].Value = "Tên khách hàng là bắt buộc";
                                        rownumber++;
                                    }
                                    else if (String.IsNullOrWhiteSpace(Phone))
                                    {
                                        eSheet.Cells[rownumber, 1].Value = Id;
                                        eSheet.Cells[rownumber, 2].Value = OrderID;
                                        eSheet.Cells[rownumber, 3].Value = Name;
                                        eSheet.Cells[rownumber, 4].Value = Phone;
                                        eSheet.Cells[rownumber, 5].Value = CustomerRank;
                                        eSheet.Cells[rownumber, 6].Value = Merchant;
                                        eSheet.Cells[rownumber, 7].Value = Carrier;
                                        eSheet.Cells[rownumber, 8].Value = "Số điện thoại là bắt buộc";
                                        rownumber++;
                                    }
                                    else
                                    {
                                        if (check != null)
                                        {
                                            //update
                                            //kiem tra phone va name co trung voi data cu hay ko
                                            var checkColumName = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("[Phone] = {0}  AND SurveyManagementID={1} AND Id <>{2}", Phone, SurveyManagementID, Id);
                                            if (checkColumName != null)
                                            {
                                                eSheet.Cells[rownumber, 1].Value = Id;
                                                eSheet.Cells[rownumber, 2].Value = OrderID;
                                                eSheet.Cells[rownumber, 3].Value = Name;
                                                eSheet.Cells[rownumber, 4].Value = Phone;
                                                eSheet.Cells[rownumber, 5].Value = CustomerRank;
                                                eSheet.Cells[rownumber, 6].Value = Merchant;
                                                eSheet.Cells[rownumber, 7].Value = Carrier;
                                                eSheet.Cells[rownumber, 8].Value = "Số điện thoại hoặc CMND trùng với khách hàng trong KS này";
                                                rownumber++;
                                                continue;
                                            }
                                            else
                                            {
                                                check.Name = Name;
                                                check.Phone = Phone;
                                                check.CustomerRank = CustomerRank;
                                                check.Carrier = Carrier;
                                                check.Merchant = Merchant;
                                                check.OrderID = OrderID;
                                                dbConn.Update(check);
                                            }

                                        }
                                        else
                                        {
                                            var checkColumName = dbConn.FirstOrDefault<CRM_Survey_Management_Customer>("[Phone] = {0} AND SurveyManagementID={1} AND Id <>{2}", Phone, SurveyManagementID, Id);
                                            if (checkColumName != null)
                                            {
                                                eSheet.Cells[rownumber, 1].Value = Id;
                                                eSheet.Cells[rownumber, 2].Value = OrderID;
                                                eSheet.Cells[rownumber, 3].Value = Name;
                                                eSheet.Cells[rownumber, 4].Value = Phone;
                                                eSheet.Cells[rownumber, 5].Value = CustomerRank;
                                                eSheet.Cells[rownumber, 6].Value = Merchant;
                                                eSheet.Cells[rownumber, 7].Value = Carrier;
                                                eSheet.Cells[rownumber, 8].Value = "Số điện thoại hoặc CMND trùng với khách hàng trong KS này";
                                                rownumber++;
                                                continue;
                                            }
                                            else
                                            {
                                                var data = new CRM_Survey_Management_Customer();
                                                data.CustomerID = "";
                                                data.IsDone = false;
                                                data.Name = Name;
                                                data.Phone = Phone;
                                                data.CustomerRank = CustomerRank;
                                                data.Carrier = Carrier;
                                                data.Merchant = Merchant;
                                                data.OrderID = OrderID;
                                                data.SurveyManagementID = int.Parse(SurveyManagementID.Trim());
                                                dbConn.Save(data);
                                            }
                                        }
                                        total++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    eSheet.Cells[rownumber, 1].Value = Id;
                                    eSheet.Cells[rownumber, 2].Value = OrderID;
                                    eSheet.Cells[rownumber, 3].Value = Name;
                                    eSheet.Cells[rownumber, 4].Value = Phone;
                                    eSheet.Cells[rownumber, 5].Value = CustomerRank;
                                    eSheet.Cells[rownumber, 6].Value = Merchant;
                                    eSheet.Cells[rownumber, 7].Value = Carrier;
                                    eSheet.Cells[rownumber, 8].Value = e.Message;
                                    rownumber++;
                                    continue;
                                }
                            }
                            dbTrans.Commit();
                            _excelPkg.Save();
                            return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "File extension is not valid. *.xlsx please." });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "File upload null" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public ActionResult ExportCustomer(string SurveyManagementID)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                if (asset.Export)
                {
                    var request = new DataSourceRequest();
                    request.Sorts = new List<Kendo.Mvc.SortDescriptor>();
                    request.Filters = new List<Kendo.Mvc.IFilterDescriptor>();
                    var data = KendoApplyFilter.KendoData<CRM_Survey_Management_Customer>(request, "SurveyManagementID=" + SurveyManagementID);
                    IEnumerable datas = data.Data;

                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\CRM_SurveyCustomer.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);
                    ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["CRM_SurveyCustomer"];
                    int rowData = 1;
                    foreach (CRM_Survey_Management_Customer item in datas)
                    {
                        int i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = item.Id;
                        dataSheet.Cells[rowData, i++].Value = item.OrderID;
                        dataSheet.Cells[rowData, i++].Value = item.Name;
                        dataSheet.Cells[rowData, i++].Value = item.Phone;
                        dataSheet.Cells[rowData, i++].Value = item.CustomerRank;
                        dataSheet.Cells[rowData, i++].Value = item.Merchant;
                        dataSheet.Cells[rowData, i++].Value = item.Carrier;
                    }
                    //IEnumerable dataCategorys = dataCategory.ToDataSourceResult(request).Data;
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    string fileName = "CRM_SurveyCustomer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);

                }
                else
                {
                    ModelState.AddModelError("", "Không có quyền xuất ra excel");
                    return File("", //The binary data of the XLS file
                        "application/vnd.ms-excel", //MIME type of Excel files
                        "CRM_SurveyCustomer_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
                }
        }
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
                        if (model.Source == "SurveyCustomer")
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
        [HttpPost]
        public ActionResult GetListUsersByGroup(int GroupID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = dbConn.Select<Users>("Groups LIKE '%Id:" + GroupID + ",%'");
                return Json(new { success = true, data = data });
            }
        }
        public ActionResult DeleteActionUser(string data, int id)
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
                            var checkexits = dbConn.FirstOrDefault<CRM_Survey_Management_User>("Id = {0} AND SurveyManagementID= {1}", item, id);
                            if (checkexits != null)
                            {
                                dbConn.Delete(checkexits);
                            }
                        }
                        dbTrans.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, alert = ex.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, alert = "Không có quyền cập nhật." });
                }
        }
        public ActionResult SaveActionUser(CRM_Survey_Management_User model)
        {
            if (asset.Update && ModelState.IsValid)
            {
                using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        //lay survey hien tai
                        var currentSurvey = dbConn.FirstOrDefault<CRM_Survey_Management>("Id={0}", Request["SurveyManagementID2"]);
                        if (currentSurvey.Status == "Kết thúc")
                        {
                            return Json(new { success = false, message = "Khảo sát không thể cập nhật khi trạng thái là 'Kết thúc'" });
                        }
                        //get current user cua survey nay
                        var currentUserlist = dbConn.Select<CRM_Survey_Management_User>("SurveyManagementID={0}", Request["SurveyManagementID2"]);
                        //check question neu la all
                        if (model.listUserName.Contains("Tất cả"))
                        {
                            //lay tat ca userr trong group
                            var userlist = dbConn.Select<Users>("Groups LIKE '%Id:" + model.UserGroup + ",%'");
                            //save question chua co
                            if (userlist != null)
                            {
                                foreach (var user in userlist)
                                {
                                    //neu cau hoi nay chua co trong danh sach cau hoi thi add vao.
                                    if (currentUserlist.Count(a => a.UserName == user.UserName) < 1)
                                    {
                                        var newQuestion = new CRM_Survey_Management_User();
                                        newQuestion.UserName = user.UserName;
                                        newQuestion.UserGroup = model.UserGroup;
                                        newQuestion.SurveyManagementID = int.Parse(Request["SurveyManagementID2"]);
                                        newQuestion.FullName = user.FullName;
                                        dbConn.Insert(newQuestion);
                                    }
                                }
                            }
                        }
                        else //ko lay het.
                        {
                            //lay danh sach user nguoi dung da chon
                            foreach (string username in model.listUserName.Split(','))
                            {
                                var thisUsername = dbConn.FirstOrDefault<Users>("UserName={0}", username);
                                if (currentUserlist.Count(a => a.UserName == username) < 1)
                                {
                                    var newQuestion = new CRM_Survey_Management_User();
                                    newQuestion.UserName = thisUsername.UserName;
                                    newQuestion.UserGroup = model.UserGroup;
                                    newQuestion.SurveyManagementID = int.Parse(Request["SurveyManagementID2"]);
                                    newQuestion.FullName = thisUsername.FullName;
                                    dbConn.Insert(newQuestion);
                                }
                            }
                        }
                        //update survey
                        currentSurvey.UpdatedAt = DateTime.Now;
                        currentSurvey.UpdatedBy = currentUser.UserName;
                        dbConn.Update(currentSurvey);
                        dbTrans.Commit();
                        return Json(new { success = true, message = "success" });
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                    //save lai neu question chua co 
                }
            }
            else
            {
                if (!asset.Update) return Json(new { error = true, message = "Không có quyền cập nhật" });
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { error = true, message = message });
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
        public ActionResult ResultByCustomer_Read([DataSourceRequest] DataSourceRequest request, string ManagementID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var result = new DataSourceResult();
                //datatable ket qua
                DataTable dataResult = new DataTable("Statitics");

                //tra ra danh sach header.
                //2 cot dau tien
                //lay danh sach cau hoi, bind thanh header.
                string query = @"SELECT mq.Id, q.QuestionID, q.[Description] FROM CRM_Survey_Management_Question mq
                                    LEFT JOIN CRM_Survey_Question q
                                    ON mq.QuestionID = q.QuestionID
                                    WHERE mq.SurveyManagementID = " + ManagementID + " ORDER BY mq.SortOrder";
                var listQuestion = dbConn.Select<CRM_Survey_Management_Question>(query);
                //add collum to datatable
                dataResult.Columns.Add("CreatedAt", typeof(string));
                dataResult.Columns.Add("CustomerInfo", typeof(string));
                foreach (var question in listQuestion)
                {
                    dataResult.Columns.Add(question.QuestionID, typeof(string));
                }
                //get list customer đã làm survey dựa theo pagesize.
                string customerQuery = @"select * from (
	                                    SELECT  
		                                    ROW_NUMBER() OVER ( ORDER BY (SELECT NULL)) AS RowNum 
		                                    ,Phone
		                                    ,CustomerName
	                                    FROM (
		                                    SELECT Distinct Phone, CustomerName From CRM_Survey_Management_Proceeded Where SurveyManagementID = " + ManagementID + @"
	                                    )AS result
                                    )a WHERE RowNum > " + (request.Page - 1) * request.PageSize + " AND RowNum<= " + request.Page * request.PageSize;
                var listCustomer = dbConn.Select<CRM_Survey_Management_Proceeded>(customerQuery);
                result.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM (SELECT DISTINCT Phone,CustomerName FROM CRM_Survey_Management_Proceeded WHERE SurveyManagementID=" + ManagementID + ") a");
                //add row to datatable
                foreach (var customer in listCustomer)
                {
                    //moi khach hang la 1 row.
                    DataRow newRow = dataResult.NewRow();
                    newRow["CustomerInfo"] = customer.CustomerName + " - " + customer.Phone;
                    // danh sach cau tra loi cua khach hang nay
                    var listAnswer = dbConn.Select<CRM_Survey_Management_Proceeded>("Phone={0} AND CustomerName={1}", customer.Phone, customer.CustomerName);
                    foreach (var question in listQuestion)
                    {
                        var answer = listAnswer.FirstOrDefault(a => a.QuestionID == question.QuestionID);
                        if (answer != null)
                        {
                            newRow["CreatedAt"] = answer.CreatedAt.ToString("HH:mm dd/MM/yyyy");
                            newRow[question.QuestionID] = answer.Answer;
                        }
                        else
                        {
                            newRow[question.QuestionID] = "Chưa trả lời";
                        }

                    }
                    dataResult.Rows.Add(newRow);
                }

                result.Data = dataResult.ToDataSourceResult(new DataSourceRequest()).Data;
                return Json(result);
            }
        }
        public ActionResult ExportData(string ManagementID)
        {
            if (asset.Export)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    //using (ExcelPackage excelPkg = new ExcelPackage())
                    FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\SurveyManagement.xlsx"));
                    var excelPkg = new ExcelPackage(fileInfo);

                    string fileName = "SurveyManagement_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    string query = @"SELECT 
	                    m.Title AS SurveyTitle
	                    ,mp.CustomerID
	                    ,mp.Id AS ProceededID
	                    ,mp.CustomerName
	                    ,mp.Phone
	                    ,mp.PhysicalID
	                    ,mp.QuestionID
	                    ,mp.[Source] AS CustomerSource
	                    ,q.[Description] AS QuestionContent
	                    ,mp.QuestionType
	                    ,mp.AnswerID
	                    ,mp.Answer AS Note
	                    ,mp.CreatedAt AS SurveyTime
	                    ,mp.CreatedBy AS SurveyBy
                    FROM CRM_Survey_Management_Proceeded mp
                    LEFT JOIN CRM_Survey_Management m ON mp.SurveyManagementID = m.Id
                    LEFT JOIN CRM_Survey_Question q ON mp.QuestionID = q.QuestionID
                    WHERE mp.SurveyManagementID = " + ManagementID;
                    var data = dbConn.Select<DC_Survey_ExcelView>(query);
                    ExcelWorksheet expenseSheet = excelPkg.Workbook.Worksheets["Survey"];

                    int rowData = 1;

                    foreach (var item in data)
                    {
                        if (item.QuestionType == "Free text")
                        {
                            int i = 1;
                            rowData++;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyTitle;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                            expenseSheet.Cells[rowData, i++].Value = item.Phone;
                            expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerSource;
                            expenseSheet.Cells[rowData, i++].Value = item.QuestionContent;
                            expenseSheet.Cells[rowData, i++].Value = item.QuestionType;
                            expenseSheet.Cells[rowData, i++].Value = item.Note;
                            expenseSheet.Cells[rowData, i++].Value = item.Note;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyTime;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyBy;
                        }
                        else if (item.QuestionType == "Single choice")
                        {
                            int i = 1;
                            rowData++;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyTitle;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                            expenseSheet.Cells[rowData, i++].Value = item.Phone;
                            expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                            expenseSheet.Cells[rowData, i++].Value = item.CustomerSource;
                            expenseSheet.Cells[rowData, i++].Value = item.QuestionContent;
                            expenseSheet.Cells[rowData, i++].Value = item.QuestionType;
                            expenseSheet.Cells[rowData, i++].Value = item.Note.Split('-')[0];
                            expenseSheet.Cells[rowData, i++].Value = item.Note;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyTime;
                            expenseSheet.Cells[rowData, i++].Value = item.SurveyBy;
                        }
                        else
                        {
                            string[] arrAnswer = item.AnswerID.Split(',');
                            //get list answer of question
                            var listAnswer = dbConn.Select<CRM_Survey_AnswerList>("QuestionID={0}", item.QuestionID);

                            //get list note of answer
                            var listNote = dbConn.Select<CRM_Survey_Management_Proceeded_Answer>("ProceededID={0}", item.ProceededID);
                            for (int j = 0; j < arrAnswer.Length; j++)
                            {
                                int i = 1;
                                rowData++;
                                expenseSheet.Cells[rowData, i++].Value = item.SurveyTitle;
                                expenseSheet.Cells[rowData, i++].Value = item.CustomerID == null ? "" : item.CustomerID;
                                expenseSheet.Cells[rowData, i++].Value = item.CustomerName;
                                expenseSheet.Cells[rowData, i++].Value = item.Phone;
                                expenseSheet.Cells[rowData, i++].Value = item.PhysicalID;
                                expenseSheet.Cells[rowData, i++].Value = item.CustomerSource;
                                expenseSheet.Cells[rowData, i++].Value = item.QuestionContent;
                                expenseSheet.Cells[rowData, i++].Value = item.QuestionType;
                                expenseSheet.Cells[rowData, i++].Value = listAnswer.FirstOrDefault(a => a.AnswerID == arrAnswer[j]).AnswerDescription;
                                expenseSheet.Cells[rowData, i++].Value = listNote.FirstOrDefault(a => a.AnswerID == arrAnswer[j]) == null ? "" : listNote.FirstOrDefault(a => a.AnswerID == arrAnswer[j]).Answer;
                                expenseSheet.Cells[rowData, i++].Value = item.SurveyTime;
                                expenseSheet.Cells[rowData, i++].Value = item.SurveyBy;
                            }
                        }

                    }
                    MemoryStream output = new MemoryStream();
                    excelPkg.SaveAs(output);
                    output.Position = 0;
                    return File(output.ToArray(), contentType, fileName);
                }
            }
            else
            {
                return Json(new { success = false });
            }


        }
        [HttpPost]
        public ActionResult SkipSurvey(CRM_Survey_Management_Skip model)
        {
            if (asset.Update && ModelState.IsValid)
            {
                try
                {
                    using (var dbConn = OrmliteConnection.openConn())
                    {
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