using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Item_OpController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Item_Op(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetIssueReceiptList()
        {
            try
            {
                List<IssueReceiptObject> issueReceiptList = IssueReceipt.GetIssueReceiptList(this.FTSMain);
                return Ok(issueReceiptList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                dmItemOp.LoadDataByID(idvalue);
                return Ok(dmItemOp.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Item_OpObject dmitemopobject)
        {
            try
            {
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                return Ok(dmItemOp.IsDataChanged(dmitemopobject));
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
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                DataRow row = dmItemOp.AddNew();
                return Ok(dmItemOp.GetDataObject());
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
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                dmItemOp.LoadDataByID(idvalue);
                if (dmItemOp.IsValidRow(0))
                {
                    DataRow newrow = dmItemOp.CopyRecord(0);
                    return Ok(dmItemOp.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_Item_OpObject dmitemopobject)
        {
            try
            {
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                dmItemOp.LoadDataByID(dmitemopobject.ITEM_OP_ID);
                if (dmItemOp.IsValidRow(0))
                {
                    dmItemOp.SyncObjectToTable(dmitemopobject);
                    dmItemOp.UpdateData();
                    return Ok(dmitemopobject);
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
        public IHttpActionResult UpdateNewData(Dm_Item_OpObject dmitemopobject)
        {
            try
            {
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                dmItemOp.LoadDataByID(dmitemopobject.ITEM_OP_ID);
                if (dmItemOp.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmItemOp.AddNew();
                    dmItemOp.SyncObjectToTable(dmitemopobject);
                    dmItemOp.UpdateData();
                    return Ok(dmitemopobject);
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
                Dm_Item_Op dmItemOp = new Dm_Item_Op(this.FTSMain);
                dmItemOp.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }        
    }
}