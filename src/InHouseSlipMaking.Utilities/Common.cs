using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace InHouseSlipMaking.Utilities
{
    public class Common
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Configuration config = null;
        private static bool _initLog4net = false;


        #region Log4net
        public static log4net.ILog InitLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                logger.Debug("Init log4net completed");

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return logger;
        }

        private static void ReconfigLogAppender(string _configFile, string refixLogName)
        {
            FileInfo fInfo = new FileInfo(_configFile);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fInfo);
            string appenderNames = "FileAppender";
            log4net.Appender.IAppender[] appenders = logger.Logger.Repository.GetAppenders();
            foreach (log4net.Appender.IAppender appender in appenders)
            {
                if (appenderNames.Equals(appender.Name))
                {
                    log4net.Appender.RollingFileAppender fileAppender = appender as log4net.Appender.RollingFileAppender;
                    fileAppender.File = Path.Combine(Path.GetDirectoryName(fileAppender.File), refixLogName + Path.GetExtension(fileAppender.File));
                    fileAppender.ActivateOptions();
                    break;
                }
            }
        }

        public static log4net.ILog InitLog4Net(string logName)
        {
            CheckConfigRoot();
            if (_initLog4net)
                return logger;
            try
            {
                string _configFile = System.IO.Path.Combine(GetEnviromentConfigPath(), Constant.FILE_CONFIG);
                if (File.Exists(_configFile))
                {
                    if (!_initLog4net)
                    {
                        ReconfigLogAppender(_configFile, logName);
                        logger.Debug(string.Format("Config file path: \"{0}\"", _configFile));
                        _initLog4net = true;
                    }
                }
                else
                    throw new Exception("Do not have config file!!!");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw new Exception(ResourceUtil.Instance.GetString("ERROR_NOT_CONFIG_ROOT"));
            }
            return logger;
        }

        public static void CheckConfigRoot()
        {
            string rootPath = Common.GetEnviromentConfigPath();
            if (string.Empty.Equals(rootPath))
                throw new Exception(ResourceUtil.Instance.GetString("ERROR_NOT_CONFIG_ROOT"));
        }

        #endregion
        #region Config

        //public static void SetWSConnections(DataSourceCollection dsCollection)
        //{
        //    foreach (DataSource ds in dsCollection)
        //    {
        //        DataConnection dc = ds.QueryConnection;
        //        if (dc is WebServiceConnection)
        //        {
        //            WebServiceConnection wsCon = (WebServiceConnection)dc;
        //            wsCon.ServiceUrl = new Uri(GetWebServicesBaseUrl(wsCon.ServiceUrl.AbsoluteUri));
        //            wsCon.Timeout = int.Parse(AppSettingKey(Constant.WS_TIME_OUT));
        //        }
        //    }
        //}

        public static string GetWebServicesBaseUrl(string sourceUrl,string pathSettingFile)
        {
            try
            {
                logger.Debug("Begin GetWebServicesBaseUrl ");
                int index = sourceUrl.LastIndexOf("/");
                Setting setting = new Setting(pathSettingFile);
                string newUrl = setting.WEB_SERVICE_URL;//AppSettingKey(GetPathConfigFile(configName, configPath), Constant.WS_CONFIG);
                logger.Debug("Get WebServiceURL : " + newUrl);
                if (newUrl != "")
                {
                    if (newUrl.EndsWith("/"))
                        newUrl = newUrl.Substring(0, newUrl.Length - 1) + sourceUrl.Substring(index);
                    else
                        newUrl = newUrl + sourceUrl.Substring(index);
                    logger.Debug("Return WebServiceURL : " + newUrl);
                    return newUrl;
                }
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string AppSettingKey(string key)
        {
            try
            {
                logger.Debug("Key: " + key);
                if (config == null)
                {
                    string _configFile = System.IO.Path.Combine(GetEnviromentConfigPath(), Constant.FILE_CONFIG);
                    logger.DebugFormat("config file: {0}", _configFile);
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = _configFile;
                    config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    logger.Debug("Load config completed");
                }
                else
                {
                    logger.Debug("config != null");
                }
                return config.AppSettings.Settings[key].Value;
            }
            catch (Exception ex)
            {
                logger.Error("ERROR AppSettingKey", ex);
                return string.Empty;
            }
        }

        public static string GetEnviromentConfigPath()
        {
            string result = System.Environment.GetEnvironmentVariable(Constant.ENVIROMENT_PATH_CONFIG, EnvironmentVariableTarget.Machine);
            logger.DebugFormat("GetEnviromentConfigPath() => {0}", result);
            return result;
        }

        #endregion


        public static decimal? HoursToMinutes(decimal? Number)
        {
            return (decimal)((int)(((int)Number) * 60 + (Number - (int)Number) * 100));
        }

        public static decimal? MinutesToHours(decimal? Number)
        {
            return (Number % 60) / 100 + (int)(Number / 60);
        }

        public static List<String> ListWorkItem = new List<string>()
        { 

            "発注",    //kawane changed from  方案
            "材料取り", //kawane changed from  "木取り"
            "CAD",
            "CAM",
            "仕様図",
            "NC加工",
            	
            "ワイヤーカット",
            "仕上げ",
            "検査表",
            "検査",

        };

        public struct CodeNameValue
        {
            public string Code;
            public string Name;
            public decimal? Value;
            public CodeNameValue(string code, string name, decimal? value)
            {
                this.Code = code;
                this.Name = name;
                this.Value = value;
            }
        }

    }
    public class Setting
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _WEB_SERVICE_URL;

        public string WEB_SERVICE_URL
        {
            get { return _WEB_SERVICE_URL; }
            private set { _WEB_SERVICE_URL = value; }
        }
        public Setting(string filePath)
        {
            ReadFile(filePath);
        }
        public void ReadFile(string filePath)
        {
            try
            {
                int index = 0;
                object[] obj = new object[10];
                using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS")))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        int indexSpace = line.IndexOf(Constant.EQUAL_CHAR);
                        obj[index++] = line.Substring(indexSpace + 1).Trim();
                    }
                }
                WEB_SERVICE_URL = obj[0].ToString();
                logger.DebugFormat("Get Info LOTMAN_FOLDER = {0}", WEB_SERVICE_URL); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
