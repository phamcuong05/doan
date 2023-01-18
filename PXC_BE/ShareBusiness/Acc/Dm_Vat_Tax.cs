#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Vat_Tax : ObjectBase {
        public Dm_Vat_Tax(FTSMain ftsmain) : base(ftsmain, "Dm_Vat_Tax") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Vat_Tax(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Vat_Tax") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Vat_Tax(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Vat_Tax", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_TAX_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_TAX_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_TAX_RATE", DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_VAT_TAX;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Vat_TaxObject dmVatTaxObject = new Dm_Vat_TaxObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmVatTaxObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmVatTaxObject;
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
                    Dm_Vat_TaxObject dmVatTaxObject = new Dm_Vat_TaxObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmVatTaxObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmVatTaxObject);
                }
            }

            return list;
        }
    }
}