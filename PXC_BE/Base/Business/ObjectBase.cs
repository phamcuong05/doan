// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:47
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         ObjectBase.cs
// Project Item Filename:     ObjectBase.cs
// Project Item Kind:         Code
// Purpose:                   D:\FTS\FTSSolutionRelease\Base\Classes\ManagerBase.cs
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Security;
using FTS.Base.Systems;
using FTS.Base.Utilities;
using FTS.Tools;
using Microsoft.Practices.EnterpriseLibrary.Data;

#endregion

namespace FTS.Base.Business
{
    [Serializable]
    public class ObjectBase : ObjectBaseBase
    {
        [NonSerialized] private FTSMain mFTSMain;
        [NonSerialized] private DataSet mDataSet;
        [NonSerialized] private DataTable mDataTable;
        private string mTableName;
        private string mExcludedFieldList = string.Empty;
        private List<FieldInfo> mFieldCollection;
        private string mNameField = string.Empty;
        private string mFieldList = string.Empty;
        private string mCondittion = string.Empty;
        private FTSFunction mFTSFunction = null;
        private Hashtable mConfigHashTable = null;
        protected bool IsOrganizationFilter = false;
        internal bool IsDmOrganization = false;
        private bool mCheckIDStruct = true;
        private string mFilterFieldList = string.Empty;
        public string DataBySearchFieldList = string.Empty;

        public ObjectBase(FTSMain ftsmain, string tablename)
        {
            this.mFTSMain = ftsmain;
            this.mDataSet = new DataSet();
            this.mTableName = tablename;
            this.mExcludedFieldList = string.Empty;
            this.LoadEmptyData();
            this.SetIDNameFields();
            this.SetRole();
            this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.ViewAction, string.Empty);

        }

