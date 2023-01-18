#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Estimate_Type : ObjectBase {
        public Dm_Estimate_Type(FTSMain ftsmain) : base(ftsmain, "Dm_Estimate_Type") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Estimate_Type(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Estimate_Type") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Estimate_Type(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Estimate_Type", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ESTIMATE_TYPE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ESTIMATE_TYPE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ESTIMATE_TYPE;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Estimate_TypeObject dmBankObject = new Dm_Estimate_TypeObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmBankObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmBankObject;
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
                    Dm_Estimate_TypeObject dmBankObject = new Dm_Estimate_TypeObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmBankObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmBankObject);
                }
            }

            return list;
        }
    }
}