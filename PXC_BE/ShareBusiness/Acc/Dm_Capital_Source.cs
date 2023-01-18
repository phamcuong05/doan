using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Capital_Source : ObjectBase
    {
        public Dm_Capital_Source(FTSMain ftsmain) : base(ftsmain, "Dm_Capital_Source")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Capital_Source(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Capital_Source")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Capital_Source(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Capital_Source", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();

            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CAPITAL_SOURCE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CAPITAL_SOURCE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_CAPITAL_SOURCE;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Capital_SourceObject dmCapitalSourceObject = new Dm_Capital_SourceObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmCapitalSourceObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }
                return dmCapitalSourceObject;
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
                    Dm_Capital_SourceObject dmCapitalSourceObject = new Dm_Capital_SourceObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmCapitalSourceObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmCapitalSourceObject);
                }
            }
            return list;
        }
    }
}
