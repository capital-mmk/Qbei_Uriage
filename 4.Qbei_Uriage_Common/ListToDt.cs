using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace _4.Qbei_Uriage_Common
{
    public static class ListToDt
    {
        public static DataTable ConvertListToDt<T>(this IList<T> data)
        {
            DataRow dr;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dtTable = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                dtTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                dr = dtTable.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    dr[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dtTable.Rows.Add(dr);

            }
            return dtTable;
        }
    }
}
