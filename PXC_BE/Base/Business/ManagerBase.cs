// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:47
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         ManagerBase.cs
// Project Item Filename:     ManagerBase.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Security;
using FTS.Base.Systems;
using FTS.Base.Utilities;
using FTS.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion

namespace FTS.Base.Business {
    [Serializable]
    public class ManagerBase : ManageBaseBase {
        public bool AllowCreateOtherOrg = false;
        public bool IsLogging = true;
        protected bool IsOrganizationFilter = false;
        private Hashtable mConfigHashTable = null;
        private DataSet mDataSet;
        [NonSerialized] private Hashtable mDefaultHashTable = new Hashtable();
        private FTSFunction mFTSFunction = null;
        [NonSerialized] private FTSMain mFTSMain;
        private FTSCollection<ObjectBase> mObjectList;
        private string mTranDateField = string.Empty;
        private string mTranId = string.Empty;
        private string mTranIdField = string.Empty;
        private string mTranNoField = string.Empty;
        protected string ExtraFilter = string.Empty;
        public string ExtraTranID = string.Empty;
        public string PrDetailFieldList = string.Empty;
        public string PrDetailEmptyFilter = " 1=1 ";
        public string ItemFieldList = string.Empty;
        public string ItemEmptyFilter = " 1=1 ";
        public ManagerBase(FTSMain ftsmain, string tranid) {
            this.TranId = tranid;
            this.mFTSMain = ftsmain;
            this.mDataSet = new DataSet();
            this.mObjectList = new FTSCollection<ObjectBase>();
            this.LoadData();
            this.SetRole();
            this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.ViewAction, string.Empty);
        }

        public DataSet DataSet {
            get { return this.mDataSet; }
            set { this.mDataSet = value; }
        }

        public FTSMain FTSMain {
            get { return this.mFTSMain; }
            set { this.mFTSMain = value; }
        }

        public FTSCollection<ObjectBase> ObjectList {
            get { return this.mObjectList; }
            set { this.mObjectList = value; }
        }

        public string TranDateField {
            get { return this.mTranDateField; }
            set { this.mTranDateField = value; }
        }

        public Hashtable DefaultHashTable {
            get { return this.mDefaultHashTable; }
        }

        public string TranIdField {
            get { return this.mTranIdField; }
            set { this.mTranIdField = value; }
        }

        public string TranNoField {
            get { return this.mTranNoField; }
            set { this.mTranNoField = value; }
        }

        public FTSFunction FTSFunction {
            get { return this.mFTSFunction; }
            set { this.mFTSFunction = value; }
        }

        public string TranId {
            get { return this.mTranId; }
            set {
                this.mTranId = value.Trim();
            }
        }

        public virtual DataRow AddNewDetail(int detailID, DataRow r) {
            if (this.mObjectList[0].DataTable.Rows.Count > 0) {
                DataRow row = this.mObjectList[detailID].AddNew(r);
                row["fr_key"] = this.mObjectList[0].DataTable.Rows[0]["pr_key"];
                row.EndEdit();
                return row;
            }

            throw new FTSException("MSG_NO_HEADER_ROW");
        }

        public virtual DataRow AddNewDetail(int detailID) {
            if (this.mObjectList[0].DataTable.Rows.Count > 0) {
                DataRow row = this.mObjectList[detailID].AddNew();
                row["fr_key"] = this.mObjectList[0].DataTable.Rows[0]["pr_key"];
                row.EndEdit();
                return row;
            }

            throw new FTSException("MSG_NO_HEADER_ROW");
        }

        public virtual void CopyRecord() {
            if (this.mObjectList[0].DataTable.Rows.Count == 0) {
                return;
            }

            List<DataTable> tablelist = new List<DataTable>();
            foreach (ObjectBase ob in this.mObjectList) {
                DataTable obDt = ob.DataTable.Copy();
                DataTable tmp = obDt.Clone();
                if (obDt.Columns.IndexOf("LIST_ORDER") > 0) {
                    DataView dvtmp = new DataView(ob.DataTable, "", "LIST_ORDER", DataViewRowState.CurrentRows);
                    foreach (DataRowView drv in dvtmp) {
                        DataRow nr = tmp.NewRow();
                        nr.ItemArray = drv.Row.ItemArray;
                        tmp.Rows.Add(nr);
                    }
                } else {
                    tmp = obDt;
                }

                tablelist.Add(tmp);
            }
        }

