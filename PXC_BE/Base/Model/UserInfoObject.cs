using System;
using System.Data;

namespace FTS.Base.Model
{
    public class UserInfoObject
    {
        public UserInfoObject()
        {
        }

        public UserInfoObject(int wokingyear, DataRow row)
        {
            this.UserID = row["USER_ID"].ToString();
            this.UserName = row["USER_NAME"].ToString();
            this.UserGroupID = row["USER_GROUP_ID"].ToString();
            this.EmployeeId = row["EMPLOYEE_ID"].ToString();
            this.OrganizationID = row["ORGANIZATION_ID"].ToString();
            this.OrganizationName = row["ORGANIZATION_NAME"].ToString();
            this.WorkingYear = wokingyear;
        }

        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserGroupID { get; set; } = string.Empty;
        public string ClientIP { get; set; } = string.Empty;
        public string ClientMachineName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string OrganizationID { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string ParentOrganizationID { get; set; } = string.Empty;
        public string ParentOrganizationName { get; set; } = string.Empty;
        public string ModuleList { get; set; } = string.Empty;
        public string POSShiftKey { get; set; } = string.Empty;
        public string HTShiftKey { get; set; } = string.Empty;
        public string RegisterID { get; set; } = string.Empty;
        public bool IsSubOrg { get; set; } = false;
        public int WorkingYear { get; set; } = DateTime.Now.Year;
    }
}
