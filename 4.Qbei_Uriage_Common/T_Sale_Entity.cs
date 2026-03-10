using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Qbei_Uriage_Common
{
    public class T_Sale_Entity
    {
        public int intSaleId { get; set; }
        public DateTime dtSaleDate { get; set; }
        public int intBranchCode { get; set; }
        public int intPaymentType { get; set; }
        public Decimal decAmount { get; set; }
        public int intPackingID { get; set; }
        public int intOutsourcingID { get; set; }
        public int intSalesCommissionID { get; set; }
        public int intCommercialTaxID { get; set; }
        public int intInsuranceID { get; set; }
        public int intFixedCostID { get; set; }
        public int intHolidayID { get; set; }
        public int intQTY { get; set; }
        public Decimal decUnitPrice { get; set; }
        public string strCostType { get; set; }
        public bool bolFinFlag { get; set; }
        public Decimal decOverwrite_Amt { get; set; }
        public int intCreatedBy { get; set; }
        public int intModifiedBy { get; set; }
        public string strShopCategory { get; set; }
        public List<T_Sale_Entity> lstSale { get; set; }
        //
        public string strAccTitle { get; set; }
        //
    }
}
