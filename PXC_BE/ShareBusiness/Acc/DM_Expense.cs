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
    public class Dm_Expense : ObjectBase
    {
        public Dm_Expense(FTSMain ftsmain) : base(ftsmain, "Dm_Expense")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Expense(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Expense")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Expense(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Expense", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_CLASS_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_CLASS_NAME", DbType.String, false));

            this.ExcludedFieldList = "EXPENSE_CLASS_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_EXPENSE;
        }

        public override void LoadEmptyData()
        {
            string sql = @"SELECT dbo.DM_EXPENSE.*,
                                   dbo.DM_EXPENSE_CLASS.EXPENSE_CLASS_NAME
                            FROM dbo.DM_EXPENSE
                                LEFT JOIN dbo.DM_EXPENSE_CLASS
                                    ON DM_EXPENSE_CLASS.EXPENSE_CLASS_ID = DM_EXPENSE.EXPENSE_CLASS_ID where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        public override void LoadDataByID(object idvalue)
        {
            string sql = @"SELECT dbo.DM_EXPENSE.*,
                                   dbo.DM_EXPENSE_CLASS.EXPENSE_CLASS_NAME
                            FROM dbo.DM_EXPENSE
                                LEFT JOIN dbo.DM_EXPENSE_CLASS
                                    ON DM_EXPENSE_CLASS.EXPENSE_CLASS_ID = DM_EXPENSE.EXPENSE_CLASS_ID where " + this.IdField + " = " + this.FTSMain.BuildParameterName(this.IdField);
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
                DM_ExpenseObject dm_ExpenseObject = new DM_ExpenseObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dm_ExpenseObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }
                return dm_ExpenseObject;
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
                    DM_ExpenseObject dmExpenseObject = new DM_ExpenseObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmExpenseObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmExpenseObject);
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

            string sql = @"SELECT COUNT(EXPENSE_ID)
                            FROM (
                                    SELECT dbo.DM_EXPENSE.*,
                                           dbo.DM_EXPENSE_CLASS.EXPENSE_CLASS_NAME
                                    FROM dbo.DM_EXPENSE
                                    LEFT JOIN dbo.DM_EXPENSE_CLASS
                                    ON DM_EXPENSE_CLASS.EXPENSE_CLASS_ID = DM_EXPENSE.EXPENSE_CLASS_ID
                                )A 
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
                                            SELECT dbo.DM_EXPENSE.*,
                                                   dbo.DM_EXPENSE_CLASS.EXPENSE_CLASS_NAME
                                            FROM dbo.DM_EXPENSE
                                                LEFT JOIN dbo.DM_EXPENSE_CLASS
                                                    ON DM_EXPENSE_CLASS.EXPENSE_CLASS_ID = DM_EXPENSE.EXPENSE_CLASS_ID
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
