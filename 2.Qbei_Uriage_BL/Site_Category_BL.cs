using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;

namespace _2.Qbei_Uriage_BL
{
    public class Site_Category_BL
    {
        Site_Category_DL sc = new Site_Category_DL();
        public bool SiteCategory_InsertXml(DataTable dt)
        {
            sc.SiteCategory_InsertXml(dt);
            return true;
        }
    }
}
