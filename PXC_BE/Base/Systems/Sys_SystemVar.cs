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
    public class Sys_SystemVar : ObjectBase
    {
        public Sys_SystemVar(FTSMain ftsmain) : base(ftsmain, "Sys_SystemVar")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Sys_SystemVar(FTSMain ftsmain, bool isempty) : base(ftsmain, "Sys_SystemVar")
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
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAR_NAME", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAR_VALUE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DESCRIPTION", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAR_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "VAR_GROUP", DbType.String, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Sys_SystemvarObject sysSystemVarObject = new Sys_SystemvarObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    sysSystemVarObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return sysSystemVarObject;
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
                    Sys_SystemvarObject sysSystemVarObject = new Sys_SystemvarObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        sysSystemVarObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(sysSystemVarObject);
                }
            }

            return list;
        }
    }
}