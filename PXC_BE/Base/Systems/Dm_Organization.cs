#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Security;
using FTS.Base.Utilities;
using FTS.Base.Model;
using FTS.Base.Model.Paging;

#endregion

namespace FTS.Base.Systems
{
    public class Dm_Organization : ObjectBase
    {
        public string CurrentOrganizationID = string.Empty;

        public Dm_Organization(FTSMain ftsmain) : base(ftsmain, "DM_ORGANIZATION")
        {
            this.IsDmOrganization = true;
            this.Condittion = string.Empty;
            this.LoadData();
            this.LoadFields();
        }

        public Dm_Organization(FTSMain ftsmain, bool isempty) : base(ftsmain, "DM_ORGANIZATION")
        {
            this.IsDmOrganization = true;
            if (!isempty)
            {
                this.LoadData();
            }
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME_DISPLAY", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PARENT_ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PARENT_ORGANIZATION_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ADDRESS", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CITY", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DISTRICT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PHONE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FAX", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMAIL", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TAX_FILE_NUMBER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME_SHORT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DIRECTOR", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNTANT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CHIEF_ACCOUNTANT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_ACCOUNT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_BRANCH", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CASHIER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));

            this.ExcludedFieldList = "PARENT_ORGANIZATION_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTSFunctionCollection.DM_ORGANIZATION;
        }

        public string GetOrganizationName(string organizationid)
        {
            DataRow foundrow = this.DataTable.Rows.Find(organizationid);
            if (foundrow != null)
            {
                return foundrow["ORGANIZATION_NAME"].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetOrganizationNameShort(string organizationid)
        {
            DataRow foundrow = this.DataTable.Rows.Find(organizationid);
            if (foundrow != null)
            {
                return foundrow["ORGANIZATION_NAME_SHORT"].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetOrganizationFilter()
        {
            return " ORGANIZATION_ID IN " +
                   Functions.PopulateString(
                       this.GetChildOrganization(this.FTSMain.UserInfo.OrganizationID));
        }

        public string GetOrganizationFilter(string organizationid)
        {
            if (organizationid == string.Empty)
            {
                return "1=1";
            }
            else
            {
                return " ORGANIZATION_ID IN " +
                       Functions.PopulateString(
                           this.GetChildOrganization(organizationid));
            }
        }

        public string GetChildOrganization(string organizationid)
        {
            string listchild = "" + organizationid + "," + GetChildOrganizationRecursive(organizationid);
            if (listchild.Length > 0 && listchild.Substring(listchild.Length - 1, 1) == ",")
            {
                listchild = listchild.Substring(0, listchild.Length - 1);
            }
            return listchild;
        }

        private string GetChildOrganizationRecursive(string organizationid)
        {
            string listchild = string.Empty;
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (row["PARENT_ORGANIZATION_ID"].ToString().Trim() == organizationid)
                {
                    listchild += "" + row["ORGANIZATION_ID"].ToString() + ",";
                    listchild += GetChildOrganizationRecursive(row["ORGANIZATION_ID"].ToString());
                }
            }
            return listchild;
        }

        public string GetParentOrganizationID(string organizationid)
        {
            DataRow foundrow = this.DataTable.Rows.Find(organizationid);
            if (foundrow != null)
            {
                return foundrow["PARENT_ORGANIZATION_ID"].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public bool IsChildOrSame(string childorganizationid) {
            if (this.FTSMain.UserInfo.OrganizationID == childorganizationid) {
                return true;
            } else {
                string childlist = this.GetChildOrganization(this.FTSMain.UserInfo.OrganizationID);
                if (Functions.InListAbsolute(childorganizationid, childlist)) {
                    return true;
                } else {
                    return false;
                }
            }

            return false;
        }

        public override void CheckBusinessRules()
        {
            if (!Functions.IsAdmin(this.FTSMain))
            {
                throw (new FTSException("MSG_NO_PERMISSION"));
            }
            int count = 0;
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if (row["ORGANIZATION_ID"].ToString() == row["PARENT_ORGANIZATION_ID"].ToString())
                    {
                        throw (new FTSException(null, "MSG_INVALID_PARENT_ORGANIZATION_ID", this.TableName, "PARENT_ORGANIZATION_ID", count));
                    }
                }
                count++;
            }
            base.CheckBusinessRules();
        }

        public override void LoadData()
        {
            base.LoadData();
            this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns["ORGANIZATION_ID"] };
        }

        public override void UpdateData()
        {
            base.UpdateData();
            this.FTSMain.DmOrganization.LoadData();
        }
        public override void LoadEmptyData()
        {
            string sql = "select *, '' AS PARENT_ORGANIZATION_NAME from " + this.TableName + " where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
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
                                    SELECT DM_ORGANIZATION.*, PARENT.ORGANIZATION_NAME AS PARENT_ORGANIZATION_NAME ,
                                           ROW_NUMBER() OVER ({this.ReGenerateFilterOrSorts(this.GenerateSort(sorts))}) AS ROW_INDEX
                                    FROM DM_ORGANIZATION LEFT JOIN DM_ORGANIZATION PARENT ON DM_ORGANIZATION.PARENT_ORGANIZATION_ID = PARENT.ORGANIZATION_ID
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

            string sql = $@"SELECT COUNT(*) 
                            FROM DM_ORGANIZATION 
                                LEFT JOIN DM_ORGANIZATION PARENT ON DM_ORGANIZATION.PARENT_ORGANIZATION_ID = PARENT.ORGANIZATION_ID 
                            WHERE " + this.ReGenerateFilterOrSorts(filter);

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


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_OrganizarionObject organizarionObject = new Dm_OrganizarionObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    organizarionObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return organizarionObject;
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
                    Dm_OrganizarionObject organizarionObject = new Dm_OrganizarionObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        organizarionObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(organizarionObject);
                }
            }

            return list;
        }

        public bool IsDependentChild(string childid, string parentid)
        {
            DataRow row = this.DataTable.Rows.Find(childid);
            if (row != null && row["ORGANIZATION_TYPE"].ToString().Trim() == OrganizationType.DEPENDANT_ORGANIZATION && row["PARENT_ORGANIZATION_ID"].ToString().Trim() == parentid)
            {
                return true;
            }
            return false;
        }

        public bool IsChild(string childid, string parentid)
        {
            DataRow row = this.DataTable.Rows.Find(childid);
            if (row != null)
            {
                if (row["PARENT_ORGANIZATION_ID"].ToString().Trim() == parentid)
                {
                    return true;
                }
                else
                {
                    return this.IsChild(row["PARENT_ORGANIZATION_ID"].ToString().Trim(), parentid);
                }
            }
            return false;
        }
    }
}