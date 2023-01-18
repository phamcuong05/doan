using FTS.Base.Business;
using System;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class DM_ExpenseObject : ObjectInfoBase
    {
        public DM_ExpenseObject() : base()
        {
            this.EXPENSE_ID = string.Empty;
            this.EXPENSE_NAME = string.Empty;
            this.EXPENSE_CLASS_ID = string.Empty;
            this.EXPENSE_CLASS_NAME = string.Empty;
            this.ACTIVE = 0;
            this.USER_ID = string.Empty;
        }

        public DM_ExpenseObject(DataRow row) : base(row)
        {

        }

        public string EXPENSE_ID
        {
            get { return this.GetValueOrDefault<string>("EXPENSE_ID"); }
            set { this.SetValue("EXPENSE_ID", value); }
        }

        public string EXPENSE_NAME
        {
            get { return this.GetValueOrDefault<string>("EXPENSE_NAME"); }
            set { this.SetValue("EXPENSE_NAME", value); }
        }

        public string EXPENSE_CLASS_ID
        {
            get { return this.GetValueOrDefault<string>("EXPENSE_CLASS_ID"); }
            set { this.SetValue("EXPENSE_CLASS_ID", value); }
        }

        public string EXPENSE_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("EXPENSE_CLASS_NAME"); }
            set { this.SetValue("EXPENSE_CLASS_NAME", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}
