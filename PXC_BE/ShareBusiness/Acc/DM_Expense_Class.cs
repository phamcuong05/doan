using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;
using System.Collections.Generic;
using System.Data;

namespace FTS.ShareBusiness.Acc
{
    public class Dm_Expense_Class : ObjectBase
    {
        public Dm_Expense_Class(FTSMain ftsmain) : base(ftsmain, "Dm_Expense_Class")
        {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Expense_Class(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Expense_Class")
        {
            if (!isempty)
            {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Expense_Class(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Expense_Class", false)
        {
            this.LoadFields();
        }

        public override void LoadFields()
        {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_CLASS_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EXPENSE_CLASS_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_EXPENSE_CLASS;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                DM_Expense_ClassObject dmExpenseClassObject = new DM_Expense_ClassObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmExpenseClassObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }
                return dmExpenseClassObject;
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
                    DM_Expense_ClassObject dmExpenseClassObject = new DM_Expense_ClassObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmExpenseClassObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmExpenseClassObject);
                }
            }
            return list;
        }
    }
}