        public ObjectBase(FTSMain ftsmain, DataSet ds, string tablename, bool ismanagerobject)
        {
            this.mFTSMain = ftsmain;
            this.mDataSet = ds;
            this.mTableName = tablename;
            this.mExcludedFieldList = string.Empty;
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
            if (this.mDataTable == null)
            {
                this.LoadEmptyData();
            }

            if (ismanagerobject == false)
            {
                this.SetIDNameFields();
                this.SetRole();
                this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.ViewAction, string.Empty);
            }
        }

        #region Properties

        public FTSMain FTSMain
        {
            get { return this.mFTSMain; }
            set { this.mFTSMain = value; }
        }

        public string IdField
        {
            get
            {
                if (this.mFieldCollection == null)
                {
                    return string.Empty;
                }

                for (int i = 0; i < this.mFieldCollection.Count; i++)
                {
                    FieldInfo fi = this.mFieldCollection[i];
                    if ((fi).IsPrkey)
                    {
                        return fi.FieldName;
                    }
                }

                return string.Empty;
            }
        }

        public DbType IdFieldType
        {
            get
            {
                for (int i = 0; i < this.mFieldCollection.Count; i++)
                {
                    FieldInfo fi = this.mFieldCollection[i];
                    if ((fi).IsPrkey)
                    {
                        return fi.FieldType;
                    }
                }

                throw (new FTSException("MSG_NO_ID_FIELD"));
            }
        }

        public string FilterFieldList
        {
            get { return this.mFilterFieldList; }
            set { this.mFilterFieldList = value; }
        }

        public string TableName
        {
            get { return this.mTableName; }
            set { this.mTableName = value; }
        }


        public string NameField
        {
            get { return this.mNameField; }
            set { this.mNameField = value; }
        }

        public string FieldList
        {
            get { return this.mFieldList; }
            set { this.mFieldList = value; }
        }

        protected List<FieldInfo> FieldCollection
        {
            get { return this.mFieldCollection; }
            set { this.mFieldCollection = value; }
        }

        public string ExcludedFieldList
        {
            get { return this.mExcludedFieldList; }
            set { this.mExcludedFieldList = value; }
        }

        public string Condittion
        {
            get { return this.mCondittion; }
            set { this.mCondittion = value; }
        }

        public DataSet DataSet
        {
            get { return this.mDataSet; }
            set { this.mDataSet = value; }
        }

        public DataTable DataTable
        {
            get { return this.mDataTable; }
            set { this.mDataTable = value; }
        }


        public FTSFunction FTSFunction
        {
            get { return this.mFTSFunction; }
            set { this.mFTSFunction = value; }
        }


        public Hashtable ConfigHashTable
        {
            get { return this.mConfigHashTable; }
        }

        public bool CheckIdStruct
        {
            get { return this.mCheckIDStruct; }
            set { this.mCheckIDStruct = value; }
        }

        #endregion


        public virtual void LoadData()
        {
            string sql = "select * from " + this.mTableName;
            if (this.mFieldList.Length != 0)
            {
                sql = "select " + this.mFieldList + " from " + this.mTableName;
            }

            if (this.mCondittion.Length != 0)
            {
                if (this.IsOrganizationFilter)
                {
                    sql += " where " + this.mCondittion + " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }
                else
                {
                    sql += " where " + this.mCondittion + " AND " +
                           this.FTSMain.IdManager.Filter(this.mTableName, this.FTSMain.UserInfo.OrganizationID);
                }
            }
            else
            {
                if (this.IsOrganizationFilter)
                {
                    sql += " where " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }
                else
                {
                    sql += " where " + this.FTSMain.IdManager.Filter(this.mTableName, this.FTSMain.UserInfo.OrganizationID);
                }
            }

            if (this.IdField.Length != 0)
            {
                sql += " order by " + this.IdField;
            }

            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            this.mFTSMain.DbMain.LoadDataSet(this.mFTSMain.DbMain.GetSqlStringCommand(sql), this.mDataSet, this.mTableName);
        }


        public virtual void LoadEmptyData()
        {
            string sql = "select * from " + this.mTableName + " where 1=0";
            this.mFTSMain.DbMain.LoadDataSet(this.mFTSMain.DbMain.GetSqlStringCommand(sql), this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }


        public virtual void LoadDataByCommand(DbCommand cmd)
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadDataByCommand(DbCommand cmd, DbTransaction tran)
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName, tran);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadDataByID(object idvalue)
        {
            string sql = "select * from " + this.mTableName + " where " + this.IdField + " = " + this.mFTSMain.BuildParameterName(this.IdField);
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, idvalue);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void GetDataByFilter(List<FilterGroup> filterlist)
        {
            List<FilterGroup> localfiltergrouplist = new List<FilterGroup>();

            //mapping filters
            if (filterlist?.Count > 0)
            {
                for (int i = 0; i < filterlist.Count; i++)
                {
                    var group = filterlist[i];
                    for (int j = 0; j < group.Filters.Count; j++)
                    {
                        var filteritem = group.Filters[j];
                        var fieldInfo = this.FieldCollection.Find(x => x.FieldName.Equals(filteritem.Field, StringComparison.OrdinalIgnoreCase));
                        if (fieldInfo != null)
                        {
                            filteritem.DbType = fieldInfo.FieldType;
                            filteritem.Field = fieldInfo.FieldName;
                            filteritem.ParamName = $"@{fieldInfo.FieldName}_{i}{j}";
                        }
                    }

                    localfiltergrouplist.Add(group);
                }
            }

            string filter = this.GenerateFilter(localfiltergrouplist);

            string sql = "select * from " + this.mTableName + $" where {filter}";
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            foreach (var group in localfiltergrouplist)
            {
                foreach (var filtervalue in group.Filters)
                {
                    this.FTSMain.DbMain.AddInParameter(cmd, filtervalue.ParamName, filtervalue.DbType, filtervalue.Value);
                }
            }
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadDataByFrkey(object fr_key)
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            string sql = "select * from " + this.mTableName + " where fr_key= " + this.mFTSMain.BuildParameterName("fr_key");
            if (this.mDataTable != null && this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
            {
                sql += " order by list_order";
            }

            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, "fr_key", this.IdFieldType, fr_key);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadDataByFrkey(object fr_key, DbTransaction tran)
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }

            string sql = "select * from " + this.mTableName + " where fr_key= " + this.mFTSMain.BuildParameterName("fr_key");
            if (this.mDataTable != null && this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
            {
                sql += " order by list_order";
            }

            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, "fr_key", this.IdFieldType, fr_key);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName, tran);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadDataByReferencePrKey(object referenceprkey)
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }
            string sql = "select * from " + this.mTableName + " where REFERENCE_PR_KEY= " + this.mFTSMain.BuildParameterName("REFERENCE_PR_KEY");
            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, "REFERENCE_PR_KEY", this.IdFieldType, referenceprkey);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            this.mDataTable = this.mDataSet.Tables[this.mTableName];
        }

        public virtual void LoadNextRecord(object pr_key)
        {
            string extCondittion = string.Empty;
            if (this.mCondittion.Length != 0)
            {
                extCondittion = this.mCondittion + " and ";
            }

            this.Clear();
            string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " pr_key > " +
                         this.mFTSMain.BuildParameterName("pr_key") +
                         " order by pr_key asc";
            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, "pr_key", this.IdFieldType, pr_key);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            if (this.mDataTable.Rows.Count == 0)
            {
                sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " 1=1 order by pr_key desc";
                this.Clear();
                this.mFTSMain.DbMain.LoadDataSet(this.mFTSMain.DbMain.GetSqlStringCommand(sql), this.mDataSet, this.mTableName);
            }
        }

        public virtual void LoadRecord(object key)
        {
            string extCondittion = string.Empty;
            if (this.mCondittion.Length != 0)
            {
                extCondittion = this.mCondittion + " and ";
            }

            if (this.mDataTable.Rows.Count > 0)
            {
                this.Clear();
                string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + this.IdField + " = " +
                             this.mFTSMain.BuildParameterName(this.IdField);
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, key);
                this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            }
        }

        public virtual void LoadNextRecord()
        {
            string extCondittion = string.Empty;
            if (this.mCondittion.Length != 0)
            {
                if (this.IsOrganizationFilter)
                {
                    extCondittion = this.mCondittion + " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter() + " and ";
                }
                else
                {
                    extCondittion = this.mCondittion + " and " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID) +
                                    " and ";
                }
            }
            else
            {
                if (this.IsOrganizationFilter)
                {
                    extCondittion = this.FTSMain.DmOrganization.GetOrganizationFilter() + " and ";
                }
                else
                {
                    extCondittion = this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID) + " and ";
                }
            }

            if (this.mDataTable.Rows.Count > 0)
            {
                object key = this.mDataTable.Rows[0][this.IdField];
                this.Clear();
                string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + this.IdField + " > " +
                             this.mFTSMain.BuildParameterName(this.IdField) + " order by " + this.IdField + " asc";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, key);
                this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            }

            if (this.mDataTable.Rows.Count == 0)
            {
                string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " 1=1  order by " + this.IdField + " desc";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            }
        }

        public virtual void LoadPreviousRecord()
        {
            string extCondittion = string.Empty;
            if (this.mCondittion.Length != 0)
            {
                if (this.IsOrganizationFilter)
                {
                    extCondittion = this.mCondittion + " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter() + " and ";
                }
                else
                {
                    extCondittion = this.mCondittion + " and " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID) +
                                    " and ";
                }
            }
            else
            {
                if (this.IsOrganizationFilter)
                {
                    extCondittion = this.FTSMain.DmOrganization.GetOrganizationFilter() + " and ";
                }
                else
                {
                    extCondittion = this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID) + " and ";
                }
            }

            if (this.mDataTable.Rows.Count > 0)
            {
                object key = this.mDataTable.Rows[0][this.IdField];
                this.Clear();
                string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + this.IdField + " < " +
                             this.mFTSMain.BuildParameterName(this.IdField) + " order by " + this.IdField + " desc";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, key);
                this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            }

            if (this.mDataTable.Rows.Count == 0)
            {
                string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " 1=1  order by " + this.IdField + " asc";
                DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
                this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            }
        }

        public virtual void LoadPreviousRecord(object pr_key)
        {
            string extCondittion = string.Empty;
            if (this.mCondittion.Length != 0)
            {
                extCondittion = this.mCondittion + " and ";
            }

            this.Clear();
            string sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " pr_key < " +
                         this.mFTSMain.BuildParameterName("pr_key") +
                         " order by pr_key asc";
            DbCommand cmd = this.mFTSMain.DbMain.GetSqlStringCommand(sql);
            this.mFTSMain.DbMain.AddInParameter(cmd, "pr_key", this.IdFieldType, pr_key);
            this.mFTSMain.DbMain.LoadDataSet(cmd, this.mDataSet, this.mTableName);
            if (this.mDataTable.Rows.Count == 0)
            {
                sql = "select top 1 * from " + this.mTableName + " where " + extCondittion + " 1=1 order by pr_key asc";
                this.Clear();
                this.mFTSMain.DbMain.LoadDataSet(this.mFTSMain.DbMain.GetSqlStringCommand(sql), this.mDataSet, this.mTableName);
            }
        }

        public virtual DataRow AddNew() {
            DataRow nr = this.mDataTable.NewRow();
            bool pr_key = false;
            foreach (FieldInfo c in this.mFieldCollection) {
                if (c.IsBound) {
                    if (c.FieldName == "PR_KEY") {
                        nr[c.FieldName] = Guid.NewGuid();
                        pr_key = true;
                    } else {
                        if (c.FieldName == "ORGANIZATION_ID") {
                            if (!this.IsDmOrganization) {
                                nr[c.FieldName] = this.mFTSMain.UserInfo.OrganizationID;
                            } else {
                                nr[c.FieldName] = string.Empty;
                            }
                        } else {
                            if (c.FieldName == "ORGANIZATION_NAME") {
                                if (!this.IsDmOrganization) {
                                    nr[c.FieldName] = this.mFTSMain.UserInfo.OrganizationName;
                                } else {
                                    nr[c.FieldName] = string.Empty;
                                }
                            } else {
                                if (c.FieldName == "USER_ID") {
                                    nr[c.FieldName] = this.mFTSMain.UserInfo.UserID;
                                } else {
                                    if (c.FieldName == "MODIFY_DATE") {
                                        nr[c.FieldName] = DateTime.Now;
                                    } else {
                                        if (c.FieldName == "CREATE_DATE") {
                                            nr[c.FieldName] = DateTime.Now;
                                        } else {
                                            if (c.FieldName == "ACTIVE") {
                                                nr[c.FieldName] = 1;
                                            } else {
                                                if (c.FieldName == "LIST_ORDER") {
                                                    nr[c.FieldName] = this.GetLastOrder() + 1;
                                                } else {
                                                    if (!c.AllowDbNull) {
                                                        if (c.FieldType == DbType.String) {
                                                            nr[c.FieldName] = string.Empty;
                                                        } else {
                                                            if (c.FieldType == DbType.Date) {
                                                                nr[c.FieldName] = DateTime.Today.Date;
                                                            } else {
                                                                if (c.FieldType == DbType.Guid) {
                                                                    nr[c.FieldName] = Guid.Empty;
                                                                } else {
                                                                    nr[c.FieldName] = 0;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    object defaultvalue = this.GetDefaultValue(c.FieldName);
                    if (defaultvalue != null) {
                        nr[c.FieldName] = defaultvalue;
                    }
                }
            }

            if ((!pr_key) && (this.IdFieldType == DbType.String)) {
                string idauto = this.mFTSMain.TableManager.GetIdAuto(this.mTableName);
                if (idauto != string.Empty) {
                    nr[this.IdField] = idauto;
                }
            }

            this.mDataTable.Rows.Add(nr);
            return nr;
        }

        public virtual void ApplyDefaultValues(DataRow nr)
        {
            foreach (FieldInfo c in this.mFieldCollection)
            {
                object defaultvalue = this.GetDefaultValue(c.FieldName);
                if (defaultvalue != null)
                {
                    nr[c.FieldName] = defaultvalue;
                }
            }
        }

        public virtual DataRow AddNewRowNotInTable()
        {
            DataRow nr = this.mDataTable.NewRow();
            bool pr_key = false;
            foreach (FieldInfo c in this.mFieldCollection)
            {
                if (c.IsBound)
                {
                    if (c.FieldName == "PR_KEY")
                    {
                        nr[c.FieldName] = Guid.NewGuid();
                        pr_key = true;
                    }
                    else
                    {
                        if (c.FieldName == "ORGANIZATION_ID")
                        {
                            if (!this.IsDmOrganization)
                            {
                                nr[c.FieldName] = this.mFTSMain.UserInfo.OrganizationID;
                            }
                            else
                            {
                                nr[c.FieldName] = string.Empty;
                            }
                        }
                        else
                        {
                            if (c.FieldName == "USER_ID")
                            {
                                nr[c.FieldName] = this.mFTSMain.UserInfo.UserID;
                            }
                            else
                            {
                                if (c.FieldName == "MODIFY_DATE")
                                {
                                    nr[c.FieldName] = DateTime.Now;
                                }
                                else
                                {
                                    if (c.FieldName == "CREATE_DATE")
                                    {
                                        nr[c.FieldName] = DateTime.Now;
                                    }
                                    else
                                    {
                                        if (c.FieldName == "ACTIVE")
                                        {
                                            nr[c.FieldName] = 1;
                                        }
                                        else
                                        {
                                            if (c.FieldName == "LIST_ORDER")
                                            {
                                                nr[c.FieldName] = this.GetLastOrder() + 1;
                                            }
                                            else
                                            {
                                                if (!c.AllowDbNull)
                                                {
                                                    if (c.FieldType == DbType.String)
                                                    {
                                                        nr[c.FieldName] = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        if (c.FieldType == DbType.Date)
                                                        {
                                                            nr[c.FieldName] = DateTime.Today.Date;
                                                        }
                                                        else
                                                        {
                                                            if (c.FieldType == DbType.Guid)
                                                            {
                                                                nr[c.FieldName] = Guid.Empty;
                                                            }
                                                            else
                                                            {
                                                                nr[c.FieldName] = 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    object defaultvalue = this.GetDefaultValue(c.FieldName);
                    if (defaultvalue != null)
                    {
                        nr[c.FieldName] = defaultvalue;
                    }
                }
            }

            if ((!pr_key) && (this.IdFieldType == DbType.String))
            {
                string idauto = this.mFTSMain.TableManager.GetIdAuto(this.mTableName);
                if (idauto != string.Empty)
                {
                    nr[this.IdField] = idauto;
                }
            }

            return nr;
        }

        public virtual object GetDefaultValue(string fieldname)
        {
            return null;
        }

        public virtual DataRow AddNew(DataRow row)
        {
            DataRow nr = this.mDataTable.NewRow();
            bool pr_key = false;
            if (row.Table.Columns.Count == this.mDataTable.Columns.Count)
            {
                nr.ItemArray = (object[])row.ItemArray.Clone();
                foreach (FieldInfo c in this.mFieldCollection)
                {
                    if (c.IsBound)
                    {
                        if (c.FieldName == "PR_KEY")
                        {
                            nr[c.FieldName] = Guid.NewGuid();
                            pr_key = true;
                        }
                        else
                        {
                            if (c.FieldName == "LIST_ORDER")
                            {
                                nr[c.FieldName] = this.GetLastOrder() + 1;
                            }
                            else
                            {
                                if (c.FieldName == "ORGANIZATION_ID")
                                {
                                    if (!this.IsDmOrganization)
                                    {
                                        nr[c.FieldName] = this.FTSMain.UserInfo.OrganizationID;
                                    }
                                    else
                                    {
                                        nr[c.FieldName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    if (c.FieldName == "USER_ID")
                                    {
                                        nr[c.FieldName] = this.FTSMain.UserInfo.UserID;
                                    }
                                    else
                                    {
                                        if (c.FieldName == "MODIFY_DATE")
                                        {
                                            nr[c.FieldName] = DateTime.Now;
                                        }
                                        else
                                        {
                                            if (c.FieldName == "CREATE_DATE")
                                            {
                                                nr[c.FieldName] = DateTime.Now;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataColumn c in this.mDataTable.Columns)
                {
                    if (row.Table.Columns.IndexOf(c.ColumnName) >= 0)
                    {
                        if (c.ColumnName == "PR_KEY")
                        {
                            nr[c.ColumnName] = Guid.NewGuid();
                        }
                        else
                        {
                            if (c.ColumnName == "LIST_ORDER")
                            {
                                nr[c.ColumnName] = this.GetLastOrder() + 1;
                            }
                            else
                            {
                                if (c.ColumnName == "ORGANIZATION_ID")
                                {
                                    if (this.IsDmOrganization)
                                    {
                                        nr[c.ColumnName] = this.FTSMain.UserInfo.OrganizationID;
                                    }
                                    else
                                    {
                                        nr[c.ColumnName] = string.Empty;
                                    }
                                }
                                else
                                {
                                    if (c.ColumnName == "USER_ID")
                                    {
                                        nr[c.ColumnName] = this.FTSMain.UserInfo.UserID;
                                    }
                                    else
                                    {
                                        if (c.ColumnName == "MODIFY_DATE")
                                        {
                                            nr[c.ColumnName] = DateTime.Now;
                                        }
                                        else
                                        {
                                            if (c.ColumnName == "CREATE_DATE")
                                            {
                                                nr[c.ColumnName] = DateTime.Now;
                                            }
                                            else
                                            {
                                                nr[c.ColumnName] = row[c.ColumnName];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if ((!pr_key) && (this.IdFieldType == DbType.String))
            {
                string idauto = this.mFTSMain.TableManager.GetIdAuto(this.mTableName);
                if (idauto != string.Empty)
                {
                    nr[this.IdField] = idauto;
                }
            }

            this.mDataTable.Rows.Add(nr);
            return nr;
        }

        public virtual DataRow AddRow(DataRow row)
        {
            DataRow nr = this.mDataTable.NewRow();
            if (row.Table.Columns.Count == this.mDataTable.Columns.Count)
            {
                nr.ItemArray = (object[])row.ItemArray.Clone();
            }
            else
            {
                foreach (DataColumn c in this.mDataTable.Columns)
                {
                    if (row.Table.Columns.IndexOf(c.ColumnName) >= 0)
                    {
                        nr[c.ColumnName] = row[c.ColumnName];
                    }
                }
            }

            this.mDataTable.Rows.Add(nr);
            return nr;
        }

        public virtual DataRow AddRowOtherSource(DataRow row)
        {
            DataRow nr = this.AddNew();
            foreach (DataColumn c in this.mDataTable.Columns)
            {
                if (row.Table.Columns.IndexOf(c.ColumnName) >= 0 && nr.Table.Columns[c.ColumnName].DataType != Type.GetType("System.Guid"))
                {
                    nr[c.ColumnName] = row[c.ColumnName];
                }
            }

            return nr;
        }

        public virtual void AcceptChanges()
        {
            this.mDataTable.AcceptChanges();
        }

        public virtual DataRow CopyRecord(int pos)
        {
            if (pos >= 0)
            {
                DataRow nr = this.mDataTable.NewRow();
                bool pr_key = false;
                nr.ItemArray = (object[])this.mDataTable.Rows[pos].ItemArray.Clone();
                if (this.mDataTable.Columns.IndexOf("PR_KEY") >= 0)
                {
                    nr["pr_key"] = Guid.NewGuid();
                    pr_key = true;
                }

                if (this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
                {
                    nr["LIST_ORDER"] = this.GetLastOrder() + 1;
                }

                if (this.mDataTable.Columns.IndexOf("ORGANIZATION_ID") >= 0)
                {
                    if (!this.IsDmOrganization)
                    {
                        nr["ORGANIZATION_ID"] = this.FTSMain.UserInfo.OrganizationID;
                    }
                    else
                    {
                        nr["ORGANIZATION_ID"] = string.Empty;
                    }
                }

                if (this.mDataTable.Columns.IndexOf("MODIFY_DATE") >= 0)
                {
                    nr["MODIFY_DATE"] = DateTime.Now;
                }

                if (this.mDataTable.Columns.IndexOf("CREATE_DATE") >= 0)
                {
                    nr["CREATE_DATE"] = DateTime.Now;
                }

                if (this.mDataTable.Columns.IndexOf("USER_ID") >= 0)
                {
                    nr["USER_ID"] = this.FTSMain.UserInfo.UserID;
                }

                if ((!pr_key) && (this.IdFieldType == DbType.String))
                {
                    string idauto = this.mFTSMain.TableManager.GetIdAuto(this.mTableName);
                    if (idauto != string.Empty)
                    {
                        nr[this.IdField] = idauto;
                    }
                    else
                    {
                        nr[this.IdField] = string.Empty;
                    }
                }

                this.mDataTable.Clear();
                this.mDataTable.AcceptChanges();
                this.mDataTable.Rows.Add(nr);
                return nr;
            }
            else
            {
                return null;
            }
        }

        public virtual DataRow InsertRecord(int pos)
        {
            DataRow nr1 = this.AddNew();
            DataRow nr = this.DataTable.NewRow();
            nr.ItemArray = (object[])nr1.ItemArray.Clone();
            this.DataTable.Rows.Remove(nr1);
            if (pos >= 0)
            {
                this.mDataTable.Rows.InsertAt(nr, pos);
                if (this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
                {
                    for (int i = 0; i < this.mDataTable.Rows.Count; i++)
                    {
                        if (this.mDataTable.Rows[i].RowState != DataRowState.Deleted)
                        {
                            this.mDataTable.Rows[i]["LIST_ORDER"] = i;
                        }
                    }
                }

                return nr;
            }
            else
            {
                if (this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
                {
                    nr["list_order"] = 1;
                }

                this.mDataTable.Rows.Add(nr);
                return nr;
            }
        }

        public virtual DataRow CopyRecord(DataRow row)
        {
            if (row != null)
            {
                DataRow nr = this.mDataTable.NewRow();
                bool pr_key = false;
                nr.ItemArray = (object[])row.ItemArray.Clone();
                if (this.mDataTable.Columns.IndexOf("PR_KEY") >= 0)
                {
                    nr["PR_KEY"] = Guid.NewGuid();
                    pr_key = true;
                }

                if (this.IdFieldType == DbType.Decimal || this.IdFieldType == DbType.Int32 || this.IdFieldType == DbType.Int16)
                {
                    nr[this.IdField] = 0;
                }
                else
                {
                    if (this.IdFieldType == DbType.String)
                    {
                        nr[this.IdField] = string.Empty;
                    }
                }

                if (this.mDataTable.Columns.IndexOf("LIST_ORDER") >= 0)
                {
                    nr["LIST_ORDER"] = this.GetLastOrder() + 1;
                }

                if (this.mDataTable.Columns.IndexOf("ORGANIZATION_ID") >= 0)
                {
                    if (!this.IsDmOrganization)
                    {
                        nr["ORGANIZATION_ID"] = this.FTSMain.UserInfo.OrganizationID;
                    }
                    else
                    {
                        nr["ORGANIZATION_ID"] = string.Empty;
                    }
                }

                if ((!pr_key) && (this.IdFieldType == DbType.String))
                {
                    string idauto = this.mFTSMain.TableManager.GetIdAuto(this.mTableName);
                    if (idauto != string.Empty)
                    {
                        nr[this.IdField] = idauto;
                    }
                }

                this.mDataTable.Rows.Add(nr);
                return nr;
            }
            else
            {
                return null;
            }
        }

        public virtual void UpdateData(DataSet ds, DbTransaction transaction)
        {
            if (this.mFTSFunction != null && this.DataTable.Rows.Count > 0)
            {
                if (this.DataTable.Rows[0].RowState == DataRowState.Added)
                {
                    this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, string.Empty);
                }
                else
                {
                    if (this.DataTable.Rows[0].RowState == DataRowState.Deleted)
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, string.Empty);
                    }
                    else
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, string.Empty);
                    }
                }
            }

            this.mFTSMain.DbMain.UpdateDataSet(ds, this.mTableName,
                this.mFTSMain.DbMain.CreateInsertCommand(this.mTableName, this.mDataTable, this.mExcludedFieldList),
                this.mFTSMain.DbMain.CreateUpdateCommand(this.mTableName, this.mDataTable, this.IdField, this.mExcludedFieldList),
                this.mFTSMain.DbMain.CreateDeleteCommand(this.mTableName, this.mDataTable, this.IdField), transaction);

        }

        public virtual void UpdateData(DataSet ds)
        {
            if (this.mFTSFunction != null && this.DataTable.Rows.Count > 0)
            {
                if (this.DataTable.Rows[0].RowState == DataRowState.Added)
                {
                    this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, string.Empty);
                }
                else
                {
                    if (this.DataTable.Rows[0].RowState == DataRowState.Deleted)
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, string.Empty);
                    }
                    else
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, string.Empty);
                    }
                }
            }

            this.mFTSMain.DbMain.UpdateDataSet(ds, this.mTableName,
                this.mFTSMain.DbMain.CreateInsertCommand(this.mTableName, this.mDataTable, this.mExcludedFieldList),
                this.mFTSMain.DbMain.CreateUpdateCommand(this.mTableName, this.mDataTable, this.IdField, this.mExcludedFieldList),
                this.mFTSMain.DbMain.CreateDeleteCommand(this.mTableName, this.mDataTable, this.IdField), UpdateBehavior.Standard);
        }

        public virtual void UpdateData()
        {
            DbTransaction tran = null;
            try
            {
                this.EndEdit();
                this.CheckBusinessRules();
                if (this.mFTSFunction != null && this.IsValidRow(0))
                {
                    if (this.DataTable.Rows[0].RowState == DataRowState.Added)
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, string.Empty);
                    }
                    else
                    {
                        if (this.DataTable.Rows[0].RowState == DataRowState.Deleted)
                        {
                            this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, string.Empty);
                        }
                        else
                        {
                            if (this.DataTable.Rows[0].RowState == DataRowState.Modified)
                            {
                                this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, string.Empty);
                            }
                        }
                    }
                }


                this.mFTSMain.DbMain.UpdateDataSet(this.mDataSet, this.mTableName,
                    this.mFTSMain.DbMain.CreateInsertCommand(this.mTableName, this.mDataTable, this.mExcludedFieldList),
                    this.mFTSMain.DbMain.CreateUpdateCommand(this.mTableName, this.mDataTable, this.IdField, this.mExcludedFieldList),
                    this.mFTSMain.DbMain.CreateDeleteCommand(this.mTableName, this.mDataTable, this.IdField), UpdateBehavior.Standard);
                this.mDataSet.Tables[this.mTableName].AcceptChanges();

            }
            catch (Exception ex)
            {
                try
                {
                    tran.Rollback();
                }
                catch (Exception) { }

                this.FTSMain.ExceptionManager.HandlingDbException(ex, this.DataTable, this.TableName, this.IdField);
                throw;
            }
        }

        public virtual void UpdateData(DbTransaction tran)
        {
            try
            {
                this.EndEdit();
                this.CheckBusinessRules();
                if (this.mFTSFunction != null && this.IsValidRow(0))
                {
                    if (this.DataTable.Rows[0].RowState == DataRowState.Added)
                    {
                        this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.AddAction, string.Empty);
                    }
                    else
                    {
                        if (this.DataTable.Rows[0].RowState == DataRowState.Deleted)
                        {
                            this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.DeleteAction, string.Empty);
                        }
                        else
                        {
                            if (this.DataTable.Rows[0].RowState == DataRowState.Modified)
                            {
                                this.FTSMain.SecurityManager.CheckSecurity(this.mFTSFunction, DataAction.EditAction, string.Empty);
                            }
                        }
                    }
                }

                this.mFTSMain.DbMain.UpdateDataSet(this.mDataSet, this.mTableName,
                    this.mFTSMain.DbMain.CreateInsertCommand(this.mTableName, this.mDataTable, this.mExcludedFieldList),
                    this.mFTSMain.DbMain.CreateUpdateCommand(this.mTableName, this.mDataTable, this.IdField, this.mExcludedFieldList),
                    this.mFTSMain.DbMain.CreateDeleteCommand(this.mTableName, this.mDataTable, this.IdField), tran);

                this.mDataSet.Tables[this.mTableName].AcceptChanges();
            }
            catch (Exception ex)
            {
                this.FTSMain.ExceptionManager.HandlingDbException(ex, this.DataTable, this.TableName, this.IdField);
                throw;
            }
        }

        public virtual void EndEdit()
        {
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    row.EndEdit();
                }
            }
        }

        public virtual void CheckBusinessRules()
        {
            bool keystring = false;
            if (this.IdField.Length != 0 && this.IdFieldType == DbType.String)
            {
                keystring = true;
            }

            int pos = 0;
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                    {
                        foreach (FieldInfo c in this.mFieldCollection)
                        {
                            if (c.IsRequired && this.mDataTable.Columns.IndexOf(c.FieldName) >= 0 && row[c.FieldName].ToString().Trim().Length == 0)
                            {
                                throw (new FTSException(null, "DATA_EMPTY_FIELD", this.mTableName, c.FieldName, pos));
                            }

                            if (row.Table.Columns.IndexOf(c.FieldName) >= 0)
                            {
                                if (row[c.FieldName] != System.DBNull.Value && (c.FieldType == DbType.Currency) &&
                                    (this.mDataTable.Columns.IndexOf(c.FieldName) >= 0))
                                {
                                    try
                                    {
                                        if ((Convert.ToDecimal(row[c.FieldName])) < (Decimal)(-922337203685477.5808) ||
                                            (Convert.ToDecimal(row[c.FieldName])) > (Decimal)(922337203685477.5807))
                                        {
                                            throw (new FTSException(null, "DATA_NUMBER_OVERFLOW", this.mTableName, c.FieldName, pos));
                                        }
                                    }
                                    catch (Exception e1)
                                    {
                                        int i = 3;
                                        throw e1;
                                    }
                                }
                            }
                        }

                        if (keystring)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                            {
                                string idvalue = row[this.IdField].ToString().Replace(" ", string.Empty);
                                if (row[this.IdField].ToString() != idvalue)
                                {
                                    row[this.IdField] = idvalue;
                                    row.EndEdit();
                                }

                                if (row[this.IdField].ToString() == string.Empty)
                                {
                                    throw (new FTSException(null, "EMPTY_ID_FIELD", this.mTableName, this.IdField, pos));
                                }

                                if (this.NameField != string.Empty && row[this.NameField].ToString() == string.Empty)
                                {
                                    throw (new FTSException(null, "EMPTY_NAME_FIELD", this.mTableName, this.NameField, pos));
                                }

                                if (Functions.IsTiengViet(row[this.IdField].ToString()))
                                {
                                    FTSMessageBox.ShowErrorMessage(row[this.IdField].ToString());
                                    throw (new FTSException(null, "DATA_SPECIAL_CHARACTER", this.mTableName, this.IdField, pos));
                                }

                                try
                                {
                                    if (this.CheckIdStruct)
                                    {
                                        if (row.RowState == DataRowState.Added)
                                        {
                                            this.FTSMain.IdManager.Validate(this.DataTable, this.mTableName, (string)row[this.IdField]);
                                        }
                                        else
                                        {
                                            this.FTSMain.IdManager.Validate(this.DataTable, this.mTableName,
                                                (string)row[this.IdField, DataRowVersion.Original]);
                                        }
                                    }
                                }
                                catch (FTSException ex)
                                {
                                    throw (new FTSException(null, ex.ExceptionID, this.mTableName, this.IdField, pos));
                                }
                            }
                        }
                    }

                    pos++;
                }
                else
                {
                    if (keystring && row.RowState == DataRowState.Deleted)
                    {
                        try
                        {
                            if (this.CheckIdStruct)
                            {
                                this.FTSMain.IdManager.ValidateDelete(this.DataTable, this.mTableName,
                                    (string)row[this.IdField, DataRowVersion.Original]);
                            }
                        }
                        catch (FTSException ex)
                        {
                            throw (new FTSException(null, ex.ExceptionID, this.mTableName, this.IdField, pos));
                        }
                    }

                    pos++;
                }
            }
        }

        public virtual void DeleteAll()
        {
            base.DeleteAllBase(this.mDataTable);
        }

        public virtual void DeleteAll(object fr_key)
        {
            base.DeleteAllBase(this.DataTable, fr_key);
        }

        public virtual void DeleteAllWithFilter(string filter)
        {
            List<DataRow> list = new List<DataRow>();
            DataView dv = new DataView(this.DataTable, filter, "", DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                list.Add(drv.Row);
            }

            foreach (DataRow row in list)
            {
                row.Delete();
            }
        }

        public virtual void DeleteWithID(object rowKeyValue)
        {
            if (this.IdField != null)
            {
                DataColumn[] oldprimarykey = this.mDataTable.PrimaryKey;
                this.mDataTable.PrimaryKey = new DataColumn[] { this.mDataTable.Columns[this.IdField] };
                DataRow nr = this.mDataTable.Rows.Find(rowKeyValue);
                if ((nr != null) && (nr.RowState != DataRowState.Deleted))
                {
                    nr.Delete();
                }

                this.mDataTable.PrimaryKey = oldprimarykey;
            }
        }

        public virtual void DeleteInData(object idvalue)
        {
            if (this.IdField != null)
            {
                this.LoadDataByID(idvalue);
                this.DeleteAll();
                this.UpdateData();
            }
        }


        public virtual void DeleteRow(DataRow row)
        {
            if (this.IsValidRow(row))
            {
                row.Delete();
            }
        }

        public virtual void DeleteAtPosition(int position)
        {
            if (this.IsValidRow(position))
            {
                this.mDataTable.Rows[position].Delete();
            }
        }

        public virtual void Clear()
        {
            if (this.mDataTable != null)
            {
                this.mDataTable.Clear();
            }
        }

        public virtual void Restore()
        {
            this.mDataTable.RejectChanges();
        }

        public virtual bool HasChanged()
        {
            return this.mDataSet.HasChanges();
        }

        public virtual void SetIDNameFields()
        {
            this.mNameField = this.mFTSMain.TableManager.GetNameField(this.mTableName);
        }

        protected virtual int GetLastOrder()
        {
            DataView dv = new DataView(this.mDataTable, string.Empty, "LIST_ORDER desc", DataViewRowState.CurrentRows);
            if (dv.Count > 0)
            {
                return Convert.ToInt32(dv[0]["list_order"]);
            }
            else
            {
                return 0;
            }
        }

        public DataRow GetRowWithID(object id)
        {
            if (this.mDataTable.PrimaryKey == null || this.mDataTable.PrimaryKey.Length == 0)
            {
                DataView dv = new DataView(this.mDataTable, string.Empty, this.IdField, DataViewRowState.CurrentRows);
                DataRowView[] drv = dv.FindRows(id);
                if (drv.Length > 0)
                {
                    return drv[0].Row;
                }
                else
                {
                    DataTable dt =
                        this.mFTSMain.DbMain.LoadDataTable(
                            this.mFTSMain.DbMain.GetSqlStringCommand("SELECT * FROM " + this.mTableName + " WHERE " + this.IdField + "='" + id + "'"),
                            this.mTableName);
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return this.mDataTable.Rows.Find(id);
            }
        }

        public virtual int GetRowIndex(DataRow row)
        {
            IEnumerator ice = this.mDataTable.Rows.GetEnumerator();
            ice.Reset();
            int i = 0;
            while (ice.MoveNext())
            {
                if (((ice.Current)) == row)
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        public virtual FieldInfo GetFieldInfo(string fieldname)
        {
            for (int i = 0; i < this.mFieldCollection.Count; i++)
            {
                if (string.Compare((this.mFieldCollection[i]).FieldName, fieldname, true) == 0)
                {
                    return this.mFieldCollection[i];
                }
            }

            throw (new FTSException("INVALID_FIELD_NAME", new object[] { fieldname }, this.mTableName, fieldname, -1));
        }

        public virtual void SetValue(int pos, string fieldname, object colvalue)
        {
            if (this.IsValidRow(pos))
            {
                this.mDataTable.Rows[pos][fieldname] = colvalue;
            }
        }

        public virtual void SetValue(DataRow row, string fieldname, object colvalue)
        {
            row[fieldname] = colvalue;
        }

        public virtual void SetValueIfChange(DataRow row, string fieldname, object colvalue)
        {
            if (this.IsValidRow(row))
            {
                if (row[fieldname] == System.DBNull.Value)
                {
                    if (colvalue != System.DBNull.Value && colvalue!= null)
                    {
                        if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.DateTime"))
                        {
                            DateTime date = (DateTime)colvalue;
                            row[fieldname] = new DateTime(date.Year, date.Month, date.Day);
                            row.EndEdit();
                        }
                        else
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                }
                else
                {
                    if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.String"))
                    {
                        if (!row[fieldname].ToString().Trim().Equals(colvalue.ToString().Trim()))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Decimal"))
                    {
                        if ((decimal)row[fieldname] != Convert.ToDecimal(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Guid"))
                    {
                        if ((Guid)row[fieldname] != (Guid)colvalue)
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Int32"))
                    {
                        if ((Int32)row[fieldname] != Convert.ToInt32(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Int16"))
                    {
                        if ((Int16)row[fieldname] != Convert.ToInt16(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Boolean"))
                    {
                        if ((bool)row[fieldname] != Convert.ToBoolean(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.DateTime"))
                    {
                        if (row[fieldname] == DBNull.Value)
                        {
                            if (colvalue != DBNull.Value && colvalue!= null)
                            {
                                row[fieldname] = colvalue;
                                row.EndEdit();
                            }
                        }
                        else
                        {
                            if (colvalue != DBNull.Value)
                            {
                                if ((DateTime)row[fieldname] != (DateTime)colvalue)
                                {
                                    row[fieldname] = colvalue;
                                    row.EndEdit();
                                }
                            }
                            else
                            {
                                row[fieldname] = colvalue;
                            }
                        }
                    }
                }
            }
        }

        public virtual void SetValue(string fieldname, object colvalue)
        {
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.String"))
                    {
                        if (!row[fieldname].ToString().Trim().Equals(colvalue.ToString().Trim()))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Decimal"))
                    {
                        if ((decimal)row[fieldname] != Convert.ToDecimal(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Guid"))
                    {
                        if ((Guid)row[fieldname] != (Guid)colvalue)
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Int32"))
                    {
                        if ((Int32)row[fieldname] != Convert.ToInt32(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Int16"))
                    {
                        if ((Int16)row[fieldname] != Convert.ToInt16(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.Boolean"))
                    {
                        if ((bool)row[fieldname] != Convert.ToBoolean(colvalue))
                        {
                            row[fieldname] = colvalue;
                            row.EndEdit();
                        }
                    }
                    else if (this.DataTable.Columns[fieldname].DataType == Type.GetType("System.DateTime"))
                    {
                        if (row[fieldname] == DBNull.Value)
                        {
                            if (colvalue != DBNull.Value)
                            {
                                row[fieldname] = colvalue;
                                row.EndEdit();
                            }
                        }
                        else
                        {
                            if (colvalue != DBNull.Value)
                            {
                                if ((DateTime)row[fieldname] != (DateTime)colvalue)
                                {
                                    row[fieldname] = colvalue;
                                    row.EndEdit();
                                }
                            }
                            else
                            {
                                row[fieldname] = colvalue;
                            }
                        }
                    }
                }
            }
        }

        public virtual decimal SumColumn(string field)
        {
            decimal s = 0;
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    s += (decimal)row[field];
                }
            }

            return s;
        }

        public decimal SumColumn(string field, string condition)
        {
            DataView dv = new DataView(this.mDataTable, condition, string.Empty, DataViewRowState.CurrentRows);
            decimal s = 0;
            foreach (DataRowView drv in dv)
            {
                if (this.IsValidRow(drv.Row))
                {
                    s += (decimal)drv[field];
                }
            }

            return s;
        }

        public string GetFieldList()
        {
            StringBuilder list = new StringBuilder();
            foreach (DataColumn c in this.mDataTable.Columns)
            {
                list.Append(c.ColumnName).Append(",");
            }

            if (list.Length != 0)
            {
                list.Remove(list.Length - 1, 1);
            }

            return list.ToString();
        }

        public ObjectBase Copy()
        {
            ObjectBase oj = (ObjectBase)this.MemberwiseClone();
            oj.mDataSet = this.mDataSet.Copy();
            return oj;
        }

        /*
        public bool HasSumFields()
        {
            for (int i = 0; i < this.mFieldCollection.Count; i++)
            {
                FieldInfo fi = this.mFieldCollection[i];
                if ((fi).IsSum)
                {
                    return true;
                }
            }
            return false;
        }
        */

        public object GetFirstRowValue(string fieldname)
        {
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    return row[fieldname];
                }
            }

            if (this.GetFieldInfo(fieldname).FieldType == DbType.String)
            {
                return string.Empty;
            }
            else
            {
                if (this.GetFieldInfo(fieldname).FieldType == DbType.Decimal || this.GetFieldInfo(fieldname).FieldType == DbType.Int32 ||
                    this.GetFieldInfo(fieldname).FieldType == DbType.Currency)
                {
                    return 0;
                }
                else
                {
                    return null;
                }
            }
        }

        public int GetFirstRowPos()
        {
            for (int i = 0; i < this.mDataTable.Rows.Count; i++)
            {
                if (this.IsValidRow(i))
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual DataRow GetFirstRow()
        {
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    return row;
                }
            }

            return null;
        }

        public virtual DataRow GetDataRow(Guid prkey)
        {
            DataRow[] rows = this.DataTable.Select("PR_KEY='" + prkey + "'");
            if (rows.Length > 0)
            {
                return rows[0];
            }
            return null;
        }

        public int GetActiveRow()
        {
            int i = 0;
            foreach (DataRow row in this.mDataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    i++;
                }
            }

            return i;
        }

        public object GetValue(string fieldname)
        {
            return this.GetValue(0, fieldname);
        }

        public object GetValue(int pos, string fieldname)
        {
            if (this.IsValidRow(pos))
            {
                return this.DataTable.Rows[pos][fieldname];
            }

            throw new FTSException("MSG_NO_CURRENTROW", fieldname);
        }

        public object GetValue(DataRow row, string fieldname)
        {
            if (this.IsValidRow(row))
            {
                return row[fieldname];
            }

            throw new FTSException("MSG_NO_CURRENTROW", fieldname);
        }

        public bool IsValidRow(int pos)
        {
            return base.IsValidRowBase(this.DataTable, pos);
        }


        #region To be overwridden

        public virtual void LoadFields() { }

        public virtual void SetRole() { }

        #endregion

        public virtual void RemoveEmptyRows()
        {
            if (this.IdField != null && this.IdFieldType == DbType.String)
            {
                foreach (DataRow row in this.DataTable.Rows)
                {
                    if (row.RowState == DataRowState.Added && row[this.IdField].ToString() == string.Empty)
                    {
                        row.RejectChanges();
                        this.RemoveEmptyRows();
                        return;
                    }
                }
            }
        }

        protected virtual string GetOrganizationID()
        {
            if (this.IsValidRow(0))
            {
                if (this.DataTable.Columns.IndexOf("ORGANIZATION_ID") >= 0)
                {
                    return this.GetValue("ORGANIZATION_ID").ToString();
                }
                else
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        public virtual void SetAllRow(string fieldname, object fieldvalue)
        {
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    row[fieldname] = fieldvalue;
                    row.EndEdit();
                }
            }
        }

        public virtual void CopyAllRow(string fieldsource, string fielddes)
        {
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    row[fielddes] = row[fieldsource];
                    row.EndEdit();
                }
            }
        }


        public static DataRow GetRowByID(FTSMain ftsmain, string tablename, string idfield, string idvalue)
        {
            DataTable dt =
                ftsmain.DbMain.LoadDataTable(
                    ftsmain.DbMain.GetSqlStringCommand("SELECT * FROM " + tablename + " WHERE " + idfield + "='" + idvalue + "'"),
                    tablename);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void ImportData(DataTable excelData, DataTable dm_template_detail)
        {
            DataColumn[] keys = this.DataTable.PrimaryKey;
            this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[this.IdField] };
            dm_template_detail.PrimaryKey = new DataColumn[] { dm_template_detail.Columns["DATA_COLUMN_NAME"] };
            List<DataRow> listAdded = new List<DataRow>();
            foreach (DataRow row in excelData.Rows)
            {
                if (this.IsValidExcelData(row, dm_template_detail))
                {
                    if (excelData.Columns.IndexOf(this.IdField) >= 0)
                    {
                        if (this.DataTable.Rows.Find(row[this.IdField]) == null)
                        {
                            DataRow newrow = this.AddNew();
                            foreach (DataColumn c in excelData.Columns)
                            {
                                if (this.DataTable.Columns.IndexOf(c.ColumnName) >= 0)
                                {
                                    if (this.DataTable.Columns[c.ColumnName].DataType == Type.GetType("System.String"))
                                    {
                                        newrow[c.ColumnName] = row[c.ColumnName].ToString().Trim();
                                    }
                                    else
                                    {
                                        newrow[c.ColumnName] = row[c.ColumnName];
                                    }
                                }
                            }

                            if (this.IdFieldType == DbType.String)
                            {
                                newrow[this.IdField] = newrow[this.IdField].ToString().ToUpper().Trim();
                            }

                            newrow.EndEdit();
                            listAdded.Add(row);
                        }
                    }
                    else
                    {
                        DataRow newrow = this.AddNew();
                        foreach (DataColumn c in excelData.Columns)
                        {
                            if (this.DataTable.Columns.IndexOf(c.ColumnName) >= 0)
                            {
                                if (this.DataTable.Columns[c.ColumnName].DataType == Type.GetType("System.String"))
                                {
                                    newrow[c.ColumnName] = row[c.ColumnName].ToString().Trim();
                                }
                                else
                                {
                                    newrow[c.ColumnName] = row[c.ColumnName];
                                }
                            }
                        }

                        newrow.EndEdit();
                        listAdded.Add(row);
                    }
                }
            }

            foreach (DataRow row in listAdded)
            {
                row.Delete();
            }

            excelData.AcceptChanges();
            this.DataTable.PrimaryKey = keys;
        }

        protected bool IsValidExcelData(DataRow row, DataTable dm_template_detail)
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                string columnname = c.ColumnName;
                DataRow templaterow = dm_template_detail.Rows.Find(columnname);
                if (templaterow != null)
                {
                    switch (templaterow["DATA_TYPE"].ToString().ToUpper())
                    {
                        case "STRING":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = string.Empty;
                            }

                            break;
                        case "DECIMAL":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    decimal cellvalue = Convert.ToDecimal(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "INT":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    Int32 cellvalue = Convert.ToInt32(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "BOOLEAN":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    Int16 cellvalue = Convert.ToInt16(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "DATE":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = this.FTSMain.DayStartOfFirstYear;
                            }
                            else
                            {
                                try
                                {
                                    DateTime cellvalue = Convert.ToDateTime(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                    }
                }
            }

            return true;
        }

        public virtual bool IDExists(string id)
        {
            object oj =
                this.FTSMain.DbMain.ExecuteScalar(
                    this.FTSMain.DbMain.GetSqlStringCommand("SELECT 'TRUE' FROM " + this.TableName + " where " + this.IdField + " = '" + id + "'"));
            if (oj != null && oj != System.DBNull.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void MergeRecord(string oldid, string newid)
        {
            if (!Functions.IsAdmin(this.FTSMain))
            {
                return;
            }

            DataTable dt =
                this.mFTSMain.DbMain.LoadDataTable(
                    this.mFTSMain.DbMain.GetSqlStringCommand("SELECT * FROM DM_FIELD_RELATION WHERE TABLE_NAME='" + this.TableName + "'"),
                    "DM_FIELD_RELATION");
            if (dt.Rows.Count == 0)
            {
                return;
            }

            DbTransaction tran = null;
            try
            {
                using (DbConnection connection = this.mFTSMain.DbMain.CreateConnection())
                {
                    connection.Open();
                    if ((bool)this.FTSMain.SystemVars.GetSystemVars("USE_SNAPSHOT_TRANSACTION"))
                    {
                        tran = connection.BeginTransaction(IsolationLevel.Snapshot);
                    }
                    else
                    {
                        tran = connection.BeginTransaction();
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        bool exists = true;
                        string sql = "SELECT 'TRUE' FROM " + row["TABLE_NAME_DES"] + " WHERE " + row["FIELD_NAME_DES"] + "=''";
                        try
                        {
                            if (tran != null)
                            {
                                object oj = this.FTSMain.DbMain.ExecuteScalar(this.FTSMain.DbMain.GetSqlStringCommand(sql), tran);
                            }
                            else
                            {
                                object oj = this.FTSMain.DbMain.ExecuteScalar(this.FTSMain.DbMain.GetSqlStringCommand(sql));
                            }
                        }
                        catch (Exception ex)
                        {
                            exists = false;
                        }

                        if (exists)
                        {
                            sql = "UPDATE " + row["TABLE_NAME_DES"] + " SET " + row["FIELD_NAME_DES"] + "=N'" + newid + "' WHERE " +
                                  row["FIELD_NAME_DES"] +
                                  "=N'" + oldid + "'";
                            this.mFTSMain.DbMain.ExecuteNonQuery(this.mFTSMain.DbMain.GetSqlStringCommand(sql), tran);
                        }
                    }

                    this.mFTSMain.DbMain.ExecuteNonQuery(
                        this.mFTSMain.DbMain.GetSqlStringCommand("UPDATE " + this.TableName + " SET ACTIVE=0 WHERE " + this.IdField + "=N'" + oldid +
                                                                 "'"),
                        tran);
                    tran.Commit();

                }
            }
            catch (Exception ex)
            {
                try
                {
                    tran.Rollback();
                }
                catch (Exception) { }

                throw ex;
            }
        }

        public virtual void ChangeRecord(string oldid, string newid)
        {
            if (!Functions.IsAdmin(this.FTSMain))
            {
                return;
            }

            DataTable dt =
                this.mFTSMain.DbMain.LoadDataTable(
                    this.mFTSMain.DbMain.GetSqlStringCommand("SELECT * FROM DM_FIELD_RELATION WHERE TABLE_NAME='" + this.TableName + "'"),
                    "DM_FIELD_RELATION");
            if (dt.Rows.Count > 0)
            {
                if (this.CreateNewID(this.TableName, this.IdField, oldid, newid))
                {
                    this.MergeRecord(oldid, newid);
                }
            }
        }

        private bool CreateNewID(string tablename, string idfield, string oldid, string newid)
        {
            DataTable dt =
                this.mFTSMain.DbMain.LoadDataTable(
                    this.mFTSMain.DbMain.GetSqlStringCommand("SELECT * FROM " + tablename + " WHERE " + idfield + "=N'" + oldid + "' OR " + idfield +
                                                             "=N'" +
                                                             newid + "'"), tablename);
            dt.PrimaryKey = new DataColumn[] { dt.Columns[idfield] };
            DataRow foundrow = dt.Rows.Find(newid);
            if (foundrow == null)
            {
                DataRow oldrow = dt.Rows.Find(oldid);
                if (oldrow != null)
                {
                    foundrow = dt.NewRow();
                    foundrow.ItemArray = (object[])oldrow.ItemArray.Clone();
                    foundrow[idfield] = newid;
                    dt.Rows.Add(foundrow);
                    this.mFTSMain.DbMain.UpdateTable(dt, this.mFTSMain.DbMain.CreateInsertCommand(tablename, dt, string.Empty), null, null,
                        UpdateBehavior.Standard);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public virtual bool ValidateImportData(string fieldname, object fieldvalue)
        {
            return true;
        }

        public virtual string GetAutoID()
        {
            return string.Empty;
        }

        public virtual void LoadDataBySearch(string filterstring, int pagenumber, string sortstring)
        {
            if (filterstring == string.Empty)
            {
                return;
            }

            string sql = "select * from " + this.TableName;
            if (this.FieldList.Length != 0)
            {
                sql = "select " + this.FieldList + " from " + this.TableName;
            }

            string filterfieldlist = this.IdField + "," + this.NameField;
            if (this.DataBySearchFieldList != string.Empty)
            {
                filterfieldlist = this.DataBySearchFieldList;
            }

            string[] filterfieldarray = FilterFieldList.Split(',');

            sql += " WHERE (";
            for (int i = 0; i < filterfieldarray.Length; i++)
            {
                sql += filterfieldarray[i] + " LIKE N'%" + filterstring + "%' OR ";
            }

            sql = sql.Substring(0, sql.Length - 4) + ")";
            if (this.Condittion.Length != 0)
            {
                sql += " and " + this.Condittion;
            }

            if (this.IsOrganizationFilter)
            {
                sql += " and " + this.FTSMain.DmOrganization.GetOrganizationFilter();
            }
            else
            {
                sql += " and " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID);
            }

            if (sortstring != string.Empty)
            {
                sql += " order by " + sortstring;
            }
            else
            {
                sql += " order by " + this.IdField;
            }

            //int recordperpage = (int)this.FTSMain.SystemVars.GetSystemVars("DM_LISTING_RECORD_PER_PAGE");
            //sql += " OFFSET " + (pagenumber - 1) * recordperpage + " ROWS FETCH NEXT " + recordperpage + " ROWS ONLY";
            this.LoadDataByCommand(this.FTSMain.DbMain.GetSqlStringCommand(sql));

        }

        public virtual ObjectInfoBase GetDataObject()
        {
            return null;
        }

        public virtual List<ObjectInfoBase> GetDataObjectList()
        {
            return null;
        }

        public virtual void SyncObjectToTable(ObjectInfoBase objectInfoBase)
        {
            DataColumn[] key = this.DataTable.PrimaryKey;
            try
            {
                this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[this.IdField] };
                DataRow row = this.DataTable.Rows.Find(objectInfoBase.GetValue(this.IdField));
                if (row == null)
                {
                    row = this.DataTable.Rows[0];
                }

                foreach (DataColumn c in this.DataTable.Columns)
                {
                    this.SetValueIfChange(row, c.ColumnName, objectInfoBase.GetValue(c.ColumnName));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.DataTable.PrimaryKey = key;
            }
        }

        public virtual void SyncObjectToTable(ObjectInfoBase[] objectInfoBaseList)
        {
            DataColumn[] key = this.DataTable.PrimaryKey;
            try
            {
                this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[this.IdField] };
                foreach (ObjectInfoBase objectInfoBase in objectInfoBaseList)
                {
                    DataRow row = this.DataTable.Rows.Find(objectInfoBase.GetValue(this.IdField));
                    if (row == null)
                    {
                        row = this.AddNew();
                    }

                    if (row != null)
                    {
                        foreach (DataColumn c in this.DataTable.Columns)
                        {
                            this.SetValueIfChange(row, c.ColumnName, objectInfoBase.GetValue(c.ColumnName));
                        }
                    }
                }
            }
            finally
            {
                this.DataTable.PrimaryKey = key;
            }
        }

        public virtual bool IsDataChanged(ObjectInfoBase objectInfoBase)
        {
            this.LoadDataByID(objectInfoBase.GetValue(this.IdField));
            if (this.IsValidRow(0))
            {
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    this.SetValueIfChange(this.DataTable.Rows[0], c.ColumnName, objectInfoBase.GetValue(c.ColumnName));
                }

                return this.HasChanged();
            }
            else
            {
                return true;
            }
        }


        #region paging



        /// <summary>
        /// Load paging data
        /// </summary>
        /// <param name="pageIndex">Trang cần lấy dl</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="filters">Điều kiện lọc</param>
        /// <param name="sort">Điều kiện sắp xếp</param>
        /// <param name="totalRecord">Output Tổng số bản ghi</param>
        /// <param name="fields">danh sách các trường dữ liệu cần lấy</param>
        /// <returns></returns>
        /// Created by: MTLUC - 05/01/2022 
        public virtual PagingDataResult LoadPagingData(
            List<string> fieldlist,
            List<string> summaryfieldlist,
            List<FilterGroup> filtergrouplist,
            List<Sort> sorts,
            int pagesize,
            int pageindex
        )
        {
            List<FilterGroup> localfiltergrouplist = new List<FilterGroup>();

            //mapping filters
            if (filtergrouplist?.Count > 0)
            {
                for (int i = 0; i < filtergrouplist.Count; i++)
                {
                    var group = filtergrouplist[i];
                    for (int j = 0; j < group.Filters.Count; j++)
                    {
                        var filteritem = group.Filters[j];
                        var fieldInfo = this.FieldCollection.Find(x => x.FieldName.Equals(filteritem.Field, StringComparison.OrdinalIgnoreCase));
                        if (fieldInfo != null)
                        {
                            filteritem.DbType = fieldInfo.FieldType;
                            filteritem.Field = fieldInfo.FieldName;
                            filteritem.ParamName = $"@{fieldInfo.FieldName}_{i}{j}";
                        }
                    }

                    localfiltergrouplist.Add(group);
                }
            }

            this.LoadPagingData(fieldlist, localfiltergrouplist, sorts, pagesize, pageindex);
            return new PagingDataResult
            {
                Data = this.GetDataObjectList(),
                RecordCount = this.GetRecordCount(localfiltergrouplist),
                SummaryData = this.GetSummaryData(summaryfieldlist, localfiltergrouplist)
            };
        }

        public virtual int GetRecordCount(List<FilterGroup> filtergrouplist)
        {
            string filter = this.GenerateFilter(filtergrouplist);
            if (this.IsOrganizationFilter)
            {
                filter += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
            }
            else
            {
                filter += " AND " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID);
            }

            string sql = "SELECT COUNT(*) FROM " + this.TableName + " WHERE " + filter;

            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            foreach (var group in filtergrouplist)
            {
                foreach (var filtervalue in group.Filters)
                {
                    this.FTSMain.DbMain.AddInParameter(cmd, filtervalue.ParamName, filtervalue.DbType, filtervalue.Value);
                }
            }
            object obj = this.FTSMain.DbMain.ExecuteScalar(cmd);
            if (obj == null || obj == System.DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public virtual void LoadPagingData(List<string> fiedlist, List<FilterGroup> filterlist, List<Sort> sorts, int pagesize, int pageindex)
        {

            string filter = this.GenerateFilter(filterlist);
            if (this.IsOrganizationFilter)
            {
                filter += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
            }
            else
            {
                filter += " AND " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID);
            }

            string sql = $@" SELECT *
                                FROM
                                (
                                    SELECT {this.GenerateQueryField(fiedlist)},
                                           ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                    FROM {this.TableName}
                                    WHERE 1 = 1 AND {filter}
                                ) tb
                                WHERE tb.ROW_INDEX > {(pageindex - 1) * pagesize}
                                      AND tb.ROW_INDEX <= {(pageindex * pagesize)};";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            foreach (var group in filterlist)
            {
                foreach (var filtervalue in group.Filters)
                {
                    this.FTSMain.DbMain.AddInParameter(cmd, filtervalue.ParamName, filtervalue.DbType, filtervalue.Value);
                }
            }
            this.LoadDataByCommand(cmd);
        }
        public virtual List<SummaryDataObject> GetSummaryData(List<string> summaryfieldlist, List<FilterGroup> filtergrouplist)
        {
            if (summaryfieldlist?.Count > 0)
            {
                string filter = this.GenerateFilter(filtergrouplist);
                if (this.IsOrganizationFilter)
                {
                    filter += " AND " + this.FTSMain.DmOrganization.GetOrganizationFilter();
                }
                else
                {
                    filter += " AND " + this.FTSMain.IdManager.Filter(this.TableName, this.FTSMain.UserInfo.OrganizationID);
                }

                string sql = $@"SELECT 
                                        {this.GenerateSumaryField(summaryfieldlist)}
                                    FROM {this.TableName}
                                    WHERE 1 = 1 AND {filter};";
                DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
                foreach (var group in filtergrouplist)
                {
                    foreach (var filtervalue in group.Filters)
                    {
                        this.FTSMain.DbMain.AddInParameter(cmd, filtervalue.ParamName, filtervalue.DbType, filtervalue.Value);
                    }
                }

                DataTable dt = this.FTSMain.DbMain.LoadDataTable(cmd, "dt");
                if (dt.Rows.Count > 0)
                {
                    List<SummaryDataObject> list = new List<SummaryDataObject>();
                    foreach (string summaryfield in summaryfieldlist)
                    {
                        list.Add(new SummaryDataObject(summaryfield.ToUpper(), dt.Rows[0][summaryfield]));
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Xử lý chỗi các trường dữ liệu cần query cho câu sql, map ping vs FieldCollection tránh sql injection
        /// </summary>
        /// <param name="fields">mảng tên cột cần lấy</param>
        /// <returns></returns>
        /// Created by: MTLUC - 05/01/2022 
        public string GenerateQueryField(List<string> filterfields)
        {
            if (filterfields?.Count > 0)
            {
                List<string> listFieldMap = new List<string>();
                foreach (var item in filterfields)
                {
                    if (this.FieldCollection.Exists(x => x.FieldName.Equals(item, StringComparison.OrdinalIgnoreCase)))
                    {
                        listFieldMap.Add($"[{item.ToUpper()}]");
                    }
                }

                if (listFieldMap.Count > 0)
                {
                    return string.Join(",", listFieldMap.ToArray());
                }
            }

            return "*";
        }

        /// <summary>
        /// Xử lý chỗi các trường dữ liệu cần tính tổng cho câu sql, map ping vs FieldCollection tránh sql injection
        /// </summary>
        /// <param name="fields">mảng tên cột cần lấy</param>
        /// <returns></returns>
        /// Created by: MTLUC - 05/01/2022 
        public string GenerateSumaryField(List<string> sumaryfields)
        {
            if (sumaryfields?.Count > 0)
            {
                List<string> listFieldMap = new List<string>();
                foreach (var item in sumaryfields)
                {
                    if (this.FieldCollection.Exists(x => x.FieldName.Equals(item, StringComparison.OrdinalIgnoreCase)))
                    {
                        listFieldMap.Add($"SUM(CAST([{item.ToUpper()}] AS DECIMAL)) AS [{item.ToUpper()}]");
                    }
                }

                if (listFieldMap.Count > 0)
                {
                    return string.Join(",", listFieldMap.ToArray());
                }
            }

            return " * ";
        }


        /// <summary>
        /// Tạo chuỗi filter sql
        /// Lưu ý filters phải maping trước vs FieldCollection gán DBType và ParamName
        /// (Không set trực tiếp value tránh lỗi injection)
        /// </summary>
        /// <param name="filtergroups"></param>
        /// <returns></returns>
        /// Created by: MTLUC - 05/01/2022 
        public string GenerateFilter(List<FilterGroup> filtergroups)
        {
            if (filtergroups?.Count > 0)
            {
                List<string> listFieldMap = new List<string>();
                for (int i = 0; i < filtergroups.Count; i++)
                {
                    var group = filtergroups[i];
                    List<string> groupMap = new List<string>();
                    for (int j = 0; j < group.Filters.Count; j++)
                    {
                        var filter = group.Filters[j];
                        groupMap.Add(this.GenerateFilterByDataType(filter));
                    }

                    if (groupMap.Count > 0)
                        listFieldMap.Add(
                            $" ( {string.Join($" {(group.Logic.Equals("OR", StringComparison.OrdinalIgnoreCase) ? "OR" : "AND")} ", groupMap.ToArray())} )");
                }

                if (listFieldMap.Count > 0)
                {
                    return string.Join(" AND ", listFieldMap.ToArray());
                }
            }

            return " 1 = 1 ";
        }

        /// <summary>
        /// Tạo chỗi filter theo Data
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GenerateFilterByDataType(Filter filter)
        {
            string result = string.Empty;
            switch (filter.DbType)
            {
                case DbType.Boolean:
                    result = $"= {filter.ParamName}";
                    break;
                case DbType.Date:
                case DbType.DateTime:
                    //bằng
                    if (filter.Operator == "eq")
                    {
                        result = $"= {filter.ParamName}";
                    }
                    //khác
                    else if (filter.Operator == "neq")
                    {
                        result = $"<> {filter.ParamName}";
                    }
                    //Sau hoặc bằng ngày 
                    else if (filter.Operator == "gte")
                    {
                        result = $">= {filter.ParamName}";

                    }
                    //Sau ngày
                    else if (filter.Operator == "gt")
                    {
                        result = $"> {filter.ParamName}";

                    }
                    //Trước hoặc bằng ngày
                    else if (filter.Operator == "lte")
                    {
                        result = $"<= {filter.ParamName}";

                    } //Trước ngày
                    else if (filter.Operator == "lt")
                    {
                        result = $"< {filter.ParamName}";

                    }
                    //isnull
                    else if (filter.Operator == "isnull")
                    {
                        result = $" IS NULL";
                    }
                    //isnotnull
                    else if (filter.Operator == "isnotnull")
                    {
                        result = $" IS NOT NULL";
                    }

                    break;
                case DbType.Currency:
                case DbType.Decimal:
                case DbType.Double:
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    //bằng
                    if (filter.Operator == "eq")
                    {
                        result = $"= {filter.ParamName}";
                    }
                    //khác
                    else if (filter.Operator == "neq")
                    {
                        result = $"<> {filter.ParamName}";
                    }
                    //Lớn hơn hoặc bằng
                    else if (filter.Operator == "gte")
                    {
                        result = $">= {filter.ParamName}";

                    }
                    //Lớn hơn
                    else if (filter.Operator == "gt")
                    {
                        result = $"> {filter.ParamName}";

                    }
                    //Nhỏ hơn hoặc bằng
                    else if (filter.Operator == "lte")
                    {
                        result = $"<= {filter.ParamName}";

                    }
                    //Nhỏ hơn
                    else if (filter.Operator == "lt")
                    {
                        result = $"< {filter.ParamName}";

                    }
                    //isnull
                    else if (filter.Operator == "isnull")
                    {
                        result = $" IS NULL";
                    }
                    //isnotnull
                    else if (filter.Operator == "isnotnull")
                    {
                        result = $" IS NOT NULL";
                    }

                    break;
                default: //mặc định string
                    //bằng
                    if (filter.Operator == "eq")
                    {
                        result = $"= {filter.ParamName}";
                    }
                    //khác
                    else if (filter.Operator == "neq")
                    {
                        result = $"<> {filter.ParamName}";
                    }
                    //Chứa
                    else if (filter.Operator == "contains")
                    {
                        result = $"LIKE N'%' + {filter.ParamName} + '%'";
                    }
                    //không chứa
                    else if (filter.Operator == "doesnotcontain")
                    {
                        result = $" NOT LIKE N'%' + {filter.ParamName} + '%'";
                    }
                    //bắt đầu bằng
                    else if (filter.Operator == "startswith")
                    {
                        result = $" LIKE N'' + {filter.ParamName} + '%'";
                    }
                    //kết thúc bằng
                    else if (filter.Operator == "endswith")
                    {
                        result = $" LIKE N'%' + {filter.ParamName} + ''";
                    }
                    //isnull
                    else if (filter.Operator == "isnull")
                    {
                        result = $" IS NULL";
                    }
                    //isnotnull
                    else if (filter.Operator == "isnotnull")
                    {
                        result = $" IS NOT NULL";
                    }
                    //trống
                    else if (filter.Operator == "isempty")
                    {
                        result = $" = ''";
                    }
                    //không trống
                    else if (filter.Operator == "isnotempty")
                    {
                        result = $" <> ''";
                    }

                    break;
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = " 1 = 0 "; // set 1=1 để ko phải check WHERE
            }
            else
            {
                result = $"[{filter.Field}] {result}";
            }

            return result;
        }

        /// <summary>
        /// Tạo chỗi sql sort
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        /// Created by: MTLUC - 05/01/2022 
        public string GenerateSort(List<Sort> sorts)
        {
            if (sorts?.Count > 0)
            {
                List<string> lstSortMaping = new List<string>();
                foreach (var sort in sorts)
                {
                    if (sort != null && this.FieldCollection.Exists(x => x.FieldName.Equals(sort.Field, StringComparison.OrdinalIgnoreCase)))
                    {
                        lstSortMaping.Add($" [{sort.Field}] {(sort.Dir.ToLower() == "asc" ? "ASC" : "DESC")}");
                    }
                }
                if (lstSortMaping.Count > 0)
                {
                    return $"ORDER BY {string.Join(",", lstSortMaping)}";
                }
            }
            return $"ORDER BY [{this.IdField}] ASC";
        }

        /// <summary>
        /// Xử lý lại chuỗi Filter hoặc Sort.
        /// Đối với các trường hợp phải join nhiều table cần chỉ rõ Filter hoặc Sort trên COLUMN của TABLE nào.
        /// Create by: tan.vu - 21.02.2022
        /// </summary>
        /// <param name="filterorsorts">Filter or Sorts</param>
        /// <returns></returns>
        protected virtual string ReGenerateFilterOrSorts(string filterorsorts)
        {
            string[] arrExcludedFieldList = this.ExcludedFieldList.Split(',');
            string reFilterOrSorts = filterorsorts;
            foreach (FieldInfo fieldInfo in this.FieldCollection)
            {
                if (arrExcludedFieldList.Contains(fieldInfo.FieldName))
                {
                    //reFilterOrSorts = reFilterOrSorts.Replace($"[" + fieldInfo.FieldName + "]", $"{this.TableName}.{fieldInfo.FieldName}");
                }
                else
                {
                    reFilterOrSorts = reFilterOrSorts.Replace($"[" + fieldInfo.FieldName + "]", $"{this.TableName}.{fieldInfo.FieldName}");
                }
            }
            return reFilterOrSorts;
        }

        #endregion

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DataSet != null)
                {
                    Functions.ClearDataSet(this.DataSet);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        #endregion
    }
}