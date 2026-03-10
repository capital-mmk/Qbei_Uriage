using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using LumenWorks.Framework.IO.Csv;
using _2.Qbei_Uriage_BL;
using System.Collections;
using System.Data.SqlClient;
using Spire.Xls;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Xml;
using SplittingDT;
using System.Net;//<remark Add Logic for Download at FTP Server 2021/08/02 />

namespace Qbei_Uriage_Console
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <remark>
    /// Data Table and Common Function and Field
    /// </remark>
    class Program
    {
        static string consoleWriteLinePath = ConfigurationManager.AppSettings["ConsoleWriteLinePath"].ToString();
        public static Category_Honten_BL Cht = new Category_Honten_BL();
        public static Brand_BL brand = new Brand_BL();
        public static ItemMaster_BL im = new ItemMaster_BL();
        public static Item_BL item = new Item_BL();
        public static Site_Category_BL sc = new Site_Category_BL();
        public static Inventory_BL inventory = new Inventory_BL();//<remark Add Logic for Inventory 2020/08/07 />

        /// <summary>
        /// DataBase Connection.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Qbei Uriage Input Console";
                ConsoleWriteLine_Tofile("Qbei Uriage Input Console : " + DateTime.Now);
                DataTable dt_ItemMaster = new DataTable();
                SqlCommand cmd;
                SqlConnection con;
                SqlDataAdapter adp;
                try
                {
                    //string MyConnection = "Data Source=DESKTOP-5B8MLGG;Initial Catalog=Qbei_Uriage;User ID=sa;Password=12345;Integrated Security=True";
                    string MyConnection = "Data Source= 133.167.121.63, 1433;Initial Catalog=Qbei_Uriage;Persist Security Info=True;User ID=sa;Password=manager@0";
                    con = new SqlConnection(MyConnection);
                    con.Open();
                    cmd = new SqlCommand("Select * from ItemMaster", con);
                    cmd.CommandTimeout = 0;
                    adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_ItemMaster);
                    con.Close();
                }
                catch (Exception ex)
                {
                    con = new SqlConnection("");

                    ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
                }
                //string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード" };

                ///9/9/2019<Remark>
                ///SKU列を　追加。
                ///9/9/2019</Remark>
                //string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "SKU", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード" };

                ///30/04/2020<Remark>
                ///セット識別フラグ列を　追加。 
                //string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "セット識別フラグ", "SKU", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード" };
                ///30/04/2020</Remark>

                ///06/10/2020<Remark>
                ///送り状番号列を　追加。 
                //string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "セット識別フラグ", "SKU", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード", "送り状番号" };
                ///06/10/2020</Remark>

                ///08/11/2021<Remark>
                ///出荷場所（出荷元コード）, 配送先氏名, 伝票番号, 配送備考2の 列を　追加。 
                //string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "セット識別フラグ", "SKU", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード", "送り状番号", "出荷場所（出荷元コード）", "配送先氏名", "伝票番号", "配送備考2" };
                ///08/10/2020</Remark>

                ///14/12/2021<Remark>
                ///顧客名,顧客名カナ,県コード,県名,Emailの 列を　追加。 
                string[] strmain = new string[] { "売上日", "受注番号", "自社品番", "セット識別フラグ", "SKU", "単価", "数量", "標準原価", "金額", "クーポン", "値引き", "送料", "消費税", "総合計金額", "店名", "代引手数料", "使用ポイント", "付加ポイント", "支店コード", "キャンセル日", "返品手数料", "売上区分", "移動平均原価", "最終仕入原価", "支払方法コード", "配送先県コード", "配送会社コード", "送り状番号", "出荷場所（出荷元コード）", "配送先氏名", "伝票番号", "配送備考2", "顧客名", "顧客名カナ", "県コード", "県名", "Email" };
                ///14/12/2020</Remark>

                DataTable dttemp = new DataTable();
                DataTable dtmain = new DataTable();

                foreach (string col in strmain)
                {
                    dttemp.Columns.Add(col);
                }
                DataColumn Date = new DataColumn("Date", typeof(Int32));

                ///<remark>
                ///Check to year of DataDate.
                ///</remark>
                if (dt_ItemMaster.Rows.Count > 0)
                {
                    ///<remark>
                    ///For 2 years.
                    ///</remark>
                    #region 2 years
                    int Year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    //con.Open();//<Close Logic for Delet to this year 2022/02/14 />
                    Year -= 1;
                    //SqlCommand command = new SqlCommand("Delete from ItemMaster where Date =" + Year + " or " + "Date = " + (Year + 1), con);
                    //SqlCommand command = new SqlCommand("Delete from ItemMaster where Date = " + (Year + 1), con);//<Edit Logic for Delet to this year 2022/01/11 />//<Close Logic for Delet to this year 2022/02/14 />
                    //command.CommandTimeout = 0;//<Close Logic for Delet to this year 2022/02/14 />
                    //command.ExecuteNonQuery();//<Close Logic for Delet to this year 2022/02/14 />
                    //con.Close();//<Close Logic for Delet to this year 2022/02/14 />
                    //DownloadFile("qbei-suruzo-etl-csv", Year + ".zip");
                    Ftp_Download("order/", Year + ".zip", "jutyu_meisai_list_" + Year + ".csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />

                    dtmain = GetDatatable("jutyu_meisai_list_" + Year + ".csv");
                    //TestAndAddColumn(strmain, dtmain);

                    //  CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                    ///9/9/2019<Remark>
                    ///SKU列を　追加。
                    ///9/9/2019</Remark>
                    //  CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                    ///30/04/2020<Remark>
                    ///セット識別フラグ列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");
                    ///30/04/2020</Remark>

                    ///06/10/2020<Remark>
                    ///送り状番号列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号");
                    ///06/10/2020</Remark>

                    ///08/11/2021<Remark>
                    ///出荷場所（出荷元コード）列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号", "出荷場所（出荷元コード）", "出荷場所（出荷元コード）",
                    //"配送先氏名", "配送先氏名", "伝票番号", "伝票番号", "配送備考2", "配送備考2");
                    ///08/11/2021</Remark>

                    //<remark Close Logic 2022/01/11 Start>

                    ///14/12/2021<Remark>
                    ///顧客名,顧客名カナ,県コード,県名,Email 列を　追加。                   
                    CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    "単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号", "出荷場所（出荷元コード）", "出荷場所（出荷元コード）",
                    "配送先氏名", "配送先氏名", "伝票番号", "伝票番号", "配送備考2", "配送備考2", "顧客名", "顧客名", "顧客名カナ", "顧客名カナ", "県コード", "県コード", "県名", "県名", "Email", "Email");
                    ///14/12/2021</Remark>

                    Date.DefaultValue = Year;
                    dttemp.Columns.Add(Date);
                    dttemp.Columns["出荷場所（出荷元コード）"].ColumnName = "出荷場所_出荷元コード";//<remark Add Logic for Change of Column Name at Table 2021/08/12 />

                    //<remark Add Logic for Insert Process to this year 2022/02/14 Start>
                    //string insert_date_1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    //var check_data_1 = dttemp.Select("売上日 Like '" + insert_date_1 + "%'").Count();//<remark Add Logic for check date of data at CSV 2022/05/10 />



                    //<remark Add Logic for Insert Process to this year 2024/03/21 Start>
                    string insert_date_1 = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    var check_data_1 = dttemp.Select(String.Format("売上日 >= #{0}# ", insert_date_1)).Count();

                    if (check_data_1 != 0)//<remark Add Logic for check date of data at CSV 2022/05/10 />
                    {
                        DataTable daily_update_1 = new DataTable();
                        daily_update_1 = dttemp.Select(String.Format("売上日 >= #{0}# ", insert_date_1)).CopyToDataTable();
                        //</remark 2022/02/14 End>

                        //<remark Add Logic for 受注番号 is not exhist into ItemMaster table to this year 2024/03/21 Start>
                        var leftJoinQuery = from rowA in daily_update_1.AsEnumerable()
                                            join rowB in dt_ItemMaster.AsEnumerable()
                                            on rowA.Field<string>("受注番号") equals rowB.Field<string>("OrderNo") into joinedTable
                                            from rowB in joinedTable.DefaultIfEmpty()
                                            where rowB == null
                                            select new
                                            {
                                                売上日 = rowA.Field<string>("売上日"),//DateTime.TryParseExact(rowA.Field<string>("売上日"), "yyyy-MM-dd" ,null, System.Globalization.DateTimeStyles.None, out resultDateTime) ? resultDateTime ,
                                                受注番号 = rowA.Field<string>("受注番号"),
                                                自社品番 = rowA.Field<string>("自社品番"),
                                                セット識別フラグ = rowA.Field<string>("セット識別フラグ"),
                                                SKU = rowA.Field<string>("SKU"),
                                                単価 = rowA.Field<string>("単価"),
                                                数量 = rowA.Field<string>("数量"),
                                                標準原価 = rowA.Field<string>("標準原価"),
                                                金額 = rowA.Field<string>("金額"),
                                                クーポン = rowA.Field<string>("クーポン"),
                                                値引き = rowA.Field<string>("値引き"),
                                                送料 = rowA.Field<string>("送料"),
                                                消費税 = rowA.Field<string>("消費税"),
                                                総合計金額 = rowA.Field<string>("総合計金額"),
                                                店名 = rowA.Field<string>("店名"),
                                                代引手数料 = rowA.Field<string>("代引手数料"),
                                                使用ポイント = rowA.Field<string>("使用ポイント"),
                                                付加ポイント = rowA.Field<string>("付加ポイント"),
                                                支店コード = rowA.Field<string>("支店コード"),
                                                キャンセル日 = rowA.Field<string>("キャンセル日"),
                                                返品手数料 = rowA.Field<string>("返品手数料"),
                                                売上区分 = rowA.Field<string>("売上区分"),
                                                移動平均原価 = rowA.Field<string>("移動平均原価"),
                                                最終仕入原価 = rowA.Field<string>("最終仕入原価"),
                                                支払方法コード = rowA.Field<string>("支払方法コード"),
                                                配送先県コード = rowA.Field<string>("配送先県コード"),
                                                配送会社コード = rowA.Field<string>("配送会社コード"),
                                                送り状番号 = rowA.Field<string>("送り状番号"),
                                                出荷場所_出荷元コード = rowA.Field<string>("出荷場所_出荷元コード"),
                                                配送先氏名 = rowA.Field<string>("配送先氏名"),
                                                伝票番号 = rowA.Field<string>("伝票番号"),
                                                配送備考2 = rowA.Field<string>("配送備考2"),
                                                顧客名 = rowA.Field<string>("顧客名"),
                                                顧客名カナ = rowA.Field<string>("顧客名カナ"),
                                                県コード = rowA.Field<string>("県コード"),
                                                県名 = rowA.Field<string>("県名"),
                                                Email = rowA.Field<string>("Email"),
                                                Date = rowA.Field<Int32>("Date"),
                                            };
                        //</remark 2024/03/21 End>
                        //Convert result to DataTable
                        DataTable resultDataTable = ToDataTable(leftJoinQuery);

                        ////<remark RoWNo列 の追加　2021/08/23 Start>
                        DataTable RoWNo = new DataTable();
                        DataColumn AutoNumberColumn = new DataColumn();
                        AutoNumberColumn.ColumnName = "RoWNo";
                        AutoNumberColumn.DataType = typeof(int);
                        AutoNumberColumn.AutoIncrement = true;
                        AutoNumberColumn.AutoIncrementSeed = 1;
                        AutoNumberColumn.AutoIncrementStep = 1;
                        RoWNo.Columns.Add(AutoNumberColumn);
                        //RoWNo.Merge(dttemp);
                        RoWNo.Merge(resultDataTable);//</remark Edit Logic for Insert Process to this year 2022/05/06 />
                                                    //</remark 2021/08/23 End>

                        //</remark 2022/01/11 End>

                        //DivideTable dtb = new DivideTable();

                        //      var totalRows = dttemp.Rows.Count;

                        //var      halfway = totalRows / 10000;
                        //      var firstHalf = dttemp.AsEnumerable().Take(halfway).CopyToDataTable();

                        //<remark Open Logic 2022/05/06 Start>
                        Chuncked cuk = new Chuncked();

                        var l_dt = cuk.Separate(RoWNo, 20000);
                        foreach (DataTable sub_dt2017 in l_dt)
                        {
                            im.ItemMaster_InsertXml(sub_dt2017);
                        }
                        RoWNo.Clear();//<remark Open Logic 2022/05/06 />
                    }
                    //</remark 2022/05/06 End>

                    /* knz modified 2019/01/02 start*/
                    //ConsoleWriteLine_Tofile("1. ItemMaster Data for 2017 completed: " + DateTime.Now);
                    ConsoleWriteLine_Tofile("1. ItemMaster Data for " + Year + " completed: " + DateTime.Now);
                    /* knz modified 2019/01/02 end*/
                    //dtb.separater(dttemp, 1);

                    //var totalRows = dttemp.Rows.Count;

                    //var halfway = totalRows / 4;


                    //var firstHalf = dttemp.AsEnumerable().Take(halfway).CopyToDataTable();

                    //im.ItemMaster_InsertXml(firstHalf);
                    //ConsoleWriteLine_Tofile("1_1.First Half ItemMaster Data for 2017 completed: " + DateTime.Now);
                    //var secondHalf = dttemp.AsEnumerable().Skip(halfway).Take(totalRows - halfway).CopyToDataTable();
                    //ConsoleWriteLine_Tofile("1_2.Second Half ItemMaster Data for 2017 completed: " + DateTime.Now);
                    //im.ItemMaster_InsertXml(secondHalf);


                    //int a=0;

                    //   for (int b=0; b < )
                    //   {
                    //   }

                    dttemp.Columns.Remove("Date"); //<remark Open Logic 2022/05/06 />

                    dtmain.Clear();
                    dttemp.Clear();
                    //RoWNo.Clear();//<remark Open Logic 2022/05/06 />

                    Year += 1;
                    //DownloadFile("qbei-suruzo-etl-csv", Year + ".zip");
                    Ftp_Download("order/", Year + ".zip", "jutyu_meisai_list_" + Year + ".csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                    dtmain = GetDatatable("jutyu_meisai_list_" + Year + ".csv");
                    dtmain = Remove_Doublecode(dtmain);
                    //int count = dtmain.Rows.Count;
                    //dtmain = GETtable("jutyu_meisai_list_" + Year + ".csv");
                    //TestAndAddColumn(strmain, dtmain);

                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                    ///9/9/2019<Remark>
                    ///SKU列を　追加。
                    ///9/9/2019</Remark>
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                    ///30/04/2020<Remark>
                    ///セット識別フラグ列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");
                    ///30/04/2020</Remark>

                    ///06/10/2020<Remark>
                    ///送り状番号列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号");
                    ///06/10/2020</Remark>

                    dttemp.Columns["出荷場所_出荷元コード"].ColumnName = "出荷場所（出荷元コード）";//<remark Open Logic 2022/05/06 />

                    ///08/11/2021<Remark>
                    ///出荷場所（出荷元コード）, 配送先氏名, 伝票番号, 配送備考2の 列を　追加。                   
                    //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号", "出荷場所（出荷元コード）", "出荷場所（出荷元コード）",
                    //"配送先氏名", "配送先氏名", "伝票番号", "伝票番号", "配送備考2", "配送備考2");
                    ///08/11/2021</Remark>

                    ///14/12/2021<Remark>
                    ///顧客名,顧客名カナ,県コード,県名,Email 列を　追加。                   
                    CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                    "単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号", "出荷場所（出荷元コード）", "出荷場所（出荷元コード）",
                    "配送先氏名", "配送先氏名", "伝票番号", "伝票番号", "配送備考2", "配送備考2", "顧客名", "顧客名", "顧客名カナ", "顧客名カナ", "県コード", "県コード", "県名", "県名", "Email", "Email");
                    ///14/12/2021</Remark>

                    Date.DefaultValue = Year;
                    dttemp.Columns.Add(Date);

                    dttemp.Columns["出荷場所（出荷元コード）"].ColumnName = "出荷場所_出荷元コード";//<remark Add Logic for Change of Column Name at Table 2021/08/12 />

                    //<remark Add Logic for Insert Process to this year 2022/02/14 Start>
                    //string insert_date_2 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");     
                    //var check_date_2 = dttemp.Select("売上日 Like '" + insert_date_2 + "%'").Count();//<remark Add Logic for check date of data at CSV 2022/05/10 />

                    //<remark Add Logic for Insert Process to this year 2024/03/21 Start>
                    string insert_date_2 = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    var check_date_2 = dttemp.Select(String.Format("売上日 >= #{0}# ", insert_date_2)).Count();

                    if (check_date_2 != 0)//<remark Add Logic for check date of data at CSV 2022/05/10 />
                    {
                        DataTable daily_update_2 = new DataTable();
                        daily_update_2 = dttemp.Select(String.Format("売上日 >= #{0}# ", insert_date_2)).CopyToDataTable();
                        //</remark 2022/02/14 End>

                        //<remark Add Logic for 受注番号 is not exhist into ItemMaster table to this year 2024/03/21 Start>
                        var leftJoinQuery = from rowA in daily_update_2.AsEnumerable()
                                            join rowB in dt_ItemMaster.AsEnumerable()
                                            on rowA.Field<string>("受注番号") equals rowB.Field<string>("OrderNo") into joinedTable
                                            from rowB in joinedTable.DefaultIfEmpty()
                                            where rowB == null
                                            select new
                                            {
                                                売上日 = rowA.Field<string>("売上日"),//DateTime.TryParseExact(rowA.Field<string>("売上日"), "yyyy-MM-dd" ,null, System.Globalization.DateTimeStyles.None, out resultDateTime) ? resultDateTime ,
                                                受注番号 = rowA.Field<string>("受注番号"),
                                                自社品番 = rowA.Field<string>("自社品番"),
                                                セット識別フラグ = rowA.Field<string>("セット識別フラグ"),
                                                SKU = rowA.Field<string>("SKU"),
                                                単価 = rowA.Field<string>("単価"),
                                                数量 = rowA.Field<string>("数量"),
                                                標準原価 = rowA.Field<string>("標準原価"),
                                                金額 = rowA.Field<string>("金額"),
                                                クーポン = rowA.Field<string>("クーポン"),
                                                値引き = rowA.Field<string>("値引き"),
                                                送料 = rowA.Field<string>("送料"),
                                                消費税 = rowA.Field<string>("消費税"),
                                                総合計金額 = rowA.Field<string>("総合計金額"),
                                                店名 = rowA.Field<string>("店名"),
                                                代引手数料 = rowA.Field<string>("代引手数料"),
                                                使用ポイント = rowA.Field<string>("使用ポイント"),
                                                付加ポイント = rowA.Field<string>("付加ポイント"),
                                                支店コード = rowA.Field<string>("支店コード"),
                                                キャンセル日 = rowA.Field<string>("キャンセル日"),
                                                返品手数料 = rowA.Field<string>("返品手数料"),
                                                売上区分 = rowA.Field<string>("売上区分"),
                                                移動平均原価 = rowA.Field<string>("移動平均原価"),
                                                最終仕入原価 = rowA.Field<string>("最終仕入原価"),
                                                支払方法コード = rowA.Field<string>("支払方法コード"),
                                                配送先県コード = rowA.Field<string>("配送先県コード"),
                                                配送会社コード = rowA.Field<string>("配送会社コード"),
                                                送り状番号 = rowA.Field<string>("送り状番号"),
                                                出荷場所_出荷元コード = rowA.Field<string>("出荷場所_出荷元コード"),
                                                配送先氏名 = rowA.Field<string>("配送先氏名"),
                                                伝票番号 = rowA.Field<string>("伝票番号"),
                                                配送備考2 = rowA.Field<string>("配送備考2"),
                                                顧客名 = rowA.Field<string>("顧客名"),
                                                顧客名カナ = rowA.Field<string>("顧客名カナ"),
                                                県コード = rowA.Field<string>("県コード"),
                                                県名 = rowA.Field<string>("県名"),
                                                Email = rowA.Field<string>("Email"),
                                                Date = rowA.Field<Int32>("Date"),
                                            };
                        //</remark 2024/03/21 End>
                        //Convert result to DataTable
                        DataTable resultDataTable = ToDataTable(leftJoinQuery);
                        //<remark RoWNo_2列 の追加　2021/08/23 Start>
                        DataTable RoWNo_2 = new DataTable();
                        DataColumn AutoNumberColumn_2 = new DataColumn();
                        AutoNumberColumn_2.ColumnName = "RoWNo";
                        AutoNumberColumn_2.DataType = typeof(int);
                        AutoNumberColumn_2.AutoIncrement = true;
                        AutoNumberColumn_2.AutoIncrementSeed = 1;
                        AutoNumberColumn_2.AutoIncrementStep = 1;
                        RoWNo_2.Columns.Add(AutoNumberColumn_2);
                        //RoWNo_2.Merge(dttemp);
                        RoWNo_2.Merge(resultDataTable);//<Edit Logic for Insert Process to this year 2022/02/14 />
                                                      //</remark 2021/08/23 End>

                        Chuncked chk = new Chuncked();
                        var co_dt = chk.Separate(RoWNo_2, 20000);
                        foreach (DataTable sub_dt2018 in co_dt)
                        {
                            im.ItemMaster_InsertXml(sub_dt2018);
                        }
                        RoWNo_2.Clear();//<remark RoWNo列 2021/08/23 />
                    }
                    /* knz modified 2019/01/02 start*/
                    //ConsoleWriteLine_Tofile("2. ItemMaster Data for 2018 completed: " + DateTime.Now);
                    ConsoleWriteLine_Tofile("2. ItemMaster Data for " + Year + " completed: " + DateTime.Now);
                    /* knz modified 2019/01/02 end*/

                    //im.ItemMaster_InsertXml(dttemp);


                    //dtb.separater(dttemp, 1);

                    // totalRows = dttemp.Rows.Count;

                    // halfway = totalRows / 2;


                    //firstHalf = dttemp.AsEnumerable().Take(halfway).CopyToDataTable();

                    //im.ItemMaster_InsertXml(firstHalf);
                    //ConsoleWriteLine_Tofile("2_1.First Half ItemMaster Data for 2018 completed: " + DateTime.Now);
                    //secondHalf = dttemp.AsEnumerable().Skip(halfway).Take(totalRows - halfway).CopyToDataTable();

                    //im.ItemMaster_InsertXml(secondHalf);
                    //ConsoleWriteLine_Tofile("2_2.Second Half ItemMaster Data for 2018 completed: " + DateTime.Now);
                    //dttemp.Columns.Remove("Date");
                    dtmain.Clear();
                    dttemp.Clear();
                    //RoWNo_2.Clear();//<remark RoWNo列 2021/08/23 />
                    #endregion
                }
                else
                {
                    ///<remark>
                    ///For 4 years.
                    ///</remark>
                    #region 4 year
                    int Y = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    /* knz addnew 2019/01/02 start*/
                    int currentYear = Y;
                    /* knz addnew 2019/01/02 end*/
                    for (int i = 0; i < 4; i++)
                    {
                        /* knz modified 2019/01/02 start*/
                        //if (Y == 2018 || Y == 2017)
                        if (Y == currentYear || Y == currentYear - 1)
                        /* knz modified 2019/01/02 end*/
                        {
                            if (Exists("qbei-suruzo-etl-csv", Y + ".zip") == true)
                            {
                                DownloadFile("qbei-suruzo-etl-csv", Y + ".zip");

                            }
                            else
                            {
                                /* knz modified 2019/01/02 start*/
                                //DownloadFile("qbei-suruzo-etl-csv-2015", Y + ".zip");
                                DownloadFile("qbei-suruzo-etl-csv-" + (currentYear - 3).ToString(), Y + ".zip");
                                /* knz modified 2019/01/02 end*/
                            }
                        }

                        dtmain = GetDatatable("jutyu_meisai_list_" + Y + ".csv");

                        //dtmain = GETtable("jutyu_meisai_list_" + Y + ".csv");
                        TestAndAddColumn(strmain, dtmain);

                        //  CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番",
                        //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                        ///9/9/2019<Remark>
                        ///SKU列を　追加。
                        ///9/9/2019</Remark>
                        //    CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "SKU", "SKU",
                        //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");
                        ///30/04/2020<Remark>
                        ///セット識別フラグ列を　追加。                   
                        //CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                        //"単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");
                        ///30/04/2020</Remark>

                        ///06/10/2020<Remark>
                        ///送り状番号列を　追加。                   
                        CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番", "セット識別フラグ", "セット識別フラグ", "SKU", "SKU",
                        "単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード", "送り状番号", "送り状番号");
                        ///06/10/2020</Remark>

                        Date.DefaultValue = Y;
                        dttemp.Columns.Add(Date);

                        im.ItemMaster_InsertXml(dttemp);

                        dttemp.Columns.Remove("Date");
                        dtmain.Clear();
                        dttemp.Clear();

                        Y -= 1;

                    }
                    #endregion
                }

                #region comments
                //else
                //{
                //int Y = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                //int Y = 2015;

                //    for (int i = 0; i < 4; i++)
                //    {
                //        if (Y == 2015)
                //        {
                //            dtmain = GETtable("jutyu_meisai_list_" + Y + ".csv");
                //            int count = dtmain.Rows.Count;
                //            TestAndAddColumn(strmain, dtmain);

                //            CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番",
                //           "単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                //            Date.DefaultValue = Y;
                //            dttemp.Columns.Add(Date);

                //            im.ItemMaster_InsertXml(dttemp);

                //            dttemp.Columns.Remove("Date");
                //            dtmain.Clear();
                //            dttemp.Clear();

                //            Y -= 1;
                //            break;
                //        }
                //        if (Exists("qbei-suruzo-etl-csv", Y + ".zip") == true)
                //        {
                //            DownloadFile("qbei-suruzo-etl-csv", Y + ".zip");

                //        }
                //        else
                //        {
                //            DownloadFile("qbei-suruzo-etl-csv-2015", Y + ".zip");
                //        }

                //        //dtmain = GetDatatable("jutyu_meisai_list_" + Y + ".csv");

                //        dtmain = GETtable("jutyu_meisai_list_" + Y + ".csv");
                //        TestAndAddColumn(strmain, dtmain);

                //        CopyColumns(dtmain, dttemp, "売上日", "売上日", "受注番号", "受注番号", "自社品番", "自社品番",
                //      "単価", "単価", "数量", "数量", "標準原価", "標準原価", "金額", "金額", "クーポン", "クーポン", "値引き", "値引き", "送料", "送料", "消費税", "消費税", "総合計金額", "総合計金額", "店名", "店名", "代引手数料", "代引手数料", "使用ポイント", "使用ポイント", "付加ポイント", "付加ポイント", "キャンセル日", "キャンセル日", "支店コード", "支店コード", "返品手数料", "返品手数料", "売上区分", "売上区分", "移動平均原価", "移動平均原価", "最終仕入原価", "最終仕入原価", "支払方法コード", "支払方法コード", "配送先県コード", "配送先県コード", "配送会社コード", "配送会社コード");

                //        Date.DefaultValue = Y;
                //        dttemp.Columns.Add(Date);

                //        im.ItemMaster_InsertXml(dttemp);

                //        dttemp.Columns.Remove("Date");
                //        dtmain.Clear();
                //        dttemp.Clear();

                //        Y -= 1;

                //}
                //}

                #endregion


                string todayDate = DateTime.Now.ToString("yyyyMMdd");

                ///<remark>
                ///Process of Site_category_honten Data.
                ///</remark>
                //DownloadFile("qbei-suruzo-etl-csv", todayDate + "_site_category_honten.zip");
                Ftp_Download("master/", todayDate + "_site_category_honten.zip", "site_category_honten.csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                // string[] strCH = new string[] { "category_ids", "site_category_url" };
                string[] strCH = new string[] { "category_ids", "category_names", "skn_nm", "rkn_nm", "site_category_url", "display_no", "site_display_name" };//<remark Add Logic for Columns 2020/10/01 />
                DataTable dtCategoryHonten = GetDatatable("site_category_honten.csv");
                TestAndAddColumn(strCH, dtCategoryHonten);
                string ColName = "category_ids";
                RemoveDuplicateRows(dtCategoryHonten, ColName);
                Cht.Category_Honten_InsertXml(dtCategoryHonten);
                ConsoleWriteLine_Tofile("3. Site_category_honten Data completed: " + DateTime.Now);

                ///<remark>
                ///Process of p_brand Data.
                ///</remark>
                //DownloadFile("qbei-suruzo-etl-csv", todayDate + "_p_brand.zip");
                Ftp_Download("master/", todayDate + "_p_brand.zip", "p_brand.csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                DataTable dtbrand = GetDatatable("p_brand.csv");

                //<remark Edit Logic of Add Columns 2020/08/11 Start>
                //string[] strbrand = new string[] { "ブランドコード", "表示名称", "URL" };
                //TestAndAddColumn(strbrand, dtbrand);
                //brand.Brand_InsertXml(dtbrand);                

                DataTable tempbrand = new DataTable();
                tempbrand.Columns.Add("ブランドコード");
                tempbrand.Columns.Add("ブランド名");
                tempbrand.Columns.Add("ブランド名かな");
                tempbrand.Columns.Add("自転車ブランド");
                tempbrand.Columns.Add("パーツブランド");
                tempbrand.Columns.Add("BrandCategory");
                tempbrand.Columns.Add("URL");
                tempbrand.Columns.Add("Modified_Date");

                dtbrand.Columns.Add("BrandCategory");
                dtbrand.AsEnumerable().Where(r => (r.Field<string>("BrandCategory") == null))
                      .Select(r => r["BrandCategory"] = r["自転車ブランド"]).ToList();

                dtbrand.Columns.Add("Modified_Date");
                dtbrand.AsEnumerable().Where(r => (r.Field<string>("Modified_Date") == null))
                      .Select(r => r["Modified_Date"] = DateTime.Now).ToList();

                CopyColumns(dtbrand, tempbrand,
                   "ブランドコード", "ブランドコード",
                   "ブランド名", "ブランド名",
                   "ブランド名かな", "ブランド名かな",
                   "自転車ブランド", "自転車ブランド",
                   "パーツブランド", "パーツブランド",
                   "BrandCategory", "BrandCategory",
                   "URL", "URL",
                   "Modified_Date", "Modified_Date"
                    );

                tempbrand.AsEnumerable().Where(r => (r.Field<string>("BrandCategory") == "1"))
                      .Select(r => r["BrandCategory"] = "03").ToList();

                tempbrand.AsEnumerable().Where(r => (r.Field<string>("BrandCategory") == "0"))
                      .Select(r => r["BrandCategory"] = "04").ToList();
                //<remark Add Logic ブランド名かな is null for  2022/02/16 Start>
                tempbrand.AsEnumerable().Where(r => (r.Field<string>("ブランド名かな") == null))
                      .Select(r => r["ブランド名かな"] = " ").ToList();
                //</remark 2022/02/16 End>
                brand.Brand_InsertXml(tempbrand);
                //</remark 2020/08/11 End>

                ///<remark 追加処理　UpdateBrandTable　2020/08/11>
                ///BrandCode IN ('01104','01122','01127','01130','01228','01245','01253','01264') => BrandCategory is 01
                ///BrandCode IN ('00045','00093','00189','00904','01086','01172','01233') => BrandCategory is 02
                ///</remark>
                DataTable Mbrand = new DataTable();
                con.Open();
                cmd = new SqlCommand("Select * from Mブランド分類", con);
                cmd.CommandTimeout = 0;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(Mbrand);
                con.Close();
                for (int i = 0; i < Mbrand.Rows.Count; ++i)
                {
                    string Mcolums = Mbrand.Rows[i]["固定ブランドCD"].ToString();
                    if (Mcolums != "")
                    {
                        string Bcolumn = Mbrand.Rows[i]["ブランド分類CD"].ToString();
                        Bcolumn = "'" + Bcolumn + "'";
                        string[] m = Mcolums.Split(',');
                        foreach (string Mcolumn in m)
                        {
                            con.Open();
                            cmd = new SqlCommand("Update Brand set BrandCategory =" + Bcolumn + "where BrandCode =" + Mcolumn, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                ConsoleWriteLine_Tofile("4. p_brand Data completed: " + DateTime.Now);

                ///<remark>
                ///Process of Item Data.
                ///</remark>
                //DownloadFile("qbei-suruzo-etl-csv", todayDate + "_item_data.zip");
                Ftp_Download("master/", todayDate + "_item_data.zip", "item_data.csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Delete from Item", con);
                cmd1.ExecuteNonQuery();
                con.Close();
                DataTable dtitemdata = GetDatatable("item_data.csv");
                DataTable tempitem = new DataTable();
                tempitem.Columns.Add("自社品番");

                //<remark 追加ロジック　2020/04/27 Start(ALH)>
                tempitem.Columns.Add("有効無効");
                //<remark 追加ロジック　2020/06/08 Start(ALH)>
                tempitem.Columns.Add("本店有効無効");
                tempitem.Columns.Add("接客サイト有効無効");
                //</remark 追加ロジック　2020/06/08 End>

                tempitem.Columns.Add("商品名");

                tempitem.Columns.Add("商品名略称");

                //<remark 追加ロジック　2021/03/18 Start(ALH)>
                tempitem.Columns.Add("商品名_正");
                tempitem.Columns.Add("商品名_副");
                //</remark 追加ロジック　2021/03/18 End>

                tempitem.Columns.Add("ブランド");

                tempitem.Columns.Add("メーカー品番");
                tempitem.Columns.Add("セットフラグ");
                tempitem.Columns.Add("メーカー希望小売価格");
                tempitem.Columns.Add("販売価格");
                tempitem.Columns.Add("実店舗販売価格");
                tempitem.Columns.Add("実店舗販売価格_催事用");
                tempitem.Columns.Add("原価");
                tempitem.Columns.Add("仕入先コード");
                tempitem.Columns.Add("企画商品");
                tempitem.Columns.Add("注力商品");
                tempitem.Columns.Add("販売区分");
                tempitem.Columns.Add("商品分類");
                tempitem.Columns.Add("大型商品");

                //<remark 追加ロジック　2021/05/13 Start(ALH)>
                tempitem.Columns.Add("年式名称");
                tempitem.Columns.Add("ナビプラス_キーワード11");
                tempitem.Columns.Add("ナビプラス_キーワード12");
                tempitem.Columns.Add("ナビプラス_キーワード13");
                tempitem.Columns.Add("ナビプラス_キーワード14");
                tempitem.Columns.Add("ナビプラス_キーワード15");
                tempitem.Columns.Add("ナビプラス_キーワード16");
                tempitem.Columns.Add("ナビプラス_キーワード17");
                tempitem.Columns.Add("ナビプラス_キーワード18");
                tempitem.Columns.Add("ナビプラス_キーワード19");
                tempitem.Columns.Add("ナビプラス_キーワード20");
                //</remark 追加ロジック　2021/05/13 End>

                tempitem.Columns.Add("商品カテゴリ");
                tempitem.Columns.Add("サイトカテゴリ1");

                //<remark 追加ロジック　2022/04/18 Start(ALH)>
                tempitem.Columns.Add("消費税率");
                //</remark 追加ロジック　2022/04/18 End(ALH)>

                dtitemdata.Columns["有効・無効"].ColumnName = "有効無効";
                dtitemdata.Columns["実店舗販売価格(催事用)"].ColumnName = "実店舗販売価格_催事用";

                dtitemdata.Columns["商品カテゴリ（00101010101010101）"].ColumnName = "商品カテゴリ";
                dtitemdata.Columns["【本店】サイトカテゴリ1（00101010101010101）"].ColumnName = "サイトカテゴリ1";
                //<remark 追加ロジック　2020/06/08 Start(ALH)>
                dtitemdata.Columns["【本店】有効・無効"].ColumnName = "本店有効無効";
                dtitemdata.Columns["【接客サイト】有効・無効"].ColumnName = "接客サイト有効無効";
                //</remark 追加ロジック　2020/06/08 End>
                //<remark 追加ロジック　2021/03/18 Start(ALH)>
                dtitemdata.Columns["商品名(正)"].ColumnName = "商品名_正";
                dtitemdata.Columns["商品名(副)"].ColumnName = "商品名_副";
                //</remark 追加ロジック　2021/03/18 End>

                //<remark 追加ロジック　2021/05/13 Start(ALH)>
                dtitemdata.Columns["ナビプラス：キーワード11"].ColumnName = "ナビプラス_キーワード11";
                dtitemdata.Columns["ナビプラス：キーワード12"].ColumnName = "ナビプラス_キーワード12";
                dtitemdata.Columns["ナビプラス：キーワード13"].ColumnName = "ナビプラス_キーワード13";
                dtitemdata.Columns["ナビプラス：キーワード14"].ColumnName = "ナビプラス_キーワード14";
                dtitemdata.Columns["ナビプラス：キーワード15"].ColumnName = "ナビプラス_キーワード15";
                dtitemdata.Columns["ナビプラス：キーワード16"].ColumnName = "ナビプラス_キーワード16";
                dtitemdata.Columns["ナビプラス：キーワード17"].ColumnName = "ナビプラス_キーワード17";
                dtitemdata.Columns["ナビプラス：キーワード18"].ColumnName = "ナビプラス_キーワード18";
                dtitemdata.Columns["ナビプラス：キーワード19"].ColumnName = "ナビプラス_キーワード19";
                dtitemdata.Columns["ナビプラス：キーワード20"].ColumnName = "ナビプラス_キーワード20";
                //</remark 追加ロジック　2021/05/13 End>

                //CopyColumns(dtitemdata, tempitem, "自社品番", "自社品番", "商品名略称", "商品名略称", "ブランド", "ブランド", "商品カテゴリ", "商品カテゴリ", "サイトカテゴリ1", "サイトカテゴリ1");

                CopyColumns(dtitemdata, tempitem, "自社品番", "自社品番",
                   "有効無効", "有効無効",
                   //<remark 追加ロジック　2020/06/08 Start(ALH)>
                   "本店有効無効", "本店有効無効",
                   "接客サイト有効無効", "接客サイト有効無効",
                    //</remark 追加ロジック　2020/06/08 End>
                    "商品名", "商品名",
                    "商品名略称", "商品名略称",
                   //<remark 追加ロジック　2021/03/18 Start(ALH)>
                   "商品名_正", "商品名_正",
                   "商品名_副", "商品名_副",
                    //</remark 追加ロジック　2021/03/18 End>
                    "ブランド", "ブランド",
                    "メーカー品番", "メーカー品番",
                    "セットフラグ", "セットフラグ",
                    "メーカー希望小売価格", "メーカー希望小売価格",
                    "販売価格", "販売価格",
                    "実店舗販売価格", "実店舗販売価格",
                    "実店舗販売価格_催事用", "実店舗販売価格_催事用",
                    "原価", "原価",
                    "仕入先コード", "仕入先コード",
                    "企画商品", "企画商品",
                    "注力商品", "注力商品",
                    "販売区分", "販売区分",
                    "商品分類", "商品分類",
                    "大型商品", "大型商品",
                   //<remark 追加ロジック　2021/05/13 Start(ALH)>
                   "年式名称", "年式名称",
                   "ナビプラス_キーワード11", "ナビプラス_キーワード11",
                   "ナビプラス_キーワード12", "ナビプラス_キーワード12",
                   "ナビプラス_キーワード13", "ナビプラス_キーワード13",
                   "ナビプラス_キーワード14", "ナビプラス_キーワード14",
                   "ナビプラス_キーワード15", "ナビプラス_キーワード15",
                   "ナビプラス_キーワード16", "ナビプラス_キーワード16",
                   "ナビプラス_キーワード17", "ナビプラス_キーワード17",
                   "ナビプラス_キーワード18", "ナビプラス_キーワード18",
                   "ナビプラス_キーワード19", "ナビプラス_キーワード19",
                   "ナビプラス_キーワード20", "ナビプラス_キーワード20",
                    //</remark 追加ロジック　2021/05/13 End>
                    "商品カテゴリ", "商品カテゴリ",
                    "サイトカテゴリ1", "サイトカテゴリ1",
                   //<remark 追加ロジック　2022/04/18 Start(ALH)>
                   "消費税率", "消費税率");
                //</remark 追加ロジック　2022/04/18 End>

                tempitem.AsEnumerable().ToList().ForEach(r => r["メーカー希望小売価格"] = r.Field<string>("メーカー希望小売価格").Replace("'", " "));
                tempitem.AsEnumerable().ToList().ForEach(r => r["販売価格"] = r.Field<string>("販売価格").Replace("'", " "));
                tempitem.AsEnumerable().ToList().ForEach(r => r["実店舗販売価格"] = r.Field<string>("実店舗販売価格").Replace("'", " "));
                tempitem.AsEnumerable().ToList().ForEach(r => r["実店舗販売価格_催事用"] = r.Field<string>("実店舗販売価格_催事用").Replace("'", " "));
                tempitem.AsEnumerable().ToList().ForEach(r => r["原価"] = r.Field<string>("原価").Replace("'", " "));

                tempitem.AsEnumerable().Where(r => (r.Field<string>("有効無効") == "'有効'"))
                      .Select(r => r["有効無効"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("有効無効") == "'無効'"))
                      .Select(r => r["有効無効"] = "1").ToList();
                //<remark 追加ロジック　2020/06/08 Start(ALH)>
                tempitem.AsEnumerable().Where(r => (r.Field<string>("本店有効無効") == "'有効'"))
                     .Select(r => r["本店有効無効"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("本店有効無効") == "'無効'"))
                      .Select(r => r["本店有効無効"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("接客サイト有効無効") == "'有効'"))
                     .Select(r => r["接客サイト有効無効"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("接客サイト有効無効") == "'無効'"))
                      .Select(r => r["接客サイト有効無効"] = "1").ToList();
                //</remark 追加ロジック　2020/06/08 End>
                tempitem.AsEnumerable().Where(r => (r.Field<string>("セットフラグ") == "'0'"))
                     .Select(r => r["セットフラグ"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("セットフラグ") != "0"))
                      .Select(r => r["セットフラグ"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("企画商品") == "'0'"))
                     .Select(r => r["企画商品"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("企画商品") != "0"))
                      .Select(r => r["企画商品"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("注力商品") == "'0'"))
                     .Select(r => r["注力商品"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("注力商品") != "0"))
                      .Select(r => r["注力商品"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("商品分類") == "'0'"))
                     .Select(r => r["商品分類"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("商品分類") != "0"))
                      .Select(r => r["商品分類"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("大型商品") == "'0'"))
                     .Select(r => r["大型商品"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("大型商品") != "0"))
                      .Select(r => r["大型商品"] = "1").ToList();
                //</remark 追加ロジック　2020/04/27 End>

                //<remark 追加ロジック　2022/04/18 Start(ALH)>
                tempitem.AsEnumerable().Where(r => !(r.Field<string>("消費税率") == "'通常税率'" || r.Field<string>("消費税率") == "'軽減税率'" || r.Field<string>("消費税率") == "'非課税'"))
                     .Select(r => r["消費税率"] = "0").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("消費税率") == "'通常税率'"))
                     .Select(r => r["消費税率"] = "1").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("消費税率") == "'軽減税率'"))
                     .Select(r => r["消費税率"] = "2").ToList();

                tempitem.AsEnumerable().Where(r => (r.Field<string>("消費税率") == "'非課税'"))
                     .Select(r => r["消費税率"] = "9").ToList();
                //</remark 追加ロジック　2022/04/18 End>

                Chuncked ck = new Chuncked();
                var co_dt001 = ck.Separate(tempitem, 20000);
                foreach (DataTable sub_dt001 in co_dt001)
                {
                    item.Item_InsertXml(sub_dt001);
                }
                ConsoleWriteLine_Tofile("5. Item Data completed: " + DateTime.Now);
                //var totalRows1 = tempitem.Rows.Count;

                //var halfway1 = totalRows1 / 2;


                //var firstHalf1 = tempitem.AsEnumerable().Take(halfway1).CopyToDataTable();

                //item.Item_InsertXml(firstHalf1);
                //ConsoleWriteLine_Tofile("5_1.Item Data First Half completed: " + DateTime.Now);

                //var secondHalf1 = tempitem.AsEnumerable().Skip(halfway1).Take(totalRows1 - halfway1).CopyToDataTable();

                //item.Item_InsertXml(secondHalf1);
                //ConsoleWriteLine_Tofile("5_2.Item Data Second Half completed: " + DateTime.Now);

                ///<remark>
                ///Process of site_category Data.
                ///</remark>
                //DownloadFile("qbei-suruzo-etl-csv", todayDate + "_site_category.zip");
                Ftp_Download("master/", todayDate + "_site_category.zip", "site_category.csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                DataTable dtsc = GetDatatable("site_category.csv");
                //string[] strsc = new string[] { "カテゴリID", "カテゴリ名" };
                string[] strsc = new string[] { "カテゴリID", "カテゴリ階層", "カテゴリ名" };//<remark Add Logic of Column 2020/10/20 />
                TestAndAddColumn(strsc, dtsc);
                sc.SiteCategory_InsertXml(dtsc);
                //brand.Brand_InsertXml(dtbrand);
                ConsoleWriteLine_Tofile("6. site_category Data completed: " + DateTime.Now);

                ///<remark 追加処理　2020/08/06>
                ///Process of Item_SKU Data.
                ///</remark>
                string nowdate = DateTime.Now.ToString("yyyyMMdd");
                //DownloadFile("qbei-suruzo-etl-csv", nowdate + "_sku_data.zip");
                Ftp_Download("master/", nowdate + "_sku_data.zip", "sku_data.csv");//<remark Add Logic for Download at FTP Server 2021/08/02 />
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Truncate Table Item_SKU", con);
                cmd2.ExecuteNonQuery();
                con.Close();

                //<remark Add&Edit Logic for Large CSV to divide Small CSV and Loop Insert Process 2021/10/28 Start>
                Directory.GetFiles(@"C:\Qbei_Uriage\Divide\").ToList().ForEach(File.Delete);
                SplitFile(@"C:\Qbei_Uriage\UpdateData\sku_data.csv", @"C:\Qbei_Uriage\Divide\sku_data_");
                int fCount = Directory.GetFiles(@"C:\Qbei_Uriage\Divide\", "*", SearchOption.AllDirectories).Length;
                int CSV_loop = 1;
                while (CSV_loop - 1 < fCount)
                {
                    //DataTable dtskudata = GetDatatable("sku_data.csv");
                    DataTable dtskudata = GetDatatable2("sku_data_" + CSV_loop + ".csv");
                    DataTable tempitem2 = new DataTable();

                    tempitem2.Columns.Add("自社品番");
                    tempitem2.Columns.Add("表示順");
                    tempitem2.Columns.Add("倉庫商品コード");
                    tempitem2.Columns.Add("JANCD");
                    tempitem2.Columns.Add("AmazonSKU");
                    tempitem2.Columns.Add("棚番号");
                    tempitem2.Columns.Add("カラー");
                    tempitem2.Columns.Add("サイズ");
                    tempitem2.Columns.Add("仕入先コード");
                    tempitem2.Columns.Add("出荷元コード");
                    tempitem2.Columns.Add("発注禁止");
                    tempitem2.Columns.Add("発注種別");
                    tempitem2.Columns.Add("最高点");
                    tempitem2.Columns.Add("発注点");

                    dtskudata.Columns["JAN表示順"].ColumnName = "表示順";
                    dtskudata.Columns["ＪＡＮ"].ColumnName = "JANCD";

                    CopyColumns(dtskudata, tempitem2,
                        "自社品番", "自社品番",
                        "表示順", "表示順",
                        "倉庫商品コード", "倉庫商品コード",
                        "JANCD", "JANCD",
                        "AmazonSKU", "AmazonSKU",
                        "棚番号", "棚番号",
                        "カラー", "カラー",
                        "サイズ", "サイズ",
                        "仕入先コード", "仕入先コード",
                        "出荷元コード", "出荷元コード",
                        "発注禁止", "発注禁止",
                        "発注種別", "発注種別",
                        "最高点", "最高点",
                        "発注点", "発注点"
                        );



                    string empty = String.Empty;

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("発注禁止") == null))
                         .Select(r => r["発注禁止"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("倉庫商品コード") == null))
                       .Select(r => r["倉庫商品コード"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("AmazonSKU") == null))
                        .Select(r => r["AmazonSKU"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("棚番号") == null))
                       .Select(r => r["棚番号"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("カラー") == null))
                       .Select(r => r["カラー"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("サイズ") == null))
                       .Select(r => r["サイズ"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("仕入先コード") == null))
                       .Select(r => r["仕入先コード"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("出荷元コード") == null))
                       .Select(r => r["出荷元コード"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("出荷元コード") == null))
                       .Select(r => r["出荷元コード"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("発注種別") == null))
                       .Select(r => r["発注種別"] = empty).ToList();

                    //<remark Add Logic for Null case 2020/09/14 Start>
                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("表示順") == null))
                       .Select(r => r["表示順"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("最高点") == null))
                       .Select(r => r["最高点"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("発注点") == null))
                       .Select(r => r["発注点"] = empty).ToList();

                    tempitem2.AsEnumerable().Where(r => (r.Field<string>("発注禁止") == null))
                      .Select(r => r["発注禁止"] = empty).ToList();
                    //</reamark 2020/09/14 End>

                    tempitem2.AsEnumerable().ToList().ForEach(r => r["表示順"] = r.Field<string>("表示順").Replace("'", " "));
                    tempitem2.AsEnumerable().ToList().ForEach(r => r["最高点"] = r.Field<string>("最高点").Replace("'", " "));
                    tempitem2.AsEnumerable().ToList().ForEach(r => r["発注点"] = r.Field<string>("発注点").Replace("'", " "));
                    tempitem2.AsEnumerable().ToList().ForEach(r => r["発注禁止"] = r.Field<string>("発注禁止").Replace("'", " "));

                    //<remark Edit Logic of Insert Table 2020/11/20 Start>
                    //Chuncked ck2 = new Chuncked();
                    //var co_dt003 = ck2.Separate(tempitem2, 20000);
                    //foreach (DataTable sub_dt003 in co_dt003)
                    //{
                    //    item.ItemSKU_InsertXml(sub_dt003);
                    //}
                    item.ItemSKU_InsertXml(tempitem2);
                    //</remark 2020/11/20 End>
                    ++CSV_loop;
                }
                //</remark 2021/10/28 End>
                ConsoleWriteLine_Tofile("7. Item_SKU Data completed: " + DateTime.Now);

                ///<remark  追加処理　2020/08/07>
                ///Process of Inventory Data.
                ///</remark> 

                //<remark Add&Edit Logic for Large CSV to divide Small CSV and Loop Insert Process 2021/11/09 Start>
                Directory.GetFiles(@"C:\Qbei_Log\Divide").ToList().ForEach(File.Delete);
                SplitFile(@"C:\Qbei_Log\Csv\maker_status.csv", @"C:\Qbei_Log\Divide\maker_status_");

                //DataTable Inventorydt = GetInventoryDatatable();//<remaek Close Logic 2021/11/09 />
                con.Open();
                SqlCommand cmd3 = new SqlCommand("Truncate Table Maker_Status", con);
                cmd3.ExecuteNonQuery();
                con.Close();

                int Inventoru_fCount = Directory.GetFiles(@"C:\Qbei_Log\Divide\", "*", SearchOption.AllDirectories).Length;
                int InventoryCSV_loop = 1;
                while (InventoryCSV_loop - 1 < Inventoru_fCount)
                {
                    DataTable Inventorydt = GetInventoryDatatable_2("maker_status_" + InventoryCSV_loop);

                    Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("代理店ID") != "")).CopyToDataTable();
                    Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("JANコード") != null)).CopyToDataTable();
                    //Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("発注コード").All(c => char.IsLetterOrDigit(c)))).CopyToDataTable();
                    Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("下代") != null)).CopyToDataTable();
                    Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("下代").All(c => char.IsDigit(c)))).CopyToDataTable();
                    Inventorydt = Inventorydt.AsEnumerable().Where(r => (r.Field<string>("下代").All(c => char.IsNumber(c)))).CopyToDataTable();

                    string Empty = String.Empty;
                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("在庫情報") == null))
                      .Select(r => r["在庫情報"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("入荷予定") == null))
                      .Select(r => r["入荷予定"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("自社品番") == null))
                      .Select(r => r["自社品番"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("商品名") == null))
                      .Select(r => r["商品名"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("カラー") == null))
                    .Select(r => r["カラー"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("サイズ") == null))
                   .Select(r => r["サイズ"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("メーカー情報日") == null))
                   .Select(r => r["メーカー情報日"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("最終反映日") == null))
                  .Select(r => r["最終反映日"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("ブランドコード") == null))
                .Select(r => r["ブランドコード"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("ステータス変更日") == null))
               .Select(r => r["ステータス変更日"] = Empty).ToList();

                    Inventorydt.AsEnumerable().Where(r => (r.Field<string>("purchaserURL") == null))
               .Select(r => r["purchaserURL"] = Empty).ToList();

                    //<remark Edit Logic of Insert Table 2020/09/18 Start>
                    Chuncked ck1 = new Chuncked();
                    //var co_dt002 = ck1.Separate(Inventorydt, 20000);
                    //var co_dt002 = ck1.Separate(Inventorydt, 10000);//<remark Edit Logic for Separate Data 2021/10/01 />
                    //foreach (DataTable sub_dt002 in co_dt002)
                    //{
                    //    inventory.Inventory_InsertXML(sub_dt002);
                    //}
                    inventory.Inventory_InsertXML(Inventorydt);
                    //</remark 2020/09/18 End>
                    ++InventoryCSV_loop;
                }
                //</remark 2021/11/09 End>
                ConsoleWriteLine_Tofile("8. Inventory Data completed: " + DateTime.Now);
            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
            }
        }

        /// <summary>
        /// Get Data for Inventory of Process(2020/08/07)
        /// </summary>
        /// <returns></returns>
        //public static DataTable GetInventoryDatatable()
        //{
        //    DataTable dtResult = new DataTable();
        //    DataTable dtCsv = new DataTable();
        //    string[] columns = { "代理店ID", "JANコード", "在庫情報", "入荷予定", "下代", "発注コード", "自社品番", "商品名", "カラー", "サイズ", "メーカー情報日", "最終反映日", "ブランドコード", "ステータス変更日", "purchaserURL" };
        //    string[] filelist = Directory.GetFiles(@"C:\Qbei_Log\Csv");
        //    foreach (string file in filelist)
        //    {
        //        string ext = Path.GetExtension(file);
        //        if (ext.Equals(".csv"))
        //        {
        //            using (var csv = new CachedCsvReader(new StreamReader(file, Encoding.GetEncoding(932)), true))
        //            {
        //                if (file.Contains("maker_status"))
        //                {
        //                    ConsoleWriteLine_Tofile("Csv Name is Right!" + DateTime.Now);
        //                    dtCsv.Load(csv);
        //                    if (dtResult.Columns.Count <= 0)
        //                        dtResult = dtCsv.Clone();

        //                    if (checkCsvFormat(dtCsv, columns))
        //                    {
        //                        dtResult.Merge(dtCsv);
        //                    }
        //                    else
        //                    {
        //                        ConsoleWriteLine_Tofile("CsvFile Format Wrong!" + DateTime.Now);
        //                        Environment.Exit(0);
        //                    }
        //                }
        //                else
        //                {
        //                    File.Move(file, @"C:\Qbei_Log\Trash\" + @"\" + Path.GetFileName(file));
        //                    ConsoleWriteLine_Tofile("Csv Name is Wrong!" + DateTime.Now);
        //                    Environment.Exit(0);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //File.Move(file, trashPath + @"\" + Path.GetFileName(file));
        //            File.Move(file, @"C:\Qbei_Log\Trash\" + @"\" + Path.GetFileName(file));
        //            ConsoleWriteLine_Tofile("Csv is Wrong!" + DateTime.Now);
        //            Environment.Exit(0);
        //        }
        //    }
        //    if (filelist.ToArray().Count() == 0)
        //    {
        //        ConsoleWriteLine_Tofile("Csv is Not Exit!" + DateTime.Now);
        //        Environment.Exit(0);
        //    }
        //    return dtResult;
        //}

        /// <summary>
        /// Edit Logic to Get Data for Inventory of Process(2021/11/09)
        /// </summary>
        public static DataTable GetInventoryDatatable_2(string csvFileName)
        {
            DataTable dtResult = new DataTable();
            DataTable dtCsv = new DataTable();
            string[] columns = { "代理店ID", "JANコード", "在庫情報", "入荷予定", "下代", "発注コード", "自社品番", "商品名", "カラー", "サイズ", "メーカー情報日", "最終反映日", "ブランドコード", "ステータス変更日", "purchaserURL" };
            string file = @"C:\Qbei_Log\Divide\" + csvFileName + ".csv";
            string ext = Path.GetExtension(file);
            if (file.Contains(".csv"))
            {
                using (var csv = new CachedCsvReader(new StreamReader(file, Encoding.GetEncoding(932)), true))
                {
                    if (file.Contains("maker_status"))
                    {
                        ConsoleWriteLine_Tofile("Csv Name is Right!" + DateTime.Now);
                        dtCsv.Load(csv);
                        if (dtResult.Columns.Count <= 0)
                            dtResult = dtCsv.Clone();

                        if (checkCsvFormat(dtCsv, columns))
                        {
                            dtResult.Merge(dtCsv);
                        }
                        else
                        {
                            ConsoleWriteLine_Tofile("CsvFile Format Wrong!" + DateTime.Now);
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        File.Move(file, @"C:\Qbei_Log\Trash\" + @"\" + Path.GetFileName(file));
                        ConsoleWriteLine_Tofile("Csv Name is Wrong!" + DateTime.Now);
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                File.Move(file, @"C:\Qbei_Log\Trash\" + @"\" + Path.GetFileName(file));
                ConsoleWriteLine_Tofile("Csv is Wrong!" + DateTime.Now);
                Environment.Exit(0);
            }
            return dtResult;
        }

        private static bool checkCsvFormat(DataTable dtCsv, string[] columns)
        {

            foreach (string colName in columns)
            {
                DataColumnCollection cols = dtCsv.Columns;
                if (!cols.Contains(colName))
                    return false;
            }
            return true;
        }
        public static bool Exists(string bucketName, string fileName)
        {
            AmazonS3 s3Client = GetS3Client();
            try
            {
                var request = s3Client.GetObjectMetadata(new GetObjectMetadataRequest().WithBucketName(bucketName).WithKey(fileName));
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// download file from amazons3
        /// </summary>
        /// 
        //public static DataTable GetDataTableFromExcel(string filePath)
        //{
        //    var reader = new StreamReader(FileStream null);

        //    //Then one can read in the data in the following formats.

        //    //XML Document:
        //    XmlDocument xmldoc = reader.ReadAsXmlDocument();

        //    //XML string:
        //    string xml = reader.ReadAsXml();

        //    //.NET DataTable:
        //    DataTable dataTable = reader.ReadAsDataTable();

        //    return dataTable;

        //}


        public static DataTable GetDataTableFromExcel(string filePath)
        {

            string connectionString = string.Empty;

            string[] arFile = filePath.Split('.');

            string fileExtension = arFile[1];



            if (fileExtension.ToLower() == "csv")

                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;;FMT=Delimited(,)";

            else if (fileExtension.ToLower() == "csv")

                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;;FMT=Delimited(,)";



            OleDbConnection objOleDbConnection = new OleDbConnection(connectionString);

            OleDbCommand objOleDbCommand = new OleDbCommand();



            try
            {

                objOleDbCommand.CommandType = System.Data.CommandType.Text;

                objOleDbCommand.Connection = objOleDbConnection;



                OleDbDataAdapter dAdapter = new OleDbDataAdapter(objOleDbCommand);

                DataTable dtExcelRecords = new DataTable();



                objOleDbConnection.Open();

                DataTable dtExcelSheetName = objOleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();

                objOleDbCommand.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";

                dAdapter.SelectCommand = objOleDbCommand;

                dAdapter.Fill(dtExcelRecords);

                return dtExcelRecords;

            }

            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
                throw ex;

            }

            finally
            {

                objOleDbConnection.Close();

                objOleDbConnection.Dispose();

            }

        }


        /// <summary>
        /// AmazonS3
        /// </summary>
        /// <param name="bucketName">Input to FolderName.</param>
        /// <param name="fileName">Input to FileName.</param>
        public static void DownloadFile(string bucketName, string fileName)
        {
            ///<remark>
            ///Check and Delete and Create of filename at C:Drive.
            ///Download to file at AmazonS3. 
            ///</remark>
            AmazonS3 s3Client = GetS3Client();
            using (MemoryStream ms = GetFile(s3Client, bucketName, fileName))//client,foldername,filename)
            {
                if (File.Exists("C:\\Qbei_Uriage\\UpdateData\\" + fileName))
                    File.Delete("C:\\Qbei_Uriage\\UpdateData\\" + fileName);



                FileStream fs = new FileStream("C:\\Qbei_Uriage\\UpdateData\\" + fileName, FileMode.CreateNew);
                ms.WriteTo(fs);
                ms.Close();
                ms.Dispose();
            }


            // extract entries that use encryption
            ///<remark>
            ///Process of ZipFile.
            ///</remark>
            using (ZipFile zip = ZipFile.Read("C:\\Qbei_Uriage\\UpdateData\\" + fileName))
            {
                zip.ExtractAll("C:\\Qbei_Uriage\\UpdateData\\", ExtractExistingFileAction.OverwriteSilently);
            }

            //if ()
            var str = fileName;
            bool status = true;
            try
            {



                str = str.Substring(9);
            }
            catch (Exception ex)
            {
                //ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
                status = false;
            }

            if (status)
            {
                foreach (String fpahname in System.IO.Directory.GetFiles("C:\\Qbei_Uriage\\UpdateData\\"))
                {
                    //if ()


                    if (Path.GetFileName(fpahname).Contains(str))
                    {
                        if (!Path.GetFileName(fpahname).Equals(fileName))
                        {
                            File.Delete(fpahname);

                        }

                        //changezip(fname);
                        //File.Delete(fname);
                    }
                }
            }
        }
        //GetDataTableFromExcel

        /// <summary>
        /// Create to DataTable from CSV File of Data.
        /// </summary>
        /// <param name="csvName">Input to CSVfile of Name.</param>
        /// <returns></returns>
        public static DataTable GetDatatable(string csvName)

        {
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\Qbei_Uriage\UpdateData\");
            FileInfo[] Files = dinfo.GetFiles("*.csv");
            DataTable dt = new DataTable();

            using (CsvReader csvReader =
            new CsvReader(new StreamReader(@"C:\Qbei_Uriage\UpdateData\" + csvName, Encoding.GetEncoding(932)), true))
            {
                dt.Load(csvReader);
            }

            return dt;
        }

        /// <summary>
        /// Add Logic to Create for DataTable from CSV File of Data.(2021/10/28)
        /// </summary>
        /// <param name="csvName">Input to CSVfile of Name.</param>
        /// <returns></returns>
        public static DataTable GetDatatable2(string csvName)

        {
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\Qbei_Uriage\Divide\");
            FileInfo[] Files = dinfo.GetFiles(" *.csv");
            DataTable dt = new DataTable();

            using (CsvReader csvReader =
            new CsvReader(new StreamReader(@"C:\Qbei_Uriage\Divide\" + csvName, Encoding.GetEncoding(932)), true))
            {
                dt.Load(csvReader);
            }

            return dt;
        }

        /// <summary>
        /// Replace to DataTable of Rows in ColumnsName.
        /// </summary>
        /// <param name="dt">Input to DataTable.</param>
        /// <returns>Check to DataTable</returns>
        static DataTable Remove_Doublecode(DataTable dt)
        {
            try
            {
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    dt.Columns[i].ColumnName = dt.Columns[i].ToString().Replace("\"", "");
                //}
                int colCount = dt.Columns.Count;

                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ReadOnly = false;
                    dc.ColumnName = dc.ColumnName.Trim().Replace("\"", "");
                }

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        if (dr[i] != null && dr[i] != DBNull.Value)
                        {
                            string field = dr[i].ToString();
                            dr[i] = field.Replace("\\\"", "\"");
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
                throw ex;
            }
        }

        /// <summary>
        /// Create Folder
        /// </summary>
        /// <param name="path">Input to string.</param>
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Create AmazonS3 from key in Application Config.
        /// </summary>
        /// <returns s3Client>AmazonS3</returns>
        public static AmazonS3 GetS3Client()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;

            AmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"]
                    );
            return s3Client;
        }

        /// <summary>
        /// GetFile from AmazonS3.
        /// </summary>
        /// <param name="s3Client">Input to AmazonS3 Client.</param>
        /// <param name="bucketName">Input to BucketName.</param>
        /// <param name="fileName">Input to FileName.</param>
        /// <returns></returns>
        public static MemoryStream GetFile(AmazonS3 s3Client, string bucketName, string fileName)
        {
            ///<remark>
            ///Get to object of BucketName and FileName using AmazonS3.
            ///</remark>
            using (s3Client)
            {
                MemoryStream file = new MemoryStream();
                try
                {
                    GetObjectResponse r = s3Client.GetObject(new GetObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = fileName
                    });
                    try
                    {
                        const int readSize = 256;
                        byte[] buffer = new byte[readSize];
                        //MemoryStream ms = new MemoryStream();

                        int count = r.ResponseStream.Read(buffer, 0, readSize);
                        while (count > 0)
                        {
                            file.Write(buffer, 0, count);
                            count = r.ResponseStream.Read(buffer, 0, readSize);
                        }
                        file.Position = 0;
                        r.ResponseStream.Close();


                        //int count = inputStream.Read(buffer, 0, readSize);
                        //while (count > 0)
                        //{
                        //    ms.Write(buffer, 0, count);
                        //    count = inputStream.Read(buffer, 0, readSize);
                        //}


                        //long transferred = 0L;
                        //BufferedStream stream2 = new BufferedStream(r.ResponseStream);
                        //byte[] buffer = new byte[0x2000];
                        //int count = 0;
                        //while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                        //{
                        //    file.Write(buffer, 0, count);
                        //}
                    }
                    finally
                    {
                    }
                    return file;
                }
                catch (AmazonS3Exception)
                {
                    //Show exception
                }
            }
            return null;
        }

        /// <summary>
        /// GetFileList of AmazonS3.
        /// </summary>
        public static void GetFileList()
        {
            AmazonS3 s3Client = GetS3Client();
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = "qbei-suruzo-etl-csv";
            do
            {
                ListObjectsResponse response = s3Client.ListObjects(request);

                // Process response.
                // ...

                // If response is truncated, set the marker to get the next 
                // set of keys.
                if (response.IsTruncated)
                {
                    request.Marker = response.NextMarker;
                }
                else
                {
                    request = null;
                }
            } while (request != null);
        }


        public static String DataTableToXml(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }

        private static void CopyColumns(DataTable source, DataTable dest, params string[] columns)
        {
            foreach (DataRow sourcerow in source.Rows)
            {
                DataRow destRow = dest.NewRow();
                foreach (string colname in columns)
                {
                    destRow[colname] = sourcerow[colname];
                }
                dest.Rows.Add(destRow);
            }
        }

        private static void TestAndAddColumn(string[] dcName, DataTable datatable)
        {
            DataView view = new DataView(datatable);
            DataTable dtLineItemUnit = view.ToTable(false, dcName);
        }

        //remove duplicate rows from datatable
        public static DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        /// <summary>
        /// GetTable from CSV FileName.
        /// </summary>
        /// <param name="csvFileName">Input to CSVFileName.</param>
        /// <returns>CSVData of Rows</returns>
        private static DataTable GETtable(string csvFileName)
        {
            DataTable csvData = new DataTable();
            using (CsvReader csv =
            new CsvReader(new StreamReader(@"C:\Qbei_Uriage\UpdateData\" + csvFileName, Encoding.GetEncoding(932)), true))
            {
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                foreach (string header in headers)
                {
                    csvData.Columns.Add(header.Trim());
                }
                while (csv.ReadNextRecord())
                {

                    DataRow newRow = csvData.NewRow();
                    // this bit could be modified to do type conversions, skip columns, etc
                    for (int i = 0; i < headers.Length; i++)
                    {
                        try
                        {
                            if (csv[i] == null)
                            {
                                newRow[i] = csv[i];
                            }
                            else
                            {
                                string field = csv[i];
                                newRow[i] = field.Replace("\\\"", "\"");
                            }

                        }
                        catch (MalformedCsvException ex)
                        {
                            throw;
                        }

                    }
                    csvData.Rows.Add(newRow);
                }
            }
            //DataTable csvData = new DataTable();
            //string csvFilePath = @"C:\\Qbei_Uriage\\UpdateData\\" + csvFileName;
            //try
            //{
            //    string[] seps = { "\",", ",\"" };
            //    char[] quotes = { '\"', ' ' };
            //    string[] colFields = null;
            //    foreach (var line in File.ReadLines(csvFilePath, Encoding.GetEncoding(932)))
            //    {
            //        var fields = line
            //            .Split(seps, StringSplitOptions.None)
            //            .Select(s => s.Trim(quotes).Replace("\\\"", "\""))
            //            .ToArray();

            //        if (colFields == null)
            //        {
            //            colFields = fields;
            //            foreach (string column in colFields)
            //            {
            //                DataColumn datacolumn = new DataColumn(column);
            //                datacolumn.AllowDBNull = true;
            //                csvData.Columns.Add(datacolumn);
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < colFields.Length; i++)
            //            {
            //                if (fields[i] == "")
            //                {
            //                    fields[i] = null;
            //                }
            //            }
            //            csvData.Rows.Add(fields);
            //        }
            //    }
            //}
            //catch (MalformedCsvException e)
            //{
            //    throw;
            //}
            return csvData;
        }

        static void ConsoleWriteLine_Tofile(string traceText)
        {
            StreamWriter sw = new StreamWriter(consoleWriteLinePath + "Uriage_Input_Console.txt", true, System.Text.Encoding.GetEncoding("Shift_Jis"));
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.WriteLine(traceText);
            sw.Close();
            sw.Dispose();
        }

        //<remark Add Function for Downoad File at FTP Server 2021/07/23>
        public static void Ftp_Download(string folder_name, string file_name, string csv_name)
        {
            string LocalFilePath = ConfigurationManager.AppSettings["LocalFilePath"].ToString();
            string LocalFileCopyPath = ConfigurationManager.AppSettings["LocalFileCopyPath"].ToString();
            string FTP_Host = ConfigurationManager.AppSettings["FTP_Host"].ToString();
            string Username = ConfigurationManager.AppSettings["Username"].ToString();
            string Password = ConfigurationManager.AppSettings["Password"].ToString();
            FtpWebRequest reqFTP;
            string strName;
            ZipFile objZip;
            FtpWebResponse response;
            try
            {
                if (File.Exists(LocalFilePath + file_name))
                    File.Delete(LocalFilePath + file_name);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTP_Host + folder_name + file_name));
                reqFTP.Credentials = new NetworkCredential(Username, Password);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(LocalFilePath + file_name, FileMode.Create);
                ConsoleWriteLine_Tofile("DownLoad success!");
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();


                if (file_name.Contains("item_data") || file_name.Contains("sku_data"))
                {
                    File.Copy(LocalFilePath + file_name, LocalFileCopyPath + file_name);
                }
                ///<remark>
                ///Process of ZipFile.
                ///</remark>
                using (ZipFile zip = ZipFile.Read(LocalFilePath + file_name))
                {
                    zip.ExtractAll(LocalFilePath, ExtractExistingFileAction.OverwriteSilently);
                }

                string fullDestPath = Path.Combine(LocalFilePath, csv_name);
                File.SetLastWriteTime(fullDestPath, DateTime.Now);

                //if ()
                var str = file_name;
                bool status = true;
                try
                {
                    str = str.Substring(9);
                }
                catch (Exception ex)
                {
                    //ConsoleWriteLine_Tofile("Qbei Uriage Input Console :" + ex.ToString() + " " + DateTime.Now);
                    status = false;
                }

                if (status)
                {
                    foreach (String fpahname in System.IO.Directory.GetFiles(LocalFilePath))
                    {
                        //if ()


                        if (Path.GetFileName(fpahname).Contains(str))
                        {
                            if (!Path.GetFileName(fpahname).Equals(file_name))
                            {
                                File.Delete(fpahname);

                            }

                            //changezip(fname);
                            //File.Delete(fname);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                ConsoleWriteLine_Tofile(ex.Message);
                FtpWebResponse ftpresponse = (FtpWebResponse)ex.Response;
                if (ftpresponse.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    //Continue Loop
                    //continue;
                }
            }
        }

        //<remark Add Function for Large CSV to divide Small Multiple CSV 2021/10/28>
        public static void SplitFile(string inputFile, string outputFile)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(inputFile, Encoding.GetEncoding(932), true))
            {
                int fileNumber = 0;
                int header = 0;
                while (!sr.EndOfStream)
                {
                    int count = 0;

                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(outputFile + ++fileNumber + ".CSV", false, Encoding.GetEncoding(932)))
                    {
                        sw.AutoFlush = true;
                        //if (header>=20000)
                        if (header >= 15000)
                        {
                            var headerLine = File.ReadLines(inputFile, Encoding.GetEncoding(932)).ToArray();
                            string FHeader = headerLine[0].ToString();
                            sw.WriteLine(FHeader);
                        }
                        //while (!sr.EndOfStream && ++count <= 20000)
                        while (!sr.EndOfStream && ++count <= 15000)
                        {
                            ++header;
                            sw.WriteLine(sr.ReadLine());
                        }
                    }
                }
            }
        }

        //<remark Method to convert IEnumerable to DataTable 2024-03-19>
        static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable();

            // Get all the properties
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            // Create columns for DataTable
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }

            // Add rows to DataTable
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

    }
}
