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
    public class ListDelivery : ObjectBase
    {
        public ListDelivery(FTSMain ftsmain) : base(ftsmain, "list_delivery")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public ListDelivery(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WARE_HOUSE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WARE_HOUSE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FROM_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NOTE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TOTAL_ORDER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WEIGHT", DbType.Decimal, false));

        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.LIST_DELIVERY;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                ListDeliveryObject listDeliveryObject = new ListDeliveryObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    listDeliveryObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return listDeliveryObject;
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
                    ListDeliveryObject listDeliveryObject = new ListDeliveryObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        listDeliveryObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(listDeliveryObject);
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

