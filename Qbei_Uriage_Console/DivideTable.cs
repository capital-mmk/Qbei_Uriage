using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.Qbei_Uriage_BL; 
using _3.Qbei_Uriage_DL; 
using _4.Qbei_Uriage_Common;

namespace Qbei_Uriage_Console
{
      
  public  class DivideTable
    {
        public static Category_Honten_BL Cht = new Category_Honten_BL();

        public static Brand_BL brand = new Brand_BL();

        public static ItemMaster_BL im = new ItemMaster_BL();
      
        public static Item_BL item = new Item_BL();

        public static Site_Category_BL sc = new Site_Category_BL();

      public DataTable spitup(DataTable dttemp)
      {
       
         

          var totalRows = dttemp.Rows.Count;

          int no_Divide = 0;

          if (totalRows < 70000)
          {
              no_Divide = totalRows/1;

              var firstHalf = dttemp.AsEnumerable().Take(no_Divide).CopyToDataTable();

              im.ItemMaster_InsertXml(firstHalf);


          }
          else 
          {
              no_Divide = totalRows/2;

              var firstHalf = dttemp.AsEnumerable().Take(no_Divide).CopyToDataTable();


              im.ItemMaster_InsertXml(firstHalf);

              var secondHalf = dttemp.AsEnumerable().Skip(no_Divide).Take(totalRows - no_Divide).CopyToDataTable();

              im.ItemMaster_InsertXml(secondHalf);

          }
          //else if (totalRows > 170000 && totalRows < 270000)
          //{
          //    no_Divide = totalRows/3;

          //    var firstHalf = dttemp.AsEnumerable().Take(no_Divide).CopyToDataTable();


          //    im.ItemMaster_InsertXml(firstHalf);


          //    var secondHalf = dttemp.AsEnumerable().Skip(no_Divide).Take(totalRows - no_Divide).CopyToDataTable();

          //    im.ItemMaster_InsertXml(secondHalf);

          //}
          //else if (totalRows > 270000)
          //{
          //    no_Divide = totalRows/4;
          //}
          //else
          //{

          //}

          //for (int i = 1; i <= no_Divide; i++)
          //{

              //var firstHalf = dttemp.AsEnumerable().Take(no_Divide).CopyToDataTable();

              //im.ItemMaster_InsertXml(firstHalf);

          //}

          //var halfway = totalRows / 2;


          //var firstHalf = dttemp.AsEnumerable().Take(halfway).CopyToDataTable();

          //im.ItemMaster_InsertXml(firstHalf);

          //var secondHalf = dttemp.AsEnumerable().Skip(halfway).Take(totalRows - halfway).CopyToDataTable();

          //im.ItemMaster_InsertXml(secondHalf);

          return new DataTable();
      }

      public void  separater(DataTable dt, int no)
  {
      var secondHalf = new DataTable();
      var a = 0; var c = 0;
      a = dt.Rows.Count / no;

      for (int b = 0; b < no; b++)
      {
          if (b == 0)
          {
               secondHalf = dt.AsEnumerable().Take(a).CopyToDataTable();
          }
          else
          {
              c = +a;
               secondHalf = dt.AsEnumerable().Skip(c).Take(a).CopyToDataTable();
          }
          im.ItemMaster_InsertXml(secondHalf);
         
      }

  }

    }
}
