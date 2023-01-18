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
    public class Sys_MenuController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Sys_Menu(this.FTSMain);
        }


        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Sys_Menu sysMenu = new Sys_Menu(this.FTSMain);
                sysMenu.AddNew();
                return Ok(sysMenu.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Sys_MenuObject sysMenuobject)
        {
            try
            {
                Sys_Menu sysMenu = new Sys_Menu(this.FTSMain);
                sysMenu.LoadDataByID(sysMenuobject.MENU_ID);
                if (sysMenu.IsValidRow(0))
                {
                    sysMenu.SyncObjectToTable(sysMenuobject);
                    sysMenu.UpdateData();
                    return Ok(sysMenuobject);
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
        public IHttpActionResult UpdateNewData(Sys_MenuObject sysMenuobject)
        {
            try
            {
                Sys_Menu sysMenu = new Sys_Menu(this.FTSMain);
                sysMenu.LoadDataByID(sysMenuobject.MENU_ID);
                if (sysMenu.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    sysMenu.AddNew();
                    sysMenu.SyncObjectToTable(sysMenuobject);
                    sysMenu.UpdateData();
                    return Ok(sysMenuobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteData(Guid idvalue)
        {
            try
            {
                Sys_Menu sysMenu = new Sys_Menu(this.FTSMain);
                sysMenu.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}