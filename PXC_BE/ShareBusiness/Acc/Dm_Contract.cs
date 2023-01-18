using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Contract : ObjectBase
    {
        public Dm_Contract(FTSMain ftsmain) : base(ftsmain, "Dm_Contract")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Contract(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Contract")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Contract(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Contract", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "CONTRACT_CLASS_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "STATUS", DbType.String, false));
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_ContractObject dmContractObject = new Dm_ContractObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmContractObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmContractObject;
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
                    Dm_ContractObject dmContractObject = new Dm_ContractObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmContractObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmContractObject);
                }
            }

            return list;
        }
    }
}
