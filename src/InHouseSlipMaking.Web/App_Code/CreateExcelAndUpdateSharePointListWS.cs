using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Net;

using log4net;
using InHouseSlipMaking.Domain;
using InHouseSlipMaking.Manager;
using InHouseSlipMaking.Utilities;
using System.IO;


/// <summary>
/// Summary description for CreateExcelAndUpdateSharePointListWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CreateExcelAndUpdateSharePointListWS : System.Web.Services.WebService
{
    private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(CreateExcelAndUpdateSharePointListWS).Name);

    public CreateExcelAndUpdateSharePointListWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void RunProcess()
    {
        try
        {
            using (DBContext db = new DBContext())
            {
                using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                {
                    try
                    {
                        //--------------------------------- Get List Update ----------------------------------------------
                        logger.Debug("begin Get List Update ");
                        List<ReceiveingInformation> ListUpdate = ReceiveingInformation.GetListForUpdateSharepoint();
                        List<SharePointKyuzuList> ListKyuzuInSharepoint = new List<SharePointKyuzuList>();

                                // --------------- Rewrite later -------------------------
                        SharepointWS ws = new SharepointWS();
                        ListKyuzuInSharepoint = ws.GetListKyuzuForInitialInSharepoint(ListUpdate.Select(i => i.RECEIVEING_NUM).ToList());
                                // -------------------------------------------------------





                        //--------------------------------- Update Database ----------------------------------------------
                        logger.Debug("begin ChageUpdateFlagTo0 ");
                        foreach (ReceiveingInformation temp in ListUpdate)
                        {
                            logger.Debug("ChageUpdateFlagTo0 for item: " + temp.RECEIVEING_NUM);
                            temp.UPDATE_FLAG = "0";
                            temp.UPD_WOKER_CD = "F3";
                            temp.UPD_DT = DateTime.Now;
                            db.Update<ReceiveingInformation>(temp);
                            logger.Debug("done for item: " + temp.RECEIVEING_NUM);
                        }

                        //--------------------------------- Update Sharepoint ----------------------------------------------

                        logger.Debug("begin ChageUpdateSharepoint ");
                        string strUrlSite = Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_URL);
                        SPWeb web = ws.OpenSPWebAsAdmin(strUrlSite);
                        SPList KyuzuList = web.Lists[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST)];
                        SPListItemCollection KyuzuListColection = KyuzuList.Items;
                        logger.Debug("Get ListItem collection done");

                        foreach (SharePointKyuzuList item in ListKyuzuInSharepoint)
                        {
                            logger.Debug("begin update Kyuzu item: " + item.ReceiveingNumber);
                            SPListItem ItemUpdate = KyuzuListColection[item.Id];
                            List<WorkingTimeRecode> ListWorkTimeRecodeCurrent = WorkingTimeRecode.GetByReceiveingNumber(item.ReceiveingNumber);
                            ReceiveingInformation ReceiveingInformationCurrent = ListUpdate.Where(i => i.RECEIVEING_NUM == item.ReceiveingNumber).FirstOrDefault();

                            //Change by kawane 24/6/2015
                            string HouanChargeName = WorkingTimeRecode.GetPersonInChargeNameOf(ListWorkTimeRecodeCurrent, "発注");
                            if (HouanChargeName == string.Empty)
                            {
                                HouanChargeName = WorkingTimeRecode.GetPersonInChargeNameOf(ListWorkTimeRecodeCurrent, "方案");
                            }
                            ItemUpdate[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHARGE_HOUAN)] = HouanChargeName;
                            
                           


                            ItemUpdate[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_PERSON_IN_CHARGE_CAD)] = WorkingTimeRecode.GetPersonInChargeNameOf(ListWorkTimeRecodeCurrent, "CAD");
                            ItemUpdate[Common.AppSettingKey(Constant.CONFIG_SHAREPOINT_KYUZU_LIST_MAN_HOUR)] = Common.MinutesToHours(WorkingTimeRecode.GetManHourOfReceivingNumber(item.ReceiveingNumber)).Value.ToString("F");

                            //------------------------------------ Update Excel Attachments ---------------------------------------
                            

                            string fileTemplate = "";
                            string fileOnSharepoint = ItemUpdate.Attachments.UrlPrefix + item.ReceiveingNumber + "社内伝票.xls";

                            SPFile file = web.GetFile(fileOnSharepoint);
                            if (!file.Exists)
                            {
                                fileTemplate = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "ExcelTemplate\\SlipExcelTemplate.xls");
                            }
                            else
                            {
                                fileTemplate = Path.Combine(Path.GetTempPath(), "TempDownloadExcel.xls");

                                byte[] binFile = file.OpenBinary();
                                System.IO.FileStream fstream = System.IO.File.Create(fileTemplate);
                                fstream.Write(binFile, 0, binFile.Length);
                                fstream.Close();
                            }





                            ExportSlipExcel exportExcel = new ExportSlipExcel();
                            string ExcelReturnLink = exportExcel.ExportExcel(fileTemplate, ReceiveingInformationCurrent, item, ListWorkTimeRecodeCurrent);
                            logger.Debug("Excel file return: " + ExcelReturnLink);


                            if (file.Exists)
                            {
                                logger.Debug("ItemUpdate.Attachments.DeleteNow = " + item.ReceiveingNumber + "社内伝票.xls");
                                ItemUpdate.Attachments.DeleteNow(item.ReceiveingNumber + "社内伝票.xls");
                            }

                            FileStream fileStream = new FileStream(ExcelReturnLink, FileMode.Open, FileAccess.Read);
                            byte[] ImageData = new byte[fileStream.Length];
                            fileStream.Read(ImageData, 0, System.Convert.ToInt32(fileStream.Length));
                            fileStream.Close();


                            ItemUpdate.Attachments.Add(item.ReceiveingNumber + "社内伝票.xls", ImageData);

                            logger.Debug("item.ReceiveingNumber 社内伝票.xls = " + item.ReceiveingNumber + "社内伝票.xls");
                            //-----------------------------------------------------------------------------------------------------

                            ItemUpdate.Update();
                            logger.Debug("end update Kyuzu item: " + item.ReceiveingNumber);
                        }
                        logger.Debug("end ChageUpdateSharepoint ");
                        //----------------------------- End -> commit Database ----------------------------------------------
                        tran.Commit();
                        logger.Debug("Commited sharepoint");

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateListWorkingTimeRecode: ", ex);
            throw ex;
        }
        

        
        



    }

    [WebMethod]
    public void RunProcessTestExcel()
    {
        try
        {
            logger.Debug("begin Get List Update ");
            List<ReceiveingInformation> ListUpdate = ReceiveingInformation.GetListForUpdateSharepoint();
            List<SharePointKyuzuList> ListKyuzuInSharepoint = new List<SharePointKyuzuList>();
            SharePointKyuzuList kyuzu = new SharePointKyuzuList();

            kyuzu.ReceiveingNumber = ListUpdate[0].RECEIVEING_NUM;
            kyuzu.PersionInChangeOfSales = "PIC";
            kyuzu.TypeOfCar = "carType";
            kyuzu.Symbol = "S";
            kyuzu.CompanyName = "Company";
            kyuzu.ProductNumber = "productNum";
            kyuzu.ProductName = "ProductName";
            kyuzu.TypeOfModel = "新規";
            kyuzu.PersonInCharge = "pers";
            kyuzu.Categoty = "W/C";
            //kyuzu.DeliveryDatePlan = 
            kyuzu.PersonInChargeOfHouan = "Ngu~";
            kyuzu.PersonInChargeOfCAD = "Ngu~";
            kyuzu.ManHour = "171.40";
            //kyuzu.DeliveryDateActial =
            //kyuzu.Remark =
            //kyuzu.NumberOfKata =
            kyuzu.Kyuzu = "Kyuzu";

            ListKyuzuInSharepoint.Add(kyuzu);

            foreach (SharePointKyuzuList item in ListKyuzuInSharepoint)
            {
                logger.Debug("begin update Kyuzu item: " + item.ReceiveingNumber);
                List<WorkingTimeRecode> ListWorkTimeRecodeCurrent = WorkingTimeRecode.GetByReceiveingNumber(item.ReceiveingNumber);
                ReceiveingInformation ReceiveingInformationCurrent = ListUpdate.Where(i => i.RECEIVEING_NUM == item.ReceiveingNumber).FirstOrDefault();

                string fileTemplate = "";
                string fileOnSharepoint = @"D:\Projects\InHourseSlipMaking\Test\" + item.ReceiveingNumber + "社内伝票.xls";

                if (!File.Exists(fileOnSharepoint))
                {
                    fileTemplate = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "ExcelTemplate\\SlipExcelTemplate.xls");
                }
                else
                {
                    //fileTemplate = Path.Combine(Path.GetTempPath(), "TempDownloadExcel.xls");
                    fileTemplate = fileOnSharepoint;
                    byte[] binFile = File.ReadAllBytes(fileTemplate);
                    System.IO.FileStream fstream = System.IO.File.Create(fileTemplate);
                    fstream.Write(binFile, 0, binFile.Length);
                    fstream.Close();
                }


                ExportSlipExcel exportExcel = new ExportSlipExcel();
                string ExcelReturnLink = exportExcel.ExportExcel(fileTemplate, ReceiveingInformationCurrent, item, ListWorkTimeRecodeCurrent);
                logger.Debug("Excel file return: " + ExcelReturnLink);

                File.Copy(ExcelReturnLink, fileOnSharepoint, true);



                logger.Debug("item.ReceiveingNumber 社内伝票.xls = " + item.ReceiveingNumber + "社内伝票.xls");
                logger.Debug("end update Kyuzu item: " + item.ReceiveingNumber);
            }
            logger.Debug("end ChageUpdateSharepoint ");
            logger.Debug("Commited sharepoint");


        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateListWorkingTimeRecode: ", ex);
            throw ex;
        }
        }

    [WebMethod]
    public void TestUpdate()
    {
        try
        {
            using (DBContext db = new DBContext())
            {
                List<ReceiveingInformation> ListUpdate = ReceiveingInformation.GetListForUpdateSharepoint();
                foreach (ReceiveingInformation temp in ListUpdate)
                {
                    logger.Debug("ChageUpdateFlagTo0 for item: " + temp.RECEIVEING_NUM);
                    temp.UPDATE_FLAG = "0";
                    temp.UPD_WOKER_CD = "F3";
                    temp.UPD_DT = DateTime.Now;
                    db.Update<ReceiveingInformation>(temp);
                    logger.Debug("done for item: " + temp.RECEIVEING_NUM);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error when UpdateListWorkingTimeRecode: ", ex);
            throw ex;
        }
    }

    

    
}

