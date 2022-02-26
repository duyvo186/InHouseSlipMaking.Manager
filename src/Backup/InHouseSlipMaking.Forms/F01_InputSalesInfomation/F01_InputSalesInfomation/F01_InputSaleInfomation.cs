using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

using F01_InputSalesInfomation.Properties;

using InHouseSlipMaking.Utilities;
using InHouseSlipMaking;

namespace F01_InputSalesInfomation
{
    public partial class F01_InputSaleInfomation : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(F01_InputSaleInfomation).Name);
        private List<InputSaleInfomationWS.SharePointKyuzuList> listSpKyuzu = new List<InputSaleInfomationWS.SharePointKyuzuList>();
        private InputSaleInfomationWS.SharePointKyuzuList kyuzu = new InputSaleInfomationWS.SharePointKyuzuList();
        InputSaleInfomationWS.ReceiveingInformation receiveingInformation = new F01_InputSalesInfomation.InputSaleInfomationWS.ReceiveingInformation();
        List<InputSaleInfomationWS.ReceiveingInformation> receiveingInformationList = new List<F01_InputSalesInfomation.InputSaleInfomationWS.ReceiveingInformation>();
        FormProgessBar form;
        private const int MAX_LINES = 3;
        private System.Threading.Thread t1;

        public F01_InputSaleInfomation()
        {
            InitializeComponent();
           
        }
        public void ThreadProc()
        {
            form = new FormProgessBar();
            form.ShowDialog();
        }
        public void ThreadProc1()
        {
            SendKeys.SendWait("{ESC}");
        }
        private void F01_InputSaleInfomation_Load(object sender, EventArgs e)
        {
            t1 = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t1.Start();


            logger.Debug("start F01_InputSaleInfomation_Load");
            receiveingInformationList = (inputSaleInfomationWS.GetListReceiveingInformation() == null) ? null : inputSaleInfomationWS.GetListReceiveingInformation().ToList();
            txtDelivaryDay.Text = DateTime.Now.ToShortDateString();
            listSpKyuzu = (inputSaleInfomationWS.GetListReceiveingNumber() == null) ? null : inputSaleInfomationWS.GetListReceiveingNumber().ToList();
            txtReceiveNumber.AutoCompleteCustomSource.AddRange(listSpKyuzu.Select(i => i.ReceiveingNumber).ToArray());
            FillDataGridView();
            FillCboCatergory();
            FillCboModelType();
            form.Close();
            this.Activate();
            logger.Debug("end F01_InputSaleInfomation_Load");
        }
        public string GetCancelFlagByReceivingNumber(string rcvNumber)
        {
            try
            {
                return receiveingInformationList.Where(n => n.RECEIVEING_NUM == rcvNumber).FirstOrDefault().CANCEL_FLAG;               
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error GetCancelFlagByReceivingNumber: {0}", ex.Message);
                return string.Empty;
            }

        }
        public InputSaleInfomationWS.ReceiveingInformation GetReceivingInformationByReceiveingNumber(string rcvNumber)
        {
            try
            {
                return receiveingInformationList.Where(n => n.RECEIVEING_NUM == rcvNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error GetCancelFlagByReceivingNumber: {0}", ex.Message);
                return null;
            }

        }
        private void FillDataGridView()
        {
            try
            {
                logger.Debug("begin FillDataGridView");
                List<InputSaleInfomationWS.SharePointKyuzuList> listSpKyuzuForGridView = new List<InputSaleInfomationWS.SharePointKyuzuList>();
                grdReceiNumber.Rows.Clear();
                if (chkDislayReceiveNum.Checked == true)
                {
                    foreach (var item in listSpKyuzu)
                    {
                        if (GetCancelFlagByReceivingNumber(item.ReceiveingNumber) == "0")
                            listSpKyuzuForGridView.Add(item);
                    }
                }
                else 
                {
                    listSpKyuzuForGridView = listSpKyuzu;
                }
                foreach (var item in listSpKyuzuForGridView)
                {
                    int row = grdReceiNumber.Rows.Add();


                    grdReceiNumber.Rows[row].Cells[Constant.RECEIVEING_NUMBER].Value = item.ReceiveingNumber;
                    grdReceiNumber.Rows[row].Cells[Constant.PRODUCT_NUMBER].Value = item.ProductNumber;
                    grdReceiNumber.Rows[row].Cells[Constant.PRODUCT_NAME].Value = item.ProductName;
                    grdReceiNumber.Rows[row].Cells[Constant.CAR_TYPE].Value = item.TypeOfCar;
                }
                logger.Debug("end FillDataGridView");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("FillDataGridView {0}", ex.Message);
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FillCboCatergory()
        {
            cboCatergory.Items.Add(Constant.PROTOTYPE);
            cboCatergory.Items.Add(Constant.SIGNAL_FOR_PORT);
            cboCatergory.Items.Add(Constant.IRON);
            cboCatergory.Items.Add(Constant.JIG);
            cboCatergory.Items.Add(Constant.WC);
            cboCatergory.Items.Add(Constant.MODEL);
            cboCatergory.Items.Add(Constant.CASTING);
            cboCatergory.SelectedIndex = 0;
            rdoCatergory.Checked = true;

        }
        private void FillCboModelType()
        {
            cboModelType.Items.Add(Constant.NOVELTY);
            cboModelType.Items.Add(Constant.ECN);
            cboModelType.Items.Add(Constant.REPAIR);
            cboModelType.Items.Add(Constant.REPAIR1);
            cboModelType.Items.Add(Constant.ADDING);
            cboModelType.Items.Add(Constant.DEFECT);
            cboModelType.Items.Add(Constant.RE_EXAMINATION);
            cboModelType.SelectedIndex = 0;
            rdoModelType.Checked = true;

        }
        #region WebService
        private InputSaleInfomationWS.InputSaleInfomationWS inputSaleInfomationWS
        {
            get
            {
                InputSaleInfomationWS.InputSaleInfomationWS ws = new InputSaleInfomationWS.InputSaleInfomationWS();
                ws.UseDefaultCredentials = true;
                ws.Url = Common.GetWebServicesBaseUrl(ws.Url, Settings.Default.PATH_SETTING_FILE);
                ws.Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings[Constant.WS_TIME_OUT]);
                return ws;
            }
        }
        #endregion
        public InputSaleInfomationWS.SharePointKyuzuList GetSharePointKyuzuListByReceiveingNumber(string rcvNumber)
        {
            try
            {
                return listSpKyuzu.Where(n => n.ReceiveingNumber == rcvNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error GetCancelFlagByReceivingNumber: {0}", ex.Message);
                return null;
                throw ex;
            }

        }
        private void butConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("begin butConfirm_Click");
                ShowMessageBox();
                InputSaleInfomationWS.SharePointKyuzuList spKyuzu = new InputSaleInfomationWS.SharePointKyuzuList();
                InputSaleInfomationWS.ReceiveingInformation recveiIfm = new InputSaleInfomationWS.ReceiveingInformation();
                recveiIfm = GetReceivingInformationByReceiveingNumber(txtReceiveNumber.Text.Trim());
                if (recveiIfm != null)
                {
                    spKyuzu = GetSharePointKyuzuListByReceiveingNumber(txtReceiveNumber.Text.Trim());
                    ClearImformation();
                    if (spKyuzu != null)
                    {
                        DisplayReceivingInformation(spKyuzu);
                    }
                    else
                    {

                        txtReceiveNumber.Text = recveiIfm.RECEIVEING_NUM;
                        if (recveiIfm.CANCEL_FLAG.Trim() == "1")
                        {
                            chkCancelFlag.Checked = true;
                        }
                        else
                        {
                            chkCancelFlag.Checked = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ResourceUtil.Instance.GetString("RECEIVINGNUMBER_NOT_EXIST_IN_TD_RECEIVEING_INFORMATION"), ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Debug("end butConfirm_Click");
                    return;
                }
                logger.Debug("end butConfirm_Click");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error butConfirm_Click{0}", ex.Message);
                logger.Debug("end butConfirm_Click");
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMessageBox()
        {
            timer1.Interval = 1000; // to close after 5 secs, say
            timer1.Enabled = true;
            MessageBox.Show(ResourceUtil.Instance.GetString("PROCESSING"), ResourceUtil.Instance.GetString("INFORMATION"));
        }


        private void DisplayReceivingInformation(InputSaleInfomationWS.SharePointKyuzuList spKyuzu)
        {
            try
            {
                logger.Debug("spkyuzu:"+ spKyuzu.ReceiveingNumber);
                txtReceiveNumber.Text = spKyuzu.ReceiveingNumber;
                txtPersoninChagre.Text = (spKyuzu.PersonInCharge == null) ? string.Empty : spKyuzu.PersonInCharge.ToString();
                txtPersionChargeOfSale.Text = spKyuzu.PersionInChangeOfSales;
                txtCarType.Text = spKyuzu.TypeOfCar;
                txtCompanyName.Text = spKyuzu.CompanyName;
                txtProductNumber.Text = spKyuzu.ProductNumber.Replace("\n", "\r\n");
                txtProductName.Text = spKyuzu.ProductName.Replace("\n", "\r\n");
                txtDelivaryDay.Text = spKyuzu.DeliveryDatePlan;
                //dtmDelivaryDay.Value = (spKyuzu.DeliveryDatePlan == null) ? DateTime.Now : (DateTime)spKyuzu.DeliveryDatePlan;
                //type of Model
                int indexModelType = cboModelType.FindStringExact((spKyuzu.TypeOfModel == null) ? string.Empty : spKyuzu.TypeOfModel.ToString());
                if (indexModelType != -1)
                {
                    txtOrtherModelType.Text = string.Empty;
                    cboModelType.SelectedIndex = indexModelType;
                    rdoModelType.Checked = true;
                }
                else
                {
                    txtOrtherModelType.Text = spKyuzu.TypeOfModel;
                    rdoOrtherModelType.Checked = true;
                }

                txtPersoninChagre.Text = spKyuzu.PersonInCharge.ToString();
                //category
                int indexCatergory = cboCatergory.FindStringExact((spKyuzu.Categoty == null) ? string.Empty : spKyuzu.Categoty.ToString());
                if (indexCatergory != -1)
                {
                    cboCatergory.SelectedIndex = indexCatergory;
                    txtOrtherCatergory.Text = string.Empty;
                    rdoCatergory.Checked = true;
                }
                else
                {
                    txtOrtherCatergory.Text = spKyuzu.Categoty;
                    rdoOrderCatergory.Checked = true;
                }
                txtDelivaryDay.Text = spKyuzu.DeliveryDatePlan;
                txtNumberOfKata.Text = spKyuzu.NumberOfKata;
                txtKyuzu.Text = spKyuzu.Kyuzu.Replace("\n", "\r\n");
                //cancelflag
                logger.Debug("cancelflag GetCancelFlagByReceivingNumber(spKyuzu.ReceiveingNumber)");
                string cancelFlag = GetCancelFlagByReceivingNumber(spKyuzu.ReceiveingNumber);
                logger.DebugFormat("cancelFlag", cancelFlag);
                if (cancelFlag == Constant.CANCEL_FLAG_1)
                {
                    chkCancelFlag.Checked = true;
                }
                else
                {
                    chkCancelFlag.Checked = false;
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error DisplayReceivingInformation{0}", ex.Message);
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtmDelivaryDay_ValueChanged(object sender, EventArgs e)
        {
           // txtDelivaryDay.Text = dtmDelivaryDay.Value.ToShortDateString();
        }

        private void grdReceiNumber_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                logger.Debug("begin grdReceiNumber_CellClick");
                int index = grdReceiNumber.SelectedCells[0].RowIndex;
                InputSaleInfomationWS.SharePointKyuzuList spKyuzu = new InputSaleInfomationWS.SharePointKyuzuList();
                if (grdReceiNumber[Constant.RECEIVEING_NUMBER, index].Value != null)
                {
                    string receivNumber = grdReceiNumber[Constant.RECEIVEING_NUMBER, index].Value.ToString();

                    ClearImformation();
                    // chua lay flag

                    spKyuzu = listSpKyuzu.Where(n => n.ReceiveingNumber == receivNumber).FirstOrDefault();
                    logger.DebugFormat("get sspKyuzu{0}",spKyuzu.ReceiveingNumber);
                    if (spKyuzu != null)
                    {
                        DisplayReceivingInformation(spKyuzu);
                    }
                }
                logger.Debug("end grdReceiNumber_CellClick");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error grdReceiNumber_CellClick{0}", ex.Message);
                logger.Debug("begin grdReceiNumber_CellClick");
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            ClearImformation();

        }

        private void ClearImformation()
        {
            //dtmDelivaryDay.Value = DateTime.Now;
            txtReceiveNumber.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtCarType.Text = string.Empty;
            txtPersoninChagre.Text = string.Empty;
            txtPersionChargeOfSale.Text = string.Empty;
            txtProductNumber.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtDelivaryDay.Text = string.Empty;
            rdoCatergory.Checked = true;
            cboCatergory.SelectedIndex = 0;
            rdoOrderCatergory.Checked = false;
            txtOrtherCatergory.Text = string.Empty;
            rdoModelType.Checked = true;
            cboModelType.SelectedIndex = 0;
            rdoOrtherModelType.Checked = false;
            txtOrtherModelType.Text = string.Empty;
            txtKyuzu.Text = string.Empty;
            txtNumberOfKata.Text = string.Empty;
            chkCancelFlag.Checked = false;
        }

        private void chkDislayReceiveNum_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("begin butSubmit_Click");
                InputSaleInfomationWS.ReceiveingInformation receiIfm = new InputSaleInfomationWS.ReceiveingInformation();
                InputSaleInfomationWS.SharePointKyuzuList spKyuzu = new InputSaleInfomationWS.SharePointKyuzuList();

                if (GetReceivingInformationByReceiveingNumber(txtReceiveNumber.Text.Trim()) != null)
                {
                    DialogResult result = MessageBox.Show(ResourceUtil.Instance.GetString("CONFIRM_WHEN_RECEIVINGNUMBER_EXIST"), ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result != DialogResult.Yes)
                    {
                        logger.Debug("end butSubmit_Click");
                        return;
                    }
                }
                ShowMessageBox();
                if (!string.IsNullOrEmpty(txtReceiveNumber.Text.Trim()))
                {
                    spKyuzu = SetImformationtoUpdateOrInsert();
                    if (spKyuzu == null)
                    {
                        logger.Debug("end butSubmit_Click");
                        return;
                    }
                    inputSaleInfomationWS.InsertAndUpdateReceivingNumberToSPandDB(spKyuzu);

                    kyuzu = GetSharePointKyuzuListByReceiveingNumber(spKyuzu.ReceiveingNumber);
                    if (kyuzu == null)
                    {
                        listSpKyuzu.Add(spKyuzu);
                    }
                    else
                    {
                        listSpKyuzu.Remove(kyuzu);
                        listSpKyuzu.Add(spKyuzu);
                    }
                    receiIfm=GetReceivingInformationByReceiveingNumber(spKyuzu.ReceiveingNumber);
                    receiveingInformation = new F01_InputSalesInfomation.InputSaleInfomationWS.ReceiveingInformation { RECEIVEING_NUM = spKyuzu.ReceiveingNumber, CANCEL_FLAG = spKyuzu.CancelFlag, UPDATE_FLAG = Constant.UPDATE_FLAG_1, UPD_WOKER_CD = Constant.F1, UPD_DT = DateTime.Now };
                    if (receiIfm == null)
                    {
                        receiveingInformationList.Add(receiveingInformation);
                    }
                    else
                    {
                        receiveingInformationList.Remove(receiIfm);
                        receiveingInformationList.Add(receiveingInformation);
                    }
                    FillDataGridView();
                    MessageBox.Show(ResourceUtil.Instance.GetString("INFORMATION_UPDATE_CORRECTLY"), ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please input receiveing number", ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                logger.Debug("end butSubmit_Click");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error butSubmit_Click{0}", ex.Message);
                logger.Debug("end butSubmit_Click");
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private InputSaleInfomationWS.SharePointKyuzuList SetImformationtoUpdateOrInsert()
        {
            try
            {
                InputSaleInfomationWS.SharePointKyuzuList spKyuzu = new InputSaleInfomationWS.SharePointKyuzuList();
                spKyuzu.ReceiveingNumber = txtReceiveNumber.Text.Trim();
                if (chkCancelFlag.Checked == true)
                {
                    spKyuzu.CancelFlag = Constant.CANCEL_FLAG_1;
                }
                else
                {
                    spKyuzu.CancelFlag = Constant.CANCEL_FLAG_0;
                }
                spKyuzu.PersionInChangeOfSales = txtPersionChargeOfSale.Text.Trim();
                spKyuzu.TypeOfCar = txtCarType.Text.Trim();
                spKyuzu.CompanyName = txtCompanyName.Text.Trim();
                spKyuzu.ProductNumber = txtProductNumber.Text;
                spKyuzu.ProductName = txtProductName.Text;
                if (rdoModelType.Checked == true)
                {
                    spKyuzu.TypeOfModel = cboModelType.Text.Trim();
                }
                else
                {
                    if (rdoOrtherModelType.Checked == true)
                        spKyuzu.TypeOfModel = txtOrtherModelType.Text.Trim();
                }
                spKyuzu.PersonInCharge = txtPersoninChagre.Text.Trim();
                if (rdoCatergory.Checked == true)
                {
                    spKyuzu.Categoty = cboCatergory.Text.Trim();
                }
                else
                {
                    if (rdoOrderCatergory.Checked == true)
                        spKyuzu.Categoty = txtOrtherCatergory.Text.Trim();
                }
                spKyuzu.DeliveryDatePlan = txtDelivaryDay.Text;
                spKyuzu.NumberOfKata = txtNumberOfKata.Text.Trim();
                spKyuzu.Kyuzu = txtKyuzu.Text.Trim();
                if (spKyuzu.ReceiveingNumber.Length >= 8)
                {
                    spKyuzu.Symbol =spKyuzu.ReceiveingNumber[7].ToString();
                }
                return spKyuzu;





            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error SetImformationtoUpdateOrInsert{0}", ex.Message);
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            form.Close();
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("begin butDelete_Click");
                string receiveingNumber =txtReceiveNumber.Text.Trim();
                if (GetReceivingInformationByReceiveingNumber(txtReceiveNumber.Text.Trim()) != null)
                {
                    DialogResult result = MessageBox.Show(ResourceUtil.Instance.GetString("CONFIRM_WHEN_ALL_RELATING_DATA_IS_DELETED"), ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result != DialogResult.Yes)
                    {
                        logger.Debug("end butDelete_Click");
                        return;
                    }
                    else
                    {
                        ShowMessageBox();
                        inputSaleInfomationWS.DeleteReceivingNumberToSPandDB(receiveingNumber);

                        kyuzu=GetSharePointKyuzuListByReceiveingNumber(receiveingNumber);
                        if (kyuzu!=null)
                        {
                            listSpKyuzu.Remove(kyuzu);
                        }
                        receiveingInformation = GetReceivingInformationByReceiveingNumber(receiveingNumber);
                        if (receiveingInformation != null)
                        {
                            receiveingInformationList.Remove(receiveingInformation);
                        }
                        ClearImformation();
                        FillDataGridView();
                        MessageBox.Show(ResourceUtil.Instance.GetString("DELETE_RECEIVING_INFORMATION_COMPLETE"), ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show(ResourceUtil.Instance.GetString("RECEIVINGNUMBER_NOT_EXIST_IN_TD_RECEIVEING_INFORMATION"), ResourceUtil.Instance.GetString("INFORMATION"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                logger.Debug("end butDelete_Click");

            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error butDelete_Click {0}", ex.Message);
                logger.Debug("end butDelete_Click");
                MessageBox.Show(ex.Message, ResourceUtil.Instance.GetString("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtProductNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtProductNumber.Lines.Length >= MAX_LINES && e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtProductName.Lines.Length >= MAX_LINES && e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void txtKyuzu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtKyuzu.Lines.Length >= MAX_LINES && e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            SendKeys.SendWait("{ESC}");

        }

    }
}
