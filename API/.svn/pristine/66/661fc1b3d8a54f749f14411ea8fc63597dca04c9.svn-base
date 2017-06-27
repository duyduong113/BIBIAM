using BIBIAM.Core.Data.Providers;
using BIBIAM.Core.Entities;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_Product_Hierarchy_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    List<SqlParameter> listpara = new List<SqlParameter>();
                    string spName = "p_Get_Merchant_Product_Hirerchy";
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (spName != null)
                    {
                        foreach (SqlParameter para in new List<SqlParameter>())
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataTable ReadByMerchantID(string MerchantID, string connectionString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    List<SqlParameter> listpara = new List<SqlParameter>();
                    string spName = "p_Get_Merchant_Product_Hirerchy_By_MerchantID";
                    SqlCommand command = con.CreateCommand();
                    command.Parameters.Add("@MerchantID", SqlDbType.NVarChar).Value = MerchantID;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (spName != null)
                    {
                        foreach (SqlParameter para in new List<SqlParameter>())
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public string UpSert(List<Merchant_Product_Hierarchy> list, string UserName, string ma_gian_hang, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        foreach (Merchant_Product_Hierarchy row in list)
                        {
                            var exit = db.FirstOrDefault<Merchant_Product_Hierarchy>(s => s.id == row.id);
                            if (exit != null)
                            {
                                row.ngay_tao = exit.ngay_tao;
                                row.nguoi_tao = exit.nguoi_tao;
                                row.ngay_cap_nhat = DateTime.Now;
                                row.nguoi_cap_nhat = UserName;
                                db.Update(EmptyIfNull(row));
                            }
                            else
                            {
                                row.trang_thai = AllConstant.trang_thai.DANG_SU_DUNG;
                                row.ngay_tao = DateTime.Now;
                                row.ngay_cap_nhat = DateTime.Parse("1900-01-01");
                                row.nguoi_tao = UserName;
                                row.nguoi_cap_nhat = "";
                                row.ma_gian_hang = ma_gian_hang;
                                db.Insert(EmptyIfNull(row));
                            }
                        }
                        dbTrans.Commit();
                        return "true";
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        return e.Message.ToString();
                    }
                }
            }
        }
        private Merchant_Product_Hierarchy EmptyIfNull(Merchant_Product_Hierarchy p)
        {
            p.ma_cay_phan_cap_1 = String.IsNullOrEmpty(p.ma_cay_phan_cap_1) ? "" : p.ma_cay_phan_cap_1;
            p.ma_cay_phan_cap_2 = String.IsNullOrEmpty(p.ma_cay_phan_cap_2) ? "" : p.ma_cay_phan_cap_2;
            p.ma_cay_phan_cap_3 = String.IsNullOrEmpty(p.ma_cay_phan_cap_3) ? "" : p.ma_cay_phan_cap_3;
            p.ma_cay_phan_cap_4 = String.IsNullOrEmpty(p.ma_cay_phan_cap_4) ? "" : p.ma_cay_phan_cap_4;
            p.ma_cay_phan_cap_5 = String.IsNullOrEmpty(p.ma_cay_phan_cap_5) ? "" : p.ma_cay_phan_cap_5;
            p.ma_cay_phan_cap_6 = String.IsNullOrEmpty(p.ma_cay_phan_cap_6) ? "" : p.ma_cay_phan_cap_6;
            p.ma_cay_phan_cap_7 = String.IsNullOrEmpty(p.ma_cay_phan_cap_7) ? "" : p.ma_cay_phan_cap_7;
            p.ma_cay_phan_cap_8 = String.IsNullOrEmpty(p.ma_cay_phan_cap_8) ? "" : p.ma_cay_phan_cap_8;
            p.ma_cay_phan_cap_9 = String.IsNullOrEmpty(p.ma_cay_phan_cap_9) ? "" : p.ma_cay_phan_cap_9;
            p.ma_cay_phan_cap_10 = String.IsNullOrEmpty(p.ma_cay_phan_cap_10) ? "" : p.ma_cay_phan_cap_10;
            return p;
        }
    }
}
