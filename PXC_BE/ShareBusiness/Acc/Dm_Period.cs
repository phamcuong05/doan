#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Model.Paging;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Period : ObjectBase {
        public Dm_Period(FTSMain ftsmain) : base(ftsmain, "Dm_Period") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Period(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Period") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Period(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Period", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_NUMBER", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PERIOD_TYPE", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole() {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_PERIOD;
        }

        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_PeriodObject dm_pr_detailobject = new Dm_PeriodObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    dm_pr_detailobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return dm_pr_detailobject;
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
                    Dm_PeriodObject dm_pr_detailobject = new Dm_PeriodObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        dm_pr_detailobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(dm_pr_detailobject);
                }
            }
            return list;
        }
    }
}