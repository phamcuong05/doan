using FTS.Base.Business;
using FTS.Base.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.Base.Systems
{
    public class Dm_Template : ObjectBase
    {
        private Dm_TemplateManager mDmTemplateManager;

        public Dm_Template(FTSMain esoftmain, DataSet dataset, Dm_TemplateManager dmtemplatemanager) : base(esoftmain, dataset, "Dm_Template", true)
        {
            this.mDmTemplateManager = dmtemplatemanager;
            this.LoadFields();
        }

        public Dm_Template(FTSMain ftsmain) : base(ftsmain, "Dm_Template")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Template(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Template")
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
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Decimal, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TEMPLATE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TABLE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID_NAME", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_FIRST_ROW_DATA", DbType.Int16, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Int16, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_TemplateObject dmTemplateObject = new Dm_TemplateObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmTemplateObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmTemplateObject;
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
                    Dm_TemplateObject dmTemplateObject = new Dm_TemplateObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmTemplateObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmTemplateObject);
                }
            }

            return list;
        }
    }
}
