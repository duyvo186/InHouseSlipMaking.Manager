using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;

using log4net;
using System.Xml.Serialization;

using InHouseSlipMaking.Utilities;

namespace InHouseSlipMaking.Domain
{
    public class BaseDomain<T> where T : class,  ICommonFunctions<T>
    {
        readonly static ILog logger = LogManager.GetLogger(typeof(T));

        protected string strDeleteFlag = string.Empty;
        protected string strUpdateUser = string.Empty;
        protected Nullable<DateTime> dtUpdateDate = null;

        protected System.Data.Linq.Binary _TimeStamp;

        #region Class Method

        //public string DeleteFlagString
        //{
        //    get 
        //    {
        //        if (!string.IsNullOrEmpty(this.strDeleteFlag) && this.strDeleteFlag.Equals(Constant.DELETE_FLAG_0))
        //            return Constant.DELETE_FLAG_0_STRING;
        //        else
        //            return Constant.DELETE_FLAG_1_STRING;
        //    }
        //    set { }
        //}

        public string Check = string.Empty;

        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }

        public string Serialize()
        {
            string strReturn = "";
            try
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                XmlSerializer ser = new XmlSerializer(typeof(T));
                ser.Serialize(sw, this);
                strReturn = sw.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                //throw ex;
            }

            return strReturn;
        }

        public static Table<T> GetTable()
        {
            DBContext db = new DBContext();
            return db.GetTable<T>();
        }

        public static Table<T> GetTable(DBContext db)
        {
            return db.GetTable<T>();
        }

        public static List<T> GetAll()
        {
            var list = from i in GetTable() select i;
            logger.Debug("list.count: " + list.Count());
            return list.ToList();
        }

        #region New Insert, Delete, Update

        public void Insert()
        {
            SetDefaultValueWhenInsertUpdate();
            DBContext db = new DBContext();
            db.Insert<T>(this as T);
        }

        public void Delete()
        {
            DBContext db = new DBContext();
            db.Delete<T>(this as T);
        }

        public void Update()
        {
            //SetDefaultValueWhenInsertUpdate();
            DBContext db = new DBContext();
            db.Update<T>(this as T);
        }


        public void SetDefaultValueWhenInsertUpdate()
        {
            this.dtUpdateDate = DateTime.Now;
            //string user = Common.GetCurrentUser();
            string user = this.strUpdateUser;
            if (user != null && user.Length > 20)
                user = user.Substring(0, 20);
            this.strUpdateUser = user;
        }

        #endregion

        //public T getByID(int ID)
        //{
        //    DBContext db=new DBContext();
        //    T entity;
        //    entity= db.ExecuteQuery<T>(
        //    return entity;       
        //}
        #endregion        
    }
    public interface ICommonFunctions<T>
    {
        T GetByPrimaryKey();
        //void SetDefaultValueWhenInsertUpdate(); 
    }
}
