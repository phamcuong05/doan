#region

using System.Collections.Generic;
using FTS.Base.Business;

#endregion

namespace FTS.Base.Model
{
    public class ProjectObject : ObjectInfoBase
    {
        public ProjectObject() : base()
        {
            this.PROJECT_ID = string.Empty;
            this.PROJECT_NAME = string.Empty;
        }

        public ProjectObject(string projectid, string projectname)
        {
            this.PROJECT_ID = projectid;
            this.PROJECT_NAME = projectname;
        }

        public string PROJECT_ID
        {
            get { return this.GetValueOrDefault<string>("PROJECT_ID"); }
            set { this.SetValue("PROJECT_ID", value); }
        }

        public string PROJECT_NAME
        {
            get { return this.GetValueOrDefault<string>("PROJECT_NAME"); }
            set { this.SetValue("PROJECT_NAME", value); }
        }
    }
}