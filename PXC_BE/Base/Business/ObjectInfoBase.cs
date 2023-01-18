#region

using System;
using System.Collections;
using System.Data.Common;
using System.Data;
#endregion

namespace FTS.Base.Business{
    [Serializable]
    public class ObjectInfoBase {
        Hashtable mHashTable = new Hashtable();

        public ObjectInfoBase() {

        }
        public ObjectInfoBase(DataRow row) {
            foreach (DataColumn c in row.Table.Columns) {
                this.SetValue(c.ColumnName, row.Table.Rows[0][c.ColumnName]);
            }
        }
        public ObjectInfoBase Copy() {
            ObjectInfoBase objectInfo =  (ObjectInfoBase) this.MemberwiseClone();
            objectInfo.mHashTable = (Hashtable)this.mHashTable.Clone();
            return objectInfo;
        }

        public virtual void SetValue(string fieldname, object fieldvalue) {
            object foundRow = (object)this.mHashTable[fieldname.ToUpper()];
            if (foundRow == null) {
                this.mHashTable.Add(fieldname.ToUpper(), fieldvalue);
            } else {
                this.mHashTable[fieldname.ToUpper()] = fieldvalue;
            }
        }

        public virtual T GetValueOrDefault<T>(string fieldname)
        {
            T result = default(T);
            var obj = this.GetValue(fieldname);
            if (obj != null && obj != DBNull.Value)
            {
                result = (T)obj;
            }
            return result;
        }

        public virtual object GetValue(string fieldname) {
            return this.mHashTable[fieldname.ToUpper()];
        }

    }
}