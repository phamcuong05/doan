using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    [Authorize]
    public class Dm_Capital_SourceController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Capital_Source(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Capital_SourceObject dmcapitalsourceobject)
        {
            try
            {
                Dm_Capital_Source dmCapitalSource = new Dm_Capital_Source(this.FTSMain);
                dmCapitalSource.LoadDataByID(dmcapitalsourceobject.CAPITAL_SOURCE_ID);
                if (dmCapitalSource.IsValidRow(0))
                {
                    dmCapitalSource.SyncObjectToTable(dmcapitalsourceobject);
                    dmCapitalSource.UpdateData();
                    return Ok(dmcapitalsourceobject);
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
        public IHttpActionResult UpdateNewData(Dm_Capital_SourceObject dmcapitalsourceobject)
        {
            try
            {
                Dm_Capital_Source dmCapitalSource = new Dm_Capital_Source(this.FTSMain);
                dmCapitalSource.LoadDataByID(dmcapitalsourceobject.CAPITAL_SOURCE_ID);
                if (dmCapitalSource.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmCapitalSource.AddNew();
                    dmCapitalSource.SyncObjectToTable(dmcapitalsourceobject);
                    dmCapitalSource.UpdateData();
                    return Ok(dmcapitalsourceobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Capital_Source dmCapitalSource = new Dm_Capital_Source(this.FTSMain);
                dmCapitalSource.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Capital_Source dmCapitalSource = new Dm_Capital_Source(this.FTSMain);
                dmCapitalSource.LoadDataByID(idvalue);
                return Ok(dmCapitalSource.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Capital_Source dmCapitalSource = new Dm_Capital_Source(this.FTSMain);
                dmCapitalSource.AddNew();
                return Ok(dmCapitalSource.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}
