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

namespace InHouseSlipMaking.Domain
{
    [Table(Name = ReceiveingInformationColumn.TABLE_NAME)]
    public class ReceiveingInformation : BaseDomain<ReceiveingInformation>, ICommonFunctions<ReceiveingInformation>
    {
        static readonly ILog logger = log4net.LogManager.GetLogger(typeof(ReceiveingInformation));

        #region Private Properties
        private string _RECEIVEING_NUM;

        private string _DATA_NUM;

        private string _SIZE_W;

        private string _SIZE_D;

        private string _SIZE_H;

        private string _WEIGHT;

        private System.Nullable<System.DateTime> _COMPLETED_DT;

        private System.Nullable<System.DateTime> _CAD_START_DT;

        private System.Nullable<System.DateTime> _MANUFACTURE_START_DT;

        private System.Nullable<System.DateTime> _HOUAN_CK_DT;

        private System.Nullable<System.DateTime> _KIBORI_CK_DT;

        private System.Nullable<System.DateTime> _CAD_CK_DT;

        private System.Nullable<System.DateTime> _CAM_CK_DT;

        private System.Nullable<System.DateTime> _SHIYOUZU_CK_DT;

        private System.Nullable<System.DateTime> _NC_CK_DT;

        private System.Nullable<System.DateTime> _WIRECUT_CK_DT;

        private System.Nullable<System.DateTime> _SHIAGE_CK_DT;

        private System.Nullable<System.DateTime> _KENSAHYOU_CK_DT;

        private System.Nullable<System.DateTime> _KENSA_CK_DT;

        private System.Nullable<System.DateTime> _COMPLETED_CK_DT;

        private string _CANCEL_FLAG;

        private string _UPDATE_FLAG;

        private string _UPD_WOKER_CD;

        private System.DateTime _UPD_DT;

