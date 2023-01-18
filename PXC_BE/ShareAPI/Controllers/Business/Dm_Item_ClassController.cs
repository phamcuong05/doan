using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using Newtonsoft.Json;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.Base.Business;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Item_ClassController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Item_Class(this.FTSMain);
        }

  

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.LoadDataByID(idvalue);
                return Ok(dmItemClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Item_ClassObject dmItemclassobject) {
            try {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                return Ok(dmItemClass.IsDataChanged(dmItemclassobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                DataRow row = dmItemClass.AddNew();
                return Ok(dmItemClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.LoadDataByID(idvalue);
                if (dmItemClass.IsValidRow(0)) {
                    DataRow newrow = dmItemClass.CopyRecord(0);
                } else {
                    dmItemClass.AddNew();
                }

                return Ok(dmItemClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Item_ClassObject dmitemclassobject)
        {
            try
            {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.LoadDataByID(dmitemclassobject.ITEM_CLASS_ID);
                if (dmItemClass.IsValidRow(0))
                {
                    dmItemClass.SyncObjectToTable(dmitemclassobject);
                    dmItemClass.UpdateData();
                    return Ok(dmitemclassobject);
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
        public IHttpActionResult UpdateData(Dm_Item_ClassObject dmitemclassobject) {
            try {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.LoadDataByID(dmitemclassobject.ITEM_CLASS_ID);
                dmItemClass.SyncObjectToTable(dmitemclassobject);
                dmItemClass.UpdateData();
                return Ok(dmitemclassobject);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Item_ClassObject dmitemclassobject)
        {
            try
            {
                Dm_Item_Class dmItemClass = new Dm_Item_Class(this.FTSMain);
                dmItemClass.LoadDataByID(dmitemclassobject.ITEM_CLASS_ID);
                if (dmItemClass.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmItemClass.AddNew();
                    dmItemClass.SyncObjectToTable(dmitemclassobject);
                    dmItemClass.UpdateData();
                    return Ok(dmitemclassobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}