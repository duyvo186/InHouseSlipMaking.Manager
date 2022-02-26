using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Xml.Serialization;

using log4net;
using InHouseSlipMaking.Utilities;

namespace InHouseSlipMaking.Domain
{
    [Table(Name = WorkingTimeRecodeColumn.TABLE_NAME)]
    public class WorkingTimeRecode : BaseDomain<WorkingTimeRecode>, ICommonFunctions<WorkingTimeRecode>
    {
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(WorkingTimeRecode).Name);

        #region Private Properties
        private string _RECEIVEING_NUM;

        private string _WORK_ITEM;

        private string _WOKER_CD;

        private string _WOKER_NM;

        private System.DateTime _WORK_DATE;

        private decimal _WORKING_HOURS;

        private System.DateTime _UPD_DT;

        private System.Data.Linq.Binary _TIME_STAMP;
        #endregion
        #region Public Properties
        [Column(Name = WorkingTimeRecodeColumn.RECEIVEING_NUM, Storage = "_RECEIVEING_NUM", DbType = "NVarChar(12) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string RECEIVEING_NUM
        {
            get
            {
                return this._RECEIVEING_NUM;
            }
            set
            {
                if ((this._RECEIVEING_NUM != value))
                {
                    this._RECEIVEING_NUM = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.WORK_ITEM, Storage = "_WORK_ITEM", DbType = "NVarChar(10) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string WORK_ITEM
        {
            get
            {
                return this._WORK_ITEM;
            }
            set
            {
                if ((this._WORK_ITEM != value))
                {
                    this._WORK_ITEM = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.WOKER_CD, Storage = "_WOKER_CD", DbType = "NVarChar(3) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        public string WOKER_CD
        {
            get
            {
                return this._WOKER_CD;
            }
            set
            {
                if ((this._WOKER_CD != value))
                {
                    this._WOKER_CD = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.WOKER_NM, Storage = "_WOKER_NM", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
        public string WOKER_NM
        {
            get
            {
                return this._WOKER_NM;
            }
            set
            {
                if ((this._WOKER_NM != value))
                {
                    this._WOKER_NM = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.WORK_DATE, Storage = "_WORK_DATE", DbType = "SmallDateTime NOT NULL", IsPrimaryKey = true)]
        public System.DateTime WORK_DATE
        {
            get
            {
                return this._WORK_DATE;
            }
            set
            {
                if ((this._WORK_DATE != value))
                {
                    this._WORK_DATE = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.WORKING_HOURS, Storage = "_WORKING_HOURS", DbType = "Decimal(4,0) NOT NULL")]
        public decimal WORKING_HOURS
        {
            get
            {
                return this._WORKING_HOURS;
            }
            set
            {
                if ((this._WORKING_HOURS != value))
                {
                    this._WORKING_HOURS = value;
                }
            }
        }

        [Column(Name = WorkingTimeRecodeColumn.UPD_DT, Storage = "_UPD_DT", DbType = "SmallDateTime NOT NULL")]
        public System.DateTime UPD_DT
        {
            get
            {
                return this._UPD_DT;
            }
            set
            {
                if ((this._UPD_DT != value))
                {
                    this._UPD_DT = value;
                }
            }
        }
        [XmlIgnore]
        [Column(Name = WorkingTimeRecodeColumn.TIME_STAMP, Storage = "_TimeStamp", AutoSync = AutoSync.Always, DbType = "rowversion", CanBeNull = true, IsDbGenerated = true, IsVersion = true)]
        public System.Data.Linq.Binary TimeStamp
        {
            get
            {
                return this._TimeStamp;
            }
            set
            {
                if ((this._TimeStamp != value))
                {
                    this._TimeStamp = value;
                }
            }
        }
        #endregion

        #region ICommonFunctions<WorkingTimeRecode> Members

        public WorkingTimeRecode GetByPrimaryKey()
        {
            WorkingTimeRecode result;
            return result = GetTable().SingleOrDefault(p => p.RECEIVEING_NUM.Equals(this.RECEIVEING_NUM)&&p.WORK_ITEM.Equals(this.WORK_ITEM)&&p.WOKER_CD.Equals(this.WOKER_CD)&&p.WORK_DATE.Equals(this.WORK_DATE));
        }

        #endregion


        public static List<WorkingTimeRecode> GetByReceiveingNumber(string Key)
        {
            try
            {
                logger.Debug("begin GetByReceiveingNumber: " + Key);
                var result = from i in GetTable()
                             where i.RECEIVEING_NUM == Key
                             select i;
                return result.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Error when GetByReceiveingNumber: ", ex);
                throw ex;
            }
        }



        public static void UpdateListWorkingTimeRecode(List<WorkingTimeRecode> ListItem)
        {
            try
            {
                logger.Debug("begin UpdateListWorkingTimeRecode ");
                using (DBContext db = new DBContext())
                {
                    using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                    {
                        try
                        {
                            foreach (WorkingTimeRecode temp in ListItem)
                            {
                                temp.UPD_DT = DateTime.Now;
                                temp.WORKING_HOURS = (decimal)((int)temp.WORKING_HOURS);
                                db.Update<WorkingTimeRecode>(temp);
                            }

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                logger.Debug("end UpdateListWorkingTimeRecode ");
            }
            catch (Exception ex)
            {
                logger.Error("Error when UpdateListWorkingTimeRecode: ", ex);
                throw ex;
            }
        }

        public static void UpdateOrInsertWorkingTimeRecode(WorkingTimeRecode Item)
        {
            try
            {
                logger.Debug("begin UpdateOrInsertWorkingTimeRecode ");
                using (DBContext db = new DBContext())
                {
                    using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                    {
                        try
                        {
                            Item.UPD_DT = DateTime.Now;
                            Item.WORKING_HOURS = (decimal)((int)Item.WORKING_HOURS);

                            var result = from i in GetTable()
                                         where i.RECEIVEING_NUM == Item.RECEIVEING_NUM && i.WOKER_CD == Item.WOKER_CD
                                         && i.WORK_DATE.Date == Item.WORK_DATE.Date && i.WORK_ITEM == Item.WORK_ITEM
                                         select i;
                            if (result.FirstOrDefault() != null )
                            {
                                db.Update<WorkingTimeRecode>(Item);
                            }
                            else
                            {
                                db.Insert<WorkingTimeRecode>(Item);
                            }

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                logger.Debug("end UpdateOrInsertWorkingTimeRecode ");
            }
            catch (Exception ex)
            {
                logger.Error("Error when UpdateOrInsertWorkingTimeRecode: ", ex);
                throw ex;
            }
        }

        public static void DeleteWorkingTimeRecode(WorkingTimeRecode Item)
        {
            try
            {
                logger.Debug("begin DeleteWorkingTimeRecode ");
                using (DBContext db = new DBContext())
                {
                    using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                    {
                        try
                        {
                            var result = from i in GetTable()
                                         where i.RECEIVEING_NUM == Item.RECEIVEING_NUM && i.WOKER_CD == Item.WOKER_CD
                                         && i.WORK_DATE.Date == Item.WORK_DATE.Date && i.WORK_ITEM == Item.WORK_ITEM
                                         select i;
                            if (result.FirstOrDefault() != null)
                            {
                                db.Delete<WorkingTimeRecode>(Item);
                            }
                            else
                            {
                                logger.Debug("Cannot find that Item to Delete");
                                return;
                            }

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
                logger.Debug("end DeleteWorkingTimeRecode ");
            }
            catch (Exception ex)
            {
                logger.Error("Error when DeleteWorkingTimeRecode: ", ex);
                throw ex;
            }
        }

        public static string GetPersonInChargeNameOf(string ReceivingInfoNumber, string WorkItemName)
        {
            logger.Debug("Begin GetPersonInChargeNameOf ");
            List<WorkingTimeRecode> ListItem = WorkingTimeRecode.GetByReceiveingNumber(ReceivingInfoNumber);
            

            List<WorkingTimeRecode> ListWorkItem = ListItem.Where(i => i.WORK_ITEM == WorkItemName).Select(i => i).ToList();
            Dictionary<string, decimal> DictCheck = new Dictionary<string, decimal>();

            foreach (WorkingTimeRecode item in ListWorkItem)
            {
                if (DictCheck.Keys.Contains(item.WOKER_CD))
                {
                    DictCheck[item.WOKER_CD] += item.WORKING_HOURS;
                }
                else
                {
                    DictCheck.Add(item.WOKER_CD, item.WORKING_HOURS);
                }
            }
            ;

            string NameReturn = ListItem.Where(k => k.WOKER_CD == DictCheck.Where(i => i.Value == DictCheck.Max(j => j.Value)).Select(i => i).FirstOrDefault().Key).FirstOrDefault().WOKER_NM;
            logger.Debug("Return value = " + NameReturn);
            return NameReturn;
        }

        public static string GetPersonInChargeNameOf(List<WorkingTimeRecode> list, string WorkItemName)
        {
            var q = from t in
                        (from t in list
                         group t by new { t.WORK_ITEM, t.WOKER_CD } into gr1
                         select new
                         {
                             gr1.Key.WORK_ITEM,
                             gr1.Key.WOKER_CD,
                             WorkerName = gr1.First().WOKER_NM,
                             total = gr1.Sum(r => r.WORKING_HOURS)

                         })
                    group t by new { t.WORK_ITEM } into gr
                    let first = gr.First(p2 => p2.total == gr.Max(p1 => p1.total))
                    select new { first.WORK_ITEM, first.WorkerName, first.total };

            var a = q.Where(i => i.WORK_ITEM == WorkItemName).FirstOrDefault();

            if (a != null)
            {
                return a.WorkerName;
            }

            return string.Empty;
        }

        public static List<Common.CodeNameValue> GetListWorkerAndHours(string WorkItem, List<WorkingTimeRecode> listWorkingTimeRecodeSameReceivingInfo)
        {
            List<Common.CodeNameValue> ReturnList = new List<Common.CodeNameValue>();
            List<string> ListWorkerForEachWorkItem = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == WorkItem).Select(t => t.WOKER_CD).Distinct().ToList();

            //total of 発注 is  ’方案' + '発注’ total?
            if (WorkItem == "発注")
            {
                List<string> ListTemp = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "方案").Select(t => t.WOKER_CD).Distinct().ToList();
                ListWorkerForEachWorkItem.AddRange(ListTemp);
                ListWorkerForEachWorkItem = ListWorkerForEachWorkItem.Distinct().ToList();
            }
            else if (WorkItem == "材料取り")
            {
                List<string> ListTemp = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "木取り").Select(t => t.WOKER_CD).Distinct().ToList();
                ListWorkerForEachWorkItem.AddRange(ListTemp);
                ListWorkerForEachWorkItem = ListWorkerForEachWorkItem.Distinct().ToList();
            }

            foreach (string WorkerCode in ListWorkerForEachWorkItem)
            {
                decimal TotalTimeEachWorker = 0;
                TotalTimeEachWorker = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();
                if (WorkItem == "発注")
                {
                    TotalTimeEachWorker += listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "方案" && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();
                }
                else if (WorkItem == "材料取り")
                {
                    TotalTimeEachWorker += listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "木取り" && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();
                }

               // string Name = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).First().WOKER_NM;


                string Name = "";
                WorkingTimeRecode tempWorkingTime = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).FirstOrDefault();
                if (tempWorkingTime != null)
                    Name = tempWorkingTime.WOKER_NM;
                else
                {
                    try
                    {
                        if (WorkItem == "発注")
                        {
                            Name = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "方案" && t.WOKER_CD == WorkerCode).First().WOKER_NM;
                        }
                        else if (WorkItem == "材料取り")
                        {
                            Name = listWorkingTimeRecodeSameReceivingInfo.Where(t => t.WORK_ITEM == "木取り" && t.WOKER_CD == WorkerCode).First().WOKER_NM;
                        }
                    }
                    catch { }
                }
                ReturnList.Add(new Common.CodeNameValue(WorkerCode, Name, TotalTimeEachWorker));
            }

            return ReturnList;
        }


        public static decimal GetManHourOfReceivingNumber(string Key)
        {
            try
            {
                logger.Debug("begin GetManHourOfReceivingNumber: " + Key);
                var result = from i in GetTable()
                             where i.RECEIVEING_NUM == Key
                             select i;
                var returnResult = result.ToList();

                if (returnResult == null)
                {
                    return 0;
                }

                return returnResult.Sum(i => i.WORKING_HOURS);
            }
            catch (Exception ex)
            {
                logger.Error("Error when GetManHourOfReceivingNumber: ", ex);
                throw ex;
            }
        }

    }
}
