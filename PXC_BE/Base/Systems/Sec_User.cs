#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Systems
{
    public class Sec_User : ObjectBase
    {
        public Sec_User(FTSMain ftsmain) : base(ftsmain, "SEC_USER")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Sec_User(FTSMain ftsmain, bool isempty) : base(ftsmain, "SEC_USER")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_GROUP_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_PASSWORD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMPLOYEE_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMPLOYEE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_KEY", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LOGIN_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "QUANTITY_INVALID", DbType.Int32, false));

            this.ExcludedFieldList = "EMPLOYEE_NAME, ORGANIZATION_NAME";
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override void LoadPagingData(List<string> fiedlist, List<FilterGroup> filterlist, List<Sort> sorts, int pagesize, int pageindex)
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

            string sql = $@" SELECT {this.GenerateQueryField(fiedlist)}
                                FROM
                                (
                                    SELECT SEC_USER.USER_ID, SEC_USER.USER_GROUP_ID, USER_NAME,'' AS USER_PASSWORD, SEC_USER.EMPLOYEE_ID, SEC_USER.ORGANIZATION_ID, SEC_USER.ACTIVE, SEC_USER.USER_KEY, SEC_USER.LOGIN_DATE, SEC_USER.QUANTITY_INVALID, EMPLOYEE_NAME , DM_ORGANIZATION.ORGANIZATION_NAME, 
                                           ROW_NUMBER() OVER ({ this.ReGenerateFilterOrSorts(this.GenerateSort(sorts))}) AS ROW_INDEX
                                    FROM SEC_USER LEFT JOIN DM_EMPLOYEE ON DM_EMPLOYEE.EMPLOYEE_ID = SEC_USER.EMPLOYEE_ID 
                                         LEFT JOIN DM_ORGANIZATION ON DM_ORGANIZATION.ORGANIZATION_ID = SEC_USER.ORGANIZATION_ID
                                    WHERE 1 = 1 AND {this.ReGenerateFilterOrSorts(filter)}
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

        public override int GetRecordCount(List<FilterGroup> filtergrouplist)
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

            string sql = $@"SELECT COUNT(*) FROM SEC_USER LEFT JOIN DM_EMPLOYEE ON DM_EMPLOYEE.EMPLOYEE_ID = SEC_USER.EMPLOYEE_ID 
                                  LEFT JOIN DM_ORGANIZATION ON DM_ORGANIZATION.ORGANIZATION_ID = SEC_USER.ORGANIZATION_ID WHERE " + this.ReGenerateFilterOrSorts(filter);

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

        public override void LoadEmptyData()
        {
            string sql = "select *, '' AS EMPLOYEE_NAME, '' AS ORGANIZATION_NAME from " + this.TableName + " where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Sec_UserObject secUserobject = new Sec_UserObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    secUserobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return secUserobject;
            }
            else
            {
                return null;
            }
        }

        public override List<ObjectInfoBase> GetDataObjectList()
        {
            List<ObjectInfoBase> list = new List<ObjectInfoBase>();
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    Sec_UserObject secUserobject = new Sec_UserObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        secUserobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(secUserobject);
                }
            }

            return list;
        }

        public override void SyncObjectToTable(ObjectInfoBase objectInfoBase)
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
                    if (c.ColumnName == "USER_PASSWORD")
                    {
                        if (objectInfoBase.GetValue(c.ColumnName).ToString() != string.Empty){
                            this.SetValueIfChange(row, c.ColumnName, FTS.Base.Utilities.FunctionsBase.Encrypt(objectInfoBase.GetValue(c.ColumnName).ToString()));
                        }
                    }
                    else
                    {
                        this.SetValueIfChange(row, c.ColumnName, objectInfoBase.GetValue(c.ColumnName));
                    }
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
    }
}