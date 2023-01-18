using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Security_Class : ObjectBase
    {
        public Dm_Security_Class(FTSMain ftsmain) : base(ftsmain, "Dm_Security_Class")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Security_Class(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Security_Class")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Security_Class(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Security_Class", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_CLASS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "SECURITY_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_SECURITY_CLASS;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Security_ClassObject dmSecurityClassObject = new Dm_Security_ClassObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmSecurityClassObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmSecurityClassObject;
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
                    Dm_Security_ClassObject dmSecurityClassObject = new Dm_Security_ClassObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmSecurityClassObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmSecurityClassObject);
                }
            }

            return list;
        }
    }
}
