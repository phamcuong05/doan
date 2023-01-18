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

namespace FTS.ShareAPI.Controllers.Business
{
    [Authorize]
    public class ListDeliveryController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListDelivery(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                listDelivery.LoadDataByID(idvalue);
                return Ok(listDelivery.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListDeliveryObject listDeliveryObject)
        {
            try
            {
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                return Ok(listDelivery.IsDataChanged(listDeliveryObject));
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
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                DataRow row = listDelivery.AddNew();
                return Ok(listDelivery.GetDataObject());
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
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                listDelivery.LoadDataByID(idvalue);
                if (listDelivery.IsValidRow(0))
                {
                    DataRow newrow = listDelivery.CopyRecord(0);
                    return Ok(listDelivery.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListDeliveryObject listDeliveryObject)
        {
            try
            {
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                listDelivery.LoadDataByID(listDeliveryObject.WARE_HOUSE_ID);
                if (listDelivery.IsValidRow(0))
                {
                    listDelivery.SyncObjectToTable(listDeliveryObject);
                    listDelivery.UpdateData();
                    return Ok(listDeliveryObject);
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
        public IHttpActionResult UpdateNewData(ListDeliveryObject listDeliveryObject)
        {
            try
            {
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                listDelivery.LoadDataByID(listDeliveryObject.WARE_HOUSE_ID);
                if (listDelivery.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listDelivery.AddNew();
                    listDelivery.SyncObjectToTable(listDeliveryObject);
                    listDelivery.UpdateData();
                    return Ok(listDeliveryObject);
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
                ListDelivery listDelivery = new ListDelivery(this.FTSMain);
                listDelivery.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}