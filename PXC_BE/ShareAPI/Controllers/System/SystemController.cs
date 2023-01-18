using System;
using System.Collections.Generic;
using System.Web.Http;
using FTS.Base.API;
using System.Net;
using FTS.Base.Model;
using FTS.Base.Security;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class SystemController : ApiBaseController
    {
        /// <summary>
        /// Lấy các project trong hệ thống
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProjectList()
        {
            try
            {
                List<ProjectObject> voucherStatusList = ProjectList.GetProjectList(this.FTSMain);
                return Ok(voucherStatusList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }

        /// <summary>
        /// Lấy các module trong hệ thống
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetModuleList()
        {
            try
            {
                List<ModuleObject> voucherStatusList = ModuleList.GetModuleList(this.FTSMain);
                return Ok(voucherStatusList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}