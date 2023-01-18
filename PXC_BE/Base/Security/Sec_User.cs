#region

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.Model;

#endregion

namespace FTS.Base.Security {
    public class Sec_User : ObjectBase {
        public Sec_User(FTSMain ftsmain) : base(ftsmain, "SEC_USER") {
            this.LoadData();
            this.LoadFields();
        }

        public Sec_User(FTSMain ftsmain, bool isempty) : base(ftsmain, "SEC_USER") {
            if (!isempty) {
                this.LoadData();
            }

            this.LoadFields();
        }

        public Sec_User(FTSMain ftsmain, DataSet ds) : base(ftsmain, ds, "SEC_USER", false) {
            this.LoadFields();
        }

        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_Id", DbType.String, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_GROUP_Id", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_NAME", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_PASSWORD", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "EMPLOYEE_Id", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ORGANIZATION_Id", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "ACTIVE", DbType.Boolean, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "USER_KEY", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "LOGIN_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "QUANTITY_INVALId", DbType.Int32, false));
        }

        public override void CheckBusinessRules()
        {
            base.CheckBusinessRules();
            foreach(DataRow dr in this.DataTable.Rows)
            {
                if(dr.RowState != DataRowState.Deleted)
                {
                    dr["USER_PASSWORD"] = FTS.Base.Utilities.FunctionsBase.Encrypt(dr["USER_PASSWORD"].ToString());
                }
            }
        }

        public override void LoadData()
        {
            base.LoadData();

            foreach(DataRow dr in this.DataTable.Rows)
            {
                dr["USER_PASSWORD"] = FTS.Base.Utilities.FunctionsBase.Decrypt(dr["USER_PASSWORD"].ToString());
            }
        }


        public override ObjectInfoBase GetDataObject()
        {
            if (this.IsValidRow(0))
            {
                Sec_UserObject secUserobject = new Sec_UserObject();
                foreach (DataColumn c in this.DataTable.Columns)
                {
                    secUserobject.SetValue(c.ColumnName, this.DataTable.Rows[0][c.ColumnName]);
                }

                return secUserobject;
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
                    Sec_UserObject secUserobject = new Sec_UserObject();
                    foreach (DataColumn c in this.DataTable.Columns)
                    {
                        secUserobject.SetValue(c.ColumnName, row[c.ColumnName]);
                    }
                    list.Add(secUserobject);
                }
            }

            return list;
        }
    }
}