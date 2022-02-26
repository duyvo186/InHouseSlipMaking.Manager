namespace F01_InputSalesInfomation
{
    partial class F01_InputSaleInfomation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtReceiveNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtPersoninChagre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDelivaryDay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.butConfirm = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.txtCarType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPersionChargeOfSale = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtOrtherModelType = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboModelType = new System.Windows.Forms.ComboBox();
            this.rdoOrtherModelType = new System.Windows.Forms.RadioButton();
            this.rdoModelType = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOrtherCatergory = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rdoOrderCatergory = new System.Windows.Forms.RadioButton();
            this.rdoCatergory = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.cboCatergory = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.txtKyuzu = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNumberOfKata = new System.Windows.Forms.TextBox();
            this.chkCancelFlag = new System.Windows.Forms.CheckBox();
            this.butSubmit = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.butDelete = new System.Windows.Forms.Button();
            this.chkDislayReceiveNum = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.grdReceiNumber = new System.Windows.Forms.DataGridView();
            this.ReceiveingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceiNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReceiveNumber
            // 
            this.txtReceiveNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtReceiveNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReceiveNumber.Location = new System.Drawing.Point(105, 18);
            this.txtReceiveNumber.Name = "txtReceiveNumber";
            this.txtReceiveNumber.Size = new System.Drawing.Size(213, 20);
            this.txtReceiveNumber.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "受入番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "受注先";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(105, 63);
            this.txtCompanyName.Multiline = true;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(213, 25);
            this.txtCompanyName.TabIndex = 3;
            // 
            // txtPersoninChagre
            // 
            this.txtPersoninChagre.Location = new System.Drawing.Point(105, 111);
            this.txtPersoninChagre.Multiline = true;
            this.txtPersoninChagre.Name = "txtPersoninChagre";
            this.txtPersoninChagre.Size = new System.Drawing.Size(159, 25);
            this.txtPersoninChagre.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "進行者";
            // 
            // txtProductNumber
            // 
            this.txtProductNumber.Location = new System.Drawing.Point(105, 157);
            this.txtProductNumber.Multiline = true;
            this.txtProductNumber.Name = "txtProductNumber";
            this.txtProductNumber.Size = new System.Drawing.Size(213, 55);
            this.txtProductNumber.TabIndex = 7;
            this.txtProductNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductNumber_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "品番";
            // 
            // txtDelivaryDay
            // 
            this.txtDelivaryDay.BackColor = System.Drawing.SystemColors.Window;
            this.txtDelivaryDay.Location = new System.Drawing.Point(106, 235);
            this.txtDelivaryDay.Multiline = true;
            this.txtDelivaryDay.Name = "txtDelivaryDay";
            this.txtDelivaryDay.Size = new System.Drawing.Size(136, 25);
            this.txtDelivaryDay.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "納期";
            // 
            // butConfirm
            // 
            this.butConfirm.Location = new System.Drawing.Point(341, 16);
            this.butConfirm.Name = "butConfirm";
            this.butConfirm.Size = new System.Drawing.Size(87, 23);
            this.butConfirm.TabIndex = 1;
            this.butConfirm.Text = "確認";
            this.butConfirm.UseVisualStyleBackColor = true;
            this.butConfirm.Click += new System.EventHandler(this.butConfirm_Click);
            // 
            // butClear
            // 
            this.butClear.Location = new System.Drawing.Point(449, 16);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(83, 23);
            this.butClear.TabIndex = 2;
            this.butClear.Text = "クリア";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // txtCarType
            // 
            this.txtCarType.Location = new System.Drawing.Point(398, 63);
            this.txtCarType.Multiline = true;
            this.txtCarType.Name = "txtCarType";
            this.txtCarType.Size = new System.Drawing.Size(159, 25);
            this.txtCarType.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(357, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "車系";
            // 
            // txtPersionChargeOfSale
            // 
            this.txtPersionChargeOfSale.Location = new System.Drawing.Point(398, 111);
            this.txtPersionChargeOfSale.Multiline = true;
            this.txtPersionChargeOfSale.Name = "txtPersionChargeOfSale";
            this.txtPersionChargeOfSale.Size = new System.Drawing.Size(159, 25);
            this.txtPersionChargeOfSale.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "営業担当者";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(357, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "品名";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(398, 156);
            this.txtProductName.Multiline = true;
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(213, 56);
            this.txtProductName.TabIndex = 8;
            this.txtProductName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductName_KeyPress);
            // 
            // txtOrtherModelType
            // 
            this.txtOrtherModelType.Location = new System.Drawing.Point(369, 7);
            this.txtOrtherModelType.Name = "txtOrtherModelType";
            this.txtOrtherModelType.Size = new System.Drawing.Size(159, 20);
            this.txtOrtherModelType.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(320, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "その他";
            // 
            // cboModelType
            // 
            this.cboModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModelType.FormattingEnabled = true;
            this.cboModelType.Location = new System.Drawing.Point(108, 5);
            this.cboModelType.Name = "cboModelType";
            this.cboModelType.Size = new System.Drawing.Size(151, 21);
            this.cboModelType.TabIndex = 16;
            // 
            // rdoOrtherModelType
            // 
            this.rdoOrtherModelType.AutoSize = true;
            this.rdoOrtherModelType.Location = new System.Drawing.Point(296, 11);
            this.rdoOrtherModelType.Name = "rdoOrtherModelType";
            this.rdoOrtherModelType.Size = new System.Drawing.Size(14, 13);
            this.rdoOrtherModelType.TabIndex = 17;
            this.rdoOrtherModelType.TabStop = true;
            this.rdoOrtherModelType.UseVisualStyleBackColor = true;
            // 
            // rdoModelType
            // 
            this.rdoModelType.AutoSize = true;
            this.rdoModelType.Location = new System.Drawing.Point(82, 9);
            this.rdoModelType.Name = "rdoModelType";
            this.rdoModelType.Size = new System.Drawing.Size(14, 13);
            this.rdoModelType.TabIndex = 15;
            this.rdoModelType.TabStop = true;
            this.rdoModelType.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "新設";
            // 
            // txtOrtherCatergory
            // 
            this.txtOrtherCatergory.Location = new System.Drawing.Point(372, 7);
            this.txtOrtherCatergory.Name = "txtOrtherCatergory";
            this.txtOrtherCatergory.Size = new System.Drawing.Size(159, 20);
            this.txtOrtherCatergory.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(323, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "その他";
            // 
            // rdoOrderCatergory
            // 
            this.rdoOrderCatergory.AutoSize = true;
            this.rdoOrderCatergory.Location = new System.Drawing.Point(299, 12);
            this.rdoOrderCatergory.Name = "rdoOrderCatergory";
            this.rdoOrderCatergory.Size = new System.Drawing.Size(14, 13);
            this.rdoOrderCatergory.TabIndex = 13;
            this.rdoOrderCatergory.TabStop = true;
            this.rdoOrderCatergory.UseVisualStyleBackColor = true;
            // 
            // rdoCatergory
            // 
            this.rdoCatergory.AutoSize = true;
            this.rdoCatergory.Location = new System.Drawing.Point(85, 10);
            this.rdoCatergory.Name = "rdoCatergory";
            this.rdoCatergory.Size = new System.Drawing.Size(14, 13);
            this.rdoCatergory.TabIndex = 11;
            this.rdoCatergory.TabStop = true;
            this.rdoCatergory.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "型名";
            // 
            // cboCatergory
            // 
            this.cboCatergory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCatergory.FormattingEnabled = true;
            this.cboCatergory.Location = new System.Drawing.Point(111, 7);
            this.cboCatergory.Name = "cboCatergory";
            this.cboCatergory.Size = new System.Drawing.Size(151, 21);
            this.cboCatergory.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboCatergory);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.rdoCatergory);
            this.panel1.Controls.Add(this.rdoOrderCatergory);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtOrtherCatergory);
            this.panel1.Location = new System.Drawing.Point(26, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 37);
            this.panel1.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtOrtherModelType);
            this.panel2.Controls.Add(this.rdoModelType);
            this.panel2.Controls.Add(this.rdoOrtherModelType);
            this.panel2.Controls.Add(this.cboModelType);
            this.panel2.Location = new System.Drawing.Point(29, 320);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(558, 37);
            this.panel2.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(47, 360);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 34;
            this.label13.Text = "旧図";
            // 
            // txtKyuzu
            // 
            this.txtKyuzu.Location = new System.Drawing.Point(105, 359);
            this.txtKyuzu.Multiline = true;
            this.txtKyuzu.Name = "txtKyuzu";
            this.txtKyuzu.Size = new System.Drawing.Size(213, 57);
            this.txtKyuzu.TabIndex = 19;
            this.txtKyuzu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKyuzu_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(352, 364);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "型数";
            // 
            // txtNumberOfKata
            // 
            this.txtNumberOfKata.Location = new System.Drawing.Point(398, 359);
            this.txtNumberOfKata.Name = "txtNumberOfKata";
            this.txtNumberOfKata.Size = new System.Drawing.Size(159, 20);
            this.txtNumberOfKata.TabIndex = 20;
            // 
            // chkCancelFlag
            // 
            this.chkCancelFlag.AutoSize = true;
            this.chkCancelFlag.Location = new System.Drawing.Point(365, 391);
            this.chkCancelFlag.Name = "chkCancelFlag";
            this.chkCancelFlag.Size = new System.Drawing.Size(50, 17);
            this.chkCancelFlag.TabIndex = 21;
            this.chkCancelFlag.Text = "中止";
            this.chkCancelFlag.UseVisualStyleBackColor = true;
            // 
            // butSubmit
            // 
            this.butSubmit.Location = new System.Drawing.Point(352, 424);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(87, 23);
            this.butSubmit.TabIndex = 22;
            this.butSubmit.Text = "登録";
            this.butSubmit.UseVisualStyleBackColor = true;
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(550, 424);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(87, 23);
            this.butCancel.TabIndex = 24;
            this.butCancel.Text = "キャンセル";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(451, 424);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(87, 23);
            this.butDelete.TabIndex = 23;
            this.butDelete.Text = "削除";
            this.butDelete.UseVisualStyleBackColor = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // chkDislayReceiveNum
            // 
            this.chkDislayReceiveNum.AutoSize = true;
            this.chkDislayReceiveNum.Checked = true;
            this.chkDislayReceiveNum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDislayReceiveNum.Location = new System.Drawing.Point(408, 462);
            this.chkDislayReceiveNum.Name = "chkDislayReceiveNum";
            this.chkDislayReceiveNum.Size = new System.Drawing.Size(189, 17);
            this.chkDislayReceiveNum.TabIndex = 25;
            this.chkDislayReceiveNum.Text = "中止された受入番号は表示しない";
            this.chkDislayReceiveNum.UseVisualStyleBackColor = true;
            this.chkDislayReceiveNum.CheckedChanged += new System.EventHandler(this.chkDislayReceiveNum_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 462);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "受入番号リスト";
            // 
            // grdReceiNumber
            // 
            this.grdReceiNumber.AllowUserToAddRows = false;
            this.grdReceiNumber.AllowUserToDeleteRows = false;
            this.grdReceiNumber.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReceiNumber.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiveingNumber,
            this.carType,
            this.productNumber,
            this.productName});
            this.grdReceiNumber.Location = new System.Drawing.Point(29, 485);
            this.grdReceiNumber.Name = "grdReceiNumber";
            this.grdReceiNumber.ReadOnly = true;
            this.grdReceiNumber.RowTemplate.Height = 21;
            this.grdReceiNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdReceiNumber.Size = new System.Drawing.Size(633, 231);
            this.grdReceiNumber.TabIndex = 26;
            this.grdReceiNumber.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReceiNumber_CellClick);
            // 
            // ReceiveingNumber
            // 
            this.ReceiveingNumber.DataPropertyName = "ReceiveingNumber";
            this.ReceiveingNumber.HeaderText = "受入番号";
            this.ReceiveingNumber.Name = "ReceiveingNumber";
            this.ReceiveingNumber.ReadOnly = true;
            this.ReceiveingNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ReceiveingNumber.Width = 90;
            // 
            // carType
            // 
            this.carType.DataPropertyName = "TypeOfCar";
            this.carType.HeaderText = "車系";
            this.carType.Name = "carType";
            this.carType.ReadOnly = true;
            this.carType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.carType.Width = 80;
            // 
            // productNumber
            // 
            this.productNumber.DataPropertyName = "ProductNumber";
            this.productNumber.HeaderText = "品番";
            this.productNumber.Name = "productNumber";
            this.productNumber.ReadOnly = true;
            this.productNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.productNumber.Width = 170;
            // 
            // productName
            // 
            this.productName.DataPropertyName = "ProductName";
            this.productName.HeaderText = "品名";
            this.productName.Name = "productName";
            this.productName.ReadOnly = true;
            this.productName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.productName.Width = 250;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // F01_InputSaleInfomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(692, 729);
            this.Controls.Add(this.grdReceiNumber);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.chkDislayReceiveNum);
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butSubmit);
            this.Controls.Add(this.chkCancelFlag);
            this.Controls.Add(this.txtNumberOfKata);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtKyuzu);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPersionChargeOfSale);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCarType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.butClear);
            this.Controls.Add(this.butConfirm);
            this.Controls.Add(this.txtDelivaryDay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProductNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPersoninChagre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReceiveNumber);
            this.Name = "F01_InputSaleInfomation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "営業入力";
            this.Load += new System.EventHandler(this.F01_InputSaleInfomation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceiNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReceiveNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TextBox txtPersoninChagre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDelivaryDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butConfirm;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.TextBox txtCarType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPersionChargeOfSale;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtOrtherModelType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboModelType;
        private System.Windows.Forms.RadioButton rdoOrtherModelType;
        private System.Windows.Forms.RadioButton rdoModelType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOrtherCatergory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rdoOrderCatergory;
        private System.Windows.Forms.RadioButton rdoCatergory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboCatergory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtKyuzu;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNumberOfKata;
        private System.Windows.Forms.CheckBox chkCancelFlag;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.CheckBox chkDislayReceiveNum;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView grdReceiNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn carType;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.Timer timer1;
    }
}