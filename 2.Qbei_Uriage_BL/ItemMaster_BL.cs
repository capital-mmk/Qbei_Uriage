using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;

namespace _2.Qbei_Uriage_BL
{
    public class ItemMaster_BL
    {
        ItemMaster_DL dtmain = new ItemMaster_DL();
        public void Stage(string proName)
        {
            dtmain.Stage(proName);
        }

        public bool ItemMaster_InsertXml(DataTable dt)
        {
            dtmain.ItemMaster_InsertXml(dt);
            return true;
        }

        public DataTable GetItemMaster()
        {
            return dtmain.GetItemMaster();
        }
        public DataTable Item_SelectAll()
        {
            return dtmain.Item_SelectAll();
        }
        public DataTable ItemMaster_SelectAll()
        {
            return dtmain.ItemMaster_SelectAll();
        }
        public DataTable Item_Search(ItemEntity ie)
        {
            return dtmain.Item_Search(ie);
        }
        public DataTable ItemMaster_Search(ItemMaster_Entity ie)
        {
            return dtmain.ItemMaster_Search(ie);
        }
    }
}
