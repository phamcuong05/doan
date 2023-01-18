#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Report {
    public class Sys_Report_Formula : ObjectBase {
        private string Tran_Id;
        public string report_id = "";

        public Sys_Report_Formula(FTSMain ftsmain, DataSet ds, string tran_id, string reportid) : base(ftsmain, ds, "Sys_Report_Formula", true) {
            this.report_id = reportid;
            this.Tran_Id = tran_id;
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "REPORT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LIST_ORDER", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORDER_FIELD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_FIELD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DESCRIPTION", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ROW_FORMULA", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FONT_BOLD", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VISIBLE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "POSITIVE_RESULT", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "GROUP_FIELD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "GROUP_TABLE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPLY_ALL_ROWS", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_SUMMARY", DbType.Boolean, false));
        }

        public override void CheckBusinessRules() {
            base.CheckBusinessRules();
        }

        public override void LoadData() {
            base.LoadData();
        }

        public void LoadFullData() {
            string sql = "select * from sys_report_formula where report_id=" + this.FTSMain.BuildParameterName("REPORT_ID") + " order by ID_FIELD";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "REPORT_ID", DbType.String, this.report_id);
            base.LoadDataByCommand(cmd);
            foreach (DataRow row in this.DataTable.Rows) {
                row["DESCRIPTION"] = this.FTSMain.ResourceManager.GetFinancialReportDescription(this.report_id, row["ID_FIELD"].ToString(),
                    row["DESCRIPTION"].ToString());
            }
            this.DataTable.AcceptChanges();
        }

        public void UpdateDescription() {
            foreach (DataRow row in this.DataTable.Rows) {
                this.FTSMain.ResourceManager.SetFinancialReportDescription(this.report_id, row["ID_FIELD"].ToString(), row["DESCRIPTION"].ToString());
            }
        }

        public override DataRow AddNew() {
            DataRow row = base.AddNew();
            row["tran_id"] = this.Tran_Id;
            row["IS_SUMMARY"] = 1;
            row["VISIBLE"] = 1;
            row["report_id"] = this.report_id;
            row.EndEdit();
            return row;
        }

        public override object GetDefaultValue(string fieldname) {
            return string.Empty;
        }
    }
}