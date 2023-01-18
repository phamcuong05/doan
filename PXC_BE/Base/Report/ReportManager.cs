// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:52
// Project Name:              ReportBase
// Project Filename:          ReportBase.csproj
// Project Item Name:         ReportManager.cs
// Project Item Filename:     ReportManager.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FTS.Base.Systems;
using FTS.Base.Utilities;

#endregion

namespace FTS.Base.Report {
    public class ReportManager : IDisposable {
        private bool disposed = false;
        protected FTSMain mFTSMain;
        protected FTSReport mReport;

        public ReportManager(FTSReport rpt, FTSMain ftsmain) {
            this.mReport = rpt;
            this.mFTSMain = ftsmain;
        }

        public FTSReport Report {
            get { return this.mReport; }
            set { this.mReport = value; }
        }

        #region IDisposable Members

        public void Dispose() {
            this.Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        #endregion

        
        public virtual void RunReport() {
            try {
                this.CheckSystem();
                this.mReport.UpdateReport();
                this.mReport.ClearReport();
                this.mReport.CheckInput();
                   this.mReport.CalculateReport();
                int originalparts = this.mReport.Num_File_Report;
                this.mReport.Prepare();
                if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp] == null) {
                    throw (new FTSException("MSG_NODATA"));
                }
                DataTable tblmau = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp];
                foreach (DataColumn c in tblmau.Columns) {
                    c.ColumnName = c.ColumnName.ToUpper();
                }
                tblmau.TableName = tblmau.TableName.Trim();
                this.mReport.Template_Table_Tmp = this.mReport.Template_Table_Tmp.Trim();
                Functions.ClearDataSetExceptTable(this.mReport.DataSet, this.mReport.Template_Table_Tmp);
                if (this.mReport.DataSet.Tables.IndexOf(this.mReport.Template_Table_Tmp) != 0) {
                    DataTable dt = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy();
                    this.mReport.DataSet = new DataSet();
                    this.mReport.DataSet.Tables.Add(dt);
                    this.mReport.DataSet.AcceptChanges();
                }
            } catch (FTSException) {
                throw;
            } catch (Exception ex) {
                throw (new FTSException(ex));
            }
        }
        
        public virtual void RunReportData() {
            try {
                this.mReport.UpdateReport();
                this.mReport.ClearReport();
                this.mReport.CheckInput();
                   this.mReport.CalculateReport();
                int originalparts = this.mReport.Num_File_Report;
                this.mReport.Prepare();
                if (this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp] == null) {
                    throw (new FTSException("MSG_NODATA"));
                }
                DataTable tblmau = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp];
                foreach (DataColumn c in tblmau.Columns) {
                    c.ColumnName = c.ColumnName.ToUpper();
                }
                tblmau.TableName = tblmau.TableName.Trim();
                this.mReport.Template_Table_Tmp = this.mReport.Template_Table_Tmp.Trim();
                Functions.ClearDataSetExceptTable(this.mReport.DataSet, this.mReport.Template_Table_Tmp);
                if (this.mReport.DataSet.Tables.IndexOf(this.mReport.Template_Table_Tmp) != 0) {
                    DataTable dt = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy();
                    this.mReport.DataSet = new DataSet();
                    this.mReport.DataSet.Tables.Add(dt);
                    this.mReport.DataSet.AcceptChanges();
                }
            } catch (FTSException) {
                throw;
            } catch (Exception ex) {
                throw (new FTSException(ex));
            }
        }
        public virtual void RunReportBrowse() {
            try {
                this.mReport.UpdateReport();
                this.mReport.ClearReport();
                this.mReport.CheckInput();
                this.mReport.CalculateReport();
                DataTable tblmau = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp];
                foreach (DataColumn c in tblmau.Columns) {
                    c.ColumnName = c.ColumnName.ToUpper();
                }
                tblmau.TableName = tblmau.TableName.Trim();
                this.mReport.Template_Table_Tmp = this.mReport.Template_Table_Tmp.Trim();
                Functions.ClearDataSetExceptTable(this.mReport.DataSet, this.mReport.Template_Table_Tmp);
                if (this.mReport.DataSet.Tables.IndexOf(this.mReport.Template_Table_Tmp) != 0) {
                    DataTable dt = this.mReport.DataSet.Tables[this.mReport.Template_Table_Tmp].Copy();
                    this.mReport.DataSet = new DataSet();
                    this.mReport.DataSet.Tables.Add(dt);
                    this.mReport.DataSet.AcceptChanges();
                }
                this.mReport.Prepare();
                this.mReport.sys_reportfieldgrid.LoadVisibleData();
            } catch (FTSException) {
                throw;
            } catch (Exception ex) {
                throw (new FTSException(ex));
            }
        }
        public void ClearAll() {
            this.mReport.ClearReport();
        }

        private void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    this.mReport.Dispose();
                }
            }
            this.disposed = true;
        }

        private void CheckSystem() {
            if (this.mFTSMain.DEMO) {
                if (Functions.InListAbsolute("ACC", this.mFTSMain.SystemVars.GetSystemVars("PROJECT_LIST").ToString())) {
                    string sql = "select max(tran_date) as tran_date_max, min(tran_date) as tran_date_min from " + FTSConstant.TableCheck +
                                 " where tran_date >=" + Functions.ParseDate(this.mFTSMain.DayStartOfFirstYear);
                    DataTable dt = this.mFTSMain.DbMain.LoadDataTable(this.mFTSMain.DbMain.GetSqlStringCommand(sql), "tmp");
                    if (dt.Rows.Count > 0 && dt.Rows[0]["tran_date_max"] != DBNull.Value) {
                        TimeSpan t2 = (DateTime) dt.Rows[0]["tran_date_max"] - (DateTime) dt.Rows[0]["tran_date_min"];
                        if (t2.Days > FTSConstant.TrialDays) {
                            throw (new FTSException("MSG_UNREGISTER"));
                        }
                    }
                } else {
                    if (Functions.InListAbsolute("POS", this.mFTSMain.SystemVars.GetSystemVars("PROJECT_LIST").ToString())) {
                        object cnt = this.mFTSMain.DbMain.ExecuteScalar(this.mFTSMain.DbMain.GetSqlStringCommand("select count(*) from sale"));
                        if (cnt != null && cnt != System.DBNull.Value) {
                            if (Convert.ToInt32(cnt) > 200) {
                                FTSMessageBox.ShowInfoMessage(this.mFTSMain.MsgManager.GetMessage("MSG_UNREGISTER"));
                            }
                        }
                    }
                }
            }
        }

        private bool IsReportSerializable() {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            try {
                formatter.Serialize(stream, this.mReport);
            } catch (Exception) {
                return false;
            }
            return true;
        }
    }
}