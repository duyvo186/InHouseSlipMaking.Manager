using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using InHouseSlipMaking.Utilities;
using InHouseSlipMaking.Domain;

namespace InHouseSlipMaking.Manager
{
    public class ExportSlipExcel : BaseExcel
    {
        Excel.Range CADStartDate, CarType, Category, Company, CompleteDate, DataNumber, DeliveryDate, PersonInCharge, KyuzuLine1, KyuzuLine2, KyuzuLine3,
                    PersonInChargeForSales, ProductName, ProductNumber, ReceivingNumber, Size, TotalWorkTime, TypeOfModel, Weight, ManufactureStartDate, NumberOfKata,
                    WorkItem1A, WorkItem2A, WorkItem3A, WorkItem4A, WorkItem5A, WorkItem6A, WorkItem7A, WorkItem8A, WorkItem9A, WorkItem10A, WorkItem11A,
                    WorkItem1B, WorkItem2B, WorkItem3B, WorkItem4B, WorkItem5B, WorkItem6B, WorkItem7B, WorkItem8B, WorkItem9B, WorkItem10B, WorkItem11B,
                    WorkItem1C, WorkItem2C, WorkItem3C, WorkItem4C, WorkItem5C, WorkItem6C, WorkItem7C, WorkItem8C, WorkItem9C, WorkItem10C, WorkItem11C,
                    WorkItem1D, WorkItem2D, WorkItem3D, WorkItem4D, WorkItem5D, WorkItem6D, WorkItem7D, WorkItem8D, WorkItem9D, WorkItem10D, WorkItem11D;


        protected override void GetRange()
        {
            logger.Info("Begin: GetRange() ");
            CADStartDate = _excel.GetRange(_book, "CADStartDate");
            CarType = _excel.GetRange(_book, "CarType");
            Category = _excel.GetRange(_book, "Category");
            Company = _excel.GetRange(_book, "Company");
            CompleteDate = _excel.GetRange(_book, "CompleteDate");
            DataNumber = _excel.GetRange(_book, "DataNumber");
            DeliveryDate = _excel.GetRange(_book, "DeliveryDate");
            PersonInCharge = _excel.GetRange(_book, "PersonInCharge");
            PersonInChargeForSales = _excel.GetRange(_book, "PersonInChargeForSales");
            ProductName = _excel.GetRange(_book, "ProductName");
            ProductNumber = _excel.GetRange(_book, "ProductNumber");
            ReceivingNumber = _excel.GetRange(_book, "ReceivingNumber");
            Size = _excel.GetRange(_book, "Size");
            TotalWorkTime = _excel.GetRange(_book, "TotalWorkTime");
            TypeOfModel = _excel.GetRange(_book, "TypeOfModel");
            Weight = _excel.GetRange(_book, "Weight");
            ManufactureStartDate = _excel.GetRange(_book, "ManufactureStartDate");
            KyuzuLine1 = _excel.GetRange(_book, "KyuzuLine1");
            KyuzuLine2 = _excel.GetRange(_book, "KyuzuLine2");
            KyuzuLine3 = _excel.GetRange(_book, "KyuzuLine3");
            //NumberOfKata = _excel.GetRange(_book, "NumberOfKata");


            WorkItem1A = _excel.GetRange(_book, "WorkItem1A");
            WorkItem2A = _excel.GetRange(_book, "WorkItem2A");
            WorkItem3A = _excel.GetRange(_book, "WorkItem3A");
            WorkItem4A = _excel.GetRange(_book, "WorkItem4A");
            WorkItem5A = _excel.GetRange(_book, "WorkItem5A");
            WorkItem6A = _excel.GetRange(_book, "WorkItem6A");
            WorkItem7A = _excel.GetRange(_book, "WorkItem7A");
            WorkItem8A = _excel.GetRange(_book, "WorkItem8A");
            WorkItem9A = _excel.GetRange(_book, "WorkItem9A");
            WorkItem10A = _excel.GetRange(_book, "WorkItem10A");
            WorkItem11A = _excel.GetRange(_book, "WorkItem11A");

            WorkItem1B = _excel.GetRange(_book, "WorkItem1B");
            WorkItem2B = _excel.GetRange(_book, "WorkItem2B");
            WorkItem3B = _excel.GetRange(_book, "WorkItem3B");
            WorkItem4B = _excel.GetRange(_book, "WorkItem4B");
            WorkItem5B = _excel.GetRange(_book, "WorkItem5B");
            WorkItem6B = _excel.GetRange(_book, "WorkItem6B");
            WorkItem7B = _excel.GetRange(_book, "WorkItem7B");
            WorkItem8B = _excel.GetRange(_book, "WorkItem8B");
            WorkItem9B = _excel.GetRange(_book, "WorkItem9B");
            WorkItem10B = _excel.GetRange(_book, "WorkItem10B");
            WorkItem11B = _excel.GetRange(_book, "WorkItem11B");

            WorkItem1C = _excel.GetRange(_book, "WorkItem1C");
            WorkItem2C = _excel.GetRange(_book, "WorkItem2C");
            WorkItem3C = _excel.GetRange(_book, "WorkItem3C");
            WorkItem4C = _excel.GetRange(_book, "WorkItem4C");
            WorkItem5C = _excel.GetRange(_book, "WorkItem5C");
            WorkItem6C = _excel.GetRange(_book, "WorkItem6C");
            WorkItem7C = _excel.GetRange(_book, "WorkItem7C");
            WorkItem8C = _excel.GetRange(_book, "WorkItem8C");
            WorkItem9C = _excel.GetRange(_book, "WorkItem9C");
            WorkItem10C = _excel.GetRange(_book, "WorkItem10C");
            WorkItem11C = _excel.GetRange(_book, "WorkItem11C");

            WorkItem1D = _excel.GetRange(_book, "WorkItem1D");
            WorkItem2D = _excel.GetRange(_book, "WorkItem2D");
            WorkItem3D = _excel.GetRange(_book, "WorkItem3D");
            WorkItem4D = _excel.GetRange(_book, "WorkItem4D");
            WorkItem5D = _excel.GetRange(_book, "WorkItem5D");
            WorkItem6D = _excel.GetRange(_book, "WorkItem6D");
            WorkItem7D = _excel.GetRange(_book, "WorkItem7D");
            WorkItem8D = _excel.GetRange(_book, "WorkItem8D");
            WorkItem9D = _excel.GetRange(_book, "WorkItem9D");
            WorkItem10D = _excel.GetRange(_book, "WorkItem10D");
            WorkItem11D = _excel.GetRange(_book, "WorkItem11D");
            logger.Info("End: GetRange() ");
        }

