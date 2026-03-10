using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qbei_Uriage_Common
{
  public   class PackingFareMaster_Entity 
    {
      public string ID { get; set; }
        public string AccountTitle { get; set; }
        public string OrderType { get; set; }
        public string DeliveryCompanyCode { get; set; }
        public string RegionCode { get; set; }
        public string UnitPrice { get; set; }
        public string Expire_SDate { get; set; }
        public string Expire_EDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}
