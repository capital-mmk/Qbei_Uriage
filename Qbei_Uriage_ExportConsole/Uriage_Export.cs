using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _2.Qbei_Uriage_BL;
using System.IO;
using System.Configuration;

namespace Qbei_Uriage_ExportConsole
{
    /// <summary>
    /// Uriage_ExportConsole Process.
    /// </summary>
    public class Uriage_Export
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        ItemMaster_BL imbl = new ItemMaster_BL();

        /// <summary>
        /// Process of Stage.
        /// </summary>
        public Uriage_Export()
        {
            try
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Export Console : " + DateTime.Now);
                //delete row if sale Date(売上日) is empty
                //delete row if cancel date(キャンセル日が入っている) is not empty 
                ///25/10/2019<remark>
                ///Delete from ItemMaster of Data where SalesCategory is null or ''.
                ///</remark>
                imbl.Stage("Stage1");
                ConsoleWriteLine_Tofile("1.Stage1 Completed Sucessfully " + DateTime.Now);

                //split two tables(明細単位の情報 and 伝票単位の情報)
                ///25/10/2019<remark>
                ///Delete and Insert from UriageMeisai Table of Data.
                ///Delete and insert from UriageData Table of Data.
                ///Delete from UriageData Table of Data where TotalAmount is null.
                ///</remark>
                imbl.Stage("Stage2");
                ConsoleWriteLine_Tofile("2.Stage2 Completed Sucessfully " + DateTime.Now);

                //delete row on two tables(明細単位の情報 And ItemMaster) where 金額 is 0
                ///25/10/2019<remark>
                ///Delete from UriageMeisai Table of Data where Amount = 0.
                ///</remark>
                imbl.Stage("Stage3");
                ConsoleWriteLine_Tofile("3.Stage3 Completed Sucessfully " + DateTime.Now);

                //Set Null(blank) vlaue to 数量 and 原価 where 単価 <= 0
                ///25/10/2019<remark>
                ///Update dbo.UriageMeisai set Quantity = NULL, AverageCost = NULL, Cost = NULL, PurchaseCost = NULL Where SalesCategory = '値引' OR SalesCategory = '値引返品'.
                ///</remark>
                imbl.Stage("Stage4");
                ConsoleWriteLine_Tofile("4.Stage4 Completed Sucessfully " + DateTime.Now);

                //<remark Add Process for New ストアドプロシジャ 2021/11/09>
                //Back up to UriageMeisai_BK table from UriageMeisai Table and other process.
                //</remark>
                imbl.Stage("Stage4_5");
                ConsoleWriteLine_Tofile("4_5.Stage4_5 Completed Sucessfully " + DateTime.Now);

                /* Stage5
                 * Recombine 明細単位の情報 AND 伝票単位の情報 by using 受注番号 as Key
                 * No need to do something special,just use ItemMaster table.
                */
                ///25/10/2019<remark>
                ///Delete from Uriage Table of Data.
                ///Insert to Uriage Table of Data from UriageMeisai left outer join UriageData on UriageData.ID=UriageMeisai.ID.
                ///</remark>
                imbl.Stage("Stage5");
                ConsoleWriteLine_Tofile("5.Stage5 Completed Sucessfully " + DateTime.Now);


                ///25/10/2019<remark>
                ///Update ItemMaster table join with Item,Category,Brand tables by using 自社品番 as Key
                ///</remark>
                imbl.Stage("Stage6");
                ConsoleWriteLine_Tofile("6.Stage6 Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Delete rows on ItemMaster which does not exist in Branch table By using BranchCode.
                ///Select data from Uriage Table and Brand Table where Uriage.BranchCode = Branch.BranchCode.
                ///</remark>              
                imbl.Stage("Stage7");
                ConsoleWriteLine_Tofile("7.Stage7 Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Create and Select temp1 Table and Select orderno from Uriage Table where Site_Category_URL(Group By).
                ///Create and Select temp2 Table and Select orderno from Uriage Table where Site_Category_URL.
                ///Update Uriage Table  SET SalesCategory1 and SalesCategory2 where WHERE OrderNo (select Data from temp2 Table(select orderno from temp2 Table)).
                ///Create and Select temp3 Table from (SELECT Orderno FROM temp2 Table WHERE NOT EXISTS(SELECT OrderNO FROM temp1 Table WHERE temp1.OrderNo = temp2.OrderNo)).
                ///Update Uriage Table  SET SalesCategory1 and SalesCategory2 WHERE OrderNo IN( select OrderNo from temp3 Table).
                ///Drop table to temp1,temp2,temp3.
                ///</remark>
                imbl.Stage("Stage8a");
                ConsoleWriteLine_Tofile("8_a.Stage8a Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Update Uriage Table SET SalesCategory1,SalesCategory2 WHERE Site_Category_URL,BrandUrl.
                ///</remark>
                imbl.Stage("Stage8b");
                ConsoleWriteLine_Tofile("8_b.Stage8b Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Update Uriage Table SET SalesCategory1,SalesCategory2 WHERE Site_Category_URL.
                ///</remark>
                imbl.Stage("Stage8c");
                ConsoleWriteLine_Tofile("8_c.Stage8c Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Update Uriage Table SET SalesCategory1,SalesCategory2 WHERE Site_Category_URL.
                ///</remark>
                imbl.Stage("Stage8d");
                ConsoleWriteLine_Tofile("8_d.Stage8d Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Delete to Uriage Table SET SalesCategory1 is null and SalesCategory2 is null.
                ///</remark>
                imbl.Stage("Stage8e");
                ConsoleWriteLine_Tofile("8_e.Stage8e Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Update Uriage Table SET  Coupon = null , UsagePoint = null From Uriage INNER JOIN Branch ON Uriage.BranchCode = Branch.BranchCode  Where Branch.StoreCategory = 'モール'.
                ///</remark>
                imbl.Stage("Stage9");
                ConsoleWriteLine_Tofile("9.Stage9 Completed Sucessfully " + DateTime.Now);

                ///25/10/2019<remark>
                ///Update Uriage Table SET (inner join select  Data from uriage table to temp Table) on temp.OrderNo = Uriage.OrderNo AND(Uriage.SalesCategory2 != N'防犯登録').
                ///</remark>
                imbl.Stage("Stage10");
                ConsoleWriteLine_Tofile("10.Stage10 Completed Sucessfully " + DateTime.Now);
            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Export Console :" + ex.ToString() + " " + DateTime.Now);
            }
        }
        
        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Uriage_Export_Console.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

    }
}
