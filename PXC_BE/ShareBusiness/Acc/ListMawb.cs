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
    public class ListMawb : ObjectBase
    {
        public ListMawb(FTSMain ftsmain) : base(ftsmain, "list_mawb")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public ListMawb(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "MAWB_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "MAWB_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DEPARTURE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DESTINATION", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CREATE_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TOTAL_ORDER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "WEIGHT", DbType.Decimal, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.LIST_MAWB;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                ListMawbObject listMawbObject = new ListMawbObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    listMawbObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return listMawbObject;
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
                    ListMawbObject listMawbObject = new ListMawbObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        listMawbObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(listMawbObject);
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
