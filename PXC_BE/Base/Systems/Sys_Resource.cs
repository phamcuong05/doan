#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Systems
{
    public class Sys_Resource : ObjectBase
    {
        public Sys_Resource(FTSMain ftsmain) : base(ftsmain, "Sys_Resource")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Sys_Resource(FTSMain ftsmain, bool isempty) : base(ftsmain, "Sys_Resource")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }


        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RES_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "RES_VALUE", DbType.String, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override void UpdateData() {
            base.UpdateData();
            this.FTSMain.ResourceManager.ReLoadData();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Sys_ResourceObject sysResourceObject = new Sys_ResourceObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    sysResourceObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return sysResourceObject;
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
                    Sys_ResourceObject sysResourceObject = new Sys_ResourceObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        sysResourceObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(sysResourceObject);
                }
            }

            return list;
        }
    }
}