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
    public class ListOrderController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new ListOrder(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                ListOrder listOrder = new ListOrder(this.FTSMain);
                listOrder.LoadDataByID(idvalue);
                return Ok(listOrder.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(ListOrderObject listOrderObject)
        {
            try
            {
                ListOrder listOrder = new ListOrder(this.FTSMain);
                return Ok(listOrder.IsDataChanged(listOrderObject));
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
                ListOrder listOrder = new ListOrder(this.FTSMain);
                DataRow row = listOrder.AddNew();
                return Ok(listOrder.GetDataObject());
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
                ListOrder listOrder = new ListOrder(this.FTSMain);
                listOrder.LoadDataByID(idvalue);
                if (listOrder.IsValidRow(0))
                {
                    DataRow newrow = listOrder.CopyRecord(0);
                    return Ok(listOrder.GetDataObject());
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
        public IHttpActionResult UpdateEditData(ListOrderObject listOrderObject)
        {
            try
            {
                ListOrder listOrder = new ListOrder(this.FTSMain);
                listOrder.LoadDataByID(listOrderObject.ORDER_ID);
                if (listOrder.IsValidRow(0))
                {
                    listOrder.SyncObjectToTable(listOrderObject);
                    listOrder.UpdateData();
                    return Ok(listOrderObject);
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
        public IHttpActionResult UpdateNewData(ListOrderObject listOrderObject)
        {
            try
            {
                ListOrder listOrder = new ListOrder(this.FTSMain);
                listOrder.LoadDataByID(listOrderObject.ORDER_ID);
                if (listOrder.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    listOrder.AddNew();
                    listOrder.SyncObjectToTable(listOrderObject);
                    listOrder.UpdateData();
                    return Ok(listOrderObject);
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
                ListOrder listOrder = new ListOrder(this.FTSMain);
                listOrder.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}