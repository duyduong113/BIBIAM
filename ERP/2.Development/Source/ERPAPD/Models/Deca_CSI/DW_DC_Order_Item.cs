using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPD.Models
{
    public class DW_DC_Order_Item
    {
        #region Members
        private string _orderid = String.Empty;
        public string OrderID { get { return _orderid; } set { this._orderid = value; } }

        private string _msin = String.Empty;
        public string MSIN { get { return _msin; } set { this._msin = value; } }

        private string _invrefid = String.Empty;
        public string InvRefID { get { return _invrefid; } set { this._invrefid = value; } }

        private double _unitprice;
        public double UnitPrice { get { return _unitprice; } set { this._unitprice = value; } }

        private int _quantity;
        public int Quantity { get { return _quantity; } set { this._quantity = value; } }

        private double _tax;
        public double Tax { get { return _tax; } set { this._tax = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

        private string _name = String.Empty;
        public string Name { get { return _name; } set { this._name = value; } }

        private DateTime _createdat;
        public DateTime CreatedAt { get { return _createdat; } set { this._createdat = value; } }

        private DateTime _modifiedat;
        public DateTime ModifiedAt { get { return _modifiedat; } set { this._modifiedat = value; } }

        private long _dwid;
        public long DWID { get { return _dwid; } set { this._dwid = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private int _rowid;
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }


        //partial
        private string _orderMSIN = String.Empty;
        public string OrderMSIN { get { return _orderMSIN; } set { this._orderMSIN = value; } }

        private string _organizationID = String.Empty;
        public string OrganizationID { get { return _organizationID; } set { this._organizationID = value; } }

        private string _actualMerchantID = String.Empty;
        public string ActualMerchantID { get { return _actualMerchantID; } set { this._actualMerchantID = value; } }

        private string _sceMerchantID = String.Empty;
        public string SCEMerchantID { get { return _sceMerchantID; } set { this._sceMerchantID = value; } }

        private string _rootMerchantID = String.Empty;
        public string RootMerchantID { get { return _rootMerchantID; } set { this._rootMerchantID = value; } }

        private string _realMerchantID = String.Empty;
        public string RealMerchantID { get { return _realMerchantID; } set { this._realMerchantID = value; } }

        private string _poNumber = String.Empty;
        public string PONumber { get { return _poNumber; } set { this._poNumber = value; } }

        private string _rpoNumber = String.Empty;
        public string RPONumber { get { return _rpoNumber; } set { this._rpoNumber = value; } }

        private string _invoice = String.Empty;
        public string Invoice { get { return _invoice; } set { this._invoice = value; } }

        private string _providerID = String.Empty;
        public string ProviderID { get { return _providerID; } set { this._providerID = value; } }

        private string _providerName = String.Empty;
        public string ProviderName { get { return _providerName; } set { this._providerName = value; } }

        private string _customerID = String.Empty;
        public string CustomerID { get { return _customerID; } set { this._customerID = value; } }

        private double _amount;
        public double Amount { get { return _amount; } set { this._amount = value; } }

        private double _buyingPrice;
        public double BuyingPrice { get { return _buyingPrice; } set { this._buyingPrice = value; } }

        private double _actualBuyingPrice;
        public double ActualBuyingPrice { get { return _actualBuyingPrice; } set { this._actualBuyingPrice = value; } }

        private double _fee;
        public double Fee { get { return _fee; } set { this._fee = value; } }

        private Double _percentCommission;
        public Double PercentCommission { get { return _percentCommission; } set { this._percentCommission = value; } }

        private string _article = String.Empty;
        public string Article { get { return _article; } set { this._article = value; } }

        private double _deliveryCost;
        public double DeliveryCost { get { return _deliveryCost; } set { this._deliveryCost = value; } }

        private double _quotation;
        public double Quotation { get { return _quotation; } set { this._quotation = value; } }

        private string _fullName = String.Empty;
        public string FullName { get { return _fullName; } set { this._fullName = value; } }

        private string _phone = String.Empty;
        public string Phone { get { return _phone; } set { this._phone = value; } }

        private string _shippingInfo = String.Empty;
        public string ShippingInfo { get { return _shippingInfo; } set { this._shippingInfo = value; } }

        private string _importNote = String.Empty;
        public string ImportNote { get { return _importNote; } set { this._importNote = value; } }

        private string _xTranID = String.Empty;
        public string XTranID { get { return _xTranID; } set { this._xTranID = value; } }

        private string _apID = String.Empty;
        public string APID { get { return _apID; } set { this._apID = value; } }

        //
        private string _statusColor = String.Empty;
        public string StatusColor { get { return _statusColor; } set { this._statusColor = value; } }

        private string _apStatus = String.Empty;
        public string APStatus { get { return _apStatus; } set { this._apStatus = value; } }

        private string _settlementType = String.Empty;
        public string SettlementType { get { return _settlementType; } set { this._settlementType = value; } }

        public DateTime InvoiceDate { get; set; }
        public string StockInNumber { get; set; }
        public DateTime StockInDate { get; set; }
        public string WHName { get; set; }
        public string WHLocation { get; set; }
        public double ActualTotal { get; set; }

        #endregion

        public DW_DC_Order_Item()
        { }

        public DW_DC_Order_Item(string OrderID, string MSIN, string InvRefID, double UnitPrice, int Quantity, double Tax, string Status, string Name, DateTime CreatedAt, DateTime ModifiedAt, long DWID, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._orderid = OrderID;
            this._msin = MSIN;
            this._invrefid = InvRefID;
            this._unitprice = UnitPrice;
            this._quantity = Quantity;
            this._tax = Tax;
            this._status = Status;
            this._name = Name;
            this._createdat = CreatedAt;
            this._modifiedat = ModifiedAt;
            this._dwid = DWID;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public static DW_DC_Order_Item GetDW_DC_Order_Item(string OrderID, string MSIN)
        {
            string PROCEDURE = "p_SelectDW_DC_Order_Item";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = OrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MSIN";
            parameters[i].Value = MSIN;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderMSIN = row["OrderID"].ToString() + "-" + row["MSIN"].ToString(),
                ProviderID = row["ProviderID"].ToString(),
                ProviderName = row["ProviderName"].ToString(),
                RootMerchantID = row["RootMerchantID"].ToString(),
                SCEMerchantID = row["SCEMerchantID"].ToString(),
                ActualMerchantID = row["ActualMerchantID"].ToString(),
                RealMerchantID = row["RealMerchantID"].ToString(),
                Invoice = row["Invoice"].ToString(),
                PONumber = row["PONumber"].ToString(),
                RPONumber = row["RPONumber"].ToString(),
                InvRefID = row["InvRefID"].ToString(),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                Tax = Double.Parse(row["Tax"].ToString()),
                Status = row["Status"].ToString(),
                APStatus = row["APStatus"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                Name = row["Name"].ToString(),
                XTranID = row["XTxnID"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Amount = Double.Parse(row["UnitPrice"].ToString()) * Convert.ToInt32(row["Quantity"].ToString()),
                FullName = row["FullName"].ToString(),
                Phone = row["Phone"].ToString(),
                ShippingInfo = row["ShippingInfo"].ToString(),
                APID = row["APID"].ToString(),
                SettlementType = row["SettlementType"].ToString(),
                BuyingPrice = Double.Parse(row["BuyingPrice"].ToString()),
                ActualBuyingPrice = Double.Parse(row["ActualBuyingPrice"].ToString()),
                Fee = Double.Parse(row["Fee"].ToString()),
                PercentCommission = Convert.ToDouble(row["PercentCommission"].ToString()),
                Article = row["Article"].ToString(),
            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Order_Item> GetDW_DC_Order_Items(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_Order_ItemsDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                OrderMSIN = row["OrderID"].ToString() + "-" + row["MSIN"].ToString(),
                InvRefID = row["InvRefID"].ToString(),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                Tax = Double.Parse(row["Tax"].ToString()),
                Status = row["Status"].ToString(),
                Name = row["Name"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Amount = Double.Parse(row["UnitPrice"].ToString()) * Convert.ToInt32(row["Quantity"].ToString())
            }).ToList();
        }

        public static List<DW_DC_Order_Item> GetAllDW_DC_Order_Items()
        {
            string PROCEDURE = "p_SelectDW_DC_Order_ItemsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderMSIN = row["OrderID"].ToString() + "-" + row["MSIN"].ToString(),
                ProviderID = row["ProviderID"].ToString(),
                ProviderName = row["ProviderName"].ToString(),
                RootMerchantID = row["RootMerchantID"].ToString(),
                SCEMerchantID = row["SCEMerchantID"].ToString(),
                ActualMerchantID = row["ActualMerchantID"].ToString(),
                RealMerchantID = row["RealMerchantID"].ToString(),
                Invoice = row["Invoice"].ToString(),
                PONumber = row["PONumber"].ToString(),
                RPONumber = row["RPONumber"].ToString(),
                InvRefID = row["InvRefID"].ToString(),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                Tax = Double.Parse(row["Tax"].ToString()),
                Status = row["Status"].ToString(),
                APStatus = row["APStatus"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                Name = row["Name"].ToString(),
                XTranID = row["XTxnID"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Amount = Double.Parse(row["UnitPrice"].ToString()) * Convert.ToInt32(row["Quantity"].ToString()),
                FullName = row["FullName"].ToString(),
                Phone = row["Phone"].ToString(),
                ShippingInfo = row["ShippingInfo"].ToString(),
                APID = row["APID"].ToString(),
                SettlementType = row["SettlementType"].ToString(),
                BuyingPrice = Double.Parse(row["BuyingPrice"].ToString()),
                ActualBuyingPrice = Double.Parse(row["ActualBuyingPrice"].ToString()),
                Fee = Double.Parse(row["Fee"].ToString()),
                PercentCommission = Convert.ToDouble(row["PercentCommission"].ToString()),
                Article = row["Article"].ToString(),
            }).ToList();
        }

        public static async Task<List<DW_DC_Order_Item>> GetAllDW_DC_Order_ItemsDynamic(string whereCondition)
        {
            string PROCEDURE = "p_SelectDW_DC_Order_ItemsAllDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                OrderMSIN = row["OrderID"].ToString() + "-" + row["MSIN"].ToString(),
                ProviderID = row["ProviderID"].ToString(),
                ProviderName = row["ProviderName"].ToString(),
                RootMerchantID = row["RootMerchantID"].ToString(),
                SCEMerchantID = row["SCEMerchantID"].ToString(),
                ActualMerchantID = row["ActualMerchantID"].ToString(),
                RealMerchantID = row["RealMerchantID"].ToString(),
                Invoice = row["Invoice"].ToString(),
                PONumber = row["PONumber"].ToString(),
                RPONumber = row["RPONumber"].ToString(),
                InvRefID = row["InvRefID"].ToString(),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Quantity = Convert.ToInt32(row["Quantity"].ToString()),
                Tax = Double.Parse(row["Tax"].ToString()),
                Status = row["Status"].ToString(),
                APStatus = row["APStatus"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                Name = row["Name"].ToString(),
                XTranID = row["XTxnID"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Amount = Double.Parse(row["UnitPrice"].ToString()) * Convert.ToInt32(row["Quantity"].ToString()),
                FullName = row["FullName"].ToString(),
                //Phone = row["Phone"].ToString(),
                //ShippingInfo = row["ShippingInfo"].ToString(),
                APID = row["APID"].ToString(),
                SettlementType = row["SettlementType"].ToString(),
                BuyingPrice = Double.Parse(row["BuyingPrice"].ToString()),
                ActualBuyingPrice = Double.Parse(row["ActualBuyingPrice"].ToString()),
                Fee = Double.Parse(row["Fee"].ToString()),
                PercentCommission = Convert.ToDouble(row["PercentCommission"].ToString()),
                Article = row["Article"].ToString(),
                InvoiceDate = DateTime.Parse(row["InvoiceDate"].ToString()),
                StockInNumber = row["StockInNumber"].ToString(),
                StockInDate = DateTime.Parse(row["StockInDate"].ToString()),
                WHName = row["WHName"].ToString(),
                WHLocation = row["WHLocation"].ToString(),
                ActualTotal = Double.Parse(row["ActualTotal"].ToString()),
            }).ToList();
        }

        public static List<DW_DC_Order_Item> GetXtranIDByOrderId(string orderId, string name)
        {
            string PROCEDURE = "select top 1 * from [vw_XTxnProperty] where OrderID = '" + orderId + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                XTranID = row["RefID"].ToString()
            }).ToList();
        }

        public static List<DW_DC_Order_Item> GetAllDW_DC_Order_ItemsForFillter()
        {
            string PROCEDURE = "p_SelectDW_DC_Order_ItemsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                Status = row["Status"].ToString()

            }).ToList();
        }


        /* NAM (Credit Limit Request.) */

        public static List<DW_DC_Order_Item> GetSuggestDC_OrderItemList(string OrganizationID, string CustomerID, string FullName, string Phone, string OrderID)
        {

            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'%" + CustomerID + "%'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'%" + FullName + "%'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'%" + Phone + "%'";
            OrderID = string.IsNullOrEmpty(OrderID) ? "N'%%'" : "N'%" + OrderID + "%'";


            string PROCEDURE = "EXEC [p_Select_DC_OrderItem_Suggest]@OrganizationID = '" + OrganizationID + "'," + " @CustomerID = " + CustomerID + ",@MobilePhone = " + Phone + ",@OrderID = " + OrderID + ",@FullName = " + FullName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                Status = row["Status"].ToString()

            }).ToList();
        }

        public static List<DW_DC_Order_Item> GetSuggestDC_OrderItemListForGird(string OrganizationID, string CustomerID, string FullName, string Phone, string OrderID)
        {

            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'%" + CustomerID + "%'";
            FullName = string.IsNullOrEmpty(FullName) ? "N'%%'" : "N'%" + FullName + "%'";
            Phone = string.IsNullOrEmpty(Phone) ? "N'%%'" : "N'%" + Phone + "%'";
            OrderID = string.IsNullOrEmpty(OrderID) ? "N'%%'" : "N'%" + OrderID + "%'";


            string PROCEDURE = "EXEC [p_Select_DC_OrderItem_Suggest]@OrganizationID = '" + OrganizationID + "'," + " @CustomerID = " + CustomerID + ",@MobilePhone = " + Phone + ",@OrderID = " + OrderID + ",@FullName = " + FullName;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                Status = row["Status"].ToString()

            }).ToList();
        }

        public static List<DW_DC_Order_Item> CheckOrderItem(string OrganizationID, string CustomerID, string OrderID)
        {
            CustomerID = string.IsNullOrEmpty(CustomerID) ? "N'%%'" : "N'%" + CustomerID + "%'";
            OrderID = string.IsNullOrEmpty(OrderID) ? "N'%%'" : "N'%" + OrderID + "%'";

            string PROCEDURE = "EXEC [p_Select_DC_CheckOrderItem]@OrganizationID = '" + OrganizationID + "'," + " @CustomerID = " + CustomerID + ",@OrderID = " + OrderID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CustomerID = row["CustomerID"].ToString()
            }).ToList();
        }

        //vuongnd-get orderitem by Customer [Customer Screen]  
        public static List<DW_DC_Order_Item> GetOrderItemsOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getOrderItemByCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["ItemCode"].ToString(),
                Name = row["ItemName"].ToString(),
                Quantity = Int32.Parse(row["Quantity"].ToString()),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Amount = Double.Parse(row["Total"].ToString()),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
            }).ToList();
        }

        public static List<DW_DC_Order_Item> GetOrderItemsOfOrder(string orderID)
        {
            string PROCEDURE = "p_getOrderItemByOrderID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = orderID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                OrderID = row["OrderID"].ToString(),
                MSIN = row["ItemCode"].ToString(),
                Name = row["ItemName"].ToString(),
                Quantity = Int32.Parse(row["Quantity"].ToString()),
                UnitPrice = Double.Parse(row["UnitPrice"].ToString()),
                Amount = Double.Parse(row["Total"].ToString()),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
            }).ToList();
        }

        //vuongnd-get running balance
        public static double GetRunningBalanceDW_DC_Order_Item()
        {
            string text = "SELECT SUM(Quantity*UnitPrice) AS 'RunningBalance' from DW_DC_Order_Item WHERE [Status] NOT IN ('denied','failed')";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order_Item
            {
                Amount = Double.Parse(row["RunningBalance"].ToString())
            }).ToList().FirstOrDefault().Amount;
        }
    }
}