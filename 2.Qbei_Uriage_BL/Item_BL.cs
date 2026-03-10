using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;

namespace _2.Qbei_Uriage_BL
{
    public class Item_BL
    {
        Item_DL item = new Item_DL();
        public bool Item_InsertXml(DataTable dt)
        {
            item.Item_InsertXml(dt);
            return true;
        }

        public bool ItemSKU_InsertXml(DataTable dt)
        {
            item.ItemSKU_InsertXml(dt);
            return true;
        }
    }
}
