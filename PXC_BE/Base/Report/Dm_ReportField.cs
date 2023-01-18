// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:51
// Project Name:              ReportBase
// Project Filename:          ReportBase.csproj
// Project Item Name:         Sys_ReportField.cs
// Project Item Filename:     Sys_ReportField.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Security;

#endregion

namespace FTS.Base.Report {
    public class Sys_ReportField : ObjectBase {
        private string mReport_Id;
        public bool AllData = true;

        public string ReportID {
            get { return this.mReport_Id;}
            set { this.mReport_Id = value; }
        }
        public Sys_ReportField(FTSMain ftsmain, string report_id) : base(ftsmain, "sys_reportfield") {
            this.NameField = "REPORT_ID";
            this.mReport_Id = report_id;
            string sql = "select * from sys_reportfield where report_id=" + this.FTSMain.BuildParameterName("report_id");
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, report_id);
            this.LoadDataByCommand(cmd);
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["FIELD_ID"]};
            this.LoadFields();
        }

        public void ReLoadData() {
            string sql = "select * from sys_reportfield where report_id=" + this.FTSMain.BuildParameterName("report_id");
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, this.mReport_Id);
            this.LoadDataByCommand(cmd);
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["FIELD_ID"]};
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "REPORT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_GROUP_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VISIBLE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DECIMAL_DIGIT", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_WIDTH", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_ORDER", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_SUM", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PART", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FORMULA", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHOW_IN_GROUP", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "HIDE_DETAIL", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FIELD_ANGLE", DbType.Int32, false));
        }

        public override void LoadData() {
            base.LoadData();
        }

        public DataRow GetCurrentRow(int pos) {
            return this.DataTable.Rows[pos];
        }

        public DataRow GetRow(string fieldid) {
            DataColumn[] oj = this.DataTable.PrimaryKey;
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["field_id"]};
            DataRow foundrow = this.DataTable.Rows.Find(fieldid);
            this.DataTable.PrimaryKey = oj;
            return foundrow;
        }

        public DataRow GetNextRow(int pos) {
            int curorder = (int) this.DataTable.Rows[pos]["field_order"];
            DataView dv = new DataView(this.DataTable, "visible=1 and field_order > " + curorder, "field_order", DataViewRowState.CurrentRows);
            if (dv.Count > 0) {
                return dv[0].Row;
            } else {
                return null;
            }
        }

        public DataRow GetPreviousRow(int pos) {
            int curorder = (int) this.DataTable.Rows[pos]["field_order"];
            DataView dv = new DataView(this.DataTable, "visible=1 and field_order < " + curorder, "field_order desc", DataViewRowState.CurrentRows);
            if (dv.Count > 0) {
                return dv[0].Row;
            } else {
                return null;
            }
        }

        public void LoadVisibleData() {
            this.DataTable.Clear();
            string sql = "select * from sys_reportfield where report_id=" + this.FTSMain.BuildParameterName("report_id") + " and visible=1";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, this.mReport_Id);
            this.LoadDataByCommand(cmd);
            this.AllData = false;
        }

        public void LoadAllData() {
            this.DataTable.Clear();
            string sql = "select * from sys_reportfield where report_id=" + this.FTSMain.BuildParameterName("report_id");
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "report_id", DbType.String, this.mReport_Id);
            this.LoadDataByCommand(cmd);
            this.AllData = true;
        }

        public override void UpdateData() {
            bool allowupdate = true;
                if (
                    !this.FTSMain.SecurityManager.CheckSecurityInvisible(FTS.Base.Security.FTSFunctionCollection.REPORT_MANAGEMENT, DataAction.EditAction,
                        string.Empty)) {
                    allowupdate = false;
                }
            if (allowupdate) {
                base.UpdateData();
            } else {
                this.AcceptChanges();
            }
        }
    }
}