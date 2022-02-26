using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using InHouseSlipMaking.Domain;
using InHouseSlipMaking.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;


/// <summary>
/// Summary description for SharepointWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SharepointWS : System.Web.Services.WebService
{

    private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public SharepointWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    #region Common Fuction

    [WebMethod]
    public static string GetRootUrlSite()
    {
        logger.Debug("Begin : string GetUrlSite(string strEvalYear)");

        string strUrlSite = string.Empty;

        try
        {
            strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);

            logger.Debug("strUrlSite : " + strUrlSite);

        }
        catch (Exception ex)
        {
            logger.Error("Error get strUrlSite... ", ex);
            throw ex;
        }

        logger.Debug("End : string GetUrlSite(string strEvalYear)");

        return strUrlSite;

    }

    public SPWeb OpenSPWebAsAdmin()
    {
        return OpenSPWebAsAdmin(GetRootUrlSite());
    }

    public SPWeb OpenSPWebAsAdmin(string siteUrl)
    {
        SPWeb web = null;
        SPSecurity.RunWithElevatedPrivileges(delegate()
        {
            using (SPSite site = new SPSite(siteUrl))
            {
                web = site.OpenWeb();
            }
        });
        return web;
    }

    public bool ContainsWeb(SPWeb parentWeb, string childWebName)
    {
        logger.DebugFormat("parentWeb.Url = {0}", parentWeb.Url);
        logger.DebugFormat("check childWebName = {0}", childWebName);
        SPWeb childWeb = parentWeb.Webs[childWebName];
        logger.DebugFormat("childWeb.Exists = {0}", childWeb.Exists);
        return childWeb.Exists;
    }

    public SPList GetSPListByName(SPWeb spWeb, string listName)
    {
        foreach (SPList item in spWeb.Lists)
        {
            if (logger.IsDebugEnabled)
                logger.DebugFormat("Title = {0}", item.Title.Trim());
            if (listName.Trim().Equals(item.Title.Trim()))
            {
                logger.DebugFormat("Return item '{0}'", item.Title.Trim());
                return item;
            }
        }
        logger.Debug("GetSPListByName return null");
        return null;
    }

    public SPListItem GetSPListItemByProperties(SPList spList, string property, string value)
    {
        logger.DebugFormat("property = '{0}', value = '{1}'", property, value);
        logger.DebugFormat("spList.Items.Count = '{0}'", spList.Items.Count);
        foreach (SPListItem item in spList.Items)
        {
            //if (logger.IsDebugEnabled)
            //{
            //    logger.DebugFormat("item.Title = {0}", item.Title);
            //    logger.DebugFormat("item.Properties.Count = {0}", item.Fields.Count);
            //    foreach (SPField field in item.Fields)
            //        logger.DebugFormat("field.Title[{0}]", field.Title.ToString());
            //        //logger.DebugFormat("Prop[{0}] = {1}", field.Title.ToString(), item[field.Title].ToString());
            //}


            //logger.DebugFormat("ContainsField('{0}') = '{1}'", property, item.Fields.ContainsField(property));
            if ((item.Fields.ContainsField(property) && item[property] != null) && (value.Trim().Equals(item[property].ToString().Trim())))
            {
                logger.DebugFormat("ContainsField('{0}') = '{1}'", property, item.Fields.ContainsField(property));
                logger.DebugFormat("value = '{0}', item[property] = '{1}', cp = '{2}' ", value, item[property].ToString().Trim(), value.Trim().Equals(item[property].ToString().Trim()));
                return item;
            }
        }
        logger.Debug("end GetSPListItemByProperties");
        return null;
    }

    public void UpdateListItem(SPListItem spListItem, Dictionary<string, object> dictProperties)
    {
        foreach (string key in dictProperties.Keys)
        {
            logger.DebugFormat("dictProperties['{0}'] = '{1}', spListItem.Fields.ContainsField('{0}') = {2}", key, dictProperties[key], spListItem.Fields.ContainsField(key));
            if (spListItem.Fields.ContainsField(key))
                spListItem[spListItem.Fields[key].Id] = dictProperties[key];
        }
        logger.Debug("Call spListItem.Update()");
        spListItem.Update();
    }

    #endregion

    [WebMethod]
    public string TestSharepoint()
    {
        string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_MASTER_ADMIN_URL);

        SPWeb web = OpenSPWebAsAdmin(strUrlSite);

        SPList a = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORKER_LIST));
        SPListItem b = GetSPListItemByProperties(a, "Worker Code", "02");

        return b["Administration Item"].ToString();
    }

    [WebMethod]
    public string GetCurrentUser()
    {
        return this.Context.User.Identity.Name;
    }

    [WebMethod]
    public List<SharePointKyuzuList> GetListKyuzuForInitialInSharepoint(List<string> ListReceiveingNumber)
    {
        try
        {
            logger.Debug("begin GetListKyuzuForInitialInSharepoint");
            List<SharePointKyuzuList> ReturnList = new List<SharePointKyuzuList>();

            string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
            SPWeb web = OpenSPWebAsAdmin(strUrlSite);
            SPList ListKyuzu = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST));

            logger.Debug("get List done, ListKyuzu count: " + ListKyuzu.ItemCount.ToString());

            string property = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER);

            foreach (SPListItem ItemListKyuzu in ListKyuzu.Items)
            {

                if ((ItemListKyuzu.Fields.ContainsField(property) && ItemListKyuzu[property] != null) && (ListReceiveingNumber.Contains(ItemListKyuzu[property].ToString().Trim())))
                {
                    
                    ReturnList.Add(
                             new SharePointKyuzuList()
                             {
                                 ReceiveingNumber = ItemListKyuzu[property].ToString(),
                                 ProductNumber = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NUMBER)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NUMBER)].ToString(),
                                 ProductName = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NAME)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NAME)].ToString(),
                                 TypeOfCar = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CAR_TYPE)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CAR_TYPE)].ToString(),
                                 PersionInChangeOfSales = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHANGE_OF_SALES)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHANGE_OF_SALES)].ToString(),
                                 CompanyName = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_COMPANY_NAME)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_COMPANY_NAME)].ToString(),
                                 TypeOfModel = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_TYPE_OF_MODEL)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_TYPE_OF_MODEL)].ToString(),
                                 PersonInCharge = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHARGE)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHARGE)].ToString(),
                                 Categoty = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CATEGOTY)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CATEGOTY)].ToString(),


                                 DeliveryDatePlan = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_DELIVERY_DATE_PLAN)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_DELIVERY_DATE_PLAN)].ToString(),


                                 NumberOfKata = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_NUMBER_OF_KATA)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_NUMBER_OF_KATA)].ToString(),
                                 Kyuzu = (ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_KYUZU)] == null) ? string.Empty : ItemListKyuzu[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_KYUZU)].ToString(),
                                 Id = ItemListKyuzu.UniqueId
                             });
                    logger.Debug("Recieve Num = " + ItemListKyuzu[property].ToString());
                }
            }
            logger.Debug("end GetListKyuzuForInitialInSharepoint");
            return ReturnList;
        }
        catch (Exception ex)
        {
            logger.Error("GetListKyuzuForInitialInSharepoint error: ", ex);
        }
        return null;
    }

    [WebMethod]
    public bool CheckKyuzuByreceiveingNumber(string receiveingNumber)
    {
        try
        {
            logger.Debug("start CheckKyuzuByreceiveingNumber");
            string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
            SPWeb web = OpenSPWebAsAdmin(strUrlSite);

            SPList ListKyuzu = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST));

            SPListItem ItemListKyuzu = GetSPListItemByProperties(ListKyuzu, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER), receiveingNumber);
            if (ItemListKyuzu != null)
            {
                return true;
            }
            logger.Debug("end GetKyuzuByreceiveingNumber");
        }
        catch (Exception ex)
        {
            logger.Error("CheckKyuzuByreceiveingNumber error: ", ex);
        }
        logger.Debug("end CheckKyuzuByreceiveingNumber: No Value");
        return false;
    }


    #region ADD,DELETE,UPDATE KYUZU
    #region Delete Kyuzu
    [WebMethod]
    public void DeleteReceivingNumberToSP(string receivingNumber)
    {
        logger.Debug("start DeleteReceivingNumberToSP");
        logger.Debug("Start DeleteKyuzu");
        string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
        SPWeb web = OpenSPWebAsAdmin(strUrlSite);

        SPList spKyuzuList = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST));


        logger.Debug("get List done, ListKyuzu count: " + spKyuzuList.ItemCount.ToString());

        SPListItem ItemListKyuzu = GetSPListItemByProperties(spKyuzuList, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER), receivingNumber);

        ItemListKyuzu.Delete();
        logger.Debug("Delete Kyuzu success");

        logger.Debug("end DeleteReceivingNumberToSP");
    }
    #endregion
    #region Update Kyuzu
    [WebMethod]
    public void UpdateKyuzu(SharePointKyuzuList spKyuzu)
    {
        logger.Debug("Start UpdateKyuzu");
        string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
        SPWeb web = OpenSPWebAsAdmin(strUrlSite);

        SPList spKyuzuList = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST));
        logger.Debug("get List done, ListKyuzu count: " + spKyuzuList.ItemCount.ToString());

        SPListItem ItemListKyuzu = GetSPListItemByProperties(spKyuzuList, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER), spKyuzu.ReceiveingNumber);

        Dictionary<string, object> dictProperties = new Dictionary<string, object>();
        dictProperties = SetValueForKyuzu(spKyuzu, dictProperties);

        UpdateListItem(ItemListKyuzu, dictProperties);
    }

    private Dictionary<string, object> SetValueForKyuzu(SharePointKyuzuList spKyuzu, Dictionary<string, object> dictProperties)
    {
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NUMBER), spKyuzu.ProductNumber);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NAME), spKyuzu.ProductName);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CAR_TYPE), spKyuzu.TypeOfCar);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHANGE_OF_SALES), spKyuzu.PersionInChangeOfSales);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_COMPANY_NAME), spKyuzu.CompanyName);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_TYPE_OF_MODEL), spKyuzu.TypeOfModel);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHARGE), spKyuzu.PersonInCharge);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_CATEGOTY), spKyuzu.Categoty);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_DELIVERY_DATE_PLAN), spKyuzu.DeliveryDatePlan);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_NUMBER_OF_KATA), spKyuzu.NumberOfKata);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_KYUZU), spKyuzu.Kyuzu);
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_SYMBOL), spKyuzu.Symbol);
        return dictProperties;
    }

    #endregion
    #region Add Kyuzu
    public void AddKyuzu(SharePointKyuzuList spKyuzu)
    {
        logger.Debug("Start AddKyuzu");
        string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
        SPWeb web = OpenSPWebAsAdmin(strUrlSite);
        SPList spKyuzuList = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST));
        logger.Debug("add Sp");
        SPListItem ItemListKyuzu = spKyuzuList.Items.Add();
        logger.Debug("begin add Sp");

        Dictionary<string, object> dictProperties = new Dictionary<string, object>();
        dictProperties.Add(Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER), spKyuzu.ReceiveingNumber);
        dictProperties = SetValueForKyuzu(spKyuzu, dictProperties);

        UpdateListItem(ItemListKyuzu, dictProperties);

        logger.Debug("get List done, ListKyuzu count: " + spKyuzuList.ItemCount.ToString());
        logger.Debug("end AddKyuzu");

    }
    #endregion

    #endregion

    [WebMethod]
    public SharePointWorkMasterList GetWorkMasterItemByKey(String Key)
    {
        try
        {
            logger.Debug("begin GetWorkMasterItemByKey");
            string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_MASTER_ADMIN_URL);
            SPWeb web = OpenSPWebAsAdmin(strUrlSite);

            SharePointWorkMasterList ReturnValue = new SharePointWorkMasterList();
            SPList ListKWorkMaster = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORKER_LIST));
            logger.Debug("get List done, ListKWorkMaster count: " + ListKWorkMaster.ItemCount.ToString());



            logger.Debug("begin Get ItemList");
            SPListItem ItemListWorkMaster = GetSPListItemByProperties(ListKWorkMaster, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_RECEIVEING_NUMBER), Key);

            if (ItemListWorkMaster != null)
            {

                logger.DebugFormat("ItemListWorkMaster {0}", ItemListWorkMaster[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PRODUCT_NUMBER)].ToString());
                ReturnValue =
                    new SharePointWorkMasterList()
                    {
                        WorkerCode = Key,
                        WorkerName = ItemListWorkMaster[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_WORKER_NAME)].ToString(),
                        AdministrationItem = (ItemListWorkMaster[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_ADMIN_ITEM)] == null) ? null : ItemListWorkMaster[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_ADMIN_ITEM)].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).ToList()

                    };
            }
            else
            {
                logger.Debug("ItemList = null");
            }
            logger.Debug("end Get ItemList");
            return ReturnValue;
        }
        catch (Exception ex)
        {
            logger.Error("GetWorkMasterItemByKey error: ", ex);
            return null;
        }
    }

    [WebMethod]
    public List<SharePointWorkMasterList> GetAllListWorkMasterInSharepoint()
    {
        try
        {
            logger.Debug("begin GetAllListWorkMasterInSharepoint");

            List<SharePointWorkMasterList> ListReturn = new List<SharePointWorkMasterList>();

            string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_MASTER_ADMIN_URL);
            SPWeb web = OpenSPWebAsAdmin(strUrlSite);
            SPList ListKWorkMaster = GetSPListByName(web, Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORKER_LIST));
            logger.Debug("get List done, ListKWorkMaster count: " + ListKWorkMaster.ItemCount.ToString());

            foreach (SPListItem item in ListKWorkMaster.Items)
            {
                ListReturn.Add(
                new SharePointWorkMasterList()
                {
                    WorkerCode = item[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_WORKER_CODE)].ToString(),
                    WorkerName = item[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_WORKER_NAME)].ToString(),
                    AdministrationItem = (item[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_ADMIN_ITEM)] == null) ? null : item[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_WORK_MASTER_LIST_ADMIN_ITEM)].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).ToList()

                });
            }

            logger.Debug("end GetAllListWorkMasterInSharepoint");
            return ListReturn;
        }
        catch (Exception ex)
        {
            logger.Error("GetAllListWorkMasterInSharepoint error: ", ex);
        }
        return null;
    }

}

