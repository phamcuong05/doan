#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Utilities;

#endregion

namespace FTS.Base.Report {
    public class TransactionOutput : XtraReport {
        private Container components = null;
        public string reportfilename;
        private ManagerBase managerbase;
        private DataTable datatable;
        private DataSet dataset;
        protected string thousandsymbol, decimalsymbol;
        protected string tablename;
        private FTSMain ftsMain;
        protected int numberBlank;
        public Hashtable FilterStringTables = null;
        public string mFilterString = string.Empty;
        private ReportPeriod mReportPeriod;
        private Hashtable mExPara;

        public TransactionOutput() {
            this.InitializeComponent();
        }

        public TransactionOutput(FTSMain ftsmain, ManagerBase mb, DataTable dt, string tablename, string filename, int numberblank) {
            this.mExPara = new Hashtable();
            this.ftsMain = ftsmain;
            this.managerbase = mb;
            this.reportfilename = filename;
            this.tablename = tablename;
            this.numberBlank = numberblank;
            this.datatable = dt;
            this.InitializeComponent();
            this.thousandsymbol = this.ftsMain.SystemVars.GetSystemVars("THOUSAND_SYMBOL").ToString().Trim();
            if (this.thousandsymbol == "") {
                this.thousandsymbol = " ";
            }
            this.decimalsymbol = this.ftsMain.SystemVars.GetSystemVars("DECIMAL_SYMBOL").ToString().Trim();
            if (this.decimalsymbol == "") {
                this.decimalsymbol = ",";
            }
            this.LoadLayout(this.reportfilename);
            this.TempPath = filename;
            CultureInfo mCultureInfo = new CultureInfo("vi-VN", true);
            mCultureInfo.NumberFormat.NumberGroupSeparator = this.thousandsymbol;
            mCultureInfo.NumberFormat.NumberDecimalSeparator = this.decimalsymbol;
            DateTimeFormatInfo d = new DateTimeFormatInfo();
            d.ShortDatePattern = "dd/MM/yyyy";
            mCultureInfo.DateTimeFormat = d;
            Thread.CurrentThread.CurrentCulture = mCultureInfo;
            FormatInfo.AlwaysUseThreadFormat = true;
            this.PrintingSystem.ShowMarginsWarning = false;
        }

        public TransactionOutput(FTSMain ftsmain, ManagerBase mb, DataTable dt, string tablename, string filename, int numberblank, Hashtable expara) {
            this.mExPara = expara;
            this.ftsMain = ftsmain;
            this.managerbase = mb;
            this.reportfilename = filename;
            this.tablename = tablename;
            this.numberBlank = numberblank;
            this.datatable = dt;
            this.InitializeComponent();
            this.thousandsymbol = this.ftsMain.SystemVars.GetSystemVars("THOUSAND_SYMBOL").ToString().Trim();
            if (this.thousandsymbol == "") {
                this.thousandsymbol = " ";
            }
            this.decimalsymbol = this.ftsMain.SystemVars.GetSystemVars("DECIMAL_SYMBOL").ToString().Trim();
            if (this.decimalsymbol == "") {
                this.decimalsymbol = ",";
            }
            this.LoadLayout(this.reportfilename);
            this.TempPath = filename;
            CultureInfo mCultureInfo = new CultureInfo("vi-VN", true);
            mCultureInfo.NumberFormat.NumberGroupSeparator = this.thousandsymbol;
            mCultureInfo.NumberFormat.NumberDecimalSeparator = this.decimalsymbol;
            DateTimeFormatInfo d = new DateTimeFormatInfo();
            d.ShortDatePattern = "dd/MM/yyyy";
            mCultureInfo.DateTimeFormat = d;
            Thread.CurrentThread.CurrentCulture = mCultureInfo;
            FormatInfo.AlwaysUseThreadFormat = true;
        }

        public ReportPeriod ReportPeriod {
            get { return this.mReportPeriod; }
            set { this.mReportPeriod = value; }
        }

        public DataSet DataSet {
            get { return this.dataset; }
            set { this.dataset = value; }
        }

        public FTSMain FTSMain {
            get { return this.ftsMain; }
            set { this.ftsMain = value; }
        }

        public DataTable DataTable {
            get { return this.datatable; }
            set { this.datatable = value; }
        }

        public ManagerBase ManagerBase {
            get { return this.managerbase; }
            set { this.managerbase = value; }
        }

