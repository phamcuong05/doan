using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Department : ObjectBase
    {
        public Dm_Department(FTSMain ftsmain) : base(ftsmain, "Dm_Department")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Department(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Department")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Department(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Department", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTMENT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_DEPARTMENT;
        }

        public override void UpdateData() {
            DataSet ds = this.DataSet.GetChanges();
            if (ds != null && ds.Tables[this.TableName] != null) {
                Dm_Pr_Detail dmPrDetail = new Dm_Pr_Detail(this.FTSMain);
                dmPrDetail.CheckIdStruct = false;
                dmPrDetail.AllowCreateEmployee = true;
                foreach (DataRow row in ds.Tables[this.TableName].Rows) {
                    if (row.RowState != DataRowState.Deleted) {
                        dmPrDetail.LoadDataByID(row["DEPARTMENT_ID"]);
                        if (!dmPrDetail.IsValidRow(0)) {
                            DataRow newrow = dmPrDetail.AddNew();
                            newrow["PR_DETAIL_ID"] = row["DEPARTMENT_ID"];
                            newrow["PR_DETAIL_NAME"] = row["DEPARTMENT_NAME"];
                            newrow["ACTIVE"] = row["ACTIVE"];
                            newrow["PR_DETAIL_TYPE_ID"] = PrDetailType.DEPARTMENT;
                            object obj = this.FTSMain.DbMain.ExecuteScalar(
                                this.FTSMain.DbMain.GetSqlStringCommand(
                                    "SELECT TOP 1 PR_DETAIL_CLASS_ID FROM DM_PR_DETAIL_CLASS WHERE IS_DEPARTMENT=1 AND ACTIVE=1"));

                            if (obj != null && obj != System.DBNull.Value) {
                                newrow["PR_DETAIL_CLASS_ID"] = obj;
                            }

                            newrow.EndEdit();
                        } else {
                            DataRow foundrow = dmPrDetail.DataTable.Rows[0];
                            foundrow["PR_DETAIL_NAME"] = row["DEPARTMENT_NAME"];
                            foundrow["ACTIVE"] = row["ACTIVE"];
                            foundrow.EndEdit();
                        }
                    } else {
                        dmPrDetail.LoadDataByID(row["DEPARTMENT_ID", DataRowVersion.Original]);
                        if (dmPrDetail.IsValidRow(0)) {
                            dmPrDetail.DataTable.Rows[0].Delete();
                        }
                    }
                }

                dmPrDetail.UpdateData();
            }
            base.UpdateData();
        }
        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_DepartmentObject dmDepartmentObject = new Dm_DepartmentObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmDepartmentObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }
                return dmDepartmentObject;
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
                    Dm_DepartmentObject dmDepartmentObject = new Dm_DepartmentObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmDepartmentObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmDepartmentObject);
                }
            }
            return list;
        }
    }
}
