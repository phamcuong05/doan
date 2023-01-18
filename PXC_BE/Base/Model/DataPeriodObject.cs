using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model {
    public class DataPeriodObject : ObjectInfoBase {
        public string FieldName = string.Empty;

        public DataPeriodObject() : base() {
        }

        /// <summary>
        /// daystart : ngay bat dau 
        /// </summary>
        public DateTime daystart = DateTime.Now.Date;

        /// <summary>
        /// dayend : ngay ket thuc
        /// </summary>
        public DateTime dayend = DateTime.Now.Date;

    }
}