using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Qbei_Uriage_Common;
using _3.Qbei_Uriage_DL;

namespace Qbei_Uriage_BL
{
    public class WorkingDays_BL
    {
        WorkingDays_DL dalWD;

        public WorkingDays_BL()
        {
            dalWD = new WorkingDays_DL();
        }

        public DataTable WorkingDays_SelectAll()
        {
            return dalWD.WorkingDays_SelectAll();
        }

        public DataTable WorkingDays_Select(WorkingDays_Entity objParam)
        {
            return dalWD.WorkingDays_Select(objParam);
        }

        public bool WorkingDays_Delete(WorkingDays_Entity objParam)
        {
            return dalWD.WorkingDays_Delete(objParam);
        }

        public bool WorkingDays_Insert(WorkingDays_Entity objParam)
        {
            return dalWD.WorkingDays_Insert(objParam);
        }

        public bool WorkingDays_Update(WorkingDays_Entity objParam)
        {
            return dalWD.WorkingDays_Update(objParam);
        }
    }
}
