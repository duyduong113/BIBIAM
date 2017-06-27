using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ERPAPD.Models
{
    public class DC_GetCustomerResignedList
    {
        #region Member
        private string _customerID = String.Empty;
        private DateTime _resignedDate;
        private DateTime _informedDate;
        private string _resignedReason = String.Empty;
        private string _note = String.Empty;
        private string _lastActionStatus = String.Empty;
        private string _lastUpdatedTime = String.Empty;
        private string _bD = String.Empty;
        private string _organizationID = String.Empty;
        private string _fullName = String.Empty;
        private double _creditLimit;
        private double _settledBalance;
        private string _currentStatus = String.Empty;
        private string _gender = String.Empty;
        private string _groupID = String.Empty;
        private string _mobilePhone = String.Empty;
        private string _email = String.Empty;
        public string LastUpdatedTime
        {
            get
            {
                return this._lastUpdatedTime;
            }
            set
            {
                this._lastUpdatedTime = value;
            }
        }

        public string FakeCustomerID
        {
            get
            {
                return this._fakeCustomerID;
            }
            set
            {
                this._fakeCustomerID = value;
            }
        }

        public int Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
            }
        }

        public string KeyCustomerID
        {
            get
            {
                return this._keyCustomerID;
            }
            set
            {
                this._keyCustomerID = value;
            }
        }

        [Required]
        public string CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                this._customerID = value;
            }
        }

        [Required]
        public DateTime ResignedDate
        {
            get
            {
                return this._resignedDate;
            }
            set
            {
                this._resignedDate = value;
            }
        }
        [Required]
        public DateTime InformedDate
        {
            get
            {
                return this._informedDate;
            }
            set
            {
                this._informedDate = value;
            }
        }
        [Required]
        public string ResignedReason
        {
            get
            {
                return this._resignedReason;
            }
            set
            {
                this._resignedReason = value;
            }
        }

        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        public string LastActionStatus
        {
            get
            {
                return this._lastActionStatus;
            }
            set
            {
                this._lastActionStatus = value;
            }
        }

        public string BD
        {
            get
            {
                return this._bD;
            }
            set
            {
                this._bD = value;
            }
        }

        public string OrganizationID
        {
            get
            {
                return this._organizationID;
            }
            set
            {
                this._organizationID = value;
            }
        }

        public string FullName
        {
            get
            {
                return this._fullName;
            }
            set
            {
                this._fullName = value;
            }
        }

        public double CreditLimit
        {
            get
            {
                return this._creditLimit;
            }
            set
            {
                this._creditLimit = value;
            }
        }


        public double SettledBalance
        {
            get
            {
                return this._settledBalance;
            }
            set
            {
                this._settledBalance = value;
            }
        }

        public string CurrentStatus
        {
            get
            {
                return this._currentStatus;
            }
            set
            {
                this._currentStatus = value;
            }
        }

        public string Gender
        {
            get
            {
                return this._gender;
            }
            set
            {
                this._gender = value;
            }
        }

        public string GroupID
        {
            get
            {
                return this._groupID;
            }
            set
            {
                this._groupID = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                return this._mobilePhone;
            }
            set
            {
                this._mobilePhone = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public bool Active { get; set; }

        private string _importNote = String.Empty;
        public string ImportNote { get { return _importNote; } set { this._importNote = value; } }
        public string Address { get; set; }

        //partial
        private string _fakeCustomerID = String.Empty;
        private string _keyCustomerID = String.Empty;
        private int _duration;

        public DateTime DayOverdue { get; set; }
        public string Reference { get; set; }

        public string LastActionCode { get; set; }
        public string AddedProfile { get; set; }

        public string LastNote { get; set; }
        public int DurationDebt { get; set; }

        public double BaselineSettledBalance { get; set; }
        #endregion


        public static List<DC_GetCustomerResignedList> getCustomerID()
        {
            string procedure = "SELECT CustomerID FROM DW_DC_Customer";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                CustomerID = row["CustomerID"].ToString(),
            }).ToList();
        }
        public static List<DC_GetCustomerResignedList> getCustomerResignedList()
        {
            string procedure = "p_DC_GetCustomerResignedList";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
                BaselineSettledBalance = Double.Parse(row["BaselineSettledBalance"].ToString()),
            }).ToList();
        }
        public static async Task<List<DC_GetCustomerResignedList>> getCustomerResignedListDynamic(string WhereCondition)
        {
            string procedure = "p_DC_GetCustomerResignedListDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            DataSet ds = await SqlHelperAsync.ExecuteDatasetAsync(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return ds.Tables[0].AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
                BaselineSettledBalance = Double.Parse(row["BaselineSettledBalance"].ToString()),
                CollectionNotes = row["CollectionNotes"].ToString(),
                BDE = row["BDE"].ToString(),
            }).ToList();
        }

        public static List<DC_GetCustomerResignedList> CheckBaselineSettledBalance(string CustomerID)
        {
            string procedure = "p_CheckBaselineSettledBalance";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CustomerID";
            parameters[i].Value = CustomerID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                CustomerID = row["CustomerID"].ToString(),
            }).ToList();
        }
        public static List<DC_GetCustomerResignedList> getRemindDayCustomerResignedList()
        {
            string procedure = "p_GetDataRemindForDay";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                //FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
            }).ToList();
        }

        public static List<DC_GetCustomerResignedList> getRemindDayCustomerResignedListDynamic(string WhereCondition)
        {
            string procedure = "p_GetDataRemindForDayDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                //FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
            }).ToList();
        }

        public static List<DC_GetCustomerResignedList> getRemindMinuteCustomerResignedList()
        {
            string procedure = "p_GetDataRemindForMinute";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
            }).ToList();
        }
        public static List<DC_GetCustomerResignedList> getRemindMinuteCustomerResignedListDynamic(string WhereCondition)
        {
            string procedure = "p_GetDataRemindForMinuteDynamic";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, procedure, parameters);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                //FakeCustomerID = row["CustomerID"].ToString(),
                KeyCustomerID = Helpers.Locdau.convertToUnSign3(row["CustomerID"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                ResignedDate = DateTime.Parse(row["ResignedDate"].ToString()),
                InformedDate = DateTime.Parse(row["InformedDate"].ToString()),
                ResignedReason = row["ResignedReason"].ToString(),
                Note = row["Note"].ToString(),
                Reference = row["Reference"].ToString(),
                Active = Convert.ToBoolean(row["Active"]),
                LastActionStatus = row["LastActionStatus"].ToString(),
                LastUpdatedTime = row["LastUpdatedTime"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = Double.Parse(row["CreditLimit"].ToString()),
                SettledBalance = Double.Parse(row["SettledBalance"].ToString()),
                CurrentStatus = row["CurrentStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                GroupID = row["GroupID"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Duration = (DateTime.Parse(row["InformedDate"].ToString()) - DateTime.Parse(row["ResignedDate"].ToString())).Days,
                DayOverdue = DateTime.Parse(row["Days Overdue"].ToString()),
                LastActionCode = row["ActionCode"].ToString(),
                AddedProfile = row["AddedProfile"].ToString(),
                LastNote = row["LastNote"].ToString(),
                countFile = CountFile(row["CustomerID"].ToString()),
                DurationDebt = Convert.ToInt16(row["DurationDebt"].ToString()),
            }).ToList();
        }

        public int countFile { get; set; }
        public static int CountFile(string id)
        {
            try
            {
                string pathForSaving = HttpContext.Current.Server.MapPath("~/UploadCustomerAttackFile/" + id.Replace(":", "-"));
                int totalfile = 0; ;
                if (Directory.Exists(pathForSaving))
                {
                    totalfile = Directory.GetFiles(pathForSaving).Count();
                }
                return totalfile;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    result = false;
                }
            }
            return result;
        }

        public static List<DC_GetCustomerResignedList> getListOrg()
        {
            string procedure = "SELECT OrganizationID FROM DW_DC_Organization";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DC_GetCustomerResignedList> getListBD()
        {
            string procedure = "SELECT DISTINCT Value FROM DC_Organization_Meta WHERE Factor = 'BD'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                BD = row["Value"].ToString(),
            }).ToList();
        }

        public static List<DC_GetCustomerResignedList> getListCustomer()
        {
            string procedure = "SELECT DISTINCT CustomerID FROM DW_DC_Customer";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, procedure, null);
            return dt.AsEnumerable().Select(row => new DC_GetCustomerResignedList
            {
                CustomerID = row["CustomerID"].ToString(),
            }).ToList();
        }

        public string CollectionNotes { get; set; }

        public string BDE { get; set; }
    }
}