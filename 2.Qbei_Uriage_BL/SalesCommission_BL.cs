using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using Qbei_Uriage_Common;
using _3.Qbei_Uriage_DL;

namespace Qbei_Uriage_BL
{
   public class SalesCommission_BL
    {
       SalesCommission_DL sdl = new SalesCommission_DL();
       public bool SalesCommission_Insert(SalesCommission_Entity se)
       {
           return sdl.SalesCommission_Insert(se);
       }
       public bool SalesCommissionMaster_Delete(SalesCommission_Entity se)
       {
           return sdl.SalesCommissionMaster_Delete(se);
       }

       public DataTable SalesCommissionMaster_Select(SalesCommission_Entity se)
       {
           return sdl.SalesCommissionMaster_Select(se);
       }
       public bool SalesCommission_Update(SalesCommission_Entity se)
       {
           return sdl.SalesCommission_Update(se);
       }
       //public DataTable SalesCommission_Select(QbeiUser_Entity qe)
       //{
       //    QbeiUser_DL qudl = new QbeiUser_DL();
       //    return qudl.SalesCommission_Select(qe);
       //}
       public DataTable SalesCommission_SelectAll()
       {
           return sdl.SalesCommission_SelectAll();
       }
    }
}
