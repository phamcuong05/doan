using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Claim : ObjectBase
    {
        public Dm_Claim(FTSMain ftsmain) : base(ftsmain, "Dm_Claim")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Claim(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Claim")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Claim(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Claim", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CLAIM_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_DATE", DbType.DateTime, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "STATUS", DbType.String, false));
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_ClaimObject dmClaimObject = new Dm_ClaimObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmClaimObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmClaimObject;
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
                    Dm_ClaimObject dmClaimObject = new Dm_ClaimObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmClaimObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmClaimObject);
                }
            }

            return list;
        }
    }
}
