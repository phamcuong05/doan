using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Policy : ObjectBase
    {
        public Dm_Policy(FTSMain ftsmain) : base(ftsmain, "Dm_Policy")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Policy(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Policy")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Policy(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Policy", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "POLICY_NO", DbType.String, true));
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_PolicyObject dmPolicyObject = new Dm_PolicyObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmPolicyObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmPolicyObject;
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
                    Dm_PolicyObject dmPolicyObject = new Dm_PolicyObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmPolicyObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmPolicyObject);
                }
            }

            return list;
        }
    }
}
