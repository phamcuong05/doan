#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Advance_Limit : ObjectBase {
        public Dm_Advance_Limit(FTSMain ftsmain) : base(ftsmain, "Dm_Advance_Limit") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Advance_Limit(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Advance_Limit") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Advance_Limit(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Advance_Limit", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACCOUNT_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VALID_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ADVANCE_LIMIT", DbType.Currency, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_ADVANCE_LIMIT;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Advance_LimitObject dmAdvanceLimitObject = new Dm_Advance_LimitObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmAdvanceLimitObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmAdvanceLimitObject;
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
                    Dm_Advance_LimitObject dmAdvanceLimitObject = new Dm_Advance_LimitObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmAdvanceLimitObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmAdvanceLimitObject);
                }
            }

            return list;
        }
    }
}