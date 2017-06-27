using DevTrends.MvcDonutCaching;

namespace ERPAPD.Helpers
{
    public class RemoveCache
    {
        public static void CacheDashboardSCE()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("DBOrderFulfillment");
        }

        public static void CacheCCR()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("CustomerCreationRequest");
            cacheManager.RemoveItems("ManageCreationRequest");
        }

        public static void CacheCreditRequest()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("CreditRequest");
            cacheManager.RemoveItems("ManageCreditRequest");
            cacheManager.RemoveItems("AddOrder");
        }

        public static void CacheCustomer()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("Customer", "Customer_ReadAll");
        }

        public static void CacheOrganization()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("Organization", "Organization_Read");
            cacheManager.RemoveItems("OPSOrganization", "Organization_Read");
        }

        public static void CacheIntran()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("Inventory", "InTran_Read");
            cacheManager.RemoveItems("InventorySKU", "InTran_Read");
        }

        public static void CacheBadDebt()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("CustomerResigned", "CustomerResigned_Read");
            cacheManager.RemoveItems("CustomerResigned", "RemindDayResigned_Read");
            cacheManager.RemoveItems("CustomerResigned", "RemindMinuteResigned_Read");
        }

        public static void CacheTelesalesKPI()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TelesalePluginCode", "_KPIAndActualForTelesale");
        }

        public static void CacheTopUp()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TopUpMonitoring", "Read");
        }

        public static void CachePromptedPersonal()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("PromptedPersonal", "PromptedPersonals_Read");
        }
        public static void CacheArea()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("Area");
            cacheManager.RemoveItems("MOPRegion");
            cacheManager.RemoveItems("MOPBranhch");
        }

        public static void CacheSMS()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("SMSMT", "SMSMT_Read");
            cacheManager.RemoveItems("SMSMO", "SMSMO_Read");
        }

        public static void CacheOrgazanitionPDSupport()
        {
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("Organization", "Organization_Read");
        }
    }
}