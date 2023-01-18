#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using System;
using System.Runtime.CompilerServices;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Account : ObjectBase {
        public Dm_Account(FTSMain ftsmain) : base(ftsmain, "Dm_Account") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Account(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Account") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Account(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Account", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_NAME", DbType.String, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_PARENT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PARENT_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BALANCE_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CURRENCY_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RATE_METHOD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_OOB", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_PR_DETAIL", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_EXPENSE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_JOB", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_BANK", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_EMPLOYEE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_DEPARTMENT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_AGENT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_INSURANCE_SOURCE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_CAPITAL_SOURCE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_REINSURANCE_SOURCE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_VAT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_CONTRACT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ACCOUNT;
        }

        public static bool IsValidAccount(DataRow accountrow) {
            if (accountrow != null) {
                return (Convert.ToInt16(accountrow["IS_PARENT"]) == 1 || Convert.ToInt16(accountrow["ACTIVE"]) == 0) ? false : true;
            } else {
                return false;
            }
        }
        public static bool IsValidAccount(FTSMain ftsmain, string accountid) {
            object obj = ftsmain.DbMain.ExecuteScalar(
                ftsmain.DbMain.GetSqlStringCommand("SELECT 'TRUE' FROM DM_ACCOUNT WHERE ACCOUNT_ID='" + accountid +
                                                   "' AND ACTIVE=1 AND IS_PARENT=0"));

            if (obj != null && obj != System.DBNull.Value) {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsRelated(DataRow accountrow, string relationkey) {
            if (accountrow != null) {
                return Convert.ToInt16(accountrow[relationkey]) == 1 ? true : false;
            } else {
                return false;
            }
        }

        public static bool IsRelated(FTSMain ftsmain, string accountid, string relationkey) {
            object obj = ftsmain.DbMain.ExecuteScalar(
                ftsmain.DbMain.GetSqlStringCommand("SELECT " + relationkey + " FROM DM_ACCOUNT WHERE ACCOUNT_ID='" + accountid + "'"));

            if (obj != null && obj != System.DBNull.Value) {
                return Convert.ToInt16(obj) == 1 ? true : false;
            } else {
                return false;
            }
        }

        public static string GetBalanceType(FTSMain ftsmain, string account_id) {
            string sql = "select BALANCE_TYPE from dm_account where account_id='" + account_id + "'";
            object obj = ftsmain.DbMain.ExecuteScalar(ftsmain.DbMain.GetSqlStringCommand(sql));
            if (obj != null && obj != System.DBNull.Value) {
                return obj.ToString();
            } else {
                return DebitCredit.DEB;
            }
        }

        public static string GetBalanceType(DataTable dmaccount, string account_id) {
            DataRow foundrow = dmaccount.Rows.Find(account_id);
            if (foundrow != null) {
                return foundrow["BALANCE_TYPE"].ToString();
            } else {
                return DebitCredit.DEB;
            }
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_AccountObject dmAccountObject = new Dm_AccountObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmAccountObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmAccountObject;
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
                    Dm_AccountObject dmAccountObject = new Dm_AccountObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmAccountObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmAccountObject);
                }
            }

            return list;
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
                                     SELECT dbo.DM_ACCOUNT.*,
                                                             ISNULL(CURRENCY_NAME, '') AS CURRENCY_NAME,   
                                                             ISNULL(DM_ACCOUNT_PARENT.ACCOUNT_NAME, '') AS PARENT_ACCOUNT_NAME, 
                                                             ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                            FROM dbo.DM_ACCOUNT
                                    LEFT JOIN dbo.DM_CURRENCY
                                        ON DM_CURRENCY.CURRENCY_ID = DM_ACCOUNT.CURRENCY_ID
                                       LEFT JOIN
                                        (
                                            SELECT ACCOUNT_ID as PARENT_ACCOUNT_ID,
                                                   ACCOUNT_NAME
                                            FROM DM_ACCOUNT
                                            WHERE IS_PARENT = 1
                                        ) AS DM_ACCOUNT_PARENT
                                            ON DM_ACCOUNT_PARENT.PARENT_ACCOUNT_ID = DM_ACCOUNT.PARENT_ACCOUNT_ID
                                    WHERE 1 = 1 AND {filter.Replace($"[ACTIVE]", $"DM_ACCOUNT.ACTIVE").Replace($"[ACCOUNT_NAME]",$"DM_ACCOUNT.ACCOUNT_NAME").Replace($"[USER_ID]", $"DM_ACCOUNT.USER_ID").Replace($"[PARENT_ACCOUNT_ID]", $"DM_ACCOUNT.PARENT_ACCOUNT_ID").Replace($"[CURRENCY_ID]", $"DM_ACCOUNT.CURRENCY_ID")}
                                ) tb
                                WHERE tb.ROW_INDEX > {(pageindex - 1) * pagesize} AND tb.ROW_INDEX <= {(pageindex * pagesize)};";
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
                              FROM dbo.DM_ACCOUNT
                                    LEFT JOIN dbo.DM_CURRENCY
                                        ON DM_CURRENCY.CURRENCY_ID = DM_ACCOUNT.CURRENCY_ID
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




    }
}