        public string ExportExcel(string strTemplateFile,ReceiveingInformation ReceivingInfo, SharePointKyuzuList Kyuzu, List<WorkingTimeRecode> ListWorkTime)
        {
            logger.Debug("Start ExportExcel...");
            string pathFileOutput = "";
            try
            {
                if (!File.Exists(strTemplateFile)) throw new Exception("File " + strTemplateFile + " not exists!");

								
                string strOutLocation = Path.GetTempPath();
                
                pathFileOutput = Path.Combine(strOutLocation, "SlipExcelTemplate.xls");
                logger.Debug("Excel file template " + pathFileOutput);

                if (File.Exists(pathFileOutput))
                    File.Delete(pathFileOutput);
                File.Copy(strTemplateFile, pathFileOutput);
                InitExcel(pathFileOutput);


                FillExcel(ReceivingInfo, Kyuzu, ListWorkTime); // iRowStart = 2 according to the excel template

                _excel.Save(_book, pathFileOutput);
                logger.Debug("End ExportExcel !");
                return pathFileOutput;
            }
            catch (Exception ex)
            {
                logger.Error("Error ExportExcel... ", ex);
                throw ex;
            }
            finally
            {
                _excel.Close(ref _app, ref _book, ref _sheet);
            }
        }

        private void FillExcel()
        {
        }

