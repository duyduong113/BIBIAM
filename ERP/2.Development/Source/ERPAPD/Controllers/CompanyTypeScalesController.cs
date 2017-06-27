using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using System.Collections;
using System.IO;
using System.Data;
using OfficeOpenXml;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class CompanyTypeScalesController : CustomController
    {
        //
        // GET: /CompanyDefinition/

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                ViewBag.CompanyResult = DC_Company_Result.GetAllDC_Company_Results();
                ViewBag.TypeData = DC_Company_Type.GetAllDC_Company_Types();
                ViewBag.ScaleData = DC_Company_Scale.GetAllDC_Company_Scales();

                ViewBag.StageData = ViewBag.Stage = ViewData["StageData"] = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "");
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult CompanyType_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Company_Type>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Company_Type.GetDC_Company_Types(where, "TypeID DESC");
                }
                else
                {
                    data = DC_Company_Type.GetDC_Company_Types("1=1", "TypeID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult CompanyScales_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Company_Scale>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Company_Scale.GetDC_Company_Scales(where, "ScalesID DESC");
                }
                else
                {
                    data = DC_Company_Scale.GetDC_Company_Scales("1=1", "ScalesID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult DrListCompany([DataSourceRequest] DataSourceRequest request, string companyID, string orgID)
        {
            var data = DC_Company.GetListDC_Companys("CompanyName like '%" + companyID + "%' OR OrganizationID like '%" + orgID + "%'", "CompanyName asc");
            data = data.Take(10).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CompanyResult_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Company_Result>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Company_Result.GetDC_Company_Results(where, "ResultID DESC");
                }
                else
                {
                    data = DC_Company_Result.GetDC_Company_Results("1=1", "ResultID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult StageDefinition_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Stage_Definition>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Stage_Definition.GetDC_Stage_Definitions(where, "StageID DESC");
                }
                else
                {
                    data = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "StageID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult StepDefinition_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Step_Definition>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Step_Definition.GetDC_Step_Definitions(where, "StepID DESC");
                }
                else
                {
                    data = DC_Step_Definition.GetDC_Step_Definitions("1=1", "StepID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Bank_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Bank_Definition>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Bank_Definition.GetDC_Bank_Definitions(where, "BankID DESC");
                }
                else
                {
                    data = DC_Bank_Definition.GetDC_Bank_Definitions("1=1", "BankID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Buyer_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_Buyer_Definition>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_Buyer_Definition.GetDC_Buyer_Definitions(where, "BuyerID DESC");
                }
                else
                {
                    data = DC_Buyer_Definition.GetDC_Buyer_Definitions("1=1", "BuyerID DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [HttpPost]
        public ActionResult SaveCompanyType([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Type> listtype)
        {
            if (asset.Create)
            {
                try
                {
                    if (listtype != null && ModelState.IsValid)
                    {
                        foreach (var typ in listtype)
                        {
                            if (String.IsNullOrEmpty(typ.TypeName))
                            {
                                ModelState.AddModelError("", "Please input Type Name ");
                                return Json(listtype.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Company_Type();
                            var checkID = DC_Company_Type.GetDC_Company_Types("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.TypeID.Substring(2, checkID.TypeID.Length - 2)) + 1;
                                id = "CT" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "CT00001";
                            }
                            var check = DC_Company_Type.GetDC_Company_Types("1=1", "").Where(s => s.TypeName.Trim().ToLower() == typ.TypeName.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Type Name  is exists.");
                                return Json(listtype.ToDataSourceResult(request, ModelState));
                            }
                            write.TypeID = id;
                            write.TypeName = typ.TypeName.Trim();
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listtype.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listtype.ToDataSourceResult(request, ModelState));
            }
            return Json(listtype.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveCompanyScale([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Scale> listscale)
        {
            if (asset.Create)
            {
                try
                {
                    if (listscale != null && ModelState.IsValid)
                    {
                        foreach (var typ in listscale)
                        {
                            if (String.IsNullOrEmpty(typ.ScalesName))
                            {
                                ModelState.AddModelError("", "Please input Scale Name ");
                                return Json(listscale.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Company_Scale();
                            var checkID = DC_Company_Scale.GetDC_Company_Scales("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.ScalesID.Substring(2, checkID.ScalesID.Length - 2)) + 1;
                                id = "SC" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "SC00001";
                            }
                            var check = DC_Company_Scale.GetDC_Company_Scales("1=1", "").Where(s => s.ScalesName.Trim().ToLower() == typ.ScalesName.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Scale Name  is exists.");
                                return Json(listscale.ToDataSourceResult(request, ModelState));
                            }
                            write.ScalesID = id;
                            write.ScalesName = typ.ScalesName.Trim();
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listscale.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listscale.ToDataSourceResult(request, ModelState));
            }
            return Json(listscale.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveCompanyResult([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Result> listResult)
        {
            if (asset.Create)
            {
                try
                {
                    if (listResult != null && ModelState.IsValid)
                    {
                        foreach (var typ in listResult)
                        {
                            if (String.IsNullOrEmpty(typ.ResultName))
                            {
                                ModelState.AddModelError("", "Please input Result Name ");
                                return Json(listResult.ToDataSourceResult(request, ModelState));
                            }
                            var comr = new DC_Company_Result();
                            #region CreateREQ
                            string code = "";
                            string datetimePO = DateTime.Now.ToString("yyMMdd");
                            var existPO = DC_Param.GetDC_Param("0020");
                            if (existPO.ParamValue.Contains(datetimePO))
                            {
                                var nextNo = Int32.Parse(existPO.ParamValue.Substring(10, 5)) + 1;
                                code = "COMR" + datetimePO + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                code = "COMR" + datetimePO + "00001";
                            }
                            var IVNumber = DC_Param.GetDC_Param("0020");
                            IVNumber.ParamValue = code;
                            IVNumber.RowLastUpdatedTime = DateTime.Now;
                            IVNumber.RowLastUpdatedUser = currentUser.UserName;
                            IVNumber.Update();
                            #endregion
                            var check = DC_Company_Result.GetDC_Company_Results("1=1", "").Where(s => s.ResultName.Trim().ToLower() == typ.ResultName.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Result Name  is exists.");
                                return Json(listResult.ToDataSourceResult(request, ModelState));
                            }
                            comr.ResultID = code;
                            comr.ResultName = typ.ResultName.Trim();
                            comr.RowCreatedTime = DateTime.Now;
                            comr.RowCreatedUser = currentUser.UserName;
                            comr.Recommand = typ.Recommand;
                            comr.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listResult.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listResult.ToDataSourceResult(request, ModelState));
            }
            return Json(listResult.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveStageDefinition([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Stage_Definition> liststage)
        {
            if (asset.Create)
            {
                try
                {
                    if (liststage != null && ModelState.IsValid)
                    {
                        foreach (var sta in liststage)
                        {
                            if (String.IsNullOrEmpty(sta.Description))
                            {
                                ModelState.AddModelError("", "Please input Stage ");
                                return Json(liststage.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var stage = new DC_Stage_Definition();
                            var checkID = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.StageID.Substring(3, checkID.StageID.Length - 3)) + 1;
                                id = "STA" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "STA00001";
                            }
                            var check = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "").Where(s => s.Description.Trim().ToLower() == sta.Description.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Stage  is exists.");
                                return Json(liststage.ToDataSourceResult(request, ModelState));
                            }
                            stage.StageID = id;
                            stage.Description = sta.Description.Trim();
                            stage.RowCreatedTime = DateTime.Now;
                            stage.RowCreatedUser = currentUser.UserName;
                            stage.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(liststage.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(liststage.ToDataSourceResult(request, ModelState));
            }
            return Json(liststage.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveStepDefinition([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Step_Definition> listStep)
        {
            if (asset.Create)
            {
                try
                {
                    if (listStep != null && ModelState.IsValid)
                    {
                        foreach (var sta in listStep)
                        {
                            if (String.IsNullOrEmpty(sta.Description))
                            {
                                ModelState.AddModelError("", "Please input Step ");
                                return Json(listStep.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var Step = new DC_Step_Definition();
                            var checkID = DC_Step_Definition.GetDC_Step_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.StepID.Substring(3, checkID.StepID.Length - 3)) + 1;
                                id = "STE" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "STE00001";
                            }
                            var check = DC_Step_Definition.GetDC_Step_Definitions("1=1", "").Where(s => s.Description.Trim().ToLower() == sta.Description.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Step  is exists.");
                                return Json(listStep.ToDataSourceResult(request, ModelState));
                            }
                            Step.StepID = id;
                            Step.Description = sta.Description.Trim();
                            Step.StageID = sta.StageID;
                            Step.RowCreatedTime = DateTime.Now;
                            Step.RowCreatedUser = currentUser.UserName;
                            Step.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listStep.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listStep.ToDataSourceResult(request, ModelState));
            }
            return Json(listStep.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveBank([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Bank_Definition> listBank)
        {
            if (asset.Create)
            {
                try
                {
                    if (listBank != null && ModelState.IsValid)
                    {
                        foreach (var typ in listBank)
                        {
                            if (String.IsNullOrEmpty(typ.BankName))
                            {
                                ModelState.AddModelError("", "Please input Bank Name ");
                                return Json(listBank.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Bank_Definition();
                            var checkID = DC_Bank_Definition.GetDC_Bank_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.BankID.Substring(2, checkID.BankID.Length - 2)) + 1;
                                id = "BA" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "BA00001";
                            }
                            var check = DC_Bank_Definition.GetDC_Bank_Definitions("1=1", "").Where(s => s.BankName.Trim().ToLower() == typ.BankName.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Bank Name  is exists.");
                                return Json(listBank.ToDataSourceResult(request, ModelState));
                            }
                            write.BankID = id;
                            write.BankName = typ.BankName.Trim();
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listBank.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listBank.ToDataSourceResult(request, ModelState));
            }
            return Json(listBank.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult SaveBuyer([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Buyer_Definition> listBuyer)
        {
            if (asset.Create)
            {
                try
                {
                    if (listBuyer != null && ModelState.IsValid)
                    {
                        foreach (var typ in listBuyer)
                        {
                            if (String.IsNullOrEmpty(typ.BuyerName))
                            {
                                ModelState.AddModelError("", "Please input Buyer Name ");
                                return Json(listBuyer.ToDataSourceResult(request, ModelState));
                            }
                            string id = "";
                            var write = new DC_Buyer_Definition();
                            var checkID = DC_Buyer_Definition.GetDC_Buyer_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.BuyerID.Substring(2, checkID.BuyerID.Length - 2)) + 1;
                                id = "BE" + String.Format("{0:00000}", nextNo);
                            }
                            else
                            {
                                id = "BE00001";
                            }
                            var check = DC_Buyer_Definition.GetDC_Buyer_Definitions("1=1", "").Where(s => s.BuyerName.Trim().ToLower() == typ.BuyerName.Trim().ToLower()).FirstOrDefault();
                            if (check != null)
                            {
                                ModelState.AddModelError("", " Buyer Name  is exists.");
                                return Json(listBuyer.ToDataSourceResult(request, ModelState));
                            }
                            write.BuyerID = id;
                            write.BuyerName = typ.BuyerName.Trim();
                            write.RowCreatedTime = DateTime.Now;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Save();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listBuyer.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listBuyer.ToDataSourceResult(request, ModelState));
            }
            return Json(listBuyer.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateCompanyType([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Type> listtype)
        {
            if (asset.Update)
            {
                try
                {
                    if (listtype != null && ModelState.IsValid)
                    {
                        foreach (var regis in listtype)
                        {
                            if (String.IsNullOrEmpty(regis.TypeName))
                            {
                                ModelState.AddModelError("", "Please input Type Name ");
                                return Json(listtype.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Company_Type();
                            var check = DC_Company_Type.GetDC_Company_Types("1=1", "").Where(s => s.TypeName == regis.TypeName);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Type Name is exists.");
                                return Json(listtype.ToDataSourceResult(request, ModelState));
                            }
                            write.TypeID = regis.TypeID;
                            write.TypeName = regis.TypeName.Trim();
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listtype.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listtype.ToDataSourceResult(request, ModelState));
            }
            return Json(listtype.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateCompanyScale([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Scale> listscale)
        {
            if (asset.Update)
            {
                try
                {
                    if (listscale != null && ModelState.IsValid)
                    {
                        foreach (var regis in listscale)
                        {
                            if (String.IsNullOrEmpty(regis.ScalesName))
                            {
                                ModelState.AddModelError("", "Please input Scale Name ");
                                return Json(listscale.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Company_Scale();
                            var check = DC_Company_Scale.GetDC_Company_Scales("1=1", "").Where(s => s.ScalesName == regis.ScalesName);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Scale Name is exists.");
                                return Json(listscale.ToDataSourceResult(request, ModelState));
                            }
                            write.ScalesID = regis.ScalesID;
                            write.ScalesName = regis.ScalesName.Trim();
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listscale.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listscale.ToDataSourceResult(request, ModelState));
            }
            return Json(listscale.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult UpdateCompanyResult([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Company_Result> listResult)
        {
            if (asset.Update)
            {
                try
                {
                    if (listResult != null && ModelState.IsValid)
                    {
                        foreach (var regis in listResult)
                        {
                            if (String.IsNullOrEmpty(regis.ResultName))
                            {
                                ModelState.AddModelError("", "Please input Result Name ");
                                return Json(listResult.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Company_Result();
                            //var check = DC_Company_Result.GetDC_Company_Results("ResultName ='" + regis.ResultName + "'", "");
                            //if (check.Count() > 0)
                            //{
                            //    ModelState.AddModelError("", " Result Name is exists.");
                            //    return Json(listResult.ToDataSourceResult(request, ModelState));
                            //}
                            write.ResultID = regis.ResultID;
                            write.ResultName = regis.ResultName.Trim();
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Recommand = regis.Recommand;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listResult.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listResult.ToDataSourceResult(request, ModelState));
            }
            return Json(listResult.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateStageDefinition([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Stage_Definition> liststage)
        {
            if (asset.Update)
            {
                try
                {
                    if (liststage != null && ModelState.IsValid)
                    {
                        foreach (var sta in liststage)
                        {
                            if (String.IsNullOrEmpty(sta.Description))
                            {
                                ModelState.AddModelError("", "Please input Stage ");
                                return Json(liststage.ToDataSourceResult(request, ModelState));
                            }
                            var stage = new DC_Stage_Definition();
                            var check = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "").Where(s => s.Description == sta.Description);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Stage is exists.");
                                return Json(liststage.ToDataSourceResult(request, ModelState));
                            }
                            stage.StageID = sta.StageID;
                            stage.Description = sta.Description.Trim();
                            stage.Active = sta.Active != null ? Boolean.Parse(sta.Active.ToString()) : false;
                            stage.RowLastUpdatedTime = DateTime.Now;
                            stage.RowLastUpdatedUser = currentUser.UserName;
                            stage.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(liststage.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(liststage.ToDataSourceResult(request, ModelState));
            }
            return Json(liststage.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateStepDefinition([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Step_Definition> listStep)
        {
            if (asset.Update)
            {
                try
                {
                    if (listStep != null && ModelState.IsValid)
                    {
                        foreach (var sta in listStep)
                        {
                            if (String.IsNullOrEmpty(sta.Description))
                            {
                                ModelState.AddModelError("", "Please input Step ");
                                return Json(listStep.ToDataSourceResult(request, ModelState));
                            }
                            var Step = new DC_Step_Definition();
                            var check = DC_Step_Definition.GetDC_Step_Definitions("1=1", "").Where(s => s.Description == sta.Description && s.StageID == sta.StageID);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Step is exists.");
                                return Json(listStep.ToDataSourceResult(request, ModelState));
                            }
                            Step.StepID = sta.StepID;
                            Step.Description = sta.Description.Trim();
                            Step.StageID = sta.StageID;
                            Step.Active = sta.Active != null ? Boolean.Parse(sta.Active.ToString()) : false;
                            Step.RowLastUpdatedTime = DateTime.Now;
                            Step.RowLastUpdatedUser = currentUser.UserName;
                            Step.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listStep.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listStep.ToDataSourceResult(request, ModelState));
            }
            return Json(listStep.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateBank([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Bank_Definition> listBank)
        {
            if (asset.Update)
            {
                try
                {
                    if (listBank != null && ModelState.IsValid)
                    {
                        foreach (var regis in listBank)
                        {
                            if (String.IsNullOrEmpty(regis.BankName))
                            {
                                ModelState.AddModelError("", "Please input Bank Name ");
                                return Json(listBank.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Bank_Definition();
                            var check = DC_Bank_Definition.GetDC_Bank_Definitions("1=1", "").Where(s => s.BankName == regis.BankName);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Bank Name is exists.");
                                return Json(listBank.ToDataSourceResult(request, ModelState));
                            }
                            write.BankID = regis.BankID;
                            write.BankName = regis.BankName.Trim();
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listBank.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listBank.ToDataSourceResult(request, ModelState));
            }
            return Json(listBank.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateBuyer([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Buyer_Definition> listBuyer)
        {
            if (asset.Update)
            {
                try
                {
                    if (listBuyer != null && ModelState.IsValid)
                    {
                        foreach (var regis in listBuyer)
                        {
                            if (String.IsNullOrEmpty(regis.BuyerName))
                            {
                                ModelState.AddModelError("", "Please input Buyer Name ");
                                return Json(listBuyer.ToDataSourceResult(request, ModelState));
                            }
                            var write = new DC_Buyer_Definition();
                            var check = DC_Buyer_Definition.GetDC_Buyer_Definitions("1=1", "").Where(s => s.BuyerName == regis.BuyerName);
                            if (check.Count() > 0)
                            {
                                ModelState.AddModelError("", " Buyer Name is exists.");
                                return Json(listBuyer.ToDataSourceResult(request, ModelState));
                            }
                            write.BuyerID = regis.BuyerID;
                            write.BuyerName = regis.BuyerName.Trim();
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listBuyer.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(listBuyer.ToDataSourceResult(request, ModelState));
            }
            return Json(listBuyer.ToDataSourceResult(request, ModelState));

        }

        public ActionResult DeleteType(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        // check [Type] in Company:
                        //var cType = DC_Company.GetDC_Companys("[CompanyTypeID] = '" + item + "'", "").FirstOrDefault();
                        //if (cType == null)
                        //{
                        var check = new DC_Company_Type();
                        check.TypeID = item;
                        check.Delete();
                        success++;
                        //}
                        //else
                        //{
                        //    error++;
                        //}


                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteScale(string data)
        {
            if (asset.Delete)
            {
                int success = 0;
                int error = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        // check [Type] in Company:
                        //var cType = DC_Company.GetDC_Companys("[ScalesID] = '" + item + "'", "").FirstOrDefault();
                        //if (cType == null)
                        //{
                            var check = new DC_Company_Scale();
                            check.ScalesID = item;
                            check.Delete();
                            success++;
                        //}
                        //else
                        //{
                        //    error++;
                        //}

                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteResult(string data)
        {
            if (asset.Delete)
            {
                int success = 0;
                int error = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // check [Type] in Company:
                        // var aResult //= DC_Task_Assignment.GetDC_Task_Assignments("[ResultID] = '" + item + "'", "").FirstOrDefault();
                        //if (aResult == null)
                        //{
                        var check = new DC_Company_Result();
                        check.ResultID = item;
                        check.Delete();
                        success++;
                        //}
                        //else
                        //{
                        //    error++;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteStageDefinition(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        //check [Stage] in Step:
                        var ste = DC_Step_Definition.GetDC_Step_Definitions("[StageID] = '" + item + "'", "").FirstOrDefault();
                        if (ste == null)
                        {
                            var check = new DC_Stage_Definition();
                            check.StageID = item;
                            check.Delete();
                            success++;
                        }
                        else
                        {
                            error++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteStepDefinition(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        ////check [Stage] in Step:
                        var ste = DC_CheckList_Definition_Step.GetDC_CheckList_Definition_Steps("1=1", "").Where(s => s.StepID == item).FirstOrDefault();
                        if (ste == null)
                        {
                            var check = new DC_Step_Definition();
                            check.StepID = item;
                            check.Delete();
                            success++;
                        }
                        else
                        {
                            error++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteBank(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        //// check [Bank] in Company:
                        //var cBank = DC_Company.GetDC_Companys("[CompanyBankID] = '" + item + "'", "").FirstOrDefault();
                        //if (cBank == null)
                        //{
                        var check = new DC_Bank_Definition();
                        check.BankID = item;
                        check.Delete();
                        success++;
                        //}
                        //else
                        //{
                        //    error++;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult DeleteBuyer(string data)
        {
            if (asset.Delete)
            {
                int error = 0;
                int success = 0;
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {

                        //// check [Buyer] in Company:
                        //var cBuyer = DC_Company.GetDC_Companys("[CompanyBuyerID] = '" + item + "'", "").FirstOrDefault();
                        //if (cBuyer == null)
                        //{
                        var check = new DC_Buyer_Definition();
                        check.BuyerID = item;
                        check.Delete();
                        success++;
                        //}
                        //else
                        //{
                        //    error++;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true, totalSuccess = success, totalError = error });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public FileResult Export_Type([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {

                var rdata = new List<DC_Company_Type>();

                rdata = DC_Company_Type.GetAllDC_Company_Types();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Type.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Company Type"];

                int rowData = 1;
                foreach (DC_Company_Type data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.TypeID;
                    dataSheet.Cells[rowData, i++].Value = data.TypeName;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Company_Type_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Company_Type_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }

        public FileResult Export_Scale([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {

                var rdata = new List<DC_Company_Scale>();

                rdata = DC_Company_Scale.GetAllDC_Company_Scales();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Scale.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Company Scale"];

                int rowData = 1;
                foreach (DC_Company_Scale data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.ScalesID;
                    dataSheet.Cells[rowData, i++].Value = data.ScalesName;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Company_Scale_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentScale = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentScale, fileName);
            }
            else
            {
                string fileName = "DC_Company_Scale_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentScale = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentScale, fileName);
            }
        }

        public FileResult Export_Result([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {

                var rdata = new List<DC_Company_Result>();

                rdata = DC_Company_Result.GetAllDC_Company_Results();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Result.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Company Result"];

                int rowData = 1;
                foreach (DC_Company_Result data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.ResultID;
                    dataSheet.Cells[rowData, i++].Value = data.ResultName;
                    dataSheet.Cells[rowData, i++].Value = data.Recommand;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Company_Result_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentResult = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentResult, fileName);
            }
            else
            {
                string fileName = "DC_Company_Result_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentResult = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentResult, fileName);
            }
        }

        public FileResult Export_StageDefinition([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var rdata = new List<DC_Stage_Definition>();

                rdata = DC_Stage_Definition.GetAllDC_Stage_Definitions();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Stage_Definition.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Stage"];

                int rowData = 1;
                foreach (DC_Stage_Definition data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.StageID;
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Stage_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Stage_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }

        public FileResult Export_StepDefinition([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var rdata = new List<DC_Step_Definition>();

                rdata = DC_Step_Definition.GetAllDC_Step_Definitions();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Step_Definition.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Step"];

                int rowData = 1;
                foreach (DC_Step_Definition data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.StepID;
                    dataSheet.Cells[rowData, i++].Value = data.Description;
                    //Result
                    if (string.IsNullOrEmpty(data.StageID))
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    else
                    {
                        var datatemp = DC_Stage_Definition.GetDC_Stage_Definitions("[StageID]='" + data.StageID + "'", "").FirstOrDefault();
                        if (string.IsNullOrEmpty(datatemp.StageID))
                            dataSheet.Cells[rowData, i++].Value = "";
                        else
                            dataSheet.Cells[rowData, i++].Value = datatemp.Description;
                    }
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }
                //Stage
                var sheet1 = excelPkg.Workbook.Worksheets["Stage"];
                int rowData1 = 2;
                var listStage = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "");
                foreach (var list in listStage)
                {
                    int i = 1;
                    rowData1++;
                    sheet1.Cells[rowData1, i++].Value = list.Description.ToString();
                }
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Step_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_Step_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }

        public FileResult Export_Bank([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {

                var rdata = new List<DC_Bank_Definition>();

                rdata = DC_Bank_Definition.GetAllDC_Bank_Definitions();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Bank_Definition.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Bank"];

                int rowData = 1;
                foreach (DC_Bank_Definition data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.BankID;
                    dataSheet.Cells[rowData, i++].Value = data.BankName;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Bank_Definition" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentBank = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentBank, fileName);
            }
            else
            {
                string fileName = "DC_Bank_Definition" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentBank = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentBank, fileName);
            }
        }

        public FileResult Export_Buyer([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {

                var rdata = new List<DC_Buyer_Definition>();

                rdata = DC_Buyer_Definition.GetAllDC_Buyer_Definitions();
                IEnumerable datas = rdata.ToDataSourceResult(request).Data;

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Buyer_Definition.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);

                //data sheet
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Buyer"];

                int rowData = 1;
                foreach (DC_Buyer_Definition data in datas)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.BuyerID;
                    dataSheet.Cells[rowData, i++].Value = data.BuyerName;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_Buyer_Definition" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentBuyer = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentBuyer, fileName);
            }
            else
            {
                string fileName = "DC_Buyer_Definition" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentBuyer = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentBuyer, fileName);
            }
        }

        public ActionResult ImportFromExcel_Type()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Type.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Company Type"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Company Type"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string TypeID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string TypeName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(TypeName))
                                {
                                    var com_type = new DC_Company_Type();
                                    var checkAliasNameExist = DC_Company_Type.GetDC_Company_Types("1=1", "").Where(s => s.TypeName.Trim().ToLower() == TypeName.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        com_type.TypeID = TypeID;
                                        com_type.TypeName = TypeName != null ? TypeName : "";
                                        com_type.RowLastUpdatedTime = DateTime.Now;
                                        com_type.RowLastUpdatedUser = currentUser.UserName;
                                        com_type.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Company_Type.GetAllDC_Company_Types().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.TypeID.Substring(2, dataid.TypeID.Length - 2)) + 1;
                                            id = "CT" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "CT00001";
                                        }
                                        com_type.TypeID = id;
                                        com_type.TypeName = TypeName;
                                        com_type.RowCreatedTime = DateTime.Now;
                                        com_type.RowCreatedUser = currentUser.UserName;
                                        com_type.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = TypeID;
                                eSheet.Cells[rownumber, 2].Value = TypeName;
                                eSheet.Cells[rownumber, 3].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 5].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 7].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_Scale()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Scale.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Company Scale"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Company Scale"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string ScaleID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string ScaleName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(ScaleName))
                                {
                                    var com_scale = new DC_Company_Scale();
                                    var checkAliasNameExist = DC_Company_Scale.GetDC_Company_Scales("1=1", "").Where(s => s.ScalesName.Trim().ToLower() == ScaleName.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        com_scale.ScalesID = ScaleID;
                                        com_scale.ScalesName = ScaleName != null ? ScaleName : "";
                                        com_scale.RowLastUpdatedTime = DateTime.Now;
                                        com_scale.RowLastUpdatedUser = currentUser.UserName;
                                        com_scale.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Company_Scale.GetAllDC_Company_Scales().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.ScalesID.Substring(2, dataid.ScalesID.Length - 2)) + 1;
                                            id = "SC" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "SC00001";
                                        }
                                        com_scale.ScalesID = id;
                                        com_scale.ScalesName = ScaleName;
                                        com_scale.RowCreatedTime = DateTime.Now;
                                        com_scale.RowCreatedUser = currentUser.UserName;
                                        com_scale.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = ScaleID;
                                eSheet.Cells[rownumber, 2].Value = ScaleName;
                                eSheet.Cells[rownumber, 3].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 5].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 7].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_Result()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Company_Result.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Company Result"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Company Result"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string ResultID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string ResultName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string Recommand = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(ResultName))
                                {
                                    var com_Result = new DC_Company_Result();
                                    var checkAliasNameExist = DC_Company_Result.GetDC_Company_Results("1=1", "").Where(s => s.ResultName.Trim().ToLower() == ResultName.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        com_Result.ResultID = ResultID;
                                        com_Result.ResultName = ResultName != null ? ResultName : "";
                                        com_Result.Recommand = Recommand != null ? Recommand : "";
                                        com_Result.RowLastUpdatedTime = DateTime.Now;
                                        com_Result.RowLastUpdatedUser = currentUser.UserName;
                                        com_Result.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        #region CreateREQ
                                        string code = "";
                                        string datetimePO = DateTime.Now.ToString("yyMMdd");
                                        var existPO = DC_Param.GetDC_Param("0019");
                                        if (existPO.ParamValue.Contains(datetimePO))
                                        {
                                            var nextNo = Int32.Parse(existPO.ParamValue.Substring(10, 5)) + 1;
                                            code = "COMR" + datetimePO + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            code = "COMR" + datetimePO + "00001";
                                        }
                                        var IVNumber = DC_Param.GetDC_Param("0019");
                                        IVNumber.ParamValue = code;
                                        IVNumber.RowLastUpdatedTime = DateTime.Now;
                                        IVNumber.RowLastUpdatedUser = currentUser.UserName;
                                        IVNumber.Update();
                                        #endregion
                                        com_Result.ResultID = code;
                                        com_Result.ResultName = ResultName;
                                        com_Result.Recommand = Recommand != null ? Recommand : "";
                                        com_Result.RowCreatedTime = DateTime.Now;
                                        com_Result.RowCreatedUser = currentUser.UserName;
                                        com_Result.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = ResultID;
                                eSheet.Cells[rownumber, 2].Value = ResultName;
                                eSheet.Cells[rownumber, 3].Value = Recommand;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 5].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 7].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 8].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_StageDefinition()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Stage_Definition.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Stage"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Stage"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string StageID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string Description = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string Active = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(Description))
                                {
                                    var stage = new DC_Stage_Definition();
                                    var checkAliasNameExist = DC_Stage_Definition.GetDC_Stage_Definitions("1=1", "").Where(s => s.Description.Trim().ToLower() == Description.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        stage.StageID = StageID;
                                        stage.Description = Description != null ? Description : "";
                                        stage.Active = Convert.ToBoolean(Active != null ? Active : "1");
                                        stage.RowLastUpdatedTime = DateTime.Now;
                                        stage.RowLastUpdatedUser = currentUser.UserName;
                                        stage.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Stage_Definition.GetAllDC_Stage_Definitions().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.StageID.Substring(3, dataid.StageID.Length - 3)) + 1;
                                            id = "STA" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "STA00001";
                                        }
                                        stage.StageID = id;
                                        stage.Description = Description;
                                        stage.RowCreatedTime = DateTime.Now;
                                        stage.RowCreatedUser = currentUser.UserName;
                                        stage.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = StageID;
                                eSheet.Cells[rownumber, 2].Value = Description;
                                eSheet.Cells[rownumber, 3].Value = Active;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 5].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 7].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 8].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_StepDefinition()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Step_Definition.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Step"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Step"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string StepID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string Description = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string StageDescription = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string Active = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 8].Value != null ? oSheet.Cells[i, 8].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(Description))
                                {
                                    var Step = new DC_Step_Definition();
                                    var checkAliasNameExist = DC_Step_Definition.GetDC_Step_Definitions("1=1", "").Where(s => s.Description.Trim().ToLower() == Description.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        Step.StepID = StepID;
                                        Step.Description = Description != null ? Description : "";
                                        //
                                        if (string.IsNullOrEmpty(StageDescription))
                                        {
                                            Step.StageID = "";
                                        }
                                        else
                                        {
                                            var datatemp = DC_Stage_Definition.GetDC_Stage_Definitions("[Description]='" + StageDescription + "'", "").FirstOrDefault();
                                            if (string.IsNullOrEmpty(datatemp.StageID))
                                                Step.StageID = "";
                                            else
                                                Step.StageID = datatemp.StageID;
                                        }
                                        Step.Active = Active != null ? Boolean.Parse(Active) : false;
                                        Step.RowLastUpdatedTime = DateTime.Now;
                                        Step.RowLastUpdatedUser = currentUser.UserName;
                                        Step.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Step_Definition.GetAllDC_Step_Definitions().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.StepID.Substring(3, dataid.StepID.Length - 3)) + 1;
                                            id = "STE" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "STE00001";
                                        }
                                        Step.StepID = id;
                                        Step.Description = Description;
                                        //
                                        if (string.IsNullOrEmpty(StageDescription))
                                        {
                                            Step.StageID = "";
                                        }
                                        else
                                        {
                                            var datatemp = DC_Stage_Definition.GetDC_Stage_Definitions("[Description]='" + StageDescription + "'", "").FirstOrDefault();
                                            if (string.IsNullOrEmpty(datatemp.StageID))
                                                Step.StageID = "";
                                            else
                                                Step.StageID = datatemp.StageID;
                                        }
                                        Step.RowCreatedTime = DateTime.Now;
                                        Step.RowCreatedUser = currentUser.UserName;
                                        Step.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = StepID;
                                eSheet.Cells[rownumber, 2].Value = Description;
                                eSheet.Cells[rownumber, 3].Value = StageDescription;
                                eSheet.Cells[rownumber, 4].Value = Active;
                                eSheet.Cells[rownumber, 5].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 6].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 7].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 8].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 9].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_Bank()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Bank_Definition.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Bank"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Bank"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string BankID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string BankName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string Active = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(BankName))
                                {
                                    var Bank = new DC_Bank_Definition();
                                    var checkAliasNameExist = DC_Bank_Definition.GetDC_Bank_Definitions("1=1", "").Where(s => s.BankName.Trim().ToLower() == BankName.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        Bank.BankID = BankID;
                                        Bank.BankName = BankName != null ? BankName : "";
                                        Bank.Active = Convert.ToBoolean(Active != null ? Active : "1");
                                        Bank.RowLastUpdatedTime = DateTime.Now;
                                        Bank.RowLastUpdatedUser = currentUser.UserName;
                                        Bank.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Bank_Definition.GetAllDC_Bank_Definitions().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.BankID.Substring(2, dataid.BankID.Length - 2)) + 1;
                                            id = "BA" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "BA00001";
                                        }
                                        Bank.BankID = id;
                                        Bank.BankName = BankName;
                                        Bank.RowCreatedTime = DateTime.Now;
                                        Bank.RowCreatedUser = currentUser.UserName;
                                        Bank.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = BankID;
                                eSheet.Cells[rownumber, 2].Value = BankName;
                                eSheet.Cells[rownumber, 3].Value = Active;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 5].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 7].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 8].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        public ActionResult ImportFromExcel_Buyer()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        //Request.Files["fileUpload"].SaveAs(errorFileLocation);

                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_Buyer_Definition.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["Buyer"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["Buyer"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string BuyerID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string BuyerName = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string Active = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "";
                            string RowCreatedTime = oSheet.Cells[i, 4].Value != null ? oSheet.Cells[i, 4].Value.ToString() : "";
                            string RowCreatedUser = oSheet.Cells[i, 5].Value != null ? oSheet.Cells[i, 5].Value.ToString() : "";
                            string RowLastUpdatedTime = oSheet.Cells[i, 6].Value != null ? oSheet.Cells[i, 6].Value.ToString() : "";
                            string RowLastUpdatedUser = oSheet.Cells[i, 7].Value != null ? oSheet.Cells[i, 7].Value.ToString() : "";
                            try
                            {
                                if (!String.IsNullOrEmpty(BuyerName))
                                {
                                    var Buyer = new DC_Buyer_Definition();
                                    var checkAliasNameExist = DC_Buyer_Definition.GetDC_Buyer_Definitions("1=1", "").Where(s => s.BuyerName.Trim().ToLower() == BuyerName.Trim().ToLower());
                                    if (checkAliasNameExist.Count() > 0)
                                    {
                                        Buyer.BuyerID = BuyerID;
                                        Buyer.BuyerName = BuyerName != null ? BuyerName : "";
                                        Buyer.Active = Convert.ToBoolean(Active != null ? Active : "1");
                                        Buyer.RowLastUpdatedTime = DateTime.Now;
                                        Buyer.RowLastUpdatedUser = currentUser.UserName;
                                        Buyer.Update();
                                        total++;
                                    }
                                    else
                                    {
                                        string id = "";
                                        var dataid = DC_Buyer_Definition.GetAllDC_Buyer_Definitions().OrderByDescending(s => s.RowID).FirstOrDefault();
                                        if (dataid != null)
                                        {
                                            var nextNo = Int32.Parse(dataid.BuyerID.Substring(2, dataid.BuyerID.Length - 2)) + 1;
                                            id = "BE" + String.Format("{0:00000}", nextNo);
                                        }
                                        else
                                        {
                                            id = "BE00001";
                                        }
                                        Buyer.BuyerID = id;
                                        Buyer.BuyerName = BuyerName;
                                        Buyer.RowCreatedTime = DateTime.Now;
                                        Buyer.RowCreatedUser = currentUser.UserName;
                                        Buyer.Save();
                                        total++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = BuyerID;
                                eSheet.Cells[rownumber, 2].Value = BuyerName;
                                eSheet.Cells[rownumber, 3].Value = Active;
                                eSheet.Cells[rownumber, 4].Value = RowCreatedTime;
                                eSheet.Cells[rownumber, 5].Value = RowCreatedUser;
                                eSheet.Cells[rownumber, 6].Value = RowLastUpdatedTime;
                                eSheet.Cells[rownumber, 7].Value = RowLastUpdatedUser;
                                eSheet.Cells[rownumber, 8].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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

        //public ActionResult GetContactType(string Contacttype)
        //{
        //    if (Contacttype == "Lead")
        //    {
        //        var data = DC_Lead.GetAllDC_LeadsByInfo();
        //        return Json(new { data = data });
        //    }
        //    else if (Contacttype == "Company")
        //    {
        //        var data = DC_Company.GetAllDC_CompanysByInfo();
        //        return Json(new { data = data });
        //    }
        //    else
        //    {
        //        var data = DC_Company_Contact_List.GetAllDC_Company_Contact_ListsByInfo();
        //        return Json(new { data = data });
        //    }
        //}

        public ActionResult ChangeStatusActive(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Company_Type();
                        com.TypeID = item;
                        var currentStatus = DC_Company_Type.GetDC_Company_Type(item);
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Scale(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Company_Scale();
                        com.ScalesID = item;
                        var currentStatus = DC_Company_Scale.GetDC_Company_Scale(item);
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Result(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Company_Result();
                        com.ResultID = item;
                        var currentStatus = DC_Company_Result.GetDC_Company_Result(item);
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Stage(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Stage_Definition();
                        var currentStatus = DC_Stage_Definition.GetDC_Stage_Definition(item);
                        com.StageID = item;
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Bank(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Bank_Definition();
                        var currentStatus = DC_Bank_Definition.GetDC_Bank_Definition(item);
                        com.BankID = item;
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Buyer(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Buyer_Definition();
                        var currentStatus = DC_Buyer_Definition.GetDC_Buyer_Definition(item);
                        com.BuyerID = item;
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult ChangeStatusActive_Step(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        // get current status: 
                        var com = new DC_Step_Definition();
                        var currentStatus = DC_Step_Definition.GetDC_Step_Definition(item);
                        com.StepID = item;
                        com.Active = currentStatus.Active == true ? false : true;
                        com.RowLastUpdatedTime = DateTime.Now;
                        com.RowLastUpdatedUser = currentUser.UserName;
                        com.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult CheckListChangeActive(string data)
        {
            if (asset.Update)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var update = new DC_CheckList_Definition();
                        var currentStatus = DC_CheckList_Definition.GetDC_CheckList_Definitions("ChecklistID = '" + item + "'", "").FirstOrDefault();
                        update.ChecklistID = item;
                        update.Active = currentStatus.Active == true ? false : true;
                        update.RowLastUpdatedTime = DateTime.Now;
                        update.RowLastUpdatedUser = currentUser.UserName;
                        update.ChangeStatusActive();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to update record" });
            }
        }

        public ActionResult CheckList_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_CheckList_Definition> listEx)
        {
            if (asset.Create)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.Name))
                            {
                                ModelState.AddModelError("", "Please input Name. ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }

                            //Check isdefault
                            string StrCheckList = "";
                            if (regis.IsDefault == true)
                            {
                                using (var dbConn = Helpers.OrmliteConnection.openConn())
                                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                {
                                    //Update old checklist with isdefault = false
                                    var cl = dbConn.Select<DC_CheckList_Definition>("SELECT * FROM dbo.DC_CheckList_Definition Where IsDefault = 'true'").FirstOrDefault();
                                    if (cl != null)
                                    {
                                        cl.IsDefault = false;
                                        cl.RowLastUpdatedTime = DateTime.Now;
                                        cl.RowLastUpdatedUser = currentUser.UserName;
                                        StrCheckList = cl.ChecklistID;
                                        cl.Update();
                                    }
                                }
                            }

                            //Add new check list with isdefault = true
                            string id = "";
                            var write = new DC_CheckList_Definition();
                            var checkID = DC_CheckList_Definition.GetDC_CheckList_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                            if (checkID != null)
                            {
                                var nextNo = Int32.Parse(checkID.ChecklistID.Substring(3, checkID.ChecklistID.Length - 3)) + 1;
                                id = "CHE" + String.Format("{0:000}", nextNo);
                            }
                            else
                            {
                                id = "CHE001";
                            }
                            write.ChecklistID = id;
                            write.Name = regis.Name.Trim();
                            write.Active = regis.Active;
                            write.RowCreatedTime = DateTime.Now;
                            write.IsDefault = regis.IsDefault;
                            write.RowCreatedUser = currentUser.UserName;
                            write.Save();
                            //end

                            if (regis.IsDefault == true)
                            {
                                using (var dbConn = Helpers.OrmliteConnection.openConn())
                                using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                {
                                    //Check Company not in DC_CheckList_Definition_Org -> Apply all Company for this checklist (isdefault = true)
                                    var og = dbConn.Select<DC_Company>("SELECT C.[OrganizationID],C.CompanyID FROM [dbo].DC_Company C LEFT JOIN dbo.DC_CheckList_Definition_Org DO ON DO.CompanyID = C.CompanyID WHERE DO.CompanyID is null").ToList();
                                    var cdo = new DC_CheckList_ApplyFor();
                                    foreach (var itemOg in og)
                                    {
                                        string ido = "STE001";
                                        var checkid = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Orgs("1=1", "RowID DESC").FirstOrDefault();
                                        if (checkid != null)
                                        {
                                            var nextNo = Int32.Parse(checkid.ChecklistSubID.Substring(3, checkid.ChecklistSubID.Length - 3)) + 1;
                                            ido = "STE" + String.Format("{0:000}", nextNo);
                                        }
                                        cdo.ChecklistSubID = ido;
                                        cdo.ChecklistID = id;
                                        cdo.OrganizationID = itemOg.OrganizationID;
                                        cdo.CompanyID = itemOg.CompanyID;
                                        cdo.RowCreatedUser = currentUser.UserName;
                                        cdo.RowCreatedTime = DateTime.Now;
                                        cdo.Save();
                                    }
                                }
                            }

                            //Update CheckList 
                            if (StrCheckList != "")
                            {
                                var clo = new DC_CheckList_ApplyFor();
                                clo.ChecklistID = StrCheckList;
                                clo.ChecklistIDDefault = id;
                                clo.RowLastUpdatedTime = DateTime.Now;
                                clo.RowLastUpdatedUser = currentUser.UserName;
                                clo.UpdateIsDefault();
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listEx.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CheckList_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new List<DC_CheckList_Definition>();
                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    data = DC_CheckList_Definition.GetDC_CheckList_Definitions(where, "RowCreatedTime DESC");
                }
                else
                {
                    data = DC_CheckList_Definition.GetDC_CheckList_Definitions("1=1", "RowCreatedTime DESC");
                }
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "You don't have permission read.");
            }
        }

        public ActionResult CheckList_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_CheckList_Definition> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            if (String.IsNullOrEmpty(regis.Name))
                            {
                                ModelState.AddModelError("", "Please input type name ");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }
                            if (regis.Active == true)
                            {
                                if (regis.IsDefault == true)
                                {
                                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                    {
                                        var cl = dbConn.Select<DC_CheckList_Definition>("SELECT * FROM dbo.DC_CheckList_Definition Where IsDefault = 'true'").FirstOrDefault();
                                        if (cl != null)
                                        {
                                            cl.IsDefault = false;
                                            cl.RowLastUpdatedTime = DateTime.Now;
                                            cl.RowLastUpdatedUser = currentUser.UserName;
                                            cl.Update();

                                            var clo = new DC_CheckList_ApplyFor();
                                            clo.ChecklistID = cl.ChecklistID;
                                            clo.ChecklistIDDefault = regis.ChecklistID;
                                            clo.RowLastUpdatedTime = DateTime.Now;
                                            clo.RowLastUpdatedUser = currentUser.UserName;
                                            clo.UpdateIsDefault();
                                        }
                                        var og = dbConn.Select<DC_CheckList_ApplyFor>("SELECT C.[OrganizationID],C.CompanyID FROM [dbo].DC_Company C LEFT JOIN dbo.DC_CheckList_Definition_Org DO ON DO.CompanyID = C.CompanyID WHERE DO.CompanyID is null").ToList();
                                        var cdo = new DC_CheckList_ApplyFor();
                                        foreach (var itemOg in og)
                                        {
                                            string ido = "STE001";
                                            var checkid = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Orgs("1=1", "RowID DESC").FirstOrDefault();
                                            if (checkid != null)
                                            {
                                                var nextNo = Int32.Parse(checkid.ChecklistSubID.Substring(3, checkid.ChecklistSubID.Length - 3)) + 1;
                                                ido = "STE" + String.Format("{0:000}", nextNo);
                                            }
                                            cdo.ChecklistSubID = ido;
                                            cdo.ChecklistID = regis.ChecklistID;
                                            cdo.OrganizationID = itemOg.OrganizationID;
                                            cdo.CompanyID = itemOg.CompanyID;
                                            cdo.RowCreatedUser = currentUser.UserName;
                                            cdo.RowCreatedTime = DateTime.Now;
                                            cdo.Save();
                                        }
                                    }
                                }
                                else
                                {
                                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                                    {
                                        var cl = dbConn.Select<DC_CheckList_Definition>("SELECT * FROM dbo.DC_CheckList_Definition Where IsDefault = 'true'").FirstOrDefault();
                                        if (cl != null)
                                        {
                                            if (cl.ChecklistID == regis.ChecklistID)
                                            {
                                                cl.IsDefault = false;
                                                cl.RowLastUpdatedTime = DateTime.Now;
                                                cl.RowLastUpdatedUser = currentUser.UserName;
                                                cl.Update();

                                                var update = dbConn.Select<DC_CheckList_ApplyFor>("Select * from DC_CheckList_Definition_Org where ChecklistID = '" + cl.ChecklistID + "'").ToList();
                                                foreach (var item in update)
                                                {
                                                    var de = new DC_CheckList_ApplyFor();
                                                    de.ChecklistSubID = item.ChecklistSubID;
                                                    de.Delete();
                                                }

                                            }
                                        }
                                    }
                                }
                                var write = new DC_CheckList_Definition();
                                write.ChecklistID = regis.ChecklistID;
                                write.Name = regis.Name.Trim();
                                write.Active = regis.Active;
                                write.IsDefault = regis.IsDefault;
                                write.RowLastUpdatedTime = DateTime.Now;
                                write.RowLastUpdatedUser = currentUser.UserName;
                                write.Update();
                            }
                            else
                            {
                                ModelState.AddModelError("", "Check list is InActive");
                                return Json(listEx.ToDataSourceResult(request, ModelState));
                            }

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listEx.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CheckListDelete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var checkOrg = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Orgs("ChecklistID = '" + item + "'", "").FirstOrDefault();
                        var checkStep = DC_CheckList_Definition_Step.GetDC_CheckList_Definition_Steps("ChecklistID ='" + item + "'", "").FirstOrDefault();
                        if (checkOrg != null || checkStep != null)
                        {
                            return Json(new { success = false, alert = "Can not delete " + item });
                        }
                        var update = new DC_CheckList_Definition();
                        update.ChecklistID = item;
                        update.Delete();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult CheckListGetOrg([DataSourceRequest] DataSourceRequest request, string ChecklistID)
        {
            if (asset.View)
            {
                var data = DC_CheckList_ApplyFor.GetOrgList(ChecklistID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult CheckListSaveOrg([DataSourceRequest] DataSourceRequest request, string data, string ChecklistID)
        {
            if (asset.Create)
            {
                try
                {
                    string[] cont = data.Split('-');
                    var checkExistCom = DC_Company.CheckExitCompanyID(cont[0]);
                    if (checkExistCom == null)
                    {
                        return Json(new { success = false, alert = "Please check Company!" });
                    }
                    var checkExist = DC_CheckList_ApplyFor.checkExistChecklist(cont[0]);
                    if (checkExist != null)
                    {
                        checkExist.ChecklistID = ChecklistID;
                        checkExist.CompanyID = cont[0];
                        checkExist.RowLastUpdatedUser = currentUser.UserName;
                        checkExist.RowLastUpdatedTime = DateTime.Now;
                        checkExist.Update();
                    }
                    else
                    {
                        string id = "STE001";
                        var write = new DC_CheckList_ApplyFor();
                        var checkid = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Orgs("1=1", "RowID DESC").FirstOrDefault();
                        if (checkid != null)
                        {
                            var nextNo = Int32.Parse(checkid.ChecklistSubID.Substring(3, checkid.ChecklistSubID.Length - 3)) + 1;
                            id = "STE" + String.Format("{0:000}", nextNo);
                        }
                        write.ChecklistSubID = id;
                        write.ChecklistID = ChecklistID;
                        write.CompanyID = cont[0];
                        write.OrganizationID = "";
                        write.RowCreatedUser = currentUser.UserName;
                        write.RowCreatedTime = DateTime.Now;
                        write.Save();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }

        public ActionResult CheckListSub_Read([DataSourceRequest] DataSourceRequest request, string ChecklistID)
        {
            if (asset.View)
            {
                var data = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Org(ChecklistID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult CheckListSubDelete(string data) //aaa
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    using (var dbTrans = dbConn.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        foreach (var item in listRowID)
                        {
                            var cl = dbConn.Select<DC_CheckList_Definition>("SELECT * FROM dbo.DC_CheckList_Definition Where IsDefault = 'true'").FirstOrDefault();
                            if (cl != null)
                            {
                                var checkExist = DC_CheckList_ApplyFor.GetDC_CheckList_Definition_Orgs("ChecklistSubID = '" + item + "'", "").FirstOrDefault();
                                if (checkExist != null)
                                {
                                    if (checkExist.ChecklistID != cl.ChecklistID)
                                    {
                                        checkExist.ChecklistID = cl.ChecklistID;
                                        checkExist.RowLastUpdatedUser = currentUser.UserName;
                                        checkExist.RowLastUpdatedTime = DateTime.Now;
                                        checkExist.Update();
                                    }
                                    else
                                    {
                                        var update = new DC_CheckList_ApplyFor();
                                        update.ChecklistSubID = item;
                                        update.Delete();
                                    }
                                }
                            }
                            else
                            {
                                var update = new DC_CheckList_ApplyFor();
                                update.ChecklistSubID = item;
                                update.Delete();
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult CheckListGetStep([DataSourceRequest] DataSourceRequest request, string ChecklistID)
        {
            if (asset.View)
            {
                var data = DC_CheckList_Definition_Step.GetStepList(ChecklistID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult CheckListSaveStep([DataSourceRequest] DataSourceRequest request, string data, string ChecklistID)
        {
            if (asset.Create)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    var write = new DC_CheckList_Definition_Step();
                    foreach (var item in listRowID)
                    {
                        string id = "STE001";
                        int order = 1;
                        var checkid = DC_CheckList_Definition_Step.GetDC_CheckList_Definition_Steps("1=1", "RowID DESC").FirstOrDefault();
                        if (checkid != null)
                        {
                            var nextNo = Int32.Parse(checkid.ChecklistSubID.Substring(3, checkid.ChecklistSubID.Length - 3)) + 1;
                            id = "STE" + String.Format("{0:000}", nextNo);
                            order = checkid.Order + 1;
                        }
                        write.ChecklistSubID = id;
                        write.ChecklistID = ChecklistID;
                        write.StepID = item;
                        write.Order = order;
                        write.RowCreatedUser = currentUser.UserName;
                        write.RowCreatedTime = DateTime.Now;
                        write.Save();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to create record" });
            }
        }

        public ActionResult CheckListStep_Read([DataSourceRequest] DataSourceRequest request, string ChecklistID)
        {
            if (asset.View)
            {
                var data = DC_CheckList_Definition_Step.GetDC_CheckList_Definition_Step(ChecklistID);
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult CheckListStepDelete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        var update = new DC_CheckList_Definition_Step();
                        update.ChecklistSubID = item;
                        update.Delete();
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult CheckListStep_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_CheckList_Definition_Step> listEx)
        {
            if (asset.Update)
            {
                try
                {
                    if (listEx != null && ModelState.IsValid)
                    {
                        foreach (var regis in listEx)
                        {
                            var write = new DC_CheckList_Definition_Step();
                            write.ChecklistSubID = regis.ChecklistSubID;
                            write.Order = regis.Order;
                            write.ChecklistID = regis.ChecklistID;
                            write.StepID = regis.StepID;
                            write.RowLastUpdatedTime = DateTime.Now;
                            write.RowLastUpdatedUser = currentUser.UserName;
                            write.Update();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "");
                        return Json(new { success = false });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e.Message);
                    return Json(listEx.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listEx.ToDataSourceResult(request, ModelState));
            }
            return Json(listEx.ToDataSourceResult(request, ModelState));
        }

        public FileResult ExportExcelCheckList([DataSourceRequest]DataSourceRequest request)
        {
            if (asset.Export)
            {
                var Alias = DC_CheckList_Definition.GetDC_CheckList_Definitions("1=1", "RowCreatedTime DESC");
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_CheckList_Definition.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["DC_CheckList_Definition"];
                IEnumerable listData = Alias.ToDataSourceResult(request).Data;
                int rowData = 1;
                foreach (DC_CheckList_Definition data in listData)
                {
                    int i = 1;
                    rowData++;
                    dataSheet.Cells[rowData, i++].Value = data.ChecklistID;
                    dataSheet.Cells[rowData, i++].Value = data.Name;
                    dataSheet.Cells[rowData, i++].Value = data.IsDefault;
                    dataSheet.Cells[rowData, i++].Value = data.Active;
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime.ToString();
                    dataSheet.Cells[rowData, i++].Value = data.RowCreatedUser;
                    if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime.ToString();
                    }
                    else
                    {
                        dataSheet.Cells[rowData, i++].Value = "";
                    }
                    dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                }

                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                string fileName = "DC_CheckList_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                output.Position = 0;
                return File(output.ToArray(), contentType, fileName);
            }
            else
            {
                string fileName = "DC_CheckList_Definition_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File("", contentType, fileName);
            }
        }

        public ActionResult ImportFromExcelCheckList()
        {
            try
            {
                if (Request.Files["FileUpload"] != null && Request.Files["FileUpload"].ContentLength > 0)
                {
                    string fileExtension =
                        System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    if (fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["FileUpload"].FileName);
                        string errorFileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-Error]" + Request.Files["FileUpload"].FileName);
                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);
                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        var rownumber = 2;
                        var total = 0;
                        FileInfo fileInfo = new FileInfo(fileLocation);
                        var excelPkg = new ExcelPackage(fileInfo);
                        FileInfo template = new FileInfo(Server.MapPath(@"~\ExportExcelFile\DC_CheckList_Definition.xlsx"));
                        template.CopyTo(errorFileLocation);
                        FileInfo _fileInfo = new FileInfo(errorFileLocation);
                        var _excelPkg = new ExcelPackage(_fileInfo);
                        ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets["DC_CheckList_Definition"];
                        ExcelWorksheet eSheet = _excelPkg.Workbook.Worksheets["DC_CheckList_Definition"];
                        int totalRows = oSheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string ID = oSheet.Cells[i, 1].Value != null ? oSheet.Cells[i, 1].Value.ToString() : "";
                            string Name = oSheet.Cells[i, 2].Value != null ? oSheet.Cells[i, 2].Value.ToString() : "";
                            string DEFAULT = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "FALSE";
                            string ACTIVE = oSheet.Cells[i, 3].Value != null ? oSheet.Cells[i, 3].Value.ToString() : "FALSE";
                            try
                            {
                                var write = new DC_CheckList_Definition();
                                var checkAliasNameExist = DC_CheckList_Definition.GetDC_CheckList_Definitions("1=1", "").Where(s => s.ChecklistID == ID).FirstOrDefault();
                                if (string.IsNullOrEmpty(Name.ToString()))
                                {
                                    eSheet.Cells[rownumber, 1].Value = ID;
                                    eSheet.Cells[rownumber, 2].Value = Name;
                                    eSheet.Cells[rownumber, 3].Value = DEFAULT;
                                    eSheet.Cells[rownumber, 4].Value = ACTIVE;
                                    eSheet.Cells[rownumber, 9].Value = "Name required";
                                    rownumber++;
                                }
                                if (checkAliasNameExist != null)
                                {
                                    write.ChecklistID = ID;
                                    write.Name = Name;
                                    write.Active = Boolean.Parse(ACTIVE);
                                    write.IsDefault = Boolean.Parse(DEFAULT);
                                    write.RowLastUpdatedTime = DateTime.Now;
                                    write.RowLastUpdatedUser = currentUser.UserName;
                                    write.Update();
                                }
                                else
                                {
                                    string id = "";
                                    var checkID = DC_CheckList_Definition.GetDC_CheckList_Definitions("1=1", "").OrderByDescending(m => m.RowID).FirstOrDefault();
                                    if (checkID != null)
                                    {
                                        var nextNo = Int32.Parse(checkID.ChecklistID.Substring(3, checkID.ChecklistID.Length - 3)) + 1;
                                        id = "CHE" + String.Format("{0:000}", nextNo);
                                    }
                                    else
                                    {
                                        id = "CHE001";
                                    }
                                    write.ChecklistID = id;
                                    write.Name = Name;
                                    write.Active = Boolean.Parse(ACTIVE);
                                    write.IsDefault = Boolean.Parse(DEFAULT);
                                    write.RowCreatedTime = DateTime.Now;
                                    write.RowCreatedUser = currentUser.UserName;
                                    write.Save();
                                }
                                total++;
                            }
                            catch (Exception e)
                            {
                                eSheet.Cells[rownumber, 1].Value = ID;
                                eSheet.Cells[rownumber, 2].Value = Name;
                                eSheet.Cells[rownumber, 3].Value = DEFAULT;
                                eSheet.Cells[rownumber, 4].Value = ACTIVE;
                                eSheet.Cells[rownumber, 9].Value = e.Message;
                                rownumber++;
                                continue;
                            }
                        }
                        _excelPkg.Save();

                        return Json(new { success = true, total = total, totalError = rownumber - 2, link = errorFileLocation });
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
    }
}
