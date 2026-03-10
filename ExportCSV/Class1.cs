using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using LumenWorks.Framework.IO.Csv;
using CsvHelper;
using System.Data.SqlClient;

public static class CSVUtility
{
    public static void ToCSV(this DataTable dTable,string filepath)
    {
        using (var textWriter = new StreamWriter(new FileStream(filepath, FileMode.Create), Encoding.GetEncoding(932)))
        using (var csv1 = new CsvHelper.CsvWriter(textWriter))
        {
            foreach (DataColumn column in dTable.Columns)
            {
                csv1.WriteField(column.ColumnName);
            }
            csv1.NextRecord();

            foreach (DataRow row in dTable.Rows)
            {
                for (var i = 0; i < dTable.Columns.Count; i++)
                {
                    csv1.WriteField(row[i]);
                }
                csv1.NextRecord();
            }
        }
    }
}