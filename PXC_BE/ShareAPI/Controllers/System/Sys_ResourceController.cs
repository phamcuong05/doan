using System;
using System.Net;
using System.Web.Http;
using FTS.Base.Systems;
using FTS.Base.API;
using FTS.Base.Model;
using FTS.Base.Business;
using System.Data;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Sys_ResourceController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Sys_Resource(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Sys_Resource sysResource = new Sys_Resource(this.FTSMain);
                DataRow row = sysResource.AddNew();
                return Ok(sysResource.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Sys_ResourceObject sysResourceObject)
        {
            try
            {
                Sys_Resource sysResource = new Sys_Resource(this.FTSMain);
                sysResource.LoadDataByID(sysResourceObject.RES_ID);
                if (sysResource.IsValidRow(0))
                {
                    sysResource.SyncObjectToTable(sysResourceObject);
                    sysResource.UpdateData();
                    return Ok(sysResourceObject);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_NOT_EXISTS")));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Sys_Resource sysResource = new Sys_Resource(this.FTSMain);
                sysResource.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}