using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTS.Base.Report {
    [Serializable] public class FieldSelectionObject {
        public string TableName = string.Empty;
        public string FieldName = string.Empty;
        public string FieldValue = string.Empty;
        public string FieldDisplayValue = string.Empty;
        public FieldSelectionObject() {}

        public FieldSelectionObject(string tablename, string fieldname, string fieldvalue, string fielddisplayvalue) {
            this.TableName = tablename;
            this.FieldName = fieldname;
            this.FieldValue = fieldvalue;
            this.FieldDisplayValue = fielddisplayvalue;
        }
    }
}