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
    public class Dm_Estimate_TypeController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Estimate_Type(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                dmEstimateType.LoadDataByID(idvalue);
                return Ok(dmEstimateType.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Estimate_TypeObject dmestimatetypeobject)
        {
            try
            {
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                return Ok(dmEstimateType.IsDataChanged(dmestimatetypeobject));
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
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                DataRow row = dmEstimateType.AddNew();
                return Ok(dmEstimateType.GetDataObject());
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
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                dmEstimateType.LoadDataByID(idvalue);
                if (dmEstimateType.IsValidRow(0))
                {
                    DataRow newrow = dmEstimateType.CopyRecord(0);
                    return Ok(dmEstimateType.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Estimate_TypeObject dmestimatetypeobject)
        {
            try
            {
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                dmEstimateType.LoadDataByID(dmestimatetypeobject.ESTIMATE_TYPE_ID);
                if (dmEstimateType.IsValidRow(0))
                {
                    dmEstimateType.SyncObjectToTable(dmestimatetypeobject);
                    dmEstimateType.UpdateData();
                    return Ok(dmestimatetypeobject);
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
        public IHttpActionResult UpdateNewData(Dm_Estimate_TypeObject dmestimatetypeobject)
        {
            try
            {
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                dmEstimateType.LoadDataByID(dmestimatetypeobject.ESTIMATE_TYPE_ID);
                if (dmEstimateType.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmEstimateType.AddNew();
                    dmEstimateType.SyncObjectToTable(dmestimatetypeobject);
                    dmEstimateType.UpdateData();
                    return Ok(dmestimatetypeobject);
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
                Dm_Estimate_Type dmEstimateType = new Dm_Estimate_Type(this.FTSMain);
                dmEstimateType.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}