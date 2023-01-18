#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Utilities;
#endregion

namespace FTS.Base.Report {
    public class Sys_Report_Formula_Detail : ObjectBase {
        public string report_id = "";

        public Sys_Report_Formula_Detail(FTSMain ftsmain, DataSet ds, string reportid)
            : base(ftsmain, ds, "SYS_REPORT_FORMULA_DETAIL", true) {
            this.report_id = reportid;
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid,
                true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FR_KEY", DbType.Guid,
                false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "COLUMN_NO", DbType.Int32,
                false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LIST_ORDER",
                DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DATASOURCE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "OPERATOR", DbType.String,
                false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIX_VALUE",
                DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_ID_CONTRA",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_ACCOUNT_ID_CONTRA",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SQLSTRING",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_TAX_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_PR_DETAIL_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_PR_DETAIL_CLASS_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_DETAIL_CLASS1_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEALER_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "INDUSTRY_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_EXPENSE_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_EXPENSE_CLASS_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "JOB_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_JOB_CLASS_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "JOB_ID", DbType.String,
                false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_JOB_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FA_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FA_SOURCE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FA_OPERATION_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FA_STATUS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WAREHOUSE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_WAREHOUSE_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WAREHOUSE_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_WAREHOUSE_CLASS_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_ITEM_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_CLASS_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_ITEM_CLASS_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_OP_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_ITEM_OP_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ITEM_SOURCE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_ITEM_SOURCE_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BUDGET_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "INSURANCE_SOURCE_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "REINSURANCE_COST_ID",
                DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_BUDGET_INSURANCE_SOURCE_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_BUDGET_PR_DETAIL_CLASS_ID",
                DbType.Boolean, false));

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID", DbType.String,
                false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_TRAN_ID",
                DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOT_FOLLOW",
                DbType.Boolean, false));
        }

        public override void CheckBusinessRules() {
            base.CheckBusinessRules();
        }

        public override void LoadData() {
            base.LoadData();
        }

        public void LoadFullData() {
            string sql =
                "select sys_report_formula_detail.* from sys_report_formula_detail where exists(select 'true' from sys_report_formula where sys_report_formula.pr_key=sys_report_formula_detail.fr_key and report_id=" +
                this.FTSMain.BuildParameterName("REPORT_ID") + ") order by COLUMN_NO";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "REPORT_ID", DbType.String, this.report_id);
            base.LoadDataByCommand(cmd);
        }

        public override DataRow AddNew() {
            DataRow row = base.AddNew();
            row["operator"] = "+";
            row["ORGANIZATION_ID"] = string.Empty;
            row["WAREHOUSE_ID"] = string.Empty;
            row["ITEM_SOURCE_ID"] = string.Empty;
            row["IS_BUDGET_INSURANCE_SOURCE_ID"] = 0;
            row["IS_BUDGET_PR_DETAIL_CLASS_ID"] = 0;
            row.EndEdit();
            return row;
        }

        public override DataRow AddNew(DataRow row1) {
            DataRow row = base.AddNew(row1);
            row["operator"] = "+";
            row["ORGANIZATION_ID"] = string.Empty;
            row["WAREHOUSE_ID"] = string.Empty;
            row["ITEM_SOURCE_ID"] = string.Empty;
            row["IS_BUDGET_INSURANCE_SOURCE_ID"] = 0;
            row["IS_BUDGET_PR_DETAIL_CLASS_ID"] = 0;
            row.EndEdit();
            return row;
        }

        public override DataRow InsertRecord(int pos) {
            DataRow row = base.InsertRecord(pos);
            row["operator"] = "+";
            row["ORGANIZATION_ID"] = string.Empty;
            row["WAREHOUSE_ID"] = string.Empty;
            row["ITEM_SOURCE_ID"] = string.Empty;
            row["IS_BUDGET_INSURANCE_SOURCE_ID"] = 0;
            row["IS_BUDGET_PR_DETAIL_CLASS_ID"] = 0;
            return row;
        }

        public void UpdateSqlString() {
            string sqlbudget = string.Empty;
            foreach (DataRow row in this.DataTable.Rows) {
                if (row.RowState != DataRowState.Deleted) {
                    string sqlquery = "";
                    string ACCOUNT_ID = row["ACCOUNT_ID"].ToString().Trim();
                    string ACCOUNT_ID_CONTRA = row["ACCOUNT_ID_CONTRA"].ToString().Trim();
                    string VAT_TAX_ID = row["VAT_TAX_ID"].ToString().Trim();
                    string PR_DETAIL_ID = row["PR_DETAIL_ID"].ToString().Trim();
                    string PR_DETAIL_CLASS_ID = row["PR_DETAIL_CLASS_ID"].ToString().Trim();
                    string PR_DETAIL_CLASS1_ID = row["PR_DETAIL_CLASS1_ID"].ToString().Trim();
                    string DEALER_CLASS_ID = row["DEALER_CLASS_ID"].ToString().Trim();
                    string INDUSTRY_ID = row["INDUSTRY_ID"].ToString().Trim();
                    string EXPENSE_ID = row["EXPENSE_ID"].ToString().Trim();
                    string EXPENSE_CLASS_ID = row["EXPENSE_CLASS_ID"].ToString().Trim();
                    string TRAN_ID = row["TRAN_ID"].ToString().Trim();
                    string ITEM_ID = row["ITEM_ID"].ToString().Trim();
                    string ITEM_CLASS_ID = row["ITEM_CLASS_ID"].ToString().Trim();
                    string WAREHOUSE_ID = row["WAREHOUSE_ID"].ToString().Trim();
                    string WAREHOUSE_CLASS_ID = row["WAREHOUSE_CLASS_ID"].ToString().Trim();
                    string ITEM_OP_ID = row["ITEM_OP_ID"].ToString().Trim();
                    string ITEM_SOURCE_ID = row["ITEM_SOURCE_ID"].ToString().Trim();
                    string ORGANIZATION_ID = row["ORGANIZATION_ID"].ToString().Trim();
                    string BUDGET_ID = row["BUDGET_ID"].ToString().Trim();
                    string DEPARTMENT_ID = row["DEPARTMENT_ID"].ToString().Trim();
                    string INSURANCE_SOURCE_ID = row["INSURANCE_SOURCE_ID"].ToString().Trim();
                    string REINSURANCE_COST_ID = row["REINSURANCE_COST_ID"].ToString().Trim();
                    string FA_SOURCE_ID = row["FA_SOURCE_ID"].ToString().Trim();
                    string AndOrAccount_Contra =
                        (string)FunctionsBase.IIF((Int16)row["NOT_ACCOUNT_ID_CONTRA"] == 0, " and ", " and not ");
                    string AndOrPr_Detail =
                        (string)FunctionsBase.IIF((Int16)row["NOT_PR_DETAIL_ID"] == 0, " and ", " and not ");
                    string AndOrPr_Detail_Class =
                        (string)FunctionsBase.IIF((Int16)row["NOT_PR_DETAIL_CLASS_ID"] == 0, " and ", " and not ");
                    string AndOrExpense =
                        (string)FunctionsBase.IIF((Int16)row["NOT_EXPENSE_ID"] == 0, " and ", " and not ");
                    string AndOrExpense_Class =
                        (string)FunctionsBase.IIF((Int16)row["NOT_EXPENSE_CLASS_ID"] == 0, " and ", " and not ");
                    bool notfollow = (Int16)row["NOT_FOLLOW"] == 1 ? true : false;
                    string AndOrTran = (string)FunctionsBase.IIF((Int16)row["NOT_TRAN_ID"] == 0, " and ", " and not ");
                    string AndOrItem = (string)FunctionsBase.IIF((Int16)row["NOT_ITEM_ID"] == 0, " and ", " and not ");
                    string AndOrItemClass = (string)FunctionsBase.IIF((Int16)row["NOT_ITEM_CLASS_ID"] == 0, " and ", " and not ");
                    string AndOrWarehouse = (string)FunctionsBase.IIF((Int16)row["NOT_WAREHOUSE_ID"] == 0, " and ", " and not ");
                    string AndOrWarehouseClass = (string)FunctionsBase.IIF((Int16)row["NOT_WAREHOUSE_CLASS_ID"] == 0, " and ", " and not ");
                    string AndOrItemOp = (string)FunctionsBase.IIF((Int16)row["NOT_ITEM_OP_ID"] == 0, " and ", " and not ");
                    string AndOrItemSource = (string)FunctionsBase.IIF((Int16)row["NOT_ITEM_SOURCE_ID"] == 0, " and ", " and not ");
                    if (row["operator"].ToString().Trim() == "-") {
                        sqlquery = "select sum(0-";
                    } else {
                        sqlquery = "select sum(";
                    }

                    switch (row["DATASOURCE_ID"].ToString().Trim()) {
                        case "000000": // gia tri go vao
                            sqlquery = sqlquery + "fix_value) as tong_tien from Sys_Report_Formula_Detail";
                            break;

                        case "CA0001": //so du no dau nam
                        case "CA0002": //DINH MUC CHI PHI KY TRUOC
                        case "CA0003": //DINH MUC CHI PHI DEN DAU KY
                            sqlquery = sqlquery + "EXPENSE_AMOUNT) as tong_tien from CA_BUDGET_SOURCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail + this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "), "") +
                                (string)FunctionsBase.IIF(EXPENSE_ID.Trim() != "", " AND TRAN_ID <> 'BG' ", "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class + this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "CA1001": //so du no dau nam
                        case "CA1002": //DINH MUC CHI PHI KY TRUOC
                        case "CA1003": //DINH MUC CHI PHI DEN DAU KY
                            sqlquery = sqlquery + "SALARY_AMOUNT) as tong_tien from CA_BUDGET_SOURCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail + this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "), "") +
                                (string)FunctionsBase.IIF(EXPENSE_ID.Trim() != "", " AND TRAN_ID <> 'BG' ", "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class + this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;

                        case "KT0001": //so du no dau nam
                        case "KT2001": //so du no dau nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "", " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail + this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "), "") +
                                (string)FunctionsBase.IIF(EXPENSE_ID.Trim() != "", " AND TRAN_ID <> 'BG' ", "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class + this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0002": //so du co dau nam
                        case "KT2002": //so du co dau nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0003":
                        case "KT0005":
                        case "KT0007":
                        case "KT0009":
                        case "KT0011":
                        case "KT0013":
                        case "KT0015":
                        case "KT0101":
                        case "KT0103":
                        case "KT0105":
                        case "KT0109":
                        case "KT0111":
                        case "KT2003": //phat sinh no nam truoc
                        case "KT2005": //phat sinh no nam truoc
                        case "KT2007": //phat sinh no nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0004":
                        case "KT0006":
                        case "KT0008":
                        case "KT0010":
                        case "KT0012":
                        case "KT0014":
                        case "KT0016":
                        case "KT0102":
                        case "KT0104":
                        case "KT0106":
                        case "KT0110":
                        case "KT0112":
                        case "KT2004": //phat sinh co nam truoc
                        case "KT2006": //phat sinh co nam truoc
                        case "KT2008": //phat sinh co nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='CRD'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT3000": // chi phi do dang dau ky
                        case "KT3009": // chi phi do dang dau ky
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from CA_BEGINNING_AMOUNT_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT3001": // chi phi phan bo 
                        case "KT3002":
                        case "KT3003":
                        case "KT3004":
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from CA_EXPENSE_RESULT_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0901": //Du no cong no
                        case "KT0903": //Du no cong no
                        case "KT0905": //Du no cong no
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where 1=1 " +
                                       (string)
                                       FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                           " and " +
                                           this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                           AndOrPr_Detail_Class +
                                           this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                           AndOrPr_Detail +
                                           this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                           " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0902": //Du co cong no
                        case "KT0904": //Du co cong no
                        case "KT0906": //Du co cong no
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where 1=1 " +
                                       (string)
                                       FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                           " and " +
                                           this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                           AndOrPr_Detail_Class +
                                           this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                           AndOrPr_Detail +
                                           this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                           " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0911": //Du no cong no
                        case "KT0913": //Du no cong no
                        case "KT0915": //Du no cong no
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where 1=1 " +
                                       (string)
                                       FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                           " and " +
                                           this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                           AndOrPr_Detail_Class +
                                           this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                           AndOrPr_Detail +
                                           this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                           " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT0912": //Du co cong no
                        case "KT0914": //Du co cong no
                        case "KT0916": //Du co cong no
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where 1=1 " +
                                       (string)
                                       FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                           " and " +
                                           this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                           AndOrPr_Detail_Class +
                                           this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                           AndOrPr_Detail +
                                           this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID,
                                               " = "), "") +
                                       (string)
                                       FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                           " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT5001": //ps no theo ty le dinh muc nam
                            //Tinh toan ty le tuong ung
                            sqlbudget = "ISNULL((select top 1 PERCENT_LIMITED as PERCENT_LIMITED from BUDGET where BUDGET_ID = '" + BUDGET_ID +
                                        "' and DAY_START = " +
                                        Functions.ParseDate(this.FTSMain.DayStartOfCurrentYear) + " and DAY_END = " +
                                        Functions.ParseDate(this.FTSMain.DayEndOfCurrentYear) +
                                        " and ORGANIZATION_ID = LEDGER_SUMMARY_VIEW.ORGANIZATION_ID and VALID_DATE <= LEDGER_SUMMARY_VIEW.TRAN_DATE";
                            if ((Int16)row["IS_BUDGET_INSURANCE_SOURCE_ID"] == 1 && INSURANCE_SOURCE_ID != string.Empty)
                                sqlbudget += " and INSURANCE_SOURCE_ID = '" + INSURANCE_SOURCE_ID + "'";
                            if ((Int16)row["IS_BUDGET_PR_DETAIL_CLASS_ID"] == 1 && PR_DETAIL_CLASS_ID != string.Empty)
                                sqlbudget += " and PR_DETAIL_CLASS_ID = '" + PR_DETAIL_CLASS_ID + "'";
                            sqlbudget += " order by VALID_DATE desc),0) as TYLE";

                            sqlquery = sqlquery + "AMOUNT/100*TYLE) as tong_tien from ( select AMOUNT, " + sqlbudget +
                                       " from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            sqlquery += " ) as TMP ";
                            break;
                        case "KT5002": //ps co theo ty le dinh muc nam
                            //Tinh toan ty le tuong ung
                            sqlbudget = "ISNULL((select top 1 PERCENT_LIMITED as PERCENT_LIMITED from BUDGET where BUDGET_ID = '" + BUDGET_ID +
                                        "' and DAY_START = " +
                                        Functions.ParseDate(this.FTSMain.DayStartOfCurrentYear) + " and DAY_END = " +
                                        Functions.ParseDate(this.FTSMain.DayEndOfCurrentYear) +
                                        " and ORGANIZATION_ID = LEDGER_SUMMARY_VIEW.ORGANIZATION_ID and VALID_DATE <= LEDGER_SUMMARY_VIEW.TRAN_DATE";
                            if ((Int16)row["IS_BUDGET_INSURANCE_SOURCE_ID"] == 1 && INSURANCE_SOURCE_ID != string.Empty)
                                sqlbudget += " and INSURANCE_SOURCE_ID = '" + INSURANCE_SOURCE_ID + "'";
                            if ((Int16)row["IS_BUDGET_PR_DETAIL_CLASS_ID"] == 1 && PR_DETAIL_CLASS_ID != string.Empty)
                                sqlbudget += " and PR_DETAIL_CLASS_ID = '" + PR_DETAIL_CLASS_ID + "'";
                            sqlbudget += " order by VALID_DATE desc),0) as TYLE";

                            sqlquery = sqlquery + "AMOUNT/100*TYLE) as tong_tien from ( select AMOUNT, " + sqlbudget +
                                       " from LEDGER_SUMMARY_VIEW where Debit_Credit='CRD'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            sqlquery += " ) as TMP ";
                            break;
                        case "KT5003": //ps no theo ty le dinh muc trong ky
                            //Tinh toan ty le tuong ung
                            sqlbudget = "ISNULL((select top 1 PERCENT_LIMITED as PERCENT_LIMITED from BUDGET where BUDGET_ID = '" + BUDGET_ID + "' " +
                                        " and ORGANIZATION_ID = LEDGER_SUMMARY_VIEW.ORGANIZATION_ID and VALID_DATE <= LEDGER_SUMMARY_VIEW.TRAN_DATE";
                            if ((Int16)row["IS_BUDGET_INSURANCE_SOURCE_ID"] == 1 && INSURANCE_SOURCE_ID != string.Empty)
                                sqlbudget += " and INSURANCE_SOURCE_ID = '" + INSURANCE_SOURCE_ID + "'";
                            if ((Int16)row["IS_BUDGET_PR_DETAIL_CLASS_ID"] == 1 && PR_DETAIL_CLASS_ID != string.Empty)
                                sqlbudget += " and PR_DETAIL_CLASS_ID = '" + PR_DETAIL_CLASS_ID + "'";
                            sqlbudget += " order by VALID_DATE desc),0) as TYLE";

                            sqlquery = sqlquery + "AMOUNT/100*TYLE) as tong_tien from ( select AMOUNT, " + sqlbudget +
                                       " from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            sqlquery += " ) as TMP ";
                            break;
                        case "KT5004": //ps co theo ty le dinh muc nam
                            //Tinh toan ty le tuong ung
                            sqlbudget = "ISNULL((select top 1 PERCENT_LIMITED as PERCENT_LIMITED from BUDGET where BUDGET_ID = '" + BUDGET_ID + "' " +
                                        " and ORGANIZATION_ID = LEDGER_SUMMARY_VIEW.ORGANIZATION_ID and VALID_DATE <= LEDGER_SUMMARY_VIEW.TRAN_DATE";
                            if ((Int16)row["IS_BUDGET_INSURANCE_SOURCE_ID"] == 1 && INSURANCE_SOURCE_ID != string.Empty)
                                sqlbudget += " and INSURANCE_SOURCE_ID = '" + INSURANCE_SOURCE_ID + "'";
                            if ((Int16)row["IS_BUDGET_PR_DETAIL_CLASS_ID"] == 1 && PR_DETAIL_CLASS_ID != string.Empty)
                                sqlbudget += " and PR_DETAIL_CLASS_ID = '" + PR_DETAIL_CLASS_ID + "'";
                            sqlbudget += " order by VALID_DATE desc),0) as TYLE";

                            sqlquery = sqlquery + "AMOUNT/100*TYLE) as tong_tien from ( select AMOUNT, " + sqlbudget +
                                       " from LEDGER_SUMMARY_VIEW where Debit_Credit='CRD'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            sqlquery += " ) as TMP ";
                            break;
                        // so ke hoach ky nay
                        case "KH1001":
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH1002":
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH1003":
                        case "KH1005":
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH0001":
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH0002":
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH0003":
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from BUDGET_VIEW where 1=1 ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "KH0004": //Ty le dinh muc nam
                        case "KH1004": //Ty le dinh muc ky nay
                            sqlquery = sqlquery + "PERCENT_LIMITED)/count(*) as tong_tien from BUDGET_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(BUDGET_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_BUDGET", "BUDGET_ID", BUDGET_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "");
                            break;
                        case "TSCD01":
                        case "TSCD02":
                        case "TSCD03":
                        case "TSCD04":
                        case "TSCD05":
                        case "TSCD08":
                        case "TSCD10":
                        case "TSCD09":
                        case "TSCD14":
                            sqlquery = string.Empty;
                            break;
                        case "VAT001":
                        case "VAT002":
                        case "VAT005":
                        case "VAT007":
                            sqlquery = sqlquery + "AMOUNT_ITEM) as tong_tien from VAT_TRANSACTION_VIEW where 1=1 ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(VAT_TAX_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_VAT_TAX", "VAT_TAX_ID", VAT_TAX_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VAT009":
                            sqlquery = sqlquery + "AMOUNT_ITEM) as tong_tien from VAT_TRANSACTION_VIEW where amount =0 ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(VAT_TAX_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_VAT_TAX", "VAT_TAX_ID", VAT_TAX_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VAT003":
                        case "VAT004":
                        case "VAT006":
                        case "VAT008":
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from VAT_TRANSACTION_VIEW where 1=1 ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(VAT_TAX_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_VAT_TAX", "VAT_TAX_ID", VAT_TAX_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT0000": //sl ton kho dau nam
                        case "VT0100": //sl tonkho dau nam truoc
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from WAREHOUSE_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT0010": //tien ton kho dau nam
                        case "VT0110": //tien tonkho dau nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from WAREHOUSE_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1000": //sl nhap kho ky nay
                        case "VT1100": //sl nhap kho ky nay nam truoc
                        case "VT1001": //sl nhap kho den dau ky
                        case "VT1101": //sl nhap kho den dau ky nay nam truoc
                        case "VT1002": //sl nhap kho den cuoi ky
                        case "VT1102": //sl nhap kho den cuoi ky nay nam truoc
                        case "VT1003": //sl nhap kho ky truoc
                        case "VT1103": //sl nhap kho ky truoc nam truoc
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from WAREHOUSE_VIEW where ISSUE_RECEIVE='N'  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1010": //so tien nhap kho ky nay
                        case "VT1110": //so tien nhap kho ky nay nam truoc
                        case "VT1011": //so tien nhap kho den dau ky
                        case "VT1111": //so tien nhap kho den dau ky nay nam truoc
                        case "VT1012": //so tien nhap kho den cuoi ky
                        case "VT1112": //so tien nhap kho den cuoi ky nay nam truoc
                        case "VT1013": //so tien nhap kho ky truoc
                        case "VT1113": //so tien nhap kho ky truoc nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from WAREHOUSE_VIEW where ISSUE_RECEIVE='N'  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1005": //sl nhap mua ky nay
                        case "VT1105": //sl nhap mua ky nay nam truoc
                        case "VT1006": //sl nhap mua den dau ky
                        case "VT1106": //sl nhap mua den dau ky nay nam truoc
                        case "VT1007": //sl nhap mua den cuoi ky
                        case "VT1107": //sl nhap mua den cuoi ky nay nam truoc
                        case "VT1008": //sl nhap mua ky truoc
                        case "VT1108": //sl nhap mua ky truoc nam truoc
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from PURCHASE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1015": //so tien nhap mua ky nay
                        case "VT1115": //so tien nhap mua ky nay nam truoc
                        case "VT1016": //so tien nhap mua den dau ky
                        case "VT1116": //so tien nhap mua den dau ky nay nam truoc
                        case "VT1017": //so tien nhap mua den cuoi ky
                        case "VT1117": //so tien nhap mua den cuoi ky nay nam truoc
                        case "VT1018": //so tien nhap mua ky truoc
                        case "VT1118": //so tien nhap mua ky truoc nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from PURCHASE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1025": //TIEN THUE nhap mua ky nay
                        case "VT1125": //TIEN THUEnhap mua ky nay nam truoc
                        case "VT1026": //TIEN THUE nhap mua den dau ky
                        case "VT1126": //TIEN THUE nhap mua den dau ky nay nam truoc
                        case "VT1027": //TIEN THUE nhap mua den cuoi ky
                        case "VT1127": //TIEN THUE nhap mua den cuoi ky nay nam truoc
                        case "VT1028": //TIEN THUE nhap mua ky truoc
                        case "VT1128": //TIEN THUE nhap mua ky truoc nam truoc
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from PURCHASE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT1035": //TIEN HT  nhap mua ky nay
                        case "VT1135": //TIEN HT nhap mua ky nay nam truoc
                        case "VT1036": //TIEN HT nhap mua den dau ky
                        case "VT1136": //TIEN HT nhap mua den dau ky nay nam truoc
                        case "VT1037": //TIEN HT nhap mua den cuoi ky
                        case "VT1137": //TIEN HT nhap mua den cuoi ky nay nam truoc
                        case "VT1038": //TIEN HT nhap mua ky truoc
                        case "VT1138": //TIEN HT nhap mua ky truoc nam truoc
                            sqlquery = sqlquery + "FIXED_AMOUNT) as tong_tien from PURCHASE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2000": //sl xuat kho ky nay
                        case "VT2100": //sl xuat kho ky nay nam truoc
                        case "VT2001": //sl xuat kho den dau ky
                        case "VT2101": //sl xuat kho den dau ky nay nam truoc
                        case "VT2002": //sl xuat kho den cuoi ky
                        case "VT2102": //sl xuat kho den cuoi ky nay nam truoc
                        case "VT2003": //sl xuat kho ky truoc
                        case "VT2103": //sl xuat kho ky truoc nam truoc
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from WAREHOUSE_VIEW where ISSUE_RECEIVE='X'  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2010": //so tien xuat kho ky nay
                        case "VT2110": //so tien xuat kho ky nay nam truoc
                        case "VT2011": //so tien xuat kho den dau ky
                        case "VT2111": //so tien xuat kho den dau ky nay nam truoc
                        case "VT2012": //so tien xuat kho den cuoi ky
                        case "VT2112": //so tien xuat kho den cuoi ky nay nam truoc
                        case "VT2013": //so tien xuat kho ky truoc
                        case "VT2113": //so tien xuat kho ky truoc nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from WAREHOUSE_VIEW where ISSUE_RECEIVE='X'  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2005": //sl xuat mua ky nay
                        case "VT2105": //sl xuat mua ky nay nam truoc
                        case "VT2006": //sl xuat mua den dau ky
                        case "VT2106": //sl xuat mua den dau ky nay nam truoc
                        case "VT2007": //sl xuat mua den cuoi ky
                        case "VT2107": //sl xuat mua den cuoi ky nay nam truoc
                        case "VT2008": //sl xuat mua ky truoc
                        case "VT2108": //sl xuat mua ky truoc nam truoc
                            sqlquery = sqlquery + "QUANTITY) as tong_tien from SALE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2015": //so tien xuat mua ky nay
                        case "VT2115": //so tien xuat mua ky nay nam truoc
                        case "VT2016": //so tien xuat mua den dau ky
                        case "VT2116": //so tien xuat mua den dau ky nay nam truoc
                        case "VT2017": //so tien xuat mua den cuoi ky
                        case "VT2117": //so tien xuat mua den cuoi ky nay nam truoc
                        case "VT2018": //so tien xuat mua ky truoc
                        case "VT2118": //so tien xuat mua ky truoc nam truoc
                            sqlquery = sqlquery + "AMOUNT) as tong_tien from SALE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2025": //TIEN THUE xuat mua ky nay
                        case "VT2125": //TIEN THUExuat mua ky nay nam truoc
                        case "VT2026": //TIEN THUE xuat mua den dau ky
                        case "VT2126": //TIEN THUE xuat mua den dau ky nay nam truoc
                        case "VT2027": //TIEN THUE xuat mua den cuoi ky
                        case "VT2127": //TIEN THUE xuat mua den cuoi ky nay nam truoc
                        case "VT2028": //TIEN THUE xuat mua ky truoc
                        case "VT2128": //TIEN THUE xuat mua ky truoc nam truoc
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from SALE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "VT2035": //TIEN HT  xuat mua ky nay
                        case "VT2135": //TIEN HT xuat mua ky nay nam truoc
                        case "VT2036": //TIEN HT xuat mua den dau ky
                        case "VT2136": //TIEN HT xuat mua den dau ky nay nam truoc
                        case "VT2037": //TIEN HT xuat mua den cuoi ky
                        case "VT2137": //TIEN HT xuat mua den cuoi ky nay nam truoc
                        case "VT2038": //TIEN HT xuat mua ky truoc
                        case "VT2138": //TIEN HT xuat mua ky truoc nam truoc
                            sqlquery = sqlquery + "FIXED_AMOUNT) as tong_tien from SALE_VIEW where 1=1  ";
                            sqlquery +=
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem + this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_CLASS_ID.Trim() != "",
                                    AndOrWarehouseClass +
                                    this.GetFilter("DM_WAREHOUSE_CLASS", "WAREHOUSE_CLASS_ID", WAREHOUSE_CLASS_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(WAREHOUSE_ID.Trim() != "",
                                    AndOrWarehouse + this.GetFilter("DM_WAREHOUSE", "WAREHOUSE_ID", WAREHOUSE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_OP_ID.Trim() != "",
                                    AndOrItemOp + this.GetFilter("DM_ITEM_OP", "ITEM_OP_ID", ITEM_OP_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_SOURCE_ID.Trim() != "",
                                    AndOrItemSource + this.GetFilter("DM_ITEM_SOURCE", "ITEM_SOURCE_ID", ITEM_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        //PJICO
                        case "KT4001": //so du no dau nam PHI
                            sqlquery = sqlquery + "GOODS_AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4002": //so du no dau nam THUE
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4003": //so du co dau nam PHI
                            sqlquery = sqlquery + "GOODS_AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4004": //so du co dau nam THUE
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from LEDGER_BALANCE_VIEW where 1=1  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        //tiep
                        case "KT4005": //PS NO DEN DAU KY PHI
                        case "KT4009": //PS NO TRONG KY PHI
                        case "KT4013": //PS NO TRONG KY PHI
                            sqlquery = sqlquery + "GOODS_AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4006": //so du no dau nam THUE
                        case "KT4010": //so du no dau nam THUE
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4007": //so du co dau nam PHI
                        case "KT4011": //so du co dau nam PHI
                        case "KT4014": //so du co dau nam PHI
                            sqlquery = sqlquery + "GOODS_AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='CRD'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        case "KT4008": //so du co dau nam THUE
                        case "KT4012": //so du co dau nam THUE
                            sqlquery = sqlquery + "VAT_TAX_AMOUNT) as tong_tien from LEDGER_SUMMARY_VIEW where Debit_Credit='DEB'  ";
                            sqlquery +=
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID.Trim() != "",
                                    " and " + this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID", ACCOUNT_ID, " LIKE "), "") +
                                (string)
                                FunctionsBase.IIF(ACCOUNT_ID_CONTRA.Trim() != "",
                                    AndOrAccount_Contra +
                                    this.GetFilter("DM_ACCOUNT", "ACCOUNT_ID_CONTRA", ACCOUNT_ID_CONTRA, " LIKE "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(ITEM_ID.Trim() != "",
                                    AndOrItem +
                                    this.GetFilter("DM_ITEM", "ITEM_ID", ITEM_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ITEM_CLASS_ID.Trim() != "",
                                    AndOrItemClass +
                                    this.GetFilter("DM_ITEM_CLASS", "ITEM_CLASS_ID", ITEM_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " and " +
                                    this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_ID.Trim() != "",
                                    AndOrPr_Detail +
                                    this.GetFilter("DM_PR_DETAIL", "PR_DETAIL_ID", PR_DETAIL_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS_ID.Trim() != "",
                                    AndOrPr_Detail_Class +
                                    this.GetFilter("DM_PR_DETAIL_CLASS", "PR_DETAIL_CLASS_ID", PR_DETAIL_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(PR_DETAIL_CLASS1_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "PR_DETAIL_CLASS1_ID", PR_DETAIL_CLASS1_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(DEALER_CLASS_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_PR_DETAIL_CLASS1", "DEALER_CLASS_ID", DEALER_CLASS_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(INDUSTRY_ID.Trim() != "",
                                    " AND " +
                                    this.GetFilter("DM_INDUSTRY", "INDUSTRY_ID", INDUSTRY_ID, " = "),
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_ID.Trim() != "",
                                    " AND TRAN_ID <> 'BG' ",
                                    "") +
                                (string)
                                FunctionsBase.IIF(EXPENSE_CLASS_ID.Trim() != "",
                                    AndOrExpense_Class +
                                    this.GetFilter("DM_EXPENSE_CLASS", "EXPENSE_CLASS_ID", EXPENSE_CLASS_ID, " = "), "") +
                                (notfollow
                                    ? " AND IS_FOLLOW=0"
                                    : string.Empty) +
                                (string)
                                FunctionsBase.IIF(TRAN_ID.Trim() != "",
                                    AndOrTran + this.GetFilter("SYS_TRAN", "TRAN_ID", TRAN_ID, " = "), "") +
                                (string)FunctionsBase.IIF(DEPARTMENT_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_DEPARTMENT", "DEPARTMENT_ID", DEPARTMENT_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(INSURANCE_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_INSURANCE_SOURCE", "INSURANCE_SOURCE_ID", INSURANCE_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(FA_SOURCE_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_CAPITAL_SOURCE", "CAPITAL_SOURCE_ID", FA_SOURCE_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(REINSURANCE_COST_ID.Trim() != "",
                                    " AND " + this.GetFilter("DM_REINSURANCE_COST", "REINSURANCE_COST_ID", REINSURANCE_COST_ID, " = "), "") +
                                (string)
                                FunctionsBase.IIF(ORGANIZATION_ID.Trim() != "",
                                    " AND " + this.GetOrganizationFilter("DM_ORGANIZATION", "ORGANIZATION_ID", ORGANIZATION_ID, " = "), "");
                            break;
                        default:
                            sqlquery = string.Empty;
                            break;
                    }

                    try {
                        if (sqlquery != string.Empty) {
                            string sqlquery1 = sqlquery.Replace("where", "where 1=0 and ");
                            DbCommand dax = this.FTSMain.DbMain.GetSqlStringCommand(sqlquery1);
                            this.FTSMain.DbMain.ExecuteNonQuery(dax);
                        }
                    } catch (Exception ex) {
                        throw (new FTSException("MSG_QUERYERROR"));
                    }

                    row["sqlstring"] = sqlquery;
                    row.EndEdit();
                }
            }
        }

        private string GetFilter(string tablename, string strField, string strGiaTri, string strOper) {
            StringBuilder strKQua = new StringBuilder();
            if (strGiaTri.Length > 0) {
                string str = strGiaTri.Trim() + ",";
                int nSo;
                if (strOper.Length == 0) {
                    strOper = "=";
                }

                while (str.Length > 0) {
                    nSo = str.IndexOf(",");
                    if (strOper.ToUpper().Trim() == "LIKE") {
                        strKQua.Append(" ").Append(strField).Append(" ").Append(strOper).Append(" '").Append(
                            str.Substring(0, nSo) + "%'");
                    } else {
                        string idvalue = str.Substring(0, nSo);
                        string newidvalue = this.ParseParentValue(tablename, idvalue);
                        if (idvalue == newidvalue) {
                            strKQua.Append(" ").Append(strField).Append(" ").Append(strOper).Append(" '").Append(idvalue + "'");
                        } else {
                            strKQua.Append(" ").Append(strField).Append(" ").Append("LIKE").Append(" '").Append(newidvalue + "%'");
                        }
                    }

                    str = str.Substring(nSo + 1).Trim();
                    if (str.Length > 0) {
                        strKQua.Append(" Or ");
                    }
                }

                strKQua.Insert(0, "(").Append(")");
            }

            return strKQua.ToString();
        }

        private string ParseParentValue(string tablename, string idvalue) {
            int idparts = this.FTSMain.TableManager.IdParts(tablename);
            if (idparts <= 1) {
                return idvalue;
            } else {
                int partstart = 0;
                string parentcharacter = this.FTSMain.SystemVars.GetSystemVars("ID_PARENT_CHARACTER").ToString();
                string anycharater = this.FTSMain.SystemVars.GetSystemVars("ID_ANY_CHARACTER").ToString(); //"[0-9,A-Z,a-z]";
                string returnvalue = string.Empty;
                if (parentcharacter != string.Empty && anycharater != string.Empty) {
                    for (int i = 0; i < idparts; i++) {
                        int partlength = this.FTSMain.IdManager.PartLength(tablename, i + 1);
                        string partvalue = idvalue.Substring(partstart, partlength);
                        string partparentvalue = string.Empty;
                        for (int j = 0; j < partlength; j++) {
                            partparentvalue += parentcharacter;
                        }

                        if (partvalue == partparentvalue) {
                            for (int j = 0; j < partlength; j++) {
                                returnvalue += anycharater;
                            }
                        } else {
                            returnvalue += partvalue;
                        }

                        partstart += partlength;
                    }

                    return returnvalue;
                } else {
                    return idvalue;
                }
            }
        }

        private string GetOrganizationFilter(string tablename, string strField, string strGiaTri, string strOper) {
            StringBuilder strKQua = new StringBuilder();
            if (strGiaTri.Length > 0) {
                string str = strGiaTri.Trim() + ",";
                int nSo;
                if (strOper.Length == 0) {
                    strOper = "=";
                }

                while (str.Length > 0) {
                    nSo = str.IndexOf(",");
                    string idvalue = str.Substring(0, nSo);
                    string query = this.FTSMain.DmOrganization.GetOrganizationFilter(idvalue);
                    strKQua.Append(" ").Append(query);
                    str = str.Substring(nSo + 1).Trim();
                    if (str.Length > 0) {
                        strKQua.Append(" Or ");
                    }
                }

                strKQua.Insert(0, "(").Append(")");
            }

            return strKQua.ToString();
        }

        public override object GetDefaultValue(string fieldname) {
            return string.Empty;
        }
    }
}