        public virtual DataRow CopyDetail(int detailID, int pos) {
            return this.mObjectList[detailID].CopyRecord(pos);
        }

        public virtual DataRow InsertDetail(int detailID, int pos) {
            if (this.mObjectList[0].DataTable.Rows.Count > 0) {
                DataRow row = this.mObjectList[detailID].InsertRecord(pos);
                row["fr_key"] = this.mObjectList[0].DataTable.Rows[0]["pr_key"];
                row.EndEdit();
                return row;
            }

            throw new FTSException("MSG_NO_HEADER_ROW");
        }

        public virtual void LoadData() { }

        public virtual void RegisterDefaultValues() { }

        public virtual void LoadRecordWithPrkey(object key) {
            this.ClearData();
            this.mObjectList[0].LoadDataByID(key);
            for (int i = 1; i < this.mObjectList.Count; i++) {
                this.mObjectList[i].LoadDataByFrkey(key);
            }
        }

        public virtual void LoadRecordWithReferencePrkey(object key) {
            this.ClearData();
            this.mObjectList[0].LoadDataByReferencePrKey(key);
            for (int i = 1; i < this.mObjectList.Count; i++) {
                this.mObjectList[i].LoadDataByFrkey(key);
            }
        }

        public virtual void ClearData() {
            foreach (ObjectBase ob in this.mObjectList) {
                ob.Clear();
            }
        }

