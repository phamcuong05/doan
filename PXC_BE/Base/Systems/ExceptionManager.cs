// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:54
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         ExceptionManager.cs
// Project Item Filename:     ExceptionManager.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using FTS.Base.Utilities;
using System.Data.Common;

#endregion

namespace FTS.Base.Systems {
    public class ExceptionManager {
        //private Hashtable mHs;
        private FTSMain mFTSMain;

        public ExceptionManager(FTSMain ftsmain) {
            this.mFTSMain = ftsmain;
        }

        public string GetMessage(FTSException ex) {
            if (ex.ExceptionID.Length == 0) {
                if (ex.SystemException == null) {
                    return string.Empty;
                } else {
                    return ex.SystemException.Message + ex.ExtraInformation;
                }
            } else {
                if (!String.IsNullOrEmpty(ex.TableName) && !String.IsNullOrEmpty(ex.FieldName)) {
                    return this.mFTSMain.ResourceManager.GetValue("ERR_" + ex.ExceptionID, ex.ExceptionID) + ex.ExtraInformation + ": " +
                           this.mFTSMain.ResourceManager.GetValue("TBL_" + ex.TableName, ex.TableName) + " - " + ex.FieldName;
                } else {
                    return this.mFTSMain.ResourceManager.GetValue("ERR_" + ex.ExceptionID, ex.ExceptionID) + ex.ExtraInformation;
                }
            }
        }

        public FTSExceptionObject ProcessException(Exception e) {
            FTSException ex = null;
            try {
                if (e.GetType().Equals(typeof(FTSException))) {
                    ex = (FTSException) e;
                    this.LogError(ex);
                } else {
                    ex = new FTSException(e);
                    this.LogError(ex);
                }
                return new FTSExceptionObject(this.GetMessage(ex),ex.ExceptionID,ex.TableName,ex.FieldName,ex.RowPos,ex.ExtraInformation);
            } catch (Exception) {
                return new FTSExceptionObject(this.GetMessage(ex), ex.ExceptionID, ex.TableName, ex.FieldName, ex.RowPos, ex.ExtraInformation);
            }
        }

        public void ProcessExceptionShowMessage(Exception e) {
            FTSException ex = null;
            try {
                if (e.GetType().Equals(typeof(FTSException))) {
                    ex = (FTSException)e;
                    this.LogError(ex);
                } else {
                    ex = new FTSException(e);
                    this.LogError(ex);
                }
                FTSMessageBox.ShowErrorMessage(this.GetMessage(ex) + "-" + ex.StackTrace + "-" +  ex.ExceptionID + "-" + ex.TableName + "-" + ex.FieldName + "-" + ex.RowPos + "-" + ex.ExtraInformation);
            } catch (Exception) {
                FTSMessageBox.ShowErrorMessage(this.GetMessage(ex) + "-" + ex.StackTrace + "-" + ex.ExceptionID + "-" + ex.TableName + "-" + ex.FieldName + "-" + ex.RowPos + "-" + ex.ExtraInformation);
            }
        }


