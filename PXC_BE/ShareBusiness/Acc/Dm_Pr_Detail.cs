#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Model.Paging;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Pr_Detail : ObjectBase
    {
        public bool AllowCreateEmployee = false;

        public Dm_Pr_Detail(FTSMain ftsmain) : base(ftsmain, "dm_pr_detail")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Pr_Detail(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS1_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_TYPE_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PHONE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMAIL", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TAX_FILE_NUMBER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IDENTITY_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_CODE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_BRANCH", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_ACCOUNT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_ACCOUNT_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BANK_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ADDRESS", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PROVINCE_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DISTRICT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PASSWORD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CREATE_DATE", DbType.Date, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS1_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_ACCOUNT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PROVINCE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DISTRICT_NAME", DbType.String, false));

            this.ExcludedFieldList = "PR_DETAIL_CLASS_NAME,PR_DETAIL_CLASS1_NAME,PR_ACCOUNT_NAME,PROVINCE_NAME,DISTRICT_NAME";
        }

        public override void LoadEmptyData()
        {
            string sql = @" SELECT DM_PR_DETAIL.*,DM_ACCOUNT.ACCOUNT_NAME AS  PR_ACCOUNT_NAME ,PR_DETAIL_CLASS_NAME,PR_DETAIL_CLASS1_NAME,'' AS PROVINCE_NAME, '' AS DISTRICT_NAME
                            FROM DM_PR_DETAIL
                                LEFT JOIN DM_ACCOUNT ON DM_PR_DETAIL.PR_ACCOUNT_ID = DM_ACCOUNT.ACCOUNT_ID
                                LEFT JOIN DM_PR_DETAIL_CLASS ON DM_PR_DETAIL.PR_DETAIL_CLASS_ID = DM_PR_DETAIL_CLASS.PR_DETAIL_CLASS_ID
                                LEFT JOIN DM_PR_DETAIL_CLASS1 ON DM_PR_DETAIL.PR_DETAIL_CLASS1_ID = DM_PR_DETAIL_CLASS1.PR_DETAIL_CLASS1_ID 
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

            string sql = $@"WITH tbView AS(
	                            SELECT DM_PR_DETAIL.*,
		                               DM_ACCOUNT.ACCOUNT_NAME AS PR_ACCOUNT_NAME,
		                               PR_DETAIL_CLASS_NAME,
		                               PR_DETAIL_CLASS1_NAME,
		                               PROVINCE_NAME,
		                               DISTRICT_NAME
	                            FROM DM_PR_DETAIL
		                            LEFT JOIN DM_ACCOUNT
			                            ON DM_PR_DETAIL.PR_ACCOUNT_ID = DM_ACCOUNT.ACCOUNT_ID
		                            LEFT JOIN DM_DISTRICT
			                            ON DM_PR_DETAIL.DISTRICT_ID = DM_DISTRICT.DISTRICT_ID
		                            LEFT JOIN DM_PROVINCE
			                            ON DM_PR_DETAIL.PROVINCE_ID = DM_PROVINCE.PROVINCE_ID
		                            LEFT JOIN DM_PR_DETAIL_CLASS
			                            ON DM_PR_DETAIL.PR_DETAIL_CLASS_ID = DM_PR_DETAIL_CLASS.PR_DETAIL_CLASS_ID
		                            LEFT JOIN DM_PR_DETAIL_CLASS1
			                            ON DM_PR_DETAIL.PR_DETAIL_CLASS1_ID = DM_PR_DETAIL_CLASS1.PR_DETAIL_CLASS1_ID
                            )
                            SELECT {this.GenerateQueryField(fiedlist)}
                                FROM
                                (
                                    SELECT PR_DETAIL_ID AS _KEY_,
                                           ROW_NUMBER() OVER ({this.GenerateSort(sorts)}) AS ROW_INDEX
                                    FROM tbView
                                    WHERE 1 = 1 AND {filter}
                                ) A INNER JOIN tbView
                                    ON tbView.{this.IdField} = A._KEY_
                                WHERE ROW_INDEX > {(pageindex - 1) * pagesize}
                                      AND ROW_INDEX + 0 <= {pageindex * pagesize}
                                ORDER BY ROW_INDEX ASC";

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
                           FROM DM_PR_DETAIL
                                LEFT JOIN DM_ACCOUNT ON DM_PR_DETAIL.PR_ACCOUNT_ID = DM_ACCOUNT.ACCOUNT_ID
                                LEFT JOIN DM_DISTRICT ON DM_PR_DETAIL.DISTRICT_ID = DM_DISTRICT.DISTRICT_ID
                                LEFT JOIN DM_PROVINCE ON DM_PR_DETAIL.PROVINCE_ID = DM_PROVINCE.PROVINCE_ID
                                LEFT JOIN DM_PR_DETAIL_CLASS ON DM_PR_DETAIL.PR_DETAIL_CLASS_ID = DM_PR_DETAIL_CLASS.PR_DETAIL_CLASS_ID
                                LEFT JOIN DM_PR_DETAIL_CLASS1 ON DM_PR_DETAIL.PR_DETAIL_CLASS1_ID = DM_PR_DETAIL_CLASS1.PR_DETAIL_CLASS1_ID  
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
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_PR_DETAIL;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Pr_DetailObject dm_pr_detailobject = new Dm_Pr_DetailObject();
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
                    Dm_Pr_DetailObject dm_pr_detailobject = new Dm_Pr_DetailObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dm_pr_detailobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dm_pr_detailobject);
                }
            }

            return list;
        }


        public override DataRow AddNew()
        {
            DataRow row = base.AddNew();
            row["PR_DETAIL_ID"] = "";
            row["PR_DETAIL_NAME"] = "";
            row["PR_DETAIL_TYPE_ID"] = PrDetailType.CUSTOMER;
            row.EndEdit();
            return row;
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
            int count = 0;
            foreach (DataRow row in this.DataTable.Rows)
            {
                //if (row.RowState == DataRowState.Added) {
                //    if (row["PR_DETAIL_TYPE_ID"].ToString() == FTS.ShareBusiness.Acc.PrDetailType.EMPLOYEE && this.AllowCreateEmployee == false) {
                //        throw (new FTSException(null, "MSG_NOT_ALLOW_CREATE_EMPLOYEE_HERE", this.TableName, "", count));
                //    }
                //}

                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    //    if (Functions.InListAbsolute(row["PR_DETAIL_CLASS_ID"].ToString(),
                    //        this.FTSMain.SystemVars.GetSystemVars("PR_DETAIL_CLASS_CHECK_TAX_CODE").ToString())) {
                    //        if (row["TAX_FILE_NUMBER"].ToString() == string.Empty) {
                    //            throw (new FTSException(null, "MSG_INVALID_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count));
                    //        }

                    //        if (!Functions.CheckTaxCode(row["TAX_FILE_NUMBER"].ToString())) {
                    //            throw (new FTSException(null, "MSG_INVALID_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count));
                    //        }

                    //        object obj;
                    //        if ((bool)this.FTSMain.SystemVars.GetSystemVars("PR_DETAIL_CLASS_CHECK_TAX_CODE_BY_ORG")) {
                    //            obj =
                    //                this.FTSMain.DbMain.ExecuteScalar(
                    //                    this.FTSMain.DbMain.GetSqlStringCommand("SELECT PR_DETAIL_ID FROM DM_PR_DETAIL WHERE PR_DETAIL_ID <> '" +
                    //                                                            row["PR_DETAIL_ID"].ToString() + "' AND TAX_FILE_NUMBER='" +
                    //                                                            row["TAX_FILE_NUMBER"] +
                    //                                                            "' AND PR_DETAIL_TYPE_ID='" + row["PR_DETAIL_TYPE_ID"] + "' AND " +
                    //                                                            this.FTSMain.IdManager.Filter(this.TableName,
                    //                                                                this.FTSMain.UserInfo.OrganizationID)));
                    //        } else {
                    //            obj =
                    //                this.FTSMain.DbMain.ExecuteScalar(
                    //                    this.FTSMain.DbMain.GetSqlStringCommand("SELECT PR_DETAIL_ID FROM DM_PR_DETAIL WHERE PR_DETAIL_ID <> '" +
                    //                                                            row["PR_DETAIL_ID"].ToString() + "' AND TAX_FILE_NUMBER='" +
                    //                                                            row["TAX_FILE_NUMBER"] +
                    //                                                            "' AND PR_DETAIL_TYPE_ID='" + row["PR_DETAIL_TYPE_ID"] + "'"));
                    //        }

                    //        if (obj != null && obj != System.DBNull.Value) {
                    //            throw (new FTSException(null, "MSG_DUPLICATE_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count, obj.ToString()));
                    //        }
                    //    }

                    //    if (Functions.InListAbsolute(row["PR_DETAIL_CLASS_ID"].ToString(),
                    //        this.FTSMain.SystemVars.GetSystemVars("PR_DETAIL_CLASS_CHECK_ID").ToString())) {
                    //        if (row["TAX_FILE_NUMBER"].ToString() == string.Empty) {
                    //            throw (new FTSException(null, "MSG_INVALID_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count));
                    //        }

                    //        object obj =
                    //            this.FTSMain.DbMain.ExecuteScalar(
                    //                this.FTSMain.DbMain.GetSqlStringCommand("SELECT PR_DETAIL_ID FROM DM_PR_DETAIL WHERE PR_DETAIL_ID <> '" +
                    //                                                        row["PR_DETAIL_ID"].ToString() + "' AND TAX_FILE_NUMBER='" +
                    //                                                        row["TAX_FILE_NUMBER"] +
                    //                                                        "' AND PR_DETAIL_TYPE_ID='" + row["PR_DETAIL_TYPE_ID"] + "'"));
                    //        if (obj != null && obj != System.DBNull.Value) {
                    //            throw (new FTSException(null, "MSG_DUPLICATE_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count, obj.ToString()));
                    //        }
                    //    }

                    //    if (row["TAX_FILE_NUMBER"].ToString() != string.Empty &&
                    //        (bool)this.FTSMain.SystemVars.GetSystemVars("NOT_ALLOW_DUPLICATE_TAX_CODE")) {
                    //        object obj =
                    //            this.FTSMain.DbMain.ExecuteScalar(
                    //                this.FTSMain.DbMain.GetSqlStringCommand("SELECT PR_DETAIL_ID FROM DM_PR_DETAIL WHERE PR_DETAIL_ID <> '" +
                    //                                                        row["PR_DETAIL_ID"].ToString() + "' AND TAX_FILE_NUMBER='" +
                    //                                                        row["TAX_FILE_NUMBER"] +
                    //                                                        "' AND PR_DETAIL_TYPE_ID='" + row["PR_DETAIL_TYPE_ID"] + "'"));
                    //        if (obj != null && obj != System.DBNull.Value) {
                    //            throw (new FTSException(null, "MSG_DUPLICATE_TAX_FILE_NUMBER", this.TableName, "TAX_FILE_NUMBER", count, obj.ToString()));
                    //        }
                    //    }

                    if (row["PR_DETAIL_TYPE_ID"].ToString() == string.Empty)
                    {
                        throw (new FTSException(null, "MSG_INVALID_PR_DETAIL_TYPE_ID", this.TableName, "PR_DETAIL_TYPE_ID", count));
                    }
                }

                count++;
            }
        }


        #region ImportData

        public override void ImportData(DataTable excelData, DataTable dm_template_detail)
        {
            DataColumn[] keys = this.DataTable.PrimaryKey;
            this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns[this.IdField] };
            dm_template_detail.PrimaryKey = new DataColumn[] { dm_template_detail.Columns["DATA_COLUMN_NAME"] };
            List<DataRow> listAdded = new List<DataRow>();
            foreach (DataRow row in excelData.Rows)
            {
                if (this.DataTable.Rows.Find(row[this.IdField]) == null)
                {
                    if (this.IsValidExcelData(row, dm_template_detail))
                    {
                        DataRow newrow = this.AddNew();
                        foreach (DataColumn c in excelData.Columns)
                        {
                            if (this.DataTable.Columns.IndexOf(c.ColumnName) >= 0)
                            {
                                newrow[c.ColumnName] = row[c.ColumnName];
                            }
                        }

                        newrow.EndEdit();
                        listAdded.Add(row);
                    }
                }
            }

            foreach (DataRow row in listAdded)
            {
                row.Delete();
            }

            excelData.AcceptChanges();
            this.DataTable.PrimaryKey = keys;
        }

        protected bool IsValidExcelData(DataRow row, DataTable dm_template_detail)
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                string columnname = c.ColumnName;
                DataRow templaterow = dm_template_detail.Rows.Find(columnname);
                if (templaterow != null)
                {
                    switch (templaterow["DATA_TYPE"].ToString().ToUpper())
                    {
                        case "STRING":
                            bool flag = false;
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = string.Empty;
                            }
                            else
                            {
                                //if (columnname.ToUpper() == "PR_DETAIL_CLASS_ID")
                                //{
                                //    this.DataSet.Tables["DM_PR_DETAIL_CLASS"].PrimaryKey = new DataColumn[]
                                //        {this.DataSet.Tables["DM_PR_DETAIL_CLASS"].Columns["PR_DETAIL_CLASS_ID"]};

                                //    if (this.DataSet.Tables["DM_PR_DETAIL_CLASS"].Rows.Find(row[c.ColumnName]) != null)
                                //    {
                                //        flag = true;
                                //        continue;
                                //    }
                                //    else
                                //    {
                                //        foreach (DataRow dr in this.DataSet.Tables["DM_PR_DETAIL_CLASS"].Rows)
                                //        {
                                //            if (dr["PR_DETAIL_CLASS_NAME"].ToString() == row[c.ColumnName].ToString())
                                //            {
                                //                row[c.ColumnName] = dr["PR_DETAIL_CLASS_ID"];
                                //                flag = true;
                                //                break;
                                //            }
                                //        }

                                //        if (flag == false && row[c.ColumnName].ToString().Trim().Length != 0)
                                //        {
                                //            return false;
                                //        }

                                //        continue;
                                //    }
                                //}
                                //else if (columnname.ToUpper() == "PR_DETAIL_CLASS1_ID")
                                //{
                                //    this.DataSet.Tables["DM_PR_DETAIL_CLASS1"].PrimaryKey = new DataColumn[]
                                //        {this.DataSet.Tables["DM_PR_DETAIL_CLASS1"].Columns["PR_DETAIL_CLASS1_ID"]};

                                //    if (this.DataSet.Tables["DM_PR_DETAIL_CLASS1"].Rows.Find(row[c.ColumnName]) != null)
                                //    {
                                //        flag = true;
                                //        continue;
                                //    }
                                //    else
                                //    {
                                //        foreach (DataRow dr in this.DataSet.Tables["DM_PR_DETAIL_CLASS1"].Rows)
                                //        {
                                //            if (dr["PR_DETAIL_CLASS1_NAME"].ToString() == row[c.ColumnName].ToString())
                                //            {
                                //                row[c.ColumnName] = dr["PR_DETAIL_CLASS1_ID"];
                                //                flag = true;
                                //                break;
                                //            }
                                //        }

                                //        if (flag == false && row[c.ColumnName].ToString().Trim().Length != 0)
                                //        {
                                //            return false;
                                //        }

                                //        continue;
                                //    }
                                //}
                                //else if (columnname.ToUpper() == "PROVINCE_ID")
                                //{
                                //    this.DataSet.Tables["DM_PROVINCES"].PrimaryKey = new DataColumn[]
                                //        {this.DataSet.Tables["DM_PROVINCES"].Columns["PROVINCE_ID"]};

                                //    if (this.DataSet.Tables["DM_PROVINCES"].Rows.Find(row[c.ColumnName]) != null)
                                //    {
                                //        flag = true;
                                //        continue;
                                //    }
                                //    else
                                //    {
                                //        foreach (DataRow dr in this.DataSet.Tables["DM_PROVINCES"].Rows)
                                //        {
                                //            if (dr["PROVINCE_NAME"].ToString() == row[c.ColumnName].ToString())
                                //            {
                                //                row[c.ColumnName] = dr["PROVINCE_ID"];
                                //                flag = true;
                                //                break;
                                //            }
                                //        }

                                //        if (flag == false && row[c.ColumnName].ToString().Trim().Length != 0)
                                //        {
                                //            return false;
                                //        }

                                //        continue;
                                //    }
                                //}
                                //else if (columnname.ToUpper() == "DISTRICT_ID")
                                //{
                                //    this.DataSet.Tables["DM_DISTRICTS"].PrimaryKey = new DataColumn[]
                                //        {this.DataSet.Tables["DM_DISTRICTS"].Columns["DISTRICT_ID"]};

                                //    if (this.DataSet.Tables["DM_DISTRICTS"].Rows.Find(row[c.ColumnName]) != null)
                                //    {
                                //        flag = true;
                                //        continue;
                                //    }
                                //    else
                                //    {
                                //        foreach (DataRow dr in this.DataSet.Tables["DM_DISTRICTS"].Rows)
                                //        {
                                //            if (dr["DISTRICT_NAME"].ToString() == row[c.ColumnName].ToString())
                                //            {
                                //                row[c.ColumnName] = dr["DISTRICT_ID"];
                                //                flag = true;
                                //                break;
                                //            }
                                //        }

                                //        if (flag == false && row[c.ColumnName].ToString().Trim().Length != 0)
                                //        {
                                //            return false;
                                //        }

                                //        continue;
                                //    }
                                //}
                                //else if (columnname.ToUpper() == "TAX_FILE_NUMBER" || columnname.ToUpper() == "PHONE" ||
                                //         columnname.ToUpper() == "FAX" ||
                                //         columnname.ToUpper() == "PR_DETAIL_NAME_EN" || columnname.ToUpper() == "ADDRESS")
                                //{
                                //    if (row[c.ColumnName].ToString() == "")
                                //    {
                                //        row[c.ColumnName] = " ";
                                //    }
                                //}
                                try
                                {
                                    string cellvalue = row[c.ColumnName].ToString();
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "DECIMAL":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    decimal cellvalue = Convert.ToDecimal(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "INT":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    Int32 cellvalue = Convert.ToInt32(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "BOOLEAN":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    Int16 cellvalue = Convert.ToInt16(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                        case "DATE":
                            if (row[c.ColumnName] == System.DBNull.Value || row[c.ColumnName].ToString().Trim() == string.Empty)
                            {
                                row[c.ColumnName] = 0;
                            }
                            else
                            {
                                try
                                {
                                    DateTime cellvalue = Convert.ToDateTime(row[c.ColumnName]);
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                            break;
                    }
                }
            }

            return true;
        }

        #endregion

        public override void EndEdit()
        {
            base.EndEdit();
            foreach (DataRow row in this.DataTable.Rows)
            {
                if (this.IsValidRow(row))
                {
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        //DataRow foundrow = this.GetDm("DM_PR_DETAIL_CLASS").Rows.Find(row["pr_detail_class_id"]);
                        //if (foundrow != null) {
                        //    row["PR_DETAIL_TYPE_ID"] = foundrow["PR_DETAIL_TYPE_ID"];
                        //    row.EndEdit();
                        //}
                    }
                }
            }
        }


        public override string GetAutoID()
        {
            if (!this.IsValidRow(0))
            {
                return string.Empty;
            }

            if (this.DataTable.Rows[0].RowState != DataRowState.Added)
            {
                return string.Empty;
            }

            string mMaskChar = "[0-9|A-Z|?|_|.]";
            string sql = "SELECT * FROM SYS_ID_DEFAULT WHERE TABLE_NAME='" + this.TableName + "' AND ORGANIZATION_ID='" +
                         this.FTSMain.UserInfo.OrganizationID +
                         "' AND EXISTS(SELECT 'TRUE' FROM SYS_ID_STRUCT WHERE SYS_ID_DEFAULT.TABLE_NAME=SYS_ID_STRUCT.TABLE_NAME AND SYS_ID_DEFAULT.PART_ID=SYS_ID_STRUCT.PART_ID)";
            DataTable sysiddefault = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SYS_ID_DEFAULT");
            sysiddefault.PrimaryKey = new DataColumn[] { sysiddefault.Columns["PART_ID"] };
            sql = "SELECT * FROM SYS_ID_STRUCT WHERE TABLE_NAME='" + this.TableName + "'";
            DataTable sysidstruct = this.FTSMain.DbMain.LoadDataTable(this.FTSMain.DbMain.GetSqlStringCommand(sql), "SYS_ID_STRUCT");
            sysidstruct.PrimaryKey = new DataColumn[] { sysidstruct.Columns["PART_ID"] };
            string mask = string.Empty;
            string idvalue = string.Empty;
            int parts = this.FTSMain.TableManager.IdParts(this.TableName);
            int startpos = 0;
            for (int i = 1; i <= parts; i++)
            {
                DataRow structrow = sysidstruct.Rows.Find(i);
                DataRow defaultrow = sysiddefault.Rows.Find(i);
                if (structrow == null || defaultrow == null)
                {
                    return string.Empty;
                }

                int partlength = (int)structrow["PART_LENGTH"];
                string partvalue = string.Empty;
                if (defaultrow != null && (Int16)defaultrow["IS_AUTO_INCREMENT"] == 1)
                {
                    string[] incrementfields = defaultrow["INCREMENT_FIELDS"].ToString().Split(',');
                    if (incrementfields.Length == 0 || (incrementfields.Length == 1 && incrementfields[0] == string.Empty))
                    {
                        string[] incrementparts = defaultrow["INCREMENT_PARTS"].ToString().Split(',');
                        if (incrementparts.Length == 0 || (incrementparts.Length == 1 && incrementparts[0] == string.Empty))
                        {
                            sql = "SELECT MAX(SUBSTRING(PR_DETAIL_ID," + (startpos + 1) + "," + partlength + ")) FROM DM_PR_DETAIL";
                            object oj = this.FTSMain.DbMain.ExecuteScalar(this.FTSMain.DbMain.GetSqlStringCommand(sql));
                            if (oj != null && oj != System.DBNull.Value)
                            {
                                try
                                {
                                    partvalue = (Convert.ToInt32(oj) + 1).ToString().PadLeft(partlength, '0');
                                    if (partvalue.Length > partlength)
                                    {
                                        partvalue = partvalue.Substring(0, partlength);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                partvalue = "1".PadLeft(partlength, '0');
                            }
                        }
                        else
                        {
                            string filter = "1=1";
                            bool valid = true;
                            for (int k = 0; k < incrementparts.Length; k++)
                            {
                                int startpos1 = this.FTSMain.IdManager.PartStartPos(this.TableName, Convert.ToInt32(incrementparts[k]));
                                int partlenth1 = this.FTSMain.IdManager.PartLength(this.TableName, Convert.ToInt32(incrementparts[k]));
                                if (idvalue.Length >= startpos1 + partlenth1)
                                {
                                    string incrementpartvalue =
                                        idvalue.ToString()
                                            .Trim()
                                            .Substring(this.FTSMain.IdManager.PartStartPos(this.TableName, Convert.ToInt32(incrementparts[k])),
                                                this.FTSMain.IdManager.PartLength(this.TableName, Convert.ToInt32(incrementparts[k])));
                                    if (incrementpartvalue != string.Empty)
                                    {
                                        filter += " AND SUBSTRING(" + this.IdField + "," +
                                                  (this.FTSMain.IdManager.PartStartPos(this.TableName, Convert.ToInt32(incrementparts[k])) + 1) +
                                                  "," +
                                                  this.FTSMain.IdManager.PartLength(this.TableName, Convert.ToInt32(incrementparts[k])) + ")='" +
                                                  incrementpartvalue + "'";
                                    }
                                    else
                                    {
                                        valid = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    valid = false;
                                    break;
                                }
                            }

                            if (valid)
                            {
                                sql = "SELECT MAX(SUBSTRING(PR_DETAIL_ID," + (startpos + 1) + "," + partlength + ")) FROM DM_PR_DETAIL WHERE " +
                                      filter;
                                object oj = this.FTSMain.DbMain.ExecuteScalar(this.FTSMain.DbMain.GetSqlStringCommand(sql));
                                if (oj != null && oj != System.DBNull.Value)
                                {
                                    try
                                    {
                                        partvalue = (Convert.ToInt32(oj) + 1).ToString().PadLeft(partlength, '0');
                                        if (partvalue.Length > partlength)
                                        {
                                            partvalue = partvalue.Substring(0, partlength);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else
                                {
                                    partvalue = "1".PadLeft(partlength, '0');
                                }
                            }
                        }
                    }
                    else
                    {
                        string filter = "1=1";
                        bool valid = true;
                        for (int k = 0; k < incrementfields.Length; k++)
                        {
                            string incrementfieldvalue = this.GetValue(incrementfields[k]).ToString().Trim();
                            if (incrementfieldvalue != string.Empty)
                            {
                                filter += " AND " + incrementfields[k] + "='" + this.GetValue(incrementfields[k]) + "'";
                            }
                            else
                            {
                                valid = false;
                                break;
                            }
                        }

                        if (valid)
                        {
                            sql = "SELECT MAX(SUBSTRING(PR_DETAIL_ID," + (startpos + 1) + "," + partlength + ")) FROM DM_PR_DETAIL WHERE " + filter;
                            object oj = this.FTSMain.DbMain.ExecuteScalar(this.FTSMain.DbMain.GetSqlStringCommand(sql));
                            if (oj != null && oj != System.DBNull.Value)
                            {
                                try
                                {
                                    partvalue = (Convert.ToInt32(oj) + 1).ToString().PadLeft(partlength, '0');
                                    if (partvalue.Length > partlength)
                                    {
                                        partvalue = partvalue.Substring(0, partlength);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                partvalue = "1".PadLeft(partlength, '0');
                            }
                        }
                    }
                }
                else
                {
                    if (defaultrow != null && defaultrow["DEFAULT_VALUE"].ToString().Trim() != string.Empty)
                    {
                        partvalue = defaultrow["DEFAULT_VALUE"].ToString();
                    }
                    else
                    {
                        if (defaultrow != null && defaultrow["DEFAULT_VALUE_FIELD"].ToString().Trim() != string.Empty)
                        {
                            partvalue = this.GetValue(defaultrow["DEFAULT_VALUE_FIELD"].ToString().Trim()).ToString();
                        }
                    }
                }

                if (partvalue != string.Empty)
                {
                    if ((Int16)defaultrow["ALLOW_NEW_VALUE"] == 1)
                    {
                        for (int j = 0; j < partlength; j++)
                        {
                            mask += mMaskChar;
                        }

                        idvalue += partvalue;
                    }
                    else
                    {
                        mask += partvalue;
                        idvalue += partvalue;
                    }
                }
                else
                {
                    for (int j = 0; j < partlength; j++)
                    {
                        mask += mMaskChar;
                        idvalue += "_";
                    }
                }

                startpos += partlength;
            }

            string currentvalue = this.GetValue(this.IdField).ToString().Trim();
            for (int i = 0; i < idvalue.ToUpper().Length; i++)
            {
                if (idvalue.ToUpper().Substring(i, 1) == "_" && currentvalue.Length > i && currentvalue.Substring(i, 1) != string.Empty)
                {
                    if (idvalue.Length > i + 1)
                    {
                        idvalue = idvalue.Substring(0, i) + currentvalue.Substring(i, 1) + idvalue.Substring(i + 1);
                    }
                    else
                    {
                        idvalue = idvalue.Substring(0, i) + currentvalue.Substring(i, 1);
                    }
                }
            }

            this.SetValue(this.IdField, idvalue.ToUpper());
            return mask;
        }
    }
}