#region

using System.Collections.Generic;
using System.Data;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public class Dm_Province : ObjectBase {
        public Dm_Province(FTSMain ftsmain) : base(ftsmain, "Dm_Province") {
            this.Condittion = string.Empty;
            this.LoadFields();
        }

        public Dm_Province(FTSMain ftsmain, bool isempty) : base(ftsmain, "Dm_Province") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Dm_Province(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "Dm_Province", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PROVINCE_ID", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PROVINCE_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_ID", DbType.String, false));
        }

        public override void SetRole()
        {
            this.FTSFunction = FTS.Base.Security.FTSFunctionCollection.DM_PROVINCE;
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Dm_ProvinceObject DmProvinceObject = new Dm_ProvinceObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    DmProvinceObject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return DmProvinceObject;
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
                    Dm_ProvinceObject DmProvinceObject = new Dm_ProvinceObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        DmProvinceObject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(DmProvinceObject);
                }
            }

            return list;
        }
    }
}