using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPD.Models
{
    public class DW_DC_Order
    {
        #region Members
        private string _orderid = String.Empty;
        public string OrderID { get { return _orderid; } set { this._orderid = value; } }

        private string _channel = String.Empty;
        public string Channel { get { return _channel; } set { this._channel = value; } }

        private double _amount;
        public double Amount { get { return _amount; } set { this._amount = value; } }

        private string _customerid = String.Empty;
        public string CustomerID { get { return _customerid; } set { this._customerid = value; } }

        private string _paymentid = String.Empty;
        public string PaymentID { get { return _paymentid; } set { this._paymentid = value; } }

        private string _paymenttype = String.Empty;
        public string PaymentType { get { return _paymenttype; } set { this._paymenttype = value; } }

        private string _shippingid = String.Empty;
        public string ShippingID { get { return _shippingid; } set { this._shippingid = value; } }

        private string _shippingtype = String.Empty;
        public string ShippingType { get { return _shippingtype; } set { this._shippingtype = value; } }

        private string _shippingstreet = String.Empty;
        public string ShippingStreet { get { return _shippingstreet; } set { this._shippingstreet = value; } }


        private string _shippingprovince = String.Empty;
        [Required]
        public string ShippingProvince { get { return _shippingprovince; } set { this._shippingprovince = value; } }


        private string _shippingcity = String.Empty;
        [Required]
        public string ShippingCity { get { return _shippingcity; } set { this._shippingcity = value; } }


        private string _shippingcountry = String.Empty;
        [Required]
        public string ShippingCountry { get { return _shippingcountry; } set { this._shippingcountry = value; } }

        private int _kpi;
        public int KPI { get { return _kpi; } set { this._kpi = value; } }

        private string _shippingemail = String.Empty;
        public string ShippingEmail { get { return _shippingemail; } set { this._shippingemail = value; } }

        private string _shippingmobile = String.Empty;
        public string ShippingMobile { get { return _shippingmobile; } set { this._shippingmobile = value; } }

        private string _shippingphone = String.Empty;
        public string ShippingPhone { get { return _shippingphone; } set { this._shippingphone = value; } }

        private string _status = String.Empty;
        public string Status { get { return _status; } set { this._status = value; } }

        private string _providerid = String.Empty;
        public string ProviderID { get { return _providerid; } set { this._providerid = value; } }

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
        private string _organizationID = String.Empty;
        public string OrganizationID { get { return _organizationID; } set { this._organizationID = value; } }

        private string _customerGroupID = String.Empty;
        public string CustomerGroupID { get { return _customerGroupID; } set { this._customerGroupID = value; } }

        private string _customerName = String.Empty;
        public string CustomerName { get { return _customerName; } set { this._customerName = value; } }

        private string _address = String.Empty;
        public string Address { get { return _address; } set { this._address = value; } }

        private string _color = String.Empty;
        public string Color { get { return _color; } set { this._color = value; } }

        private string _providerName = String.Empty;
        public string ProviderName { get { return _providerName; } set { this._providerName = value; } }

        private string _actualProviderID = String.Empty;
        public string ActualProviderID { get { return _actualProviderID; } set { this._actualProviderID = value; } }

        private double _quotation;
        public double Quotation { get { return _quotation; } set { this._quotation = value; } }

        private string _bd;
        public string BD { get { return _bd; } set { this._bd = value; } }

        private string _contractstatus;
        public string ContractStatus { get { return _contractstatus; } set { this._contractstatus = value; } }

        private string _reason;
        public string Reason { get { return _reason; } set { this._reason = value; } }

        private DateTime _estimateDeliveryDate;
        public DateTime EstimateDeliveryDate { get { return _estimateDeliveryDate; } set { this._estimateDeliveryDate = value; } }

        private string _periodId;
        public string PeriodID { get { return _periodId; } set { this._periodId = value; } }

        private DateTime _settlementDate;
        public DateTime SettlementDate { get { return _settlementDate; } set { this._settlementDate = value; } }

        private DateTime _actualSettlementDate;
        public DateTime ActualSettlementDate { get { return _actualSettlementDate; } set { this._actualSettlementDate = value; } }

        private double _settlementAmount;
        public double SettlementAmount { get { return _settlementAmount; } set { this._settlementAmount = value; } }

        private double _confirmAmount;
        public double ConfirmAmount { get { return _confirmAmount; } set { this._confirmAmount = value; } }

        private DateTime _orgPaymentDate;
        public DateTime OrgPaymentDate { get { return _orgPaymentDate; } set { this._orgPaymentDate = value; } }

        private DateTime _PaymentDate;
        public DateTime PaymentDate { get { return _PaymentDate; } set { this._PaymentDate = value; } }

        private double _payInPlan;
        public double PayInPlan { get { return _payInPlan; } set { this._payInPlan = value; } }

        private double _payOutPlan;
        public double PayOutPlan { get { return _payOutPlan; } set { this._payOutPlan = value; } }

        private string _repayNote;
        public string RepayNote { get { return _repayNote; } set { this._repayNote = value; } }

        private string _opsNote;
        public string OPSNote { get { return _opsNote; } set { this._opsNote = value; } }

        private string _orgCollection;
        public string ORGCollection { get { return _orgCollection; } set { this._orgCollection = value; } }

        private string _sendSMS;
        public string SendSMS { get { return _sendSMS; } set { this._sendSMS = value; } }

        private string _banktoolId;
        public string BankToolID { get { return _banktoolId; } set { this._banktoolId = value; } }
        // Item Order

        public string ItemName { get; set; }
        #endregion

        public DW_DC_Order()
        { }

        public DW_DC_Order(string OrderID, string Channel, double Amount, string CustomerID, string PaymentID, string PaymentType, string ShippingID, string ShippingType, string ShippingStreet, string ShippingProvince, string ShippingCity, string ShippingCountry, int kpi, string ShippingEmail, string ShippingMobile, string ShippingPhone, string Status, string ProviderID, DateTime CreatedAt, DateTime ModifiedAt, long DWID, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._orderid = OrderID;
            this._channel = Channel;
            this._amount = Amount;
            this._customerid = CustomerID;
            this._paymentid = PaymentID;
            this._paymenttype = PaymentType;
            this._shippingid = ShippingID;
            this._shippingtype = ShippingType;
            this._shippingstreet = ShippingStreet;
            this._shippingprovince = ShippingProvince;
            this._shippingcity = ShippingCity;
            this._shippingcountry = ShippingCountry;
            this._kpi = kpi;
            this._shippingemail = ShippingEmail;
            this._shippingmobile = ShippingMobile;
            this._shippingphone = ShippingPhone;
            this._status = Status;
            this._providerid = ProviderID;
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
        public static  DW_DC_Order CheckOrder(string OrderID)
        {
            string PROCEDURE = "select CreatedAt, Status  from DW_DC_Order  where OrderID = @OrderID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrderID";
            parameters[0].Value = OrderID;


            DataSet ds = SqlHelperAsync.ExecuteDataset(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = OrderID,
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                Status = row["Status"].ToString(),
            }).ToList().FirstOrDefault();
        }
        public static DW_DC_Order CheckOrderMSIN(string OrderID, string MSIN)
        {
            string PROCEDURE = "select o.CreatedAt, o.Status, o.CustomerID, i.UnitPrice * i.Quantity as Amount from DW_DC_Order o inner join DW_DC_Order_Item i on i.OrderID = o.OrderID and i.MSIN = @MSIN where o.OrderID = @OrderID";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrderID";
            parameters[0].Value = OrderID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@MSIN";
            parameters[1].Value = MSIN;


            DataSet ds = SqlHelperAsync.ExecuteDataset(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = OrderID,
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                Status = row["Status"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                Amount = double.Parse(row["Amount"].ToString()),
            }).ToList().FirstOrDefault();
        }
        public static async Task<DW_DC_Order> GetDW_DC_Order(string OrderID)
        {
            string PROCEDURE = "p_SelectDW_DC_Order";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrderID";
            parameters[0].Value = OrderID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = row["OrderID"].ToString(),
                Channel = row["Channel"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Status = row["Status"].ToString(),
                KPI = Convert.ToInt32(row["KPI"].ToString()),
                Address = row["Address"].ToString(),
                ShippingProvince = row["ShippingProvince"].ToString(),
                ShippingCity = row["ShippingCity"].ToString(),
                ShippingCountry = row["ShippingCountry"].ToString(),
                Reason = row["Reason"].ToString(),
                EstimateDeliveryDate = Convert.ToDateTime(row["EstimateDeliveryDate"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                ShippingEmail = row["ShippingEmail"].ToString(),
                ShippingMobile = row["ShippingMobile"].ToString(),
                BD = row["BD"].ToString(),
                Color = row["Color"].ToString(),


                /*  PaymentID = row["PaymentID"].ToString(),
                    PaymentType = row["PaymentType"].ToString(),
                    ShippingID = row["ShippingID"].ToString(),
                    ShippingType = row["ShippingType"].ToString(),
                    ShippingStreet = row["ShippingStreet"].ToString(),            
                    ContractStatus = row["Contract Status"].ToString(),
                    ShippingPhone = row["ShippingPhone"].ToString(),         
                    ProviderID = row["ProviderID"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                    ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                    Active = Convert.ToBoolean(row["Active"].ToString()),
                    RowID = Convert.ToInt32(row["RowID"].ToString()),
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                    RowCreatedUser = row["RowCreatedUser"].ToString(),
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),          
                    CustomerGroupID = row["GroupID"].ToString(),                                
                    OrganizationID = row["CustomerID"].ToString().Split(':')[0] */

            }).ToList().FirstOrDefault();
        }

        public static List<DW_DC_Order> GetDW_DC_Orders(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_OrdersDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = row["OrderID"].ToString(),
                Channel = row["Channel"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Status = row["Status"].ToString(),
                KPI = Convert.ToInt32(row["KPI"].ToString()),
                Address = row["Address"].ToString(),
                ShippingProvince = row["ShippingProvince"].ToString(),
                ShippingCity = row["ShippingCity"].ToString(),
                ShippingCountry = row["ShippingCountry"].ToString(),
                Reason = row["Reason"].ToString(),
                EstimateDeliveryDate = Convert.ToDateTime(row["EstimateDeliveryDate"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                ShippingEmail = row["ShippingEmail"].ToString(),
                ShippingMobile = row["ShippingMobile"].ToString(),
                BD = row["BD"].ToString(),
                Color = row["Color"].ToString(),


                /*  PaymentID = row["PaymentID"].ToString(),
                    PaymentType = row["PaymentType"].ToString(),
                    ShippingID = row["ShippingID"].ToString(),
                    ShippingType = row["ShippingType"].ToString(),
                    ShippingStreet = row["ShippingStreet"].ToString(),            
                    ContractStatus = row["Contract Status"].ToString(),
                    ShippingPhone = row["ShippingPhone"].ToString(),         
                    ProviderID = row["ProviderID"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                    ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                    Active = Convert.ToBoolean(row["Active"].ToString()),
                    RowID = Convert.ToInt32(row["RowID"].ToString()),
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                    RowCreatedUser = row["RowCreatedUser"].ToString(),
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),          
                    CustomerGroupID = row["GroupID"].ToString(),                                
                    OrganizationID = row["CustomerID"].ToString().Split(':')[0] */
            }).ToList();
        }

        public static List<DW_DC_Order> GetAllDW_DC_Orders()
        {
            string PROCEDURE = "p_SelectDW_DC_OrdersAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = row["OrderID"].ToString(),
                Channel = row["Channel"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Status = row["Status"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                KPI = Convert.ToInt32(row["KPI"].ToString()),
                Address = row["Address"].ToString(),
                ShippingProvince = row["ShippingProvince"].ToString(),
                ShippingCity = row["ShippingCity"].ToString(),
                ShippingCountry = row["ShippingCountry"].ToString(),
                Reason = row["Reason"].ToString(),
                EstimateDeliveryDate = Convert.ToDateTime(row["EstimateDeliveryDate"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                ShippingEmail = row["ShippingEmail"].ToString(),
                ShippingMobile = row["ShippingMobile"].ToString(),
                BD = row["BD"].ToString(),
                Color = row["Color"].ToString(),
                CustomerGroupID = row["GroupID"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString())

                /*  PaymentID = row["PaymentID"].ToString(),
                    PaymentType = row["PaymentType"].ToString(),
                    ShippingID = row["ShippingID"].ToString(),
                    ShippingType = row["ShippingType"].ToString(),
                    ShippingStreet = row["ShippingStreet"].ToString(),            
                    ContractStatus = row["Contract Status"].ToString(),
                    ShippingPhone = row["ShippingPhone"].ToString(),         
                    ProviderID = row["ProviderID"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                    ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                    Active = Convert.ToBoolean(row["Active"].ToString()),
                    RowID = Convert.ToInt32(row["RowID"].ToString()),
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                    RowCreatedUser = row["RowCreatedUser"].ToString(),
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),          
                    CustomerGroupID = row["GroupID"].ToString(),                                
                    OrganizationID = row["CustomerID"].ToString().Split(':')[0] */
            }).ToList();
        }



        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Order";
            SqlParameter[] parameters = new SqlParameter[20];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this._orderid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Channel";
            parameters[i].Value = this._channel;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Amount";
            parameters[i].Value = this._amount;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = this._customerid;
            i++;


            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PaymentID";
            parameters[i].Value = this._paymentid;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PaymentType";
            parameters[i].Value = this._paymenttype;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingID";
            parameters[i].Value = this._shippingid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingType ";
            parameters[i].Value = this._shippingtype;
            i++;




            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingStreet";
            parameters[i].Value = this._shippingstreet;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingProvince";
            parameters[i].Value = this._shippingprovince;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingCity";
            parameters[i].Value = this.ShippingCity;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingCountry";
            parameters[i].Value = this._shippingcountry;
            i++;



            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingEmail";
            parameters[i].Value = this._shippingemail;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingMobile ";
            parameters[i].Value = this._shippingmobile;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingPhone";
            parameters[i].Value = this._shippingphone;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedAt";
            parameters[i].Value = this._shippingphone;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ProviderID";
            parameters[i].Value = this._providerid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        /*   NAM (Credit Limit Request.)*/

        // Class check : CustomerID & OrderID  for Credit Limit
        public static List<DW_DC_Order> CheckGetDW_DC_Orders(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDW_DC_OrdersDynamic";
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
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = row["OrderID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
            }).ToList();
        }

        //vuongnd-get orderlist by Customer [Customer Screen]  
        public static List<DW_DC_Order> GetOrderListOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getOrderByCustomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                OrderID = row["OrderID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString()),
            }).ToList();
        }

        public static async Task<List<DW_DC_Order>> GetSettlementOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getSettlementForCustomer_By_ORGCollection_No";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                PeriodID = row["PeriodID"].ToString(),
                SettlementDate = DateTime.Parse(row["SettlementDate"].ToString()),
                ActualSettlementDate = DateTime.Parse(row["ActualSettlementDate"].ToString()),
                SettlementAmount = Double.Parse(row["SettlementAmount"].ToString()),
                ConfirmAmount = Double.Parse(row["ConfirmAmount"].ToString()),
                OrgPaymentDate = DateTime.Parse(row["OrgPaymentDate"].ToString()),
                PaymentDate = DateTime.Parse(row["PaymentDate"].ToString()),
                PayInPlan = Double.Parse(row["PayInPlan"].ToString()),
                PayOutPlan = Double.Parse(row["PayOutPlan"].ToString()),
                RepayNote = row["RepayNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                ORGCollection = row["ORGCollection"].ToString(),
                SendSMS = row["SendSMS"].ToString(),
                BankToolID = row["BankToolID"].ToString(),
                Remain = Double.Parse(row["Remain"].ToString())
            }).ToList();
        }


        public static async Task<List<DW_DC_Order>> GetSettlementOfOrganization(string OrganizationID)
        {
            string PROCEDURE = "p_getSettlementForOrganization";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = OrganizationID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                PeriodID = row["PeriodID"].ToString(),
                SettlementDate = DateTime.Parse(row["SettlementDate"].ToString()),
                ActualSettlementDate = DateTime.Parse(row["ActualSettlementDate"].ToString()),
                SettlementAmount = Double.Parse(row["SettlementAmount"].ToString()),
                ConfirmAmount = Double.Parse(row["ConfirmAmount"].ToString()),
                OrgPaymentDate = DateTime.Parse(row["OrgPaymentDate"].ToString()),
                PaymentDate = DateTime.Parse(row["PaymentDate"].ToString()),
                PayInPlan = Double.Parse(row["PayInPlan"].ToString()),
                PayOutPlan = Double.Parse(row["PayOutPlan"].ToString()),
                RepayNote = row["RepayNote"].ToString(),
                OPSNote = row["OPSNote"].ToString(),
                //ORGCollection = row["ORGCollection"].ToString(),
                //SendSMS = row["SendSMS"].ToString(),
                //BankToolID = row["BankToolID"].ToString(),
                Remain = Double.Parse(row["Remain"].ToString())
            }).ToList();
        }



        public double Remain { get; set; }


        private string _type = String.Empty;
        public string Type { get { return _type; } set { this._type = value; } }

        private string _xTxnId = String.Empty;
        public string XTxnID { get { return _xTxnId; } set { this._xTxnId = value; } }

        private string _xAccountId = String.Empty;
        public string XAccountID { get { return _xAccountId; } set { this._xAccountId = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }


        public static List<DW_DC_Order> GetAirtimeOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getAirtimeTransactionByCutomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                Type = row["Type"].ToString(),
                XTxnID = row["XTxnID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }

        public static async Task<List<DW_DC_Order>> GetCashAdvanceOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getCashAdvanceTransactionByCutomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                Type = row["Type"].ToString(),
                XTxnID = row["XTxnID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }

        public static async Task<List<DW_DC_Order>> GetDepositOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getDepositTransactionByCutomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DW_DC_Order
            {
                Type = row["Type"].ToString(),
                XTxnID = row["XTxnID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }

        public static List<DW_DC_Order> GetDiscountOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getDiscountTransactionByCutomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                Type = row["Type"].ToString(),
                XTxnID = row["XTxnID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }

        public static List<DW_DC_Order> GetUnpayOfCustomer(string customerID)
        {
            string PROCEDURE = "p_getUnpayTransactionByCutomer";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = customerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                Type = row["Type"].ToString(),
                XTxnID = row["XTxnID"].ToString(),
                XAccountID = row["XAccountID"].ToString(),
                Amount = Double.Parse(row["Amount"].ToString()),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }


        public string MSIN { get; set; }
        public string ID { get; set; }
        public static List<DW_DC_Order> GetUnpayOfCustomerForTeleSale(string customerID)
        {
            string PROCEDURE = "Select Top 15 A.CreatedAt,ShippingType,B.Name As ItemName,B.MSIN,ISNULL(A.OrderID,'') AS OrderID,ISNULL(A.OrderID,'') + '-' + B.MSIN AS ID,A.[Status],A.Amount,A.ModifiedAt from DW_DC_Order A left join DW_DC_Order_Item B On a.OrderID = B.OrderID where CustomerID = '" + customerID + "' AND A.Status ='completed' Order by A.CreatedAt Desc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DW_DC_Order
            {
                CreatedAt = DateTime.Parse(row["CreatedAt"].ToString()),
                ShippingType = row["ShippingType"].ToString(),
                ItemName = row["ItemName"].ToString(),
                MSIN = row["MSIN"].ToString(),
                OrderID = row["OrderID"].ToString(),
                ID = row["ID"].ToString(),
                Status = row["Status"].ToString(),
                Amount = double.Parse(row["Amount"].ToString()),
                ModifiedAt = DateTime.Parse(row["ModifiedAt"].ToString())
            }).ToList();
        }
    }
}