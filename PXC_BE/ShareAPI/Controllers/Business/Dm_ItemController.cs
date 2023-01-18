using System;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using System.Data;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_ItemController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Item(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                dmItem.LoadDataByID(idvalue);
                return Ok(dmItem.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_ItemObject dmitemobject)
        {
            try
            {
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                return Ok(dmItem.IsDataChanged(dmitemobject));
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
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                DataRow row = dmItem.AddNew();
                return Ok(dmItem.GetDataObject());
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
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                dmItem.LoadDataByID(idvalue);
                if (dmItem.IsValidRow(0))
                {
                    DataRow newrow = dmItem.CopyRecord(0);
                    return Ok(dmItem.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_ItemObject dmitemobject)
        {
            try
            {
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                dmItem.LoadDataByID(dmitemobject.ITEM_ID);
                if (dmItem.IsValidRow(0))
                {
                    dmItem.SyncObjectToTable(dmitemobject);
                    dmItem.UpdateData();
                    return Ok(dmitemobject);
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
        public IHttpActionResult UpdateNewData(Dm_ItemObject dmitemobject)
        {
            try
            {
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                dmItem.LoadDataByID(dmitemobject.ITEM_ID);
                if (dmItem.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmItem.AddNew();
                    dmItem.SyncObjectToTable(dmitemobject);
                    dmItem.UpdateData();
                    return Ok(dmitemobject);
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
                Dm_Item dmItem = new Dm_Item(this.FTSMain);
                dmItem.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }


    }
}