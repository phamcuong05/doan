using FTS.Base.Business;
using FTS.Base.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.Base.Systems
{
    public class Dm_Template_Detail : ObjectBase
    {
        private Dm_TemplateManager mDmTemplateManager;

        public Dm_Template_Detail(FTSMain esoftmain, DataSet dataset, Dm_TemplateManager dmtemplatemanager) : base(esoftmain, dataset, "Dm_Template_Detail", true)
        {
            this.mDmTemplateManager = dmtemplatemanager;
            this.LoadFields();
        }

        public Dm_Template_Detail(FTSMain ftsmain) : base(ftsmain, "Dm_Template_Detail")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Template_Detail(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Template_Detail")
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
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Decimal, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LIST_ORDER", DbType.Int16, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXCEL_COLUMN_NO", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DATA_COLUMN_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "DATA_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "IS_PR_KEY", DbType.Int16, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_Template_DetailObject dmTemplateDetailObject = new Dm_Template_DetailObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmTemplateDetailObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmTemplateDetailObject;
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
                    Dm_Template_DetailObject dmTemplateDetailObject = new Dm_Template_DetailObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmTemplateDetailObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmTemplateDetailObject);
                }
            }

            return list;
        }
    }
}
