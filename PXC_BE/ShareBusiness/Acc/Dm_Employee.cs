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
    public class Dm_Employee : ObjectBase
    {
        public Dm_Employee(FTSMain ftsmain) : base(ftsmain, "Dm_Employee")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Employee(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Employee")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Employee(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Employee", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMPLOYEE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMPLOYEE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ADDRESS", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PHONE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMAIL", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IDENTITY_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_NAME", DbType.String, false));

            this.ExcludedFieldList = "DEPARTMENT_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_EMPLOYEE;
        }

        public override void LoadEmptyData()
        {
            string sql = @"SELECT dbo.DM_EMPLOYEE.*,
                                   dbo.DM_DEPARTMENT.DEPARTMENT_NAME
                            FROM dbo.DM_EMPLOYEE
                                LEFT JOIN dbo.DM_DEPARTMENT
                                    ON DM_DEPARTMENT.DEPARTMENT_ID = DM_EMPLOYEE.DEPARTMENT_ID where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        public override void LoadDataByID(object idvalue)
        {
            string sql = @"SELECT dbo.DM_EMPLOYEE.*,
                                   dbo.DM_DEPARTMENT.DEPARTMENT_NAME
                            FROM dbo.DM_EMPLOYEE
                                LEFT JOIN dbo.DM_DEPARTMENT
                                    ON DM_DEPARTMENT.DEPARTMENT_ID = DM_EMPLOYEE.DEPARTMENT_ID where " + this.IdField + " = " + this.FTSMain.BuildParameterName(this.IdField);
            if (this.DataTable != null)
            {
                this.DataTable.Clear();
            }

            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, this.IdField, this.IdFieldType, idvalue);
            this.FTSMain.DbMain.LoadDataSet(cmd, this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        public override void UpdateData() {
            DataSet ds = this.DataSet.GetChanges();
            if (ds != null && ds.Tables[this.TableName] != null) {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.CheckIdStruct = false;
                dmPrDetail.AllowCreateEmployee = true;
                foreach (DataRow row in ds.Tables[this.TableName].Rows) {
                    if (row.RowState != DataRowState.Deleted) {
                        dmPrDetail.LoadDataByID(row["EMPLOYEE_ID"]);
                        if (!dmPrDetail.IsValidRow(0)) {
                            DataRow newrow = dmPrDetail.AddNew();
                            newrow["PR_DETAIL_ID"] = row["EMPLOYEE_ID"];
                            newrow["PR_DETAIL_NAME"] = row["EMPLOYEE_NAME"];
                            newrow["ADDRESS"] = row["ADDRESS"];
                            newrow["PHONE"] = row["PHONE"];
                            newrow["EMAIL"] = row["EMAIL"];
                            newrow["IDENTITY_NO"] = row["IDENTITY_NO"];
                            newrow["ACTIVE"] = row["ACTIVE"];
                            newrow["PR_DETAIL_TYPE_ID"] = PrDetailType.EMPLOYEE;
                            object obj = this.FTSMain.DbMain.ExecuteScalar(
                                this.FTSMain.DbMain.GetSqlStringCommand(
                                    "SELECT TOP 1 PR_DETAIL_CLASS_ID FROM DM_PR_DETAIL_CLASS WHERE IS_EMPLOYEE=1 AND ACTIVE=1"));

                            if (obj != null && obj != System.DBNull.Value) {
                                newrow["PR_DETAIL_CLASS_ID"] = obj;
                            }

                            newrow.EndEdit();
                        } else {
                            DataRow foundrow = dmPrDetail.DataTable.Rows[0];
                            foundrow["PR_DETAIL_NAME"] = row["EMPLOYEE_NAME"];
                            foundrow["ADDRESS"] = row["ADDRESS"];
                            foundrow["PHONE"] = row["PHONE"];
                            foundrow["EMAIL"] = row["EMAIL"];
                            foundrow["IDENTITY_NO"] = row["IDENTITY_NO"];
                            foundrow["ACTIVE"] = row["ACTIVE"];
                            foundrow.EndEdit();
                        }
                    } else {
                        dmPrDetail.LoadDataByID(row["EMPLOYEE_ID", DataRowVersion.Original]);
                        if (dmPrDetail.IsValidRow(0)) {
                            dmPrDetail.DataTable.Rows[0].Delete();
                        }
                    }
                }

                dmPrDetail.UpdateData();
            }
            base.UpdateData();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_EmployeeObject dmEmployeeObject = new Dm_EmployeeObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmEmployeeObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }
                return dmEmployeeObject;
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
                    Dm_EmployeeObject dmEmployeeObject = new Dm_EmployeeObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmEmployeeObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmEmployeeObject);
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

            string sql = @"SELECT COUNT(EMPLOYEE_ID)
                            FROM (
                                    SELECT  dbo.DM_EMPLOYEE.*,
                                            dbo.DM_DEPARTMENT.DEPARTMENT_NAME
                                    FROM dbo.DM_EMPLOYEE
                                            LEFT JOIN dbo.DM_DEPARTMENT ON DM_DEPARTMENT.DEPARTMENT_ID = DM_EMPLOYEE.DEPARTMENT_ID
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
                                            SELECT dbo.DM_EMPLOYEE.*,
                                                   dbo.DM_DEPARTMENT.DEPARTMENT_NAME
                                            FROM dbo.DM_EMPLOYEE
                                                LEFT JOIN dbo.DM_DEPARTMENT
                                                    ON DM_DEPARTMENT.DEPARTMENT_ID = DM_EMPLOYEE.DEPARTMENT_ID
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
