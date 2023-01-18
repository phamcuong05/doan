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
    public class Dm_Vat_PurchaseController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Vat_Purchase(this.FTSMain);
        }

        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                dmVatPurchase.LoadDataByID(idvalue);
                return Ok(dmVatPurchase.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Vat_PurchaseObject dmvatpurchaseobject)
        {
            try
            {
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                return Ok(dmVatPurchase.IsDataChanged(dmvatpurchaseobject));
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
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                DataRow row = dmVatPurchase.AddNew();
                return Ok(dmVatPurchase.GetDataObject());
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
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                dmVatPurchase.LoadDataByID(idvalue);
                if (dmVatPurchase.IsValidRow(0))
                {
                    DataRow newrow = dmVatPurchase.CopyRecord(0);
                    return Ok(dmVatPurchase.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Vat_PurchaseObject dmvatpurchaseobject)
        {
            try
            {
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                dmVatPurchase.LoadDataByID(dmvatpurchaseobject.VAT_PURCHASE_ID);
                if (dmVatPurchase.IsValidRow(0))
                {
                    dmVatPurchase.SyncObjectToTable(dmvatpurchaseobject);
                    dmVatPurchase.UpdateData();
                    return Ok(dmvatpurchaseobject);
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
        public IHttpActionResult UpdateNewData(Dm_Vat_PurchaseObject dmvatpurchaseobject)
        {
            try
            {
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                dmVatPurchase.LoadDataByID(dmvatpurchaseobject.VAT_PURCHASE_ID);
                if (dmVatPurchase.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmVatPurchase.AddNew();
                    dmVatPurchase.SyncObjectToTable(dmvatpurchaseobject);
                    dmVatPurchase.UpdateData();
                    return Ok(dmvatpurchaseobject);
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
                Dm_Vat_Purchase dmVatPurchase = new Dm_Vat_Purchase(this.FTSMain);
                dmVatPurchase.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }


    }
}
