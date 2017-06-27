using System;
using System.Configuration;
using log4net;

namespace ERPAPD
{
    public class Constants
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Constants));
        private static Constants m_Constants;
        

        #region Setting from App.config
        public string CONNECTION_STRING;
        public string CONNECTION_STRING1;
        public int CONNECTION_TIMEOUT;
        public int LOG_DB_CALL;
        public static int TIME_ZONE;

        #endregion

        public Constants()
        {
            logger.Debug("Get CONNECTION_STRING");
            CONNECTION_STRING = ConfigurationManager.AppSettings["ERPAPDConnection"].ToString().Trim();
            logger.Debug("Get CONNECTION_TIMEOUT");
            CONNECTION_TIMEOUT = int.Parse(ConfigurationManager.AppSettings["connectionTimeout"].ToString().Trim());
            logger.Debug("Get LOG_DB_CALL");
            LOG_DB_CALL = int.Parse(ConfigurationManager.AppSettings["logDBCall"].ToString().Trim());
            TIME_ZONE = int.Parse(ConfigurationManager.AppSettings["TimeZone"].ToString().Trim());
        }

        public static Constants AllConstants()
        {
            if (m_Constants == null)
            {
                try
                {
                    logger.Debug("Begin initing constants");
                    m_Constants = new Constants();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    System.Environment.Exit(1);
                }
            }
            return m_Constants;
        }

        public static void Refresh()
        {
            if (m_Constants != null)
            {
                logger.Debug("Begin refresh constants");
                m_Constants = null;
                Constants.AllConstants();
                logger.Debug("End refresh constants");
            }
        }

    }
    
}
