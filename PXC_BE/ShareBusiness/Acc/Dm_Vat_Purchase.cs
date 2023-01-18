using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Vat_Purchase : ObjectBase
    {
        public bool AllowCreateEmployee = false;

        public Dm_Vat_Purchase(FTSMain ftsmain) : base(ftsmain, "dm_vat_purchase")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Vat_Purchase(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_PURCHASE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAT_PURCHASE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_VAT_PURCHASE;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Vat_PurchaseObject dm_vat_purchaseobject = new Dm_Vat_PurchaseObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dm_vat_purchaseobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dm_vat_purchaseobject;
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
                    Dm_Vat_PurchaseObject dm_vat_purchaseobject = new Dm_Vat_PurchaseObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dm_vat_purchaseobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dm_vat_purchaseobject);
                }
            }

            return list;
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }
    }
}
