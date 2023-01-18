using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Systems;
using System;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers
{
    [Authorize]
    public class Sys_TableController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Sys_Table(this.FTSMain);
        }


        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Sys_Table sysTable = new Sys_Table(this.FTSMain);
                sysTable.AddNew();
                return Ok(sysTable.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Sys_TableObject sysTableobject)
        {
            try
            {
                Sys_Table sysTable = new Sys_Table(this.FTSMain);
                sysTable.LoadDataByID(sysTableobject.TABLE_NAME);
                if (sysTable.IsValidRow(0))
                {
                    sysTable.SyncObjectToTable(sysTableobject);
                    sysTable.UpdateData();
                    return Ok(sysTableobject);
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

        [HttpPost]
        public IHttpActionResult UpdateNewData(Sys_TableObject sysTableobject)
        {
            try
            {
                Sys_Table sysTable = new Sys_Table(this.FTSMain);
                sysTable.LoadDataByID(sysTableobject.TABLE_NAME);
                if (sysTable.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    sysTable.AddNew();
                    sysTable.SyncObjectToTable(sysTableobject);
                    sysTable.UpdateData();
                    return Ok(sysTableobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Sys_Table sysTable = new Sys_Table(this.FTSMain);
                sysTable.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}