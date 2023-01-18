using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.ShareBusiness.Model
{
    public class Dm_Pr_Detail_ClassObject : ObjectInfoBase
    {
        public Dm_Pr_Detail_ClassObject() : base()
        {
            this.PR_DETAIL_CLASS_ID = string.Empty;
            this.PR_DETAIL_CLASS_NAME = string.Empty;
            this.PR_DETAIL_TYPE_ID = string.Empty;
            this.PR_DETAIL_TYPE_NAME = string.Empty;
            this.ACTIVE = 1;
            this.IS_EMPLOYEE = 0;
            this.IS_AGENT = 0;
            this.IS_DEPARTMENT = 0;
            this.USER_ID = "ADMIN";
        }

        public Dm_Pr_Detail_ClassObject(DataRow row) : base(row)
        {

        }

        public string PR_DETAIL_CLASS_ID
        {
            get { return this.GetValue("PR_DETAIL_CLASS_ID").ToString(); }
            set { this.SetValue("PR_DETAIL_CLASS_ID", value); }
        }

        public string PR_DETAIL_CLASS_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_CLASS_NAME"); }
            set { this.SetValue("PR_DETAIL_CLASS_NAME", value); }
        }

        public string PR_DETAIL_TYPE_ID
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_TYPE_ID"); }
            set { this.SetValue("PR_DETAIL_TYPE_ID", value); }
        }
        public string PR_DETAIL_TYPE_NAME
        {
            get { return this.GetValueOrDefault<string>("PR_DETAIL_TYPE_NAME"); }
            set { this.SetValue("PR_DETAIL_TYPE_NAME", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("User_ID"); }
            set { this.SetValue("User_ID", value); }
        }

        public Int16 ACTIVE
        {
            get { return this.GetValueOrDefault<Int16>("Active"); }
            set { this.SetValue("Active", value); }
        }

        public Int16 IS_EMPLOYEE
        {
            get { return this.GetValueOrDefault<Int16>("IS_EMPLOYEE"); }
            set { this.SetValue("IS_EMPLOYEE", value); }
        }

        public Int16 IS_AGENT
        {
            get { return this.GetValueOrDefault<Int16>("IS_AGENT"); }
            set { this.SetValue("IS_AGENT", value); }
        }

        public Int16 IS_DEPARTMENT
        {
            get { return this.GetValueOrDefault<Int16>("IS_DEPARTMENT"); }
            set { this.SetValue("IS_DEPARTMENT", value); }
        }
    }
}
