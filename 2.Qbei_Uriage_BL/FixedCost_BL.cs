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
    public class FixedCost_BL
    {
        FixedCost_DL dalFixedC;

        public FixedCost_BL()
        {
            dalFixedC = new FixedCost_DL();
        }

        public DataTable FixedCost_SelectAll()
        {
            return dalFixedC.FixedCost_SelectAll();
        }

        public DataTable FixedCost_Select(FixedCost_Entity objParam)
        {
            return dalFixedC.FixedCost_Select(objParam);
        }

        public DataTable ShopCategory_Select()
        {
            return dalFixedC.ShopCategory_Select();
        }

        public bool FixedCost_Delete(FixedCost_Entity objParam)
        {
            return dalFixedC.FixedCost_Delete(objParam);
        }

        public bool FixedCost_Insert (FixedCost_Entity objParam)
        {
            return dalFixedC.FixedCost_Insert(objParam);
        }

        public bool FixedCost_Update(FixedCost_Entity objParam)
        {
            return dalFixedC.FixedCost_Update(objParam);
        }


    }
}
