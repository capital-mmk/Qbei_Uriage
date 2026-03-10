using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _3.Qbei_Uriage_DL;

namespace _2.Qbei_Uriage_BL
{
    public class Category_Honten_BL
    {
        Category_Honten_DL Cht = new Category_Honten_DL();
        public bool Category_Honten_InsertXml(DataTable dt)
        {
            Cht.Category_Honten_InsertXML(dt);
            return true;
        }
        
    }
}
