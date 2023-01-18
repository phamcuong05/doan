// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:51
// Project Name:              ReportBase
// Project Filename:          ReportBase.csproj
// Project Item Name:         FTSReport.cs
// Project Item Filename:     FTSReport.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Security;
using FTS.Base.Systems;
using FTS.Base.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace FTS.Base.Report {
    /// <summary>FTSReport la class goc cho tat ca cac bao cao trong he thong. FTSReport dinh nghia cac thuoc tinh co ban cua
    // mot bao cao, mot so ham de cac subclass co the ke thua, va mot so ham thu vien de cho cac subclass dung.
    ///Tat ca cac class bao cao khac trong he thong phai duoc ke thua tu class FTSReport.</summary>
    [Serializable] public abstract class FTSReport : IDisposable {
        public bool AllowDrill;
        private bool disposed;
        public bool IsDetail = true;
        public bool AlwaysHideSummary = false;

        #region Properties

        public string Report_ID;
        public string Parent_ID;
        public string Report_Group_ID;
        public int List_Order;
        public string Template_ID;
        public int Num_File_Report;
        public string Paper_Size;
        public string Page_Orient;
        public int Page_Margin_Top;
        public int Page_Margin_Bottom;
        public int Page_Margin_Left;
        public int Page_Margin_Right;
        public int Detail_Height;
        public int Header_Height;
        public string[] Required_Filter;
        public string[] Required_Filter_Distinct;
        public string[] Filter_List;
        public string[] Sub_Title = new string[5];
        public string[] SubtitleString = new string[5];
        public string[] Footer_name = new string[5];
        public string[] Group_Field = new string[5];
        public string[] Group_Table = new string[5];
        public string[] Sort_Field = new string[5];
        public string[] Sort_Direction = new string[5];
        public string Currency_Text = string.Empty;
        public string Title_Font_Name;
        public string Title_Font_Style;
        public int Title_Font_Size;
        public string Title_Font_Color;
        public string Subtitle_Font_Name;
        public string Subtitle_Font_Style;
        public int Subtitle_Font_Size;
        public string Subtitle_Font_Color;
        public string Header_Font_Name;
        public string Header_Font_Style;
        public int Header_Font_Size;
        public string Header_Font_Color;
        public string Footer_Font_Name;
        public string Footer_Font_Style;
        public int Footer_Font_Size;
        public string Footer_Font_Color;
        public string Group_Font_Name;
        public string Group_Font_Style;
        public int Group_Font_Size;
        public string Group_Font_Color;
        public string Detail_Font_Name;
        public string Detail_Font_Style;
        public int Detail_Font_Size;
        public string Detail_Font_Color;
        public bool Formula;
        public bool Repeat_Column;
        public bool Active;
        public string Print_Condition;
        public string Print_Condition_Extra;
        public string Print_Style;
        public bool Show_Order;
        public bool Show_Balance;
        public bool Show_Lke;
        public bool Show_Balance_Nte;
        public bool Show_Lke_Nte;
        public string Print_Date;
        public string FilterString;
        public string FilterStringWithoutOrganization;
        public string Template_Table_Tmp;
        public int Part = 1;
        public decimal BeginningBalance;
        public decimal EndingBalance;
        public decimal BeginningBalanceNte;
        public decimal EndingBalanceNte;
        public decimal LkeNo;
        public decimal LkeCo;
        public decimal LkeNoNte;
        public decimal LkeCoNte;
        public string Loai_Tte;
        public int Order_Width = 30;
        public bool Show_Hour = false;
        public int HourStart = 0;
        public int MinuteStart = 0;
        public int HourEnd = 23;
        public int MinuteEnd = 59;

        public string Comment_Text;
        public string Comment_Font_Name;
        public string Comment_Font_Style;
        public int Comment_Font_Size;
        public string Comment_Font_Color;
        public int Extra_Col = 5;

        protected DataTable mDataTable;
        [NonSerialized] public Sys_ReportField sys_reportfield;
        [NonSerialized] public Sys_ReportField sys_reportfieldgrid;
        [NonSerialized] private Sys_Report_Config sys_report_config;
        private DataSet mDataSet;
        private ReportPeriod mReportPeriod;
        [NonSerialized] private FTSMain mFTSMain;
        public string ReportDebitCredit = "DEB";
        private bool mShowReportPeriod = true;
        public Hashtable FilterStringTables = new Hashtable();
        public string ExcludedFilterStringTables = string.Empty;
        public string TableShowSubTitlesByName = string.Empty;
        public bool ShowToDate = false;
        #endregion

        private bool mExcel = false;
        public bool ShowZero = false;
        private List<FieldSelectionObject> mFieldSelection = new List<FieldSelectionObject>();

        public bool Excel {
            get { return this.mExcel; }
            set { this.mExcel = value; }
        }

        public ReportPeriod ReportPeriod {
            get { return this.mReportPeriod; }
            set { this.mReportPeriod = value; }
        }

        public bool ShowReportPeriod {
            get { return this.mShowReportPeriod; }
        }

        public DataSet DataSet {
            get { return this.mDataSet; }
            set { this.mDataSet = value; }
        }

        public DataTable DataTable {
            get { return this.mDataTable; }
        }

        public FTSMain FTSMain {
            get { return this.mFTSMain; }
            set { this.mFTSMain = value; }
        }

        public Sys_Report_Config Sys_Report_Config {
            get { return this.sys_report_config; }
            set { this.sys_report_config = value; }
        }

        public string Report_Name {
            get { return this.FTSMain.ResourceManager.GetReportName(this.Report_ID); }
            set { this.FTSMain.ResourceManager.SetReportName(this.Report_ID, value); }
        }

        public List<FieldSelectionObject> FieldSelection {
            get { return this.mFieldSelection; }
            set { this.mFieldSelection = value; }
        }

        public void SetFooter(int i, string footer) {
            this.FTSMain.ResourceManager.SetReportFooter(this.Report_ID, i, footer);
        }

        public string GetFooter(int i) {
            return this.FTSMain.ResourceManager.GetReportFooter(this.Report_ID, i);
        }

        public FTSReport() {}

        /**
		 *<summary>Constructor, khoi tao mot bao cao moi. Doc tu file dm_baocao trong CSDL de xac dinh cac thong so co ban cho bao cao nay.</summary>
				*/

        public FTSReport(string reportid, FTSMain ftsMain) {
            this.FTSMain = ftsMain;
            this.FTSMain.SecurityManager.CheckSecurity(new FTSFunction("REPORT_" + reportid, "ACC", true, false, false, false, false,false),
                DataAction.ViewAction);
            this.mDataSet = new DataSet("FTSReport");
            this.Report_ID = reportid;
            this.mReportPeriod = new ReportPeriod(string.Empty, this.FTSMain.DayStartOfFirstYear, this.FTSMain.DayStartOfCurrentYear);
            string sql = "Select * from sys_report where report_id=" + this.FTSMain.BuildParameterName("report_id");
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, this.Report_ID);
            this.mDataTable = this.FTSMain.DbMain.LoadDataTable(cmd, "sys_report");
            if (this.mDataTable.Rows.Count > 0) {
                this.Parent_ID = (string) this.mDataTable.Rows[0]["Parent_ID"];
                this.Report_Group_ID = (string) this.mDataTable.Rows[0]["Report_Group_ID"];
                this.List_Order = (int) this.mDataTable.Rows[0]["List_Order"];
                this.Template_ID = (string) this.mDataTable.Rows[0]["Template_ID"];
                string Template_Table = (string) this.mDataTable.Rows[0]["Template_Table_Tmp"];
                this.Template_Table_Tmp = ((string) this.mDataTable.Rows[0]["Template_Table_Tmp"]).Trim();
                if (this.Template_Table_Tmp.Trim() == string.Empty) {
                    this.Template_Table_Tmp = Template_Table + "_Tmp";
                }
                this.Num_File_Report = (int) this.mDataTable.Rows[0]["Num_File_Report"];
                this.Paper_Size = (string) this.mDataTable.Rows[0]["Paper_Size"];
                this.Page_Orient = (string) this.mDataTable.Rows[0]["Page_Orient"];
                this.Page_Margin_Top = (int) this.mDataTable.Rows[0]["Page_Margin_Top"];
                this.Page_Margin_Bottom = (int) this.mDataTable.Rows[0]["Page_Margin_Bottom"];
                this.Page_Margin_Left = (int) this.mDataTable.Rows[0]["Page_Margin_Left"];
                this.Page_Margin_Right = (int) this.mDataTable.Rows[0]["Page_Margin_Right"];
                if (this.Page_Margin_Top < 30) {
                    this.Page_Margin_Top = 30;
                }
                if (this.Page_Margin_Bottom < 30) {
                    this.Page_Margin_Bottom = 30;
                }
                if (this.Page_Margin_Left < 10) {
                    this.Page_Margin_Left = 10;
                }
                if (this.Page_Margin_Right < 10) {
                    this.Page_Margin_Right = 10;
                }
                this.Detail_Height = (int) this.mDataTable.Rows[0]["Detail_Height"];
                this.Header_Height = (int) this.mDataTable.Rows[0]["Header_Height"];
                this.Required_Filter =
                    FunctionsBase.ParseString(this.mDataTable.Rows[0]["Required_Filter"].ToString().ToUpper().Trim().Replace(" ", string.Empty));
                this.Required_Filter_Distinct =
                    FunctionsBase.ParseString(this.mDataTable.Rows[0]["Required_Filter_Distinct"].ToString().ToUpper().Trim().Replace(" ", string.Empty));
                    string filterlist = this.mDataTable.Rows[0]["Filter_List"].ToString().ToUpper().Trim().Replace(" ", string.Empty);
                    if (filterlist.EndsWith(",")) {
                        filterlist += "DM_ORGANIZATION";
                    } else {
                        filterlist += ",DM_ORGANIZATION";
                    }
                    this.Filter_List = FunctionsBase.ParseString(filterlist);
                this.Sub_Title[0] = this.mDataTable.Rows[0]["Subtitle1"].ToString().Trim();
                this.Sub_Title[1] = this.mDataTable.Rows[0]["Subtitle2"].ToString().Trim();
                this.Sub_Title[2] = this.mDataTable.Rows[0]["Subtitle3"].ToString().Trim();
                this.Sub_Title[3] = this.mDataTable.Rows[0]["Subtitle4"].ToString().Trim();
                this.Sub_Title[4] = this.mDataTable.Rows[0]["Subtitle5"].ToString().Trim();
                this.Currency_Text = this.mDataTable.Rows[0]["Currency_text"].ToString().Trim();
                this.Footer_name[0] = this.mDataTable.Rows[0]["Footer_name1"].ToString().Trim();
                this.Footer_name[1] = this.mDataTable.Rows[0]["Footer_name2"].ToString().Trim();
                this.Footer_name[2] = this.mDataTable.Rows[0]["Footer_name3"].ToString().Trim();
                this.Footer_name[3] = this.mDataTable.Rows[0]["Footer_name4"].ToString().Trim();
                this.Footer_name[4] = this.mDataTable.Rows[0]["Footer_name5"].ToString().Trim();
                this.Group_Field[0] = this.mDataTable.Rows[0]["Group_Field1"].ToString().Trim();
                this.Group_Field[1] = this.mDataTable.Rows[0]["Group_Field2"].ToString().Trim();
                this.Group_Field[2] = this.mDataTable.Rows[0]["Group_Field3"].ToString().Trim();
                this.Group_Field[3] = this.mDataTable.Rows[0]["Group_Field4"].ToString().Trim();
                this.Group_Field[4] = this.mDataTable.Rows[0]["Group_Field5"].ToString().Trim();
                this.Group_Table[0] = this.mDataTable.Rows[0]["Group_Table1"].ToString().Trim();
                this.Group_Table[1] = this.mDataTable.Rows[0]["Group_Table2"].ToString().Trim();
                this.Group_Table[2] = this.mDataTable.Rows[0]["Group_Table3"].ToString().Trim();
                this.Group_Table[3] = this.mDataTable.Rows[0]["Group_Table4"].ToString().Trim();
                this.Group_Table[4] = this.mDataTable.Rows[0]["Group_Table5"].ToString().Trim();
                this.Sort_Field[0] = this.mDataTable.Rows[0]["Sort_Field1"].ToString().Trim();
                this.Sort_Field[1] = this.mDataTable.Rows[0]["Sort_Field2"].ToString().Trim();
                this.Sort_Field[2] = this.mDataTable.Rows[0]["Sort_Field3"].ToString().Trim();
                this.Sort_Field[3] = this.mDataTable.Rows[0]["Sort_Field4"].ToString().Trim();
                this.Sort_Field[4] = this.mDataTable.Rows[0]["Sort_Field5"].ToString().Trim();
                this.Sort_Direction[0] = this.mDataTable.Rows[0]["Sort_Direction1"].ToString().Trim();
                this.Sort_Direction[1] = this.mDataTable.Rows[0]["Sort_Direction2"].ToString().Trim();
                this.Sort_Direction[2] = this.mDataTable.Rows[0]["Sort_Direction3"].ToString().Trim();
                this.Sort_Direction[3] = this.mDataTable.Rows[0]["Sort_Direction4"].ToString().Trim();
                this.Sort_Direction[4] = this.mDataTable.Rows[0]["Sort_Direction5"].ToString().Trim();
                this.Title_Font_Name = this.mDataTable.Rows[0]["Title_Font_Name"].ToString().Trim();
                this.Title_Font_Style = this.mDataTable.Rows[0]["Title_Font_Style"].ToString().Trim();
                this.Title_Font_Size = (int) this.mDataTable.Rows[0]["Title_Font_Size"];
                this.Title_Font_Color = this.mDataTable.Rows[0]["Title_Font_Color"].ToString().Trim();
                this.Subtitle_Font_Name = this.mDataTable.Rows[0]["Subtitle_Font_Name"].ToString().Trim();
                this.Subtitle_Font_Style = this.mDataTable.Rows[0]["Subtitle_Font_Style"].ToString().Trim();
                this.Subtitle_Font_Size = (int) this.mDataTable.Rows[0]["Subtitle_Font_Size"];
                this.Subtitle_Font_Color = this.mDataTable.Rows[0]["Subtitle_Font_Color"].ToString().Trim();
                this.Header_Font_Name = this.mDataTable.Rows[0]["Header_Font_Name"].ToString().Trim();
                this.Header_Font_Style = this.mDataTable.Rows[0]["Header_Font_Style"].ToString().Trim();
                this.Header_Font_Size = (int) this.mDataTable.Rows[0]["Header_Font_Size"];
                this.Header_Font_Color = this.mDataTable.Rows[0]["Header_Font_Color"].ToString().Trim();
                this.Footer_Font_Name = this.mDataTable.Rows[0]["Footer_Font_Name"].ToString().Trim();
                this.Footer_Font_Style = this.mDataTable.Rows[0]["Footer_Font_Style"].ToString().Trim();
                this.Footer_Font_Size = (int) this.mDataTable.Rows[0]["Footer_Font_Size"];
                this.Footer_Font_Color = this.mDataTable.Rows[0]["Footer_Font_Color"].ToString().Trim();
                this.Group_Font_Name = this.mDataTable.Rows[0]["Group_Font_Name"].ToString().Trim();
                this.Group_Font_Style = this.mDataTable.Rows[0]["Group_Font_Style"].ToString().Trim();
                this.Group_Font_Size = (int) this.mDataTable.Rows[0]["Group_Font_Size"];
                this.Group_Font_Color = this.mDataTable.Rows[0]["Group_Font_Color"].ToString().Trim();
                this.Detail_Font_Name = this.mDataTable.Rows[0]["Detail_Font_Name"].ToString().Trim();
                this.Detail_Font_Style = this.mDataTable.Rows[0]["Detail_Font_Style"].ToString().Trim();
                this.Detail_Font_Size = (int) this.mDataTable.Rows[0]["Detail_Font_Size"];
                this.Detail_Font_Color = this.mDataTable.Rows[0]["Detail_Font_Color"].ToString().Trim();

                this.Comment_Text = this.mDataTable.Rows[0]["COMMENT_TEXT"].ToString().Trim();
                this.Comment_Font_Name = this.mDataTable.Rows[0]["COMMENT_FONT_NAME"].ToString().Trim();
                this.Comment_Font_Style = this.mDataTable.Rows[0]["COMMENT_FONT_STYLE"].ToString().Trim();
                this.Comment_Font_Size = (int) this.mDataTable.Rows[0]["COMMENT_FONT_SIZE"];
                this.Comment_Font_Color = this.mDataTable.Rows[0]["COMMENT_FONT_COLOR"].ToString().Trim();
                if ((int) this.mDataTable.Rows[0]["EXTRA_COL"] > 0) {
                    this.Extra_Col = (int) this.mDataTable.Rows[0]["EXTRA_COL"];
                }

                this.Formula = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Formula"] == 1, true, false);
                this.Repeat_Column = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Repeat_Column"] == 1, true, false);
                this.Active = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Active"] == 1, true, false);
                this.Show_Order = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Order"] == 1, true, false);
                this.Show_Hour = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Hour"] == 1, true, false);
                this.Show_Balance = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Balance"] == 1, true, false);
                this.Show_Lke = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Lke"] == 1, true, false);
                this.Show_Balance_Nte = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Balance_Nte"] == 1, true, false);
                this.Show_Lke_Nte = (bool) FunctionsBase.IIF((Int16) this.mDataTable.Rows[0]["Show_Lke_Nte"] == 1, true, false);
                this.Print_Condition = this.mDataTable.Rows[0]["Print_Condition"].ToString().Trim();
                this.Print_Condition_Extra = this.Print_Condition;
                this.Print_Style = this.mDataTable.Rows[0]["Print_Style"].ToString().Trim();
                this.Loai_Tte = this.mDataTable.Rows[0]["Loai_Tte"].ToString().Trim();
                this.Order_Width = (int) this.mDataTable.Rows[0]["Order_Width"];
                this.sys_reportfield = new Sys_ReportField(this.FTSMain, this.Report_ID);
                this.sys_reportfieldgrid = new Sys_ReportField(this.FTSMain, this.Report_ID);
                this.sys_reportfieldgrid.LoadVisibleData();
                this.sys_report_config = new Sys_Report_Config(this.FTSMain, this.Report_ID);
                try {
                    this.mShowReportPeriod = (this.sys_report_config.GetConfigValue("SHOW_REPORT_PERIOD").ToString().Trim() == "0") ? false : true;
                } catch {
                    this.mShowReportPeriod = true;
                }
            } else {
                throw (new FTSException("MSG_REPORT_ID_NOTFOUND"));
            }
            this.SetPrintDate();
            this.SetFont();
        }

        private void SetFont() {
            if (this.mFTSMain.Language == Language.JP) {
                this.Title_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Subtitle_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Header_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Footer_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Comment_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Group_Font_Name = FTSConstant.JAPANESE_FONT;
                this.Detail_Font_Name = FTSConstant.JAPANESE_FONT;
            }
            if (this.mFTSMain.Language == Language.LAOS) {
                this.Title_Font_Name = FTSConstant.LAOS_FONT;
                this.Subtitle_Font_Name = FTSConstant.LAOS_FONT;
                this.Header_Font_Name = FTSConstant.LAOS_FONT;
                this.Comment_Font_Name = FTSConstant.LAOS_FONT;
                this.Footer_Font_Name = FTSConstant.LAOS_FONT;
                this.Group_Font_Name = FTSConstant.LAOS_FONT;
                this.Detail_Font_Name = FTSConstant.LAOS_FONT;
            }
        }

        /**
		*<summary>Tinh toan de tao ra file du lieu de ra bao cao.
 *Ham nay can duoc override boi cac subclass de lay du lieu cho tung bao cao.</summary>
		*/

        public virtual void CalculateReport() {}

        

        /**
		*<summary>Luu thay doi nhung thong so bao cao vao CSDL.</summary>
		*/

        public void UpdateReport() {
            bool allowupdate = true;
                if (
                    !this.FTSMain.SecurityManager.CheckSecurityInvisible(FTS.Base.Security.FTSFunctionCollection.REPORT_MANAGEMENT, DataAction.EditAction,
                        string.Empty)) {
                    allowupdate = false;
                }
            if (allowupdate) {
                this.mDataTable.Rows[0]["Num_File_Report"] = this.Num_File_Report;
                this.mDataTable.Rows[0]["Paper_Size"] = this.Paper_Size;
                this.mDataTable.Rows[0]["Page_Orient"] = this.Page_Orient;
                this.mDataTable.Rows[0]["Page_Margin_Top"] = this.Page_Margin_Top;
                this.mDataTable.Rows[0]["Page_Margin_Bottom"] = this.Page_Margin_Bottom;
                this.mDataTable.Rows[0]["Page_Margin_Left"] = this.Page_Margin_Left;
                this.mDataTable.Rows[0]["Page_Margin_Right"] = this.Page_Margin_Right;
                this.mDataTable.Rows[0]["Detail_Height"] = this.Detail_Height;
                this.mDataTable.Rows[0]["HEADER_HEIGHT"] = this.Header_Height;
                this.mDataTable.Rows[0]["Subtitle1"] = this.Sub_Title[0];
                this.mDataTable.Rows[0]["Subtitle2"] = this.Sub_Title[1];
                this.mDataTable.Rows[0]["Subtitle3"] = this.Sub_Title[2];
                this.mDataTable.Rows[0]["Subtitle4"] = this.Sub_Title[3];
                this.mDataTable.Rows[0]["Subtitle5"] = this.Sub_Title[4];
                this.mDataTable.Rows[0]["Currency_Text"] = this.Currency_Text;
                this.mDataTable.Rows[0]["Footer_name1"] = this.Footer_name[0];
                this.mDataTable.Rows[0]["Footer_name2"] = this.Footer_name[1];
                this.mDataTable.Rows[0]["Footer_name3"] = this.Footer_name[2];
                this.mDataTable.Rows[0]["Footer_name4"] = this.Footer_name[3];
                this.mDataTable.Rows[0]["Footer_name5"] = this.Footer_name[4];
                this.mDataTable.Rows[0]["Group_Field1"] = this.Group_Field[0];
                this.mDataTable.Rows[0]["Group_Field2"] = this.Group_Field[1];
                this.mDataTable.Rows[0]["Group_Field3"] = this.Group_Field[2];
                this.mDataTable.Rows[0]["Group_Field4"] = this.Group_Field[3];
                this.mDataTable.Rows[0]["Group_Field5"] = this.Group_Field[4];
                this.mDataTable.Rows[0]["Group_Table1"] = this.Group_Table[0];
                this.mDataTable.Rows[0]["Group_Table2"] = this.Group_Table[1];
                this.mDataTable.Rows[0]["Group_Table3"] = this.Group_Table[2];
                this.mDataTable.Rows[0]["Group_Table4"] = this.Group_Table[3];
                this.mDataTable.Rows[0]["Group_Table5"] = this.Group_Table[4];
                this.mDataTable.Rows[0]["Sort_Field1"] = this.Sort_Field[0];
                this.mDataTable.Rows[0]["Sort_Field2"] = this.Sort_Field[1];
                this.mDataTable.Rows[0]["Sort_Field3"] = this.Sort_Field[2];
                this.mDataTable.Rows[0]["Sort_Field4"] = this.Sort_Field[3];
                this.mDataTable.Rows[0]["Sort_Field5"] = this.Sort_Field[4];
                this.mDataTable.Rows[0]["Sort_Direction1"] = this.Sort_Direction[0];
                this.mDataTable.Rows[0]["Sort_Direction2"] = this.Sort_Direction[1];
                this.mDataTable.Rows[0]["Sort_Direction3"] = this.Sort_Direction[2];
                this.mDataTable.Rows[0]["Sort_Direction4"] = this.Sort_Direction[3];
                this.mDataTable.Rows[0]["Sort_Direction5"] = this.Sort_Direction[4];
                this.mDataTable.Rows[0]["Title_Font_Name"] = this.Title_Font_Name;
                this.mDataTable.Rows[0]["Title_Font_Style"] = this.Title_Font_Style;
                this.mDataTable.Rows[0]["Title_Font_Size"] = this.Title_Font_Size;
                this.mDataTable.Rows[0]["Title_Font_Color"] = this.Title_Font_Color;
                this.mDataTable.Rows[0]["Subtitle_Font_Name"] = this.Subtitle_Font_Name;
                this.mDataTable.Rows[0]["Subtitle_Font_Style"] = this.Subtitle_Font_Style;
                this.mDataTable.Rows[0]["Subtitle_Font_Size"] = this.Subtitle_Font_Size;
                this.mDataTable.Rows[0]["Subtitle_Font_Color"] = this.Subtitle_Font_Color;
                this.mDataTable.Rows[0]["Header_Font_Name"] = this.Header_Font_Name;
                this.mDataTable.Rows[0]["Header_Font_Style"] = this.Header_Font_Style;
                this.mDataTable.Rows[0]["Header_Font_Size"] = this.Header_Font_Size;
                this.mDataTable.Rows[0]["Header_Font_Color"] = this.Header_Font_Color;
                this.mDataTable.Rows[0]["Footer_Font_Name"] = this.Footer_Font_Name;
                this.mDataTable.Rows[0]["Footer_Font_Style"] = this.Footer_Font_Style;
                this.mDataTable.Rows[0]["Footer_Font_Size"] = this.Footer_Font_Size;
                this.mDataTable.Rows[0]["Footer_Font_Color"] = this.Footer_Font_Color;
                this.mDataTable.Rows[0]["Group_Font_Name"] = this.Group_Font_Name;
                this.mDataTable.Rows[0]["Group_Font_Style"] = this.Group_Font_Style;
                this.mDataTable.Rows[0]["Group_Font_Size"] = this.Group_Font_Size;
                this.mDataTable.Rows[0]["Group_Font_Color"] = this.Group_Font_Color;
                this.mDataTable.Rows[0]["Detail_Font_Name"] = this.Detail_Font_Name;
                this.mDataTable.Rows[0]["Detail_Font_Style"] = this.Detail_Font_Style;
                this.mDataTable.Rows[0]["Detail_Font_Size"] = this.Detail_Font_Size;
                this.mDataTable.Rows[0]["Detail_Font_Color"] = this.Detail_Font_Color;
                this.mDataTable.Rows[0]["Print_Condition"] = this.Print_Condition;
                this.mDataTable.Rows[0]["Print_Style"] = this.Print_Style;
                this.mDataTable.Rows[0]["Loai_Tte"] = this.Loai_Tte;
                this.mDataTable.Rows[0]["Show_Order"] = FunctionsBase.IIF(this.Show_Order, 1, 0);
                this.mDataTable.Rows[0]["Show_Hour"] = FunctionsBase.IIF(this.Show_Hour, 1, 0);
                this.mDataTable.Rows[0]["Show_Balance"] = FunctionsBase.IIF(this.Show_Balance, 1, 0);
                this.mDataTable.Rows[0]["Show_Lke"] = FunctionsBase.IIF(this.Show_Lke, 1, 0);
                this.mDataTable.Rows[0]["Show_Balance_Nte"] = FunctionsBase.IIF(this.Show_Balance_Nte, 1, 0);
                this.mDataTable.Rows[0]["Show_Lke_Nte"] = FunctionsBase.IIF(this.Show_Lke_Nte, 1, 0);
                this.mDataTable.Rows[0]["Order_Width"] = this.Order_Width;

                this.mDataTable.Rows[0]["Comment_Text"] = this.Comment_Text;
                this.mDataTable.Rows[0]["Comment_Font_Name"] = this.Footer_Font_Name;
                this.mDataTable.Rows[0]["Comment_Font_Style"] = this.Footer_Font_Style;
                this.mDataTable.Rows[0]["Comment_Font_Size"] = this.Footer_Font_Size;
                this.mDataTable.Rows[0]["Comment_Font_Color"] = this.Footer_Font_Color;

                this.mDataTable.Rows[0]["Extra_Col"] = this.Extra_Col;

                this.FTSMain.DbMain.UpdateTable(this.mDataTable, null, this.FTSMain.DbMain.CreateUpdateCommand("sys_report", this.mDataTable, "REPORT_ID"), null,
                    UpdateBehavior.Standard);
            }
        }

        public virtual void Prepare() {
            DataTable tblsource = this.mDataSet.Tables[this.Template_Table_Tmp];

            for (int i = 0; i < this.Extra_Col; i++) {
                string colname = "EXTRA_" + Convert.ToString(i + 1);
                if (tblsource.Columns.IndexOf(colname) < 0) {
                    DataRow fieldrow = this.sys_reportfield.GetRow(colname);
                    if (fieldrow != null && (Int16) fieldrow["visible"] == 1 && fieldrow["formula"].ToString().Trim() != string.Empty) {
                        DataColumn c = null;
                        try {
                            c = new DataColumn(colname, Type.GetType("System.Decimal"), fieldrow["formula"].ToString().Trim());
                            c.DefaultValue = 0;
                            tblsource.Columns.Add(c);
                        } catch (Exception) {
                            if (c != null) {
                                c.Expression = string.Empty;
                            }
                            this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_WRONG_FORMULA"));
                        }
                    } else {
                        DataColumn c = new DataColumn(colname, Type.GetType("System.Decimal"));
                        c.DefaultValue = 0;
                        tblsource.Columns.Add(c);
                    }
                }
            }
            for (int i = 0; i < 5; i++) {
                string colname = "EXTRA_STRING_" + Convert.ToString(i + 1);
                if (tblsource.Columns.IndexOf(colname) < 0) {
                    DataRow fieldrow = this.sys_reportfield.GetRow(colname);
                    if (fieldrow != null && (Int16) fieldrow["visible"] == 1 && fieldrow["formula"].ToString().Trim() != string.Empty) {
                        try {
                            DataColumn c = new DataColumn(colname, Type.GetType("System.String"), fieldrow["formula"].ToString().Trim());
                            c.DefaultValue = string.Empty;
                            tblsource.Columns.Add(c);
                        } catch (Exception) {
                            this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_WRONG_FORMULA"));
                        }
                    } else {
                        DataColumn c = new DataColumn(colname, Type.GetType("System.String"));
                        c.DefaultValue = string.Empty;
                        tblsource.Columns.Add(c);
                    }
                }
            }
            for (int i = 0; i < 5; i++) {
                string colname = "GROUP_NAME" + Convert.ToString(i + 1);
                if (tblsource.Columns.IndexOf(colname) < 0) {
                    DataColumn c = new DataColumn(colname, Type.GetType("System.String"));
                    c.DefaultValue = string.Empty;
                    tblsource.Columns.Add(c);
                }
            }
            DataSet ds1 = new DataSet();
            foreach (DataRow row in tblsource.Rows) {
                for (int i = 0; i < 5; i++) {
                    if (this.Group_Field[i] != string.Empty & this.Group_Table[i] != string.Empty) {
                        if (this.DataSet.Tables[this.Template_Table_Tmp].Columns.IndexOf(this.Group_Field[i]) >= 0) {
                            DataTable dt = ds1.Tables[this.Group_Table[i]];
                            string field_name = this.FTSMain.TableManager.GetNameField(this.Group_Table[i]);
                            if (dt == null) {
                                //To get the accountname not in Vietnamese
                                    if (this.Group_Table[i] == "DM_CTIEU") {
                                        try {
                                            dt =
                                                this.FTSMain.DbMain.LoadDataTable(
                                                    this.FTSMain.DbMain.GetSqlStringCommand(
                                                        "SELECT DISTINCT ID_FIELD,'' AS DESCRIPTION FROM SYS_REPORT_FORMULA WHERE REPORT_ID='" + this.Report_ID +
                                                        "'"), "DM_CTIEU");
                                            dt.PrimaryKey = new DataColumn[] {dt.Columns["ID_FIELD"]};
                                            foreach (DataRow ctieurow in dt.Rows) {
                                                ctieurow["DESCRIPTION"] = this.FTSMain.ResourceManager.GetFinancialReportDescription(this.Report_ID,
                                                    ctieurow["ID_FIELD"].ToString());
                                            }
                                        } catch (Exception ex) {
                                            this.FTSMain.ExceptionManager.ProcessException(ex);
                                        }
                                    } else {
                                        try {
                                            dt = this.FTSMain.TableManager.LoadTable(ds1, this.Group_Table[i],
                                                this.FTSMain.TableManager.GetIDField(this.Group_Table[i]) + "," + field_name, "1=1");
                                        } catch (Exception ex) {
                                            this.FTSMain.ExceptionManager.ProcessException(ex);
                                        }
                                    }
                                
                            }
                            if (dt != null) {
                                DataRow foundrow = dt.Rows.Find(row[this.Group_Field[i]]);
                                if (foundrow != null) {
                                    try {
                                        row["GROUP_NAME" + Convert.ToString(i + 1)] = foundrow[field_name];
                                    } catch (Exception ex) {
                                        int k = 3;
                                    }
                                }
                            }
                        } else {
                            this.Group_Field[i] = string.Empty;
                            this.Group_Table[i] = string.Empty;
                        }
                    }
                }
            }
            Functions.ClearDataSet(ds1);
            this.UpdateField(tblsource);
        }

        private void UpdateField(DataTable tblsource) {
            string sql = "select max(field_order) from sys_reportfield where report_id=" + this.FTSMain.BuildParameterName("report_id");
            object oj = null;
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, this.Report_ID);
            oj = this.FTSMain.DbMain.ExecuteScalar(cmd);
            int last_field_order = 0;
            if (oj != null && oj != DBNull.Value) {
                last_field_order = Convert.ToInt32(oj);
            }
            foreach (DataColumn c in tblsource.Columns) {
                if (this.sys_reportfield.GetRow(c.ColumnName) == null) {
                    DataRow row = this.sys_reportfield.AddNew();
                    row["report_id"] = this.Report_ID;
                    row["field_id"] = c.ColumnName.ToUpper().Trim();
                    if (c.DataType == Type.GetType("System.String")) {
                        row["field_type"] = "STRING";
                        row["field_width"] = 20;
                    }
                    if (c.DataType == Type.GetType("System.Decimal") || c.DataType == Type.GetType("System.Int16") || c.DataType == Type.GetType("System.Int32")) {
                        row["field_type"] = "NUMBER";
                        row["field_width"] = 15;
                    }
                    if (c.DataType == Type.GetType("System.DateTime")) {
                        row["field_type"] = "DATE";
                        row["field_width"] = 10;
                    }
                    if (c.DataType == Type.GetType("System.Byte[]")) {
                        row["field_type"] = "IMAGE";
                        row["field_width"] = 20;
                    }
                    row["part"] = 1;
                    row["field_angle"] = 0;
                    last_field_order++;
                    row["field_order"] = last_field_order;
                    row["show_in_group"] = 0;
                }
            }
            for (int i = 1; i <= this.Extra_Col; i++) {
                string extracol = "EXTRA_" + i.ToString().Trim();
                if (this.sys_reportfield.GetRow(extracol) == null) {
                    DataRow row = this.sys_reportfield.AddNew();
                    row["report_id"] = this.Report_ID;
                    row["field_id"] = extracol;
                    row["field_type"] = "NUMBER";
                    row["field_width"] = 15;
                    row["part"] = 1;
                    row["field_angle"] = 0;
                    last_field_order++;
                    row["field_order"] = last_field_order;
                    row["show_in_group"] = 0;
                }
            }
            for (int i = 1; i <= 5; i++) {
                string extracol = "EXTRA_STRING_" + i.ToString().Trim();
                if (this.sys_reportfield.GetRow(extracol) == null) {
                    DataRow row = this.sys_reportfield.AddNew();
                    row["report_id"] = this.Report_ID;
                    row["field_id"] = extracol;
                    row["field_type"] = "STRING";
                    row["field_width"] = 20;
                    row["part"] = 1;
                    row["field_angle"] = 0;
                    last_field_order++;
                    row["field_order"] = last_field_order;
                    row["show_in_group"] = 0;
                }
            }
            List<DataRow> removelist = new List<DataRow>();
            foreach (DataRow row in this.sys_reportfield.DataTable.Rows) {
                if (row.RowState != DataRowState.Deleted && tblsource.Columns.IndexOf(row["field_id"].ToString().Trim()) < 0) {
                    removelist.Add(row);
                }
            }
            foreach (DataRow row in removelist) {
                row.Delete();
            }
            this.sys_reportfield.UpdateData();
        }

        /**
		*<summary>Luu file bao cao de gui len don vi cap tren.
 *Ham nay se duoc goi tu form dau ra cua bao cao, khi NSD chon luu bao cao sau khi xem.</summary>
		*/

        public virtual void SaveReportData() {}

        public virtual void DrillDown(DataRow row) {}

        public void ClearReport() {
            Functions.ClearDataSet(this.mDataSet);
            this.mDataSet = new DataSet();
        }

        public void ClearAll() {
            try {
                Functions.ClearDataSet(this.mDataSet);
                if (this.sys_reportfield != null) {
                    this.sys_reportfield = null;
                }
                if (this.sys_reportfieldgrid != null) {
                    this.sys_reportfieldgrid = null;
                }
                this.Group_Field = null;
                this.Group_Table = null;
                this.Sort_Field = null;
                this.Sort_Direction = null;
                this.Sub_Title = null;
                this.FilterString = null;
                this.FilterStringWithoutOrganization = null;
                this.Filter_List = null;
                this.Required_Filter = null;
            } catch (Exception) {}
        }

        public void SetPrintDate() {
            if ((bool) this.FTSMain.SystemVars.GetSystemVars("REPORT_PRINT_DATE_WITH_TIME")) {
                this.Print_Date = DateTime.Today.ToLongDateString() + "," + DateTime.Now.ToLongTimeString();
            } else {
                if (this.FTSMain.Language == Language.VN) {
                    this.Print_Date = this.FTSMain.MsgManager.GetMessage("MSG_DAY") + " " + DateTime.Today.Day + " " +
                                      this.FTSMain.MsgManager.GetMessage("MSG_MONTH").ToLower() + " " + DateTime.Today.Month + " " +
                                      this.FTSMain.MsgManager.GetMessage("MSG_YEAR").ToLower() + " " + DateTime.Today.Year;
                } else {
                    if (this.FTSMain.Language == Language.JP) {
                        this.Print_Date = DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
                    } else {
                        this.Print_Date = DateTime.Today.ToLongDateString();
                    }
                }
            }
        }

        public bool ShowHeader0() {
            DataView dv = new DataView(this.sys_reportfieldgrid.DataTable, "visible=1 and field_width > 0", "field_order", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                if (drv["field_group_id"].ToString().Trim() != string.Empty) {
                    return true;
                }
            }
            return false;
        }

        public void CheckInput() {
            for (int i = 0; i < this.Required_Filter.Length; i++) {
                string fiedname = this.Required_Filter[i];
                if (this.GetNumberOfSelectedItems(fiedname) == 0) {
                    throw (new FTSException("MSG_SELECTATLEASTONE"));
                }
            }
            for (int i = 0; i < this.Required_Filter_Distinct.Length; i++) {
                string fiedname = this.Required_Filter_Distinct[i];
                if (this.GetNumberOfSelectedItems(fiedname) != 1) {
                    throw (new FTSException("MSG_SELECTONLYONE"));
                }
            }
        }

        public int GetNumberOfSelectedItems(string fieldname) {
            int count = 0;
            foreach (FieldSelectionObject selectfield in this.FieldSelection) {
                if (selectfield.FieldName.ToUpper() == fieldname.ToUpper()) {
                    count++;
                }
            }
            return count;

            //DataTable ddt = this.FTSMain.XmlQuery.Join2Tables("ReportPara", "SYS_TABLE", "TABLE_NAME,TABLE_NAME",
            //    "report_id='" + this.Report_ID + "' and ID_FIELD = '" + fieldname + "'", "");
            //if (ddt != null) {
            //    return Convert.ToInt32(ddt.Rows.Count);
            //} else {
            //    return 0;
            //}
        }

        public string GetSelectedValue(string fieldname) {
            //if (this.FTSMain.IsWeb == false) {
            //    DataTable ddt = this.FTSMain.XmlQuery.Join2Tables("ReportPara", "SYS_TABLE", "TABLE_NAME,TABLE_NAME",
            //        "report_id='" + this.Report_ID + "' and ID_FIELD = '" + fieldname + "'", "");
            //    if (ddt != null && ddt.Rows.Count > 0) {
            //        return ddt.Rows[0]["ID"].ToString();
            //    } else {
            //        return string.Empty;
            //    }
            //} else {

            //}
            foreach (FieldSelectionObject selectfield in this.FieldSelection) {
                if (selectfield.FieldName.ToUpper() == fieldname.ToUpper()) {
                    return selectfield.FieldValue;
                }
            }
            return string.Empty;
        }

        //public void SetSelectedValue(string tablename, string fieldname, string fieldvalue, string fielddisplayvalue) {
        //    DataTable ddt = this.FTSMain.XmlQuery.Join2Tables("ReportPara", "SYS_TABLE", "TABLE_NAME,TABLE_NAME",
        //        "report_id='" + this.Report_ID + "' and ID_FIELD = '" + fieldname + "'", "");
        //    DataTable dt = this.FTSMain.XmlQuery.Dataset.Tables["REPORTPARA"];
        //    foreach (DataRow dr in ddt.Rows) {
        //        for (int i = 0; i < dt.Rows.Count; i++) {
        //            if (dr["REPORT_ID"].ToString() ==
        //                this.FTSMain.XmlQuery.Dataset.Tables["REPORTPARA"].Rows[i]["REPORT_ID"] &&
        //                dr["ID"].ToString() ==
        //                this.FTSMain.XmlQuery.Dataset.Tables["REPORTPARA"].Rows[i]["ID"].ToString() &&
        //                dr["TABLE_NAME"].ToString() ==
        //                this.FTSMain.XmlQuery.Dataset.Tables["REPORTPARA"].Rows[i]["TABLE_NAME"].ToString()) {
        //                this.FTSMain.XmlQuery.Dataset.Tables["REPORTPARA"].Rows.RemoveAt(i);
        //            }
        //        }
        //    }
        //    string sql = "INSERT INTO REPORTPARA VALUES ('" + this.Report_ID + "','" + tablename + "','" +
        //                 this.mFTSMain.TableManager.GetDisplayName(tablename) + "','" + fieldvalue + "')";
        //    this.FTSMain.XmlQuery.ExecuteNonQuery(sql);
        //}

        public string GetSelectedValues(string fieldname) {
            //string retval = string.Empty;
            //DataTable ddt = this.FTSMain.XmlQuery.Join2Tables("ReportPara", "SYS_TABLE", "TABLE_NAME,TABLE_NAME",
            //    "report_id='" + this.Report_ID + "' and ID_FIELD = '" + fieldname + "'", "");
            //foreach (DataRow row in ddt.Rows) {
            //    retval += row["ID"].ToString().Trim() + ",";
            //}
            //ddt.Clear();
            //if (retval != string.Empty) {
            //    retval = retval.Substring(0, retval.Length - 1);
            //}
            //return retval;

            string retval = string.Empty;
            foreach (FieldSelectionObject selectfield in this.FieldSelection) {
                if (selectfield.FieldName.ToUpper() == fieldname.ToUpper()) {
                    retval += selectfield.FieldValue + ",";
                }
            }
            if (retval != string.Empty) {
                retval = retval.Substring(0, retval.Length - 1);
            }
            return retval;
        }

        public virtual string GetLastGroup() {
            for (int i = 4; i >= 0; i--) {
                if (this.Group_Field[i].Trim() != string.Empty) {
                    return this.Group_Field[i].Trim();
                }
            }
            return string.Empty;
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    this.ClearAll();
                }
            }
            this.disposed = true;
        }

        public void SetReportPeriod(DateTime daystartoffirstyear, DateTime daystartofyear, DateTime daystart, DateTime dayend) {
            string s = string.Empty;
            if (this.ReportPeriod != null) {
                s = this.ReportPeriod.ReportPeriodName;
            }
            this.ReportPeriod = ReportPeriod.GetReportPeriod(this.FTSMain, Functions.ParseDate(daystart) + "_" + Functions.ParseDate(dayend));
            if (this.ReportPeriod == null) {
                this.ReportPeriod = new ReportPeriod(s, daystartoffirstyear, daystart, dayend, daystartofyear);
            }
        }

        private string result = string.Empty;

        public virtual string GetPara(string para) {
            return string.Empty;
        }
    }
}