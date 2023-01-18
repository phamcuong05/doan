using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_JobController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Job(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                dmJob.LoadDataByID(idvalue);
                return Ok(dmJob.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_JobObject dmjobobject)
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                return Ok(dmJob.IsDataChanged(dmjobobject));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                DataRow row = dmJob.AddNew();
                return Ok(dmJob.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult CopyData(string idvalue)
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                dmJob.LoadDataByID(idvalue);
                if (dmJob.IsValidRow(0))
                {
                    DataRow newrow = dmJob.CopyRecord(0);
                    return Ok(dmJob.GetDataObject());
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

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_JobObject dmjobobject)
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                dmJob.LoadDataByID(dmjobobject.JOB_ID);
                if (dmJob.IsValidRow(0))
                {
                    dmJob.SyncObjectToTable(dmjobobject);
                    dmJob.UpdateData();
                    return Ok(dmjobobject);
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

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_JobObject dmjobobject)
        {
            try
            {
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                dmJob.LoadDataByID(dmjobobject.JOB_ID);
                if (dmJob.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmJob.AddNew();
                    dmJob.SyncObjectToTable(dmjobobject);
                    dmJob.UpdateData();
                    return Ok(dmjobobject);
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
                Dm_Job dmJob = new Dm_Job(this.FTSMain);
                dmJob.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}