        private void FillExcel(ReceiveingInformation ReceivingInfo, SharePointKyuzuList Kyuzu, List<WorkingTimeRecode> ListWorkTime)
        {
            try
            {

                _excel.ClearContent(_sheet, 11, 2, 32, 9);


                _excel.SetCell(this._book, ReceivingNumber, ReceivingInfo.RECEIVEING_NUM);
                _excel.SetCell(this._book, Company, Kyuzu.CompanyName);
                _excel.SetCell(this._book, PersonInCharge, Kyuzu.PersonInCharge);
                _excel.SetCell(this._book, PersonInChargeForSales, Kyuzu.PersionInChangeOfSales);
                _excel.SetCell(this._book, ProductNumber, Kyuzu.ProductNumber);
                _excel.SetCell(this._book, ProductName, Kyuzu.ProductName);
                _excel.SetCell(this._book, DeliveryDate, Kyuzu.DeliveryDatePlan);
                _excel.SetCell(this._book, CompleteDate, ReceivingInfo.COMPLETED_DT.HasValue ? ReceivingInfo.COMPLETED_DT.Value.ToShortDateString() : "");
                _excel.SetCell(this._book, CADStartDate, ReceivingInfo.CAD_START_DT.HasValue ? ReceivingInfo.CAD_START_DT.Value.ToShortDateString() : "");
                _excel.SetCell(this._book, ManufactureStartDate, ReceivingInfo.MANUFACTURE_START_DT.HasValue ? ReceivingInfo.MANUFACTURE_START_DT.Value.ToShortDateString() : "");
                _excel.SetCell(this._book, CarType, Kyuzu.TypeOfCar);
                _excel.SetCell(this._book, DataNumber, ReceivingInfo.DATA_NUM);
                _excel.SetCell(this._book, Category, Kyuzu.Categoty);
                _excel.SetCell(this._book, TypeOfModel, Kyuzu.TypeOfModel);
                _excel.SetCell(this._sheet, Weight.Row + 1, Weight.Column,  Kyuzu.NumberOfKata);

                

                string[] StringKyuzu = Kyuzu.Kyuzu.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (string line in StringKyuzu)
                {
                    _excel.SetCell(this._sheet, KyuzuLine1.Row + i, KyuzuLine1.Column, line);
                    i++;
                    if (i == 4)
                        break;
                }

                _excel.SetCell(this._book, Size, ReceivingInfo.SIZE_W + " x " + ReceivingInfo.SIZE_D + " x " + ReceivingInfo.SIZE_H);
                _excel.SetCell(this._book, Weight, ReceivingInfo.WEIGHT + "Kg");

                int CurrentRow = 0;
                decimal TotalAllItemWorkHours = 0;
                foreach (string WorkItem in Common.ListWorkItem)
                {
                    int CurrentCol = 0;
                    decimal SumWorkItemHour = 0;
                    int AddMoreRow = 0;

                    List<Common.CodeNameValue> GetListWorkerAndHours = WorkingTimeRecode.GetListWorkerAndHours(WorkItem, ListWorkTime);

                    foreach (Common.CodeNameValue temp in GetListWorkerAndHours)
                    {
                        if (WorkItem == "NC加工" && CurrentCol == 8 && AddMoreRow == 0)
                        {
                            CurrentRow += 2;
                            AddMoreRow = 1;
                            CurrentCol = 0;
                        }
                        if (WorkItem == "NC加工" && AddMoreRow == 1 && CurrentCol == 8)
                        {
                            SumWorkItemHour += temp.Value.Value;
                        }
                        else
                        {
                            _excel.SetCell(this._sheet, WorkItem1A.Row + CurrentRow, WorkItem1A.Column + CurrentCol, temp.Name);
                            _excel.SetCell(this._sheet, WorkItem1B.Row + CurrentRow, WorkItem1B.Column + CurrentCol, Common.MinutesToHours(temp.Value).Value.ToString("F"));
                            CurrentCol++;
                            SumWorkItemHour += temp.Value.Value;
                        }
                    }



                    if (WorkItem == "NC加工" && AddMoreRow == 0)
                    {
                        CurrentRow += 2;
                        AddMoreRow = 1;
                    }

                    if (WorkItem == "NC加工")
                    {
                        
                        WorkItem5C.Copy(WorkItem6D);
                        WorkItem5D.Copy(WorkItem7C);
                        _excel.ClearCell(this._sheet, WorkItem6C.Row, WorkItem6C.Column);
                        _excel.ClearCell(this._sheet, WorkItem7D.Row, WorkItem7D.Column);
                        WorkItem6C.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
                        WorkItem7D.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlLineStyleNone;


                        _excel.SetCell(this._sheet, WorkItem6D.Row, WorkItem6D.Column, ReceiveingInformation.GetDateTimeForWorkingItem(WorkItem, ReceivingInfo) == null ? "合計" : "合計　☑");
                        _excel.SetCell(this._sheet, WorkItem7C.Row, WorkItem7C.Column, Common.MinutesToHours(SumWorkItemHour).Value.ToString("F"));
                        
                    }
                    else
                    {
                        _excel.SetCell(this._sheet, WorkItem1C.Row + CurrentRow, WorkItem1C.Column, ReceiveingInformation.GetDateTimeForWorkingItem(WorkItem, ReceivingInfo) == null ? "合計" : "合計　☑");
                        _excel.SetCell(this._sheet, WorkItem1D.Row + CurrentRow, WorkItem1D.Column, Common.MinutesToHours(SumWorkItemHour).Value.ToString("F"));
                    }

                    TotalAllItemWorkHours += SumWorkItemHour;
                    CurrentRow += 2;
                }

                _excel.SetCell(this._book, TotalWorkTime, Common.MinutesToHours(TotalAllItemWorkHours).Value.ToString("F"));

            }
            catch (Exception ex)
            {
                logger.Error("Error FillExcel", ex);
                throw ex;
            }
        }

    }
}
