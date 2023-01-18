#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Item : ObjectBase {
        public Dm_Item(FTSMain ftsmain) : base(ftsmain, "Dm_Item") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Item(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Item") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Item(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Item", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "UNIT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS1_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS1_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "UNIT_NAME", DbType.String, false));
            this.ExcludedFieldList = "ITEM_CLASS_NAME,ITEM_CLASS1_NAME,UNIT_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ITEM;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_ItemObject dmItemObject = new Dm_ItemObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmItemObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmItemObject;
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
                    Dm_ItemObject dmItemObject = new Dm_ItemObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmItemObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmItemObject);
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
                                     SELECT dbo.DM_ITEM.*,
                                                            ISNULL( UNIT_NAME,'') AS UNIT_NAME,
                                                            ISNULL( ITEM_CLASS_NAME,'') AS ITEM_CLASS_NAME	,
                                                            ISNULL( ITEM_CLASS1_NAME,'') AS	ITEM_CLASS1_NAME,
                                                            ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                            FROM dbo.DM_ITEM
                                                LEFT JOIN dbo.DM_UNIT
                                                    ON DM_UNIT.UNIT_ID = DM_ITEM.UNIT_ID
                                                LEFT JOIN dbo.DM_ITEM_CLASS
                                                    ON DM_ITEM_CLASS.ITEM_CLASS_ID = DM_ITEM.ITEM_CLASS_ID
                                                LEFT JOIN dbo.DM_ITEM_CLASS1
                                                    ON DM_ITEM_CLASS1.ITEM_CLASS1_ID = DM_ITEM.ITEM_CLASS1_ID
                                    WHERE 1 = 1 AND { this.ReGenerateFilterOrSorts(filter)}
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
                            FROM  dbo.DM_ITEM
                                                LEFT JOIN dbo.DM_UNIT
                                                    ON DM_UNIT.UNIT_ID = DM_ITEM.UNIT_ID
                                                LEFT JOIN dbo.DM_ITEM_CLASS
                                                    ON DM_ITEM_CLASS.ITEM_CLASS_ID = DM_ITEM.ITEM_CLASS_ID
                                                LEFT JOIN dbo.DM_ITEM_CLASS1
                                                    ON DM_ITEM_CLASS1.ITEM_CLASS1_ID = DM_ITEM.ITEM_CLASS1_ID
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



        public override void LoadEmptyData()
        {
            string sql = @" SELECT dbo.DM_ITEM.*,UNIT_NAME,
                                   ITEM_CLASS_NAME,
                                   ITEM_CLASS1_NAME
                            FROM dbo.DM_ITEM
                                LEFT JOIN dbo.DM_UNIT
                                    ON DM_UNIT.UNIT_ID = DM_ITEM.UNIT_ID
                                LEFT JOIN dbo.DM_ITEM_CLASS
                                    ON DM_ITEM_CLASS.ITEM_CLASS_ID = DM_ITEM.ITEM_CLASS_ID
                                LEFT JOIN dbo.DM_ITEM_CLASS1
                                    ON DM_ITEM_CLASS1.ITEM_CLASS1_ID = DM_ITEM.ITEM_CLASS1_ID
                            WHERE 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }

    }
}