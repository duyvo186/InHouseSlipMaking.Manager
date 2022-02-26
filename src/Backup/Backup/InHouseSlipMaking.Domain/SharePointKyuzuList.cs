using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InHouseSlipMaking.Domain
{
    public class SharePointKyuzuList
    {

        public string ReceiveingNumber { get; set; }
        public string PersionInChangeOfSales { get; set; }
        public string TypeOfCar { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string TypeOfModel { get; set; }
        public string PersonInCharge { get; set; }
        public string Categoty { get; set; }
        public string DeliveryDatePlan { get; set; }
        public string PersonInChargeOfHouan { get; set; }
        public string PersonInChargeOfCAD { get; set; }
        public string ManHour { get; set; }
        public DateTime? DeliveryDateActial { get; set; }
        public string Remark { get; set; }
        public string NumberOfKata { get; set; }
        public string Kyuzu { get; set; }

        public string CancelFlag { get; set; }
        public Guid Id { get; set; }

        public SharePointKyuzuList() { }
    }
}
