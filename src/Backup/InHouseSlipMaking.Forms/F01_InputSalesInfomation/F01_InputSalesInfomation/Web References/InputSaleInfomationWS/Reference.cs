﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace F01_InputSalesInfomation.InputSaleInfomationWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="InputSaleInfomationWSSoap", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseDomainOfReceiveingInformation))]
    public partial class InputSaleInfomationWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetListReceiveingNumberOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetListReceiveingInformationOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetReceiveingInformationByReceivingNumberOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertAndUpdateReceivingNumberToSPandDBOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteReceivingNumberToSPandDBOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public InputSaleInfomationWS() {
            this.Url = global::F01_InputSalesInfomation.Properties.Settings.Default.F01_InputSalesInfomation_InputSaleInfomationWS_InputSaleInfomationWS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetListReceiveingNumberCompletedEventHandler GetListReceiveingNumberCompleted;
        
        /// <remarks/>
        public event GetListReceiveingInformationCompletedEventHandler GetListReceiveingInformationCompleted;
        
        /// <remarks/>
        public event GetReceiveingInformationByReceivingNumberCompletedEventHandler GetReceiveingInformationByReceivingNumberCompleted;
        
        /// <remarks/>
        public event InsertAndUpdateReceivingNumberToSPandDBCompletedEventHandler InsertAndUpdateReceivingNumberToSPandDBCompleted;
        
        /// <remarks/>
        public event DeleteReceivingNumberToSPandDBCompletedEventHandler DeleteReceivingNumberToSPandDBCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetListReceiveingNumber", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SharePointKyuzuList[] GetListReceiveingNumber() {
            object[] results = this.Invoke("GetListReceiveingNumber", new object[0]);
            return ((SharePointKyuzuList[])(results[0]));
        }
        
        /// <remarks/>
        public void GetListReceiveingNumberAsync() {
            this.GetListReceiveingNumberAsync(null);
        }
        
        /// <remarks/>
        public void GetListReceiveingNumberAsync(object userState) {
            if ((this.GetListReceiveingNumberOperationCompleted == null)) {
                this.GetListReceiveingNumberOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListReceiveingNumberOperationCompleted);
            }
            this.InvokeAsync("GetListReceiveingNumber", new object[0], this.GetListReceiveingNumberOperationCompleted, userState);
        }
        
        private void OnGetListReceiveingNumberOperationCompleted(object arg) {
            if ((this.GetListReceiveingNumberCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListReceiveingNumberCompleted(this, new GetListReceiveingNumberCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetListReceiveingInformation", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ReceiveingInformation[] GetListReceiveingInformation() {
            object[] results = this.Invoke("GetListReceiveingInformation", new object[0]);
            return ((ReceiveingInformation[])(results[0]));
        }
        
        /// <remarks/>
        public void GetListReceiveingInformationAsync() {
            this.GetListReceiveingInformationAsync(null);
        }
        
        /// <remarks/>
        public void GetListReceiveingInformationAsync(object userState) {
            if ((this.GetListReceiveingInformationOperationCompleted == null)) {
                this.GetListReceiveingInformationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListReceiveingInformationOperationCompleted);
            }
            this.InvokeAsync("GetListReceiveingInformation", new object[0], this.GetListReceiveingInformationOperationCompleted, userState);
        }
        
        private void OnGetListReceiveingInformationOperationCompleted(object arg) {
            if ((this.GetListReceiveingInformationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListReceiveingInformationCompleted(this, new GetListReceiveingInformationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetReceiveingInformationByReceivingNumber", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ReceiveingInformation GetReceiveingInformationByReceivingNumber(string rcvNumber) {
            object[] results = this.Invoke("GetReceiveingInformationByReceivingNumber", new object[] {
                        rcvNumber});
            return ((ReceiveingInformation)(results[0]));
        }
        
        /// <remarks/>
        public void GetReceiveingInformationByReceivingNumberAsync(string rcvNumber) {
            this.GetReceiveingInformationByReceivingNumberAsync(rcvNumber, null);
        }
        
        /// <remarks/>
        public void GetReceiveingInformationByReceivingNumberAsync(string rcvNumber, object userState) {
            if ((this.GetReceiveingInformationByReceivingNumberOperationCompleted == null)) {
                this.GetReceiveingInformationByReceivingNumberOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetReceiveingInformationByReceivingNumberOperationCompleted);
            }
            this.InvokeAsync("GetReceiveingInformationByReceivingNumber", new object[] {
                        rcvNumber}, this.GetReceiveingInformationByReceivingNumberOperationCompleted, userState);
        }
        
        private void OnGetReceiveingInformationByReceivingNumberOperationCompleted(object arg) {
            if ((this.GetReceiveingInformationByReceivingNumberCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetReceiveingInformationByReceivingNumberCompleted(this, new GetReceiveingInformationByReceivingNumberCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertAndUpdateReceivingNumberToSPandDB", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InsertAndUpdateReceivingNumberToSPandDB(SharePointKyuzuList spKyuzuList) {
            this.Invoke("InsertAndUpdateReceivingNumberToSPandDB", new object[] {
                        spKyuzuList});
        }
        
        /// <remarks/>
        public void InsertAndUpdateReceivingNumberToSPandDBAsync(SharePointKyuzuList spKyuzuList) {
            this.InsertAndUpdateReceivingNumberToSPandDBAsync(spKyuzuList, null);
        }
        
        /// <remarks/>
        public void InsertAndUpdateReceivingNumberToSPandDBAsync(SharePointKyuzuList spKyuzuList, object userState) {
            if ((this.InsertAndUpdateReceivingNumberToSPandDBOperationCompleted == null)) {
                this.InsertAndUpdateReceivingNumberToSPandDBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertAndUpdateReceivingNumberToSPandDBOperationCompleted);
            }
            this.InvokeAsync("InsertAndUpdateReceivingNumberToSPandDB", new object[] {
                        spKyuzuList}, this.InsertAndUpdateReceivingNumberToSPandDBOperationCompleted, userState);
        }
        
        private void OnInsertAndUpdateReceivingNumberToSPandDBOperationCompleted(object arg) {
            if ((this.InsertAndUpdateReceivingNumberToSPandDBCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertAndUpdateReceivingNumberToSPandDBCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteReceivingNumberToSPandDB", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteReceivingNumberToSPandDB(string receivingNumber) {
            this.Invoke("DeleteReceivingNumberToSPandDB", new object[] {
                        receivingNumber});
        }
        
        /// <remarks/>
        public void DeleteReceivingNumberToSPandDBAsync(string receivingNumber) {
            this.DeleteReceivingNumberToSPandDBAsync(receivingNumber, null);
        }
        
        /// <remarks/>
        public void DeleteReceivingNumberToSPandDBAsync(string receivingNumber, object userState) {
            if ((this.DeleteReceivingNumberToSPandDBOperationCompleted == null)) {
                this.DeleteReceivingNumberToSPandDBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteReceivingNumberToSPandDBOperationCompleted);
            }
            this.InvokeAsync("DeleteReceivingNumberToSPandDB", new object[] {
                        receivingNumber}, this.DeleteReceivingNumberToSPandDBOperationCompleted, userState);
        }
        
        private void OnDeleteReceivingNumberToSPandDBOperationCompleted(object arg) {
            if ((this.DeleteReceivingNumberToSPandDBCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteReceivingNumberToSPandDBCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class SharePointKyuzuList {
        
        private string receiveingNumberField;
        
        private string persionInChangeOfSalesField;
        
        private string typeOfCarField;
        
        private string symbolField;
        
        private string companyNameField;
        
        private string productNumberField;
        
        private string productNameField;
        
        private string typeOfModelField;
        
        private string personInChargeField;
        
        private string categotyField;
        
        private string deliveryDatePlanField;
        
        private string personInChargeOfHouanField;
        
        private string personInChargeOfCADField;
        
        private string manHourField;
        
        private System.Nullable<System.DateTime> deliveryDateActialField;
        
        private string remarkField;
        
        private string numberOfKataField;
        
        private string kyuzuField;
        
        private string cancelFlagField;
        
        private System.Guid idField;
        
        /// <remarks/>
        public string ReceiveingNumber {
            get {
                return this.receiveingNumberField;
            }
            set {
                this.receiveingNumberField = value;
            }
        }
        
        /// <remarks/>
        public string PersionInChangeOfSales {
            get {
                return this.persionInChangeOfSalesField;
            }
            set {
                this.persionInChangeOfSalesField = value;
            }
        }
        
        /// <remarks/>
        public string TypeOfCar {
            get {
                return this.typeOfCarField;
            }
            set {
                this.typeOfCarField = value;
            }
        }
        
        /// <remarks/>
        public string Symbol {
            get {
                return this.symbolField;
            }
            set {
                this.symbolField = value;
            }
        }
        
        /// <remarks/>
        public string CompanyName {
            get {
                return this.companyNameField;
            }
            set {
                this.companyNameField = value;
            }
        }
        
        /// <remarks/>
        public string ProductNumber {
            get {
                return this.productNumberField;
            }
            set {
                this.productNumberField = value;
            }
        }
        
        /// <remarks/>
        public string ProductName {
            get {
                return this.productNameField;
            }
            set {
                this.productNameField = value;
            }
        }
        
        /// <remarks/>
        public string TypeOfModel {
            get {
                return this.typeOfModelField;
            }
            set {
                this.typeOfModelField = value;
            }
        }
        
        /// <remarks/>
        public string PersonInCharge {
            get {
                return this.personInChargeField;
            }
            set {
                this.personInChargeField = value;
            }
        }
        
        /// <remarks/>
        public string Categoty {
            get {
                return this.categotyField;
            }
            set {
                this.categotyField = value;
            }
        }
        
        /// <remarks/>
        public string DeliveryDatePlan {
            get {
                return this.deliveryDatePlanField;
            }
            set {
                this.deliveryDatePlanField = value;
            }
        }
        
        /// <remarks/>
        public string PersonInChargeOfHouan {
            get {
                return this.personInChargeOfHouanField;
            }
            set {
                this.personInChargeOfHouanField = value;
            }
        }
        
        /// <remarks/>
        public string PersonInChargeOfCAD {
            get {
                return this.personInChargeOfCADField;
            }
            set {
                this.personInChargeOfCADField = value;
            }
        }
        
        /// <remarks/>
        public string ManHour {
            get {
                return this.manHourField;
            }
            set {
                this.manHourField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> DeliveryDateActial {
            get {
                return this.deliveryDateActialField;
            }
            set {
                this.deliveryDateActialField = value;
            }
        }
        
        /// <remarks/>
        public string Remark {
            get {
                return this.remarkField;
            }
            set {
                this.remarkField = value;
            }
        }
        
        /// <remarks/>
        public string NumberOfKata {
            get {
                return this.numberOfKataField;
            }
            set {
                this.numberOfKataField = value;
            }
        }
        
        /// <remarks/>
        public string Kyuzu {
            get {
                return this.kyuzuField;
            }
            set {
                this.kyuzuField = value;
            }
        }
        
        /// <remarks/>
        public string CancelFlag {
            get {
                return this.cancelFlagField;
            }
            set {
                this.cancelFlagField = value;
            }
        }
        
        /// <remarks/>
        public System.Guid Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReceiveingInformation))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class BaseDomainOfReceiveingInformation {
        
        private string checkField;
        
        /// <remarks/>
        public string Check {
            get {
                return this.checkField;
            }
            set {
                this.checkField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ReceiveingInformation : BaseDomainOfReceiveingInformation {
        
        private string rECEIVEING_NUMField;
        
        private string dATA_NUMField;
        
        private string sIZE_WField;
        
        private string sIZE_DField;
        
        private string sIZE_HField;
        
        private string wEIGHTField;
        
        private System.Nullable<System.DateTime> cOMPLETED_DTField;
        
        private System.Nullable<System.DateTime> cAD_START_DTField;
        
        private System.Nullable<System.DateTime> mANUFACTURE_START_DTField;
        
        private System.Nullable<System.DateTime> hOUAN_CK_DTField;
        
        private System.Nullable<System.DateTime> kIBORI_CK_DTField;
        
        private System.Nullable<System.DateTime> cAD_CK_DTField;
        
        private System.Nullable<System.DateTime> cAM_CK_DTField;
        
        private System.Nullable<System.DateTime> sHIYOUZU_CK_DTField;
        
        private System.Nullable<System.DateTime> nC_CK_DTField;
        
        private System.Nullable<System.DateTime> wIRECUT_CK_DTField;
        
        private System.Nullable<System.DateTime> sHIAGE_CK_DTField;
        
        private System.Nullable<System.DateTime> kENSAHYOU_CK_DTField;
        
        private System.Nullable<System.DateTime> kENSA_CK_DTField;
        
        private System.Nullable<System.DateTime> cOMPLETED_CK_DTField;
        
        private string cANCEL_FLAGField;
        
        private string uPDATE_FLAGField;
        
        private string uPD_WOKER_CDField;
        
        private System.DateTime uPD_DTField;
        
        /// <remarks/>
        public string RECEIVEING_NUM {
            get {
                return this.rECEIVEING_NUMField;
            }
            set {
                this.rECEIVEING_NUMField = value;
            }
        }
        
        /// <remarks/>
        public string DATA_NUM {
            get {
                return this.dATA_NUMField;
            }
            set {
                this.dATA_NUMField = value;
            }
        }
        
        /// <remarks/>
        public string SIZE_W {
            get {
                return this.sIZE_WField;
            }
            set {
                this.sIZE_WField = value;
            }
        }
        
        /// <remarks/>
        public string SIZE_D {
            get {
                return this.sIZE_DField;
            }
            set {
                this.sIZE_DField = value;
            }
        }
        
        /// <remarks/>
        public string SIZE_H {
            get {
                return this.sIZE_HField;
            }
            set {
                this.sIZE_HField = value;
            }
        }
        
        /// <remarks/>
        public string WEIGHT {
            get {
                return this.wEIGHTField;
            }
            set {
                this.wEIGHTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> COMPLETED_DT {
            get {
                return this.cOMPLETED_DTField;
            }
            set {
                this.cOMPLETED_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CAD_START_DT {
            get {
                return this.cAD_START_DTField;
            }
            set {
                this.cAD_START_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> MANUFACTURE_START_DT {
            get {
                return this.mANUFACTURE_START_DTField;
            }
            set {
                this.mANUFACTURE_START_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> HOUAN_CK_DT {
            get {
                return this.hOUAN_CK_DTField;
            }
            set {
                this.hOUAN_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> KIBORI_CK_DT {
            get {
                return this.kIBORI_CK_DTField;
            }
            set {
                this.kIBORI_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CAD_CK_DT {
            get {
                return this.cAD_CK_DTField;
            }
            set {
                this.cAD_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CAM_CK_DT {
            get {
                return this.cAM_CK_DTField;
            }
            set {
                this.cAM_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> SHIYOUZU_CK_DT {
            get {
                return this.sHIYOUZU_CK_DTField;
            }
            set {
                this.sHIYOUZU_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> NC_CK_DT {
            get {
                return this.nC_CK_DTField;
            }
            set {
                this.nC_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> WIRECUT_CK_DT {
            get {
                return this.wIRECUT_CK_DTField;
            }
            set {
                this.wIRECUT_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> SHIAGE_CK_DT {
            get {
                return this.sHIAGE_CK_DTField;
            }
            set {
                this.sHIAGE_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> KENSAHYOU_CK_DT {
            get {
                return this.kENSAHYOU_CK_DTField;
            }
            set {
                this.kENSAHYOU_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> KENSA_CK_DT {
            get {
                return this.kENSA_CK_DTField;
            }
            set {
                this.kENSA_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> COMPLETED_CK_DT {
            get {
                return this.cOMPLETED_CK_DTField;
            }
            set {
                this.cOMPLETED_CK_DTField = value;
            }
        }
        
        /// <remarks/>
        public string CANCEL_FLAG {
            get {
                return this.cANCEL_FLAGField;
            }
            set {
                this.cANCEL_FLAGField = value;
            }
        }
        
        /// <remarks/>
        public string UPDATE_FLAG {
            get {
                return this.uPDATE_FLAGField;
            }
            set {
                this.uPDATE_FLAGField = value;
            }
        }
        
        /// <remarks/>
        public string UPD_WOKER_CD {
            get {
                return this.uPD_WOKER_CDField;
            }
            set {
                this.uPD_WOKER_CDField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime UPD_DT {
            get {
                return this.uPD_DTField;
            }
            set {
                this.uPD_DTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetListReceiveingNumberCompletedEventHandler(object sender, GetListReceiveingNumberCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListReceiveingNumberCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListReceiveingNumberCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SharePointKyuzuList[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SharePointKyuzuList[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetListReceiveingInformationCompletedEventHandler(object sender, GetListReceiveingInformationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetListReceiveingInformationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetListReceiveingInformationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReceiveingInformation[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReceiveingInformation[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetReceiveingInformationByReceivingNumberCompletedEventHandler(object sender, GetReceiveingInformationByReceivingNumberCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetReceiveingInformationByReceivingNumberCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetReceiveingInformationByReceivingNumberCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReceiveingInformation Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReceiveingInformation)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void InsertAndUpdateReceivingNumberToSPandDBCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void DeleteReceivingNumberToSPandDBCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591