#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Contract_Status : ObjectBase {
        public Dm_Contract_Status(FTSMain ftsmain) : base(ftsmain, "Dm_Contract_Status") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Contract_Status(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Contract_Status") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Contract_Status(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Contract_Status", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_STATUS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_STATUS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_CONTRACT_STATUS;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Contract_StatusObject dmContractStatusObject = new Dm_Contract_StatusObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmContractStatusObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmContractStatusObject;
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
                    Dm_Contract_StatusObject dmContractStatusObject = new Dm_Contract_StatusObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmContractStatusObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmContractStatusObject);
                }
            }

            return list;
        }
    }
}