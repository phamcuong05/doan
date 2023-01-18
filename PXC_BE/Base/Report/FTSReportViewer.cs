// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:51
// Project Name:              ReportBase
// Project Filename:          ReportBase.csprojd
// Project Item Name:         FTSReportViewer.cs
// Project Item Filename:     FTSReportViewer.cs
// Project Item Kind:         Component
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using FTS.Base.Systems;
using FTS.Base.Utilities;
using FTS.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion

namespace FTS.Base.Report {
    public class FTSReportViewer : FTSReportViewerBase {
        private DetailBand Detail;
        private PageHeaderBand PageHeader;
        private PageFooterBand PageFooter;
        private ReportHeaderBand ReportHeader;
        private Container components = null;
        private XRTable tblHeader;
        private XRTable tblDetail;
        private XRTableRow rowDetail;
        private XRTableRow rowHeader;
        private GroupHeaderBand GroupHeader1;
        private GroupHeaderBand GroupHeader2;
        private GroupHeaderBand GroupHeader3;
        private GroupHeaderBand GroupHeader4;
        private GroupHeaderBand GroupHeader5;
        private ReportFooterBand ReportFooter;
        private XRTableRow rowGroup5;
        private XRTableRow rowGroup4;
        private XRTableRow rowGroup3;
        private XRTableRow rowGroup2;
        private XRTable tblGroup1;
        private XRTable tblFooter;
        private XRTableRow rowGroup1;
        private XRTable tblGroup2;
        private XRTable tblGroup3;
        private XRTable tblGroup4;
        private XRTable tblGroup5;
        private XRTableRow rowFooter;
        private GroupHeaderBand GroupHeader0;
        private int orderdetail = 1;
        private int group1detail = 1;
        private int group2detail = 1;
        private int group3detail = 1;
        private int group4detail = 1;
        private int group5detail = 1;
        private int mOrderWidth = 30;
        private int mGroupHeight = 23, mLabelHeight = 25, mFirstLevelHeight = 35, mSecondLevelHeight = 23;
        private DataSet mDataSetTmp;
        private int mFooterSpace = 100;
        private string mThousandSymbol = string.Empty;
        private CultureInfo mCultureInfo;
        private bool mShowGroupId = true;
        private int mRepeatCount = 2;
        private FTSMain mFTSMain;
        private FTSReport mReport;
        private int mTotalPageWidth = 0;

        public FTSReportViewer() {
            this.InitializeComponent();
        }

