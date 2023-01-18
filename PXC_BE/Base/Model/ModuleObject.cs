#region

using FTS.Base.Business;

#endregion

namespace FTS.Base.Model
{
    public class ModuleObject : ObjectInfoBase
    {
        public ModuleObject() : base()
        {
            this.MODULE_ID = string.Empty;
            this.MODULE_NAME = string.Empty;
        }

        public ModuleObject(string moduleid, string modulename)
        {
            this.MODULE_ID = moduleid;
            this.MODULE_NAME = modulename;
        }

        public string MODULE_ID
        {
            get { return this.GetValueOrDefault<string>("MODULE_ID"); }
            set { this.SetValue("MODULE_ID", value); }
        }

        public string MODULE_NAME
        {
            get { return this.GetValueOrDefault<string>("MODULE_NAME"); }
            set { this.SetValue("MODULE_NAME", value); }
        }
    }
}