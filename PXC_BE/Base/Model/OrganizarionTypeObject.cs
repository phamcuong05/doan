using System;
using System.Data;
using FTS.Base.Business;

namespace FTS.Base.Model
{
    public class OrganizarionTypeObject : ObjectInfoBase
    {
        public OrganizarionTypeObject(string OrganizarionTypeId, string OrganizarionTypeName)
        {
            this.ORGANIZATION_TYPE_ID = OrganizarionTypeId;
            this.ORGANIZATION_TYPE_NAME = OrganizarionTypeName;
        }

        public string ORGANIZATION_TYPE_ID { get; set; }

        public string ORGANIZATION_TYPE_NAME { get; set; }
    }
}