        public FTSReportViewer(FTSMain ftsmain, FTSReport rpt, bool export) : base() {
            this.mFTSMain = ftsmain;
            this.mReport = rpt;
            this.mOrderWidth = this.mReport.Order_Width;
            this.mGroupHeight = this.mReport.Detail_Height;
            if (this.mReport.Excel == false) {
                this.mThousandSymbol = this.mFTSMain.SystemVars.GetSystemVars("THOUSAND_SYMBOL").ToString().Trim();
                if (this.mThousandSymbol == string.Empty) {
                    this.mThousandSymbol = " ";
                }
                this.mCultureInfo = new CultureInfo("vi-VN", true);
                this.mCultureInfo.NumberFormat.NumberGroupSeparator = this.mThousandSymbol;
                this.mCultureInfo.NumberFormat.NumberDecimalSeparator = this.mFTSMain.SystemVars.GetSystemVars("DECIMAL_SYMBOL").ToString().Trim();
                DateTimeFormatInfo d = new DateTimeFormatInfo();
                d.ShortDatePattern = "dd/MM/yyyy";
                this.mCultureInfo.DateTimeFormat = d;
                Thread.CurrentThread.CurrentCulture = this.mCultureInfo;
                FormatInfo.AlwaysUseThreadFormat = true;
            } else {
                this.mCultureInfo = CultureInfo.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = this.mCultureInfo;
                FormatInfo.AlwaysUseThreadFormat = true;
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "visible=1", "field_order", DataViewRowState.CurrentRows);
            if (dv.Count > 0) {
                if ((int) dv[0]["field_width"] == 0) {
                    this.mShowGroupId = false;
                }
            }
            this.InitializeComponent();
            this.GetRepeatCount();
            this.SetData();
            this.SetPage();
            this.SetPart();
            this.CreateStyle();

            this.CreateDetailField();

            this.CreateGroupField();
            this.MoveGroupField();
            this.CreateTotal();
            this.MoveFieldUp();
            if (this.mReport.Excel == false) {
                this.CreateCustomFieldsBefore();
                this.CreateReportHeader();
                this.CreateBalanceLuykeHeader();
                this.CreateCurrencyText();
                this.CreateBalanceLuykeFooter();
                this.CreateFooterText();
                    this.CreateFooterNameText();
                this.CreatePageFooter();
            } else {
                this.CreateReportHeaderExcel();
                this.CreateBalanceLuykeHeaderExcel();
            }
            this.CreateHeader();
            this.SetFieldFormat();
            this.SetFont();
            //this.SetSort();
            this.HideAfterFirstPage();
            this.SetOthers();
            this.SetTH();
            this.ChangeGroup();
            this.CreateCustomFieldsAfter();
            this.AllowResizeColumn = true;
            if ((bool) this.mFTSMain.SystemVars.GetSystemVars("USE_DOTTED_REPORT_LINE") && export == false) {
                if (this.mReport.Print_Style == "DETAIL") {
                    this.Detail.BeforePrint += this.Detail_BeforePrint;
                }
                //this.Detail.AfterPrint += Detail_AfterPrint;
                if (this.mReport.Group_Field[0].Trim() != string.Empty) {
                    this.GroupHeader1.BeforePrint += this.GroupHeader1_BeforePrint;
                }
                if (this.mReport.Group_Field[1].Trim() != string.Empty) {
                    this.GroupHeader2.BeforePrint += this.GroupHeader2_BeforePrint;
                }
                if (this.mReport.Group_Field[2].Trim() != string.Empty) {
                    this.GroupHeader3.BeforePrint += this.GroupHeader3_BeforePrint;
                }
                if (this.mReport.Group_Field[3].Trim() != string.Empty) {
                    this.GroupHeader4.BeforePrint += this.GroupHeader4_BeforePrint;
                }
                if (this.mReport.Group_Field[4].Trim() != string.Empty) {
                    this.GroupHeader5.BeforePrint += this.GroupHeader5_BeforePrint;
                }
                //this.Detail.AfterPrint += Detail_AfterPrint;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (this.components != null) {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.tblDetail = new DevExpress.XtraReports.UI.XRTable();
            this.rowDetail = new DevExpress.XtraReports.UI.XRTableRow();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.tblHeader = new DevExpress.XtraReports.UI.XRTable();
            this.rowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblGroup1 = new DevExpress.XtraReports.UI.XRTable();
            this.rowGroup1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblGroup2 = new DevExpress.XtraReports.UI.XRTable();
            this.rowGroup2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblGroup3 = new DevExpress.XtraReports.UI.XRTable();
            this.rowGroup3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblGroup4 = new DevExpress.XtraReports.UI.XRTable();
            this.rowGroup4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupHeader5 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.tblGroup5 = new DevExpress.XtraReports.UI.XRTable();
            this.rowGroup5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.tblFooter = new DevExpress.XtraReports.UI.XRTable();
            this.rowFooter = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupHeader0 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize) (this.tblDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblDetail});
            this.Detail.Height = 15;
            this.Detail.Name = "Detail";
            // 
            // tblDetail
            // 
            this.tblDetail.Location = new System.Drawing.Point(0, 0);
            this.tblDetail.Name = "tblDetail";
            this.tblDetail.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowDetail});
            this.tblDetail.Size = new System.Drawing.Size(642, 15);
            //this.tblDetail.Draw.b += new DevExpress.XtraReports.UI.DrawEventHandler(this.Table_Draw);
            this.tblDetail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.DetailBeforePrint);
            // 
            // rowDetail
            // 
            this.rowDetail.Name = "rowDetail";
            this.rowDetail.Size = new System.Drawing.Size(642, 15);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblHeader});
            this.PageHeader.Height = 30;
            this.PageHeader.Name = "PageHeader";
            // 
            // tblHeader
            // 
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowHeader});
            this.tblHeader.Size = new System.Drawing.Size(100, 5);
            // 
            // rowHeader
            // 
            this.rowHeader.Name = "rowHeader";
            this.rowHeader.Size = new System.Drawing.Size(100, 5);
            // 
            // PageFooter
            // 
            this.PageFooter.Height = 20;
            this.PageFooter.Name = "PageFooter";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Height = 10;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblGroup1});
            this.GroupHeader1.Height = 20;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // tblGroup1
            // 
            this.tblGroup1.Location = new System.Drawing.Point(0, 0);
            this.tblGroup1.Name = "tblGroup1";
            this.tblGroup1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowGroup1});
            this.tblGroup1.Size = new System.Drawing.Size(100, 25);
            // 
            // rowGroup1
            // 
            this.rowGroup1.Name = "rowGroup1";
            this.rowGroup1.Size = new System.Drawing.Size(100, 25);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblGroup2});
            this.GroupHeader2.Height = 33;
            this.GroupHeader2.Level = 2;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // tblGroup2
            // 
            this.tblGroup2.Location = new System.Drawing.Point(75, 8);
            this.tblGroup2.Name = "tblGroup2";
            this.tblGroup2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowGroup2});
            this.tblGroup2.Size = new System.Drawing.Size(100, 25);
            // 
            // rowGroup2
            // 
            this.rowGroup2.Name = "rowGroup2";
            this.rowGroup2.Size = new System.Drawing.Size(100, 25);
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblGroup3});
            this.GroupHeader3.Height = 33;
            this.GroupHeader3.Level = 3;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // tblGroup3
            // 
            this.tblGroup3.Location = new System.Drawing.Point(50, 8);
            this.tblGroup3.Name = "tblGroup3";
            this.tblGroup3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowGroup3});
            this.tblGroup3.Size = new System.Drawing.Size(100, 25);
            // 
            // rowGroup3
            // 
            this.rowGroup3.Name = "rowGroup3";
            this.rowGroup3.Size = new System.Drawing.Size(100, 25);
            // 
            // GroupHeader4
            // 
            this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblGroup4});
            this.GroupHeader4.Height = 42;
            this.GroupHeader4.Level = 4;
            this.GroupHeader4.Name = "GroupHeader4";
            // 
            // tblGroup4
            // 
            this.tblGroup4.Location = new System.Drawing.Point(58, 17);
            this.tblGroup4.Name = "tblGroup4";
            this.tblGroup4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowGroup4});
            this.tblGroup4.Size = new System.Drawing.Size(100, 25);
            // 
            // rowGroup4
            // 
            this.rowGroup4.Name = "rowGroup4";
            this.rowGroup4.Size = new System.Drawing.Size(100, 25);
            // 
            // GroupHeader5
            // 
            this.GroupHeader5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblGroup5});
            this.GroupHeader5.Height = 42;
            this.GroupHeader5.Level = 5;
            this.GroupHeader5.Name = "GroupHeader5";
            // 
            // tblGroup5
            // 
            this.tblGroup5.Location = new System.Drawing.Point(33, 17);
            this.tblGroup5.Name = "tblGroup5";
            this.tblGroup5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowGroup5});
            this.tblGroup5.Size = new System.Drawing.Size(100, 25);
            // 
            // rowGroup5
            // 
            this.rowGroup5.Name = "rowGroup5";
            this.rowGroup5.Size = new System.Drawing.Size(100, 25);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {this.tblFooter});
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Height = 20;
            // 
            // tblFooter
            // 
            this.tblFooter.Location = new System.Drawing.Point(0, 0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {this.rowFooter});
            this.tblFooter.Size = new System.Drawing.Size(100, 20);
            // 
            // rowFooter
            // 
            this.rowFooter.Name = "rowFooter";
            this.rowFooter.Size = new System.Drawing.Size(100, 20);
            // 
            // GroupHeader0
            // 			
            this.GroupHeader0.Name = "GroupHeader0";
            // 
            // FTSReportViewer
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.Detail, this.PageHeader, this.PageFooter, this.ReportHeader, this.GroupHeader1, this.GroupHeader2, this.GroupHeader3, this.GroupHeader4,
                this.GroupHeader5, this.ReportFooter, this.GroupHeader0
            });
            ((System.ComponentModel.ISupportInitialize) (this.tblDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tblFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();
        }

        #endregion

        private void SetData() {
            //if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("VISIBLE") >= 0 &&
            //    this.mReport.Formula) {
            //    this.mDataSetTmp = new DataSet();
            //    this.mDataSetTmp.Tables.Add(this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy());
            //    this.mDataSetTmp.AcceptChanges();
            //}
            //this.DeleteInvisible();
            //this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].AcceptChanges();
            //DataView dv = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].DefaultView;
            //if (this.mReport.Print_Condition != string.Empty)
            //{
            //    dv = new DataView(this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp], this.mReport.Print_Condition, string.Empty, DataViewRowState.CurrentRows);
            //}
            ///*
            //try
            //{
            //    if (this.mReport.Print_Condition != string.Empty)
            //    {
            //        DataTable dt = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy();
            //        DataView dv = new DataView(dt, this.mReport.Print_Condition, string.Empty, DataViewRowState.CurrentRows);
            //        this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Clear();
            //        foreach (DataRowView drv in dv)
            //        {
            //            this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].ImportRow(drv.Row);
            //        }
            //        dv = null;
            //        Functions.ClearTable(dt);
            //    }
            //    this.DataSource = this.mReport.DataSet;
            //    this.DataMember = this.mReport.Template_Table_Tmp;
            //}
            //catch (Exception)
            //{
            //    throw (new FTSException("MSG_INVALID_CONDITION"));
            //}
            //*/
            //this.DataSource = dv;
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("VISIBLE") >= 0 && this.mReport.Formula) {
                this.mDataSetTmp = new DataSet();
                this.mDataSetTmp.Tables.Add(this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy());
                this.mDataSetTmp.AcceptChanges();
            }
            this.DeleteInvisible();
            this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].AcceptChanges();
            string sort = string.Empty;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Sort_Field[i] != string.Empty && this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf(this.mReport.Sort_Field[i]) >= 0) {
                    sort += this.mReport.Sort_Field[i].Trim() + " " + this.mReport.Sort_Direction[i] + ",";
                }
            }
            if (sort != string.Empty) {
                sort = sort.Substring(0, sort.Length - 1);
            }

            try {
                if (this.mReport.Print_Condition_Extra != string.Empty && this.mReport.Print_Condition_Extra.Trim() != "1=1") {
                    DataView dv = new DataView(this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp], this.mReport.Print_Condition_Extra, sort, DataViewRowState.CurrentRows);
                    DataTable dt = dv.ToTable();
                    this.mReport.DataSet.Tables.Remove(this.mReport.Template_Table_Tmp);
                    this.mReport.DataSet.Tables.Add(dt);
                    //DataTable dt = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy();
                    //DataView dv = new DataView(dt, this.mReport.Print_Condition_Extra, sort, DataViewRowState.CurrentRows);
                    //this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Clear();
                    //foreach (DataRowView drv in dv) {
                    //    this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].ImportRow(drv.Row);
                    //}
                    //dv = null;
                    //Functions.ClearTable(dt);
                } else {
                    if (sort != string.Empty) {
                        DataView dv = new DataView(this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp], "1=1", sort, DataViewRowState.CurrentRows);
                        DataTable dt = dv.ToTable();
                        this.mReport.DataSet.Tables.Remove(this.mReport.Template_Table_Tmp);
                        this.mReport.DataSet.Tables.Add(dt);
                    }
                }
            } catch (Exception) {
                FTSMessageBox.ShowErrorMessage(this.mFTSMain.MsgManager.GetMessage("MSG_INVALID_CONDITION"));
            }
            this.DataSource = this.mReport.DataSet;
            this.DataMember = this.mReport.Template_Table_Tmp;
        }

        private void GetRepeatCount() {
            this.mRepeatCount = 1;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Table[i] != string.Empty) {
                    this.mRepeatCount = 2;
                    return;
                }
            }
        }

        private void SetPage() {
            //A4: width 1169, Height: 827
            //A4: width 1654, Height 1169
            this.Landscape = false;
            if (this.mReport.Paper_Size == "A4") {
                if (this.mReport.Page_Orient == "Landscape") {
                    this.Landscape = true;
                    this.mTotalPageWidth = 1169 - this.mReport.Page_Margin_Left - this.mReport.Page_Margin_Right;
                } else {
                    this.mTotalPageWidth = 827 - this.mReport.Page_Margin_Left - this.mReport.Page_Margin_Right;
                }
                this.PaperKind = PaperKind.A4;
            } else {
                this.PaperKind = PaperKind.A3;
                if (this.mReport.Page_Orient == "Landscape") {
                    this.Landscape = true;
                    this.mTotalPageWidth = 1654 - this.mReport.Page_Margin_Left - this.mReport.Page_Margin_Right;
                } else {
                    this.mTotalPageWidth = 1169 - this.mReport.Page_Margin_Left - this.mReport.Page_Margin_Right;
                }
            }
            this.Margins.Bottom = this.mReport.Page_Margin_Bottom;
            this.Margins.Left = this.mReport.Page_Margin_Left;
            this.Margins.Right = this.mReport.Page_Margin_Right;
            this.Margins.Top = this.mReport.Page_Margin_Top;
            this.tblDetail.Width = this.mTotalPageWidth;
            this.tblHeader.Width = this.mTotalPageWidth;
            this.tblGroup1.Width = this.mTotalPageWidth;
            this.tblGroup2.Width = this.mTotalPageWidth;
            this.tblGroup3.Width = this.mTotalPageWidth;
            this.tblGroup4.Width = this.mTotalPageWidth;
            this.tblGroup5.Width = this.mTotalPageWidth;
            this.rowFooter.Width = this.mTotalPageWidth;
        }

        private void SetPart() {
            if (this.mReport.Num_File_Report == 1) {
                foreach (DataRow row in this.mReport.sys_reportfield.DataTable.Rows) {
                    row["part"] = 1;
                }
                //this.mReport.sys_reportfield.DataTable.AcceptChanges();
            } else {
                DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                int totalwidth = 0;
                foreach (DataRowView drv in dv) {
                    totalwidth += (int) drv["field_width"];
                }
                if (this.mReport.Repeat_Column && dv.Count > 1) {
                    for (int i = 0; i < this.mReport.Num_File_Report - 1; i++) {
                        totalwidth += (int) dv[0]["field_width"];
                        if (this.mRepeatCount > 1) {
                            totalwidth += (int) dv[1]["field_width"];
                        }
                    }
                }
                int tpagewidth = this.mTotalPageWidth*this.mReport.Num_File_Report;
                int start = 0;
                int oldpart = 1;
                int newpart = 1;
                int i1 = 0;
                int first = 0, second = 0;
                if (Convert.ToBoolean(this.mFTSMain.SystemVars.GetSystemVars("AUTO_SET_PART_REPORT"))) {
                    foreach (DataRowView drv in dv) {
                        int fieldwidth = (int) drv["field_width"];
                        start += Convert.ToInt32(((decimal) tpagewidth/(decimal) totalwidth)*(decimal) fieldwidth);
                        if (i1 == 0) {
                            first = fieldwidth;
                        }
                        if (i1 == 1) {
                            second = fieldwidth;
                        }
                        newpart = Convert.ToInt32(Math.Floor((decimal) (start/this.mTotalPageWidth))) + 1;
                        if (this.mReport.Repeat_Column && dv.Count > 1) {
                            if (newpart != oldpart) {
                                //sang trang moi
                                start += Convert.ToInt32(((decimal) tpagewidth/(decimal) totalwidth)*(decimal) first);
                                if (this.mRepeatCount > 1) {
                                    start += Convert.ToInt32(((decimal) tpagewidth/(decimal) totalwidth)*(decimal) second);
                                }
                                newpart = Convert.ToInt32(Math.Floor((decimal) (start/this.mTotalPageWidth))) + 1;
                            }
                        }
                        if (newpart > this.mReport.Num_File_Report) {
                            newpart = this.mReport.Num_File_Report;
                        }
                        drv.Row["part"] = newpart;
                        oldpart = newpart;
                        i1++;
                    }
                }

                //this.mReport.sys_reportfield.DataTable.AcceptChanges();
            }
        }

        private void CreateStyle() {
            if ((bool) this.mFTSMain.SystemVars.GetSystemVars("USE_DOTTED_REPORT_LINE")) {
                this.CreateStyleBase1(this.mFTSMain.DbMain, false, this.mReport.ShowHeader0());
            } else {
                this.CreateStyleBase(this.mFTSMain.DbMain, false, this.mReport.ShowHeader0());
            }
        }

        public override void RefreshContent(ColumnsSizeChangeEventArgs e) {
            this.mTotalPageWidth = 0;
            this.orderdetail = 1;
            this.group1detail = 1;
            this.group2detail = 1;
            this.group3detail = 1;
            this.group4detail = 1;
            this.group5detail = 1;
            foreach (ColumnsSizeInfo col in e.Columns) {
                this.mTotalPageWidth += col.Width;
            }
            if (e.Columns.Count > 0) {
                XRTable table = this.Detail.Controls[0] as XRTable;
                XRTableRow row = table.Rows[0];
                table.BeginInit();
                row.SuspendLayout();
                int left = 0;
                foreach (ColumnsSizeInfo col in e.Columns) {
                    if (col.Name.ToUpper() == "ORDERDETAIL") {
                        this.mReport.Order_Width = col.Width;
                    }
                    col.Left = ((XRTableCell) row.Cells[col.Name]).Left = left;
                    ((XRTableCell) row.Cells[col.Name]).Width = col.Width;
                    left += col.Width;
                }
                table.Width = left;
                row.Width = left;
                row.ResumeLayout();
                table.EndInit();
            }
            this.RefreshGroupField();
            this.RefreshMoveGroupField();
            this.RefreshTotal();
            this.RefreshCustomFieldsBefore();
            this.RefreshReportHeader();
            this.RefreshBalanceLuykeHeader();
            this.RefreshBalanceLuykeFooter();
            this.RefreshFooterText();
            this.RefreshFooterNameText();
            this.RefreshPageFooter();
            this.RefreshHeader();
            this.RefreshCustomFieldsAfter();
            this.CreateDocument();
            //if (this.mReport.FTSMain.DEMO){
            //    this.PrintingSystem.Watermark.Text = "FTS Accounting, Sản phẩm chưa đăng ký bản quyền!";
            //}
        }

        private void CreateDetailField() {
            if (this.mReport.Print_Style == "DETAIL") {
                this.tblDetail.StyleName = "detailTableStyle";
                this.tblDetail.Height = this.mGroupHeight;
            }
            XRTableCell cellorder = null;
            int totalwidth = 0;
            if (this.mReport.Show_Order) {
                cellorder = new XRTableCell();
                this.rowDetail.Cells.Add(cellorder);
                cellorder.Name = "ORDERDETAIL";
                cellorder.Text = this.orderdetail.ToString();
                if (this.mReport.Print_Style == "DETAIL") {
                    this.FormatStringField(cellorder);
                    cellorder.BeforePrint += new PrintEventHandler(this.cellorder_BeforePrint);
                }
            }
            string field_type = string.Empty;
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    field_type = dv1[i]["field_type"].ToString().Trim();

                    XRTableCell cell = new XRTableCell();
                    this.rowDetail.Cells.Add(cell);
                    if (field_type == "IMAGE") {
                        cell.Name = fieldname;
                        XRPictureBox picimage = new XRPictureBox();
                        picimage.Sizing = ImageSizeMode.AutoSize;
                        picimage.Borders = BorderSide.None;
                        picimage.DataBindings.AddRange(new XRBinding[] {new XRBinding("Image", this.mReport.DataSet, fieldname, string.Empty)});
                        cell.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {picimage});
                        cell.Multiline = true;
                    } else {
                        cell.Name = fieldname;
                        cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    }
                    if (this.mReport.Print_Style == "DETAIL") {
                        cell.BeforePrint += new PrintEventHandler(this.cell_BeforePrint2);
                        cell.PreviewDoubleClick += new PreviewMouseEventHandler(this.DetailCellPreviewDoubleClick);
                    }
                    totalwidth += (int) dv1[i]["field_width"];
                }
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString().Trim();
                field_type = drv["field_type"].ToString().Trim();
                XRTableCell cell = new XRTableCell();
                this.rowDetail.Cells.Add(cell);
                if (field_type == "IMAGE") {
                    cell.Name = fieldname;
                    XRPictureBox picimage = new XRPictureBox();
                    picimage.Borders = BorderSide.None;
                    picimage.Sizing = ImageSizeMode.AutoSize;
                    picimage.DataBindings.AddRange(new XRBinding[] {new XRBinding("Image", this.mReport.DataSet, fieldname, string.Empty)});
                    cell.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {picimage});
                    cell.Multiline = true;
                } else {
                    cell.Name = fieldname;
                    if ((Int16) drv["hide_detail"] != 1) {
                        cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    }
                }

                totalwidth += (int) drv["field_width"];
                if (this.mReport.Print_Style == "DETAIL") {
                    if (drv["field_type"].ToString() == "NUMBER") {
                        cell.BeforePrint += new PrintEventHandler(this.cell_BeforePrint1);
                    } else {
                        cell.BeforePrint += new PrintEventHandler(this.cell_BeforePrint2);
                    }
                    cell.PreviewDoubleClick += new PreviewMouseEventHandler(this.DetailCellPreviewDoubleClick);
                }
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                XRTableCell cell = new XRTableCell();
                this.rowDetail.Cells.Add(cell);
                cell.Name = "FONT_BOLD";
                cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, "FONT_BOLD", string.Empty)});
                cell.Visible = false;
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                XRTableCell cell = new XRTableCell();
                this.rowDetail.Cells.Add(cell);
                cell.Name = "FONT_UNDERLINE";
                cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, "FONT_UNDERLINE", string.Empty)});
                cell.Visible = false;
                this.mTotalPageWidth -= 9;
            }
            int startleft = 0;
            ((ISupportInitialize) (this.tblDetail)).BeginInit();
            if (this.mReport.Show_Order) {
                cellorder.Left = startleft;
                cellorder.Width = this.mOrderWidth;
                startleft += this.mOrderWidth;
            }
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    XRTableCell cell = (XRTableCell) this.rowDetail.Cells[fieldname];
                    cell.Left = startleft;
                    int fw = (int) dv1[i]["field_width"];
                    int fw1 = 0;
                    if (this.mReport.Show_Order) {
                        fw1 = Convert.ToInt32(((((float) this.mTotalPageWidth - this.mOrderWidth))/(float) totalwidth)*(float) fw);
                    } else {
                        fw1 = Convert.ToInt32((((float) this.mTotalPageWidth)/(float) totalwidth)*(float) fw);
                    }
                    cell.Width = fw1;
                    startleft += fw1;
                }
            }
            
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                XRTableCell cell = (XRTableCell) this.rowDetail.Cells["FONT_BOLD"];
                cell.Visible = false;
                cell.Left = this.mTotalPageWidth;
                cell.Width = 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                XRTableCell cell = (XRTableCell) this.rowDetail.Cells["FONT_UNDERLINE"];
                cell.Visible = false;
                cell.Left = this.mTotalPageWidth;
                cell.Width = 9;
            }

            for (int i = 0; i < dv.Count; i++) {
                DataRowView drv = dv[i];
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = (XRTableCell) this.rowDetail.Cells[fieldname];
                cell.Left = startleft;
                int fw = (int) drv["field_width"];
                int fw1 = 0;
                if (this.mReport.Show_Order) {
                    fw1 = Convert.ToInt32((((float) (this.mTotalPageWidth - this.mOrderWidth))/(float) totalwidth)*(float) fw);
                } else {
                    fw1 = Convert.ToInt32((((float) this.mTotalPageWidth)/(float) totalwidth)*(float) fw);
                }
                if (i == dv.Count - 1) {
                    fw1 = this.mTotalPageWidth - startleft;
                }
                cell.Width = fw1;
                startleft += fw1;
            }
            ((ISupportInitialize) (this.tblDetail)).EndInit();
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }



        private void SetFieldFormat() {
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int k = 0; k < this.mRepeatCount; k++) {
                    string fieldname = dv1[k]["field_id"].ToString().Trim();
                    XRTableCell cell = (XRTableCell) this.rowDetail.Cells[fieldname];
                    switch (dv1[k]["field_type"].ToString().Trim()) {
                        case "NUMBER":
                            cell.TextAlignment = TextAlignment.MiddleRight;
                            this.FormatNumberField(cell, (int) dv1[k]["decimal_digit"]);
                            if ((Int16) dv1[k]["is_sum"] == 1 && dv1[k]["field_type"].ToString() == "NUMBER") {
                                for (int i = 0; i < 5; i++) {
                                    if (this.mReport.Group_Field[i] != string.Empty) {
                                        XRTableRow rowGroup = null;
                                        switch (i) {
                                            case 0:
                                                rowGroup = this.rowGroup5;
                                                break;
                                            case 1:
                                                rowGroup = this.rowGroup4;
                                                break;
                                            case 2:
                                                rowGroup = this.rowGroup3;
                                                break;
                                            case 3:
                                                rowGroup = this.rowGroup2;
                                                break;
                                            case 4:
                                                rowGroup = this.rowGroup1;
                                                break;
                                            default:
                                                break;
                                        }
                                        cell = (XRTableCell) rowGroup.Cells["GROUP" + Convert.ToString(5 - i) + fieldname];
                                        this.FormatNumberField(cell, (int) dv1[k]["decimal_digit"]);
                                    }
                                }
                                cell = (XRTableCell) this.rowFooter.Cells["TOTAL" + fieldname];
                                this.FormatNumberField(cell, (int) dv1[k]["decimal_digit"]);
                            }
                            break;
                        case "DATE":
                            this.FormatDateField(cell);
                            break;
                        case "IMAGE":
                            this.FormatImageField(cell);
                            break;
                        default:
                            this.FormatStringField(cell);
                            break;
                    }
                }
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = (XRTableCell) this.rowDetail.Cells[fieldname];
                switch (drv["field_type"].ToString().Trim()) {
                    case "NUMBER":
                        cell.TextAlignment = TextAlignment.MiddleRight;
                        this.FormatNumberField(cell, (int) drv["decimal_digit"]);
                        if ((Int16) drv["is_sum"] == 1 && drv["field_type"].ToString() == "NUMBER") {
                            for (int i = 0; i < 5; i++) {
                                if (this.mReport.Group_Field[i] != string.Empty) {
                                    XRTableRow rowGroup = null;
                                    switch (i) {
                                        case 0:
                                            rowGroup = this.rowGroup5;
                                            break;
                                        case 1:
                                            rowGroup = this.rowGroup4;
                                            break;
                                        case 2:
                                            rowGroup = this.rowGroup3;
                                            break;
                                        case 3:
                                            rowGroup = this.rowGroup2;
                                            break;
                                        case 4:
                                            rowGroup = this.rowGroup1;
                                            break;
                                        default:
                                            break;
                                    }
                                    cell = (XRTableCell) rowGroup.Cells["GROUP" + Convert.ToString(5 - i) + fieldname];
                                    this.FormatNumberField(cell, (int) drv["decimal_digit"]);
                                }
                            }
                            cell = (XRTableCell) this.rowFooter.Cells["TOTAL" + fieldname];
                            this.FormatNumberField(cell, (int) drv["decimal_digit"]);
                        }
                        break;
                    case "DATE":
                        this.FormatDateField(cell);
                        break;
                    default:
                        this.FormatStringField(cell);
                        break;
                }
            }
        }

        private void FormatDateField(XRTableCell cell) {
            cell.TextAlignment = TextAlignment.MiddleLeft;
            if (cell.DataBindings.Count > 0) {
                cell.DataBindings[0].FormatString = "{0:dd/MM/yyyy}";
            }
            cell.Padding = new PaddingInfo(2, 1, 1, 1);
        }

        private void FormatStringField(XRTableCell cell) {
            cell.TextAlignment = TextAlignment.MiddleLeft;
            cell.Padding = new PaddingInfo(2, 1, 1, 1);
            cell.Multiline = true;
        }

        private void FormatImageField(XRTableCell cell) {
            cell.TextAlignment = TextAlignment.MiddleLeft;
            cell.Padding = new PaddingInfo(2, 1, 1, 1);
            cell.Multiline = true;
        }

        private void FormatNumberField(XRTableCell cell, int dec) {
            if (cell == null) {
                return;
            }
            cell.TextAlignment = TextAlignment.MiddleRight;
            if (dec == 0) {
                if (this.mReport.ShowZero) {
                    if (cell.DataBindings.Count > 0) {
                        cell.DataBindings[0].FormatString = "{0:n0}";
                    }
                    if (cell.Summary != null) {
                        cell.Summary.FormatString = "{0:#,#}";
                    }
                } else {
                    if (cell.DataBindings.Count > 0) {
                        cell.DataBindings[0].FormatString = "{0:#,#}";
                    }
                    if (cell.Summary != null) {
                        cell.Summary.FormatString = "{0:#,#}";
                    }
                }
            } else {
                string zeros1 = dec.ToString().Trim();
                if (cell.DataBindings.Count > 0) {
                    cell.DataBindings[0].FormatString = "{0:n" + zeros1 + "}";
                }
                if (cell.Summary != null) {
                    cell.Summary.FormatString = "{0:n" + zeros1 + "}";
                }
            }
            cell.Padding = new PaddingInfo(1, 2, 1, 1);
        }

        private void SetFont() {
            this.StyleSheet["detailTableStyle"].ForeColor = Color.FromName(this.mReport.Detail_Font_Color);
            switch (this.mReport.Detail_Font_Style) {
                case "Bold":
                    this.StyleSheet["detailTableStyle"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["detailTableStyle"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    this.StyleSheet["detailTableStyle"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["detailTableStyle"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Regular);
                    break;
            }
            this.StyleSheet["detailTableStyleBold"].ForeColor = Color.FromName(this.mReport.Detail_Font_Color);
            this.StyleSheet["detailTableStyleBold"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Bold);
            this.StyleSheet["detailTableStyleBoldUnderline"].ForeColor = Color.FromName(this.mReport.Detail_Font_Color);
            this.StyleSheet["detailTableStyleBoldUnderline"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size,
                FontStyle.Bold | FontStyle.Underline);
            this.StyleSheet["detailTableStyleUnderline"].ForeColor = Color.FromName(this.mReport.Detail_Font_Color);
            this.StyleSheet["detailTableStyleUnderline"].Font = new Font(this.mReport.Detail_Font_Name, this.mReport.Detail_Font_Size, FontStyle.Underline);
            this.StyleSheet["groupTableStyle"].ForeColor = Color.FromName(this.mReport.Group_Font_Color);
            switch (this.mReport.Group_Font_Style) {
                case "Bold":
                    this.StyleSheet["groupTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["groupTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    this.StyleSheet["groupTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["groupTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Regular);
                    break;
            }
            this.StyleSheet["totalTableStyle"].ForeColor = Color.FromName(this.mReport.Group_Font_Color);
            switch (this.mReport.Group_Font_Style) {
                case "Bold":
                    this.StyleSheet["totalTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["totalTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    this.StyleSheet["totalTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["totalTableStyle"].Font = new Font(this.mReport.Group_Font_Name, this.mReport.Group_Font_Size, FontStyle.Regular);
                    break;
            }
            this.StyleSheet["footerTableStyle"].ForeColor = Color.FromName(this.mReport.Footer_Font_Color);
            switch (this.mReport.Footer_Font_Style) {
                case "Bold":
                    this.StyleSheet["footerTableStyle"].Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["footerTableStyle"].Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    this.StyleSheet["footerTableStyle"].Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["footerTableStyle"].Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Regular);
                    break;
            }
            this.StyleSheet["headerTableStyle"].ForeColor = Color.FromName(this.mReport.Header_Font_Color);
            switch (this.mReport.Header_Font_Style) {
                case "Bold":
                    this.StyleSheet["headerTableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["headerTableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size,
                        FontStyle.Bold | FontStyle.Italic);
                    break;
                case "Italic":
                    this.StyleSheet["headerTableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["headerTableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Regular);
                    break;
            }
            this.StyleSheet["header0TableStyle"].ForeColor = Color.FromName(this.mReport.Header_Font_Color);
            switch (this.mReport.Header_Font_Style) {
                case "Bold":
                    this.StyleSheet["header0TableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    this.StyleSheet["header0TableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size,
                        FontStyle.Bold | FontStyle.Italic);
                    break;
                case "Italic":
                    this.StyleSheet["header0TableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Italic);
                    break;
                default:
                    this.StyleSheet["header0TableStyle"].Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Regular);
                    break;
            }
            if (this.mReport.Print_Style != "DETAIL") {
                this.tblGroup1.StyleName = "detailTableStyle";
            }
        }

        private void SetSubtitleFont(XRLabel lbl) {
            lbl.ForeColor = Color.FromName(this.mReport.Subtitle_Font_Color);
            switch (this.mReport.Subtitle_Font_Style) {
                case "Bold":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Regular);
                    break;
            }
        }

        private void SetFooterFont(XRLabel lbl) {
            lbl.ForeColor = Color.FromName(this.mReport.Footer_Font_Color);
            switch (this.mReport.Footer_Font_Style) {
                case "Bold":
                    lbl.Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    lbl.Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    lbl.Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(this.mReport.Footer_Font_Name, this.mReport.Footer_Font_Size, FontStyle.Regular);
                    break;
            }
        }

        private void SetCommentFont(XRLabel lbl) {
            lbl.ForeColor = Color.FromName(this.mReport.Comment_Font_Color);
            switch (this.mReport.Comment_Font_Style) {
                case "Bold":
                    lbl.Font = new Font(this.mReport.Comment_Font_Name, this.mReport.Comment_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    lbl.Font = new Font(this.mReport.Comment_Font_Name, this.mReport.Comment_Font_Size, FontStyle.Bold);
                    break;
                case "Italic":
                    lbl.Font = new Font(this.mReport.Comment_Font_Name, this.mReport.Comment_Font_Size, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(this.mReport.Comment_Font_Name, this.mReport.Comment_Font_Size, FontStyle.Regular);
                    break;
            }
        }

        private void SetHeaderFont(XRLabel lbl) {
            lbl.ForeColor = Color.FromName(this.mReport.Header_Font_Color);
            switch (this.mReport.Header_Font_Style) {
                case "Bold":
                    lbl.Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    lbl.Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Bold | FontStyle.Italic);
                    break;
                case "Italic":
                    lbl.Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(this.mReport.Header_Font_Name, this.mReport.Header_Font_Size, FontStyle.Regular);
                    break;
            }
        }

        private void SetSubtitleFont1(XRLabel lbl) {
            lbl.ForeColor = Color.FromName(this.mReport.Subtitle_Font_Color);
            switch (this.mReport.Subtitle_Font_Style) {
                case "Bold":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size - 1, FontStyle.Bold);
                    break;
                case "BoldItalic":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size - 1, FontStyle.Bold);
                    break;
                case "Italic":
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size - 1, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size - 1, FontStyle.Regular);
                    break;
            }
        }

        private void CreateGroupField() {
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Field[i].Trim() != string.Empty) {
                    this.CreateGroupField(i);
                } else {
                    switch (i) {
                        case 0:
                            this.GroupHeader5.Visible = false;
                            break;
                        case 1:
                            this.GroupHeader4.Visible = false;
                            break;
                        case 2:
                            this.GroupHeader3.Visible = false;
                            break;
                        case 3:
                            this.GroupHeader2.Visible = false;
                            break;
                        case 4:
                            this.GroupHeader1.Visible = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void RefreshGroupField() {
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Field[i].Trim() != string.Empty) {
                    this.RefreshGroupField(i);
                }
            }
        }

        private void RefreshGroupField(int group) {
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            GroupHeaderBand groupheader = null;
            switch (group) {
                case 0:
                    groupheader = this.GroupHeader5;
                    tblGroup = this.tblGroup5;
                    rowGroup = this.rowGroup5;
                    break;
                case 1:
                    groupheader = this.GroupHeader4;
                    tblGroup = this.tblGroup4;
                    rowGroup = this.rowGroup4;
                    break;
                case 2:
                    groupheader = this.GroupHeader3;
                    tblGroup = this.tblGroup3;
                    rowGroup = this.rowGroup3;
                    break;
                case 3:
                    groupheader = this.GroupHeader2;
                    tblGroup = this.tblGroup2;
                    rowGroup = this.rowGroup2;
                    break;
                case 4:
                    groupheader = this.GroupHeader1;
                    tblGroup = this.tblGroup1;
                    rowGroup = this.rowGroup1;
                    break;
                default:
                    break;
            }
            tblGroup.BeginInit();
            rowGroup.SuspendLayout();
            tblGroup.Width = rowGroup.Width = this.mTotalPageWidth;
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            if (group == 4) {
                firstsumfield = this.GetFirstSumFieldLastGroup();
            }
            string firstfield = this.GetFirstField();
            XRTableCell cellid = null;
            XRTableCell cellname = null;
            XRTableCell cellempty = null;
            XRTableCell cellorder = null;
            if (this.mReport.Show_Order) {
                cellorder = (XRTableCell) rowGroup.Cells["ORDERGROUP" + Convert.ToString(5 - group)];
            } else {
                cellorder = new XRTableCell();
            }
            if (firstsumfield != string.Empty) {
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    string fieldname = this.mReport.Group_Field[group];
                    if (!this.mShowGroupId && group == 4) {
                        cellid = new XRTableCell();
                    } else {
                        cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + fieldname];
                    }
                    cellname = (XRTableCell) rowGroup.Cells["GROUP_NAME" + Convert.ToString(group + 1)];
                } else {
                    if (firstfield != firstsumfield) {
                        cellempty = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + "EMPTY"];
                    }
                }
                int startleft = 0;
                if (this.mReport.Show_Order) {
                    XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                    cellorder.Left = cellorderdetail.Left;
                    cellorder.Width = cellorderdetail.Width;
                    startleft += cellorderdetail.Width;
                }
                XRTableCell firstsumcell1 = (XRTableCell) this.rowDetail.Cells[firstsumfield];
                XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    if (!this.mShowGroupId && group == 4) {
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        if (cellname != null) {
                            cellname.Left = startleft;
                            cellname.Width = firstsumcell1.Left - (startleft);
                            if (firstsumcell1.Left - (startleft) <= 5) {
                                rowGroup.Cells.Remove(cellname);
                            }
                        }
                    } else {
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        if (cellname != null) {
                            cellname.Left = startleft + firstcell1.Width;
                            cellname.Width = firstsumcell1.Left - (startleft + firstcell1.Width);
                            if (firstsumcell1.Left - (startleft + firstcell1.Width) <= 5) {
                                rowGroup.Cells.Remove(cellname);
                            }
                        }
                    }
                } else {
                    if (cellempty != null) {
                        cellempty.Left = startleft;
                        cellempty.Width = firstsumcell1.Left - (startleft);
                    }
                }
                bool doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        string fieldname2 = "GROUP" + Convert.ToString(5 - group) + drv["field_id"].ToString().Trim();
                        XRTableCell cell = (XRTableCell) rowGroup.Cells[fieldname2];
                        XRTableCell celldetail = (XRTableCell) this.rowDetail.Cells[fieldname1];
                        cell.Left = celldetail.Left;
                        cell.Width = celldetail.Width;
                    }
                }
            } else {
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    string fieldname = this.mReport.Group_Field[group];
                    if (!this.mShowGroupId && group == 4) {
                        cellid = new XRTableCell();
                    } else {
                        cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + fieldname];
                    }
                    cellname = (XRTableCell) rowGroup.Cells["GROUP_NAME" + Convert.ToString(group + 1)];
                    int startleft = 0;
                    if (this.mReport.Show_Order) {
                        XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                        cellorder.Left = cellorderdetail.Left;
                        cellorder.Width = cellorderdetail.Width;
                        startleft += cellorderdetail.Width;
                        XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        if (cellname != null) {
                            cellname.Left = startleft + firstcell1.Width;
                            cellname.Width = this.mTotalPageWidth - (startleft + firstcell1.Width);
                            if (this.mTotalPageWidth - (startleft + firstcell1.Width) <= 5) {
                                rowGroup.Cells.Remove(cellname);
                            }
                        }
                    } else {
                        XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                        if (!this.mShowGroupId && group == 4) {
                            cellid.Left = startleft;
                            cellid.Width = firstcell1.Width;
                            if (cellname != null) {
                                cellname.Left = startleft;
                                cellname.Width = this.mTotalPageWidth - (startleft);
                                if (this.mTotalPageWidth - (startleft) <= 5) {
                                    rowGroup.Cells.Remove(cellname);
                                }
                            }
                        } else {
                            cellid.Left = startleft;
                            cellid.Width = firstcell1.Width;
                            if (cellname != null) {
                                cellname.Left = startleft + firstcell1.Width;
                                cellname.Width = this.mTotalPageWidth - (startleft + firstcell1.Width);
                                if (this.mTotalPageWidth - (startleft + firstcell1.Width) <= 5) {
                                    rowGroup.Cells.Remove(cellname);
                                }
                            }
                        }
                    }
                } else {
                    cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + "EMPTY"];
                    int startleft = 0;
                    if (this.mReport.Show_Order) {
                        XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                        cellorder.Left = cellorderdetail.Left;
                        cellorder.Width = cellorderdetail.Width;
                        startleft += cellorderdetail.Width;
                        cellid.Left = startleft;
                        cellid.Width = this.mTotalPageWidth - cellorderdetail.Width;
                    } else {
                        cellid.Left = startleft;
                        cellid.Width = this.mTotalPageWidth;
                    }
                }
            }
            rowGroup.ResumeLayout();
            tblGroup.EndInit();
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private void CreateGroupField(int group) {
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            GroupHeaderBand groupheader = null;
            switch (group) {
                case 0:
                    groupheader = this.GroupHeader5;
                    tblGroup = this.tblGroup5;
                    rowGroup = this.rowGroup5;
                    break;
                case 1:
                    groupheader = this.GroupHeader4;
                    tblGroup = this.tblGroup4;
                    rowGroup = this.rowGroup4;
                    break;
                case 2:
                    groupheader = this.GroupHeader3;
                    tblGroup = this.tblGroup3;
                    rowGroup = this.rowGroup3;
                    break;
                case 3:
                    groupheader = this.GroupHeader2;
                    tblGroup = this.tblGroup2;
                    rowGroup = this.rowGroup2;
                    break;
                case 4:
                    groupheader = this.GroupHeader1;
                    tblGroup = this.tblGroup1;
                    rowGroup = this.rowGroup1;
                    break;
                default:
                    break;
            }
            tblGroup.StyleName = "groupTableStyle";
            tblGroup.Left = 0;
            tblGroup.Top = 0;
            tblGroup.Width = this.mTotalPageWidth;
            tblGroup.Height = this.mGroupHeight;
            groupheader.Height = this.mGroupHeight;
            groupheader.GroupFields.Add(new GroupField(this.mReport.Group_Field[group], XRColumnSortOrder.Ascending));
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            if (group == 4) {
                firstsumfield = this.GetFirstSumFieldLastGroup();
            }
            string firstfield = this.GetFirstField();
            XRTableCell cellid = null;
            XRTableCell cellname = null;
            XRTableCell cellempty = null;
            XRTableCell cellorder = null;
            if (this.mReport.Show_Order) {
                cellorder = new XRTableCell();
                rowGroup.Cells.Add(cellorder);
                cellorder.Name = "ORDERGROUP" + Convert.ToString(5 - group);
                cellorder.Text = this.orderdetail.ToString();
                this.FormatStringField(cellorder);
                cellorder.BeforePrint += new PrintEventHandler(this.cellorder_BeforePrint);
            }
            if (firstsumfield != string.Empty) {
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    string fieldname = this.mReport.Group_Field[group];
                    cellid = new XRTableCell();
                    if (!this.mShowGroupId && group == 4) {} else {
                        rowGroup.Cells.Add(cellid);
                    }
                    cellid.Name = "GROUP_ID" + Convert.ToString(5 - group) + fieldname;
                    cellid.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    this.FormatCellID(cellid, fieldname);
                    fieldname = "GROUP_NAME" + Convert.ToString(group + 1);
                    cellname = new XRTableCell();
                    rowGroup.Cells.Add(cellname);
                    cellname.Name = fieldname;
                    cellname.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});

                    this.FormatStringField(cellname);
                } else {
                    if (firstfield != firstsumfield) {
                        cellempty = new XRTableCell();
                        rowGroup.Cells.Add(cellempty);
                        cellempty.Name = "GROUP_ID" + Convert.ToString(5 - group) + "EMPTY";
                    }
                }
                bool doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        XRTableCell cell = new XRTableCell();
                        rowGroup.Cells.Add(cell);
                        cell.Name = "GROUP" + Convert.ToString(5 - group) + fieldname1;
                        if ((Int16)drv["is_sum"] == 1 && drv["field_type"].ToString() == "NUMBER") {
                            cell.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.mReport.DataSet, fieldname1, string.Empty) });
                            XRSummary summary = new XRSummary();
                            summary.Running = SummaryRunning.Group;
                            if (this.mReport.Print_Style == "SUMMARY" && group == 4) {
                                cell.Summary = summary;
                            } else {
                                summary.Func = SummaryFunc.Custom;
                                cell.Summary = summary;
                                cell.SummaryReset += new EventHandler(this.cell_SummaryReset);
                                cell.SummaryRowChanged += new EventHandler(this.cell_SummaryRowChanged);
                                cell.SummaryGetResult += new SummaryGetResultHandler(this.cell_SummaryGetResult);
                            }
                            cell.SummaryCalculated += new TextFormatEventHandler(this.cell_BeforePrint);
                            cell.PreviewDoubleClick += new PreviewMouseEventHandler(this.DetailCellPreviewDoubleClick1);
                        } else {
                            if (drv["field_type"].ToString() == "NUMBER") {
                                cell.BeforePrint += new PrintEventHandler(this.cell_BeforePrint1);
                            }
                        }
                    }
                }
                int startleft = 0;
                ((ISupportInitialize) (tblGroup)).BeginInit();
                if (this.mReport.Show_Order) {
                    XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                    cellorder.Left = cellorderdetail.Left;
                    cellorder.Width = cellorderdetail.Width;
                    startleft += cellorderdetail.Width;
                }
                XRTableCell firstsumcell1 = (XRTableCell) this.rowDetail.Cells[firstsumfield];
                XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                if (firstcell1 == null) {
                    return;
                }
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    if (!this.mShowGroupId && group == 4) {
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        cellname.Left = startleft;
                        cellname.Width = firstsumcell1.Left - (startleft);
                        if (firstsumcell1.Left - (startleft) <= 5) {
                            rowGroup.Cells.Remove(cellname);
                        }
                    } else {
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        cellname.Left = startleft + firstcell1.Width;
                        cellname.Width = firstsumcell1.Left - (startleft + firstcell1.Width);
                        if (firstsumcell1.Left - (startleft + firstcell1.Width) <= 5) {
                            rowGroup.Cells.Remove(cellname);
                        }
                    }
                } else {
                    if (cellempty != null) {
                        cellempty.Left = startleft;
                        cellempty.Width = firstsumcell1.Left - (startleft);
                    }
                }
                doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        string fieldname2 = "GROUP" + Convert.ToString(5 - group) + drv["field_id"].ToString().Trim();
                        XRTableCell cell = (XRTableCell)rowGroup.Cells[fieldname2];
                        XRTableCell celldetail = (XRTableCell)this.rowDetail.Cells[fieldname1];
                        cell.Left = celldetail.Left;
                        cell.Width = celldetail.Width;
                    }
                }
                ((ISupportInitialize) (tblGroup)).EndInit();
            } else {
                if (this.mReport.Part == 1 || this.mReport.Repeat_Column) {
                    string fieldname = this.mReport.Group_Field[group];
                    cellid = new XRTableCell();
                    if (!this.mShowGroupId && group == 4) {} else {
                        rowGroup.Cells.Add(cellid);
                    }
                    cellid.Name = "GROUP_ID" + Convert.ToString(5 - group) + fieldname;
                    cellid.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    this.FormatCellID(cellid, fieldname);
                    fieldname = "GROUP_NAME" + Convert.ToString(group + 1);
                    cellname = new XRTableCell();
                    rowGroup.Cells.Add(cellname);
                    cellname.Name = fieldname;
                    cellname.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    this.FormatStringField(cellname);
                    int startleft = 0;
                    ((ISupportInitialize) (tblGroup)).BeginInit();
                    if (this.mReport.Show_Order) {
                        XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                        cellorder.Left = cellorderdetail.Left;
                        cellorder.Width = cellorderdetail.Width;
                        startleft += cellorderdetail.Width;
                        XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                        if (firstcell1 == null) {
                            return;
                        }
                        cellid.Left = startleft;
                        cellid.Width = firstcell1.Width;
                        cellname.Left = startleft + firstcell1.Width;
                        cellname.Width = this.mTotalPageWidth - (startleft + firstcell1.Width);
                        if (this.mTotalPageWidth - (startleft + firstcell1.Width) <= 5) {
                            rowGroup.Cells.Remove(cellname);
                        }
                    } else {
                        XRTableCell firstcell1 = (XRTableCell) this.rowDetail.Cells[firstfield];
                        if (firstcell1 == null) {
                            return;
                        }
                        if (!this.mShowGroupId && group == 4) {
                            cellid.Left = startleft;
                            cellid.Width = firstcell1.Width;
                            cellname.Left = startleft;
                            cellname.Width = this.mTotalPageWidth - (startleft);
                            if (this.mTotalPageWidth - (startleft) <= 5) {
                                rowGroup.Cells.Remove(cellname);
                            }
                        } else {
                            cellid.Left = startleft;
                            cellid.Width = firstcell1.Width;
                            cellname.Left = startleft + firstcell1.Width;
                            cellname.Width = this.mTotalPageWidth - (startleft + firstcell1.Width);
                            if (this.mTotalPageWidth - (startleft + firstcell1.Width) <= 5) {
                                rowGroup.Cells.Remove(cellname);
                            }
                        }
                    }
                    ((ISupportInitialize) (tblGroup)).EndInit();
                } else {
                    cellid = new XRTableCell();
                    rowGroup.Cells.Add(cellid);
                    cellid.Name = "GROUP_ID" + Convert.ToString(5 - group) + "EMPTY";
                    int startleft = 0;
                    ((ISupportInitialize) (tblGroup)).BeginInit();
                    if (this.mReport.Show_Order) {
                        XRTableCell cellorderdetail = (XRTableCell) this.rowDetail.Cells["ORDERDETAIL"];
                        cellorder.Left = cellorderdetail.Left;
                        cellorder.Width = cellorderdetail.Width;
                        startleft += cellorderdetail.Width;
                        cellid.Left = startleft;
                        cellid.Width = this.mTotalPageWidth - cellorderdetail.Width;
                    } else {
                        cellid.Left = startleft;
                        cellid.Width = this.mTotalPageWidth;
                    }
                    ((ISupportInitialize) (tblGroup)).EndInit();
                }
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private string GetFirstSumField() {
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable,
                "Visible=1 and field_width>0 and (is_sum=1 and field_type='NUMBER') and part=" + this.mReport.Part, "Field_Order", DataViewRowState.CurrentRows);
            if (dv.Count > 0) {
                return dv[0]["field_id"].ToString();
            } else {
                return string.Empty;
            }
        }

        private string GetFirstSumFieldLastGroup() {
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable,
                "Visible=1 and field_width>0 and ((is_sum=1 and field_type='NUMBER') or Show_in_group=1) and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            if (dv.Count > 0) {
                return dv[0]["field_id"].ToString();
            } else {
                return string.Empty;
            }
        }

        private string GetFirstField() {
            if (this.mReport.Repeat_Column) {
                DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                if (dv.Count > 0) {
                    return dv[0]["field_id"].ToString();
                } else {
                    return string.Empty;
                }
            } else {
                DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                    DataViewRowState.CurrentRows);
                if (dv.Count > 0) {
                    return dv[0]["field_id"].ToString();
                } else {
                    return string.Empty;
                }
            }
        }

        private void RefreshTotal() {
            if (this.mReport.AlwaysHideSummary) {
                return;
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and is_sum=1 and field_type='NUMBER'", "Field_Order",
                DataViewRowState.CurrentRows);
            if (dv.Count == 0) {
                this.tblFooter.Visible = false;
                return;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            this.tblFooter.BeginInit();
            this.rowFooter.SuspendLayout();
            this.tblFooter.Width = this.rowFooter.Width = this.mTotalPageWidth;
            dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            string firstfield = this.GetFirstField();
            XRTableCell cellid = null;
            XRTableCell cellempty = null;
            if (firstsumfield != string.Empty) {
                if (this.mReport.Part == 1) {
                    cellid = (XRTableCell) this.rowFooter.Cells["TOTAL_TEXT"];
                } else {
                    if (firstfield != firstsumfield) {
                        cellempty = (XRTableCell) this.rowFooter.Cells["TOTALEMPTY"];
                    } else {
                        if (this.mReport.Show_Order) {
                            cellempty = (XRTableCell) this.rowFooter.Cells["TOTALEMPTY"];
                        }
                    }
                }
                int startleft = 0;
                XRTableCell firstsumcell1 = (XRTableCell) this.rowDetail.Cells[firstsumfield];
                if (this.mReport.Part == 1) {
                    cellid.Left = startleft;
                    cellid.Width = firstsumcell1.Left - (startleft);
                } else {
                    if (cellempty != null) {
                        cellempty.Left = startleft;
                        cellempty.Width = firstsumcell1.Left - (startleft);
                    }
                }
                bool doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        string fieldname2 = "TOTAL" + drv["field_id"].ToString().Trim();
                        XRTableCell cell = (XRTableCell) this.rowFooter.Cells[fieldname2];
                        XRTableCell celldetail = (XRTableCell) this.rowDetail.Cells[fieldname1];
                        cell.Left = celldetail.Left;
                        cell.Width = celldetail.Width;
                    }
                }
            } else {
                if (this.mReport.Part == 1) {
                    cellid = (XRTableCell) this.rowFooter.Cells["TOTAL_TEXT"];
                    cellid.Left = 0;
                    cellid.Width = this.mTotalPageWidth;
                } else {
                    cellid = (XRTableCell) this.rowFooter.Cells["TOTALEMPTY"];
                    cellid.Left = 0;
                    cellid.Width = this.mTotalPageWidth;
                }
            }
            this.rowFooter.ResumeLayout();
            this.tblFooter.EndInit();
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private void CreateTotal() {
            if (this.mReport.AlwaysHideSummary) {
                return;
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and is_sum=1 and field_type='NUMBER'", "Field_Order",
                DataViewRowState.CurrentRows);
            if (dv.Count == 0) {
                this.tblFooter.Visible = false;
                return;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            this.tblFooter.StyleName = "totalTableStyle";
            this.tblFooter.Left = 0;
            this.tblFooter.Top = 0;
            this.tblFooter.Height = this.mGroupHeight;
            this.tblFooter.Width = this.mTotalPageWidth;
            dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            string firstfield = this.GetFirstField();
            XRTableCell cellid = null;
            XRTableCell cellempty = null;
            if (firstsumfield != string.Empty) {
                if (this.mReport.Part == 1) {
                    cellid = new XRTableCell();
                    this.rowFooter.Cells.Add(cellid);
                    cellid.Name = "TOTAL_TEXT";
                    cellid.Text = "                                " + this.mFTSMain.MsgManager.GetMessage("MSG_SUM_TEXT");
                    this.FormatStringField(cellid);
                } else {
                    if (firstfield != firstsumfield) {
                        cellempty = new XRTableCell();
                        this.rowFooter.Cells.Add(cellempty);
                        cellempty.Name = "TOTALEMPTY";
                    } else {
                        if (this.mReport.Show_Order) {
                            cellempty = new XRTableCell();
                            this.rowFooter.Cells.Add(cellempty);
                            cellempty.Name = "TOTALEMPTY";
                        }
                    }
                }
                bool doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        XRTableCell cell = new XRTableCell();
                        this.rowFooter.Cells.Add(cell);
                        cell.Name = "TOTAL" + fieldname1;
                        if ((Int16) drv["is_sum"] == 1 && drv["field_type"].ToString() == "NUMBER") {
                            cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname1, string.Empty)});
                            XRSummary summary = new XRSummary();
                            summary.Running = SummaryRunning.Report;
                            cell.Summary = summary;
                            cell.SummaryCalculated += new TextFormatEventHandler(this.cell_BeforePrint);
                        } else {
                            if (drv["field_type"].ToString() == "NUMBER") {
                                cell.BeforePrint += new PrintEventHandler(this.cell_BeforePrint1);
                            }
                        }
                    }
                }
                int startleft = 0;
                ((ISupportInitialize) (this.tblFooter)).BeginInit();
                XRTableCell firstsumcell1 = (XRTableCell) this.rowDetail.Cells[firstsumfield];
                if (this.mReport.Part == 1) {
                    cellid.Left = startleft;
                    cellid.Width = firstsumcell1.Left - (startleft);
                } else {
                    if (cellempty != null) {
                        cellempty.Left = startleft;
                        cellempty.Width = firstsumcell1.Left - (startleft);
                    }
                }
                doit = false;
                foreach (DataRowView drv in dv) {
                    string fieldname1 = drv["field_id"].ToString().Trim();
                    if (fieldname1 == firstsumfield) {
                        doit = true;
                    }
                    if (doit) {
                        string fieldname2 = "TOTAL" + drv["field_id"].ToString().Trim();
                        XRTableCell cell = (XRTableCell) this.rowFooter.Cells[fieldname2];
                        XRTableCell celldetail = (XRTableCell) this.rowDetail.Cells[fieldname1];
                        cell.Left = celldetail.Left;
                        cell.Width = celldetail.Width;
                    }
                }
                ((ISupportInitialize) (this.tblFooter)).EndInit();
            } else {
                if (this.mReport.Part == 1) {
                    cellid = new XRTableCell();
                    this.rowFooter.Cells.Add(cellid);
                    cellid.Name = "TOTAL_TEXT";
                    cellid.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SUM_TEXT");
                    this.FormatStringField(cellid);
                    int startleft = 0;
                    ((ISupportInitialize) (this.tblFooter)).BeginInit();
                    cellid.Left = startleft;
                    cellid.Width = this.mTotalPageWidth;
                    ((ISupportInitialize) (this.tblFooter)).EndInit();
                } else {
                    cellid = new XRTableCell();
                    this.rowFooter.Cells.Add(cellid);
                    cellid.Name = "TOTALEMPTY";
                    int startleft = 0;
                    ((ISupportInitialize) (this.tblFooter)).BeginInit();
                    cellid.Left = startleft;
                    cellid.Width = this.mTotalPageWidth;
                    ((ISupportInitialize) (this.tblFooter)).EndInit();
                }
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private void SetTH() {
            if (this.mReport.Print_Style != "DETAIL") {
                this.Detail.Visible = false;
            }
        }

        private void MoveFieldUp() {
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable,
                "Visible=1 and field_width>0 and Show_in_group=1 and (is_sum=0 or field_type <> 'NUMBER') and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            GroupHeaderBand groupheader = null;
            groupheader = this.GroupHeader1;
            tblGroup = this.tblGroup1;
            rowGroup = this.rowGroup1;
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = (XRTableCell) rowGroup.Cells["GROUP1" + fieldname];
                if (cell != null) {
                    cell.DataBindings.AddRange(new XRBinding[] {new XRBinding("Text", this.mReport.DataSet, fieldname, string.Empty)});
                    if (drv["field_type"].ToString().Trim() == "NUMBER") {
                        this.FormatNumberField(cell, (int) drv["decimal_digit"]);
                    } else {
                        if (drv["field_type"].ToString().Trim() == "DATE") {
                            this.FormatDateField(cell);
                        } else {
                            this.FormatStringField(cell);
                        }
                    }
                }
            }
        }

        private void RefreshReportHeader() {
            XRLabel lblReport_Name = (XRLabel) this.ReportHeader.Controls["REPORT_NAME"];
            lblReport_Name.Width = this.mTotalPageWidth;
            XRLabel lblTemplate_ID = (XRLabel) this.ReportHeader.Controls["TEMPLATE_ID"];
            lblTemplate_ID.Width = this.mTotalPageWidth;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Sub_Title[i] != string.Empty) {
                    XRLabel lblSubtitle = (XRLabel) this.ReportHeader.Controls["SUBTITLE" + Convert.ToString(i + 1)];
                    lblSubtitle.Width = this.mTotalPageWidth;
                }
            }
            XRLabel lblReport_Period = (XRLabel) this.ReportHeader.Controls["REPORT_PERIOD"];
            lblReport_Period.Width = this.mTotalPageWidth;
            XRLabel lblCurrency_Text = (XRLabel) this.ReportHeader.Controls["CURRENCY_TEXT"];
            if (lblCurrency_Text != null) {
                lblCurrency_Text.Width = this.mTotalPageWidth;
            }
        }

        private void CreateReportHeader() {
            int topmost = 0;

            XRLabel lblParent_Company = new XRLabel();
            lblParent_Company.Name = "PARENT_COMPANY_NAME";
            lblParent_Company.Text = this.mFTSMain.SystemVars.GetSystemVars("PARENT_COMPANY_NAME").ToString();
            if (lblParent_Company.Text != string.Empty) {
                this.ReportHeader.Controls.Add(lblParent_Company);
                lblParent_Company.Left = 0;
                lblParent_Company.Top = topmost;
                lblParent_Company.Width = 450;
                lblParent_Company.Height = this.mLabelHeight;
                topmost += this.mLabelHeight;
                lblParent_Company.TextAlignment = TextAlignment.MiddleCenter;
                lblParent_Company.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Bold);
            }
            XRLabel lblCompany_Name = new XRLabel();
            lblCompany_Name.Name = "COMPANY_NAME";
            lblCompany_Name.Text = this.mFTSMain.SystemVars.GetSystemVars("COMPANY_NAME").ToString();
            this.ReportHeader.Controls.Add(lblCompany_Name);
            lblCompany_Name.Left = 0;
            lblCompany_Name.Top = topmost;
            lblCompany_Name.Width = 450;
            lblCompany_Name.Height = this.mLabelHeight;
            if (lblParent_Company.Text != string.Empty || (bool) this.mFTSMain.SystemVars.GetSystemVars("SHOW_ADDRESS_ON_REPORT")) {
                lblCompany_Name.TextAlignment = TextAlignment.MiddleCenter;
            } else {
                lblCompany_Name.TextAlignment = TextAlignment.MiddleLeft;
            }
            int incfont = 0;
            lblCompany_Name.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size + incfont, FontStyle.Bold);
            XRLabel lblCompany_Address = null;
            if ((bool) this.mFTSMain.SystemVars.GetSystemVars("SHOW_ADDRESS_ON_REPORT")) {
                lblCompany_Address = new XRLabel();
                lblCompany_Address.Name = "FULL_ADDRESS";
                lblCompany_Address.Text = this.mFTSMain.SystemVars.GetSystemVars("FULL_ADDRESS").ToString();
                this.ReportHeader.Controls.Add(lblCompany_Address);
                lblCompany_Address.Left = 0;
                lblCompany_Address.Top = lblCompany_Name.Bottom;
                lblCompany_Address.Width = 450;
                lblCompany_Address.Height = this.mLabelHeight;
                lblCompany_Address.TextAlignment = TextAlignment.MiddleCenter;
                incfont = 0;
                lblCompany_Address.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size + incfont, FontStyle.Regular);
            }
            string filename = Functions.GetPathName() + "Graphics\\logo.jpg";
            XRPictureBox logobox = new XRPictureBox();
            if (Functions.FileExists(filename)) {
                logobox.Sizing = ImageSizeMode.StretchImage;
                logobox.Name = "LOGO";
                logobox.Width = (int) this.mFTSMain.SystemVars.GetSystemVars("LOGO_WIDTH")*5;
                logobox.Height = (int) this.mFTSMain.SystemVars.GetSystemVars("LOGO_HEIGHT")*4;
                logobox.Left = 50;
                if (lblCompany_Address == null) {
                    logobox.Top = lblCompany_Name.Bottom;
                } else {
                    logobox.Top = lblCompany_Address.Bottom;
                }
                this.ReportHeader.Controls.Add(logobox);
                logobox.BeforePrint += new PrintEventHandler(this.logobox_BeforePrint);
            }

            int starttop = this.ReportHeader.Height;
            XRLabel lblReport_Name = new XRLabel();
            lblReport_Name.Name = "REPORT_NAME";
            lblReport_Name.Text = this.mReport.Report_Name.ToUpper();
            this.ReportHeader.Controls.Add(lblReport_Name);
            lblReport_Name.Left = 0;
            lblReport_Name.Top = starttop;
            lblReport_Name.Width = this.mTotalPageWidth;
            lblReport_Name.TextAlignment = TextAlignment.MiddleCenter;
            lblReport_Name.Font = new Font(this.mReport.Title_Font_Name, this.mReport.Title_Font_Size, FontStyle.Bold);
            lblReport_Name.ForeColor = Color.FromName(this.mReport.Title_Font_Color);
            XRLabel lblTemplate_ID = new XRLabel();
            lblTemplate_ID.Name = "TEMPLATE_ID";
            lblTemplate_ID.Text = this.mReport.Template_ID;
            this.ReportHeader.Controls.Add(lblTemplate_ID);
            lblTemplate_ID.Left = 0;
            lblTemplate_ID.Top = 0;
            lblTemplate_ID.Width = this.mTotalPageWidth;
            lblTemplate_ID.TextAlignment = TextAlignment.MiddleRight;
            lblTemplate_ID.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size, FontStyle.Regular);
            starttop = lblReport_Name.Bottom;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Sub_Title[i] != string.Empty) {
                    XRLabel lblSubtitle = new XRLabel();
                    lblSubtitle.Name = "SUBTITLE" + Convert.ToString(i + 1);
                    lblSubtitle.Text = this.mReport.SubtitleString[i];
                    this.ReportHeader.Controls.Add(lblSubtitle);
                    lblSubtitle.Left = 0;
                    lblSubtitle.Top = starttop;
                    lblSubtitle.Width = this.mTotalPageWidth;
                    lblSubtitle.TextAlignment = TextAlignment.MiddleCenter;
                    this.SetSubtitleFont(lblSubtitle);
                    starttop += lblSubtitle.Height;
                }
            }
            XRLabel lblReport_Period = new XRLabel();
            lblReport_Period.Name = "REPORT_PERIOD";
            lblReport_Period.Text = this.mReport.ReportPeriod.ReportPeriodName;
            this.ReportHeader.Controls.Add(lblReport_Period);
            lblReport_Period.Left = 0;
            lblReport_Period.Top = starttop + 10;
            lblReport_Period.Width = this.mTotalPageWidth;
            lblReport_Period.TextAlignment = TextAlignment.MiddleCenter;
            this.SetSubtitleFont(lblReport_Period);
        }

        private void CreateCurrencyText() {
            if (this.mReport.Currency_Text != string.Empty) {
                XRLabel lblCurrency_Text = new XRLabel();
                lblCurrency_Text.Name = "Currency_Text";
                lblCurrency_Text.Text = this.mReport.Currency_Text;
                this.ReportHeader.Controls.Add(lblCurrency_Text);
                lblCurrency_Text.Left = 0;
                lblCurrency_Text.Top = this.ReportHeader.Bottom + 10;
                lblCurrency_Text.Width = this.mTotalPageWidth;
                lblCurrency_Text.TextAlignment = TextAlignment.MiddleRight;
                this.SetSubtitleFont(lblCurrency_Text);
            }
        }

        private void RefreshBalanceLuykeHeader() {
            XRLabel lblReport_Period = (XRLabel) this.ReportHeader.Controls["REPORT_PERIOD"];
            int balancewidth = 110;
            int nocowidth = 35;
            int ntewidth = 80;
            int extrawidth = 80;
            int labelwidth = 130;
            int totalwidth = this.mTotalPageWidth;
            if (this.mReport.Show_Balance_Nte) {
                XRLabel lblbalance_nte_no = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NTE_NO"];
                lblbalance_nte_no.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblbalance_nte_co = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NTE_CO"];
                lblbalance_nte_co.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblbalance_nte_no_text = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NTE_NO_TEXT"];
                lblbalance_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblbalance_nte_co_text = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NTE_CO_TEXT"];
                lblbalance_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblbalance_nte = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NTE"];
                lblbalance_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                totalwidth = lblbalance_nte.Left;
            }
            if (this.mReport.Show_Balance) {
                XRLabel lblbalance_no = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NO"];
                lblbalance_no.Left = totalwidth - balancewidth;
                XRLabel lblbalance_co = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_CO"];
                lblbalance_co.Left = totalwidth - balancewidth;
                XRLabel lblbalance_no_text = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_NO_TEXT"];
                lblbalance_no_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblbalance_co_text = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE_CO_TEXT"];
                lblbalance_co_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblbalance = (XRLabel) this.ReportHeader.Controls["BEGINNING_BALANCE"];
                lblbalance.Left = totalwidth - balancewidth - nocowidth - labelwidth;
            }
        }

        private void CreateBalanceLuykeHeader() {
            XRLabel lblReport_Period = (XRLabel) this.ReportHeader.Controls["REPORT_PERIOD"];
            int starttop = lblReport_Period.Bottom;
            int balancewidth = 110;
            int nocowidth = 35;
            int ntewidth = 80;
            int extrawidth = 80;
            int labelwidth = 130;
            int totalwidth = this.mTotalPageWidth;
            if (this.mReport.Show_Balance_Nte) {
                XRLabel lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NTE_NO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.BeginningBalanceNte > 0) {
                        lblbalance_nte_no.Text = this.ConvertToString(this.mReport.BeginningBalanceNte);
                    } else {
                        lblbalance_nte_no.Text = string.Empty;
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_nte_no.Text = this.ConvertToString(this.mReport.BeginningBalanceNte);
                    } else {
                        lblbalance_nte_no.Text = string.Empty;
                    }
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = this.mTotalPageWidth - balancewidth;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_nte_co = new XRLabel();
                lblbalance_nte_co.Name = "BEGINNING_BALANCE_NTE_CO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.BeginningBalanceNte > 0) {
                        lblbalance_nte_co.Text = string.Empty;
                    } else {
                        lblbalance_nte_co.Text = this.ConvertToString(this.mReport.BeginningBalanceNte*-1);
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_nte_co.Text = string.Empty;
                    } else {
                        lblbalance_nte_co.Text = this.ConvertToString(this.mReport.BeginningBalanceNte*(-1));
                    }
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_co);
                lblbalance_nte_co.Left = this.mTotalPageWidth - balancewidth;
                lblbalance_nte_co.Top = starttop + lblbalance_nte_no.Height;
                lblbalance_nte_co.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_nte_co);
                lblbalance_nte_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_nte_no_text = new XRLabel();
                lblbalance_nte_no_text.Name = "BEGINNING_BALANCE_NTE_NO_TEXT";
                lblbalance_nte_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportHeader.Controls.Add(lblbalance_nte_no_text);
                lblbalance_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblbalance_nte_no_text.Top = starttop;
                lblbalance_nte_no_text.Width = nocowidth;
                lblbalance_nte_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_nte_no_text);
                XRLabel lblbalance_nte_co_text = new XRLabel();
                lblbalance_nte_co_text.Name = "BEGINNING_BALANCE_NTE_CO_TEXT";
                lblbalance_nte_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportHeader.Controls.Add(lblbalance_nte_co_text);
                lblbalance_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblbalance_nte_co_text.Top = starttop + lblbalance_nte_no.Height;
                lblbalance_nte_co_text.Width = nocowidth;
                lblbalance_nte_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_nte_co_text);
                XRLabel lblbalance_nte = new XRLabel();
                lblbalance_nte.Name = "BEGINNING_BALANCE_NTE";
                lblbalance_nte.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NTE");
                this.ReportHeader.Controls.Add(lblbalance_nte);
                lblbalance_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                lblbalance_nte.Top = starttop;
                lblbalance_nte.Width = ntewidth;
                lblbalance_nte.TextAlignment = TextAlignment.MiddleCenter;
                this.SetSubtitleFont1(lblbalance_nte);
                totalwidth = lblbalance_nte.Left;
            }
            if (this.mReport.Show_Balance) {
                XRLabel lblbalance_no = new XRLabel();
                lblbalance_no.Name = "BEGINNING_BALANCE_NO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.BeginningBalance > 0) {
                        lblbalance_no.Text = this.ConvertToString(this.mReport.BeginningBalance);
                    } else {
                        lblbalance_no.Text = string.Empty;
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_no.Text = this.ConvertToString(this.mReport.BeginningBalance);
                    } else {
                        lblbalance_no.Text = string.Empty;
                    }
                }
                this.ReportHeader.Controls.Add(lblbalance_no);
                lblbalance_no.Left = totalwidth - balancewidth;
                lblbalance_no.Top = starttop;
                lblbalance_no.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_no);
                lblbalance_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_co = new XRLabel();
                lblbalance_co.Name = "BEGINNING_BALANCE_CO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.BeginningBalance > 0) {
                        lblbalance_co.Text = string.Empty;
                    } else {
                        lblbalance_co.Text = this.ConvertToString(this.mReport.BeginningBalance*-1);
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_co.Text = string.Empty;
                    } else {
                        lblbalance_co.Text = this.ConvertToString(this.mReport.BeginningBalance*(-1));
                    }
                }
                this.ReportHeader.Controls.Add(lblbalance_co);
                lblbalance_co.Left = totalwidth - balancewidth;
                lblbalance_co.Top = starttop + lblbalance_no.Height;
                lblbalance_co.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_co);
                lblbalance_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_no_text = new XRLabel();
                lblbalance_no_text.Name = "BEGINNING_BALANCE_NO_TEXT";
                lblbalance_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportHeader.Controls.Add(lblbalance_no_text);
                lblbalance_no_text.Left = totalwidth - balancewidth - nocowidth;
                lblbalance_no_text.Top = starttop;
                lblbalance_no_text.Width = nocowidth;
                lblbalance_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_no_text);
                XRLabel lblbalance_co_text = new XRLabel();
                lblbalance_co_text.Name = "BEGINNING_BALANCE_CO_TEXT";
                lblbalance_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportHeader.Controls.Add(lblbalance_co_text);
                lblbalance_co_text.Left = totalwidth - balancewidth - nocowidth;
                lblbalance_co_text.Top = starttop + lblbalance_no.Height;
                lblbalance_co_text.Width = nocowidth;
                lblbalance_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_co_text);
                XRLabel lblbalance = new XRLabel();
                lblbalance.Name = "BEGINNING_BALANCE";
                lblbalance.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY");
                this.ReportHeader.Controls.Add(lblbalance);
                lblbalance.Left = totalwidth - balancewidth - nocowidth - labelwidth;
                lblbalance.Top = starttop;
                lblbalance.Width = labelwidth;
                lblbalance.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance);
            }
        }

        private void RefreshBalanceLuykeFooter() {
            int balancewidth = 110;
            int nocowidth = 35;
            int ntewidth = 78;
            int extrawidth = 78;
            int labelwidth = 130;
            int totalwidth = this.mTotalPageWidth;
            if (this.mReport.Show_Lke_Nte) {
                XRLabel lblluyke_nte_no = (XRLabel) this.ReportFooter.Controls["LUYKE_NTE_NO"];
                lblluyke_nte_no.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblluyke_nte_co = (XRLabel) this.ReportFooter.Controls["LUYKE_NTE_CO"];
                lblluyke_nte_co.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblluyke_nte_no_text = (XRLabel) this.ReportFooter.Controls["LUYKE_NTE_NO_TEXT"];
                lblluyke_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblluyke_nte_co_text = (XRLabel) this.ReportFooter.Controls["LUYKE_NTE_CO_TEXT"];
                lblluyke_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblluyke_nte = (XRLabel) this.ReportFooter.Controls["LUYKE_NTE"];
                lblluyke_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                totalwidth = lblluyke_nte.Left;
            } 
            if (this.mReport.Show_Lke) {
                XRLabel lblluyke_no = (XRLabel) this.ReportFooter.Controls["LUYKE_NO"];
                lblluyke_no.Left = totalwidth - balancewidth;
                XRLabel lblluyke_co = (XRLabel) this.ReportFooter.Controls["LUYKE_CO"];
                lblluyke_co.Left = totalwidth - balancewidth;
                XRLabel lblluyke_no_text = (XRLabel) this.ReportFooter.Controls["LUYKE_NO_TEXT"];
                lblluyke_no_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblluyke_co_text = (XRLabel) this.ReportFooter.Controls["LUYKE_CO_TEXT"];
                lblluyke_co_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblluyke = (XRLabel) this.ReportFooter.Controls["LUYKE"];
                lblluyke.Left = totalwidth - balancewidth - nocowidth - labelwidth;
            }
            totalwidth = this.mTotalPageWidth;
            if (this.mReport.Show_Balance_Nte) {
                XRLabel lblbalance_nte_no = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NTE_NO"];
                lblbalance_nte_no.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblbalance_nte_co = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NTE_CO"];
                lblbalance_nte_co.Left = this.mTotalPageWidth - balancewidth;
                XRLabel lblbalance_nte_no_text = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NTE_NO_TEXT"];
                lblbalance_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblbalance_nte_co_text = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NTE_CO_TEXT"];
                lblbalance_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                XRLabel lblbalance_nte = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NTE"];
                lblbalance_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                totalwidth = lblbalance_nte.Left;
            }
            if (this.mReport.Show_Balance) {
                XRLabel lblbalance_no = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NO"];
                lblbalance_no.Left = totalwidth - balancewidth;
                XRLabel lblbalance_co = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_CO"];
                lblbalance_co.Left = totalwidth - balancewidth;
                XRLabel lblbalance_no_text = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_NO_TEXT"];
                lblbalance_no_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblbalance_co_text = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE_CO_TEXT"];
                lblbalance_co_text.Left = totalwidth - balancewidth - nocowidth;
                XRLabel lblbalance = (XRLabel) this.ReportFooter.Controls["ENDING_BALANCE"];
                lblbalance.Left = totalwidth - balancewidth - nocowidth - labelwidth;
            }
        }

        private void CreateBalanceLuykeFooter() {
            int starttop = 0;
            if (this.tblFooter.Visible) {
                starttop = this.tblFooter.Bottom + 20;
                this.ReportFooter.Height = this.tblFooter.Height;
            } else {
                this.ReportFooter.Height = 20;
            }
            int balancewidth = 90;
            int nocowidth = 35;
            int ntewidth = 78;
            int extrawidth = 78;
            int labelwidth = 130;
            int totalwidth = this.mTotalPageWidth;
            XRLabel lblluyke_nte_co_text = null;
            XRLabel lblluyke_extra_co_text = null;
            if (this.mReport.Show_Lke_Nte) {
                XRLabel lblluyke_nte_no = new XRLabel();
                lblluyke_nte_no.Name = "LUYKE_NTE_NO";
                lblluyke_nte_no.Text = this.ConvertToString(this.mReport.LkeNoNte);
                this.ReportFooter.Controls.Add(lblluyke_nte_no);
                lblluyke_nte_no.Left = this.mTotalPageWidth - balancewidth;
                lblluyke_nte_no.Top = starttop;
                lblluyke_nte_no.Width = balancewidth;
                this.SetSubtitleFont1(lblluyke_nte_no);
                lblluyke_nte_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblluyke_nte_co = new XRLabel();
                lblluyke_nte_co.Name = "LUYKE_NTE_CO";
                lblluyke_nte_co.Text = this.ConvertToString(this.mReport.LkeCoNte);
                this.ReportFooter.Controls.Add(lblluyke_nte_co);
                lblluyke_nte_co.Left = this.mTotalPageWidth - balancewidth;
                lblluyke_nte_co.Top = starttop + lblluyke_nte_no.Height;
                lblluyke_nte_co.Width = balancewidth;
                this.SetSubtitleFont1(lblluyke_nte_co);
                lblluyke_nte_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblluyke_nte_no_text = new XRLabel();
                lblluyke_nte_no_text.Name = "LUYKE_NTE_NO_TEXT";
                lblluyke_nte_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportFooter.Controls.Add(lblluyke_nte_no_text);
                lblluyke_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblluyke_nte_no_text.Top = starttop;
                lblluyke_nte_no_text.Width = nocowidth;
                lblluyke_nte_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblluyke_nte_no_text);
                lblluyke_nte_co_text = new XRLabel();
                lblluyke_nte_co_text.Name = "LUYKE_NTE_CO_TEXT";
                lblluyke_nte_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportFooter.Controls.Add(lblluyke_nte_co_text);
                lblluyke_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblluyke_nte_co_text.Top = starttop + lblluyke_nte_no.Height;
                lblluyke_nte_co_text.Width = nocowidth;
                lblluyke_nte_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblluyke_nte_co_text);
                XRLabel lblluyke_nte = new XRLabel();
                lblluyke_nte.Name = "LUYKE_NTE";
                lblluyke_nte.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NTE");
                this.ReportFooter.Controls.Add(lblluyke_nte);
                lblluyke_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                lblluyke_nte.Top = starttop;
                lblluyke_nte.Width = ntewidth;
                lblluyke_nte.TextAlignment = TextAlignment.MiddleCenter;
                this.SetSubtitleFont1(lblluyke_nte);
                totalwidth = lblluyke_nte.Left;
            } 
            XRLabel lblluyke_co_text = null;
            if (this.mReport.Show_Lke) {
                XRLabel lblluyke_no = new XRLabel();
                lblluyke_no.Name = "LUYKE_NO";
                lblluyke_no.Text = this.ConvertToString(this.mReport.LkeNo);
                this.ReportFooter.Controls.Add(lblluyke_no);
                lblluyke_no.Left = totalwidth - balancewidth;
                lblluyke_no.Top = starttop;
                lblluyke_no.Width = balancewidth;
                this.SetSubtitleFont1(lblluyke_no);
                lblluyke_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblluyke_co = new XRLabel();
                lblluyke_co.Name = "LUYKE_CO";
                lblluyke_co.Text = this.ConvertToString(this.mReport.LkeCo);
                this.ReportFooter.Controls.Add(lblluyke_co);
                lblluyke_co.Left = totalwidth - balancewidth;
                lblluyke_co.Top = starttop + lblluyke_no.Height;
                lblluyke_co.Width = balancewidth;
                this.SetSubtitleFont1(lblluyke_co);
                lblluyke_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblluyke_no_text = new XRLabel();
                lblluyke_no_text.Name = "LUYKE_NO_TEXT";
                lblluyke_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportFooter.Controls.Add(lblluyke_no_text);
                lblluyke_no_text.Left = totalwidth - balancewidth - nocowidth;
                lblluyke_no_text.Top = starttop;
                lblluyke_no_text.Width = nocowidth;
                lblluyke_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblluyke_no_text);
                lblluyke_co_text = new XRLabel();
                lblluyke_co_text.Name = "LUYKE_CO_TEXT";
                lblluyke_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportFooter.Controls.Add(lblluyke_co_text);
                lblluyke_co_text.Left = totalwidth - balancewidth - nocowidth;
                lblluyke_co_text.Top = starttop + lblluyke_no.Height;
                lblluyke_co_text.Width = nocowidth;
                lblluyke_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblluyke_co_text);
                XRLabel lblluyke = new XRLabel();
                lblluyke.Name = "LUYKE";
                lblluyke.Text = this.mFTSMain.MsgManager.GetMessage("TONG_PHAT_SINH_LKE");
                this.ReportFooter.Controls.Add(lblluyke);
                lblluyke.Left = totalwidth - balancewidth - nocowidth - labelwidth;
                lblluyke.Top = starttop;
                lblluyke.Width = labelwidth;
                lblluyke.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblluyke);
            }
            starttop = 20;
            if (this.tblFooter.Visible) {
                starttop = this.tblFooter.Bottom + 20;
            }
            if (this.mReport.Show_Lke_Nte) {
                starttop = lblluyke_nte_co_text.Bottom;
            }
            if (this.mReport.Show_Lke) {
                starttop = lblluyke_co_text.Bottom;
            }
            totalwidth = this.mTotalPageWidth;
            if (this.mReport.Show_Balance_Nte) {
                XRLabel lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "ENDING_BALANCE_NTE_NO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.EndingBalanceNte > 0) {
                        lblbalance_nte_no.Text = this.ConvertToString(this.mReport.EndingBalanceNte);
                    } else {
                        lblbalance_nte_no.Text = string.Empty;
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_nte_no.Text = this.ConvertToString(this.mReport.EndingBalanceNte);
                    } else {
                        lblbalance_nte_no.Text = string.Empty;
                    }
                }
                this.ReportFooter.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = this.mTotalPageWidth - balancewidth;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_nte_co = new XRLabel();
                lblbalance_nte_co.Name = "ENDING_BALANCE_NTE_CO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.EndingBalanceNte > 0) {
                        lblbalance_nte_co.Text = string.Empty;
                    } else {
                        lblbalance_nte_co.Text = this.ConvertToString(this.mReport.EndingBalanceNte*-1);
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_nte_co.Text = string.Empty;
                    } else {
                        lblbalance_nte_co.Text = this.ConvertToString(this.mReport.EndingBalanceNte*(-1));
                    }
                }
                this.ReportFooter.Controls.Add(lblbalance_nte_co);
                lblbalance_nte_co.Left = this.mTotalPageWidth - balancewidth;
                lblbalance_nte_co.Top = starttop + lblbalance_nte_no.Height;
                lblbalance_nte_co.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_nte_co);
                lblbalance_nte_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_nte_no_text = new XRLabel();
                lblbalance_nte_no_text.Name = "ENDING_BALANCE_NTE_NO_TEXT";
                lblbalance_nte_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportFooter.Controls.Add(lblbalance_nte_no_text);
                lblbalance_nte_no_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblbalance_nte_no_text.Top = starttop;
                lblbalance_nte_no_text.Width = nocowidth;
                lblbalance_nte_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_nte_no_text);
                XRLabel lblbalance_nte_co_text = new XRLabel();
                lblbalance_nte_co_text.Name = "ENDING_BALANCE_NTE_CO_TEXT";
                lblbalance_nte_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportFooter.Controls.Add(lblbalance_nte_co_text);
                lblbalance_nte_co_text.Left = this.mTotalPageWidth - balancewidth - nocowidth;
                lblbalance_nte_co_text.Top = starttop + lblbalance_nte_no.Height;
                lblbalance_nte_co_text.Width = nocowidth;
                lblbalance_nte_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_nte_co_text);
                XRLabel lblbalance_nte = new XRLabel();
                lblbalance_nte.Name = "ENDING_BALANCE_NTE";
                lblbalance_nte.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NTE");
                this.ReportFooter.Controls.Add(lblbalance_nte);
                lblbalance_nte.Left = this.mTotalPageWidth - balancewidth - nocowidth - ntewidth;
                lblbalance_nte.Top = starttop;
                lblbalance_nte.Width = ntewidth;
                lblbalance_nte.TextAlignment = TextAlignment.MiddleCenter;
                this.SetSubtitleFont1(lblbalance_nte);
                totalwidth = lblbalance_nte.Left;
            }
            if (this.mReport.Show_Balance) {
                XRLabel lblbalance_no = new XRLabel();
                lblbalance_no.Name = "ENDING_BALANCE_NO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.EndingBalance > 0) {
                        lblbalance_no.Text = this.ConvertToString(this.mReport.EndingBalance);
                    } else {
                        lblbalance_no.Text = string.Empty;
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_no.Text = this.ConvertToString(this.mReport.EndingBalance);
                    } else {
                        lblbalance_no.Text = string.Empty;
                    }
                }
                this.ReportFooter.Controls.Add(lblbalance_no);
                lblbalance_no.Left = totalwidth - balancewidth;
                lblbalance_no.Top = starttop;
                lblbalance_no.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_no);
                lblbalance_no.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_co = new XRLabel();
                lblbalance_co.Name = "ENDING_BALANCE_CO";
                if (this.mReport.ReportDebitCredit == "DEBCRD") {
                    if (this.mReport.EndingBalance > 0) {
                        lblbalance_co.Text = string.Empty;
                    } else {
                        lblbalance_co.Text = this.ConvertToString(this.mReport.EndingBalance*-1);
                    }
                } else {
                    if (this.mReport.ReportDebitCredit == "DEB") {
                        lblbalance_co.Text = string.Empty;
                    } else {
                        lblbalance_co.Text = this.ConvertToString(this.mReport.EndingBalance*(-1));
                    }
                }
                this.ReportFooter.Controls.Add(lblbalance_co);
                lblbalance_co.Left = totalwidth - balancewidth;
                lblbalance_co.Top = starttop + lblbalance_no.Height;
                lblbalance_co.Width = balancewidth;
                this.SetSubtitleFont1(lblbalance_co);
                lblbalance_co.TextAlignment = TextAlignment.MiddleRight;
                XRLabel lblbalance_no_text = new XRLabel();
                lblbalance_no_text.Name = "ENDING_BALANCE_NO_TEXT";
                lblbalance_no_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_NO");
                this.ReportFooter.Controls.Add(lblbalance_no_text);
                lblbalance_no_text.Left = totalwidth - balancewidth - nocowidth;
                lblbalance_no_text.Top = starttop;
                lblbalance_no_text.Width = nocowidth;
                lblbalance_no_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_no_text);
                XRLabel lblbalance_co_text = new XRLabel();
                lblbalance_co_text.Name = "ENDING_BALANCE_CO_TEXT";
                lblbalance_co_text.Text = this.mFTSMain.MsgManager.GetMessage("MSG_CO");
                this.ReportFooter.Controls.Add(lblbalance_co_text);
                lblbalance_co_text.Left = totalwidth - balancewidth - nocowidth;
                lblbalance_co_text.Top = starttop + lblbalance_no.Height;
                lblbalance_co_text.Width = nocowidth;
                lblbalance_co_text.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance_co_text);
                XRLabel lblbalance = new XRLabel();
                lblbalance.Name = "ENDING_BALANCE";
                lblbalance.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY");
                this.ReportFooter.Controls.Add(lblbalance);
                lblbalance.Left = totalwidth - balancewidth - nocowidth - labelwidth;
                lblbalance.Top = starttop;
                lblbalance.Width = labelwidth;
                lblbalance.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblbalance);
            }
        }

        private void RefreshFooterText() {
            XRLabel lblDatetime = (XRLabel) this.ReportFooter.Controls["DATE_TIME"];
            lblDatetime.Name = "DATE_TIME";
            lblDatetime.Width = this.mTotalPageWidth;
            int numcol = 3;
            if (this.mReport.GetFooter(3) != string.Empty) {
                numcol = 4;
            }
            XRTable tblFooterText = (XRTable) this.ReportFooter.Controls["FooterText"];
            XRTableRow rowFooterText = (XRTableRow) tblFooterText.Controls[0];
            tblFooterText.BeginInit();
            rowFooterText.SuspendLayout();
            tblFooterText.Width = rowFooterText.Width = this.mTotalPageWidth;
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = (XRTableCell) rowFooterText.Cells["FOOTER" + Convert.ToString(i + 1)];
                cell.Left = Convert.ToInt32((this.mTotalPageWidth/numcol)*i);
                cell.Width = Convert.ToInt32((this.mTotalPageWidth/numcol));
            }
            rowFooterText.ResumeLayout();
            tblFooterText.EndInit();
        }

        private void CreateFooterText() {
            int starttop = this.ReportFooter.Height;
            XRLabel lblDatetime = new XRLabel();
            lblDatetime.Name = "DATE_TIME";
            lblDatetime.Text = this.mFTSMain.SystemVars.GetSystemVars("CITY").ToString() + ", " + this.mReport.Print_Date;
            this.ReportFooter.Controls.Add(lblDatetime);
            lblDatetime.Left = 0;
            lblDatetime.Top = starttop;
            lblDatetime.Width = this.mTotalPageWidth;
            lblDatetime.TextAlignment = TextAlignment.MiddleRight;
            this.SetFooterFont(lblDatetime);
            XRLabel lblComment = new XRLabel();
            lblComment.Name = "COMMENT_TEXT";
            lblComment.Text = this.mReport.Comment_Text.Replace("\\n", Environment.NewLine);
            lblComment.Multiline = true;
            this.ReportFooter.Controls.Add(lblComment);
            lblComment.Top = starttop;
            lblComment.Width = this.mTotalPageWidth;
            lblComment.TextAlignment = TextAlignment.MiddleLeft;
            this.SetCommentFont(lblComment);
            int numcol = 3;
            if (this.mReport.GetFooter(4) != string.Empty) {
                numcol = 5;
            } else {
                if (this.mReport.GetFooter(3) != string.Empty) {
                    numcol = 4;
                }
            }
            XRTable tblFooterText = new XRTable();
            tblFooterText.Name = "FooterText";
            this.ReportFooter.Controls.Add(tblFooterText);
            XRTableRow rowFooterText = new XRTableRow();
            tblFooterText.Rows.Add(rowFooterText);
            tblFooterText.Top = lblDatetime.Bottom;
            tblFooterText.Left = 0;
            tblFooterText.Width = this.mTotalPageWidth;
            tblFooterText.Height = 23;
            tblFooterText.StyleName = "footerTableStyle";
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = new XRTableCell();
                cell.Name = "FOOTER" + Convert.ToString(i + 1);
                cell.Text = this.mReport.GetFooter(i);
                rowFooterText.Cells.Add(cell);
                cell.TextAlignment = TextAlignment.MiddleCenter;
            }
            ((ISupportInitialize) (tblFooterText)).BeginInit();
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = (XRTableCell) rowFooterText.Cells["FOOTER" + Convert.ToString(i + 1)];
                cell.Left = Convert.ToInt32((this.mTotalPageWidth/numcol)*i);
                cell.Width = Convert.ToInt32((this.mTotalPageWidth/numcol));
            }
            ((ISupportInitialize) (tblFooterText)).EndInit();
        }

        private void RefreshFooterNameText() {
            if (this.mReport.Footer_name[0] == string.Empty && this.mReport.Footer_name[1] == string.Empty && this.mReport.Footer_name[2] == string.Empty &&
                this.mReport.Footer_name[3] == string.Empty && this.mReport.Footer_name[4] == string.Empty) {
                return;
            }
            int starttop = this.ReportFooter.Height;
            int numcol = 3;
            if (this.mReport.Footer_name[4] != string.Empty) {
                numcol = 5;
            } else {
                if (this.mReport.Footer_name[3] != string.Empty) {
                    numcol = 4;
                }
            }
            XRTable tblFooterText = (XRTable) this.ReportFooter.Controls["FooterNameText"];
            XRTableRow rowFooterText = (XRTableRow) tblFooterText.Controls[0];
            tblFooterText.BeginInit();
            rowFooterText.SuspendLayout();
            tblFooterText.Width = rowFooterText.Width = this.mTotalPageWidth;
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = (XRTableCell) rowFooterText.Cells["FOOTER_NAME" + Convert.ToString(i + 1)];
                cell.Left = Convert.ToInt32((this.mTotalPageWidth/numcol)*i);
                cell.Width = Convert.ToInt32((this.mTotalPageWidth/numcol));
            }
            rowFooterText.ResumeLayout();
            tblFooterText.EndInit();
        }

        private void CreateFooterNameText() {
            if (this.mReport.Footer_name[0] == string.Empty && this.mReport.Footer_name[1] == string.Empty && this.mReport.Footer_name[2] == string.Empty &&
                this.mReport.Footer_name[3] == string.Empty && this.mReport.Footer_name[4] == string.Empty) {
                return;
            }
            int starttop = this.ReportFooter.Height;
            int numcol = 3;
            if (this.mReport.Footer_name[4] != string.Empty) {
                numcol = 5;
            } else {
                if (this.mReport.Footer_name[3] != string.Empty) {
                    numcol = 4;
                }
            }
            XRTable tblFooterText = new XRTable();
            tblFooterText.Name = "FooterNameText";
            this.ReportFooter.Controls.Add(tblFooterText);
            XRTableRow rowFooterText = new XRTableRow();
            tblFooterText.Rows.Add(rowFooterText);
            tblFooterText.Top = starttop + this.mFooterSpace;
            tblFooterText.Left = 0;
            tblFooterText.Width = this.mTotalPageWidth;
            tblFooterText.Height = 23;
            tblFooterText.StyleName = "footerTableStyle";
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = new XRTableCell();
                cell.Name = "FOOTER_NAME" + Convert.ToString(i + 1);
                cell.Text = this.mReport.Footer_name[i];
                rowFooterText.Cells.Add(cell);
                cell.TextAlignment = TextAlignment.MiddleCenter;
            }
            ((ISupportInitialize)(tblFooterText)).BeginInit();
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = (XRTableCell)rowFooterText.Cells["FOOTER_NAME" + Convert.ToString(i + 1)];
                cell.Left = Convert.ToInt32((this.mTotalPageWidth / numcol) * i);
                cell.Width = Convert.ToInt32((this.mTotalPageWidth / numcol));
            }
            ((ISupportInitialize)(tblFooterText)).EndInit();
        }

        private void CreateFooterNameTextNXB() {
            if (this.mReport.GetFooter(0) == string.Empty && this.mReport.GetFooter(1) == string.Empty && this.mReport.GetFooter(2) == string.Empty &&
                this.mReport.GetFooter(3) == string.Empty && this.mReport.GetFooter(4) == string.Empty) {
                return;
            }
            int starttop = this.ReportFooter.Height;
            int numcol = 3;
            if (this.mReport.GetFooter(4) != string.Empty) {
                numcol = 5;
            } else {
                if (this.mReport.GetFooter(3) != string.Empty) {
                    numcol = 4;
                }
            }
            XRTable tblFooterText = new XRTable();
            tblFooterText.Name = "FooterNameText";
            this.ReportFooter.Controls.Add(tblFooterText);
            XRTableRow rowFooterText = new XRTableRow();
            tblFooterText.Rows.Add(rowFooterText);
            tblFooterText.Top = starttop + this.mFooterSpace;
            tblFooterText.Left = 0;
            tblFooterText.Width = this.mTotalPageWidth;
            tblFooterText.Height = 23;
            tblFooterText.StyleName = "footerTableStyle";
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = new XRTableCell();
                cell.Name = "FOOTER_NAME" + Convert.ToString(i + 1);
                    if (this.mReport.GetFooter(i) == "Kế toán trưởng") {
                        cell.Text = this.mFTSMain.SystemVars.GetSystemVars("CHIEF_ACCOUNTANT").ToString();
                    } else {
                        if (this.mReport.GetFooter(i) == "Thủ trưởng đơn vị") {
                            cell.Text = this.mFTSMain.SystemVars.GetSystemVars("DIRECTOR").ToString();
                        } else {
                            if (this.mReport.GetFooter(i) == "Thủ quỹ") {
                                cell.Text = this.mFTSMain.SystemVars.GetSystemVars("CASHIER").ToString();
                            } else {
                                if (this.mReport.GetFooter(i) == "Người lập biểu") {
                                    cell.Text = this.mFTSMain.UserInfo.UserName;
                                } else {
                                    cell.Text = this.mReport.Footer_name[i];
                                }
                            }
                        }
                    }

                rowFooterText.Cells.Add(cell);
                cell.TextAlignment = TextAlignment.MiddleCenter;
            }
            ((ISupportInitialize)(tblFooterText)).BeginInit();
            for (int i = 0; i < numcol; i++) {
                XRTableCell cell = (XRTableCell)rowFooterText.Cells["FOOTER_NAME" + Convert.ToString(i + 1)];
                cell.Left = Convert.ToInt32((this.mTotalPageWidth / numcol) * i);
                cell.Width = Convert.ToInt32((this.mTotalPageWidth / numcol));
            }
            ((ISupportInitialize)(tblFooterText)).EndInit();
        }

        private void RefreshPageFooter() {
            XRPageInfo lblPageNumber = (XRPageInfo) this.PageFooter.Controls["PAGE_NUMBER"];
            lblPageNumber.Left = this.mTotalPageWidth - 35;
            XRLabel lblPageNumberText = (XRLabel) this.PageFooter.Controls["PAGE"];
            lblPageNumberText.Width = this.mTotalPageWidth - 35;
        }

        private void CreatePageFooter() {
            this.PageFooter.Height = 0;
            XRPageInfo lblPageNumber = new XRPageInfo();
            lblPageNumber.Name = "PAGE_NUMBER";
            lblPageNumber.PageInfo = PageInfo.NumberOfTotal;
            this.PageFooter.Controls.Add(lblPageNumber);
            lblPageNumber.Left = this.mTotalPageWidth - 45;
            lblPageNumber.Top = 0;
            lblPageNumber.Width = 45;
            lblPageNumber.TextAlignment = TextAlignment.MiddleRight;
            XRLabel lblPageNumberText = new XRLabel();
            lblPageNumberText.Name = "PAGE";
            lblPageNumberText.Text = this.mFTSMain.MsgManager.GetMessage("MSG_PAGE");
            this.PageFooter.Controls.Add(lblPageNumberText);
            lblPageNumberText.Left = 0;
            lblPageNumberText.Top = 0;
            lblPageNumberText.Width = this.mTotalPageWidth - 45;
            lblPageNumberText.TextAlignment = TextAlignment.MiddleRight;
            this.SetSubtitleFont1(lblPageNumberText);
        }

        private void SetSort() {
            this.GroupHeader0.Visible = false;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Sort_Field[i] != string.Empty) {
                    GroupField groupfield = new GroupField(this.mReport.Sort_Field[i]);
                    if (this.mReport.Sort_Direction[i] == "DESC") {
                        groupfield.SortOrder = XRColumnSortOrder.Descending;
                    } else {
                        groupfield.SortOrder = XRColumnSortOrder.Ascending;
                    }
                    this.GroupHeader0.GroupFields.Add(groupfield);
                }
            }
        }

        private void RefreshHeader() {
            int starttop = 10;
            if (this.mReport.Num_File_Report > 1) {
                XRLabel lblPartNumberText = (XRLabel) this.PageHeader.Controls["PART"];
                lblPartNumberText.Width = this.mTotalPageWidth;
                starttop += lblPartNumberText.Bottom;
            }
            this.tblHeader.BeginInit();
            this.rowHeader.SuspendLayout();
            this.tblHeader.Top = starttop;
            this.tblHeader.Width = this.mTotalPageWidth;
            int totalwidth = 0;
            XRTableCell cellorder = null;
            if (this.mReport.Show_Order) {
                cellorder = (XRTableCell) this.rowHeader.Cells["ORDERHEADER"];
            }
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    totalwidth += ((XRTableCell) this.rowDetail.Cells[fieldname]).Width;
                }
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString().Trim();
                totalwidth += ((XRTableCell) this.rowDetail.Cells[fieldname]).Width;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth -= 9;
            }
            int startleft = 0;
            if (this.mReport.Show_Order) {
                cellorder.Left = startleft;
                cellorder.Width = ((XRTableCell) this.rowDetail.Cells["ORDERDETAIL"]).Width;
                startleft += cellorder.Width;
            }
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    XRTableCell cell = (XRTableCell) this.rowHeader.Cells[fieldname];
                    cell.Left = startleft;
                    int fw = ((XRTableCell) this.rowDetail.Cells[fieldname]).Width;
                    cell.Width = fw;
                    startleft += fw;
                }
            }
            for (int i = 0; i < dv.Count; i++) {
                DataRowView drv = dv[i];
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = (XRTableCell) this.rowHeader.Cells[fieldname];
                cell.Left = startleft;
                int fw = ((XRTableCell) this.rowDetail.Cells[fieldname]).Width;
                cell.Width = fw;
                startleft += fw;
            }
            if (this.mReport.ShowHeader0()) {
                XRTable header0table = (XRTable) this.PageHeader.Controls["HEADER0"];
                header0table.Top = starttop;
                header0table.Width = this.mTotalPageWidth;
                this.tblHeader.Top = header0table.Bottom;
                XRTableRow rowheadergroup = (XRTableRow) header0table.Rows[0];
                string lastgroupid = string.Empty;
                string col1 = string.Empty;
                string col2 = string.Empty;
                if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                    DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order",
                        DataViewRowState.CurrentRows);
                    col1 = dv1[0]["field_id"].ToString().Trim();
                    if (this.mRepeatCount > 1) {
                        col2 = dv1[1]["field_id"].ToString().Trim();
                    }
                }
                if (col1 != string.Empty && col2 != string.Empty) {
                    dv = new DataView(this.mReport.sys_reportfield.DataTable,
                        "Visible=1 and field_width>0 and (field_id='" + col1 + "' or field_id='" + col2 + "' or part=" + this.mReport.Part + ")", "Field_Order",
                        DataViewRowState.CurrentRows);
                } else {
                    if (col1 != string.Empty) {
                        dv = new DataView(this.mReport.sys_reportfield.DataTable,
                            "Visible=1 and field_width>0 and (field_id='" + col1 + "' or part=" + this.mReport.Part + ")", "Field_Order",
                            DataViewRowState.CurrentRows);
                    }
                }
                XRTableCell cellorder0 = null;
                if (this.mReport.Show_Order) {
                    cellorder0 = (XRTableCell) rowheadergroup.Cells["ORDERHEADER0"];
                }
                header0table.BeginInit();
                rowheadergroup.SuspendLayout();
                if (this.mReport.Show_Order) {
                    cellorder0.Left = cellorder.Left;
                    cellorder0.Width = cellorder.Width;
                }
                XRTableCell lastcell = null;
                XRTableCell lastdetailcell = null;
                lastgroupid = string.Empty;
                for (int i = 0; i < dv.Count; i++) {
                    DataRowView drv = dv[i];
                    string fieldname = drv["field_id"].ToString().Trim();
                    string groupid = drv["field_group_id"].ToString().Trim();
                    string groupname = string.Empty;
                    if (groupid != string.Empty) {
                        groupname = this.mFTSMain.ResourceManager.GetFieldGroupDisplayName(this.mReport.Report_ID, drv["FIELD_GROUP_ID"]);
                    }
                    if (groupid != lastgroupid || groupname == string.Empty) {
                        XRTableCell cell = (XRTableCell) rowheadergroup.Cells["HEADER0" + fieldname];
                        lastgroupid = groupid;
                        XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname];
                        cell.Left = detailcell.Left;
                        if (lastcell != null && lastdetailcell != null) {
                            lastcell.Width = detailcell.Left - lastdetailcell.Left;
                        }
                        if (i == dv.Count - 1) {
                            cell.Width = this.mTotalPageWidth - detailcell.Left;
                        }
                        lastcell = cell;
                        lastdetailcell = detailcell;
                    } else {
                        if (i == dv.Count - 1) {
                            XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname];
                            if (lastcell != null && lastdetailcell != null) {
                                lastcell.Width = this.mTotalPageWidth - lastdetailcell.Left;
                            }
                        }
                    }
                }
                rowheadergroup.ResumeLayout();
                header0table.EndInit();
                if (this.mReport.Show_Order) {
                    XRTableCell cell = (XRTableCell) rowheadergroup.Cells["ORDERHEADER0"];
                    XRTableCell detailcell = (XRTableCell) this.rowHeader.Cells["ORDERHEADER"];
                    if (detailcell != null) {
                        XRLabel lbl = (XRLabel) this.PageHeader.Controls["PAGEHEADER_LABLE_1"];
                        lbl.Left = cell.Left;
                        lbl.Width = cell.Width;
                    }
                }
                for (int i = 0; i < rowheadergroup.Cells.Count; i++) {
                    XRTableCell cell = (XRTableCell) rowheadergroup.Cells[i];
                    if (cell.Text.Trim() == string.Empty) {
                        XRTableCell detailcell = (XRTableCell) this.rowHeader.Cells[cell.Name.Substring(7)];
                        if (detailcell != null) {
                            XRLabel lbl = (XRLabel) this.PageHeader.Controls["PAGEHEADER_LABLE_1" + i.ToString()];
                            lbl.Left = cell.Left;
                            lbl.Width = cell.Width;
                        }
                    }
                }
            }
            this.rowHeader.ResumeLayout();
            this.tblHeader.EndInit();
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private void CreateHeader() {
            int starttop = 10;
            if (this.mReport.Num_File_Report > 1) {
                XRLabel lblPartNumberText = new XRLabel();
                lblPartNumberText.Name = "PART";
                lblPartNumberText.Text = this.mFTSMain.MsgManager.GetMessage("MSG_PART_TEXT") + " " + this.mReport.Part.ToString();
                this.PageHeader.Controls.Add(lblPartNumberText);
                lblPartNumberText.Left = 0;
                lblPartNumberText.Top = 0;
                lblPartNumberText.Width = this.mTotalPageWidth;
                lblPartNumberText.TextAlignment = TextAlignment.MiddleLeft;
                this.SetSubtitleFont1(lblPartNumberText);
                starttop += lblPartNumberText.Bottom;
            }
            this.tblHeader.Top = starttop;
            this.tblHeader.Left = 0;
            this.tblHeader.Width = this.mTotalPageWidth;
            if (this.mReport.Header_Height > 0) {
                this.tblHeader.Height = this.mReport.Header_Height;
            } else {
                this.tblHeader.Height = this.mFirstLevelHeight;
            }
            this.tblHeader.StyleName = "headerTableStyle";
            int totalwidth = 0;
            XRTableCell cellorder = null;
            if (this.mReport.Show_Order) {
                cellorder = new XRTableCell();
                this.rowHeader.Cells.Add(cellorder);
                cellorder.Name = "ORDERHEADER";
                cellorder.Text = this.mFTSMain.MsgManager.GetMessage("MSG_ORDER");
                this.FormatStringField(cellorder);
            }
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    XRTableCell cell = new XRTableCell();
                    this.rowHeader.Cells.Add(cell);
                    cell.Name = fieldname;
                    cell.Text = this.mFTSMain.ResourceManager.GetFieldDisplayName(this.mReport.Report_ID, dv1[i]["FIELD_ID"]);
                    cell.Angle = (int) dv1[i]["field_angle"];
                    totalwidth += (int) dv1[i]["field_width"];
                    cell.TextAlignment = TextAlignment.MiddleCenter;
                }
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = new XRTableCell();
                this.rowHeader.Cells.Add(cell);
                cell.Name = fieldname;
                cell.Angle = (int) drv["field_angle"];
                cell.Text = this.mFTSMain.ResourceManager.GetFieldDisplayName(this.mReport.Report_ID, drv["FIELD_ID"]);
                totalwidth += (int) drv["field_width"];
                cell.TextAlignment = TextAlignment.MiddleCenter;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                XRTableCell cell = new XRTableCell();
                this.rowHeader.Cells.Add(cell);
                cell.Name = "FONT_BOLD";
                cell.Text = string.Empty;
                cell.Visible = false;
                this.mTotalPageWidth -= 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                XRTableCell cell = new XRTableCell();
                this.rowHeader.Cells.Add(cell);
                cell.Name = "FONT_UNDERLINE";
                cell.Text = string.Empty;
                cell.Visible = false;
                this.mTotalPageWidth -= 9;
            }
            int startleft = 0;
            ((ISupportInitialize) (this.tblHeader)).BeginInit();
            if (this.mReport.Show_Order) {
                cellorder.Left = startleft;
                cellorder.Width = this.mOrderWidth;
                startleft += this.mOrderWidth;
            }
            if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order", DataViewRowState.CurrentRows);
                for (int i = 0; i < this.mRepeatCount; i++) {
                    string fieldname = dv1[i]["field_id"].ToString().Trim();
                    XRTableCell cell = (XRTableCell) this.rowHeader.Cells[fieldname];
                    cell.Left = startleft;
                    int fw = (int) dv1[i]["field_width"];
                    int fw1 = 0;
                    if (this.mReport.Show_Order) {
                        fw1 = Convert.ToInt32(((((float) this.mTotalPageWidth - this.mOrderWidth))/(float) totalwidth)*(float) fw);
                    } else {
                        fw1 = Convert.ToInt32((((float) this.mTotalPageWidth)/(float) totalwidth)*(float) fw);
                    }
                    cell.Angle = (int) dv1[i]["field_angle"];
                    cell.Width = fw1;
                    startleft += fw1;
                }
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                XRTableCell cell = (XRTableCell) this.rowHeader.Cells["FONT_BOLD"];
                cell.Visible = false;
                cell.Left = this.mTotalPageWidth;
                cell.Width = 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                XRTableCell cell = (XRTableCell) this.rowHeader.Cells["FONT_UNDERLINE"];
                cell.Visible = false;
                cell.Left = this.mTotalPageWidth;
                cell.Width = 9;
            }
            for (int i = 0; i < dv.Count; i++) {
                DataRowView drv = dv[i];
                string fieldname = drv["field_id"].ToString().Trim();
                XRTableCell cell = (XRTableCell) this.rowHeader.Cells[fieldname];
                cell.Left = startleft;
                int fw = (int) drv["field_width"];
                int fw1 = 0;
                if (this.mReport.Show_Order) {
                    fw1 = Convert.ToInt32(((((float) this.mTotalPageWidth - this.mOrderWidth))/(float) totalwidth)*(float) fw);
                } else {
                    fw1 = Convert.ToInt32(((float) this.mTotalPageWidth/(float) totalwidth)*(float) fw);
                }
                if (i == dv.Count - 1) {
                    fw1 = this.mTotalPageWidth - startleft;
                }
                cell.Angle = (int) drv["field_angle"];
                cell.Width = fw1;
                startleft += fw1;
            }
            ((ISupportInitialize) (this.tblHeader)).EndInit();
            this.ReportHeader.Height = this.tblHeader.Bottom;
            if (this.mReport.ShowHeader0()) {
                XRTable header0table = new XRTable();
                header0table.Name = "HEADER0";
                header0table.Top = starttop;
                header0table.Left = 0;
                header0table.Width = this.mTotalPageWidth;
                header0table.Height = this.mSecondLevelHeight;
                header0table.StyleName = "header0TableStyle";
                this.PageHeader.Controls.Add(header0table);
                this.tblHeader.Top = header0table.Bottom;
                XRTableRow rowheadergroup = new XRTableRow();
                header0table.Rows.Add(rowheadergroup);
                string lastgroupid = string.Empty;
                string col1 = string.Empty;
                string col2 = string.Empty;
                if (this.mReport.Part > 1 && this.mReport.Repeat_Column) {
                    DataView dv1 = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0", "Field_Order",
                        DataViewRowState.CurrentRows);
                    col1 = dv1[0]["field_id"].ToString().Trim();
                    if (this.mRepeatCount > 1) {
                        col2 = dv1[1]["field_id"].ToString().Trim();
                    }
                }
                if (col1 != string.Empty && col2 != string.Empty) {
                    dv = new DataView(this.mReport.sys_reportfield.DataTable,
                        "Visible=1 and field_width>0 and (field_id='" + col1 + "' or field_id='" + col2 + "' or part=" + this.mReport.Part + ")", "Field_Order",
                        DataViewRowState.CurrentRows);
                } else {
                    if (col1 != string.Empty) {
                        dv = new DataView(this.mReport.sys_reportfield.DataTable,
                            "Visible=1 and field_width>0 and (field_id='" + col1 + "' or part=" + this.mReport.Part + ")", "Field_Order",
                            DataViewRowState.CurrentRows);
                    }
                }
                XRTableCell cellorder0 = null;
                if (this.mReport.Show_Order) {
                    cellorder0 = new XRTableCell();
                    rowheadergroup.Cells.Add(cellorder0);
                    cellorder0.Name = "ORDERHEADER0";
                    cellorder0.Text = string.Empty;
                    this.FormatStringField(cellorder0);
                    // this.SetHeaderFont(cellorder0);
                }
                for (int i = 0; i < dv.Count; i++) {
                    DataRowView drv = dv[i];
                    string fieldname = drv["field_id"].ToString().Trim();
                    string groupid = drv["field_group_id"].ToString().Trim();
                    string groupname = string.Empty;
                    if (groupid != string.Empty) {
                        groupname = this.mFTSMain.ResourceManager.GetFieldGroupDisplayName(this.mReport.Report_ID, drv["FIELD_GROUP_ID"]);
                    }
                    if (groupid != lastgroupid || groupname == string.Empty) {
                        XRTableCell cell = new XRTableCell();
                        cell.Name = "HEADER0" + fieldname;
                        cell.Text = groupname;
                        rowheadergroup.Cells.Add(cell);
                        lastgroupid = groupid;
                        if (cell.Text.Trim() != string.Empty) {
                            if (i == 0 && !this.mReport.Show_Order) {
                                cell.Borders = BorderSide.All;
                            } else {
                                cell.Borders = (BorderSide.Bottom | BorderSide.Top | BorderSide.Right);
                            }
                        }
                        cell.TextAlignment = TextAlignment.MiddleCenter;
                        this.SetHeaderFont(cell);
                    }
                }
                ((ISupportInitialize) (header0table)).BeginInit();
                if (this.mReport.Show_Order) {
                    cellorder0.Left = cellorder.Left;
                    cellorder0.Width = cellorder.Width;
                }
                XRTableCell lastcell = null;
                XRTableCell lastdetailcell = null;
                lastgroupid = string.Empty;
                for (int i = 0; i < dv.Count; i++) {
                    DataRowView drv = dv[i];
                    string fieldname = drv["field_id"].ToString().Trim();
                    string groupid = drv["field_group_id"].ToString().Trim();
                    string groupname = string.Empty;
                    if (groupid != string.Empty) {
                        groupname = this.mFTSMain.ResourceManager.GetFieldGroupDisplayName(this.mReport.Report_ID, drv["FIELD_GROUP_ID"]);
                    }
                    if (groupid != lastgroupid || groupname == string.Empty) {
                        XRTableCell cell = (XRTableCell) rowheadergroup.Cells["HEADER0" + fieldname];
                        lastgroupid = groupid;
                        XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname];
                        cell.Left = detailcell.Left;
                        if (lastcell != null && lastdetailcell != null) {
                            lastcell.Width = detailcell.Left - lastdetailcell.Left;
                        }
                        if (i == dv.Count - 1) {
                            cell.Width = this.mTotalPageWidth - detailcell.Left;
                        }
                        lastcell = cell;
                        lastdetailcell = detailcell;
                    } else {
                        if (i == dv.Count - 1) {
                            XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname];
                            if (lastcell != null && lastdetailcell != null) {
                                lastcell.Width = this.mTotalPageWidth - lastdetailcell.Left;
                            }
                        }
                    }
                }
                ((ISupportInitialize) (header0table)).EndInit();
                //if (!this.mFTSMain.IsWeb && this.mReport.Excel == false) {
                //    if (this.mReport.Show_Order) {
                //        XRTableCell cell = (XRTableCell) rowheadergroup.Cells["ORDERHEADER0"];
                //        XRTableCell detailcell = (XRTableCell) this.rowHeader.Cells["ORDERHEADER"];
                //        if (detailcell != null) {
                //            XRLabel lbl = new XRLabel();
                //            lbl.Name = "PAGEHEADER_LABLE_1";
                //            lbl.Text = detailcell.Text;
                //            detailcell.Text = string.Empty;
                //            lbl.Left = cell.Left;
                //            if (this.mReport.Num_File_Report > 1) {
                //                lbl.Top = cell.Bottom + 15;
                //                lbl.Height = detailcell.Height - 8;
                //            } else {
                //                lbl.Top = cell.Bottom - 8;
                //                lbl.Height = detailcell.Height + 8;
                //            }
                //            lbl.Width = cell.Width;
                //            this.PageHeader.Controls.Add(lbl);
                //            this.SetHeaderFont(lbl);
                //            lbl.TextAlignment = TextAlignment.TopCenter;
                //        }
                //    }
                //    for (int i = 0; i < rowheadergroup.Cells.Count; i++) {
                //        XRTableCell cell = (XRTableCell) rowheadergroup.Cells[i];
                //        if (cell.Text.Trim() == string.Empty) {
                //            XRTableCell detailcell = (XRTableCell) this.rowHeader.Cells[cell.Name.Substring(7)];
                //            if (detailcell != null) {
                //                XRLabel lbl = new XRLabel();
                //                lbl.Name = "PAGEHEADER_LABLE_1" + i.ToString();
                //                lbl.Text = detailcell.Text;
                //                detailcell.Text = string.Empty;
                //                lbl.Left = cell.Left;
                //                if (this.mReport.Num_File_Report > 1) {
                //                    lbl.Top = cell.Bottom + 15;
                //                    lbl.Height = detailcell.Height - 8;
                //                } else {
                //                    lbl.Top = cell.Bottom - 8;
                //                    lbl.Height = detailcell.Height + 8;
                //                }
                //                lbl.Width = cell.Width;
                //                this.PageHeader.Controls.Add(lbl);
                //                this.SetHeaderFont(lbl);
                //                lbl.TextAlignment = TextAlignment.TopCenter;
                //            }
                //        }
                //    }
                //}
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                this.mTotalPageWidth += 9;
            }
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                this.mTotalPageWidth += 9;
            }
        }

        private void HideAfterFirstPage() {
            if (this.mReport.Part != 1) {
                foreach (XRControl c in this.ReportHeader.Controls) {
                    c.ForeColor = Color.White;
                }
            }
            if (this.mReport.Part != this.mReport.Num_File_Report) {
                foreach (XRControl c in this.ReportFooter.Controls) {
                    c.ForeColor = Color.White;
                }
                XRTable tbl = (XRTable) this.ReportFooter.Controls["FooterText"];
                if (tbl != null) {
                    foreach (XRTableRow r in tbl.Rows) {
                        foreach (XRTableCell c in r.Cells) {
                            c.ForeColor = Color.White;
                        }
                    }
                }
                XRTable tbl1 = (XRTable) this.ReportFooter.Controls["FooterNameText"];
                if (tbl1 != null) {
                    foreach (XRTableRow r in tbl1.Rows) {
                        foreach (XRTableCell c in r.Cells) {
                            c.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private void SetOthers() {}

        private void cellorder_BeforePrint(object sender, PrintEventArgs e) {
            XRTableCell cell = (XRTableCell) sender;
            if (cell.Name == "ORDERDETAIL") {
                cell.Text = this.orderdetail.ToString();
                this.orderdetail++;
            }
            if (cell.Name == "ORDERGROUP1") {
                cell.Text = this.group1detail.ToString();
                this.group1detail++;
            }
            if (cell.Name == "ORDERGROUP2") {
                cell.Text = this.group2detail.ToString();
                this.group2detail++;
            }
            if (cell.Name == "ORDERGROUP3") {
                cell.Text = this.group3detail.ToString();
                this.group3detail++;
            }
            if (cell.Name == "ORDERGROUP4") {
                cell.Text = this.group4detail.ToString();
                this.group4detail++;
            }
            if (cell.Name == "ORDERGROUP5") {
                cell.Text = this.group5detail.ToString();
                this.group5detail++;
            }
        }

        private void RefreshMoveGroupField() {
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Field[i].Trim() != string.Empty) {
                    this.RefreshMoveGroupField(i);
                }
            }
        }

        private void MoveGroupField() {
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Field[i].Trim() != string.Empty) {
                    this.MoveGroupField(i);
                }
            }
        }

        private void RefreshMoveGroupField(int group) {
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            switch (group) {
                case 0:
                    tblGroup = this.tblGroup5;
                    rowGroup = this.rowGroup5;
                    break;
                case 1:
                    tblGroup = this.tblGroup4;
                    rowGroup = this.rowGroup4;
                    break;
                case 2:
                    tblGroup = this.tblGroup3;
                    rowGroup = this.rowGroup3;
                    break;
                case 3:
                    tblGroup = this.tblGroup2;
                    rowGroup = this.rowGroup2;
                    break;
                case 4:
                    tblGroup = this.tblGroup1;
                    rowGroup = this.rowGroup1;
                    break;
                default:
                    break;
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            if (group == 4) {
                firstsumfield = this.GetFirstSumFieldLastGroup();
            }
            string firstfield = this.GetFirstField();
            string fieldname = this.mReport.Group_Field[group];
            XRTableCell cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + fieldname];
            XRTableCell cellname = (XRTableCell) rowGroup.Cells["GROUP_NAME" + Convert.ToString(group + 1)];
            if (cellid == null) {
                return;
            }
            XRTableCell lastgroupcell = this.GetLastGroupCell();
            if (lastgroupcell == null) {
                return;
            }
            XRTableCell celldetail1 = (XRTableCell) this.rowDetail.Cells[fieldname];
            if (celldetail1 == null) {
                return;
            } else {
                if (celldetail1.Left > lastgroupcell.Left) {
                    return;
                }
            }
            if (lastgroupcell.Name == firstfield) {
                return;
            }
            tblGroup.BeginInit();
            rowGroup.SuspendLayout();
            if (cellname != null) {
                int oldcellnameleft = cellname.Left;
                cellname.Left = lastgroupcell.Right;
                cellname.Width = cellname.Width - (lastgroupcell.Right - oldcellnameleft);
                if (cellname.Width - (lastgroupcell.Right - oldcellnameleft) <= 5) {
                    rowGroup.Cells.Remove(cellname);
                }
            }
            foreach (DataRowView drv in dv) {
                string fieldname1 = drv["field_id"].ToString();
                if (this.IsGroup(fieldname1)) {
                    XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname1];
                    if (fieldname == fieldname1) {
                        cellid.Left = detailcell.Left;
                        cellid.Width = detailcell.Width;
                    } else {
                        XRTableCell groupcell = (XRTableCell) rowGroup.Cells["GROUP" + Convert.ToString(5 - group) + fieldname1];
                        groupcell.Left = detailcell.Left;
                        groupcell.Width = detailcell.Width;
                    }
                } else {
                    break;
                }
            }
            rowGroup.ResumeLayout();
            tblGroup.EndInit();
        }

        private void MoveGroupField(int group) {
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            GroupHeaderBand groupheader = null;
            switch (group) {
                case 0:
                    groupheader = this.GroupHeader5;
                    tblGroup = this.tblGroup5;
                    rowGroup = this.rowGroup5;
                    break;
                case 1:
                    groupheader = this.GroupHeader4;
                    tblGroup = this.tblGroup4;
                    rowGroup = this.rowGroup4;
                    break;
                case 2:
                    groupheader = this.GroupHeader3;
                    tblGroup = this.tblGroup3;
                    rowGroup = this.rowGroup3;
                    break;
                case 3:
                    groupheader = this.GroupHeader2;
                    tblGroup = this.tblGroup2;
                    rowGroup = this.rowGroup2;
                    break;
                case 4:
                    groupheader = this.GroupHeader1;
                    tblGroup = this.tblGroup1;
                    rowGroup = this.rowGroup1;
                    break;
                default:
                    break;
            }
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            string firstsumfield = this.GetFirstSumField();
            if (group == 4) {
                firstsumfield = this.GetFirstSumFieldLastGroup();
            }
            string firstfield = this.GetFirstField();
            string fieldname = this.mReport.Group_Field[group];
            XRTableCell cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + fieldname];
            XRTableCell cellname = (XRTableCell) rowGroup.Cells["GROUP_NAME" + Convert.ToString(group + 1)];
            if (cellid == null) {
                return;
            }
            XRTableCell lastgroupcell = this.GetLastGroupCell();
            if (lastgroupcell == null) {
                return;
            }
            XRTableCell celldetail1 = (XRTableCell) this.rowDetail.Cells[fieldname];
            if (celldetail1 == null) {
                return;
            } else {
                if (celldetail1.Left > lastgroupcell.Left) {
                    return;
                }
            }
            if (lastgroupcell.Name == firstfield) {
                return;
            }
            ((ISupportInitialize) (tblGroup)).BeginInit();
            if (cellname != null) {
                int oldcellnameleft = cellname.Left;
                cellname.Left = lastgroupcell.Right;
                cellname.Width = cellname.Width - (lastgroupcell.Right - oldcellnameleft);
                if (cellname.Width - (lastgroupcell.Right - oldcellnameleft) <= 5) {
                    rowGroup.Cells.Remove(cellname);
                }
            }
            int idx = 0;
            foreach (DataRowView drv in dv) {
                string fieldname1 = drv["field_id"].ToString();
                if (this.IsGroup(fieldname1)) {
                    XRTableCell detailcell = (XRTableCell) this.rowDetail.Cells[fieldname1];
                    if (fieldname == fieldname1) {
                        cellid.Left = detailcell.Left;
                        cellid.Width = detailcell.Width;
                    } else {
                        XRTableCell groupcell = new XRTableCell();
                        groupcell.Name = "GROUP" + Convert.ToString(5 - group) + fieldname1;
                        groupcell.Text = string.Empty;
                        rowGroup.Cells.Add(groupcell);
                        if (this.mReport.Show_Order) {
                            rowGroup.Cells.SetChildIndex(groupcell, idx + 1);
                        } else {
                            rowGroup.Cells.SetChildIndex(groupcell, idx);
                        }
                        groupcell.Left = detailcell.Left;
                        groupcell.Width = detailcell.Width;
                    }
                } else {
                    break;
                }
                idx++;
            }
            ((ISupportInitialize) (tblGroup)).EndInit();
        }

        private XRTableCell GetLastGroupCell() {
            XRTableCell lastcell = null;
            DataView dv = new DataView(this.mReport.sys_reportfield.DataTable, "Visible=1 and field_width>0 and part=" + this.mReport.Part, "Field_Order",
                DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv) {
                string fieldname = drv["field_id"].ToString();
                if (!this.IsGroup(fieldname)) {
                    return lastcell;
                }
                lastcell = (XRTableCell) this.rowDetail.Cells[fieldname];
            }
            return null;
        }

        private bool IsGroup(string fieldname) {
            for (int i = 0; i < 5; i++) {
                if (fieldname == this.mReport.Group_Field[i]) {
                    return true;
                }
            }
            return false;
        }

        private void FormatCellID(XRTableCell cell, string fieldname) {
            DataRow foundrow = this.mReport.sys_reportfield.GetRow(fieldname);
            if (foundrow != null) {
                switch (foundrow["field_type"].ToString()) {
                    case "NUMBER":
                        this.FormatNumberField(cell, (int) foundrow["decimal_digit"]);
                        break;
                    case "DATE":
                        this.FormatDateField(cell);
                        break;
                    case "STRING":
                        this.FormatStringField(cell);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ClearData() {
            Functions.ClearDataSet(this.mReport.DataSet);
            Functions.ClearDataSet(this.mDataSetTmp);
        }

        private string ConvertToString(object number) {
            return this.ConvertToStringBase(number, this.mFTSMain.SystemVars.GetSystemVars("DECIMAL_SYMBOL").ToString(), this.mFTSMain.SystemVars.GetSystemVars("THOUSAND_SYMBOL").ToString());
        }

        private void RefreshCustomFieldsAfter() {
            string filename = Functions.GetPathName() + "Reports\\" + this.mReport.Report_ID.ToUpper() + ".XML";
            if (!Functions.FileExists(filename)) {
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof (CustomFieldCollection));
            FileStream fs = new FileStream(filename, FileMode.Open);
            CustomFieldCollection customfields = (CustomFieldCollection) serializer.Deserialize(fs);
            CustomField[] items = customfields.CustomFields;
            foreach (CustomField field in items) {
                if (field.beforeafter.ToUpper() == "AFTER") {
                    this.RefreshCustomField(field);
                }
            }
            fs.Close();
        }

        private void CreateCustomFieldsAfter() {
            string filename = Functions.GetPathName() + "Reports\\" + this.mReport.Report_ID.ToUpper() + ".XML";
            if (!Functions.FileExists(filename)) {
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof (CustomFieldCollection));
            FileStream fs = new FileStream(filename, FileMode.Open);
            CustomFieldCollection customfields = (CustomFieldCollection) serializer.Deserialize(fs);
            CustomField[] items = customfields.CustomFields;
            foreach (CustomField field in items) {
                if (field.beforeafter.ToUpper() == "AFTER") {
                    this.CreateCustomField(field);
                }
            }
            fs.Close();
        }

        private void RefreshCustomFieldsBefore() {
            string filename = Functions.GetPathName() + "Reports\\" + this.mReport.Report_ID.ToUpper() + ".XML";
            if (!Functions.FileExists(filename)) {
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof (CustomFieldCollection));
            FileStream fs = new FileStream(filename, FileMode.Open);
            CustomFieldCollection customfields = (CustomFieldCollection) serializer.Deserialize(fs);
            CustomField[] items = customfields.CustomFields;
            foreach (CustomField field in items) {
                if (field.beforeafter.ToUpper() == "BEFORE") {
                    this.RefreshCustomField(field);
                }
            }
            fs.Close();
        }

        private void CreateCustomFieldsBefore() {
            string filename = Functions.GetPathName() + "Reports\\" + this.mReport.Report_ID.ToUpper() + ".XML";
            if (!Functions.FileExists(filename)) {
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof (CustomFieldCollection));
            FileStream fs = new FileStream(filename, FileMode.Open);
            CustomFieldCollection customfields = (CustomFieldCollection) serializer.Deserialize(fs);
            CustomField[] items = customfields.CustomFields;
            foreach (CustomField field in items) {
                if (field.beforeafter.ToUpper() == "BEFORE") {
                    this.CreateCustomField(field);
                }
            }
            fs.Close();
        }

        private void RefreshCustomField(CustomField field) {
            XRLabel lbl = null;
            switch (field.Section.ToUpper()) {
                case "REPORTHEADER":
                    lbl = (XRLabel) this.ReportHeader.Controls[field.Name];
                    break;
                case "REPORTFOOTER":
                    lbl = (XRLabel) this.ReportFooter.Controls[field.Name];
                    break;
                case "PAGEHEADER":
                    lbl = (XRLabel) this.PageHeader.Controls[field.Name];
                    break;
                case "PAGEFOOTER":
                    lbl = (XRLabel) this.PageFooter.Controls[field.Name];
                    break;
                default:
                    return;
            }
            if (lbl != null) {
                if (field.width == -1) {
                    lbl.Width = this.mTotalPageWidth;
                } else {
                    lbl.Width = field.width;
                }
            }
        }

        private void CreateCustomField(CustomField field) {
            int starttop = this.ReportHeader.Height;
            if (field.Section.ToUpper() == "REPORTFOOTER") {
                starttop = this.ReportFooter.Height;
            }
            if (field.Section.ToUpper() == "PAGEFOOTER") {
                starttop = this.PageFooter.Height;
            }
            if (field.Section.ToUpper() == "PAGEHEADER") {
                starttop = this.PageHeader.Height;
            }
            XRLabel lbl = new XRLabel();
            lbl.Name = field.Name;
            if (field.top != -1) {
                lbl.Top = field.top;
            } else {
                lbl.Top = starttop;
            }
            lbl.Left = field.left;
            if (field.width == -1) {
                lbl.Width = this.mTotalPageWidth;
            } else {
                lbl.Width = field.width;
            }
            lbl.Text = field.Text + " " + this.GetPara(field.Para);
            switch (field.Section.ToUpper()) {
                case "REPORTHEADER":
                    this.ReportHeader.Controls.Add(lbl);
                    break;
                case "REPORTFOOTER":
                    this.ReportFooter.Controls.Add(lbl);
                    break;
                case "PAGEHEADER":
                    this.PageHeader.Controls.Add(lbl);
                    break;
                case "PAGEFOOTER":
                    this.PageFooter.Controls.Add(lbl);
                    break;
                default:
                    return;
            }
            lbl.ForeColor = Color.FromName(field.font_color);
            lbl.BackColor = Color.White;
            lbl.BringToFront();
            switch (field.font_style.ToUpper()) {
                case "BOLD":
                    lbl.Font = new Font(field.font_name, field.font_size, FontStyle.Bold);
                    break;
                case "BOLDITALIC":
                    lbl.Font = new Font(field.font_name, field.font_size, FontStyle.Bold);
                    break;
                case "ITALIC":
                    lbl.Font = new Font(field.font_name, field.font_size, FontStyle.Italic);
                    break;
                default:
                    lbl.Font = new Font(field.font_name, field.font_size, FontStyle.Regular);
                    break;
            }
            switch (field.textalignment) {
                case "CENTER":
                    lbl.TextAlignment = TextAlignment.MiddleCenter;
                    break;
                case "RIGHT":
                    lbl.TextAlignment = TextAlignment.MiddleRight;
                    break;
                default:
                    lbl.TextAlignment = TextAlignment.MiddleLeft;
                    break;
            }
            if (field.box == 1) {
                lbl.Borders = BorderSide.All;
            }
        }

        private string GetPara(string para) {
            switch (para.ToUpper()) {
                case "TEN_DON_VI":
                    return this.mFTSMain.SystemVars.GetSystemVars("COMPANY_NAME").ToString().Trim();
                case "DIA_CHI_FULL":
                    return this.mFTSMain.SystemVars.GetSystemVars("FULL_ADDRESS").ToString();
                case "MA_SO_THUE":
                    return this.mFTSMain.SystemVars.GetSystemVars("TAX_FILE_NUMBER").ToString().Trim();
                case "DIA_CHI":
                    return this.mFTSMain.SystemVars.GetSystemVars("ADDRESS").ToString().Trim();
                default:
                    return this.mReport.GetPara(para);
            }
        }

        private void cell_BeforePrint2(object sender, PrintEventArgs e) {
            XRTableCell cell = (XRTableCell) sender;
            cell.Tag = this.GetCurrentRow();
        }

        private void cell_BeforePrint1(object sender, PrintEventArgs e) {
            XRTableCell cell = (XRTableCell) sender;
            cell.Tag = this.GetCurrentRow();
            try {
                decimal x = 0;
                if (cell.Text != string.Empty) {
                    x = Convert.ToDecimal(cell.Text);
                }
                if (cell.Text != string.Empty && x == 0) {
                    cell.Text = string.Empty;
                } else {
                    if (cell.Text != string.Empty && x < 0 && this.mReport.Excel == false) {
                        cell.Text = "(" + cell.Text.Replace("-", string.Empty) + ")";
                    }
                }
            } catch (Exception) {}
        }

        private void cell_BeforePrint(object sender, TextFormatEventArgs e) {
            XRTableCell cell = (XRTableCell) sender;
            cell.Tag = this.GetCurrentRow();
            try {
                decimal x = Convert.ToDecimal(e.Value);
                if (x == 0) {
                    e.Text = string.Empty;
                } else {
                    if (x < 0 && this.mReport.Excel == false) {
                        e.Text = "(" + e.Text.Replace("-", string.Empty) + ")";
                    }
                }
            } catch (Exception) {}
        }

        private void DetailBeforePrint(object sender, PrintEventArgs e) {
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_BOLD") >= 0) {
                if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                    XRTableCell cell = (XRTableCell) this.rowDetail.Cells["FONT_BOLD"];
                    XRTableCell cell1 = (XRTableCell) this.rowDetail.Cells["FONT_UNDERLINE"];
                    if (cell.Text == "1") {
                        if (cell1.Text == "1") {
                            this.rowDetail.StyleName = "detailTableStyleBoldUnderline";
                        } else {
                            this.rowDetail.StyleName = "detailTableStyleBold";
                        }
                    } else {
                        if (cell1.Text == "1") {
                            this.rowDetail.StyleName = "detailTableStyleUnderline";
                        } else {
                            this.rowDetail.StyleName = "detailTableStyle";
                        }
                    }
                } else {
                    XRTableCell cell = (XRTableCell) this.rowDetail.Cells["FONT_BOLD"];
                    if (cell.Text == "1") {
                        this.rowDetail.StyleName = "detailTableStyleBold";
                    } else {
                        this.rowDetail.StyleName = "detailTableStyle";
                    }
                }
            } else {
                if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("FONT_UNDERLINE") >= 0) {
                    XRTableCell cell = (XRTableCell) this.rowDetail.Cells["FONT_UNDERLINE"];
                    if (cell.Text == "1") {
                        this.rowDetail.StyleName = "detailTableStyleUnderline";
                    } else {
                        this.rowDetail.StyleName = "detailTableStyle";
                    }
                }
            }
        }

        private void DeleteInvisible() {
            if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Columns.IndexOf("VISIBLE") >= 0 && this.mReport.Formula) {
                List<DataRow> deletelist = new List<DataRow>();
                foreach (DataRow row in this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Rows) {
                    if (row.RowState != DataRowState.Deleted && (Int16) row["visible"] == 0) {
                        deletelist.Add(row);
                    }
                }
                foreach (DataRow row in deletelist) {
                    row.Delete();
                }
                this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].AcceptChanges();
            }
            

                
        }

        private void ChangeGroup() {
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Group_Field[i] != string.Empty && this.mReport.Group_Table[i] == string.Empty) {
                    this.ChangeGroup(i);
                }
            }
        }

        private void ChangeGroup(int group) {
            XRTable tblGroup = null;
            XRTableRow rowGroup = null;
            GroupHeaderBand groupheader = null;
            switch (group) {
                case 0:
                    groupheader = this.GroupHeader5;
                    tblGroup = this.tblGroup5;
                    rowGroup = this.rowGroup5;
                    break;
                case 1:
                    groupheader = this.GroupHeader4;
                    tblGroup = this.tblGroup4;
                    rowGroup = this.rowGroup4;
                    break;
                case 2:
                    groupheader = this.GroupHeader3;
                    tblGroup = this.tblGroup3;
                    rowGroup = this.rowGroup3;
                    break;
                case 3:
                    groupheader = this.GroupHeader2;
                    tblGroup = this.tblGroup2;
                    rowGroup = this.rowGroup2;
                    break;
                case 4:
                    groupheader = this.GroupHeader1;
                    tblGroup = this.tblGroup1;
                    rowGroup = this.rowGroup1;
                    break;
                default:
                    break;
            }
            string firstsumfield = this.GetFirstSumField();
            if (group == 4) {
                firstsumfield = this.GetFirstSumFieldLastGroup();
            }
            if (this.mReport.Part == 1) {
                string fieldname = this.mReport.Group_Field[group];
                XRTableCell cellid = null;
                XRTableCell cellname = null;
                try {
                    cellid = (XRTableCell) rowGroup.Cells["GROUP_ID" + Convert.ToString(5 - group) + fieldname];
                    cellname = (XRTableCell) rowGroup.Cells["GROUP_NAME" + Convert.ToString(group + 1)];
                } catch (Exception) {}
                ((ISupportInitialize) (tblGroup)).BeginInit();
                if (cellid != null) {
                    if (cellname != null) {
                        if (Math.Abs(cellname.Left - cellid.Right) < 4) {
                            int width = cellid.Width + cellname.Width;
                            rowGroup.Cells.Remove(cellname);
                            cellid.Width = width;
                        }
                    }
                }
                ((ISupportInitialize) (tblGroup)).EndInit();
            }
        }

        private void logobox_BeforePrint(object sender, PrintEventArgs e) {
            string filename = Functions.GetPathName() + "Graphics\\logo.jpg";
            ((XRPictureBox) sender).Image = new Bitmap(filename);
            ((XRPictureBox) sender).Sizing = ImageSizeMode.StretchImage;
        }

        private void DetailCellPreviewDoubleClick(object sender, PreviewMouseEventArgs e) {
            try {
                this.mReport.DrillDown(((DataRowView) e.Brick.Value).Row);
            } catch (Exception ex) {
                this.mFTSMain.ExceptionManager.ProcessException(ex);
            }
        }

        private void DetailCellPreviewDoubleClick1(object sender, PreviewMouseEventArgs e) {
            try {
                DataTable dt = ((DataRowView) e.Brick.Value).Row.Table;
            this.mReport.DrillDown(((DataRowView) e.Brick.Value).Row);
            } catch (Exception ex) {
                this.mFTSMain.ExceptionManager.ProcessException(ex);
            }
        }

        private Hashtable mHs1 = new Hashtable();
        private Hashtable mHs2 = new Hashtable();
        private Hashtable mHs3 = new Hashtable();
        private Hashtable mHs4 = new Hashtable();
        private Hashtable mHs5 = new Hashtable();

        private void cell_SummaryReset(object sender, EventArgs e) {
            //string fieldname = ((XRTableCell)sender).Tag.ToString();
            //cell.Name = "GROUP" + Convert.ToString(5 - group) + fieldname1;

            XRTableCell cell = ((XRTableCell) sender);
            if (cell.DataBindings.Count > 0) {
                string fieldname = cell.DataBindings[0].DataMember;
                Hashtable mHs = null;
                if (cell.Name == "GROUP1" + fieldname) {
                    mHs = this.mHs1;
                }
                if (cell.Name == "GROUP2" + fieldname) {
                    mHs = this.mHs2;
                }
                if (cell.Name == "GROUP3" + fieldname) {
                    mHs = this.mHs3;
                }
                if (cell.Name == "GROUP4" + fieldname) {
                    mHs = this.mHs4;
                }
                if (cell.Name == "GROUP5" + fieldname) {
                    mHs = this.mHs5;
                }
                if (mHs != null) {
                    decimal result = 0;
                    if (mHs[fieldname] == null) {
                        mHs.Add(fieldname, result);
                    } else {
                        mHs.Remove(fieldname);
                        mHs.Add(fieldname, result);
                    }
                }
            }
        }

        private void cell_SummaryRowChanged(object sender, EventArgs e) {
            XRTableCell cell = ((XRTableCell) sender);
            if (cell.DataBindings.Count > 0) {
                string fieldname = cell.DataBindings[0].DataMember;
                Hashtable mHs = null;
                if (cell.Name == "GROUP1" + fieldname) {
                    mHs = this.mHs1;
                }
                if (cell.Name == "GROUP2" + fieldname) {
                    mHs = this.mHs2;
                }
                if (cell.Name == "GROUP3" + fieldname) {
                    mHs = this.mHs3;
                }
                if (cell.Name == "GROUP4" + fieldname) {
                    mHs = this.mHs4;
                }
                if (cell.Name == "GROUP5" + fieldname) {
                    mHs = this.mHs5;
                }
                if (mHs != null) {
                    object oj = this.GetCurrentColumnValue("IS_SUMMARY");
                    if (oj == null || oj == System.DBNull.Value) {
                        object oj1 = this.GetCurrentColumnValue(fieldname);
                        decimal result = 0;
                        if (oj1 != null && oj1 != System.DBNull.Value) {
                            result = Convert.ToDecimal(oj1);
                        }
                        if (mHs[fieldname] == null) {
                            mHs.Add(fieldname, result);
                        } else {
                            result = (decimal) mHs[fieldname] + result;
                            mHs.Remove(fieldname);
                            mHs.Add(fieldname, result);
                        }
                    } else {
                        if (Convert.ToInt16(oj) == 1) {
                            object oj1 = this.GetCurrentColumnValue(fieldname);
                            decimal result = 0;
                            if (oj1 != null && oj1 != System.DBNull.Value) {
                                result = Convert.ToDecimal(oj1);
                            }
                            if (mHs[fieldname] == null) {
                                mHs.Add(fieldname, result);
                            } else {
                                result = (decimal) mHs[fieldname] + result;
                                mHs.Remove(fieldname);
                                mHs.Add(fieldname, result);
                            }
                        }
                    }
                }
            }
        }

        private void cell_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            XRTableCell cell = ((XRTableCell) sender);
            if (cell.DataBindings.Count > 0) {
                string fieldname = cell.DataBindings[0].DataMember;
                Hashtable mHs = null;
                if (cell.Name == "GROUP1" + fieldname) {
                    mHs = this.mHs1;
                }
                if (cell.Name == "GROUP2" + fieldname) {
                    mHs = this.mHs2;
                }
                if (cell.Name == "GROUP3" + fieldname) {
                    mHs = this.mHs3;
                }
                if (cell.Name == "GROUP4" + fieldname) {
                    mHs = this.mHs4;
                }
                if (cell.Name == "GROUP5" + fieldname) {
                    mHs = this.mHs5;
                }
                if (mHs != null) {
                    decimal result = 0;
                    if (mHs[fieldname] == null) {
                        e.Result = result;
                    } else {
                        e.Result = mHs[fieldname];
                    }
                    e.Handled = true;
                }
            }
        }

        private void Detail_AfterPrint(object sender, EventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.Location = new Point(0, this.rowDetail.Height - 1);
            (sender as DetailBand).Controls.Add(line);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowDetail.Height - 1);
            (sender as DetailBand).Controls.Add(line);
        }

        private void GroupHeader1_BeforePrint(object sender, PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowGroup1.Height - 1);
            (sender as GroupBand).Controls.Add(line);
        }

        private void GroupHeader2_BeforePrint(object sender, PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowGroup2.Height - 1);
            (sender as GroupBand).Controls.Add(line);
        }

        private void GroupHeader3_BeforePrint(object sender, PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowGroup3.Height - 1);
            (sender as GroupBand).Controls.Add(line);
        }

        private void GroupHeader4_BeforePrint(object sender, PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowGroup4.Height - 1);
            (sender as GroupBand).Controls.Add(line);
        }

        private void GroupHeader5_BeforePrint(object sender, PrintEventArgs e) {
            XRLine line = new XRLine() {LineWidth = 1, SizeF = new SizeF(this.PageWidth - this.Margins.Left - this.Margins.Right, 1)};
            line.LineStyle = DashStyle.Dot;
            line.AnchorVertical = VerticalAnchorStyles.Bottom;
            line.Location = new Point(0, this.rowGroup5.Height - 1);
            (sender as GroupBand).Controls.Add(line);
        }

        private void CreateReportHeaderExcel() {
            //int topmost = 0;

            //XRLabel lblParent_Company = new XRLabel();
            //lblParent_Company.Name = "PARENT_COMPANY_NAME";
            //lblParent_Company.Text = this.mFTSMain.SystemVars.GetSystemVars("PARENT_COMPANY_NAME").ToString();
            //if (lblParent_Company.Text != string.Empty) {
            //    this.ReportHeader.Controls.Add(lblParent_Company);
            //    lblParent_Company.Left = 0;
            //    lblParent_Company.Top = topmost;
            //    lblParent_Company.Width = 450;
            //    lblParent_Company.Height = this.mLabelHeight;
            //    topmost += this.mLabelHeight;
            //    lblParent_Company.TextAlignment = TextAlignment.MiddleCenter;
            //    lblParent_Company.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size,
            //        FontStyle.Bold);
            //}
            //XRLabel lblCompany_Name = new XRLabel();
            //lblCompany_Name.Name = "COMPANY_NAME";
            //lblCompany_Name.Text = this.mFTSMain.SystemVars.GetSystemVars("COMPANY_NAME").ToString();
            //this.ReportHeader.Controls.Add(lblCompany_Name);
            //lblCompany_Name.Left = 0;
            //lblCompany_Name.Top = topmost;
            //lblCompany_Name.Width = 450;
            //lblCompany_Name.Height = this.mLabelHeight;
            //if (lblParent_Company.Text != string.Empty || (bool)this.mFTSMain.SystemVars.GetSystemVars("SHOW_ADDRESS_ON_REPORT")) {
            //    lblCompany_Name.TextAlignment = TextAlignment.MiddleCenter;
            //} else {
            //    lblCompany_Name.TextAlignment = TextAlignment.MiddleLeft;
            //}
            //int incfont = 0;
            //lblCompany_Name.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size + incfont,
            //    FontStyle.Bold);
            //XRLabel lblCompany_Address = null;
            //if ((bool)this.mFTSMain.SystemVars.GetSystemVars("SHOW_ADDRESS_ON_REPORT")) {
            //    lblCompany_Address = new XRLabel();
            //    lblCompany_Address.Name = "COMPANY_ADDRESS";
            //    lblCompany_Address.Text = this.mFTSMain.SystemVars.GetSystemVars("COMPANY_ADDRESS").ToString();
            //    this.ReportHeader.Controls.Add(lblCompany_Address);
            //    lblCompany_Address.Left = 0;
            //    lblCompany_Address.Top = lblCompany_Name.Bottom;
            //    lblCompany_Address.Width = 450;
            //    lblCompany_Address.Height = this.mLabelHeight;
            //    lblCompany_Address.TextAlignment = TextAlignment.MiddleCenter;
            //    incfont = 0;
            //    lblCompany_Name.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size + incfont, FontStyle.Regular);

            //}
            //string filename = Functions.GetPathName() + "Graphics\\logo.jpg";
            //XRPictureBox logobox = new XRPictureBox();
            //if (Functions.FileExists(filename)) {
            //    logobox.Sizing = ImageSizeMode.StretchImage;
            //    logobox.Name = "LOGO";
            //    logobox.Width = (int)this.mFTSMain.SystemVars.GetSystemVars("LOGO_WIDTH") * 5;
            //    logobox.Height = (int)this.mFTSMain.SystemVars.GetSystemVars("LOGO_HEIGHT") * 4;
            //    logobox.Left = 150;
            //    if (lblCompany_Address == null) {
            //        logobox.Top = lblCompany_Name.Bottom;
            //    } else {
            //        logobox.Top = lblCompany_Address.Bottom;
            //    }
            //    this.ReportHeader.Controls.Add(logobox);
            //    logobox.BeforePrint += new PrintEventHandler(this.logobox_BeforePrint);
            //}

            int starttop = 0;
            XRLabel lblReport_Name = new XRLabel();
            lblReport_Name.Name = "REPORT_NAME";
            lblReport_Name.Text = this.mReport.Report_Name.ToUpper();
            this.ReportHeader.Controls.Add(lblReport_Name);
            lblReport_Name.Left = 0;
            lblReport_Name.Top = starttop;
            lblReport_Name.Width = this.mTotalPageWidth;
            lblReport_Name.TextAlignment = TextAlignment.MiddleCenter;
            lblReport_Name.Font = new Font(this.mReport.Title_Font_Name, this.mReport.Title_Font_Size, FontStyle.Bold);
            lblReport_Name.ForeColor = Color.FromName(this.mReport.Title_Font_Color);
            //XRLabel lblTemplate_ID = new XRLabel();
            //lblTemplate_ID.Name = "TEMPLATE_ID";
            //lblTemplate_ID.Text = this.mReport.Template_ID;
            //this.ReportHeader.Controls.Add(lblTemplate_ID);
            //lblTemplate_ID.Left = 0;
            //lblTemplate_ID.Top = 0;
            //lblTemplate_ID.Width = this.mTotalPageWidth;
            //lblTemplate_ID.TextAlignment = TextAlignment.MiddleRight;
            //lblTemplate_ID.Font = new Font(this.mReport.Subtitle_Font_Name, this.mReport.Subtitle_Font_Size,
            //    FontStyle.Regular);
            starttop = lblReport_Name.Bottom;
            for (int i = 0; i < 5; i++) {
                if (this.mReport.Sub_Title[i] != string.Empty) {
                    XRLabel lblSubtitle = new XRLabel();
                    lblSubtitle.Name = "SUBTITLE" + Convert.ToString(i + 1);
                    lblSubtitle.Text = this.mReport.SubtitleString[i];
                    this.ReportHeader.Controls.Add(lblSubtitle);
                    lblSubtitle.Left = 0;
                    lblSubtitle.Top = starttop;
                    lblSubtitle.Width = this.mTotalPageWidth;
                    lblSubtitle.TextAlignment = TextAlignment.MiddleCenter;
                    this.SetSubtitleFont(lblSubtitle);
                    starttop += lblSubtitle.Height;
                }
            }
            XRLabel lblReport_Period = new XRLabel();
            lblReport_Period.Name = "REPORT_PERIOD";
            lblReport_Period.Text = this.mReport.ReportPeriod.ReportPeriodName;
            this.ReportHeader.Controls.Add(lblReport_Period);
            lblReport_Period.Left = 0;
            lblReport_Period.Top = starttop;
            lblReport_Period.Width = this.mTotalPageWidth;
            lblReport_Period.TextAlignment = TextAlignment.MiddleCenter;
            this.SetSubtitleFont(lblReport_Period);
        }

        private void CreateBalanceLuykeHeaderExcel() {
            XRLabel lblReport_Period = (XRLabel) this.ReportHeader.Controls["REPORT_PERIOD"];
            int starttop = lblReport_Period.Bottom;
            XRLabel lblbalance_nte_no;
            if (this.mReport.Show_Balance_Nte) {
                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NTE_NO";
                if (this.mReport.BeginningBalanceNte > 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") + ": " + this.ConvertToString(this.mReport.BeginningBalanceNte);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") + ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;

                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NTE_CO";
                if (this.mReport.BeginningBalanceNte < 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") + ": " +
                                             this.ConvertToString(this.mReport.BeginningBalanceNte*-1);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") + ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;
            }

            if (this.mReport.Show_Balance) {
                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NO";
                if (this.mReport.BeginningBalance > 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") +
                                             ": " + this.ConvertToString(this.mReport.BeginningBalance);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") +
                                             ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;

                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_CO";
                if (this.mReport.BeginningBalance < 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") +
                                             ": " + this.ConvertToString(this.mReport.BeginningBalance*-1);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_DAU_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") +
                                             ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;
            }
            if (this.mReport.Show_Balance_Nte) {
                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NTE_NO";
                if (this.mReport.EndingBalanceNte > 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") + ": " + this.ConvertToString(this.mReport.EndingBalanceNte);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") + ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;

                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NTE_CO";
                if (this.mReport.EndingBalanceNte < 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") + ": " + this.ConvertToString(this.mReport.EndingBalanceNte*-1);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NTE") +
                                             " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") + ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;
            }

            if (this.mReport.Show_Balance) {
                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_NO";
                if (this.mReport.EndingBalance > 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") +
                                             ": " + this.ConvertToString(this.mReport.EndingBalance);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_NO") +
                                             ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;

                lblbalance_nte_no = new XRLabel();
                lblbalance_nte_no.Name = "BEGINNING_BALANCE_CO";
                if (this.mReport.EndingBalance < 0) {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") +
                                             ": " + this.ConvertToString(this.mReport.EndingBalance*-1);
                } else {
                    lblbalance_nte_no.Text = this.mFTSMain.MsgManager.GetMessage("MSG_SO_DU_CUOI_KY") + " " + this.mFTSMain.MsgManager.GetMessage("MSG_CO") +
                                             ": 0";
                }
                this.ReportHeader.Controls.Add(lblbalance_nte_no);
                lblbalance_nte_no.Left = 0;
                lblbalance_nte_no.Top = starttop;
                lblbalance_nte_no.Width = this.mTotalPageWidth;
                this.SetSubtitleFont1(lblbalance_nte_no);
                lblbalance_nte_no.TextAlignment = TextAlignment.MiddleRight;
                starttop = lblbalance_nte_no.Bottom;
            }
        }
    }
}