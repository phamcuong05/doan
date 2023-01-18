using System;
using System.Data;
using FTS.Base.Business;

namespace  FTS.Base.Report
{
    public class FilterDmObject : ObjectInfoBase {
        public FilterDmObject() : base() {
            this.FILTER_TABLE_ID = string.Empty;
            this.FILTER_TABLE_NAME = string.Empty;
            this.ID_FIELD = string.Empty;
            this.NAME_FIELD = string.Empty;
        }

        public FilterDmObject(string idfield, string filtertableid, string filtertablename,string namefield) {
            this.FILTER_TABLE_ID = filtertableid;
            this.FILTER_TABLE_NAME = filtertablename;
            this.ID_FIELD = idfield;
            this.NAME_FIELD = namefield;
        }

        public string FILTER_TABLE_ID
        {
            get { return this.GetValueOrDefault<string>("FILTER_TABLE_ID"); }
            set { this.SetValue("FILTER_TABLE_ID", value); }
        }

        public string ID_FIELD
        {
            get { return this.GetValueOrDefault<string>("ID_FIELD"); }
            set { this.SetValue("ID_FIELD", value); }
        }
        public string FILTER_TABLE_NAME
        {
            get { return this.GetValueOrDefault<string>("FILTER_TABLE_NAME"); }
            set { this.SetValue("FILTER_TABLE_NAME", value); }
        }
        public string NAME_FIELD
        {
            get { return this.GetValueOrDefault<string>("NAME_FIELD"); }
            set { this.SetValue("NAME_FIELD", value); }
        }
    }
}
