#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Unit : ObjectBase {
        public Dm_Unit(FTSMain ftsmain) : base(ftsmain, "Dm_Unit") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Unit(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Unit") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Unit(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Unit", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "UNIT_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "UNIT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_UNIT;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_UnitObject dmUnitObject = new Dm_UnitObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmUnitObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmUnitObject;
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
                    Dm_UnitObject dmUnitObject = new Dm_UnitObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmUnitObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmUnitObject);
                }
            }

            return list;
        }
    }
}