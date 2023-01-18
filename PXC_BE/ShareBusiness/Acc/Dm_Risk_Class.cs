#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Risk_Class : ObjectBase {
        public Dm_Risk_Class(FTSMain ftsmain) : base(ftsmain, "Dm_Risk_Class") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Risk_Class(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Risk_Class") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Risk_Class(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Risk_Class", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RISK_CLASS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RISK_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RISK_CLASS_CATEGORY", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole() {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_RISK_CLASS;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Risk_ClassObject Dm_Risk_Classobject = new Dm_Risk_ClassObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    Dm_Risk_Classobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return Dm_Risk_Classobject;
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
                    Dm_Risk_ClassObject dmRiskClassObject = new Dm_Risk_ClassObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmRiskClassObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmRiskClassObject);
                }
            }

            return list;
        }


    }
}