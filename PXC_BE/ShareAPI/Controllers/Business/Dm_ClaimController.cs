using FTS.Base.API;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_ClaimController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Claim(this.FTSMain);
        }
    }
}