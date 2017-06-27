using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a IXOrderFulfillmentTable.
    /// </summary>
    public class IXOrderFulfillmentTable
    {
        #region Members

        public string ID { get; set; }


        public string OrderID { get; set; }


        public string MSIN { get; set; }


        public string iCareCenter { get; set; }


        public string ItemName { get; set; }


        public string Customer { get; set; }


        public string CustomerName { get; set; }


        public string MobilePhone { get; set; }


        public string OrgDeliveryNote { get; set; }


        public string OrgCity { get; set; }


        public string OrgProvince { get; set; }


        public string OrgAddress { get; set; }


        public string ShippingMobile { get; set; }


        public string FullName { get; set; }


        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }


        public string CustomerNote { get; set; }


        public string OrganizationID { get; set; }


        public string BD { get; set; }


        public int Quantity { get; set; }


        public double UnitPrice { get; set; }


        public double Amount { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime ModifiedAt { get; set; }


        public string Status { get; set; }


        public int KPI { get; set; }


        public string DeniedColor { get; set; }


        public DateTime LastUpdateAt { get; set; }


        public string LastUpdateBy { get; set; }


        public double Total { get; set; }


        public double ActualTotal { get; set; }


        public string VipColor { get; set; }


        public double Duration { get; set; }


        public string Alert { get; set; }


        public string Color { get; set; }


        public string Group { get; set; }


        public string StatusColor { get; set; }


        public DateTime BBBGHHPrintAt { get; set; }


        public string BBBGHHLastPrintUser { get; set; }


        public DateTime BBBGHHLastPrintAt { get; set; }


        public string BBBGHHPrintID { get; set; }


        public string AssignedTo { get; set; }


        public DateTime AssignedDate { get; set; }


        public string OPStatus { get; set; }


        public string OPNote { get; set; }


        public string Merchant { get; set; }


        public string OPStatusColor { get; set; }


        public string PONumber { get; set; }


        public string Buyer { get; set; }


        public string ActualSupplier { get; set; }


        public string PickupLocation { get; set; }


        public DateTime Deliverydate { get; set; }


        public string Deliveryby { get; set; }


        public string Unit { get; set; }


        public string MPColor { get; set; }


        public double BuyingPrice { get; set; }


        public double ExtraExpense { get; set; }


        public string ExtraExpenseNote { get; set; }


        public string MPNote { get; set; }


        public string Promotion { get; set; }


        public double CashAdvance { get; set; }


        public string AdvanceBy { get; set; }


        public DateTime AdvanceDate { get; set; }


        public DateTime PickupDate { get; set; }


        public string MPStatus { get; set; }


        public string MPStatusColor { get; set; }


        public string MPReasonBack { get; set; }


        public DateTime ReOpenDate { get; set; }


        public double Profit { get; set; }


        public string ProfitColor { get; set; }


        public string StockAvailable { get; set; }


        public string SMer { get; set; }


        public string SMStatusColor { get; set; }


        public string PickupMan { get; set; }


        public string LTNote { get; set; }


        public string LTStatus { get; set; }


        public string LTStatusColor { get; set; }


        public string LTReasonBack { get; set; }


        public string StockInNumber { get; set; }


        public string Invoice { get; set; }
        public DateTime InvoiceDate { get; set; }


        public string WHName { get; set; }


        public string WHLocation { get; set; }


        public DateTime StockinDate { get; set; }


        public string WHActualSupplier { get; set; }


        public string WHCode { get; set; }


        public int ActualQty { get; set; }


        public double ActualBuyingPrice { get; set; }


        public string WHNote { get; set; }


        public string WHStatus { get; set; }


        public string WHStatusColor { get; set; }


        public string Packing { get; set; }


        public string PackingNote { get; set; }


        public string PackingColor { get; set; }


        public string StockOut { get; set; }


        public string StockOutType { get; set; }


        public string StockOutTypeColor { get; set; }


        public DateTime StockOutDate { get; set; }


        public string StockOutNote { get; set; }


        public string Recipient { get; set; }


        public string EstimateDeliveryDateColor { get; set; }


        public int OrderBy { get; set; }


        public double Fee { get; set; }


        public string FeeNote { get; set; }


        public string DLNote { get; set; }


        public string DLStatus { get; set; }


        public string DLStatusColor { get; set; }


        public string DLReasonBack { get; set; }


        public DateTime ActualDeliveryDate { get; set; }


        public string STWHAddress { get; set; }


        public string STSupplierAddress { get; set; }


        public string STStockInBy { get; set; }


        public DateTime StockInLastPrintAt { get; set; }


        public string StockInLastPrintUser { get; set; }


        public string StockOutWHName { get; set; }


        public string StockOutAddress { get; set; }


        public string StockOutReceiverName { get; set; }


        public string StockOutReceiverAddress { get; set; }


        public string StockOutBy { get; set; }


        public DateTime StockOutLastPrintAt { get; set; }


        public string StockOutLastPrintUser { get; set; }


        public string Step { get; set; }


        public string RPONumber { get; set; }


        public string PSINumber { get; set; }

        #endregion

        public IXOrderFulfillmentTable()
        { }

        public static int UpdateInvoice(string id, string invoice)
        {
            string Text = "UPDATE IXOrderFulfillmentTable SET Invoice = '" + invoice + "' WHERE ID = '" + id + "'";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, Text, null);
        }

        public static int UpdateValue(string id, string Name, string value)
        {
            string Text = "UPDATE IXOrderFulfillmentTable SET " + Name + " = '" + value + "' WHERE ID = '" + id + "'";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, Text, null);
        }


        public int Update()
        {
            string PROCEDURE = "p_UpdateIXOrderFulfillmentTable";
            SqlParameter[] parameters = new SqlParameter[123];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderID";
            parameters[i].Value = this.OrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MSIN";
            parameters[i].Value = this.MSIN;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@iCareCenter";
            parameters[i].Value = this.iCareCenter;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ItemName";
            parameters[i].Value = this.ItemName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Customer";
            parameters[i].Value = this.Customer;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerName";
            parameters[i].Value = this.CustomerName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MobilePhone";
            parameters[i].Value = this.MobilePhone;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgDeliveryNote";
            parameters[i].Value = this.OrgDeliveryNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgCity";
            parameters[i].Value = this.OrgCity;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgProvince";
            parameters[i].Value = this.OrgProvince;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrgAddress";
            parameters[i].Value = this.OrgAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingMobile";
            parameters[i].Value = this.ShippingMobile;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FullName";
            parameters[i].Value = this.FullName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ShippingAddress";
            parameters[i].Value = this.ShippingAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerNote";
            parameters[i].Value = this.CustomerNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrganizationID";
            parameters[i].Value = this.OrganizationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BD";
            parameters[i].Value = this.BD;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Quantity";
            parameters[i].Value = this.Quantity;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UnitPrice";
            parameters[i].Value = this.UnitPrice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Amount";
            parameters[i].Value = this.Amount;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedAt";
            parameters[i].Value = this.CreatedAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ModifiedAt";
            parameters[i].Value = this.ModifiedAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPI";
            parameters[i].Value = this.KPI;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DeniedColor";
            parameters[i].Value = this.DeniedColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdateAt";
            parameters[i].Value = this.LastUpdateAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdateBy";
            parameters[i].Value = this.LastUpdateBy;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Total";
            parameters[i].Value = this.Total;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualTotal";
            parameters[i].Value = this.ActualTotal;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@VipColor";
            parameters[i].Value = this.VipColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Duration";
            parameters[i].Value = this.Duration;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Alert";
            parameters[i].Value = this.Alert;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Color";
            parameters[i].Value = this.Color;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Group";
            parameters[i].Value = this.Group;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StatusColor";
            parameters[i].Value = this.StatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BBBGHHPrintAt";
            parameters[i].Value = this.BBBGHHPrintAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BBBGHHLastPrintUser";
            parameters[i].Value = this.BBBGHHLastPrintUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BBBGHHLastPrintAt";
            parameters[i].Value = this.BBBGHHLastPrintAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BBBGHHPrintID";
            parameters[i].Value = this.BBBGHHPrintID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AssignedTo";
            parameters[i].Value = this.AssignedTo;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AssignedDate";
            parameters[i].Value = this.AssignedDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPStatus";
            parameters[i].Value = this.OPStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPNote";
            parameters[i].Value = this.OPNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Merchant";
            parameters[i].Value = this.Merchant;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OPStatusColor";
            parameters[i].Value = this.OPStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PONumber";
            parameters[i].Value = this.PONumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Buyer";
            parameters[i].Value = this.Buyer;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualSupplier";
            parameters[i].Value = this.ActualSupplier;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PickupLocation";
            parameters[i].Value = this.PickupLocation;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Deliverydate";
            parameters[i].Value = this.Deliverydate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Deliveryby";
            parameters[i].Value = this.Deliveryby;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Unit";
            parameters[i].Value = this.Unit;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MPColor";
            parameters[i].Value = this.MPColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@BuyingPrice";
            parameters[i].Value = this.BuyingPrice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ExtraExpense";
            parameters[i].Value = this.ExtraExpense;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ExtraExpenseNote";
            parameters[i].Value = this.ExtraExpenseNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MPNote";
            parameters[i].Value = this.MPNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Promotion";
            parameters[i].Value = this.Promotion;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CashAdvance";
            parameters[i].Value = this.CashAdvance;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AdvanceBy";
            parameters[i].Value = this.AdvanceBy;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AdvanceDate";
            parameters[i].Value = this.AdvanceDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PickupDate";
            parameters[i].Value = this.PickupDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MPStatus";
            parameters[i].Value = this.MPStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MPStatusColor";
            parameters[i].Value = this.MPStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MPReasonBack";
            parameters[i].Value = this.MPReasonBack;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ReOpenDate";
            parameters[i].Value = this.ReOpenDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Profit";
            parameters[i].Value = this.Profit;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ProfitColor";
            parameters[i].Value = this.ProfitColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockAvailable";
            parameters[i].Value = this.StockAvailable;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SMer";
            parameters[i].Value = this.SMer;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SMStatusColor";
            parameters[i].Value = this.SMStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PickupMan";
            parameters[i].Value = this.PickupMan;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LTNote";
            parameters[i].Value = this.LTNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LTStatus";
            parameters[i].Value = this.LTStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LTStatusColor";
            parameters[i].Value = this.LTStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LTReasonBack";
            parameters[i].Value = this.LTReasonBack;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockInNumber";
            parameters[i].Value = this.StockInNumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Invoice";
            parameters[i].Value = this.Invoice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@InvoiceDate";
            parameters[i].Value = this.InvoiceDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHName";
            parameters[i].Value = this.WHName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHLocation";
            parameters[i].Value = this.WHLocation;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockinDate";
            parameters[i].Value = this.StockinDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHActualSupplier";
            parameters[i].Value = this.WHActualSupplier;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHCode";
            parameters[i].Value = this.WHCode;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualQty";
            parameters[i].Value = this.ActualQty;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualBuyingPrice";
            parameters[i].Value = this.ActualBuyingPrice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHNote";
            parameters[i].Value = this.WHNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHStatus";
            parameters[i].Value = this.WHStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WHStatusColor";
            parameters[i].Value = this.WHStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Packing";
            parameters[i].Value = this.Packing;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PackingNote";
            parameters[i].Value = this.PackingNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PackingColor";
            parameters[i].Value = this.PackingColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOut";
            parameters[i].Value = this.StockOut;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutType";
            parameters[i].Value = this.StockOutType;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutTypeColor";
            parameters[i].Value = this.StockOutTypeColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutDate";
            parameters[i].Value = this.StockOutDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutNote";
            parameters[i].Value = this.StockOutNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Recipient";
            parameters[i].Value = this.Recipient;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EstimateDeliveryDateColor";
            parameters[i].Value = this.EstimateDeliveryDateColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderBy";
            parameters[i].Value = this.OrderBy;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fee";
            parameters[i].Value = this.Fee;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@FeeNote";
            parameters[i].Value = this.FeeNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DLNote";
            parameters[i].Value = this.DLNote;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DLStatus";
            parameters[i].Value = this.DLStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DLStatusColor";
            parameters[i].Value = this.DLStatusColor;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DLReasonBack";
            parameters[i].Value = this.DLReasonBack;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ActualDeliveryDate";
            parameters[i].Value = this.ActualDeliveryDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@STWHAddress";
            parameters[i].Value = this.STWHAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@STSupplierAddress";
            parameters[i].Value = this.STSupplierAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@STStockInBy";
            parameters[i].Value = this.STStockInBy;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockInLastPrintAt";
            parameters[i].Value = this.StockInLastPrintAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockInLastPrintUser";
            parameters[i].Value = this.StockInLastPrintUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutWHName";
            parameters[i].Value = this.StockOutWHName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutAddress";
            parameters[i].Value = this.StockOutAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutReceiverName";
            parameters[i].Value = this.StockOutReceiverName;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutReceiverAddress";
            parameters[i].Value = this.StockOutReceiverAddress;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutBy";
            parameters[i].Value = this.StockOutBy;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutLastPrintAt";
            parameters[i].Value = this.StockOutLastPrintAt;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StockOutLastPrintUser";
            parameters[i].Value = this.StockOutLastPrintUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Step";
            parameters[i].Value = this.Step;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RPONumber";
            parameters[i].Value = this.RPONumber;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PSINumber";
            parameters[i].Value = this.PSINumber;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }



        public static IXOrderFulfillmentTable GetIXOrderFulfillmentTable(string OrderID, string MSIN)
        {
            string PROCEDURE = "p_SelectIXOrderFulfillmentTable";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@OrderID";
            parameters[0].Value = OrderID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@MSIN";
            parameters[1].Value = MSIN;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new IXOrderFulfillmentTable
            {
                ID = row["ID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                iCareCenter = row["iCareCenter"].ToString(),
                ItemName = row["ItemName"].ToString(),
                Customer = row["Customer"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                OrgDeliveryNote = row["OrgDeliveryNote"].ToString(),
                OrgCity = row["OrgCity"].ToString(),
                OrgProvince = row["OrgProvince"].ToString(),
                OrgAddress = row["OrgAddress"].ToString(),
                ShippingMobile = row["ShippingMobile"].ToString(),
                FullName = row["FullName"].ToString(),
                ShippingAddress = row["ShippingAddress"].ToString(),
                CustomerNote = row["CustomerNote"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                BD = row["BD"].ToString(),
                Quantity = Convert.ToInt16(row["Quantity"].ToString()),
                UnitPrice = Convert.ToDouble(row["UnitPrice"].ToString()),
                Amount = Convert.ToDouble(row["Amount"].ToString()),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
                Status = row["Status"].ToString(),
                KPI = Convert.ToInt16(row["KPI"].ToString()),
                DeniedColor = row["DeniedColor"].ToString(),
                LastUpdateAt = Convert.ToDateTime(row["LastUpdateAt"].ToString()),
                LastUpdateBy = row["LastUpdateBy"].ToString(),
                Total = Convert.ToDouble(row["Total"].ToString()),
                ActualTotal = Convert.ToDouble(row["ActualTotal"].ToString()),
                VipColor = row["VipColor"].ToString(),
                Duration = Convert.ToDouble(row["Duration"].ToString()),
                Alert = row["Alert"].ToString(),
                Color = row["Color"].ToString(),
                Group = row["Group"].ToString(),
                StatusColor = row["StatusColor"].ToString(),
                BBBGHHPrintAt = Convert.ToDateTime(row["BBBGHHPrintAt"].ToString()),
                BBBGHHLastPrintUser = row["BBBGHHLastPrintUser"].ToString(),
                BBBGHHLastPrintAt = Convert.ToDateTime(row["BBBGHHLastPrintAt"].ToString()),
                BBBGHHPrintID = row["BBBGHHPrintID"].ToString(),
                AssignedTo = row["AssignedTo"].ToString(),
                AssignedDate = Convert.ToDateTime(row["AssignedDate"].ToString()),
                OPStatus = row["OPStatus"].ToString(),
                OPNote = row["OPNote"].ToString(),
                Merchant = row["Merchant"].ToString(),
                OPStatusColor = row["OPStatusColor"].ToString(),
                PONumber = row["PONumber"].ToString(),
                Buyer = row["Buyer"].ToString(),
                ActualSupplier = row["ActualSupplier"].ToString(),
                PickupLocation = row["PickupLocation"].ToString(),
                Deliverydate = Convert.ToDateTime(row["Deliverydate"].ToString()),
                Deliveryby = row["Deliveryby"].ToString(),
                Unit = row["Unit"].ToString(),
                MPColor = row["MPColor"].ToString(),
                BuyingPrice = Convert.ToDouble(row["BuyingPrice"].ToString()),
                ExtraExpense = Convert.ToDouble(row["ExtraExpense"].ToString()),
                ExtraExpenseNote = row["ExtraExpenseNote"].ToString(),
                MPNote = row["MPNote"].ToString(),
                Promotion = row["Promotion"].ToString(),
                CashAdvance = Convert.ToDouble(row["CashAdvance"].ToString()),
                AdvanceBy = row["AdvanceBy"].ToString(),
                AdvanceDate = Convert.ToDateTime(row["AdvanceDate"].ToString()),
                PickupDate = Convert.ToDateTime(row["PickupDate"].ToString()),
                MPStatus = row["MPStatus"].ToString(),
                MPStatusColor = row["MPStatusColor"].ToString(),
                MPReasonBack = row["MPReasonBack"].ToString(),
                ReOpenDate = Convert.ToDateTime(row["ReOpenDate"].ToString()),
                Profit = Convert.ToDouble(row["Profit"].ToString()),
                ProfitColor = row["ProfitColor"].ToString(),
                StockAvailable = row["StockAvailable"].ToString(),
                SMer = row["SMer"].ToString(),
                SMStatusColor = row["SMStatusColor"].ToString(),
                PickupMan = row["PickupMan"].ToString(),
                LTNote = row["LTNote"].ToString(),
                LTStatus = row["LTStatus"].ToString(),
                LTStatusColor = row["LTStatusColor"].ToString(),
                LTReasonBack = row["LTReasonBack"].ToString(),
                StockInNumber = row["StockInNumber"].ToString(),
                Invoice = row["Invoice"].ToString(),
                // InvoiceDate = DateTime.Parse(row["InvoiceDate"].ToString()),
                WHName = row["WHName"].ToString(),
                WHLocation = row["WHLocation"].ToString(),
                StockinDate = Convert.ToDateTime(row["StockinDate"].ToString()),
                WHActualSupplier = row["WHActualSupplier"].ToString(),
                WHCode = row["WHCode"].ToString(),
                ActualQty = Convert.ToInt16(row["ActualQty"].ToString()),
                ActualBuyingPrice = Convert.ToDouble(row["ActualBuyingPrice"].ToString()),
                WHNote = row["WHNote"].ToString(),
                WHStatus = row["WHStatus"].ToString(),
                WHStatusColor = row["WHStatusColor"].ToString(),
                Packing = row["Packing"].ToString(),
                PackingNote = row["PackingNote"].ToString(),
                PackingColor = row["PackingColor"].ToString(),
                StockOut = row["StockOut"].ToString(),
                StockOutType = row["StockOutType"].ToString(),
                StockOutTypeColor = row["StockOutTypeColor"].ToString(),
                StockOutDate = Convert.ToDateTime(row["StockOutDate"].ToString()),
                StockOutNote = row["StockOutNote"].ToString(),
                Recipient = row["Recipient"].ToString(),
                EstimateDeliveryDateColor = row["EstimateDeliveryDateColor"].ToString(),
                OrderBy = Convert.ToInt16(row["OrderBy"].ToString()),
                Fee = Convert.ToDouble(row["Fee"].ToString()),
                FeeNote = row["FeeNote"].ToString(),
                DLNote = row["DLNote"].ToString(),
                DLStatus = row["DLStatus"].ToString(),
                DLStatusColor = row["DLStatusColor"].ToString(),
                DLReasonBack = row["DLReasonBack"].ToString(),
                ActualDeliveryDate = Convert.ToDateTime(row["ActualDeliveryDate"].ToString()),
                STWHAddress = row["STWHAddress"].ToString(),
                STSupplierAddress = row["STSupplierAddress"].ToString(),
                STStockInBy = row["STStockInBy"].ToString(),
                StockInLastPrintAt = Convert.ToDateTime(row["StockInLastPrintAt"].ToString()),
                StockInLastPrintUser = row["StockInLastPrintUser"].ToString(),
                StockOutWHName = row["StockOutWHName"].ToString(),
                StockOutAddress = row["StockOutAddress"].ToString(),
                StockOutReceiverName = row["StockOutReceiverName"].ToString(),
                StockOutReceiverAddress = row["StockOutReceiverAddress"].ToString(),
                StockOutBy = row["StockOutBy"].ToString(),
                StockOutLastPrintAt = Convert.ToDateTime(row["StockOutLastPrintAt"].ToString()),
                StockOutLastPrintUser = row["StockOutLastPrintUser"].ToString(),
                Step = row["Step"].ToString(),
                RPONumber = row["RPONumber"].ToString(),
                PSINumber = row["PSINumber"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static IXOrderFulfillmentTable GetIXOrderFulfillmentTable_Tracking(string ID)
        {
            string PROCEDURE = "select * from IXOrderFulfillmentTable where ID='" + ID.Replace("''", "'") + "'";


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new IXOrderFulfillmentTable
            {
                ID = row["ID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Customer = row["Customer"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ItemName = row["ItemName"].ToString(),
                WHActualSupplier = row["WHActualSupplier"].ToString(),
                WHName = row["WHName"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"].ToString()),
                ModifiedAt = Convert.ToDateTime(row["ModifiedAt"].ToString()),
            }).ToList().FirstOrDefault();
        }


        public static List<IXOrderFulfillmentTable> GetShippingCity()
        {
            //string PROCEDURE = "SELECT DISTINCT BD FROM vw_OrganizationRaw WHERE BD <> ''";
            // Modify CanhLV Query 31/07/2014
            string PROCEDURE = "SELECT CityName AS ShippingCity FROM MCA_City";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new IXOrderFulfillmentTable
            {
                ShippingCity = row["ShippingCity"].ToString()
            }).ToList();
        }


        public static List<IXOrderFulfillmentTable> GetWHLocation()
        {
            //string PROCEDURE = "SELECT DISTINCT BD FROM vw_OrganizationRaw WHERE BD <> ''";
            // Modify CanhLV Query 31/07/2014
            string PROCEDURE = "SELECT distinct WHLocation FROM IXOrderFulfillmentTable ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new IXOrderFulfillmentTable
            {
                WHLocation = row["WHLocation"].ToString()
            }).ToList();
        }
    }
}
