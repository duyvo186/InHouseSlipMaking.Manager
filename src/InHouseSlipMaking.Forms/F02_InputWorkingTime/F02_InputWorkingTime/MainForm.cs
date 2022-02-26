using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

using InHouseSlipMaking.Utilities;
using F02_InputWorkingTime.Properties;
using InHouseSlipMaking;

namespace F02_InputWorkingTime
{
    public partial class WorkingTimeInputForm : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WorkingTimeInputForm).Name);

        InputWorkingTimeWS.ReceiveingInformation receiveingInformation = new F02_InputWorkingTime.InputWorkingTimeWS.ReceiveingInformation();
        List<InputWorkingTimeWS.WorkingTimeRecode> listWorkingTimeRecode = new List<F02_InputWorkingTime.InputWorkingTimeWS.WorkingTimeRecode>();
        List<InputWorkingTimeWS.WorkingTimeRecode> listWorkingTimeRecodeBackup = new List<F02_InputWorkingTime.InputWorkingTimeWS.WorkingTimeRecode>();
        
        List<InputWorkingTimeWS.SharePointKyuzuList> sharePointKyuzuList = new List<InputWorkingTimeWS.SharePointKyuzuList>();
        List<InputWorkingTimeWS.SharePointWorkMasterList> listSharePointWorkMasterList = new List<F02_InputWorkingTime.InputWorkingTimeWS.SharePointWorkMasterList>();

        AutoCompleteStringCollection AutoCompleteSourceForReceivingInfoTextbox = new AutoCompleteStringCollection();

        DateTime? CompleteDateDateTimeUpdate = new DateTime();
        DateTime? CADStartDateDateTimeUpdate = new DateTime();
        DateTime? ManufactureStartDateDateTimeUpdate = new DateTime();


        //string userName = "";

        public WorkingTimeInputForm()
        {
            try
            {
                logger.Debug("Begin form");
                InitializeComponent();
                

                logger.Debug("Begin Get List Kyuzu in sharepoint");
                

                InputWorkingTimeWS.SharePointKyuzuList[] Temp = inputWorkingTimeWS.GetSharepointListKyuzuForInitial();

                sharePointKyuzuList = Temp == null ? null : Temp.ToList();

                AutoCompleteSourceForReceivingInfoTextbox.AddRange(sharePointKyuzuList.Select(i => i.ReceiveingNumber).ToArray());
                ReceivingNumberTextbox.AutoCompleteCustomSource = AutoCompleteSourceForReceivingInfoTextbox;

                logger.Debug("Fill data to Kyuzu datagrid");



                //Fill list Kyuzu in datagriview ReceivingNumber
                FillDataGridViewKyuzuList(sharePointKyuzuList);

                logger.Debug("Get sharepoint work master list");
                listSharePointWorkMasterList = inputWorkingTimeWS.GetAllSharePointWorkMasterList().ToList();

                //logger.Debug("get username");
                //userName = inputWorkingTimeWS.GetCurrentUser();

                logger.Debug("Fill data to fields");
                CompleteDateDateTimePicker.CustomFormat = " ";
                CompleteDateDateTimePicker.Format = DateTimePickerFormat.Custom;
                CompleteDateDateTimeUpdate = null;

                CADStartDateDateTimePicker.CustomFormat = " ";
                CADStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;
                CADStartDateDateTimeUpdate = null;

                ManufactureStartDateDateTimePicker.CustomFormat = " ";
                ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;
                ManufactureStartDateDateTimeUpdate = null;

                WorkDateDateTimePicker.Value = DateTime.Now;

                WorkItemListBox.SelectedIndex = 0;

                CompletedCheckCheckBox.Enabled = false;
                EnableFormItem(false);

                

                logger.Debug("End initial form");
            }
            catch (Exception ex)
            {
                logger.Error("Error when Initial: ", ex);
                MessageBox.Show(ex.Message);
                this.Close();
            }


        }

       

        private InputWorkingTimeWS.InputWorkingTimeWS inputWorkingTimeWS
        {
            get
            {
                InputWorkingTimeWS.InputWorkingTimeWS ws = new InputWorkingTimeWS.InputWorkingTimeWS();
                ws.UseDefaultCredentials = true;
                //ws.Credentials = new System.Net.NetworkCredential(@"administrator", "Password!");   
                ws.Url = Common.GetWebServicesBaseUrl(ws.Url, Settings.Default.PATH_SETTING_FILE);
                ws.Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings[Constant.WS_TIME_OUT]);
                return ws;
            }            
        }

        private void CompleteDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            CompleteDateDateTimePicker.Format = DateTimePickerFormat.Short;
            CompleteDateDateTimeUpdate = CompleteDateDateTimePicker.Value;
            chkCompleteDate.Checked = CompleteDateDateTimePicker.Value != null ? false : true;
            
        }

        private void CADStartDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            CADStartDateDateTimePicker.Format = DateTimePickerFormat.Short;
            CADStartDateDateTimeUpdate = CADStartDateDateTimePicker.Value;
            chkCADStartDat.Checked = CADStartDateDateTimePicker.Value != null ? false : true;
            
        }

        private void ManufactureStartDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Short;
            ManufactureStartDateDateTimeUpdate = ManufactureStartDateDateTimePicker.Value;
            chkManufactureStartDate.Checked = ManufactureStartDateDateTimePicker.Value != null ? false : true;
            
        }

        private void WorkDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetWorkingItemToForm();

            RefillWorkTimeListBox();
            RefillWorkTimeEachWorkerListBox();
        }

        private void ComfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("Begin confirm button click");
                if (!string.IsNullOrEmpty(ReceivingNumberTextbox.Text))
                {
                    receiveingInformation = inputWorkingTimeWS.GetReceiveingInformation(ReceivingNumberTextbox.Text);
                    if (receiveingInformation == null)
                    {
                        logger.Debug("no Reiciveing return");
                        MessageBox.Show(ResourceUtil.Instance.GetString("NUMBER_DOESNT_EXIST_IN_DATABASE"));
                        return;
                    }

                    logger.Debug("begin get List Working Time Recode");

                    InputWorkingTimeWS.WorkingTimeRecode[] Temp = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
                    listWorkingTimeRecode = Temp == null ? null : Temp.ToList();

                    logger.Debug("begin get List Working Time Recode Backup");

                    InputWorkingTimeWS.WorkingTimeRecode[] Temp1 = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
                    listWorkingTimeRecodeBackup = Temp1 == null ? null : Temp1.ToList();


                    FillDataToFormAfterConfirm();
                    IndexReceiveSelect(ReceivingNumberTextbox.Text);
                    logger.Debug("End confirm click");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error when click confirm: ", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            receiveingInformation = new F02_InputWorkingTime.InputWorkingTimeWS.ReceiveingInformation();
            listWorkingTimeRecode = new List<F02_InputWorkingTime.InputWorkingTimeWS.WorkingTimeRecode>();

            CompleteDateDateTimePicker.CustomFormat = " ";
            CompleteDateDateTimePicker.Format = DateTimePickerFormat.Custom;


            CADStartDateDateTimePicker.CustomFormat = " ";
            CADStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;

            ManufactureStartDateDateTimePicker.CustomFormat = " ";
            ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;

            SetInfoToInfomationGroup(null, null, null, null, "", null, null, null, null);

            WorkDateDateTimePicker.Value = DateTime.Now;

            CompletedCheckCheckBox.Checked = false;
            CompletedCheckCheckBox.Enabled = false;
            EnableFormItem(false);
            
            WorkTimeTotalPersonTextBox.Enabled = false;

            ReceivingNumberTextbox.Text = string.Empty;
            WorkItemDataGridView.Rows.Clear();

            //Set Enable Receiving Info Textbox and disable Submit click
            ReceivingNumberTextbox.Enabled = true;
            SubmitButton.Enabled = false;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {

                if ( listSharePointWorkMasterList.Where(i => i.WorkerCode == WorkerCodeTextBox.Text).Count() == 0)
                {
                    throw new Exception(ResourceUtil.Instance.GetString("INPUT_WORKER_NOT_EXIST_IN_SHAREPOINT"));
                }


                logger.Debug("begin submit button, setFormValueToVariable");

                receiveingInformation.COMPLETED_DT = CompleteDateDateTimeUpdate;
                receiveingInformation.CAD_START_DT = CADStartDateDateTimeUpdate;
                receiveingInformation.MANUFACTURE_START_DT = ManufactureStartDateDateTimeUpdate;

                receiveingInformation.SIZE_D = CheckLenght(SizeDTextBox);
                receiveingInformation.SIZE_H = CheckLenght(SizeHTextBox);
                receiveingInformation.SIZE_W = CheckLenght(SizeWTextBox);
                receiveingInformation.WEIGHT = CheckLenght(SizeWeightTextBox);

                receiveingInformation.DATA_NUM = DataNumberTextBox.Text;

                logger.Debug("begin update ReceiveingInfo");
                inputWorkingTimeWS.UpdateReceiveingInformation(receiveingInformation);

                logger.Debug("begin update Working Tim Recode");
                InputWorkingTimeWS.WorkingTimeRecode WorkingUpdate = new F02_InputWorkingTime.InputWorkingTimeWS.WorkingTimeRecode();
                WorkingUpdate.RECEIVEING_NUM = receiveingInformation.RECEIVEING_NUM;
                WorkingUpdate.WOKER_CD = WorkerCodeTextBox.Text;
                WorkingUpdate.WOKER_NM = WokerNameTextBox.Text;
                WorkingUpdate.WORK_DATE = WorkDateDateTimePicker.Value.Date;
                WorkingUpdate.WORK_ITEM = WorkItemListBox.SelectedItem.ToString();

                if (WorkTimeTodayTextBox.Text == "0")
                {
                    inputWorkingTimeWS.DeleteWorkingTimeRecode(WorkingUpdate);
                }
                else
                {
                    WorkingUpdate.WORKING_HOURS = Common.HoursToMinutes(Convert.ToDecimal(WorkTimeTodayTextBox.Text.Replace(',', '.'))).Value;
                    inputWorkingTimeWS.UpdateOrInsertWorkingTimeRecode(WorkingUpdate);
                }

                

                logger.Debug("update done");
                MessageBox.Show(ResourceUtil.Instance.GetString("UPDATED_WORK_TIME_COMPLETED"));



                /*
                // recall form

                logger.Debug("clear form");
                CompleteDateDateTimePicker.CustomFormat = " ";
                CompleteDateDateTimePicker.Format = DateTimePickerFormat.Custom;

                CADStartDateDateTimePicker.CustomFormat = " ";
                CADStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;

                ManufactureStartDateDateTimePicker.CustomFormat = " ";
                ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;

                DataNumberLabel.Text = "";
                SizeDTextBox.Text = "";
                SizeHTextBox.Text = "";
                SizeWTextBox.Text = "";
                SizeWeightTextBox.Text = "";

                WorkDateDateTimePicker.Value = DateTime.Now;

                WorkItemListBox.SelectedIndex = 0;

                CompletedCheckCheckBox.Enabled = false;
                EnableFormItem(false);

                WorkItemDataGridView.Rows.Clear();

                //Set Enable Receiving Info Textbox and disable Submit click
                ReceivingNumberTextbox.Enabled = true;
                SubmitButton.Enabled = false;
                 *
                 */

                InputWorkingTimeWS.WorkingTimeRecode[] Temp = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
                listWorkingTimeRecode = Temp == null ? null : Temp.ToList();
                FillDataToFormAfterConfirm();

                logger.Debug("Submit done");
            }
            catch (Exception ex)
            {
                logger.Error("Error when submit: ", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WorkItemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.Debug("Begin WorkItemListBox_SelectedIndexChanged");

            try
            {
                if (999 > Convert.ToInt16(WorkerCodeTextBox.Text) && Convert.ToInt16(WorkerCodeTextBox.Text) > 100 && WorkItemListBox.SelectedIndex != 5 && WorkItemListBox.SelectedIndex != 6)
                {
                    WorkItemListBox.SelectedIndex = 5;
                    MessageBox.Show(ResourceUtil.Instance.GetString("WORKER_100_TO_999_ONLY_SET_NC"));

                    return;
                }

            }
            catch (Exception ex)
            { 
                // nothing to do
            }

            SetWorkingItemToForm();
            var AdminItem = listSharePointWorkMasterList.Where(t => t.WorkerCode == WorkerCodeTextBox.Text).SingleOrDefault();

            GetValueForAdminCheck();

            SetEnableToAdminAndCompleteCheckbox(true);

            if(CompletedCheckCheckBox.Checked) 
                SetEnableForCompleteCheckBox();

            logger.Debug("End WorkItemListBox_SelectedIndexChanged");
        }

        private void WorkerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            SetWorkingItemToForm();

            InputWorkingTimeWS.SharePointWorkMasterList temp = listSharePointWorkMasterList.Where(t => t.WorkerCode == WorkerCodeTextBox.Text).SingleOrDefault();

            if (temp != null)
            {
                WokerNameTextBox.Text = temp.WorkerName;
                SetEnableToAdminAndCompleteCheckbox(true);
            }
            else
            {
                WokerNameTextBox.Text = "";
                AdminCheckCheckBox.Enabled = false;
                CompletedCheckCheckBox.Enabled = false;

            }

            RefillWorkTimeEachWorkerListBox();
            try
            {
                if (999 > Convert.ToInt16(WorkerCodeTextBox.Text) && Convert.ToInt16(WorkerCodeTextBox.Text) > 100)
                {
                    if (WorkItemListBox.SelectedIndex != 5 && WorkItemListBox.SelectedIndex != 6)
                    {
                        WorkItemListBox.SelectedIndex = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                // nothing to do
            }

            

            

            

        }

        private void CompletedCheckCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (CompletedCheckCheckBox.Checked == true)
            {
                receiveingInformation.COMPLETED_CK_DT = DateTime.Now;
            }
            else
            {
                receiveingInformation.COMPLETED_CK_DT = null;
            }
        }

        private void CompletedCheckCheckBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CompletedCheckCheckBox.Checked == true)
            {
                receiveingInformation.COMPLETED_CK_DT = DateTime.Now;
            }
            else
            {
                receiveingInformation.COMPLETED_CK_DT = null;
            }
        }

        private void CompletedCheckCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableForCompleteCheckBox();

            WarningMessageLabel.Visible = CompletedCheckCheckBox.Checked;

            if (!CompletedCheckCheckBox.Checked)
            {
                //re-check to call AdminCheckCheckBox_CheckedChanged
                AdminCheckCheckBox.Checked = false;
                GetValueForAdminCheck();
                SetEnableToAdminAndCompleteCheckbox(false);
            }
        }

        private void AdminCheckCheckBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetValueForAdminCheck(AdminCheckCheckBox.Checked);
            SetEnableForAdminCheck();
            FillDataGridViewWorkTimeList();
            
        }

        private void AdminCheckCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            SetValueForAdminCheck(AdminCheckCheckBox.Checked);
            SetEnableForAdminCheck();
            FillDataGridViewWorkTimeList();
        }

        private void AdminCheckCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!CompletedCheckCheckBox.Checked) 
            SetEnableForAdminCheck();
        }

        private void WokerNameTextBox_Leave(object sender, EventArgs e)
        {            
            InputWorkingTimeWS.WorkingTimeRecode temp = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM && t.WOKER_CD == WorkerCodeTextBox.Text &&
                t.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date && t.WORK_ITEM == WorkItemListBox.SelectedItem.ToString()).SingleOrDefault();

            if (temp != null)
            {
                temp.WOKER_NM = WokerNameTextBox.Text;
            }
        }

        private void WorkTimeTodayTextBox_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(WorkTimeTodayTextBox.Text))
            {
                WorkTimeTodayTextBox.Text = "0";
            }

            if (!CheckWorkTimeToday(WorkTimeTodayTextBox))
            {
                WorkTimeTodayTextBox.Focus();
                return;
            }
            InputWorkingTimeWS.WorkingTimeRecode temp = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM && t.WOKER_CD == WorkerCodeTextBox.Text &&
                t.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date && t.WORK_ITEM == WorkItemListBox.SelectedItem.ToString()).SingleOrDefault();

            if (temp != null)
            {
                //temp.WORKING_HOURS = Common.HoursToMinutes(Convert.ToDecimal(WorkTimeTodayTextBox.Text.Replace(',', '.'))).Value;
                FillDataGridViewWorkTimeList();
            }
        }

        private void grdReceiNumber_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                logger.Debug("Begin click Receiving Number");
                int index = grdReceiNumber.SelectedCells[0].RowIndex;
                if (grdReceiNumber[Constant.RECEIVEING_NUMBER, index].Value != null)
                {
                    string receivNumber = grdReceiNumber[Constant.RECEIVEING_NUMBER, index].Value.ToString();
                    SelectReceiveNumber(receivNumber);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error when click Receiving Number Grid: ", ex);
                MessageBox.Show(ex.Message);
            }
        }



       

        //---------------------------------------------------------------------------

        
        public void SetInfoToInfomationGroup(string delivaryDate, DateTime? completeDate, DateTime? cADStartDate, DateTime? manuStartDate,
            string dataNumber, string W, string D, string H, string Weight)
        {
            DeliveryDateTextBox.Text = delivaryDate != null ? delivaryDate : "";

            chkCompleteDate.Checked = completeDate != null ? false : true;
            if(completeDate != null)
            {
                CompleteDateDateTimePicker.Format = DateTimePickerFormat.Short;
                CompleteDateDateTimePicker.Value = completeDate.Value;
                CompleteDateDateTimeUpdate = completeDate.Value;
            }

            chkCADStartDat.Checked = cADStartDate != null ? false : true;
            if (cADStartDate != null)
            {
                CADStartDateDateTimePicker.Format = DateTimePickerFormat.Short;
                CADStartDateDateTimePicker.Value = cADStartDate.Value;
                CADStartDateDateTimeUpdate = cADStartDate.Value;
            }

            chkManufactureStartDate.Checked = manuStartDate != null ? false : true;
            if (manuStartDate != null)
            {
                ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Short;
                ManufactureStartDateDateTimePicker.Value = manuStartDate.Value;
                ManufactureStartDateDateTimeUpdate = manuStartDate.Value;
            }

            DataNumberTextBox.Text = dataNumber;
            SizeWTextBox.Text = W;
            SizeDTextBox.Text = D;
            SizeHTextBox.Text = H;

            SizeWeightTextBox.Text = Weight ;
        }

        public void EnableFormItem(bool CheckEnable)
        {
            //DeliveryDateTextBox.Enabled = CheckEnable;
            CompleteDateDateTimePicker.Enabled = CheckEnable;
            CADStartDateDateTimePicker.Enabled = CheckEnable;
            ManufactureStartDateDateTimePicker.Enabled = CheckEnable;
            chkCADStartDat.Enabled = CheckEnable;
            chkCompleteDate.Enabled = CheckEnable;
            chkManufactureStartDate.Enabled = CheckEnable;

            DataNumberTextBox.Enabled = CheckEnable;
            SizeWTextBox.Enabled = CheckEnable;
            SizeDTextBox.Enabled = CheckEnable;
            SizeHTextBox.Enabled = CheckEnable;
            SizeWeightTextBox.Enabled = CheckEnable;

            WorkItemListBox.Enabled = CheckEnable;

            WorkerCodeTextBox.Enabled = CheckEnable;
            WokerNameTextBox.Enabled = CheckEnable;
            //WorkTimeTotalPersonTextBox.Enabled = CheckEnable;
            AdminCheckCheckBox.Enabled = CheckEnable;
            WorkDateDateTimePicker.Enabled = CheckEnable;
            WorkTimeTodayTextBox.Enabled = CheckEnable;
        }

        public void SetWorkingItemToForm()
        {
            InputWorkingTimeWS.WorkingTimeRecode currentWorkingTimeRecode = new F02_InputWorkingTime.InputWorkingTimeWS.WorkingTimeRecode();

            if (listWorkingTimeRecode != null && WorkerCodeTextBox.Text != null)
            {
                currentWorkingTimeRecode = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM && t.WOKER_CD == WorkerCodeTextBox.Text &&
                    t.WORK_ITEM == WorkItemListBox.SelectedItem.ToString() && t.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date).SingleOrDefault();

                if (currentWorkingTimeRecode != null)
                {
                    WorkTimeTodayTextBox.Text = Common.MinutesToHours(currentWorkingTimeRecode.WORKING_HOURS).Value.ToString("F");
                }
                else
                {
                    WorkTimeTodayTextBox.Text = "0";
                }

                WorkTimeTotalPersonTextBox.Text = Common.MinutesToHours( listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                    t.WORK_ITEM == WorkItemListBox.SelectedItem.ToString()).Select(t => t.WORKING_HOURS).Sum()).Value.ToString("F");
            }

            
        }

        /* Old TimerAdd
        public decimal TimerAdd(List<decimal> ListTimer)
        {
            decimal DecimalTotal = 0;

            
            foreach (decimal temp in ListTimer)
            {
                DecimalTotal = TimerAdd(DecimalTotal, temp);
            }          

            return DecimalTotal;
        }

        public decimal TimerAdd(decimal A, decimal B)
        {            
            decimal total = A + B;
            Decimal pd = (int)A + (int)B + 0.6M;
            if (total >= pd)
                total = total + 0.4M;
            return total;
        }
        */

        public decimal TimerAdd(List<decimal> ListTimer)
        {
            decimal DecimalTotal = 0;

            foreach (decimal temp in ListTimer)
            {
                DecimalTotal += temp;
            }
            return DecimalTotal;
        }

        public void SetEnableForAdminCheck()
        {

            #region old
            /*
            switch (WorkItemListBox.SelectedIndex)
            {
                case 0:
                    if (receiveingInformation.HOUAN_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 1:
                    if (receiveingInformation.KIBORI_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 2:
                    if (receiveingInformation.CAD_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 3:
                    if (receiveingInformation.CAM_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 4:
                    if (receiveingInformation.SHIYOUZU_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 5:
                    if (receiveingInformation.NC_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 6:
                    if (receiveingInformation.WIRECUT_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 7:
                    if (receiveingInformation.SHIAGE_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 8:
                    if (receiveingInformation.KENSAHYOU_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 9:
                    if (receiveingInformation.KENSA_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
            }
            */
            #endregion

            WorkTimeTodayTextBox.Enabled = !AdminCheckCheckBox.Checked;


        }

        public void SetValueForAdminCheck(bool HasValue)
        {
            switch (WorkItemListBox.SelectedIndex)
            {
                case 0:
                    if (HasValue)
                    {
                        receiveingInformation.HOUAN_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.HOUAN_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 1:
                    if (HasValue)
                    {
                        receiveingInformation.KIBORI_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.KIBORI_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 2:
                    if (HasValue)
                    {
                        receiveingInformation.CAD_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.CAD_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 3:
                    if (HasValue)
                    {
                        receiveingInformation.CAM_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.CAM_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 4:
                    if (HasValue)
                    {
                        receiveingInformation.SHIYOUZU_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.SHIYOUZU_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 5:
                    if (HasValue)
                    {
                        receiveingInformation.NC_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.NC_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 6:
                    if (HasValue)
                    {
                        receiveingInformation.WIRECUT_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.WIRECUT_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 7:
                    if (HasValue)
                    {
                        receiveingInformation.SHIAGE_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.SHIAGE_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 8:
                    if (HasValue)
                    {
                        receiveingInformation.KENSAHYOU_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.KENSAHYOU_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 9:
                    if (HasValue)
                    {
                        receiveingInformation.KENSA_CK_DT = DateTime.Now;
                        //WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        receiveingInformation.KENSA_CK_DT = null;
                        //WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
            }
        }
        
        public void GetValueForAdminCheck()
        {
            switch (WorkItemListBox.SelectedIndex)
            {
                case 0:
                    if (receiveingInformation.HOUAN_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 1:
                    if (receiveingInformation.KIBORI_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 2:
                    if (receiveingInformation.CAD_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 3:
                    if (receiveingInformation.CAM_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 4:
                    if (receiveingInformation.SHIYOUZU_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 5:
                    if (receiveingInformation.NC_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 6:
                    if (receiveingInformation.WIRECUT_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 7:
                    if (receiveingInformation.SHIAGE_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 8:
                    if (receiveingInformation.KENSAHYOU_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
                case 9:
                    if (receiveingInformation.KENSA_CK_DT != null)
                    {
                        AdminCheckCheckBox.Checked = true;
                        WorkTimeTodayTextBox.Enabled = false;
                    }
                    else
                    {
                        AdminCheckCheckBox.Checked = false;
                        WorkTimeTodayTextBox.Enabled = true;
                    }
                    break;
            }
        }

        public void SetEnableForCompleteCheckBox()
        {
            bool IsCompletedCheckCheckBoxEnabled = CompletedCheckCheckBox.Enabled;
            bool IsWorkerCodeTextBoxEnabled = WorkerCodeTextBox.Enabled;

            EnableFormItem(!CompletedCheckCheckBox.Checked);

            WorkItemListBox.Enabled = true;

            CompletedCheckCheckBox.Enabled = IsCompletedCheckCheckBoxEnabled;
            WorkerCodeTextBox.Enabled = IsWorkerCodeTextBoxEnabled;
        }

        public bool CheckWorkTimeToday(TextBox textBox)
        {
            decimal Number;
            
            try
            {
                Number = Convert.ToDecimal(textBox.Text.Replace(',', '.'));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ResourceUtil.Instance.GetString("INPUT_FORMAT_WRONG"));
                return false;
            }

            decimal Subtract = Number - (int)Number;

            if (Number - (int)Number > 0.59M || (Subtract.ToString().Length != 4 && Subtract.ToString().Length != 1))
            {
                MessageBox.Show(ResourceUtil.Instance.GetString("INPUT_FORMAT_WRONG"));
                return false;
            }

            return true;
        }

        public int? CheckInterger(TextBox Textbox)
        {
            try
            {
                if (string.IsNullOrEmpty(Textbox.Text))
                {
                    return null;
                }

                int Number = Convert.ToInt32(Textbox.Text);//.Replace(',', '.'));

                int Subtract = Number - (int)Number;
                if (Number > 99999  || Subtract.ToString().Length > 5)
                {
                    throw new Exception(""); 
                }

                return Number;
            }
            catch
            {
                Textbox.Focus();
                Textbox.SelectAll();
                throw new Exception(string.Format(ResourceUtil.Instance.GetString("INPUT_FORMAT_WRONG_WITH_FIELD"), Textbox.Name));
            }
        }

        public string CheckLenght(TextBox Textbox)
        {
            try
            {
                if (string.IsNullOrEmpty(Textbox.Text))
                {
                    return null;
                }

                if (Textbox.Text.Length > 10)
                    throw new Exception("");

                return Textbox.Text;
            }
            catch
            {
                Textbox.Focus();
                Textbox.SelectAll();
                //throw new Exception(string.Format(ResourceUtil.Instance.GetString("INPUT_FORMAT_WRONG_WITH_FIELD"), Textbox.Name));
                throw new Exception(string.Format("Len is not over 10", Textbox.Name));
            }
        }

        private void FillDataGridViewKyuzuList(List<InputWorkingTimeWS.SharePointKyuzuList> listKyuzu)
        {
            foreach (var item in listKyuzu)
            {
                int row = grdReceiNumber.Rows.Add();
                grdReceiNumber.Rows[row].Cells[Constant.RECEIVEING_NUMBER].Value = item.ReceiveingNumber;
                grdReceiNumber.Rows[row].Cells[Constant.PRODUCT_NUMBER].Value = item.ProductNumber;
                grdReceiNumber.Rows[row].Cells[Constant.PRODUCT_NAME].Value = item.ProductName;
                grdReceiNumber.Rows[row].Cells[Constant.CAR_TYPE].Value = item.TypeOfCar;
            }

        }

        private void FillDataGridViewWorkTimeList()
        {
            FillDataGridViewWorkTimeList(listWorkingTimeRecode);
        }

        private void FillDataGridViewWorkTimeList(List<InputWorkingTimeWS.WorkingTimeRecode> listWorkingTimeRecode)
        {
            WorkItemDataGridView.Rows.Clear();

            List<decimal> DecimalNumer = new List<decimal>();



            foreach (string Item in Common.ListWorkItem)
            {
                int Row = WorkItemDataGridView.Rows.Add();
                //------------------------------------- Work Item ---------------------------------------------
                WorkItemDataGridView.Rows[Row].Cells[0].Value = Item;


                //------------------------------------- Time each Person ---------------------------------------------
                int CurrentColForPerson = 1;
                int AddedExtraRow = 0;
                List<Common.CodeNameValue> ListPerWorkItem = GetListWorkerAndHours(Item);
                decimal? TotalWorkingHorsForEachWorker = 0;
                
                foreach (Common.CodeNameValue temp in ListPerWorkItem)
                {                                       
                    if (CurrentColForPerson == 9)
                    {
                        Row = WorkItemDataGridView.Rows.Add();
                        CurrentColForPerson = 1;
                        AddedExtraRow = 1;
                    }
                    
                    WorkItemDataGridView.Rows[Row].Cells[CurrentColForPerson].Value = temp.Name+":"+ Common.MinutesToHours(temp.Value).Value.ToString("F");
                    CurrentColForPerson++;
                    TotalWorkingHorsForEachWorker += temp.Value;
                }

                if ( AddedExtraRow == 0 && Item == "NC加工")
                {
                    Row = WorkItemDataGridView.Rows.Add();
                }

                //-------------------------------------- Total Working Hours -----------------------------------------------
                WorkItemDataGridView.Rows[Row].Cells[9].Value = Common.MinutesToHours(TotalWorkingHorsForEachWorker).Value.ToString("F");




                //-------------------------------------- Check   -----------------------------------------------
                WorkItemDataGridView.Rows[Row].Cells[10].Value = GetDateTimeForWorkingItem(Item, receiveingInformation) == null ? false : true;


                //-----------------------------------------
                DecimalNumer.Add(TotalWorkingHorsForEachWorker.Value);

            }

            RefillWorkTimeReceivingListBox(DecimalNumer);

        }

        public List<Common.CodeNameValue> GetListWorkerAndHours(string WorkItem)
        {
            List<Common.CodeNameValue> ReturnList = new List<Common.CodeNameValue>();
            List<string> ListWorkerForEachWorkItem = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                    t.WORK_ITEM == WorkItem).Select(t => t.WOKER_CD).Distinct().ToList();

            //total of 発注 is  ’方案' + '発注’ total?
            if (WorkItem == "発注")
            {
                List<string> ListTemp = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                   t.WORK_ITEM == "方案").Select(t => t.WOKER_CD).Distinct().ToList(); 

                ListWorkerForEachWorkItem.AddRange(ListTemp);
                ListWorkerForEachWorkItem = ListWorkerForEachWorkItem.Distinct().ToList();

               
            }
            else if (WorkItem == "材料取り")
            {
               List<string> ListTemp = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                 t.WORK_ITEM == "木取り").Select(t => t.WOKER_CD).Distinct().ToList();

               ListWorkerForEachWorkItem.AddRange(ListTemp);
               ListWorkerForEachWorkItem = ListWorkerForEachWorkItem.Distinct().ToList();
            }


            foreach (string WorkerCode in ListWorkerForEachWorkItem)
            {
                decimal tempWorkTimeTotalPerWorker = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();

                if (WorkItem == "発注")
                {
                    tempWorkTimeTotalPerWorker += listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                t.WORK_ITEM == "方案" && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();
                }
                else if (WorkItem == "材料取り")
                {
                    tempWorkTimeTotalPerWorker += listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                 t.WORK_ITEM == "木取り" && t.WOKER_CD == WorkerCode).Select(t => t.WORKING_HOURS).Sum();

                }

                //string Name = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                //t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).FirstOrDefault().WOKER_NM;

                string Name = "";
                InputWorkingTimeWS.WorkingTimeRecode tempWorkingTime = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                t.WORK_ITEM == WorkItem && t.WOKER_CD == WorkerCode).FirstOrDefault();
                if (tempWorkingTime != null)
                    Name = tempWorkingTime.WOKER_NM;
                else
                {
                    try 
                    {
                        if (WorkItem == "発注")
                        {
                            Name = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                            t.WORK_ITEM == "方案" && t.WOKER_CD == WorkerCode).First().WOKER_NM;
                        }
                        else if (WorkItem == "材料取り")
                        {
                            Name = listWorkingTimeRecode.Where(t => t.RECEIVEING_NUM == receiveingInformation.RECEIVEING_NUM &&
                         t.WORK_ITEM == "木取り" && t.WOKER_CD == WorkerCode).First().WOKER_NM;
                        }
                    }
                    catch  { }
                }
                ReturnList.Add(new Common.CodeNameValue(WorkerCode, Name, tempWorkTimeTotalPerWorker));
            }

            return ReturnList;
        }

        public DateTime? GetDateTimeForWorkingItem(string WorkingItem, InputWorkingTimeWS.ReceiveingInformation ReceiveingInformation)
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

        public void FillDataToFormAfterConfirm()
        {
            RefillWorkTimeListBox();
            RefillWorkTimeEachWorkerListBox();




            logger.Debug("Fill data to fields");


            CompleteDateDateTimePicker.CustomFormat = " ";
            CompleteDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            CompleteDateDateTimeUpdate = null;

            CADStartDateDateTimePicker.CustomFormat = " ";
            CADStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            CADStartDateDateTimeUpdate = null;

            ManufactureStartDateDateTimePicker.CustomFormat = " ";
            ManufactureStartDateDateTimePicker.Format = DateTimePickerFormat.Custom;
            ManufactureStartDateDateTimeUpdate = null;

            logger.Debug("Reicivei num = " + receiveingInformation.RECEIVEING_NUM);
            InputWorkingTimeWS.SharePointKyuzuList Temp = sharePointKyuzuList.Where(i => i.ReceiveingNumber == receiveingInformation.RECEIVEING_NUM).FirstOrDefault();
            if (Temp == null)
            {
                Temp = inputWorkingTimeWS.CheckRecieveExistInSP(receiveingInformation);
                if (Temp == null)
                {
                    logger.Debug("no Reiciveing SP return");
                    MessageBox.Show(ResourceUtil.Instance.GetString("NUMBER_DOESNT_EXIST_IN_SHAREPOINT_LIST"));
                    return;
                }
            }

            SetInfoToInfomationGroup(Temp.DeliveryDatePlan, receiveingInformation.COMPLETED_DT, receiveingInformation.CAD_START_DT, receiveingInformation.MANUFACTURE_START_DT,
                receiveingInformation.DATA_NUM, receiveingInformation.SIZE_W, receiveingInformation.SIZE_D, receiveingInformation.SIZE_H, receiveingInformation.WEIGHT);

            EnableFormItem(true);
            WorkDateDateTimePicker.Value = DateTime.Now;

            SetEnableToAdminAndCompleteCheckbox(true);

            
            SetEnableForAdminCheck();

            //re-check to call CompletedCheckCheckBox_CheckedChanged
            CompletedCheckCheckBox.Checked = false;
            CompletedCheckCheckBox.Checked = receiveingInformation.COMPLETED_CK_DT.HasValue;




            //set initial for Working Item
            SetWorkingItemToForm();
            GetValueForAdminCheck();
            
            

            if (CompletedCheckCheckBox.Checked)
                SetEnableForCompleteCheckBox();

            //Set disable Receiving Info Textbox and enable Submit click
            ReceivingNumberTextbox.Enabled = false;
            SubmitButton.Enabled = true;

            logger.Debug("Fill data to Work time List Data Grid");
            FillDataGridViewWorkTimeList();
            logger.Debug("End Fill data to fields");
        }

        private void ReceivingNumberTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{
            //    try
            //    {
            //        logger.Debug("Begin Enter click Receiving Info");
            //        if (!string.IsNullOrEmpty(ReceivingNumberTextbox.Text))
            //        {
            //            logger.Debug("Begin get Receiving Info");
            //            receiveingInformation = inputWorkingTimeWS.GetReceiveingInformation(ReceivingNumberTextbox.Text);

            //            if (receiveingInformation == null)
            //            {
            //                logger.Debug("no Reiciveing return");
            //                MessageBox.Show(ResourceUtil.Instance.GetString("NUMBER_DOESNT_EXIST_IN_DATABASE"));
            //                return;
            //            }

            //            logger.Debug("begin get List Working Time Recode");

            //            InputWorkingTimeWS.WorkingTimeRecode[] Temp = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
            //            listWorkingTimeRecode = Temp == null ? null : Temp.ToList();

            //            logger.Debug("begin get List Working Time Recode Backup");

            //            InputWorkingTimeWS.WorkingTimeRecode[] Temp1 = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
            //            listWorkingTimeRecodeBackup = Temp1 == null ? null : Temp1.ToList();


            //            FillDataToFormAfterConfirm();

            //            logger.Debug("Begin Enter click Receiving Info");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.Error("Error when Enter click Receiving Info: ", ex);
            //        MessageBox.Show(ex.Message);
            //    }

            //}

        }

        public void SetEnableToAdminAndCompleteCheckbox(bool IsCheckForCompleteCheckbox)
        {
            var adminItem = listSharePointWorkMasterList.Where(t => t.WorkerCode == WorkerCodeTextBox.Text).SingleOrDefault();


            if (adminItem != null && adminItem.AdministrationItem != null && (adminItem.AdministrationItem).Contains(WorkItemListBox.SelectedItem.ToString()))
            {
                AdminCheckCheckBox.Enabled = true;
            }
            else
            {
                AdminCheckCheckBox.Enabled = false;
            }

            if (IsCheckForCompleteCheckbox == true)
            {
                if (adminItem != null && adminItem.AdministrationItem != null && (adminItem.AdministrationItem).Contains("完了入力"))
                {
                    CompletedCheckCheckBox.Enabled = true;
                }
                else
                {
                    CompletedCheckCheckBox.Enabled = false;
                }
            }
        }

        public void RefillWorkTimeListBox()
        {
            decimal Sum = 0;

            WorkTimeListBox.Items.Clear();

            foreach (string item in Common.ListWorkItem)
            {
                var ItemValue = listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                    && i.WORK_ITEM == item).Sum(i => i.WORKING_HOURS);
   
                if (item == "発注")
                {
                    ItemValue += listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                    && i.WORK_ITEM == "方案").Sum(i => i.WORKING_HOURS);
                }
                else if (item == "材料取り")
                {
                    ItemValue += listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                    && i.WORK_ITEM == "木取り").Sum(i => i.WORKING_HOURS);
                }
                Sum += ItemValue;
                WorkTimeListBox.Items.Add(ItemValue == 0 ? "" : Common.MinutesToHours(ItemValue).Value.ToString("F"));
            }

            SumWorkTimeListTextBox.Text = Common.MinutesToHours(Sum).Value.ToString("F");
        }

        public void RefillWorkTimeEachWorkerListBox()
        {
            decimal Sum = 0;
            WorkTimeWorkerListBox.Items.Clear();

            foreach (string item in Common.ListWorkItem)
            {
                var ItemValue = listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                    && i.WORK_ITEM == item & i.WOKER_CD == WorkerCodeTextBox.Text).Sum(i => i.WORKING_HOURS);

                if (item == "発注")
                {
                    ItemValue += listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                     && i.WORK_ITEM == "方案" & i.WOKER_CD == WorkerCodeTextBox.Text).Sum(i => i.WORKING_HOURS);
                }
                else if (item == "材料取り")
                {
                    ItemValue += listWorkingTimeRecode.Where(i => i.WORK_DATE.Date == WorkDateDateTimePicker.Value.Date
                    && i.WORK_ITEM == "木取り" & i.WOKER_CD == WorkerCodeTextBox.Text).Sum(i => i.WORKING_HOURS);
                }

                Sum += ItemValue;

                WorkTimeWorkerListBox.Items.Add(ItemValue == 0 ? "" : Common.MinutesToHours(ItemValue).Value.ToString("F"));
            }
            SumWorkTimeEachWorkerListTextBox.Text = Common.MinutesToHours(Sum).Value.ToString("F");
        }

        public void RefillWorkTimeReceivingListBox(List<decimal> Number)
        {
            decimal Sum = 0;

            WorkTimeReceivingListBox.Items.Clear();

            foreach (decimal item in Number)
            {

                Sum += item;
                WorkTimeReceivingListBox.Items.Add(item == 0 ? "" : Common.MinutesToHours(item).Value.ToString("F"));
            }
            
            WorkTimeReceivingTextBox.Text = Common.MinutesToHours(Sum).Value.ToString("F");
        }

        private void WorkingTimeInputForm_Resize(object sender, EventArgs e)
        {
            WorkItemDataGridView.Width = this.ClientSize.Width - 23;  //50
           

            grdReceiNumber.Width = this.ClientSize.Width - 80;

            int temp = grdReceiNumber.Width == 910 ? 0 : (910 - grdReceiNumber.Width) / 4;

            this.ReceiveingNumber.Width = 195 - temp;
            this.CarType.Width = 170 - temp;
            this.ProductNumber.Width = 195 - temp;
            this.ProductName.Width = 295 - temp;


            //WorkItemDataGridView.Height = this.ClientSize.Height - 670;
            //grdReceiNumber.Height = this.ClientSize.Height - 697;


        }

        public void SelectReceiveNumber(string receivNumber)
        {
              try
            {
                logger.Debug("Begin click Receiving Number");

                   
                    logger.Debug("Get receiveingInformation from WS");
                    receiveingInformation = inputWorkingTimeWS.GetReceiveingInformation(receivNumber);
                    logger.Debug("Get done");
                    ReceivingNumberTextbox.Text = receivNumber;

                    //-------------------

                    logger.Debug("begin get List Working Time Recode");
                    InputWorkingTimeWS.WorkingTimeRecode[] Temp = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
                    listWorkingTimeRecode = Temp == null ? null : Temp.ToList();

                    logger.Debug("begin get List Working Time Recode Backup");
                    InputWorkingTimeWS.WorkingTimeRecode[] Temp1 = inputWorkingTimeWS.GetListWorkingTimeRecodeForForm(ReceivingNumberTextbox.Text);
                    listWorkingTimeRecodeBackup = Temp1 == null ? null : Temp1.ToList();

                    FillDataToFormAfterConfirm();
              
              }
            catch (Exception ex)
            {
                logger.Error("Error when click Receiving Number Grid: ", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckShowDateTime(CheckBox checkControl, DateTimePicker datepicker, ref DateTime? dateUpdate, DateTime? date)
        {
            if (checkControl.Checked)
            {
                datepicker.CustomFormat = " ";
                datepicker.Format = DateTimePickerFormat.Custom;
                dateUpdate = null;
            }
            else if (date.HasValue)
            {
                datepicker.Format = DateTimePickerFormat.Short;
                datepicker.Value = date.Value;
                dateUpdate = date;
            }
            else
            {
                datepicker.Format = DateTimePickerFormat.Short;
                datepicker.Value = DateTime.Today;
                dateUpdate = DateTime.Today;
            }
        }

        private void chkCompleteDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckShowDateTime(chkCompleteDate, CompleteDateDateTimePicker, ref CompleteDateDateTimeUpdate, receiveingInformation.COMPLETED_DT);
        }

        private void chkCADStartDat_CheckedChanged(object sender, EventArgs e)
        {
            CheckShowDateTime(chkCADStartDat, CADStartDateDateTimePicker, ref CADStartDateDateTimeUpdate, receiveingInformation.CAD_START_DT);
        }

        private void chkManufactureStartDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckShowDateTime(chkManufactureStartDate, ManufactureStartDateDateTimePicker, ref ManufactureStartDateDateTimeUpdate, receiveingInformation.MANUFACTURE_START_DT);
        }

        private void btnAddWorkTime_Click(object sender, EventArgs e)
        {
            WorkTimeTodayTextBox.Text = (Common.MinutesToHours(Common.HoursToMinutes(Convert.ToDecimal(WorkTimeTodayTextBox.Text.Replace(',', '.'))).Value + Common.HoursToMinutes(Convert.ToDecimal(txtAddWorkTime.Text))).Value.ToString("F"));
            WorkTimeTodayTextBox_Leave(sender, e);
            txtAddWorkTime.Text = "0";
            
        }

        private void WorkTimeTodayTextBox_EnabledChanged(object sender, EventArgs e)
        {
            txtAddWorkTime.Enabled = WorkTimeTodayTextBox.Enabled;
            btnAddWorkTime.Enabled = WorkTimeTodayTextBox.Enabled;
        }

        private void txtAddWorkTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddWorkTime.Text))
            {
                txtAddWorkTime.Text = "0";
            }
            if (!CheckWorkTimeToday(txtAddWorkTime))
            {
                txtAddWorkTime.Focus();
                return;
            }
        }

        private void IndexReceiveSelect(string receiveNum)
        {
            if(grdReceiNumber.SelectedRows.Count >0) grdReceiNumber.SelectedRows[0].Cells[0].Selected = false;
            foreach (DataGridViewRow reNum in grdReceiNumber.Rows)
            {
                if (reNum.Cells[Constant.RECEIVEING_NUMBER].Value.ToString() == receiveNum)
                {
                    reNum.Selected = true;
                    grdReceiNumber.CurrentCell = reNum.Cells[0];
                }
                
            }
        }

        private void ReceivingNumberLabel_Click(object sender, EventArgs e)
        {

        }

        private void InformationGroupBox_Enter(object sender, EventArgs e)
        {

        }
    }
}
