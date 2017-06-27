using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERPAPD.Models
{
    public class InitLanguageSystem
    {
        public string Language { get; set; }
        #region Common in System
        public string btnLogin { get; set; }
        public string btnLogout { get; set; }
        public string btnChangePassWord { get; set; }
        public string btnAdd { get; set; }
        public string btnUpdate{ get; set; }
        public string btnDelete { get; set; }
        public string btnView { get; set; }
        public string btnSave { get; set; }
        public string btnExport { get; set; }
        public string btnImport { get; set; }
        public string btnChange { get; set; }
        public string Email { get; set; }
        public string EmailVaidate { get; set; }
        public string FullName { get; set; }
        public string FullNameVaidate { get; set; }
        public string Phone { get; set; }

        #endregion Common

        #region Change Profile Layout
        public string Account { get; set; }
        public string Gender { get; set; }
        public string SalesApp { get; set; }
        public string Choose { get; set; }
        public string Position { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public string Team { get; set; }
        public string Extension { get; set; }
        public string AccountCode { get; set; }

        #endregion Change Profile Layout

        #region DateFormat in System
        public string dateFormat { get; set; }
        #endregion
        #region Curentcy in System
        public string CurrencyUnit { get; set; }
        #endregion
        #region Time Zone in System
        public string timeZone { get; set; }
        #endregion
    }
    public class LayoutLanguage
    {
        #region label
        public string LblHello { get; set; }
        public string LblProfile { get; set; }
        public string LblChangePassW { get; set; }
        public string LblSetting { get; set; }
        public string LblLogout { get; set; }
        public string LblHome { get; set; }
        public string LblExtensionNull { get; set; }
        
        #endregion label
        #region menu
        public string MenuSystem { get; set; }
        public string GroupUser { get; set; }
        public string MenuGroupUser { get; set; }
        public string MenuUsers { get; set; }
        public string MenuManageController { get; set; }
        public string MenuDataitem { get; set; }
        public string MenuDepartMent { get; set; }
        public string MenuLocation { get; set; }
        public string MenuCountryDefinition { get; set; }
        public string MenuAliasDefinition { get; set; }
        public string MenuRegionDefinition { get; set; }
        public string MenuCityDefinition { get; set; }
        public string MenuDistrict { get; set; }
        public string MenuMappingLocation { get; set; }
        public string MenuInfomationManage { get; set; }
        public string MenuInfomationWrite { get; set; }
        public string MenuInfomationInternal { get; set; }
        public string MenuCustomerSupport { get; set; }
        public string MenuCustomerSupportManageInfo { get; set; }
        public string MenuOrderOTP { get; set; }
        public string MenuSuveyTool { get; set; }
        public string MenuSurveyManagement { get; set; }
        public string MenuDataSurvey { get; set; }
        public string MenuFAQ { get; set; }
        public string MenuFAQTopic { get; set; }
        public string MenuFAQManagement { get; set; }
        public string MenuAdminCustomer { get; set; }
        public string MenuCompanyTypeScales { get; set; }
        public string MenuRequestTicket { get; set; }
        public string MenuTicketMasterData { get; set; }
        public string MenuTelesale { get; set; }
        public string MenuSMSMOManagement { get; set; }
        public string MenuApplyForManager { get; set; }
        public string MenuTelesalesAgentKPI { get; set; }
        public string MenuAvoidCallingTimeCompany { get; set; }
        public string MenuTelesalesManageInfo { get; set; }
        public string MenuTelesalesPromotionInfo { get; set; }
        public string CallHistoryManagement { get; set; }
        public string TelesalesXliteHistory { get; set; }
        public string TelesalesDataMenu { get; set; }
        public string TelesalesAgent { get; set; }
        public string TelesalesResultList { get; set; }
        public string TelesalesQuestionList { get; set; }
        public string TelesalesAvoidCallingTimeFrame { get; set; }
        public string VietinErrorCode { get; set; }
        public string SalesManagement { get; set; }
        public string BankDefinition { get; set; }
        public string Company { get; set; }
        public string PotentialCustomer { get; set; }
        public string ProfileManagement { get; set; }
        public string Customer { get; set; }
        public string OrderHeader { get; set; }
        public string InstallmentOrder { get; set; }
        public string PreOrderChangeInfo { get; set; }
        public string FailedVietinTransaction { get; set; }
        public string PlanKPI { get; set; }
        public string Bank { get; set; }
        public string BankPotentialCustomer { get; set; }
        public string BankTransactions { get; set; }
        public string ApplicationSales { get; set; }
        public string SalesAppIntro { get; set; }
        public string SalesAppBanner { get; set; }
        public string SalesAppUser { get; set; }
        #endregion menu
        public static LayoutLanguage GetLanguage()
        {
            LayoutLanguage lan = new LayoutLanguage();
            string[] propertyNames = lan.GetType().GetProperties().Select(p => p.Name).ToArray();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(@"~\InitLanguage\LayOutLanguage.xml"), FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            for (int i = 0; i < propertyNames.Length; i++)
            {
                xmlnode = xmldoc.GetElementsByTagName(propertyNames[i]);
                if (xmlnode.Count == 0)
                {
                    continue;
                }
                var text = xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
                Type type = lan.GetType();
                PropertyInfo prop = type.GetProperty(propertyNames[i]);
                prop.SetValue(lan, text, null);
            }
            return lan;
        }
    }
}
