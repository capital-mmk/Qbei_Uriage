using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qbei_Uriage_Common;
using _3.Qbei_Uriage_DL;
using System.Data;

namespace Qbei_Uriage_BL
{
   public class CommercialTax_BL
    {
        public CommercialTax_DL cdl = new CommercialTax_DL();
        public bool CommercialTax_Insert(CommercialTax_Entity ce)
        {
            return cdl.CommercialTax_Insert(ce);
        }
        public bool CommercialTax_Delete(CommercialTax_Entity ce)
        {
            return cdl.CommercialTax_Delete(ce);
        }
        public DataTable CommercialTaxMaster_Select(CommercialTax_Entity ce)
        {
            return cdl.CommercialTaxMaster_Select(ce);
        }

        public bool CommercialTax_Update(CommercialTax_Entity ce)
        {
            return cdl.CommercialTax_Update(ce);
        }

        public DataTable CommercialTax_SelectAll()
        {
            return cdl.CommercialTax_SelectAll();
        }
    }
}
