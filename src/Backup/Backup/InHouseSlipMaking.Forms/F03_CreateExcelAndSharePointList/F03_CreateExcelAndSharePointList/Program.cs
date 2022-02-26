using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InHouseSlipMaking.Utilities;
using F03_CreateExcelAndSharePointList.Properties;
using log4net;


namespace F03_CreateExcelAndSharePointList
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger("F03_CreateExcelAndSharePointList");
        static void Main(string[] args)
        {
            try
            {                
                createExcelAndUpdateSharePointListWS.RunProcess();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR " + ex.Message);
                logger.Error("F03_CreateExcelAndSharePointList error: ", ex);
                throw ex;
            }
        }
        private static CreateExcelAndUpdateSharePointListWS.CreateExcelAndUpdateSharePointListWS createExcelAndUpdateSharePointListWS
        {
            get
            {
                CreateExcelAndUpdateSharePointListWS.CreateExcelAndUpdateSharePointListWS ws = new CreateExcelAndUpdateSharePointListWS.CreateExcelAndUpdateSharePointListWS();
                ws.UseDefaultCredentials = true;
                ws.Url = Common.GetWebServicesBaseUrl(ws.Url, Settings.Default.PATH_SETTING_FILE);
                logger.Debug("Url WS: " + ws.Url);
                ws.Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings[Constant.WS_TIME_OUT]);
                return ws;
            }
        }
    }
}
