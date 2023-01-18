#region

using System;
using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Report {
    public class Report_FormulaManager : ManagerBase {
        public string report_id = "";
        public Sys_Report_Formula Sys_Report_Formula;
        public Sys_Report_Formula_Detail Sys_Report_Formula_Detail;

        public Report_FormulaManager(FTSMain ftsmain, string tran_id, string reportid) : base(ftsmain, tran_id) {
            this.report_id = reportid;
            this.LoadDm();
            this.Sys_Report_Formula = new Sys_Report_Formula(ftsmain, this.DataSet, this.TranId, this.report_id);
            this.Sys_Report_Formula_Detail = new Sys_Report_Formula_Detail(ftsmain, this.DataSet, this.report_id);
            this.ObjectList.Add(this.Sys_Report_Formula);
            this.ObjectList.Add(this.Sys_Report_Formula_Detail);
            this.TranDateField = "";
            this.TranNoField = "";
            this.TranIdField = "tran_id";
            this.RegisterDefaultValues();
            this.LoadFullData();
        }
        
        public void LoadFullData() {
            this.Sys_Report_Formula.LoadFullData();
            this.Sys_Report_Formula_Detail.LoadFullData();
        }

        public override void UpdateData() {
            this.EndEdit();
            this.Sys_Report_Formula_Detail.UpdateSqlString();
            base.UpdateData();
            this.Sys_Report_Formula.UpdateDescription();
        }

        public void DeleteData(int pos) {
            this.EndEdit();
            Guid fr_key = (Guid) this.Sys_Report_Formula.DataTable.Rows[pos]["pr_key"];
            this.Sys_Report_Formula_Detail.DeleteAll(fr_key);
            this.Sys_Report_Formula.DeleteAtPosition(pos);
        }

        public virtual DataRow AddNewDetail(int detailID, Guid fr_key) {
            DataRow row = this.ObjectList[detailID].AddNew();
            row["fr_key"] = fr_key;
            row.EndEdit();
            return row;
        }

        public void CopyRecord(DataRow parentrow) {
            if (parentrow == null) {
                return;
            }
            List<DataTable> tablelist = new List<DataTable>();
            DataView dv = new DataView(this.Sys_Report_Formula_Detail.DataTable, "fr_key=" + ((Guid) parentrow["pr_key"]).ToString("##"), "",
                DataViewRowState.CurrentRows);
            DataRow parentrownew = this.Sys_Report_Formula.AddNew(parentrow);
            foreach (DataRowView drv in dv) {
                DataRow childrownew = this.Sys_Report_Formula_Detail.AddNew(drv.Row);
                childrownew["fr_key"] = parentrownew["pr_key"];
                childrownew.EndEdit();
            }
        }

        public override void CheckBusinessRules() {
            foreach (ObjectBase ob in this.ObjectList) {
                ob.CheckBusinessRules();
            }
        }



    }
}