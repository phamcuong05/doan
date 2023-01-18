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
    public class ListContainer : ObjectBase
    {
        public ListContainer(FTSMain ftsmain) : base(ftsmain, "list_container")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public ListContainer(FTSMain ftsmain, DataSet ds, string tablename) : base(ftsmain, ds, tablename, false)
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTAINER_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTAINER_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTAINER_WEIGHT", DbType.Currency, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.LIST_CONTAINER;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                ListContainerObject listContainerObject = new ListContainerObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    listContainerObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return listContainerObject;
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
                    ListContainerObject listContainerObject = new ListContainerObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        listContainerObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(listContainerObject);
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