        public Hashtable ExPara {
            get { return this.mExPara; }
            set { this.mExPara = value; }
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
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();
        }

        #endregion

        protected virtual void SetParameter() {
            foreach (Band band in this.Bands) {
                foreach (XRControl ct in band.Controls) {
                    if ((ct is XRLabel) && (((XRLabel) ct).Parameter != "") && (((XRLabel) ct).Parameter != "(None)")) {
                        ct.Text = this.GetParameter(((XRLabel) ct).Parameter.ToUpper().Trim()).ToString();
                    }
                    if (ct is XRTable) {
                        this.SetParameter((XRTable) ct);
                    }
                    if (ct is XRSubreport) {
                        this.SetSubReport((XRSubreport) ct);
                    }
                    if (ct is XRPictureBox) {
                        object obj = this.GetParameter(((XRPictureBox) ct).Parameter.ToUpper().Trim());
                        if (obj != null && obj != System.DBNull.Value) {
                            try {
                                System.Drawing.Image newImage = byteArrayToImage((byte[])obj);
                                ((XRPictureBox) ct).Image = (System.Drawing.Image)newImage;
                            }catch(Exception){}
                        } else {
                            try {
                                ((XRPictureBox)ct).Image = null;
                            } catch (Exception) { }
                        }
                    }
                }
            }
        }
        private System.Drawing.Image byteArrayToImage(byte[] byteArrayIn) {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                return System.Drawing.Image.FromStream(ms, true);
        }

        private void SetParameter(XRTable control) {
            foreach (XRTableRow row in control.Rows) {
                foreach (XRTableCell cell in row.Cells) {
                    if (cell.Parameter.Trim() != string.Empty) {
                        cell.Text = this.GetParameter(cell.Parameter.ToUpper().Trim()).ToString();
                    }
                }
            }
        }

        //protected override void OnBeforePrint(System.Drawing.Printing.PrintEventArgs e) {
        //    base.OnBeforePrint(e);
        //    this.SetParameter();
        //}

        public virtual void SetDataSource() {
            this.dataset = new DataSet();
            if (this.managerbase != null) {
                this.dataset.Tables.Add(this.managerbase.DataSet.Tables[this.tablename].Copy());
            } else {
                if (this.datatable != null) {
                    this.dataset.Tables.Add(this.datatable);
                }
            }
            this.DataSource = this.dataset;
            this.DataMember = this.tablename;
        }

        protected virtual object GetParameter(string parametername) {
            return "";
        }

        protected virtual string ConvertToStringShowZero(object number) {
            return Functions.ConvertToStringShowZero(number, this.decimalsymbol, this.thousandsymbol);
        }

        protected virtual string ConvertToStringShowZero(object number, int tp) {
            return Functions.ConvertToStringShowZero(number, this.decimalsymbol, this.thousandsymbol, tp);
        }

        protected virtual string ConvertToString(object number) {
            return Functions.ConvertToString(number, this.decimalsymbol, this.thousandsymbol);
        }

        protected virtual string ConvertToString(object number, int tp) {
            return Functions.ConvertToString(number, this.decimalsymbol, this.thousandsymbol, tp);
        }

        protected virtual string ConvertNumberToString(decimal number) {
            string ReText = string.Empty;
            ReText = string.Format("{0:###,###,###,###,###0.00}", number);
            return ReText;
        }

        public virtual void RefreshTransactionOutput() {
            this.LoadLayout(this.reportfilename);
            /*
            if (this.managerbase != null) {
                this.RefreshDataSource();
            }
            */
            this.RefreshDataSource();
            this.SetParameter();
        }

        protected virtual void RefreshDataSource() {
            if (this.dataset != null) {
                this.dataset.Clear();
                if (this.dataset.Tables.IndexOf(this.tablename) >= 0) {
                    this.dataset.Tables.Remove(this.tablename);
                }
                if (this.managerbase != null) {
                    this.dataset.Tables.Add(this.managerbase.DataSet.Tables[this.tablename].Copy());
                } else {
                    if (this.datatable != null) {
                        this.dataset.Tables.Add(this.datatable);
                    }
                }
            }
            this.DataSource = this.dataset;
            this.DataMember = this.tablename;
        }

        public virtual void RunReport() {}
        public virtual void SetSubReport(XRSubreport subrpt) {}
    }
}