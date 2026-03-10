using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using ClosedXML.Excel;

namespace Qbei_Uriage_ExportConsole
{
    public class ExportExcel
    {
        public ExportExcel( DataTable dt)
        {   
            try
            {
                var workbook = new XLWorkbook();
                workbook.Worksheets.Add(dt, "sheet1");
                string date = DateTime.Now.ToString("hhmmss");
                workbook.SaveAs(@"D:\Projects\"+ date+"_export.xlsx");
            }
            catch (Exception)
            {

            }
        }
    }
}
