#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.ShareBusiness.Common {
    [Serializable]
    public class Transaction_Approve_Log : ObjectBase {
        public object PrKey = Guid.Empty;

        public Transaction_Approve_Log(FTSMain ftsmain) : base(ftsmain, "TRANSACTION_APPROVE_LOG") {
            this.LoadFields();
        }


        public override void LoadFields() {
            this.FieldCollection = new List<FieldInfo>();
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "PR_KEY", DbType.Guid, true));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "FR_KEY", DbType.Guid, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPROVE_DATE", DbType.Date, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPROVE_HOUR", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPROVE_MINUTE", DbType.Int32, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "TRAN_ID", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPROVE_USER", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "APPROVE_ACTION", DbType.String, false));
            this.FieldCollection.Add(this.FTSMain.FieldManager.CreateFieldInfo(this.TableName, "COMMENTS", DbType.String, false));
        }

        public override DataRow AddNew() {
            DataRow row = base.AddNew();
            row["APPROVE_DATE"] = DateTime.Today.Date;
            row["APPROVE_HOUR"] = DateTime.Now.Hour;
            row["APPROVE_MINUTE"] = DateTime.Now.Minute;
            row["APPROVE_USER"] = this.FTSMain.UserInfo.UserID;
            row.EndEdit();
            return row;
        }

        public void Add(string tranid, object prkey, string status, string comments) {
            DataRow newrow = this.AddNew();
            newrow["TRAN_ID"] = tranid;
            newrow["FR_KEY"] = prkey;
            newrow["APPROVE_ACTION"] = status;
            newrow["COMMENTS"] = comments;
            this.UpdateData();
        }

        public override void LoadData() {
            string sql = "SELECT * FROM TRANSACTION_APPROVE_LOG WHERE FR_KEY='" + this.PrKey + "' ORDER BY APPROVE_DATE";
            DbCommand cmd = this.FTSMain.DbMain.GetSqlStringCommand(sql);
            base.LoadDataByCommand(cmd);
        }

        public override void SetRole() { }
    }
}