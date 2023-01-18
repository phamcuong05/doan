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
    public class Dm_ProvinceController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Province(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);        
                dmProvince.LoadDataByID(idvalue);
                return Ok(dmProvince.GetDataObject());             
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_ProvinceObject dmprovinceobject)
        {
            try
            {
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                return Ok(dmProvince.IsDataChanged(dmprovinceobject));
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
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                DataRow row = dmProvince.AddNew();
                return Ok(dmProvince.GetDataObject());
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
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                dmProvince.LoadDataByID(idvalue);
                if (dmProvince.IsValidRow(0))
                {
                    DataRow newrow = dmProvince.CopyRecord(0);
                    return Ok(dmProvince.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_ProvinceObject dmprovinceobject)
        {
            try
            {
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                dmProvince.LoadDataByID(dmprovinceobject.PROVINCE_ID);
                if (dmProvince.IsValidRow(0))
                {
                    dmProvince.SyncObjectToTable(dmprovinceobject);
                    dmProvince.UpdateData();
                    return Ok(dmprovinceobject);
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
        public IHttpActionResult UpdateNewData(Dm_ProvinceObject dmprovinceobject)
        {
            try
            {
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                dmProvince.LoadDataByID(dmprovinceobject.PROVINCE_ID);
                if (dmProvince.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmProvince.AddNew();
                    dmProvince.SyncObjectToTable(dmprovinceobject);
                    dmProvince.UpdateData();
                    return Ok(dmprovinceobject);
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
                Dm_Province dmProvince = new Dm_Province(this.FTSMain);
                dmProvince.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}