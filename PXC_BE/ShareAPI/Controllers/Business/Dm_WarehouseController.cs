using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_WarehouseController : ApiObjectBaseController
    {

        public override ObjectBase GetObjectBase()
        {
            return new Dm_Warehouse(this.FTSMain);
        }

        // GET api/<controller>/5

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                dmWarehouse.LoadDataByID(idvalue);
                return Ok(dmWarehouse.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_WarehouseObject dmwarehouseobject)
        {
            try
            {
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                return Ok(dmWarehouse.IsDataChanged(dmwarehouseobject));
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
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                DataRow row = dmWarehouse.AddNew();
                return Ok(dmWarehouse.GetDataObject());
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
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                dmWarehouse.LoadDataByID(idvalue);
                if (dmWarehouse.IsValidRow(0))
                {
                    DataRow newrow = dmWarehouse.CopyRecord(0);
                    return Ok(dmWarehouse.GetDataObject());
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
        public IHttpActionResult UpdateEditData(Dm_WarehouseObject dmwarehouseobject)
        {
            try
            {
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                dmWarehouse.LoadDataByID(dmwarehouseobject.WAREHOUSE_ID);
                if (dmWarehouse.IsValidRow(0))
                {
                    dmWarehouse.SyncObjectToTable(dmwarehouseobject);
                    dmWarehouse.UpdateData();
                    return Ok(dmwarehouseobject);
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
        public IHttpActionResult UpdateNewData(Dm_WarehouseObject dmwarehouseobject)
        {
            try
            {
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                dmWarehouse.LoadDataByID(dmwarehouseobject.WAREHOUSE_ID);
                if (dmWarehouse.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmWarehouse.AddNew();
                    dmWarehouse.SyncObjectToTable(dmwarehouseobject);
                    dmWarehouse.UpdateData();
                    return Ok(dmwarehouseobject);
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
                Dm_Warehouse dmWarehouse = new Dm_Warehouse(this.FTSMain);
                dmWarehouse.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        
    }
}