        public virtual void PrepareUpdateData() {
            if (this.ObjectList[0].DataTable.Rows.Count > 0) {
                if (this.ObjectList[0].DataTable.Rows[0].RowState == DataRowState.Added) {
                    this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, this.GetOrganizationID());
                } else {
                    if (this.ObjectList[0].DataTable.Rows[0].RowState == DataRowState.Deleted) {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, this.GetOrganizationID());
                    } else {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, this.GetOrganizationID());
                    }
                }
            }

            this.EndEdit();
            this.CheckBusinessRules();
        }

        public virtual void PrepareUpdateData(DbTransaction tran) {
            if (this.ObjectList[0].DataTable.Rows.Count > 0) {
                if (this.ObjectList[0].DataTable.Rows[0].RowState == DataRowState.Added) {
                    this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, this.GetOrganizationID());
                } else {
                    if (this.ObjectList[0].DataTable.Rows[0].RowState == DataRowState.Deleted) {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, this.GetOrganizationID());
                    } else {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, this.GetOrganizationID());
                    }
                }
            }

        }

        public virtual void UpdateData() {
            DataTable dtlogdata = this.ObjectList[0].DataTable.Copy();
            this.PrepareUpdateData();
            string oldTran_no = string.Empty;
            string oldOtherTran_no = string.Empty;
            DbTransaction tran = null;
            try {
                DataSet ds = this.mDataSet.GetChanges();
                if (ds != null) {
                    using (DbConnection connection = this.FTSMain.DbMain.CreateConnection()) {
                            connection.Open();
                            if ((bool) this.FTSMain.SystemVars.GetSystemVars("USE_SNAPSHOT_TRANSACTION")) {
                                tran = connection.BeginTransaction(IsolationLevel.Snapshot);
                            } else {
                                tran = connection.BeginTransaction();
                            }

                        oldOtherTran_no = this.UpdateOtherTranNoTemp(null);
                        ds = this.mDataSet.GetChanges();
                        if (ds != null) {
                            foreach (ObjectBase ob in this.mObjectList) {
                                ob.UpdateData(ds, tran);
                            }

                            this.UpdateOtherTranNo(tran);
                                tran.Commit();
                            
                            ds.AcceptChanges();
                            this.AcceptChanges();
                        } else {
                            if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                                this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                            }

                            this.RestoreOtherTranNo(oldOtherTran_no);
                            try {
                                tran.Rollback();
                            } catch (Exception) { }
                        }
                    }
                }
            } catch (FTSException) {
                if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                }

                this.RestoreOtherTranNo(oldOtherTran_no);
                try {
                    tran.Rollback();
                } catch (Exception) { }

                throw;
            } catch (Exception ex) {
                if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                }

                this.RestoreOtherTranNo(oldOtherTran_no);
                try {
                    tran.Rollback();
                } catch (Exception) { }

                throw (new FTSException(ex));
            }
        }

        public virtual void UpdateData(DbTransaction tran) {
            DataTable dtlogdata = this.ObjectList[0].DataTable.Copy();
            this.PrepareUpdateData();
            string oldTran_no = string.Empty;
            string oldOtherTran_no = string.Empty;
            try {
                DataSet ds = this.mDataSet.GetChanges();
                if (ds != null) {

                    oldOtherTran_no = this.UpdateOtherTranNoTemp(null);
                    ds = this.mDataSet.GetChanges();
                    if (ds != null) {
                        foreach (ObjectBase ob in this.mObjectList) {
                            ob.UpdateData(ds, tran);
                        }

                        this.UpdateOtherTranNo(tran);

                        ds.AcceptChanges();
                        this.AcceptChanges();
                    } else {
                        if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                            this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                        }

                        this.RestoreOtherTranNo(oldOtherTran_no);

                    }
                }

            } catch (FTSException) {
                if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                }

                this.RestoreOtherTranNo(oldOtherTran_no);
                throw;
            } catch (Exception ex) {
                if (oldTran_no == this.mFTSMain.SystemVars.GetSystemVars("DF_EMPTY_TRANS_NO").ToString()) {
                    this.mObjectList[0].DataTable.Rows[0][this.mTranNoField] = oldTran_no;
                }

                this.RestoreOtherTranNo(oldOtherTran_no);
                throw (new FTSException(ex));
            }

        }

        public virtual void RemoveEmptyRows() { }

        protected virtual void CreateOtherData() { }

        protected virtual void UpdateOtherData(DbTransaction tran) { }

        protected virtual string UpdateOtherTranNo(DbTransaction tran) {
            return string.Empty;
        }

        protected virtual string UpdateOtherTranNoTemp(DbTransaction tran) {
            return string.Empty;
        }

        protected virtual string UpdateOtherTranNo(string tranno, DbTransaction tran) {
            return string.Empty;
        }

        protected virtual void RestoreOtherTranNo(string oldtranno) { }

        public virtual void AcceptChanges() {
            foreach (ObjectBase ob in this.mObjectList) {
                ob.AcceptChanges();
            }
        }

        public virtual void DeleteData() {
            try {
                this.EndEdit();
                foreach (ObjectBase ob in this.mObjectList) {
                    ob.DeleteAll();
                }

                this.UpdateData();
                this.DeleteOtherData();
            } catch (FTSException) {
                this.RestoreData();
                throw;
            } catch (Exception ex) {
                this.RestoreData();
                throw (new FTSException(ex));
            }
        }

        public virtual void DeleteData(DbTransaction tran) {
            try {
                this.EndEdit();
                foreach (ObjectBase ob in this.ObjectList) {
                    ob.DeleteAll();
                }

                this.UpdateData(tran);
            } catch (FTSException) {
                this.RestoreData();
                throw;
            } catch (Exception ex) {
                this.RestoreData();
                throw (new FTSException(ex));
            }
        }

        public virtual void DeleteDetail(int detailID, int pos) {
            this.EndEdit();
            this.mObjectList[detailID].DeleteAtPosition(pos);
        }

        public virtual void RestoreData() {
            this.EndEdit();
            foreach (ObjectBase ob in this.mObjectList) {
                ob.Restore();
            }
        }
        
        public virtual void PreviousRecord(DateTime currentdate, string currentno) {
            string sql = string.Empty;
            if (currentno != string.Empty) {
                sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranIdField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranIdField) + " and " + this.mTranNoField + " < " +
                      this.mFTSMain.BuildParameterName(this.mTranNoField) + " and " + this.mTranDateField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranDateField);
                if (this.IsOrganizationFilter) {
                    sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }

                if (this.ExtraFilter != string.Empty) {
                    sql += " AND " + this.ExtraFilter;
                }

                sql += "  order by " + this.mTranDateField + " desc," + this.mTranNoField + " desc";
                object key = null;
                DbCommand cmd = null;
                cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranDateField, DbType.Date, currentdate);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranNoField, DbType.String, currentno);
                key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                if (key != null && key != DBNull.Value) {
                    this.ClearData();
                    this.LoadRecordWithPrkey(key);
                } else {
                    sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranIdField + " = " +
                          this.mFTSMain.BuildParameterName(this.mTranIdField) + " and " + this.mTranDateField + " < " +
                          this.mFTSMain.BuildParameterName(this.mTranDateField);
                    if (this.IsOrganizationFilter) {
                        sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                    }

                    if (this.ExtraFilter != string.Empty) {
                        sql += " AND " + this.ExtraFilter;
                    }

                    sql += "  order by " + this.mTranDateField + " desc," + this.mTranNoField + " desc";
                    cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                    this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                    this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranDateField, DbType.Date, currentdate);
                    key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                    if (key != null && key != DBNull.Value) {
                        this.ClearData();
                        this.LoadRecordWithPrkey(key);
                    }
                }
            } else {
                sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranIdField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranIdField);
                if (this.IsOrganizationFilter) {
                    sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }

                if (this.ExtraFilter != string.Empty) {
                    sql += " AND " + this.ExtraFilter;
                }

                sql += "  order by " + this.mTranDateField + " asc," + this.mTranNoField + " asc";
                DbCommand cmd = null;
                object key = null;
                cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                if (key != null && key != DBNull.Value) {
                    this.LoadRecordWithPrkey(key);
                }
            }
        }

        public virtual void NextRecord(DateTime currentdate, string currentno) {
            string sql = string.Empty;
            if (currentno != string.Empty) {
                sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranDateField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranDateField) + " AND " + this.mTranNoField + " > " +
                      this.mFTSMain.BuildParameterName(this.mTranNoField) + " and " + this.mTranIdField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranIdField);
                if (this.IsOrganizationFilter) {
                    sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }

                if (this.ExtraFilter != string.Empty) {
                    sql += " AND " + this.ExtraFilter;
                }

                sql += "  order by " + this.TranDateField + " asc," + this.mTranNoField + " asc";
                DbCommand cmd = null;
                object key = null;
                cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranDateField, DbType.Date, currentdate);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranNoField, DbType.String, currentno);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                if (key != null && key != DBNull.Value) {
                    this.ClearData();
                    this.LoadRecordWithPrkey(key);
                } else {
                    sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranIdField + " = " +
                          this.mFTSMain.BuildParameterName(this.mTranIdField) + " and " + this.mTranDateField + " > " +
                          this.mFTSMain.BuildParameterName(this.mTranDateField);
                    if (this.IsOrganizationFilter) {
                        sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                    }

                    if (this.ExtraFilter != string.Empty) {
                        sql += " AND " + this.ExtraFilter;
                    }

                    sql += "  order by " + this.mTranDateField + " asc," + this.mTranNoField + " asc";
                    cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                    this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                    this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranDateField, DbType.Date, currentdate);
                    key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                    if (key != null && key != DBNull.Value) {
                        this.ClearData();
                        this.LoadRecordWithPrkey(key);
                    }
                }
            } else {
                sql = "select top 1 PR_KEY from " + this.mObjectList[0].TableName + " where " + this.mTranIdField + " = " +
                      this.mFTSMain.BuildParameterName(this.mTranIdField);
                if (this.IsOrganizationFilter) {
                    sql += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }

                if (this.ExtraFilter != string.Empty) {
                    sql += " AND " + this.ExtraFilter;
                }

                sql += "  order by " + this.mTranDateField + " desc," + this.mTranNoField + " desc";
                DbCommand cmd = null;
                object key = null;
                cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.mTranIdField, DbType.String, this.TranId);
                key = this.mFTSMain.DbMain.ExecuteScalar(cmd);
                if (key != null && key != DBNull.Value) {
                    this.LoadRecordWithPrkey(key);
                }
            }
        }
        
        public virtual bool HasChanged() {
            foreach (ObjectBase oj in this.mObjectList) {
                if (oj.HasChanged()) {
                    return true;
                }
            }

            return false;
        }


        public virtual void EndEdit() {
            foreach (ObjectBase ob in this.mObjectList) {
                ob.EndEdit();
            }
        }

        public virtual void CheckBusinessRules() {
            if (this.mObjectList[0].GetActiveRow() > 0 && this.mObjectList[1].GetActiveRow() == 0) {
                throw (new FTSException("MSG_EMPTY_DETAIL"));
            }

            foreach (ObjectBase ob in this.mObjectList) {
                ob.CheckBusinessRules();
            }

            if (this.DataSet.HasChanges()) {
                if (this.ObjectList[0].IsValidRow(0) && this.ObjectList[0].DataTable.Rows[0].RowState == DataRowState.Modified) {
                    if (this.ObjectList[0].DataTable.Columns.IndexOf("MODIFIED_USER_ID") >= 0) {
                        this.ObjectList[0].DataTable.Rows[0]["MODIFIED_USER_ID"] = this.FTSMain.UserInfo.UserID;
                    }
                }
            }

            if (this.IsOrganizationFilter && !this.AllowCreateOtherOrg) {
                ObjectBase oj = this.mObjectList[0];
                if (oj.IsValidRow(0)) {
                    if (oj.DataTable.Rows[0].RowState == DataRowState.Added) {
                        string organizationid = oj.GetValue("ORGANIZATION_ID").ToString();
                        if (organizationid != this.FTSMain.UserInfo.OrganizationID) {
                            if (this.FTSMain.DmOrganization.IsDependentChild(organizationid,
                                this.FTSMain.UserInfo.OrganizationID)) {
                                if (!(bool) this.FTSMain.SystemVars.GetSystemVars("ALLOW_EDIT_DEPENDENT_ORG")) {
                                    throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                                }
                            } else {
                                throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                            }
                        }
                    } else {
                        string organizationid = oj.GetValue("ORGANIZATION_ID").ToString();
                        string organizationidold = oj.DataTable.Rows[0]["ORGANIZATION_ID", DataRowVersion.Original].ToString();
                        if (organizationid != this.FTSMain.UserInfo.OrganizationID) {
                            if (this.FTSMain.DmOrganization.IsDependentChild(organizationid,
                                this.FTSMain.UserInfo.OrganizationID)) {
                                if (!(bool) this.FTSMain.SystemVars.GetSystemVars("ALLOW_EDIT_DEPENDENT_ORG")) {
                                    throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                                }
                            } else {
                                throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                            }
                        }

                        if (organizationidold != this.FTSMain.UserInfo.OrganizationID) {
                            if (this.FTSMain.DmOrganization.IsDependentChild(organizationidold,
                                this.FTSMain.UserInfo.OrganizationID)) {
                                if (!(bool) this.FTSMain.SystemVars.GetSystemVars("ALLOW_EDIT_DEPENDENT_ORG")) {
                                    throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                                }
                            } else {
                                throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                            }
                        }
                    }
                } else {
                    if (oj.DataTable.Rows.Count > 0 && oj.DataTable.Rows[0].RowState == DataRowState.Deleted) {
                        string organizationid = oj.DataTable.Rows[0]["ORGANIZATION_ID", DataRowVersion.Original].ToString();
                        if (organizationid != this.FTSMain.UserInfo.OrganizationID) {
                            if (this.FTSMain.DmOrganization.IsDependentChild(organizationid,
                                this.FTSMain.UserInfo.OrganizationID)) {
                                if (!(bool) this.FTSMain.SystemVars.GetSystemVars("ALLOW_EDIT_DEPENDENT_ORG")) {
                                    throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                                }
                            } else {
                                throw (new FTSException("MSG_NOT_ALLOW_UPDATE_OTHER_ORGANIZATION"));
                            }
                        }
                    }
                }
            }
        }

        public virtual void RefreshDm() { }

        public virtual void LoadDm() { }

        public virtual void LoadDm(string tablename) { }

        public virtual void SetRole() {
            this.mFTSFunction = new FTSFunction("TRAN_" + this.mTranId, "ACC", true, true, true, true, false, true);
        }


        public virtual string GetOrganizationID() {
            if (this.ObjectList[0].DataTable.Rows.Count > 0) {
                if (this.ObjectList[0].DataTable.Columns.IndexOf("ORGANIZATION_ID") >= 0) {
                    if (this.ObjectList[0].DataTable.Rows[0].RowState != DataRowState.Deleted) {
                        return this.ObjectList[0].GetValue("ORGANIZATION_ID").ToString();
                    } else {
                        return this.ObjectList[0].DataTable.Rows[0]["ORGANIZATION_ID", DataRowVersion.Original].ToString();
                    }
                } else {
                    return string.Empty;
                }
            } else {
                return string.Empty;
            }
        }

        //public virtual string GetTranClass() {
        //    DataRow foundrow = this.GetDm("SYS_TRAN").Rows.Find(this.TranId);
        //    if (foundrow != null) {
        //        return foundrow["TRAN_CLASS"].ToString();
        //    } else {
        //        return string.Empty;
        //    }
        //}

        private bool IsSameKey(DataRow row, List<string> keycolumnlist, object[] keys) {
            for (int i = 0; i < keycolumnlist.Count; i++) {
                if (row[keycolumnlist[i]].ToString() != keys[i].ToString()) {
                    return false;
                }
            }

            return true;
        }

        public virtual void ImportData(DataTable excelData, DataTable dm_template_detail) {
            bool dateasstring = (bool) this.FTSMain.SystemVars.GetSystemVars("EXCEL_TEMPLATE_DATE_AS_STRING");
            dm_template_detail.PrimaryKey = new DataColumn[] {dm_template_detail.Columns["DATA_COLUMN_NAME"]};
            List<DataRow> listAdded = new List<DataRow>();
            List<DataRow> listAddedTemp = new List<DataRow>();
            List<string> keycolumnlist = new List<string>();
            foreach (DataRow item in dm_template_detail.Rows) {
                if (item["IS_PR_KEY"].ToString() == "1") {
                    keycolumnlist.Add(item["DATA_COLUMN_NAME"].ToString());
                }
            }

            object[] keys = new object[keycolumnlist.Count];
            bool start = true;
            //for (int rowno = 0; rowno < excelData.Rows.Count; rowno++) {
            //    DataRow row = excelData.Rows[rowno];
            //    if (this.IsValidExcelData(row, dm_template_detail)) {
            //        if (start || !this.IsSameKey(row, keycolumnlist, keys)) {
            //            this.AddNewData();
            //        }

            //        if (this.ObjectList[0].IsValidRow(0)) {
            //            DataRow detailrow;
            //            if (start || !this.IsSameKey(row, keycolumnlist, keys)) {
            //                detailrow = this.ObjectList[1].DataTable.Rows[0];
            //            } else {
            //                detailrow = this.AddNewDetail(1);
            //            }

            //            foreach (DataColumn c in excelData.Columns) {
            //                DataRow templaterow = dm_template_detail.Rows.Find(c.ColumnName);
            //                if (this.ObjectList[0].DataTable.Columns.IndexOf(c.ColumnName) >= 0) {
            //                    if (templaterow != null && templaterow["DATA_TYPE"].ToString().ToUpper() == "DATE" && dateasstring) {
            //                        try {
            //                            this.ObjectList[0].DataTable.Rows[0][c.ColumnName] =
            //                                Convert.ToDateTime(row[c.ColumnName], this.FTSMain.FTSCultureInfo);
            //                        } catch (Exception) {
            //                            this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = this.mFTSMain.DayStartOfFirstYear;
            //                        }
            //                    } else {
            //                        if (templaterow["DATA_TYPE"].ToString().ToUpper() == "DATE") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = Convert.ToDateTime(row[c.ColumnName],
            //                                    this.FTSMain.FTSCultureInfo);
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = this.mFTSMain.DayStartOfFirstYear;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "STRING") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = row[c.ColumnName].ToString().Trim();
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = string.Empty;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "DECIMAL") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = Convert.ToDecimal(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "INT32") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = Convert.ToInt32(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "INT64") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = Convert.ToInt64(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "BOOLEAN") {
            //                            try {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = Convert.ToBoolean(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                this.ObjectList[0].DataTable.Rows[0][c.ColumnName] = 0;
            //                            }
            //                        }
            //                    }
            //                }

            //                if (this.ObjectList[1].DataTable.Columns.IndexOf(c.ColumnName) >= 0) {
            //                    if (templaterow != null && templaterow["DATA_TYPE"].ToString().ToUpper() == "DATE" && dateasstring) {
            //                        try {
            //                            detailrow[c.ColumnName] = Convert.ToDateTime(row[c.ColumnName], this.FTSMain.FTSCultureInfo);
            //                        } catch (Exception) {
            //                            detailrow[c.ColumnName] = this.mFTSMain.DayStartOfFirstYear;
            //                        }
            //                    } else {
            //                        if (templaterow["DATA_TYPE"].ToString().ToUpper() == "DATE") {
            //                            try {
            //                                detailrow[c.ColumnName] = Convert.ToDateTime(row[c.ColumnName], this.FTSMain.FTSCultureInfo);
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = this.mFTSMain.DayStartOfFirstYear;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "STRING") {
            //                            try {
            //                                detailrow[c.ColumnName] = row[c.ColumnName].ToString().Trim();
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = string.Empty;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "DECIMAL") {
            //                            try {
            //                                detailrow[c.ColumnName] = Convert.ToDecimal(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "INT32") {
            //                            try {
            //                                detailrow[c.ColumnName] = Convert.ToInt32(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "INT64") {
            //                            try {
            //                                detailrow[c.ColumnName] = Convert.ToInt64(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = 0;
            //                            }
            //                        } else if (templaterow["DATA_TYPE"].ToString().ToUpper() == "BOOLEAN") {
            //                            try {
            //                                detailrow[c.ColumnName] = Convert.ToBoolean(row[c.ColumnName]);
            //                            } catch (Exception) {
            //                                detailrow[c.ColumnName] = 0;
            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //            for (int i = 0; i < keycolumnlist.Count; i++) {
            //                keys[i] = row[keycolumnlist[i]];
            //            }

            //            start = false;
            //            listAddedTemp.Add(row);
            //            DataRow nextrow = null;
            //            if (rowno < excelData.Rows.Count - 1) {
            //                nextrow = excelData.Rows[rowno + 1];
            //            }

            //            if (nextrow != null && this.IsSameKey(nextrow, keycolumnlist, keys)) { } else {
            //                try {
            //                    this.UpdateImportRecord();
            //                } catch (FTSException ex) {
            //                    string msg = string.Empty;
            //                    if (ex.InnerException != null) {
            //                        msg = ex.InnerException.Message;
            //                    } else {
            //                        msg = ex.Message;
            //                    }
            //                    foreach (DataRow row1 in listAddedTemp) {
            //                        row1["IMPORT_STATUS"] = ImportStatus.FAIL;
            //                        row1["IMPORT_MSG"] = msg;
            //                    }
            //                    listAddedTemp.Clear();
            //                }
            //                foreach (DataRow row1 in listAddedTemp) {
            //                    listAdded.Add(row1);
            //                }
            //            }
            //        }
            //    } else {
            //        row["IMPORT_STATUS"] = ImportStatus.FAIL;
            //        row["IMPORT_MSG"] = "MSG_INVAID_EXCEL_DATA";
            //    }
            //}

            foreach (DataRow row in listAdded) {
                row.Delete();
            }

            excelData.AcceptChanges();
        }

        //public void LogError(FTSMain mFTSMain, FTSException ex, string strErr) {
        //    try {
        //        // tao hoac tinh bien fileName
        //        string fileName = DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString() + ".txt";
        //        // tao file de ghi loi
        //        fileName = Functions.GetPathName() + "Exceptions\\" + fileName;
        //        if (Functions.FileExists(fileName)) {
        //            FileInfo fileinfo = new FileInfo(fileName);
        //            if (fileinfo.Length > 2000000) {
        //                Functions.FileDelete(fileName);
        //            }
        //        }

        //        FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
        //        StreamWriter m_streamWriter = new StreamWriter(fs);
        //        m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
        //        // ghi ngay thang
        //        m_streamWriter.WriteLine(string.Empty);
        //        m_streamWriter.WriteLine("{0} {1}", DateTime.Today.ToLongTimeString(), DateTime.Today.ToLongDateString());
        //        // ghi Message
        //        string msg = strErr;
        //        if (ex.SystemException != null) {
        //            m_streamWriter.WriteLine(msg);
        //            // ghi Source
        //            m_streamWriter.WriteLine(ex.SystemException.Source);
        //            // ghi StackTrace
        //            m_streamWriter.WriteLine(ex.SystemException.StackTrace);
        //            if (ex.TableName != string.Empty) {
        //                m_streamWriter.WriteLine(ex.TableName);
        //            }

        //            if (ex.FieldName != string.Empty) {
        //                m_streamWriter.WriteLine(ex.FieldName);
        //            }
        //        } else {
        //            m_streamWriter.WriteLine(msg);
        //            if (ex.TableName != string.Empty) {
        //                m_streamWriter.WriteLine(ex.TableName);
        //            }

        //            if (ex.FieldName != string.Empty) {
        //                m_streamWriter.WriteLine(ex.FieldName);
        //            }
        //        }

        //        m_streamWriter.Flush();
        //        fs.Close();
        //        //
        //    } catch (Exception) { }
        //}

        protected virtual void UpdateImportRecord() {
            this.UpdateData();
        }

        protected bool IsValidExcelData(DataRow row, DataTable dm_template_detail) {
            foreach (DataColumn c in row.Table.Columns) {
                string columnname = c.ColumnName;
                DataRow templaterow = dm_template_detail.Rows.Find(columnname);
                if (templaterow != null) {
                    switch (templaterow["DATA_TYPE"].ToString().ToUpper()) {
                        case "STRING":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = string.Empty;
                            }

                            break;
                        case "DECIMAL":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = 0;
                            } else {
                                try {
                                    decimal cellvalue = Convert.ToDecimal(row[c.ColumnName]);
                                } catch (Exception) {
                                    return false;
                                }
                            }

                            break;
                        case "INT32":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = 0;
                            } else {
                                try {
                                    Int32 cellvalue = Convert.ToInt32(row[c.ColumnName]);
                                } catch (Exception) {
                                    return false;
                                }
                            }

                            break;
                        case "INT64":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = 0;
                            } else {
                                try {
                                    Int64 cellvalue = Convert.ToInt64(row[c.ColumnName]);
                                } catch (Exception) {
                                    return false;
                                }
                            }

                            break;
                        case "BOOLEAN":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = 0;
                            } else {
                                try {
                                    Int16 cellvalue = Convert.ToInt16(row[c.ColumnName]);
                                } catch (Exception) {
                                    return false;
                                }
                            }

                            break;
                        case "DATE":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty) {
                                row[c.ColumnName] = 0;
                            } else {
                                if ((bool) this.FTSMain.SystemVars.GetSystemVars("EXCEL_TEMPLATE_DATE_AS_STRING")) {
                                    try {
                                        DateTime cellvalue = Convert.ToDateTime(row[c.ColumnName], this.FTSMain.FTSCultureInfo);
                                    } catch (Exception) {
                                        return false;
                                    }
                                } else {
                                    try {
                                        DateTime cellvalue = Convert.ToDateTime(row[c.ColumnName]);
                                    } catch (Exception) {
                                        return false;
                                    }
                                }
                            }

                            break;
                    }
                }
            }

            return true;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.DataSet != null) {
                    Functions.ClearDataSet(this.DataSet);
                }
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        public virtual void DeleteOtherData() { }

        public virtual bool ValidateImportData(string fieldname, object fieldvalue) {
            return true;
        }

        public virtual void ApplyDefaultValues() {
            if (this.ObjectList[0].IsValidRow(0)) {
                this.ObjectList[0].ApplyDefaultValues(this.ObjectList[0].DataTable.Rows[0]);
            }

            foreach (DataRow row in this.ObjectList[1].DataTable.Rows) {
                if (this.ObjectList[1].IsValidRow(row)) {
                    this.ObjectList[1].ApplyDefaultValues(row);
                }
            }
        }

        public virtual List<ObjectInfoBase> GetDataList(string tranid, string fieldlist, DateTime daystart, DateTime dayend, int pagenumber,
            string sortstring) {
            return null;
        }

        public virtual ManagerObjectInfoBase GetDataObject() {
            return null;
        }


        public string GetFile(Guid prkey) {
            string sql = "SELECT REPORT_FILE_DATA FROM SYS_TRAN_OUTPUT WHERE PR_KEY=" + this.FTSMain.BuildParameterName("PR_KEY");
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "PR_KEY", DbType.Guid, prkey);
            object oj = this.FTSMain.DbMain.ExecuteScalar(cmd);
            if (oj != null && oj != System.DBNull.Value) {
                byte[] _ByteArray = (byte[])oj;
                string filename = Functions.GetTempFilePathWithExtension(".repx");
                System.IO.FileStream _FileStream = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);
                _FileStream.Close();
                return filename;
            } else {
                return string.Empty;
            }
        }
    }
}