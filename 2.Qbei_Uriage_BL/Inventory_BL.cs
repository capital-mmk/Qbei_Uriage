using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;

namespace _2.Qbei_Uriage_BL
{
    public class Inventory_BL
    {
        Inventory_DL Inventory = new Inventory_DL();
        public bool Inventory_InsertXML(DataTable dt)
        {
            Inventory.Inventory_InsertXML(dt);
            return true;
        }
    }
}
