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
    public class Dm_DistrictController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_District(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_District dmDistrict = new Dm_District(this.FTSMain);            
                dmDistrict.LoadDataByID(idvalue);
                return Ok(dmDistrict.GetDataObject());              
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_DistrictObject dmdistrictobject)
        {
            try
            {
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                return Ok(dmDistrict.IsDataChanged(dmdistrictobject));
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
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                DataRow row = dmDistrict.AddNew();
                return Ok(dmDistrict.GetDataObject());
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
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                dmDistrict.LoadDataByID(idvalue);
                if (dmDistrict.IsValidRow(0))
                {
                    DataRow newrow = dmDistrict.CopyRecord(0);
                    return Ok(dmDistrict.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_DistrictObject dmdistrictobject)
        {
            try
            {
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                dmDistrict.LoadDataByID(dmdistrictobject.DISTRICT_ID);
                if (dmDistrict.IsValidRow(0))
                {
                    dmDistrict.SyncObjectToTable(dmdistrictobject);
                    dmDistrict.UpdateData();
                    return Ok(dmdistrictobject);
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
        public IHttpActionResult UpdateNewData(Dm_DistrictObject dmdistrictobject)
        {
            try
            {
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                dmDistrict.LoadDataByID(dmdistrictobject.DISTRICT_ID);
                if (dmDistrict.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmDistrict.AddNew();
                    dmDistrict.SyncObjectToTable(dmdistrictobject);
                    dmDistrict.UpdateData();
                    return Ok(dmdistrictobject);
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
                Dm_District dmDistrict = new Dm_District(this.FTSMain);
                dmDistrict.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        
    }
}