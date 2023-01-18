#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Utilities;

#endregion

namespace FTS.Base.Report {
    [Serializable] public class Sys_Report_Config : ObjectBase {
        public string Report_Id = string.Empty;

        public Sys_Report_Config(FTSMain ftsmain, string report_id) : base(ftsmain, "sys_report_config") {
            this.Report_Id = report_id;
            this.LoadData();
            this.LoadFields();
        }

        public Sys_Report_Config(FTSMain ftsmain, bool isempty) : base(ftsmain, "sys_report_config") {
            if (!isempty) {
                this.LoadData();
            }
            this.LoadFields();
        }
        
        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "REPORT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONFIG_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONFIG_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONFIG_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONFIG_VALUE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void LoadData() {
            string sql = "select * from " + this.TableName;
            if (this.FieldList != string.Empty) {
                sql = "select " + this.FieldList + " from " + this.TableName;
            }
            sql += " where report_id=" + this.FTSMain.BuildParameterName("REPORT_ID");
            if (this.IdField != string.Empty) {
                sql += " order by CONFIG_ID";
            }
            if (this.DataTable != null) {
                this.DataTable.Clear();
            }
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            this.FTSMain.DbMain.AddInParameter(cmd, "REPORT_ID", DbType.String, this.Report_Id);
            this.FTSMain.DbMain.LoadDataSet(cmd, this.DataSet, this.TableName);
        }

        public override DataRow AddNew() {
            DataRow row = base.AddNew();
            row["report_id"] = this.Report_Id;
            row.EndEdit();
            return row;
        }

        public object GetConfigValue(string configid) {
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["CONFIG_ID"]};
            DataRow foundrow = this.DataTable.Rows.Find(configid);
            this.DataTable.PrimaryKey = null;
            if (foundrow != null) {
                return foundrow["config_value"];
            } else {
                throw new FTSException("MSG_INVALID_REPORT_CONFIG_ID");
            }
        }

        public object GetConfigValue(string configid,string configtype) {
            this.DataTable.PrimaryKey = new DataColumn[] { this.DataTable.Columns["CONFIG_ID"] };
            DataRow foundrow = this.DataTable.Rows.Find(configid);
            this.DataTable.PrimaryKey = null;
            if (foundrow != null) {
                return foundrow["config_value"];
            } else {
                if (configtype == "STRING") {
                    this.FTSMain.DbMain.ExecuteNonQuery(this.FTSMain.DbMain.GetSqlStringCommand(
                        "INSERT INTO SYS_REPORT_CONFIG(PR_KEY,REPORT_ID,CONFIG_ID,CONFIG_NAME,CONFIG_VALUE,CONFIG_TYPE,ACTIVE,USER_ID) VALUES(" +
                        Functions.GetPr_key("SYS_REPORT_CONFIG", this.FTSMain) + ",'" + this.Report_Id + "','" + configid + "','" + configid +
                        "','','STRING',1,'ADMIN')"));
                } else {
                    this.FTSMain.DbMain.ExecuteNonQuery(this.FTSMain.DbMain.GetSqlStringCommand(
                        "INSERT INTO SYS_REPORT_CONFIG(PR_KEY,REPORT_ID,CONFIG_ID,CONFIG_NAME,CONFIG_VALUE,CONFIG_TYPE,ACTIVE,USER_ID) VALUES(" +
                        Functions.GetPr_key("SYS_REPORT_CONFIG", this.FTSMain) + ",'" + this.Report_Id + "','" + configid + "','" + configid +
                        "','0','NUMBER',1,'ADMIN')"));

                }
                this.LoadData();
                return this.GetConfigValue(configid);
                //throw new FTSException("MSG_INVALID_REPORT_CONFIG_ID");
            }
        }

        public object GetConfigValueReturnNull(string configid) {
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["CONFIG_ID"]};
            DataRow foundrow = this.DataTable.Rows.Find(configid);
            this.DataTable.PrimaryKey = null;
            if (foundrow != null) {
                return foundrow["config_value"];
            } else {
                return null;
            }
        }

        public void SetConfigValue(string configid, object v) {
            this.DataTable.PrimaryKey = new DataColumn[] {this.DataTable.Columns["CONFIG_ID"]};
            DataRow foundrow = this.DataTable.Rows.Find(configid);
            this.DataTable.PrimaryKey = null;
            if (foundrow != null) {
                foundrow["config_value"] = v;
                foundrow.EndEdit();
            }
        }
    }
}