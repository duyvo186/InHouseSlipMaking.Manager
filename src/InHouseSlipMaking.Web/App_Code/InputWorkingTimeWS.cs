using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using InHouseSlipMaking.Domain;
using System.Collections.Generic;

using log4net;

/// <summary>
/// Summary description for InputWorkingTimeWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class InputWorkingTimeWS : System.Web.Services.WebService {

    private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(InputWorkingTimeWS).Name);

    public InputWorkingTimeWS () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public List<string> Test()
    {
        List<string> a = ReceiveingInformation.GetAllReceiveingNumber();

        decimal b = WorkingTimeRecode.GetManHourOfReceivingNumber("0901021K-D10");
        return a;
    }

    [WebMethod]
    public List<SharePointKyuzuList> GetSharepointListKyuzuForInitial()
    {
        try
        {
            logger.Debug("begin GetSharepointListKyuzuForInitial");
            List<string> AllReceiveingNumber = ReceiveingInformation.GetAllReceiveingNumber();

            SharepointWS temp = new SharepointWS();
            return temp.GetListKyuzuForInitialInSharepoint(AllReceiveingNumber);
        }
        catch (Exception ex)
        {
            logger.Error("Error when Get sharepoint List kyuzu for initial", ex);
            throw ex;
        }
    }

    [WebMethod]
    public ReceiveingInformation GetReceiveingInformation(string Key)
    {
        try
        {
            return ReceiveingInformation.GetByReceiveingNumber(Key); ;
        }
        catch (Exception ex)
        {
            logger.Error("Error when GetReceiveingInformation", ex);
            throw ex;
        }

    }

    [WebMethod]
    public List<WorkingTimeRecode> GetListWorkingTimeRecodeForForm(string Key)
    {
        try
        {
            return WorkingTimeRecode.GetByReceiveingNumber(Key);
        }
        catch (Exception ex)
        {
            logger.Error("Error when Get GetListWorkingTimeRecodeForForm", ex);
            throw ex;
        }

    }

    [WebMethod]
    public SharePointWorkMasterList GetSharepointItemWorkMaster(string Key)
    {
        try
        {
            SharepointWS temp = new SharepointWS();
            return temp.GetWorkMasterItemByKey(Key);
        }
        catch (Exception ex)
        {
            logger.Error("Error when GetSharepointItemWorkMaster", ex);
            throw ex;
        }

    }

    [WebMethod]
    public List<SharePointWorkMasterList> GetAllSharePointWorkMasterList()
    {
        try
        {
            SharepointWS temp = new SharepointWS();
            return temp.GetAllListWorkMasterInSharepoint();
        }
        catch (Exception ex)
        {
            logger.Error("Error when Get sharepoint List kyuzu for initial", ex);
            throw ex;
        }
    }

    [WebMethod]
    public string GetCurrentUser()
    {
        try
        {
            SharepointWS temp = new SharepointWS();
            return temp.GetCurrentUser();
        }
        catch (Exception ex)
        {
            logger.Error("Error when GetCurrentUser", ex);
            throw ex;
        }
    }

    [WebMethod]
    public void UpdateReceiveingInformation(ReceiveingInformation Item)
    {
        try
        {
            ReceiveingInformation.UpdateReceiveingInformation(Item);
        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateReceiveingInformation", ex);
            throw ex;
        }
    }


    [WebMethod]
    public void testUpdate()
    {
        Dictionary<string, string> ReturnList = new Dictionary<string, string>();

        List<WorkingTimeRecode> ListItem = WorkingTimeRecode.GetByReceiveingNumber("0901021K-D10");
        List<string> ListWorkItem = ListItem.Select(i => i.WORK_ITEM).Distinct().ToList();

        foreach (string temp in ListWorkItem)
        {
            List<WorkingTimeRecode> List1WorkItem = ListItem.Where(i => i.WORK_ITEM == temp).Select(i => i).ToList();
            Dictionary<string, decimal> DictCheck = new Dictionary<string, decimal>();

            foreach (WorkingTimeRecode item in List1WorkItem)
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
            ReturnList.Add(temp, DictCheck.Where(i => i.Value == DictCheck.Max(j => j.Value)).Select(i => i).FirstOrDefault().Key);
        }



      
        

    }

    [WebMethod]
    public void UpdateWorkingTimeRecode(List<WorkingTimeRecode> ListItem, List<WorkingTimeRecode> ListItemBackUp)
    {
        try
        {
            List<WorkingTimeRecode> ListUpdate = new List<WorkingTimeRecode>();

            for (int i = 0; i < ListItem.Count; i++)
            {
                logger.Debug("ListItem = " + ListItem[i].RECEIVEING_NUM + " / " + ListItem[i].WOKER_CD + " / " + ListItem[i].WORKING_HOURS);
                logger.Debug("ListItemBackUp = " + ListItemBackUp[i].RECEIVEING_NUM + " / " + ListItemBackUp[i].WOKER_CD + " / " + ListItemBackUp[i].WORKING_HOURS);

                if (ListItem[i].WOKER_NM != ListItemBackUp[i].WOKER_NM || ListItem[i].WORKING_HOURS != ListItemBackUp[i].WORKING_HOURS)
                {
                    ListUpdate.Add(ListItem[i]);
                }
            }

            logger.Debug("ListUpdate.Count = " + ListUpdate.Count);

            WorkingTimeRecode.UpdateListWorkingTimeRecode(ListUpdate);
        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateWorkingTimeRecode", ex);
            throw ex;
        }
    }

    [WebMethod]
    public void UpdateOrInsertWorkingTimeRecode(WorkingTimeRecode Item)
    {
        try
        {
            WorkingTimeRecode.UpdateOrInsertWorkingTimeRecode(Item);
        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateOrInsertWorkingTimeRecode", ex);
            throw ex;
        }
    }

    [WebMethod]
    public void DeleteWorkingTimeRecode(WorkingTimeRecode Item)
    {
        try
        {
            WorkingTimeRecode.DeleteWorkingTimeRecode(Item);
        }
        catch (Exception ex)
        {
            logger.Error("Error when DeleteInsertWorkingTimeRecode", ex);
            throw ex;
        }
    }

    [WebMethod]
    public SharePointKyuzuList CheckRecieveExistInSP(ReceiveingInformation recieve)
    {
        try
        {
            logger.Debug("begin CheckRecieveExistInSP");

            List<string> AllReceiveingNumber = new List<string>();
            AllReceiveingNumber.Add(recieve.RECEIVEING_NUM);

            SharepointWS temp = new SharepointWS();
            List<SharePointKyuzuList> listSP =  temp.GetListKyuzuForInitialInSharepoint(AllReceiveingNumber);
            if (listSP.Count > 0)
                return listSP[0];
            return null;
        }
        catch (Exception ex)
        {
            logger.Error("Error CheckRecieveExistInSP ", ex);
            throw ex;
        }
    }

}

