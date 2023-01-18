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
    public class ListOrder : ObjectBase
    {
        public ListOrder(FTSMain ftsmain) : base(ftsmain, "list_order")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public ListOrder(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORDER_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PACKAGE_CODE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PACKAGE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "BUY_FEE", DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHIP_FEE", DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TOTAL", DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORDER_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CUSTOMER_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PHONE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ADDRESS", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SERVICE_CHARGE_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SERVICE_CHARGE_NAME", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.LIST_ORDER;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                ListOrderObject listOrderObject = new ListOrderObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    listOrderObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return listOrderObject;
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
                    ListOrderObject listOrderObject = new ListOrderObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        listOrderObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(listOrderObject);
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
