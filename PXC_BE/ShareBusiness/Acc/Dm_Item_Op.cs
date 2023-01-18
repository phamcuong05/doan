#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Item_Op : ObjectBase {
        public Dm_Item_Op(FTSMain ftsmain) : base(ftsmain, "Dm_Item_Op") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Item_Op(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Item_Op") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Item_Op(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Item_Op", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_OP_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_OP_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ISSUE_RECEIPT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRANSFER_ITEM_OP_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRANSFER_ITEM_OP_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));

            this.ExcludedFieldList = "TRANSFER_ITEM_OP_NAME";
        }

        public override void LoadEmptyData()
        {
            string sql = "select *, '' AS TRANSFER_ITEM_OP_NAME from " + this.TableName + " where 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

        protected override string ReGenerateFilterOrSorts(string filterorsorts)
        {
            string reFilterOrSorts = filterorsorts;
            foreach (FieldInfo fieldInfo in this.FieldCollection)
            {
                if (fieldInfo.FieldName == "TRANSFER_ITEM_OP_NAME")
                {
                    reFilterOrSorts = reFilterOrSorts.Replace($"[" + fieldInfo.FieldName + "]", $"TRANFER.ITEM_OP_NAME");
                }
                else
                {
                    reFilterOrSorts = reFilterOrSorts.Replace($"[" + fieldInfo.FieldName + "]", $"{this.TableName}.{fieldInfo.FieldName}");
                }
            }
            return reFilterOrSorts;
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
                                    SELECT DM_ITEM_OP.*, TRANFER.ITEM_OP_NAME AS TRANSFER_ITEM_OP_NAME,
                                           ROW_NUMBER() OVER ({this.ReGenerateFilterOrSorts(this.GenerateSort(sorts))
                                                              }) AS ROW_INDEX
                                    FROM DM_ITEM_OP LEFT JOIN DM_ITEM_OP AS TRANFER ON DM_ITEM_OP.TRANSFER_ITEM_OP_ID = TRANFER.ITEM_OP_ID
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
                            FROM DM_ITEM_OP 
                                LEFT JOIN DM_ITEM_OP AS TRANFER ON DM_ITEM_OP.TRANSFER_ITEM_OP_ID = TRANFER.ITEM_OP_ID 
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

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ITEM_OP;
        }

        public static string GetTransferItemOpID(FTSMain ftsmain, string itemopid) {
            object obj = ftsmain.DbMain.ExecuteScalar(
                ftsmain.DbMain.GetSqlStringCommand("SELECT TRANSFER_ITEM_OP_ID FROM DM_ITEM_OP WHERE ITEM_OP_ID='" + itemopid + "'"));
            if (obj != null && obj != System.DBNull.Value) {
                return obj.ToString();
            } else {
                return string.Empty;
            }
        }
        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Item_OpObject dmItemOpObject = new Dm_Item_OpObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmItemOpObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmItemOpObject;
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
                    Dm_Item_OpObject dmItemOpObject = new Dm_Item_OpObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmItemOpObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmItemOpObject);
                }
            }

            return list;
        }
    }
}