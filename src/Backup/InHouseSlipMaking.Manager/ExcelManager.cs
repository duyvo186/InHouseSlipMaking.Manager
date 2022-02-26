using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;

using log4net;

namespace InHouseSlipMaking.Manager
{
    public class ExcelManager
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Common
        public void Open(out Excel.Application app, out Excel.Workbook book, string filename)
        {
            logger.Debug("Open Excel file: started -> " + ShowStr(filename));
            try
            {
                int nExcelProcesses = CountExcelProcesses();
                if (nExcelProcesses >= 5)
                {
                    logger.Debug("Total excel process : " + nExcelProcesses.ToString());
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Failed to run GC.Collect to shut down too many Excel processes", ex);
            }

            app = new Excel.ApplicationClass();
            logger.Debug("Init excel app is finished");
            app.Visible = false;
            app.DisplayAlerts = false;
            app.AskToUpdateLinks = false;
            book = app.Workbooks.Add(filename);
            logger.Debug("Open Excel file: finished");
        }

        public void OpenExcelAfterExport(string excelFile, string fileName)
        {
            logger.Debug("Open Excel file for user's editing: started -> " + ShowStr(excelFile) + "    " + ShowStr(fileName));
            Process process = new Process();
            process.StartInfo.FileName = string.Format("\"{0}\"", excelFile);
            process.StartInfo.Arguments = string.Format("\"{0}\"", fileName);
            process.StartInfo.UseShellExecute = true;
            process.Start();
            logger.Debug("Open Excel file for user's editing: finished");
        }

        public void Create(Excel.Application app, Excel.Workbook book, string template)
        {
            logger.Debug("Create new Excel file from template: started -> " + ShowStr(template));
            app = new Excel.ApplicationClass();
            app.Visible = false;
            app.DisplayAlerts = false;
            book = app.Workbooks.Add(template);
            logger.Debug("Create new Excel file from template: finished");
        }

        public void Save(Excel.Workbook book, string filename)
        {
            logger.Debug("Save Excel file as new file: started");
            if (File.Exists(filename))
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                File.Delete(filename);

                logger.Debug("Delete old file");
            }
            //book.Save();
            book.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, false, Type.Missing, Type.Missing);
            logger.Debug("Save Excel file as new file: finished");
        }

        private int CountExcelProcesses()
        {
            Process[] processlist = Process.GetProcesses();
            int count = 0;
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName.ToLower().Equals("excel"))
                    count++;
            }
            return count;
        }

        private void Release(ref Excel.Application obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);

                obj = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
            finally
            {
                int nExcelProcesses = CountExcelProcesses();

                if (nExcelProcesses > 0)
                {
                    logger.Debug("Error: Excel Process can’t be released");
                }

            }
        }

        private void Release(ref Excel.Workbook obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            finally
            {
                obj = null;
            }
        }

        private void Release(ref Excel.Worksheet obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            finally
            {
                obj = null;
            }
        }

        public void Close(ref Excel.Application app, ref Excel.Workbook book, ref Excel.Worksheet sheet)
        {
            if (sheet != null)
                Release(ref sheet);
            Close(ref app, ref book);
        }
        public void Close(ref Excel.Application app, ref Excel.Workbook book, ref Excel.Worksheet sheet1, ref Excel.Worksheet sheet2)
        {
            if (sheet1 != null)
                Release(ref sheet1);
            if (sheet2 != null)
                Release(ref sheet2);

            Close(ref app, ref book);
        }
        public void Close(ref Excel.Application app, ref Excel.Workbook book)
        {
            logger.Debug("Close Excel file: started");
            if (book != null)
            {
                book.Close(false, Type.Missing, Type.Missing);
                Release(ref book);
                logger.Debug("Close workbook and release COM object");
            }
            if (app != null)
            {
                app.Quit();
                Release(ref app);
                logger.Debug("Close application and release COM object");
            }
            logger.Debug("Close Excel file: finished");
        }

        public void Close(ref Excel.Workbook book)
        {
            logger.Debug("Close Excel file: started");
            if (book != null)
            {
                book.Close(false, Type.Missing, Type.Missing);
                Release(ref book);
                logger.Debug("Close workbook and release COM object");
            }

            logger.Debug("Close Excel file: finished");
        }
        public Excel.Worksheet FindSheet(Excel.Workbook book, string sheetName)
        {
            Excel.Worksheet target = null;
            Excel.Worksheet sheet;
            logger.Debug("Find sheet by name: started -> " + ShowStr(sheetName));

            for (int index = 1; index <= book.Worksheets.Count; index++)
            {
                sheet = book.Worksheets[index] as Excel.Worksheet;
                logger.Debug("Check sheet: (" + index.ToString() + ") " + ShowStr(sheet.Name));
                if (sheet.Name.Equals(sheetName))
                {
                    target = sheet;
                    logger.Debug("Found sheet: " + ShowStr(target.Name));
                    break;
                }
            }
            logger.Debug("Find sheet by name: finished");
            return target;
        }

        private string ShowStr(string data)
        {
            return (data == null ? "NULL" : data);
        }

        public Excel.Range GetCell(Excel.Worksheet sheet, int row, int column)
        {
            return sheet.Cells[row, column] as Excel.Range;
        }

        public void SetCell(Excel.Worksheet sheet, int row, int column, object value)
        {
            GetCell(sheet, row, column).Value2 = value;
        }

        public void SetCell(Excel.Workbook workBook, string rangeName, object value)
        {
            try
            {
                GetRange(workBook, rangeName).Value2 = value;
            }
            catch (Exception ex)
            {
                logger.Error("Can't not get range : " + rangeName, ex);
            }
        }

        public void SetCell(Excel.Workbook workBook, Excel.Range range, object value)
        {
            try
            {
                range.Value2 = value;
            }
            catch (Exception ex)
            {
                logger.Error("Can't not get range : " + range.Name, ex);
            }
        }

        public void SetRange(Excel.Worksheet sheet, int rowS, int columnS, int rowE, int columnE, object value)
        {
            GetRange(sheet, rowS, columnS, rowE, columnE).Value2 = value;
        }

        public string GetValue(Excel.Workbook workBook, string rangeName)
        {
            try
            {
                object value = GetRange(workBook, rangeName).Value;
                if (value != null)
                    return value.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Can't not get range : " + rangeName, ex);
            }
            return string.Empty;

        }
        public string GetValue(Excel.Workbook workBook, Excel.Range range)
        {
            try
            {
                object value = range.Value;
                if (value != null)
                    return value.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Can't not get range : " + range.Name, ex);
            }
            return string.Empty;

        }

        public string GetValue(Excel.Worksheet sheet, int row, int col)
        {
            try
            {
                object value = GetCell(sheet, row, col).Value;
                if (value != null)
                    return value.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("Can't not get cell in row : " + row.ToString() + " col :" + col.ToString(), ex);
            }
            return string.Empty;
        }

        public void ClearCell(Excel.Worksheet sheet, int row, int column)
        {
            GetCell(sheet, row, column).ClearContents();
        }

        public void ClearContent(Excel.Worksheet sheet, int rowS, int columnS, int rowE, int columnE)
        {
            GetRange(sheet, rowS, columnS, rowE, columnE).ClearContents();
        }

        public bool IsEmptyCell(Excel.Worksheet sheet, int row, int col)
        {
            Excel.Range cell = GetCell(sheet, row, col);
            if (cell.Value == null || cell.Value.ToString().Equals(string.Empty))
            {
                return true;
            }
            return false;

        }

        public Excel.Range GetRange(Excel.Worksheet sheet, int rowS, int colS, int rowE, int colE)
        {
            return sheet.get_Range(GetCell(sheet, rowS, colS), GetCell(sheet, rowE, colE));
        }

        public Excel.Range GetRange(Excel.Workbook workBook, string name)
        {
            logger.Debug("Begin : Excel.Range GetRange(Excel.Workbook workBook, string name)");

            Excel.Range target = null;
            Excel.Name namedRange;

            logger.Debug("string name : " + name);

            try
            {
                if (workBook != null)
                {
                    logger.Debug("workBook NOT NULL");
                    logger.Debug("workBook.Names.Count : " + workBook.Names.Count);
                }
                else
                {
                    logger.Debug("workBook NULL");
                }

                namedRange = workBook.Names.Item(name, Type.Missing, Type.Missing);

                if (namedRange.RefersToRange == null)
                {
                    logger.Debug("namedRange.RefersToRange NULL");
                }
                else
                {
                    logger.Debug("namedRange.RefersToRange NOT NULL");
                }

                target = namedRange.RefersToRange;

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }

            logger.Debug("End : Excel.Range GetRange(Excel.Workbook workBook, string name)");

            return target;

        }

        public Excel.Range GetRange(Excel.Workbook workBook, Excel.Worksheet sheet, string name)
        {

            Excel.Range target = null;
            Excel.Name namedRange;
            string nameSheet = sheet == workBook.Sheets[1] ? "" : sheet.Name + "!";
            for (int index = 1; index <= workBook.Names.Count; index++)
            {
                namedRange = workBook.Names.Item(index, Type.Missing, Type.Missing);
                if (namedRange.Name == nameSheet + name)
                {
                    target = namedRange.RefersToRange;
                    break;
                }
            }
            return target;

        }

        public Excel.Range GetRow(Excel.Worksheet sheet, int row)
        {
            return sheet.Rows[row, Type.Missing] as Excel.Range;
        }

        public Excel.Range GetRow(Excel.Worksheet sheet, int rowFrom, int rowTo)
        {
            return sheet.Rows[rowFrom + ":" + rowTo, Type.Missing] as Excel.Range;
        }

        public Excel.Range GetColumn(Excel.Worksheet sheet, int col)
        {
            return sheet.Columns[col, Type.Missing] as Excel.Range;
        }

        public Excel.Range GetColumn(Excel.Worksheet sheet, int colFrom, int colTo)
        {
            return sheet.Columns[GetColName(colFrom) + ":" + GetColName(colTo), Type.Missing] as Excel.Range;
        }


        public void InsertRow(Excel.Worksheet sheet, int row)
        {
            Excel.Range range = GetRow(sheet, row);
            range.Insert(true);

        }

        public string GetColName(int col)
        {
            string target = "";

            if (col > 26)
            {
                // Get the first letter
                // ASCII index 65 represent char. 'A'. So, we use 64 in this calculation as A starting point
                char first = Convert.ToChar(64 + ((col - 1) / 26));

                // Get the second letter
                char second = Convert.ToChar(64 + (col % 26 == 0 ? 26 : col % 26));

                // Concat. them
                target = first.ToString() + second.ToString();
            }
            else
            {
                target = Convert.ToChar(64 + col).ToString();
            }
            return target;
        }

        public Excel.Worksheet CopyWorkSheet(Excel.Workbook book, Excel.Worksheet sheet)
        {
            sheet.Copy(Type.Missing, book.Sheets[book.Sheets.Count]);
            return book.Sheets[book.Sheets.Count] as Excel.Worksheet;
        }

        #region Commented for Refer
        /* 
            public void ProtectSheet(Excel.Worksheet sheet)
            {
                sheet.Protect(GeneralMaster.GetExcelLockPwd(), true, true, true, true);
            }

            public void UnProtectSheet(Excel.Worksheet sheet)
            {
                sheet.Unprotect(GeneralMaster.GetExcelLockPwd());
            }

            public void ProtectSheetForDoc(Excel.Worksheet sheet)
            {
                sheet.Protect(GeneralMaster.GetExcelLockPwd(), true, true, true, true);
            }

            public void UnProtectSheetForDoc(Excel.Worksheet sheet)
            {
                sheet.Unprotect(GeneralMaster.GetExcelLockPwd());
            }

            public void ProtectBook(Excel.Workbook workBook)
            {
                string strPwd = GeneralMaster.GetExcelLockPwd();

                foreach (Excel.Worksheet sheet in workBook.Sheets)
                {
                    sheet.Protect(strPwd, true, true, true, true);
                }
            }

            public void UnProtectBook(Excel.Workbook workBook)
            {
                string strPwd = GeneralMaster.GetExcelLockPwd();

                foreach (Excel.Worksheet sheet in workBook.Sheets)
                {
                    sheet.Unprotect(strPwd);
                }
            }
             
            
            public void SetHyberlink(Excel.Worksheet sheetFrom, Excel.Worksheet sheetTo, Excel.Range cellFrom)
            {
                sheetFrom.Hyperlinks.Add(cellFrom, "", "'" + sheetTo.Name + "'!A1", Missing.Value, GetValue(sheetFrom, cellFrom.Row, cellFrom.Column));
            }

        */
        #endregion

        public void SetBackColor(Excel.Range range, Color color)
        {
            range.Interior.ColorIndex = Excel.Constants.xlAutomatic;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public void SetBorder(Excel.Range range, Excel.XlBordersIndex borderIndex, Excel.XlLineStyle lineStyle, Excel.XlBorderWeight weight, Excel.XlColorIndex colorIndex)
        {
            if (colorIndex != Excel.XlColorIndex.xlColorIndexNone)
            {
                range.Borders[borderIndex].LineStyle = lineStyle;
                range.Borders[borderIndex].Weight = weight;
            }
            range.Borders[borderIndex].ColorIndex = colorIndex;
        }

        public void FillDropDown(Excel.Range range, string listValue)
        {
            range.Validation.Delete();
            range.Validation.Add(Excel.XlDVType.xlValidateList, Excel.XlDVAlertStyle.xlValidAlertStop, Excel.XlFormatConditionOperator.xlBetween, listValue, "");
        }

        public void CallMacroFromCode(Excel.Workbook workBook, string marcoName)
        {
            workBook.Application.Run(marcoName, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
        #endregion

        public void DeleteFile(string pathFile)
        {
            try
            {
                if (File.Exists(pathFile))
                    File.Delete(pathFile);
            }
            catch (Exception ex)
            {
                logger.WarnFormat(string.Format("Can not delete {0}", pathFile), ex);
            }
        }
    }

    public class CellValue
    {
        public CellValue() { }
        public CellValue(int row, int col, string value)
        {
            Row = row;
            Column = col;
            Value = value;
        }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Value { get; set; }
    }
}
