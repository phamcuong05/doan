#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Warehouse : ObjectBase {
        public Dm_Warehouse(FTSMain ftsmain) : base(ftsmain, "Dm_Warehouse") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Warehouse(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Warehouse") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Warehouse(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Warehouse", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WAREHOUSE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WAREHOUSE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_USE_WAREHOUSE", DbType.Int16, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

            this.ExcludedFieldList = "DEPARTMENT_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_WAREHOUSE;
        }

        public override void LoadEmptyData()
        {
            string sql = @" SELECT DM_WAREHOUSE.*, '' AS DEPARTMENT_NAME
                            FROM DM_WAREHOUSE
                            WHERE 1=0";
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
                                    SELECT DM_WAREHOUSE.*,DM_DEPARTMENT.DEPARTMENT_NAME,
                                           ROW_NUMBER() OVER ({this.ReGenerateFilterOrSorts(this.GenerateSort(sorts))}) AS ROW_INDEX
                                     FROM DM_WAREHOUSE LEFT JOIN DM_DEPARTMENT ON DM_WAREHOUSE.DEPARTMENT_ID = DM_DEPARTMENT.DEPARTMENT_ID
                                    WHERE 1 = 1 AND {this.ReGenerateFilterOrSorts(filter)}
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

            string sql = "SELECT COUNT(*) FROM DM_WAREHOUSE LEFT JOIN DM_DEPARTMENT ON DM_WAREHOUSE.DEPARTMENT_ID = DM_DEPARTMENT.DEPARTMENT_ID WHERE " 
                + this.ReGenerateFilterOrSorts(filter);

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
                Dm_WarehouseObject dmWarehouseObject = new Dm_WarehouseObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmWarehouseObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmWarehouseObject;
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
                    Dm_WarehouseObject dmWarehouseObject = new Dm_WarehouseObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmWarehouseObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmWarehouseObject);
                }
            }

            return list;
        }
    }
}