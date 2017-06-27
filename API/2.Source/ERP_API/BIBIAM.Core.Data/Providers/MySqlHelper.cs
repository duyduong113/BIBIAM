using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BIBIAM.Core.Data.Providers
{
    public class MySqlHelper
    {
        //string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        string connectionString { get; set; }

        public MySqlHelper()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        }

        public MySqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MySqlConnection Connection()
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            return con;
        }

        public MySqlConnection Connection(string connectionString)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            return con;
        }

        public DataTable SelectQuery(string strSQL)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(strSQL, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataTable ExecuteQuery(string spName, List<MySqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = Connection(connectionString))
            {
                try
                {
                    MySqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    if (spName != null)
                    {
                        foreach (MySqlParameter para in listpara)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public int ExecuteNoneQuery(string spName, List<MySqlParameter> listpara)
        {
            int n = -1;
            using (MySqlConnection con = Connection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(spName, con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (MySqlParameter para in listpara)
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

        public int ExecuteNoneQuery(string spName, List<MySqlParameter> listpara, CommandType t)
        {
            int n = -1;
            using (MySqlConnection con = Connection(connectionString))
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(spName, con);
                    command.CommandType = t;
                    command.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (MySqlParameter para in listpara)
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

        public DataTable ExecuteString(string sql, List<MySqlParameter> listpara)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand command = con.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    if (sql != null)
                    {
                        foreach (MySqlParameter para in listpara)
                        {
                            command.Parameters.Add(para);
                        }
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public DataSet ExcuteQueryDataSet(string sp, List<MySqlParameter> listpara)
        {
            DataSet dts = new DataSet();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (listpara != null)
                    {
                        foreach (MySqlParameter para in listpara)
                            cmd.Parameters.Add(para);
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
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
        public static object getValueProcWithParameter(string NameStoreProcedure,MySqlParameter[] param, MySqlConnection connect)
        {
            using (connect)
            {
                object obj = null;
                try
                {
                    MySqlCommand sqlCmd = new MySqlCommand();
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
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string tmp = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return Regex.Replace(tmp, "[^a-zA-Z0-9_ -]+", "", RegexOptions.Compiled).Replace(",", "").Replace(" ", "-").Replace("--", "-").ToLower();
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
