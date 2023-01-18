using FTS.Base.Business;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_DepartmentObject : ObjectInfoBase
    {
        public Dm_DepartmentObject() : base()
        {
            this.DEPARTMENT_ID = string.Empty;
            this.DEPARTMENT_NAME = string.Empty;
            this.ORGANIZATION_ID = string.Empty;
            this.ACTIVE = false;
            this.USER_ID = string.Empty;
        }

        public Dm_DepartmentObject(DataRow row) : base(row)
        {

        }

        public string DEPARTMENT_ID
        {
            get { return this.GetValueOrDefault<string>("DEPARTMENT_ID"); }
            set { this.SetValue("DEPARTMENT_ID", value); }
        }

        public string DEPARTMENT_NAME
        {
            get { return this.GetValueOrDefault<string>("DEPARTMENT_NAME"); }
            set { this.SetValue("DEPARTMENT_NAME", value); }
        }

        public string ORGANIZATION_ID
        {
            get { return this.GetValueOrDefault<string>("ORGANIZATION_ID"); }
            set { this.SetValue("ORGANIZATION_ID", value); }
        }


        public bool ACTIVE
        {
            get { return this.GetValueOrDefault<bool>("ACTIVE"); }
            set { this.SetValue("ACTIVE", value); }
        }

        public string USER_ID
        {
            get { return this.GetValueOrDefault<string>("USER_ID"); }
            set { this.SetValue("USER_ID", value); }
        }
    }
}
