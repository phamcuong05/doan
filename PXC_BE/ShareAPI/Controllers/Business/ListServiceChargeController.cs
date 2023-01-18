using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{
    [Authorize]
    public class ListServiceChargeController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListServiceCharge(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                listServiceCharge.LoadDataByID(idvalue);
                return Ok(listServiceCharge.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListServiceChargeObject listservicechargeobject)
        {
            try
            {
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                return Ok(listServiceCharge.IsDataChanged(listservicechargeobject));
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
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                DataRow row = listServiceCharge.AddNew();
                return Ok(listServiceCharge.GetDataObject());
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
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                listServiceCharge.LoadDataByID(idvalue);
                if (listServiceCharge.IsValidRow(0))
                {
                    DataRow newrow = listServiceCharge.CopyRecord(0);
                    
                }
                else
                {
                    listServiceCharge.AddNew();
                }
                return Ok(listServiceCharge.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(ListServiceChargeObject listservicechargeobject)
        {
            try
            {
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                listServiceCharge.LoadDataByID(listservicechargeobject.SERVICE_CHARGE_ID);
                if (listServiceCharge.IsValidRow(0))
                {
                    listServiceCharge.SyncObjectToTable(listservicechargeobject);
                    listServiceCharge.UpdateData();
                    return Ok(listservicechargeobject);
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
        public IHttpActionResult UpdateNewData(ListServiceChargeObject listservicechargeobject)
        {
            try
            {
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                listServiceCharge.LoadDataByID(listservicechargeobject.SERVICE_CHARGE_ID);
                if (listServiceCharge.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listServiceCharge.AddNew();
                    listServiceCharge.SyncObjectToTable(listservicechargeobject);
                    listServiceCharge.UpdateData();
                    return Ok(listservicechargeobject);
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
                ListServiceCharge listServiceCharge = new ListServiceCharge(this.FTSMain);
                listServiceCharge.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}