#region

using System;
using System.Collections;
using System.Data.Common;
using System.Data;
#endregion

namespace FTS.Base.Business{
    [Serializable]
    public class ManagerObjectInfoBase {
        Hashtable mHashTable = new Hashtable();

        public ManagerObjectInfoBase() {

        }
        public ManagerObjectInfoBase(DataRow row) {
            foreach (DataColumn c in row.Table.Columns) {
                this.SetValue(c.ColumnName, row.Table.Rows[0][c.ColumnName]);
            }
        }
        public ManagerObjectInfoBase Copy() {
            return (ManagerObjectInfoBase) this.MemberwiseClone();
        }

        public virtual void SetValue(string fieldname, object fieldvalue) {
            object foundRow = (object)this.mHashTable[fieldname.ToUpper()];
            if (foundRow == null) {
                this.mHashTable.Add(fieldname.ToUpper(), fieldvalue);
            } else {
                this.mHashTable[fieldname.ToUpper()] = fieldvalue;
            }
        }

        public virtual object GetValue(string fieldname) {
            return this.mHashTable[fieldname.ToUpper()];
        }

    }
}