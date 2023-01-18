using System;
using System.Data;
using System.Drawing;
using System.IO;
using FTS.Base.Business;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FTS.Base.Model
{
    public class ImportDataObject
    {
        /// <summary>
        /// template import
        /// </summary>
        public DataTable dm_template { get; set; }

        /// <summary>
        /// template detail
        /// </summary>
        public DataTable dm_template_detail { get; set; }

        /// <summary>
        /// Data lấy từ excel
        /// </summary>
        public DataTable excelData { get; set; }

        /// <summary>
        /// Chuỗi data truyền lên từ FE
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Tạo file excel template import
        /// </summary>
        /// <param name="exceloutPutFile"></param>
        /// <param name="excelfolderfile"></param>
        /// <returns></returns>
        public string CreateExcelFile(string exceloutPutFile, string excelfolderfile)
        {
           
            DataView dvTemplateDetail = new DataView(this.dm_template_detail, string.Empty, "LIST_ORDER ASC", DataViewRowState.CurrentRows);

            string templateName = this.dm_template.Rows[0]["TEMPLATE_NAME"].ToString();
            int totalColumns = dvTemplateDetail.Count;
            int CurrentRow = 1;

            var existingFile = new FileInfo(excelfolderfile);
            ExcelPackage excel = new ExcelPackage(existingFile);

            // name of the sheet
            var worksheet = excel.Workbook.Worksheets.Add(templateName);
            //set font family
            worksheet.Cells.Style.Font.Name = "Arial";
            #region Write data to cell 
            int i = 1;
            int startRowData = CurrentRow + 1;
            foreach (DataRowView rows in dvTemplateDetail)
            {
                //set column name
                string excelColumnNo = rows["EXCEL_COLUMN_NO"].ToString();
                if (excelColumnNo == string.Empty)
                {
                    excelColumnNo = rows["DATA_COLUMN_NO"].ToString();
                }
                worksheet.Cells[CurrentRow, i].Value = excelColumnNo;
                worksheet.Cells[CurrentRow, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ExcelRange range_width = worksheet.Cells[CurrentRow, i];

                worksheet.Column(i).Width = 20;
                i++;
            }
            #endregion

            //format header
            ExcelRange range = worksheet.Cells[startRowData - 1, 1, startRowData - 1, totalColumns];
            range.Style.Font.Bold = true;
            range.Style.Font.Size = 10;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

            excel.Save();
            return exceloutPutFile;
        }
    }
}
