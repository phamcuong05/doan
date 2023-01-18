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
    public class Dm_Pr_Detail_Class : ObjectBase {
        public Dm_Pr_Detail_Class(FTSMain ftsmain) : base(ftsmain, "dm_pr_detail_class") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Pr_Detail_Class(FTSMain ftsmain, bool isempty) : base(ftsmain, "dm_pr_detail_class") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Pr_Detail_Class(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "dm_pr_detail_class", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_TYPE_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_TYPE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_EMPLOYEE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_AGENT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_DEPARTMENT", DbType.Boolean, false));
            this.ExcludedFieldList = "PR_DETAIL_TYPE_NAME";
        }

        public override void SetRole() {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_PR_DETAIL_CLASS;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Pr_Detail_ClassObject dm_pr_detailobject = new Dm_Pr_Detail_ClassObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dm_pr_detailobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dm_pr_detailobject;
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
                    Dm_Pr_Detail_ClassObject dm_pr_detailobject = new Dm_Pr_Detail_ClassObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dm_pr_detailobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    dm_pr_detailobject.SetValue("PR_DETAIL_TYPE_NAME", PrDetailType.GetPrDetailTypeName(this.FTSMain, row["PR_DETAIL_TYPE_ID"].ToString()));
                    list.Add(dm_pr_detailobject);
                }
            }

            return list;
        }

        public override void LoadEmptyData()
        {
            string sql = "select dbo.DM_PR_DETAIL_CLASS.*, cast('' as NVARCHAR(100)) AS PR_DETAIL_TYPE_NAME from DM_PR_DETAIL_CLASS where 1=0";
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
                                     SELECT dbo.DM_PR_DETAIL_CLASS.*,
                                                            ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                            FROM dbo.DM_PR_DETAIL_CLASS
                                    WHERE 1 = 1 AND {filter.Replace($"[PR_DETAIL_TYPE_NAME]", $"PR_DETAIL_TYPE_ID")}
                                ) tb
                                WHERE tb.ROW_INDEX > {(pageindex - 1) * pagesize} AND tb.ROW_INDEX <= {(pageindex * pagesize)};";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            foreach (var group in filterlist)
            {
                foreach (var filtervalue in group.Filters)
                {
                    if (filtervalue.ParamName.Contains("PR_DETAIL_TYPE_NAME"))
                    {
                        filtervalue.Value = filtervalue.ParamName.Substring(filtervalue.ParamName.Length - 2, 2);
                    }
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

            string sql = "SELECT COUNT(*) FROM " + this.TableName + " WHERE " +   filter.Replace($"[PR_DETAIL_TYPE_NAME]", $"PR_DETAIL_TYPE_ID");

            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            foreach (var group in filtergrouplist)
            {

                foreach (var filtervalue in group.Filters)
                {
                    if (filtervalue.ParamName.Contains("PR_DETAIL_TYPE_NAME"))
                    {
                        filtervalue.Value = filtervalue.ParamName.Substring(filtervalue.ParamName.Length - 2, 2);
                    }
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