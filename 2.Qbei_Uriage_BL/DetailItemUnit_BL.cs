using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;
using _4.Qbei_Uriage_Common;
using System.Data;

namespace _2.Qbei_Uriage_BL
{
    public class DetailItemUnit_BL
    {
        DetailItemUnit_DL dl = new DetailItemUnit_DL();
        public DataTable DetailItemUnit_Search(DetailItemUnit_Entity de)
        {
            return dl.DetailItemUnit_Search(de);
        }
        public DataTable DetailItemUnit_SelectAll()
        {
            return dl.DetailItemUnit_SelectAll();
        }
    }
}
