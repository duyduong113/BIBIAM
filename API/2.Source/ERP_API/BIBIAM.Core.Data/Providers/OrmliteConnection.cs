using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System;
using System.Configuration;
using System.Data;

namespace BIBIAM.Core.Data.Providers
{
    public class OrmliteConnection
    {
        public IDbConnection openConn()
        {
            var dbFactory = new OrmLiteConnectionFactory(ConfigurationManager.AppSettings["connectionString"].ToString().Trim(), MaxSqlProvider.Instance);
            IDbConnection dbConn = dbFactory.OpenDbConnection();
            OrmLiteConfig.DialectProvider.UseUnicode = true;
            return dbConn;
        }
        public IDbConnection openConn(string connectionString)
        {
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MaxSqlProvider.Instance);
            IDbConnection dbConn = dbFactory.OpenDbConnection();
            OrmLiteConfig.DialectProvider.UseUnicode = true;
            return dbConn;
        }
        public class MaxSqlProvider : SqlServerOrmLiteDialectProvider
        {
            public new static readonly MaxSqlProvider Instance = new MaxSqlProvider();

            public override string GetColumnDefinition(string fieldName, Type fieldType,
                      bool isPrimaryKey, bool autoIncrement, bool isNullable,
                      int? fieldLength, int? scale, string defaultValue)
            {
                var fieldDefinition = base.GetColumnDefinition(fieldName, fieldType,
                                               isPrimaryKey, autoIncrement, isNullable,
                                               fieldLength, scale, defaultValue);

                if (fieldType == typeof(string))
                {
                    if (fieldLength != null)
                    {
                        if (fieldLength > 4000)
                        {
                            var orig = string.Format(StringLengthColumnDefinitionFormat, fieldLength);
                            var max = string.Format(StringLengthColumnDefinitionFormat, "MAX");

                            fieldDefinition = fieldDefinition.Replace(orig, max);
                        }
                        else
                        {
                            fieldDefinition = "\"" + fieldName + "\" NVARCHAR(" + fieldLength + ") NULL";
                        }
                    }
                    else
                    {
                        fieldDefinition = "\"" + fieldName + "\" NVARCHAR(" + 4000 + ") NULL";
                    }
                }

                return fieldDefinition;
            }
        }
    }
}
