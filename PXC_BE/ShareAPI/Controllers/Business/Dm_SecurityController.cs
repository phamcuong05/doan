using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    public class Dm_SecurityController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Security(this.FTSMain);
        }

        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_SecurityObject dmsecurityobject)
        {
            try
            {
                Dm_Security dmSecurity = new Dm_Security(this.FTSMain);
                dmSecurity.LoadDataByID(dmsecurityobject.SECURITY_ID);
                if (dmSecurity.IsValidRow(0))
                {
                    dmSecurity.SyncObjectToTable(dmsecurityobject);
                    dmSecurity.UpdateData();
                    return Ok(dmsecurityobject);
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
        public IHttpActionResult UpdateNewData(Dm_SecurityObject dmsecurityobject)
        {
            try
            {
                Dm_Security dmSecurity = new Dm_Security(this.FTSMain);
                dmSecurity.LoadDataByID(dmsecurityobject.SECURITY_ID);
                if (dmSecurity.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmSecurity.AddNew();
                    dmSecurity.SyncObjectToTable(dmsecurityobject);
                    dmSecurity.UpdateData();
                    return Ok(dmsecurityobject);
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
                Dm_Security dmSecurity = new Dm_Security(this.FTSMain);
                dmSecurity.DeleteInData(idvalue);
                return Ok(idvalue);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try
            {
                Dm_Security dmSecurity = new Dm_Security(this.FTSMain);
                dmSecurity.LoadDataByID(idvalue);
                return Ok(dmSecurity.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData()
        {
            try
            {
                Dm_Security dmSecurity = new Dm_Security(this.FTSMain);
                dmSecurity.AddNew();
                return Ok(dmSecurity.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}