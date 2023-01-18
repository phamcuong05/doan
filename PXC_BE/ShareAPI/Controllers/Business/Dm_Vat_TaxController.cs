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
    public class Dm_Vat_TaxController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Vat_Tax(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Vat_Tax secUserGroup = new Dm_Vat_Tax(this.FTSMain);
                secUserGroup.LoadDataByID(idvalue);
                return Ok(secUserGroup.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Vat_TaxObject dmvattaxobject) {
            try {
                Dm_Vat_Tax secUserGroup = new Dm_Vat_Tax(this.FTSMain);
                return Ok(secUserGroup.IsDataChanged(dmvattaxobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Vat_Tax secUserGroup = new Dm_Vat_Tax(this.FTSMain);
                DataRow row = secUserGroup.AddNew();
                return Ok(secUserGroup.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Vat_Tax dmVatTaxObject = new Dm_Vat_Tax(this.FTSMain);
                dmVatTaxObject.LoadDataByID(idvalue);
                if (dmVatTaxObject.IsValidRow(0)) {
                    DataRow newrow = dmVatTaxObject.CopyRecord(0);
                } else {
                    dmVatTaxObject.AddNew();
                }

                return Ok(dmVatTaxObject.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Vat_TaxObject dmvattaxobject)
        {
            try
            {
                Dm_Vat_Tax dmVatTax = new Dm_Vat_Tax(this.FTSMain);
                dmVatTax.LoadDataByID(dmvattaxobject.VAT_TAX_ID);
                if (dmVatTax.IsValidRow(0))
                {
                    dmVatTax.SyncObjectToTable(dmvattaxobject);
                    dmVatTax.UpdateData();
                    return Ok(dmvattaxobject);
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
        public IHttpActionResult UpdateNewData(Dm_Vat_TaxObject dmvattaxobject)
        {
            try
            {
                Dm_Vat_Tax dmVatTax = new Dm_Vat_Tax(this.FTSMain);
                dmVatTax.LoadDataByID(dmvattaxobject.VAT_TAX_ID);
                if (dmVatTax.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmVatTax.AddNew();
                    dmVatTax.SyncObjectToTable(dmvattaxobject);
                    dmVatTax.UpdateData();
                    return Ok(dmvattaxobject);
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
                Dm_Vat_Tax dmVatTax = new Dm_Vat_Tax(this.FTSMain);
                dmVatTax.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}