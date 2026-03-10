using System.Data;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;

namespace _2.Qbei_Uriage_BL
{
    public class Brand_BL
    {
        Brand_DL brand = new Brand_DL();
        public bool Brand_InsertXml(DataTable dt)
        {
            brand.Brand_InsertXml(dt);
            return true;
        }
        public DataTable brand_SelectAll()
        {
            return brand.brand_SelectAll();
        }
        public DataTable Brand_Search(BrandEntity be)
        {
            return brand.Brand_Search(be);
        }
    }
}
