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
    public class Sys_Table : ObjectBase
    {
        public Sys_Table(FTSMain ftsmain) : base(ftsmain, "Sys_Table")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Sys_Table(FTSMain ftsmain, bool isempty) : base(ftsmain, "Sys_Table")
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
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TABLE_NAME", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_FIELD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "NAME_FIELD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TABLE_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CAN_GROUP", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_AUTO", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_MASK", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_LENGTH", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_PARTS", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ID_SPLIT", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LOAD_BY_SEARCH", DbType.Boolean, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Sys_TableObject sysTableObject = new Sys_TableObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    sysTableObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return sysTableObject;
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
                    Sys_TableObject sysTableObject = new Sys_TableObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        sysTableObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(sysTableObject);
                }
            }

            return list;
        }
    }
}