#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Item_Class : ObjectBase {
        public Dm_Item_Class(FTSMain ftsmain) : base(ftsmain, "dm_item_class") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Item_Class(FTSMain ftsmain, bool isempty) : base(ftsmain, "dm_item_class") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Item_Class(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "dm_item_class", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "INV_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.ExcludedFieldList = "ACCOUNT_NAME";
        }

        public override void SetRole() {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ITEM_CLASS;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Item_ClassObject dmItemClassObject = new Dm_Item_ClassObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmItemClassObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmItemClassObject;
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
                    Dm_Item_ClassObject dmItemClassobject = new Dm_Item_ClassObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmItemClassobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmItemClassobject);
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
                                     SELECT dbo.DM_ITEM_CLASS.*,
                                                            ISNULL(ACCOUNT_NAME,'') AS ACCOUNT_NAME,
                                                            ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                            FROM dbo.DM_ITEM_CLASS
                                                LEFT JOIN dbo.DM_ACCOUNT ON DM_ACCOUNT.ACCOUNT_ID = DM_ITEM_CLASS.INV_ACCOUNT_ID
                                                
                                    WHERE 1 = 1 AND {filter.Replace($"[ACTIVE]", $"DM_ITEM_CLASS.ACTIVE")}
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



        public override void LoadEmptyData()
        {
            string sql = @"  SELECT dbo.DM_ITEM_CLASS.*, dbo.DM_ACCOUNT.ACCOUNT_NAME
                                  FROM dbo.DM_ITEM_CLASS , dbo.DM_ACCOUNT  WHERE 1=0";
            this.FTSMain.DbMain.LoadDataSet(this.FTSMain.DbMain.GetSqlStringCommand(sql), this.DataSet, this.TableName);
            this.DataTable = this.DataSet.Tables[this.TableName];
        }


    }
}