#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Currency : ObjectBase {
        public Dm_Currency(FTSMain ftsmain) : base(ftsmain, "Dm_Currency") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Currency(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Currency") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Currency(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Currency", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CURRENCY_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CURRENCY_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_CURRENCY;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_CurrencyObject dmCurrencyObject = new Dm_CurrencyObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmCurrencyObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmCurrencyObject;
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
                    Dm_CurrencyObject dmCurrencyObject = new Dm_CurrencyObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmCurrencyObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmCurrencyObject);
                }
            }

            return list;
        }


    }
}