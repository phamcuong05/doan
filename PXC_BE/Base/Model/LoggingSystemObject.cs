using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class LoggingSystemObject : ObjectInfoBase
    {
        public LoggingSystemObject() : base()
        {
            this.PR_KEY = 0;
            this.LOG_DATE = DateTime.Now;
            this.MESSAGE = string.Empty;
            this.SOURCE = string.Empty;
            this.STACK_TRACE = string.Empty;
            this.TABLE_NAME = string.Empty;
            this.FIELD_NAME = string.Empty;
        }

        public LoggingSystemObject(DataRow row) : base(row)
        {
            this.PR_KEY = (Int64) row["PR_KEY"];
            this.MESSAGE = row["MESSAGE"].ToString();
            this.SOURCE = row["SOURCE"].ToString();
            this.LOG_DATE = (DateTime)row["LOG_DATE"];
            this.STACK_TRACE = row["STACK_TRACE"].ToString();
            this.STACK_TRACE = row["TABLE_NAME"].ToString();
            this.STACK_TRACE = row["FIELD_NAME"].ToString();
        }

        public Int64 PR_KEY
        {
            get { return (Int64) this.GetValue("PR_KEY"); }
            set { this.SetValue("PR_KEY", value); }
        }

        public string MESSAGE
        {
            get { return this.GetValue("MESSAGE").ToString(); }
            set { this.SetValue("MESSAGE", value); }
        }

        public string SOURCE
        {
            get { return this.GetValue("SOURCE").ToString(); }
            set { this.SetValue("SOURCE", value); }
        }

        public DateTime LOG_DATE
        {
            get { return (DateTime) this.GetValue("LOG_DATE"); }
            set { this.SetValue("LOG_DATE", value); }
        }

        public string STACK_TRACE
        {
            get { return this.GetValue("STACK_TRACE").ToString(); }
            set { this.SetValue("STACK_TRACE", value); }
        }

        public string TABLE_NAME
        {
            get { return this.GetValue("TABLE_NAME").ToString(); }
            set { this.SetValue("TABLE_NAME", value); }
        }

        public string FIELD_NAME
        {
            get { return this.GetValue("FIELD_NAME").ToString(); }
            set { this.SetValue("FIELD_NAME", value); }
        }
    }
}
