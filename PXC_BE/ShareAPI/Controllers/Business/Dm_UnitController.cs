using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_UnitController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Unit(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                dmUnit.LoadDataByID(idvalue);
                return Ok(dmUnit.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_UnitObject dmunitobject)
        {
            try
            {
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                return Ok(dmUnit.IsDataChanged(dmunitobject));
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
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                DataRow row = dmUnit.AddNew();
                return Ok(dmUnit.GetDataObject());
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
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                dmUnit.LoadDataByID(idvalue);
                if (dmUnit.IsValidRow(0))
                {
                    DataRow newrow = dmUnit.CopyRecord(0);
                    return Ok(dmUnit.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_UnitObject dmunitobject)
        {
            try
            {
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                dmUnit.LoadDataByID(dmunitobject.UNIT_ID);
                if (dmUnit.IsValidRow(0))
                {
                    dmUnit.SyncObjectToTable(dmunitobject);
                    dmUnit.UpdateData();
                    return Ok(dmunitobject);
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
        public IHttpActionResult UpdateNewData(Dm_UnitObject dmunitobject)
        {
            try
            {
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                dmUnit.LoadDataByID(dmunitobject.UNIT_ID);
                if (dmUnit.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmUnit.AddNew();
                    dmUnit.SyncObjectToTable(dmunitobject);
                    dmUnit.UpdateData();
                    return Ok(dmunitobject);
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
                Dm_Unit dmUnit = new Dm_Unit(this.FTSMain);
                dmUnit.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
        
    }
}