using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using InHouseSlipMaking.Domain;
using InHouseSlipMaking.Utilities;
using log4net;

/// <summary>
/// Summary description for InputSaleInfomationWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class InputSaleInfomationWS : System.Web.Services.WebService
{

    static readonly ILog logger = log4net.LogManager.GetLogger(typeof(InputSaleInfomationWS));
    private List<SharePointKyuzuList> listSpKyuzu;
    private List<ReceiveingInformation> listReceiImf;

    private SharepointWS spWS = new SharepointWS();
    public InputSaleInfomationWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        listReceiImf = new List<ReceiveingInformation>();
        listSpKyuzu = new List<SharePointKyuzuList>();
        //call service get list SharePointKyuzuList;
    }
    [WebMethod]
    public List<SharePointKyuzuList> GetListReceiveingNumber()
    {
        try
        {
            logger.Debug("Begin GetListReceiveingNumber");
            List<string> listReceivNumber = new List<string>();
            listReceivNumber = ReceiveingInformation.GetAllReceiveingNumber1();
            listSpKyuzu = spWS.GetListKyuzuForInitialInSharepoint(listReceivNumber);
            logger.Debug("End GetListReceiveingNumber");
            return listSpKyuzu;
        }
        catch (Exception ex)
        {
            logger.ErrorFormat("Error GetListReceiveingNumber: {0}", ex.Message);
            throw ex;
        }
    }
    [WebMethod]
    public List<ReceiveingInformation> GetListReceiveingInformation()
    {
        try
        {
            logger.Debug("Begin GetListReceiveingInformation");
            return ReceiveingInformation.GetAll();
        }
        catch (Exception ex)
        {
            logger.ErrorFormat("Error GetListReceiveingInformation: {0}", ex.Message);
            throw ex;
        }
    }
    [WebMethod]
    public ReceiveingInformation GetReceiveingInformationByReceivingNumber(string rcvNumber)
    {
        try
        {
            ReceiveingInformation result = new ReceiveingInformation { RECEIVEING_NUM = rcvNumber };
            return result.GetByPrimaryKey();
        }
        catch (Exception ex)
        {
            logger.ErrorFormat("Error GetReceiveingInformationByReceivingNumber: {0}", ex.Message);
            throw ex;
        }

    }

    #region Insert, Update, Delete

    [WebMethod]
    public void InsertAndUpdateReceivingNumberToSPandDB(SharePointKyuzuList spKyuzuList)
    {
        try
        {
            //Insert and Update DB
            logger.Debug("Begin InsertAndUpdateReceivingNumberToSPandDB");
            ReceiveingInformation insertOrUpdateItem = new ReceiveingInformation();
            string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
            insertOrUpdateItem.RECEIVEING_NUM = spKyuzuList.ReceiveingNumber;
            logger.DebugFormat("ReceiveingNumber is:{0}", spKyuzuList.ReceiveingNumber);
            insertOrUpdateItem = GetReceiveingInformationByReceivingNumber(insertOrUpdateItem.RECEIVEING_NUM);
            if (insertOrUpdateItem == null)
            {
                insertOrUpdateItem = new ReceiveingInformation();
                insertOrUpdateItem.RECEIVEING_NUM = spKyuzuList.ReceiveingNumber;
                insertOrUpdateItem.CANCEL_FLAG = spKyuzuList.CancelFlag;
                insertOrUpdateItem.UPDATE_FLAG = Constant.UPDATE_FLAG_1;
                insertOrUpdateItem.UPD_WOKER_CD = Constant.F1;
                insertOrUpdateItem.UPD_DT = DateTime.Now;
                insertOrUpdateItem.Insert();
            }
            else
            {
                insertOrUpdateItem.CANCEL_FLAG = spKyuzuList.CancelFlag;
                insertOrUpdateItem.UPDATE_FLAG = Constant.UPDATE_FLAG_1;
                insertOrUpdateItem.UPD_WOKER_CD = Constant.F1;
                insertOrUpdateItem.UPD_DT = DateTime.Now;
                insertOrUpdateItem.Update();
            }
            if (spWS.CheckKyuzuByreceiveingNumber(spKyuzuList.ReceiveingNumber) == true)
            {
                spWS.UpdateKyuzu(spKyuzuList);
            }
            else
            {
                spWS.AddKyuzu(spKyuzuList);
            }
            logger.Debug("End InsertAndUpdateReceivingNumberToSPandDB");

        }
        catch (Exception ex)
        {
            logger.ErrorFormat("Error InsertAndUpdateReceivingNumberToSPandDB: {0}", ex.Message);
            throw ex;
        }
    }

    [WebMethod]
    public void DeleteReceivingNumberToSPandDB(string receivingNumber)
    {
        try
        {
            //delete in td recievingImformation
            logger.Debug("start DeleteReceivingNumberToSPandDB");
            ReceiveingInformation rcIfm = GetReceiveingInformationByReceivingNumber(receivingNumber);
            rcIfm.Delete();
            // delete in WorkingTimeRecode
            logger.DebugFormat("Delete success receivingNumber {0} in ReceiveingInformation ", receivingNumber);
            List<WorkingTimeRecode> listWorkingTime = new List<WorkingTimeRecode>();
            listWorkingTime = WorkingTimeRecode.GetByReceiveingNumber(receivingNumber);
            foreach (var iten in listWorkingTime)
            {
                iten.Delete();
                logger.DebugFormat("Delete success receivingNumber {0} in WorkingTimeRecode", receivingNumber);
            }
            //delete in sharepoint
            if (spWS.CheckKyuzuByreceiveingNumber(receivingNumber)== true)
            spWS.DeleteReceivingNumberToSP(receivingNumber);
            logger.Debug("end DeleteReceivingNumberToSPandDB");

        }
        catch (Exception ex)
        {
            logger.ErrorFormat("Error DeleteReceivingNumberToSPandDB: {0}", ex);
            throw ex;
        }
    }
    #endregion

}

