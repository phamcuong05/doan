using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Cashbank_Limit : ObjectBase
    {
        public bool AllowCreateEmployee = false;

        public Dm_Cashbank_Limit(FTSMain ftsmain) : base(ftsmain, "Dm_Cashbank_Limit")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Cashbank_Limit(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Cashbank_Limit")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Cashbank_Limit(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Cashbank_Limit", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VALID_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LIMIT", DbType.Decimal, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOTES", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CREATE_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "MODIFY_DATE", DbType.Date, false));

            this.ExcludedFieldList = "ORGANIZATION_NAME,ACCOUNT_NAME,BANK_NAME";
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_CASHBANK_LIMIT;
        }

        public override void LoadEmptyData()
        {
            string sql = "select *, '' AS ORGANIZATION_NAME, '' AS ACCOUNT_NAME, '' AS BANK_NAME from " + this.TableName + " where 1=0";
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
                                    SELECT DM_CASHBANK_LIMIT.*, DM_ORGANIZATION.ORGANIZATION_NAME, DM_ACCOUNT.ACCOUNT_NAME, DM_BANK.BANK_NAME,
                                           ROW_NUMBER() OVER ({ this.ReGenerateFilterOrSorts(this.GenerateSort(sorts))}) AS ROW_INDEX
                                    FROM DM_CASHBANK_LIMIT 
                                         LEFT JOIN DM_BANK ON DM_CASHBANK_LIMIT.BANK_ID = DM_BANK.BANK_ID
                                         LEFT JOIN DM_ACCOUNT ON DM_CASHBANK_LIMIT.ACCOUNT_ID = DM_ACCOUNT.ACCOUNT_ID
                                         LEFT JOIN DM_ORGANIZATION ON DM_CASHBANK_LIMIT.ORGANIZATION_ID = DM_ORGANIZATION.ORGANIZATION_ID
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

            string sql = $@"SELECT COUNT(*) FROM DM_CASHBANK_LIMIT                                          
                                LEFT JOIN DM_BANK ON DM_CASHBANK_LIMIT.BANK_ID = DM_BANK.BANK_ID
                                LEFT JOIN DM_ACCOUNT ON DM_CASHBANK_LIMIT.ACCOUNT_ID = DM_ACCOUNT.ACCOUNT_ID
                                LEFT JOIN DM_ORGANIZATION ON DM_CASHBANK_LIMIT.ORGANIZATION_ID = DM_ORGANIZATION.ORGANIZATION_ID
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
                Dm_Cashbank_LimitObject dm_cashbank_limitobject = new Dm_Cashbank_LimitObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dm_cashbank_limitobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dm_cashbank_limitobject;
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
                    Dm_Cashbank_LimitObject dm_cashbank_limitobject = new Dm_Cashbank_LimitObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dm_cashbank_limitobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dm_cashbank_limitobject);
                }
            }

            return list;
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

    }
}
