#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Job : ObjectBase {
        public Dm_Job(FTSMain ftsmain) : base(ftsmain, "Dm_Job") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Job(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Job") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Job(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Job", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "JOB_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "JOB_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_JOB;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_JobObject dmJobObject = new Dm_JobObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dmJobObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dmJobObject;
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
                    Dm_JobObject dmJobObject = new Dm_JobObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dmJobObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dmJobObject);
                }
            }

            return list;
        }
    }
}