using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Security_Type : ObjectBase
    {
        public Dm_Security_Type(FTSMain ftsmain) : base(ftsmain, "Dm_Security_Type")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Security_Type(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Security_Type")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Security_Type(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Security_Type", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_TYPE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_TYPE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_SECURITY_TYPE;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Security_TypeObject dmSecurityTypeObject = new Dm_Security_TypeObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmSecurityTypeObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmSecurityTypeObject;
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
                    Dm_Security_TypeObject dmSecurityTypeObject = new Dm_Security_TypeObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmSecurityTypeObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmSecurityTypeObject);
                }
            }

            return list;
        }
    }
}