        #endregion
        #region Public Properties
        [Column(Name = ReceiveingInformationColumn.RECEIVEING_NUM, Storage = "_RECEIVEING_NUM", DbType = "NVarChar(12) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
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

        [Column(Name = ReceiveingInformationColumn.DATA_NUM, Storage = "_DATA_NUM", DbType = "NVarChar(20)")]
        public string DATA_NUM
        {
            get
            {
                return this._DATA_NUM;
            }
            set
            {
                if ((this._DATA_NUM != value))
                {
                    this._DATA_NUM = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.SIZE_W, Storage = "_SIZE_W", DbType = "NVarChar(10)")]
        public string SIZE_W
        {
            get
            {
                return this._SIZE_W;
            }
            set
            {
                if ((this._SIZE_W != value))
                {
                    this._SIZE_W = value;;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.SIZE_D, Storage = "_SIZE_D", DbType = "NVarChar(10)")]
        public string SIZE_D
        {
            get
            {
                return this._SIZE_D;
            }
            set
            {
                if ((this._SIZE_D != value))
                {
                    this._SIZE_D = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.SIZE_H, Storage = "_SIZE_H", DbType = "NVarChar(10)")]
        public string SIZE_H
        {
            get
            {
                return this._SIZE_H;
            }
            set
            {
                if ((this._SIZE_H != value))
                {
                    this._SIZE_H = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.WEIGHT, Storage = "_WEIGHT", DbType = "NVarChar(10)")]
        public string WEIGHT
        {
            get
            {
                return this._WEIGHT;
            }
            set
            {
                if ((this._WEIGHT != value))
                {
                    this._WEIGHT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.COMPLETED_DT, Storage = "_COMPLETED_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> COMPLETED_DT
        {
            get
            {
                return this._COMPLETED_DT;
            }
            set
            {
                if ((this._COMPLETED_DT != value))
                {
                    this._COMPLETED_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.CAD_START_DT, Storage = "_CAD_START_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> CAD_START_DT
        {
            get
            {
                return this._CAD_START_DT;
            }
            set
            {
                if ((this._CAD_START_DT != value))
                {
                    this._CAD_START_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.MANUFACTURE_START_DT, Storage = "_MANUFACTURE_START_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> MANUFACTURE_START_DT
        {
            get
            {
                return this._MANUFACTURE_START_DT;
            }
            set
            {
                if ((this._MANUFACTURE_START_DT != value))
                {
                    this._MANUFACTURE_START_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.HOUAN_CK_DT, Storage = "_HOUAN_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> HOUAN_CK_DT
        {
            get
            {
                return this._HOUAN_CK_DT;
            }
            set
            {
                if ((this._HOUAN_CK_DT != value))
                {
                    this._HOUAN_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.KIBORI_CK_DT, Storage = "_KIBORI_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> KIBORI_CK_DT
        {
            get
            {
                return this._KIBORI_CK_DT;
            }
            set
            {
                if ((this._KIBORI_CK_DT != value))
                {
                    this._KIBORI_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.CAD_CK_DT, Storage = "_CAD_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> CAD_CK_DT
        {
            get
            {
                return this._CAD_CK_DT;
            }
            set
            {
                if ((this._CAD_CK_DT != value))
                {
                    this._CAD_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.CAM_CK_DT, Storage = "_CAM_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> CAM_CK_DT
        {
            get
            {
                return this._CAM_CK_DT;
            }
            set
            {
                if ((this._CAM_CK_DT != value))
                {
                    this._CAM_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.SHIYOUZU_CK_DT, Storage = "_SHIYOUZU_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> SHIYOUZU_CK_DT
        {
            get
            {
                return this._SHIYOUZU_CK_DT;
            }
            set
            {
                if ((this._SHIYOUZU_CK_DT != value))
                {
                    this._SHIYOUZU_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.NC_CK_DT, Storage = "_NC_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> NC_CK_DT
        {
            get
            {
                return this._NC_CK_DT;
            }
            set
            {
                if ((this._NC_CK_DT != value))
                {
                    this._NC_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.WIRECUT_CK_DT, Storage = "_WIRECUT_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> WIRECUT_CK_DT
        {
            get
            {
                return this._WIRECUT_CK_DT;
            }
            set
            {
                if ((this._WIRECUT_CK_DT != value))
                {
                    this._WIRECUT_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.SHIAGE_CK_DT, Storage = "_SHIAGE_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> SHIAGE_CK_DT
        {
            get
            {
                return this._SHIAGE_CK_DT;
            }
            set
            {
                if ((this._SHIAGE_CK_DT != value))
                {
                    this._SHIAGE_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.KENSAHYOU_CK_DT, Storage = "_KENSAHYOU_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> KENSAHYOU_CK_DT
        {
            get
            {
                return this._KENSAHYOU_CK_DT;
            }
            set
            {
                if ((this._KENSAHYOU_CK_DT != value))
                {
                    this._KENSAHYOU_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.KENSA_CK_DT, Storage = "_KENSA_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> KENSA_CK_DT
        {
            get
            {
                return this._KENSA_CK_DT;
            }
            set
            {
                if ((this._KENSA_CK_DT != value))
                {
                    this._KENSA_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.COMPLETED_CK_DT, Storage = "_COMPLETED_CK_DT", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> COMPLETED_CK_DT
        {
            get
            {
                return this._COMPLETED_CK_DT;
            }
            set
            {
                if ((this._COMPLETED_CK_DT != value))
                {
                    this._COMPLETED_CK_DT = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.CANCEL_FLAG, Storage = "_CANCEL_FLAG", DbType = "NVarChar(1) NOT NULL", CanBeNull = false)]
        public string CANCEL_FLAG
        {
            get
            {
                return this._CANCEL_FLAG;
            }
            set
            {
                if ((this._CANCEL_FLAG != value))
                {
                    this._CANCEL_FLAG = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.UPDATE_FLAG, Storage = "_UPDATE_FLAG", DbType = "NVarChar(1) NOT NULL", CanBeNull = false)]
        public string UPDATE_FLAG
        {
            get
            {
                return this._UPDATE_FLAG;
            }
            set
            {
                if ((this._UPDATE_FLAG != value))
                {
                    this._UPDATE_FLAG = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.UPD_WOKER_CD, Storage = "_UPD_WOKER_CD", DbType = "NVarChar(2)")]
        public string UPD_WOKER_CD
        {
            get
            {
                return this._UPD_WOKER_CD;
            }
            set
            {
                if ((this._UPD_WOKER_CD != value))
                {
                    this._UPD_WOKER_CD = value;
                }
            }
        }

        [Column(Name = ReceiveingInformationColumn.UPD_DT, Storage = "_UPD_DT", DbType = "SmallDateTime NOT NULL")]
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
        [Column(Name = ReceiveingInformationColumn.TIME_STAMP, Storage = "_TimeStamp", AutoSync = AutoSync.Always, DbType = "rowversion", CanBeNull = true, IsDbGenerated = true, IsVersion = true)]
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

        #region ICommonFunctions<ReceiveingInformation> Members

        public ReceiveingInformation GetByPrimaryKey()
        {
            ReceiveingInformation result;
            return result = GetTable().SingleOrDefault(p => p.RECEIVEING_NUM.Equals(this.RECEIVEING_NUM));
        }
        #endregion



        public static List<string> GetAllReceiveingNumber()
        {
            try
            {
                logger.Debug("Begin GetAllReceiveingNumber");
                var result = from i in GetTable()
                             orderby i.RECEIVEING_NUM
                             where i.COMPLETED_CK_DT == null && i.CANCEL_FLAG == "0"
                             select i.RECEIVEING_NUM;
                return result.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Error when GetAllReceiveingNumber: ", ex);
                throw ex;
            }
        }

        public static List<string> GetAllReceiveingNumber1()
        {
            try
            {
                logger.Debug("Begin GetAllReceiveingNumber1");
                var result = from i in GetTable()
                             orderby i.RECEIVEING_NUM
                             where i.COMPLETED_CK_DT == null
                             select i.RECEIVEING_NUM;
                return result.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Error when GetAllReceiveingNumber1: ", ex);
                throw ex;
            }
        }

        public static ReceiveingInformation GetByReceiveingNumber(string Key)
        {
            try
            {
                logger.Debug("Begin GetByReceiveingNumber: " + Key);
                var result = from i in GetTable()
                             where i.RECEIVEING_NUM == Key
                             select i;
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("Error when GetByReceiveingNumber: ", ex);
                throw ex;
            }
        }

        public static void UpdateReceiveingInformation(ReceiveingInformation Item)
        {
            try
            {
                logger.Debug("Begin UpdateReceiveingInformation");
                Item.UPD_DT = DateTime.Now;
                Item.UPDATE_FLAG = "1";

                Item.Update();
                logger.Debug("End UpdateReceiveingInformation");
            }
            catch (Exception ex)
            {
                logger.Error("Error when UpdateReceiveingInformation: ", ex);
                throw ex;
            }
        }

        public static List<ReceiveingInformation> GetListForUpdateSharepoint()
        {
            var result = from i in GetTable()
                         orderby i.RECEIVEING_NUM
                         where i.UPDATE_FLAG == "1"
                         select i;
            return result.ToList();

        }

        

        public static Dictionary<string, ReceiveingInformation> ConvertToDictionary(List<ReceiveingInformation> ListReceiveingInformation)
        {
            Dictionary<string, ReceiveingInformation> ReturnList = new Dictionary<string, ReceiveingInformation>();
            foreach (ReceiveingInformation Item in ListReceiveingInformation)
            {
                ReturnList.Add(Item.RECEIVEING_NUM, Item);
            }

            return ReturnList;

        }

        public static DateTime? GetDateTimeForWorkingItem(string WorkingItem, ReceiveingInformation ReceiveingInformation)
        {
            switch (WorkingItem)
            {
                case "方案":
                case "発注":
                    return ReceiveingInformation.HOUAN_CK_DT;
                case "木取り":
                case "材料取り": 
                    return ReceiveingInformation.KIBORI_CK_DT;
                case "CAD":
                    return ReceiveingInformation.CAD_CK_DT;
                case "CAM":
                    return ReceiveingInformation.CAM_CK_DT;
                case "仕様図":
                    return ReceiveingInformation.SHIYOUZU_CK_DT;
                case "NC加工":
                    return ReceiveingInformation.NC_CK_DT;
                case "ワイヤーカット":
                    return ReceiveingInformation.WIRECUT_CK_DT;
                case "仕上げ":
                    return ReceiveingInformation.SHIAGE_CK_DT;
                case "検査表":
                    return ReceiveingInformation.KENSAHYOU_CK_DT;
                case "検査":
                    return ReceiveingInformation.KENSA_CK_DT;
            }
            return null;
        }

    }
}
