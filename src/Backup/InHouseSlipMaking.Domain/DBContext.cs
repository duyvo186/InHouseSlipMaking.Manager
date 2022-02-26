using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using log4net;

using InHouseSlipMaking.Utilities;


namespace InHouseSlipMaking.Domain
{
    public partial class DBContext : DataContext
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Constant.InHouseSlipMaking_CONNECTIONSTRING] != null ? System.Configuration.ConfigurationManager.ConnectionStrings[Constant.InHouseSlipMaking_CONNECTIONSTRING].ConnectionString : null;

        private bool isDispose = false;

        public bool IsDispose
        {
            get
            {
                return isDispose;
            }
        }

        private static DBContext commonDBContext = null;

        public DBContext()
            : base(connectionString)
        {

        }

        public DBContext(string connection)
            : base(connection)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.isDispose = true;
        }

        public System.Data.Common.DbTransaction UseTransaction()
        {
            this.Connection.Open();
            this.CommandTimeout = 3600;

            System.Data.Common.DbTransaction tran = this.Connection.BeginTransaction();
            this.Transaction = tran;

            return tran;
        }

        #region Solve DataContext Problem

        //Change Delete 
        //public void Delete<T>(T item) where T : class
        //{
        //    try
        //    {             
        //        Table<T> table = this.GetTable<T>();
        //        table.Attach(item as T);
        //        table.DeleteOnSubmit(item as T);
        //        this.SubmitChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }           

        //}

        public void Delete<T>(T item) where T : class, ICommonFunctions<T>
        {
            try
            {
                Table<T> table = this.GetTable<T>();
                item = item.GetByPrimaryKey();
                table.Attach(item as T);
                table.DeleteOnSubmit(item as T);
                this.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Insert<T>(T item) where T : class, ICommonFunctions<T>
        {
            try
            {
                //item.SetDefaultValueWhenInsertUpdate();
                Table<T> table = this.GetTable<T>();

                table.InsertOnSubmit(item as T);
                this.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertAll<T>(List<T> list) where T : class, ICommonFunctions<T>
        {
            try
            {
                if (list != null)
                {
                    //foreach (var item in list)                    
                    //    item.SetDefaultValueWhenInsertUpdate();
                    Table<T> table = this.GetTable<T>();

                    table.InsertAllOnSubmit(list);
                    this.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update<T>(T item) where T : class, ICommonFunctions<T>
        {
            try
            {
                //item.SetDefaultValueWhenInsertUpdate();
                T t = item.GetByPrimaryKey();
                GenericUtil.ShallowCopy<T>(item, t);

                this.GetTable<T>().Attach(t as T, true);
                this.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
