using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using GemBox.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace BAL.DataTables
{
    static class FastExportingMethod
    {

        public static void ExportToExcel(DataTable dt, string outputPath)
        {
            // Create the Excel Application object
            ApplicationClass excelApp = new ApplicationClass();

            // Create a new Excel Workbook
            Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);

            int sheetIndex = 0;

            // Copy each DataTable
            //foreach (System.Data.DataTable dt in dataSet.DispancerDataSet.Tables)
            //{

                // Copy the DataTable to an object array
                object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

                // Copy the column names to the first row of the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    rawData[0, col] = dt.Columns[col].Caption;
                }

                // Copy the values to the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        if (dt.Rows[row][col] is DateTime)
                        {
                            rawData[row + 1, col] = (DateTime)dt.Rows[row][col];
                        }
                        else
                        rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
                    }
                }

                // Calculate the final column letter
                string finalColLetter = string.Empty;
                string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int colCharsetLen = colCharset.Length;

                if (dt.Columns.Count > colCharsetLen)
                {
                    finalColLetter = colCharset.Substring(
                        (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
                }

                finalColLetter += colCharset.Substring(
                        (dt.Columns.Count - 1) % colCharsetLen, 1);

                // Create a new Sheet
                Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);

                excelSheet.Name = "Export";

                // Fast data export to Excel
                string excelRange = string.Format("A1:{0}{1}",
                    finalColLetter, dt.Rows.Count + 1);

                excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

                // Mark the first row as BOLD
                ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
            //}

            // Save and Close the Workbook
            excelWorkbook.SaveAs(outputPath, XlFileFormat.xlWorkbookNormal, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelWorkbook.Close(true, Type.Missing, Type.Missing);
            excelWorkbook = null;

            // Release the Application object
            excelApp.Quit();
            excelApp = null;

            // Collect the unreferenced objects
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        public static void GemExportToExcel(DataTable dataTable, string outputPath)
        {
            // Create new ExcelFile.
            var workbook2 = new ExcelFile();

            // Imports all tables from DataSet to new file.
            //foreach (DataTable dataTable in dataSet.DispancerDataSet.Tables)
            //{
                // Add new worksheet to the file.
                var worksheet = workbook2.Worksheets.Add("Export");

                // Change the value of the first cell in the DataTable.
                //dataTable.Rows[0][0] = "This is new file!";

                // Insert the data from DataTable to the worksheet starting at cell "A1".
                worksheet.InsertDataTable(dataTable,
                    new InsertDataTableOptions("A1") { ColumnHeaders = true });
           //}
            workbook2.Save(outputPath);
            workbook2 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
