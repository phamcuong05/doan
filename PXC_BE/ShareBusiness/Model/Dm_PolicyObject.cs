using FTS.Base.Business;
using System.Data;

namespace FTS.ShareBusiness.Model
{
    public class Dm_PolicyObject : ObjectInfoBase
    {
        public Dm_PolicyObject() : base()
        {
            this.POLICY_NO = string.Empty;
        }

        public Dm_PolicyObject(DataRow row) : base(row)
        {

        }

        public string POLICY_NO
        {
            get { return this.GetValueOrDefault<string>("POLICY_NO"); }
            set { this.SetValue("POLICY_NO", value); }
        }
    }
}
