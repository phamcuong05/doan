using FTS.Base.API;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.ShareBusiness.Model;
using System;
using System.Data;
using System.Net;
using System.Web.Http;

namespace FTS.ShareAPI.Controllers.Business
{
    [Authorize]
    public class Dm_AgentController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Agent(this.FTSMain);
        }


        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_AgentObject dmagentobject)
        {
            try
            {
                Dm_Agent dmAgent = new Dm_Agent(this.FTSMain);
                dmAgent.LoadDataByID(dmagentobject.AGENT_ID);
                if (dmAgent.IsValidRow(0))
                {
                    dmAgent.SyncObjectToTable(dmagentobject);
                    dmAgent.UpdateData();
                    return Ok(dmagentobject);
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
        public IHttpActionResult UpdateNewData(Dm_AgentObject dmagentobject)
        {
            try
            {
                Dm_Agent dmAgent = new Dm_Agent(this.FTSMain);
                dmAgent.LoadDataByID(dmagentobject.AGENT_ID);
                if (dmAgent.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmAgent.AddNew();
                    dmAgent.SyncObjectToTable(dmagentobject);
                    dmAgent.UpdateData();
                    return Ok(dmagentobject);
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
                Dm_Agent dmAgent = new Dm_Agent(this.FTSMain);
                dmAgent.DeleteInData(idvalue);
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
                Dm_Agent dmAgent = new Dm_Agent(this.FTSMain);
                dmAgent.LoadDataByID(idvalue);
                return Ok(dmAgent.GetDataObject());
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
                Dm_Agent dmAgent = new Dm_Agent(this.FTSMain);
                dmAgent.AddNew();
                return Ok(dmAgent.GetDataObject());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }

        }
    }
}
