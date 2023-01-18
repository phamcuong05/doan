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
    public class ListServiceCharge : ObjectBase
    {
        public ListServiceCharge(FTSMain ftsmain) : base(ftsmain, "list_service_charge")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public ListServiceCharge(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SERVICE_CHARGE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SERVICE_CHARGE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DESCRIPTION", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SHIP_FEE", DbType.Currency, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CREATE_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "MODIFIED_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "REGION", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.LIST_SERVICE_CHARGE;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                ListServiceChargeObject list_service_chargeobject = new ListServiceChargeObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    list_service_chargeobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return list_service_chargeobject;
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
                    ListServiceChargeObject list_service_chargeobject = new ListServiceChargeObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        list_service_chargeobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(list_service_chargeobject);
                }
            }

            return list;
        }


    }
}