        private void LogError(FTSException ex) {

            string msg = this.GetMessage(ex);
            if (ex.SystemException != null) {
                string sql = "INSERT INTO LOGGING_SYSTEM(MESSAGE,LOG_DATE,SOURCE,STACK_TRACE,TABLE_NAME,FIELD_NAME) VALUES(" +
                             this.mFTSMain.DbMain.BuildParameterName("MESSAGE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("LOG_DATE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("SOURCE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("STACK_TRACE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("TABLE_NAME") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("FIELD_NAME") + ")";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, "MESSAGE", DbType.String, msg);
                this.mFTSMain.DbMain.AddInParameter(cmd, "LOG_DATE", DbType.DateTime, DateTime.Now);
                this.mFTSMain.DbMain.AddInParameter(cmd, "SOURCE", DbType.String, ex.SystemException.Source);
                this.mFTSMain.DbMain.AddInParameter(cmd, "STACK_TRACE", DbType.String, ex.SystemException.StackTrace);
                this.mFTSMain.DbMain.AddInParameter(cmd, "TABLE_NAME", DbType.String, ex.TableName);
                this.mFTSMain.DbMain.AddInParameter(cmd, "FIELD_NAME", DbType.String, ex.FieldName);
                this.mFTSMain.DbMain.AddInParameter(cmd, "EXTRA_INFO", DbType.String, ex.ExtraInformation);
                this.mFTSMain.DbMain.ExecuteNonQuery(cmd);
            } else {
                string sql = "INSERT INTO LOGGING_SYSTEM(MESSAGE,LOG_DATE,SOURCE,STACK_TRACE,TABLE_NAME,FIELD_NAME) VALUES(" +
                             this.mFTSMain.DbMain.BuildParameterName("MESSAGE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("LOG_DATE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("SOURCE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("STACK_TRACE") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("TABLE_NAME") + "," +
                             this.mFTSMain.DbMain.BuildParameterName("FIELD_NAME") + ")";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, "MESSAGE", DbType.String, msg);
                this.mFTSMain.DbMain.AddInParameter(cmd, "LOG_DATE", DbType.DateTime, DateTime.Now);
                this.mFTSMain.DbMain.AddInParameter(cmd, "SOURCE", DbType.String, string.Empty);
                this.mFTSMain.DbMain.AddInParameter(cmd, "STACK_TRACE", DbType.String, string.Empty);
                this.mFTSMain.DbMain.AddInParameter(cmd, "TABLE_NAME", DbType.String, ex.TableName);
                this.mFTSMain.DbMain.AddInParameter(cmd, "FIELD_NAME", DbType.String, ex.FieldName);
                this.mFTSMain.DbMain.ExecuteNonQuery(cmd);
            }

        }


        public void HandlingDbException(Exception ex, DataTable datatable, string tablename, string idfield) {
            int errorcode = 0;
            if (ex is DBConcurrencyException) {
                throw (new FTSException(ex, "CONCURRENCY_EXCEPTION", tablename, string.Empty, -1));
            }

            if (ex is SqlException) {
                errorcode = ((SqlException) ex).Number;
            }

            switch (errorcode) {
                case 50000:
                    int pos = ex.Message.IndexOf("\r");
                    if (pos >= 0) {
                        throw (new FTSException(ex, ex.Message.Substring(0, pos), tablename, idfield, -1));
                    } else {
                        throw (new FTSException(ex, ex.Message, tablename, idfield, -1));
                    }
                case 2627:
                case 2601:
                    for (int j = 0; j < datatable.Rows.Count; j++) {
                        if (datatable.Rows[j].HasErrors && datatable.Rows[j].RowError.IndexOf(ex.Message) >= 0) {
                            throw (new FTSException(ex, "DUPLICATE_PRIMARY_KEY", tablename, idfield, j));
                        }
                    }

                    throw (new FTSException(ex, "DUPLICATE_PRIMARY_KEY", tablename, idfield, -1));
                case 547:
                    string exceptionid;
                    string fieldname = string.Empty;
                    if (ex.Message.IndexOf("INSERT", 0) >= 0) {
                        exceptionid = "FOREIGN_KEY_INSERT";
                    } else {
                        exceptionid = "FOREIGN_KEY_DELETE";
                    }

                    int startidx = ex.Message.IndexOf("column '", 0);
                    if (startidx >= 0) {
                        int endidx = ex.Message.IndexOf("'", startidx + 8);
                        if (endidx >= 0 && endidx >= startidx + 8) {
                            fieldname = ex.Message.Substring(startidx + 8, endidx - startidx - 8);
                        }
                    }

                    if (exceptionid == "FOREIGN_KEY_INSERT") {
                        for (int j = 0; j < datatable.Rows.Count; j++) {
                            if (datatable.Rows[j].HasErrors && datatable.Rows[j].RowError.IndexOf(ex.Message) >= 0) {
                                throw (new FTSException(ex, exceptionid, tablename, fieldname, j));
                            }
                        }
                    }

                    throw (new FTSException(ex, exceptionid, tablename, fieldname, -1));
                default:
                    break;
            }

            for (int j = 0; j < datatable.Rows.Count; j++) {
                if (datatable.Rows[j].HasErrors) {
                    throw (new FTSException(ex, ex.Message, tablename, string.Empty, j));
                }
            }
        }
    }
}