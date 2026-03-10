using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace _4.Qbei_Uriage_Common
{
    public class Common
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        string strCon;

        /// <summary>
        /// Convert datatable to XML
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public string ConvertToXml(DataTable dtData)
        {
            try
            {
                System.IO.StringWriter objWriter = new System.IO.StringWriter();
                dtData.TableName = "Cost";
                dtData.WriteXml(objWriter, XmlWriteMode.WriteSchema, false);
                return objWriter.ToString();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return ex.Message;
            }
        }
        /// <summary>
        /// Check file is opened or not
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool CheckFileOpen(string strPath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.None);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteLog("File is open by another user.");
                return false;
            }
        }

        /// <summary>
        /// Read data from CSV
        /// </summary>
        /// <param name="strFPath"></param>
        /// <returns></returns>
        public DataTable ReadCsv(string strFPath)
        {
            try {
                DataRow dr;
                string[] strData;
                string strLine;
                StreamReader rs;
                DataTable dtData = new DataTable();
                if (File.Exists(strFPath))
                {
                    if (new FileInfo(strFPath).Length > 0 && CheckFileOpen(strFPath))
                    {
                        rs = new StreamReader(strFPath, Encoding.GetEncoding(932));
                        strLine = rs.ReadLine();
                        foreach (string strtemp in strLine.Split(','))
                        {
                            dtData.Columns.Add(strtemp.Replace("\"", ""));
                        }
                        while ((strLine = rs.ReadLine()) != null)
                        {
                            if (!String.IsNullOrEmpty(strLine.Replace("\"", "")))
                            {
                                strLine = strLine.Replace("\"", "");
                                strData = strLine.Split(',');
                                dr = dtData.NewRow();
                                for (int i = 0; i < strData.Count(); i++)
                                {
                                    dr[i] = strData[i];
                                }
                                dtData.Rows.Add(dr);
                            }
                        }
                    }
                    else
                        WriteLog("Input File is Empty.");
                }
                else
                    WriteLog("File does not exist.");
                return dtData;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="strMsg"></param>
        public void WriteLog(string strMsg)
        {
            string strFileNm = @"C:\Qbei_Log\UriageLogFile\UriageLog.txt";
            StreamWriter swlog;
            if (!File.Exists(strFileNm))
            {
                swlog = new StreamWriter(strFileNm);
            }
            else
                swlog = File.AppendText(strFileNm);

            swlog.WriteLine(DateTime.Now);
            swlog.WriteLine(strMsg);
            swlog.WriteLine();
            swlog.Close();
        }

        //public string ConvertListToXml(List<T_Sale_Entity> lstParam)
        //{
        //    try {
        //        var xEle = new XElement("NewDataSet",lstParam.Select (y => new XElement("test",
        //                                                      new XElement("SaleDate", y.dtSaleDate.ToString("yyyy-MM-dd")),
        //                                                      new XElement("BranchCode", y.intBranchCode),
        //                                                      new XElement("Amount", y.decAmount),
        //                                                      new XElement("ID", y.intPackingID),
        //                                                      new XElement("OutsourcingID", y.intOutsourcingID),
        //                                                      new XElement("SalesCommissionID", y.intSalesCommissionID),
        //                                                      new XElement("CommercialTaxID", y.intCommercialTaxID),
        //                                                      new XElement("InsuranceID", y.intInsuranceID),
        //                                                      new XElement("FixedCostID", y.intFixedCostID),
        //                                                      new XElement("HolidayID", y.intHolidayID),
        //                                                      new XElement("QTY", y.intQTY),
        //                                                      new XElement("UnitPrice", y.decUnitPrice),
        //                                                      new XElement("CostType", y.strCostType),
        //                                                      new XElement("FinFlag", y.bolFinFlag),
        //                                                      new XElement("Overwrite_Amt", y.decOverwrite_Amt),
        //                                                      new XElement("Category", y.strShopCategory))));
        //        return xEle.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("Convert object list to Xml " + ex.Message);
        //        return null;
        //    }
        //}
        public string ConvertListToXml(List<T_Sale_Entity> lstParam)
        {
            try
            {
                var xEle = new XElement("NewDataSet", lstParam.Select(y => new XElement("test",
                                                              new XElement("SaleDate", y.dtSaleDate.ToString("yyyy-MM-dd")),
                                                              new XElement("BranchCode", y.intBranchCode),
                                                              new XElement("Amount", y.decAmount),
                                                              new XElement("ID", y.intPackingID),
                                                              new XElement("OutsourcingID", y.intOutsourcingID),
                                                              new XElement("SalesCommissionID", y.intSalesCommissionID),
                                                              new XElement("CommercialTaxID", y.intCommercialTaxID),
                                                              new XElement("InsuranceID", y.intInsuranceID),
                                                              new XElement("FixedCostID", y.intFixedCostID),
                                                              new XElement("HolidayID", y.intHolidayID),
                                                              new XElement("QTY", y.intQTY),
                                                              new XElement("UnitPrice", y.decUnitPrice),
                                                              new XElement("CostType", y.strCostType),
                                                              new XElement("FinFlag", y.bolFinFlag),
                                                              new XElement("Overwrite_Amt", y.decOverwrite_Amt),
                                                              new XElement("Category", y.strShopCategory),
                                                              new XElement("AccountTitle", y.strAccTitle))));
                return xEle.ToString();
            }
            catch (Exception ex)
            {
                WriteLog("Convert object list to Xml " + ex.Message);
                return null;
            }
        }
        public bool  ExportCsv (DataTable dtData, string strFileNm)
        {
            StreamWriter textWriter = null;
            try
            {
                WriteLog("Export Csv Start");
                if (File.Exists(strFileNm))
                {
                    File.Delete(strFileNm);
                }
                textWriter = new StreamWriter(strFileNm,true, Encoding.GetEncoding (932));
                foreach (DataColumn dc in dtData.Columns)
                {
                    textWriter.Write(dc.ColumnName);
                    if (dc.Ordinal < 3)
                        textWriter.Write(",");
                }
                textWriter.Write(textWriter.NewLine);
                foreach (DataRow dr in dtData.Rows)
                {
                    foreach (DataColumn dc in dtData.Columns)
                    {
                        textWriter.Write(dr[dc.ColumnName]);
                        if (dc.Ordinal < 3 )
                            textWriter.Write(",");
                    }
                    textWriter.Write(textWriter.NewLine);
                }
                textWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return false;
            }
            finally {
                WriteLog("Export Csv End");
                textWriter.Close();
            }
        }
    }
}
