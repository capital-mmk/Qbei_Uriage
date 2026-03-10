using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.Qbei_Uriage_BL;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Qbei_Uriage_ExportConsole
{
    class Program
    {
        
        public static ItemMaster_BL im = new ItemMaster_BL();
        static void Main(string[] args)
        {
            Console.Title = "Qbei Uriage Export Console";
            
            //ExportExcel export = new ExportExcel();
            Uriage_Export ue = new Uriage_Export();

            //DataTable dtmain = new DataTable();
            //dtmain = im.GetItemMaster();
            //DataRow[] dr = dtmain.Select("Sales_Date IS NOT NULL"); //If 売上日 is blank, the row will be deleted.
            //if (dr.Count() > 0)
            //    dtmain = dtmain.Select("Sales_Date IS NOT NULL").CopyToDataTable();

            //dr = dtmain.Select("Cancel_Date = '' OR Cancel_Date IS NULL"); //If キャンセル日 exists, the row will be deleted.
            //if (dr.Count() > 0)
            //    dtmain = dtmain.Select("Cancel_Date = '' OR Cancel_Date IS NULL").CopyToDataTable();

            //var rowsToUpdate = dtmain.AsEnumerable().Where(r => Convert.ToInt32(r.Field<int>("UnitPrice")) < 0);

            //foreach (var row in rowsToUpdate)
            //{
            //    row.SetField("Quantity", DBNull.Value);
            //    row.SetField("Cost", DBNull.Value);
            //}
            //dtmain = dtmain.Select("Cancel_Date = '' OR Cancel_Date IS NULL").CopyToDataTable();

        }

      
    }
}
