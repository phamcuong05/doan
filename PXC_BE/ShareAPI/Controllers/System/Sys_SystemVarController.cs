using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Systems;
using System;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{
    [Authorize]
    public class Sys_SystemVarController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Sys_SystemVar(this.FTSMain);
        }


        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Sys_SystemVar sysTran = new Sys_SystemVar(this.FTSMain);
                sysTran.AddNew();
                return Ok(sysTran.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Sys_SystemvarObject syssystemvarobject)
        {
            try
            {
                Sys_SystemVar sysTran = new Sys_SystemVar(this.FTSMain);
                sysTran.LoadDataByID(syssystemvarobject.VAR_NAME);
                if (sysTran.IsValidRow(0))
                {
                    sysTran.SyncObjectToTable(syssystemvarobject);
                    sysTran.UpdateData();
                    return Ok(syssystemvarobject);
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

        [HttpPost]
        public IHttpActionResult UpdateNewData(Sys_SystemvarObject syssystemvarobject)
        {
            try
            {
                Sys_SystemVar sysTran = new Sys_SystemVar(this.FTSMain);
                sysTran.LoadDataByID(syssystemvarobject.VAR_NAME);
                if (sysTran.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    sysTran.AddNew();
                    sysTran.SyncObjectToTable(syssystemvarobject);
                    sysTran.UpdateData();
                    return Ok(syssystemvarobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(Guid idvalue)
        {
            try
            {
                Sys_SystemVar sysTran = new Sys_SystemVar(this.FTSMain);
                sysTran.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}