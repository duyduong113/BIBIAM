using BIBIAM.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MCC.Helpers
{
    public class SqlHelper
    {
        string connectionString = AppConfigs.MCCConnectionString;
        public SqlConnection Connection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public SqlConnection Connection(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public DataTable SelectQuery(string strSQL)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataTable ExecuteQuery(string spName, List<SqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (spName != null)
                    {
                        foreach (SqlParameter para in listpara)
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

        public int ExecuteNoneQuery(string spName, List<SqlParameter> listpara)
        {
            int n = -1;
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(spName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            command.Parameters.Add(para);
                    }
                    n = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return n;
        }

        public int ExecuteNoneQuery(string spName, List<SqlParameter> listpara, CommandType t)
        {
            int n = -1;
            using (SqlConnection con = Connection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(spName, con);
                    command.CommandType = t;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            command.Parameters.Add(para);
                    }
                    n = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw ex;
                }
            }
            return n;
        }

        public DataTable ExecuteString(string sql, List<SqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    if (sql != null)
                    {
                        foreach (SqlParameter para in listpara)
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

        public DataSet ExcuteQueryDataSet(string sp, List<SqlParameter> listpara)
        {
            DataSet dts = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (SqlParameter para in listpara)
                            cmd.Parameters.Add(para);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dts);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return dts;
        }

        /// <summary>
        /// Thực thi 1 StoreProcedure trả về giá trị
        /// </summary>
        public static object getValueProcWithParameter(string NameStoreProcedure, SqlParameter[] param, SqlConnection connect)
        {
            using (connect)
            {
                object obj = null;
                try
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandTimeout = 2000;
                    sqlCmd.Connection = connect;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = NameStoreProcedure;
                    obj = sqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return obj;
            }
        }

        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
