using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class SpendingByMonth
    {
        private string _month;
        private double _totalSpending;
        public string Month
        {
            get
            {
                return this._month;
            }
            set
            {
                this._month = value;
            }
        }

        public double TotalSpending
        {
            get
            {
                return this._totalSpending;
            }
            set
            {
                this._totalSpending = value;
            }
        }

        public static List<SpendingByMonth> GetAllSpedingbyMonth()
        {
            string PROCEDURE = "p_DC_Dashboard_SpendingByMonth";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new SpendingByMonth
            {
                Month = row["CompletedMonth"].ToString(),
                TotalSpending = Double.Parse(row["TotalSpending"].ToString())
            }).ToList();
        }
    }
}