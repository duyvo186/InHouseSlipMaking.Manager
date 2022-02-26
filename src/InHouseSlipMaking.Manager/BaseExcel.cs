using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InHouseSlipMaking.Utilities;

namespace InHouseSlipMaking.Manager
{
    public abstract class BaseExcel
    {
        #region variable
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ExcelManager _excel = new ExcelManager();

        public string SHEET_NAME = "SlipExcel";

        public Excel.Application _app = null;
        public Excel.Workbook _book = null;
        public Excel.Worksheet _sheet = null;
        #endregion

        #region constructor
        ~BaseExcel()
        {
            _sheet = null;
            _book = null;
            _app = null;

        }
        #endregion

        #region public method
        public void InitExcel(string excelFile)
        {
            logger.Info("Begin : void InitExcelfile(string excelFile)");

            try
            {
                if (!File.Exists(excelFile))
                    throw new Exception("File not Exists");

                try
                {
                    _excel.Open(out _app, out _book, excelFile);
                }
                catch (Exception exx)
                {
                    logger.Error(exx);
                    throw new Exception("Init false");
                }

                if (_app == null || _book == null) throw new Exception("EXCEL_IS_NOT_TEMPLATE");

                _sheet = _excel.FindSheet(_book, SHEET_NAME);
                if (_sheet == null) throw new Exception("EXCEL_IS_NOT_TEMPLATE");

                GetRange();
                logger.Debug("Init excel file is OK");
            }
            catch (Exception ex)
            {
                logger.Error("Init export test data templte excel failed", ex);
                throw ex;
            }

            logger.Info("End : void InitExcelfile(string excelFile)");

        }

        protected abstract void GetRange();
        #endregion

    }
}
