using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InHouseSlipMaking.Domain
{
    public class SharePointWorkMasterList
    {
        public string WorkerCode { get; set; }
        public string WorkerName { get; set; }
        public List<string> AdministrationItem  { get; set; }

        public SharePointWorkMasterList() { }
    }
}
