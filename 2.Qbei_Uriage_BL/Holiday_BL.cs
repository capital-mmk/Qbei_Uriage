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
    public class Holiday_BL
    {
        Holiday_DL dalHoliday;

        public Holiday_BL()
        {
            dalHoliday = new Holiday_DL();
        }

        public DataTable Holiday_SelectAll()
        {
            return dalHoliday.Holiday_SelectAll();
        }

        public DataTable Holiday_Select(Holiday_Entity objParam)
        {
            return dalHoliday.Holiday_Select(objParam);
        }

        public bool Holiday_Delete(Holiday_Entity objParam)
        {
            return dalHoliday.Holiday_Delete(objParam);
        }

        public bool Holiday_Insert(Holiday_Entity objParam)
        {
            return dalHoliday.Holiday_Insert(objParam);
        }

        public bool Holiday_Update(Holiday_Entity objParam)
        {
            return dalHoliday.Holiday_Update(objParam);
        }
    }
}
