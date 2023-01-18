using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Security: ObjectBase
    {
        public Dm_Security(FTSMain ftsmain) : base(ftsmain, "Dm_Security")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Security(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Security")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Security(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Security", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_CLASS_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BOOK_UNIT_PRICE_ORIG", DbType.Decimal, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CURRENCY_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ISSUE_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "MATURITY_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHORT_TERM_COST_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHORT_TERM_PROFIT_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHORT_TERM_LOSS_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHORT_TERM_RESERVE_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LONG_TERM_COST_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LONG_TERM_PROFIT_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LONG_TERM_LOSS_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LONG_TERM_RESERVE_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_NAME", DbType.String, false));

            this.ExcludedFieldList = "SECURITY_CLASS_NAME,PR_DETAIL_NAME,PERIOD_NAME";

        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_SECURITY;
        }

        public override void LoadEmptyData()
        {
            string sql = @"SELECT dbo.DM_SECURITY.*,
                                   dbo.DM_SECURITY_CLASS.SECURITY_CLASS_NAME,
                                   dbo.DM_PR_DETAIL.PR_DETAIL_NAME,
                                   dbo.DM_PERIOD.PERIOD_NAME
                            FROM dbo.DM_SECURITY
                                LEFT JOIN dbo.DM_SECURITY_CLASS
                                    ON DM_SECURITY_CLASS.SECURITY_CLASS_ID = DM_SECURITY.SECURITY_CLASS_ID
                                LEFT JOIN dbo.DM_PR_DETAIL
                                    ON DM_PR_DETAIL.PR_DETAIL_ID = DM_SECURITY.PR_DETAIL_ID
                                LEFT JOIN dbo.DM_PERIOD
                                    ON DM_PERIOD.PERIOD_ID = DM_SECURITY.PERIOD_ID where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        public override void LoadDataByID(object idvalue)
        {
            string sql = @"SELECT dbo.DM_SECURITY.*,
                                   dbo.DM_SECURITY_CLASS.SECURITY_CLASS_NAME,
                                   dbo.DM_PR_DETAIL.PR_DETAIL_NAME,
                                   dbo.DM_PERIOD.PERIOD_NAME
                            FROM dbo.DM_SECURITY
                                LEFT JOIN dbo.DM_SECURITY_CLASS
                                    ON DM_SECURITY_CLASS.SECURITY_CLASS_ID = DM_SECURITY.SECURITY_CLASS_ID
                                LEFT JOIN dbo.DM_PR_DETAIL
                                    ON DM_PR_DETAIL.PR_DETAIL_ID = DM_SECURITY.PR_DETAIL_ID
                                LEFT JOIN dbo.DM_PERIOD
                                    ON DM_PERIOD.PERIOD_ID = DM_SECURITY.PERIOD_ID where " + this.IdField + " = " + this.FTSMain.BuildParameterName(this.IdField);
            if (this.DataTable != null)
            {
                this.DataTable.Clear();
            }

            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, idvalue);
            this.FTSMain.DbMain.LoadDataSet(cmd, this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_SecurityObject dmSecurityObject = new Dm_SecurityObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmSecurityObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmSecurityObject;
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
                    Dm_SecurityObject dmSecurityObject = new Dm_SecurityObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmSecurityObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmSecurityObject);
                }
            }

            return list;
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

            string sql = @"SELECT COUNT(SECURITY_ID)
                            FROM (
                                    SELECT dbo.DM_SECURITY.*,
                                           dbo.DM_SECURITY_CLASS.SECURITY_CLASS_NAME,
                                           dbo.DM_PR_DETAIL.PR_DETAIL_NAME,
                                           dbo.DM_PERIOD.PERIOD_NAME
                                    FROM dbo.DM_SECURITY
                                        LEFT JOIN dbo.DM_SECURITY_CLASS
                                            ON DM_SECURITY_CLASS.SECURITY_CLASS_ID = DM_SECURITY.SECURITY_CLASS_ID
                                        LEFT JOIN dbo.DM_PR_DETAIL
                                            ON DM_PR_DETAIL.PR_DETAIL_ID = DM_SECURITY.PR_DETAIL_ID
                                        LEFT JOIN dbo.DM_PERIOD
                                            ON DM_PERIOD.PERIOD_ID = DM_SECURITY.PERIOD_ID
                                ) A 
                            WHERE " + filter;

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

            string sql = $@" SELECT *
                                FROM
                                (
                                    SELECT {this.GenerateQueryField(fiedlist)},
                                           ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                    FROM (
                                            SELECT dbo.DM_SECURITY.*,
                                                   dbo.DM_SECURITY_CLASS.SECURITY_CLASS_NAME,
                                                   dbo.DM_PR_DETAIL.PR_DETAIL_NAME,
                                                   dbo.DM_PERIOD.PERIOD_NAME
                                            FROM dbo.DM_SECURITY
                                                LEFT JOIN dbo.DM_SECURITY_CLASS
                                                    ON DM_SECURITY_CLASS.SECURITY_CLASS_ID = DM_SECURITY.SECURITY_CLASS_ID
                                                LEFT JOIN dbo.DM_PR_DETAIL
                                                    ON DM_PR_DETAIL.PR_DETAIL_ID = DM_SECURITY.PR_DETAIL_ID
                                                LEFT JOIN dbo.DM_PERIOD
                                                    ON DM_PERIOD.PERIOD_ID = DM_SECURITY.PERIOD_ID
                                    ) A
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

    }
}
