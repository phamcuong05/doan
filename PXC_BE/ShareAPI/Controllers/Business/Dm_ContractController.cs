using FTS.Base.API;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{
    [Authorize]
    public class Dm_ContractController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Contract(this.FTSMain);
        }
    }
}