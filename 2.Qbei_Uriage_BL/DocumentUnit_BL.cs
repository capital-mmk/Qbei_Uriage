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
   public class DocumentUnit_BL
    {
       DocumentUnit_DL dl = new DocumentUnit_DL();
        public DataTable DocumentUnit_Search(DocumentUnit_Entity de)
        {
            return dl.DocumentUnit_Search(de);
        }
        public DataTable DocumentUnit_SelectAll()
        {
            return dl.DocumentUnit_SelectAll();
        }
    }
}