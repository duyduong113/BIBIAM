using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Data.OleDb;
using System.Data;
using System.Dynamic;
using Kendo.Mvc;
using OfficeOpenXml;
using ERPAPD.Helpers;
using System.Globalization;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;

namespace ERPAPD.Controllers
{
    public class BankDefinitionController : CustomController
    {
        //
        // GET: /CompanyDefinition/

        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_Bank_Action), typeof(Deca_Bank_Contactor), typeof(Deca_Bank_Installment));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Bank_Action), typeof(Deca_Bank_Contactor), typeof(Deca_Bank_Installment));
            //}
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {

                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    ViewData["Asset"] = asset;
                    ViewBag.Contract = dbConn.Select<DC_Stage_Definition>();
                    ViewBag.Assignee = dbConn.Select<Users>();
                    ViewBag.PaymentGateway = dbConn.Select<Deca_Code_Master>("[CodeType]={0}", "PaymentGateway");
                    return View();
                }

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

                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<DC_Bank_Definition>(request);
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Action_Read([DataSourceRequest] DataSourceRequest request, string BankID)
        {
            if (asset.View)
            {

                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_Bank_Action>(request, " BankID = '" + BankID + "'");
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Installment_Read([DataSourceRequest] DataSourceRequest request, string BankID)
        {
            if (asset.View)
            {

                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_Bank_Installment>(request, " BankID = '" + BankID + "'");
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Contactor_Read([DataSourceRequest] DataSourceRequest request, string BankID)
        {
            if (asset.View)
            {

                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_Bank_Contactor>(request, " BankID = '" + BankID + "'");
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [HttpPost]
        public ActionResult SaveBank([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Bank_Definition> listBank)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
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
                                typ.BankID = id;
                                typ.BankName = typ.BankName.Trim();
                                typ.Active = true;
                                typ.XMLString = "";
                                typ.RowCreatedTime = DateTime.Now;
                                typ.RowCreatedUser = currentUser.UserName;
                                typ.RowLastUpdatedTime = DateTime.Now;
                                typ.RowLastUpdatedUser = currentUser.UserName;
                                dbConn.Insert(typ);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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
        public ActionResult UpdateBank([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<DC_Bank_Definition> listBank)
        {
            if (asset.Update)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
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
                                var check = DC_Bank_Definition.GetDC_Bank_Definitions("[BankName] = '" + regis.BankName + "' AND BankID <> '" + regis.BankID + "'", "");
                                if (check.Count() > 0)
                                {
                                    ModelState.AddModelError("", " Bank Name is exists.");
                                    return Json(listBank.ToDataSourceResult(request, ModelState));
                                }
                                var currentBank = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID = {0}", regis.BankID);
                                regis.RowLastUpdatedTime = DateTime.Now;
                                regis.XMLString = "";
                                regis.RowLastUpdatedUser = currentUser.UserName;
                                regis.RowCreatedTime = currentBank.RowCreatedTime;
                                regis.RowCreatedUser = currentBank.RowCreatedUser;
                                dbConn.Update(regis);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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




        [HttpPost]
        public ActionResult SaveAction([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<Deca_Bank_Action> listBank, string BankID)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        if (listBank != null && ModelState.IsValid)
                        {
                            string lastaction = "";
                            foreach (var typ in listBank)
                            {

                                typ.BankID = BankID;
                                typ.CreatedAt = DateTime.Now;
                                typ.CreatedBy = currentUser.UserName;
                                dbConn.Insert(typ);
                                lastaction = typ.Action;
                            }
                            var currentBank = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID={0}", BankID);
                            currentBank.LastAction = lastaction;
                            dbConn.Update(currentBank);
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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

        public ActionResult DeleteAction(string data)
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
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            var check = dbConn.FirstOrDefault<Deca_Bank_Action>("ID=" + item);
                            dbConn.Delete(check);
                            success++;
                        }
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


        [HttpPost]
        public ActionResult SaveContactor([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<Deca_Bank_Contactor> listBank, string BankID)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        if (listBank != null && ModelState.IsValid)
                        {
                            foreach (var typ in listBank)
                            {
                                typ.BankID = BankID;
                                typ.CreatedAt = DateTime.Now;
                                typ.CreatedBy = currentUser.UserName;
                                dbConn.Insert(typ);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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
        public ActionResult UpdateContactor([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<Deca_Bank_Contactor> listBank, string BankID)
        {
            if (asset.Update)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        if (listBank != null && ModelState.IsValid)
                        {

                            foreach (var regis in listBank)
                            {
                                regis.BankID = BankID;
                                regis.UpdatedAt = DateTime.Now;
                                regis.UpdatedBy = currentUser.UserName;
                                dbConn.Update(regis);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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

        public ActionResult DeleteContactor(string data)
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
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            var check = dbConn.FirstOrDefault<Deca_Bank_Contactor>("ID=" + item);
                            dbConn.Delete(check);
                            success++;
                        }
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



        [HttpPost]
        public ActionResult SaveInstallment([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<Deca_Bank_Installment> listBank, string BankID)
        {
            if (asset.Create)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {

                        if (listBank != null && ModelState.IsValid)
                        {
                            int DefaultInstallment = 0;
                            //check if more than 1 data default
                            if (listBank.Count(a => a.Default == true) > 1)
                            {
                                ModelState.AddModelError("error", "Chỉ được chọn một trả góp mặc định");
                                return Json(new { success = false });
                            }
                            foreach (var typ in listBank)
                            {
                                typ.BankID = BankID;
                                typ.Active = true;
                                typ.UpdatedAt = DateTime.Now;
                                typ.CreatedBy = currentUser.UserName;
                                typ.CreatedAt = DateTime.Now;
                                typ.CreatedBy = currentUser.UserName;
                                dbConn.Insert(typ);
                                var lastinsert = dbConn.GetLastInsertId();
                                if (typ.Default)
                                {
                                    DefaultInstallment = typ.Installment;
                                    dbConn.Update<Deca_Bank_Installment>("[Default]=0", "ID <>" + lastinsert);
                                }
                            }
                            if (DefaultInstallment > 0)
                            {
                                var currentBank = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID={0}", BankID);
                                currentBank.DefaultInstallment = DefaultInstallment;
                                dbConn.Update(currentBank);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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
        public ActionResult UpdateInstallment([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]  IEnumerable<Deca_Bank_Installment> listBank, string BankID)
        {
            if (asset.Update)
            {
                try
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        if (listBank != null && ModelState.IsValid)
                        {
                            int DefaultInstallment = 0;
                            //check if more than 1 data default
                            if (listBank.Count(a => a.Default == true) > 1)
                            {
                                ModelState.AddModelError("error", "Chỉ được chọn một trả góp mặc định");
                                return Json(new { success = false });
                            }
                            foreach (var regis in listBank)
                            {
                                regis.BankID = BankID;
                                regis.Active = true;
                                regis.UpdatedAt = DateTime.Now;
                                regis.UpdatedBy = currentUser.UserName;
                                dbConn.Update(regis);
                                if (regis.Default)
                                {
                                    DefaultInstallment = regis.Installment;
                                    dbConn.Update<Deca_Bank_Installment>("[Default]=0", "ID <>" + regis.ID);
                                }
                            }
                            if (DefaultInstallment > 0)
                            {
                                var currentBank = dbConn.FirstOrDefault<DC_Bank_Definition>("BankID={0}", BankID);
                                currentBank.DefaultInstallment = DefaultInstallment;
                                dbConn.Update(currentBank);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("error", "");
                            return Json(new { success = false });
                        }
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

        public ActionResult DeleteInstallment(string data)
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
                        using (var dbConn = Helpers.OrmliteConnection.openConn())
                        {
                            var check = dbConn.FirstOrDefault<Deca_Bank_Installment>("ID=" + item);
                            dbConn.Delete(check);
                            success++;
                        }
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

    }
}
