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
    public class Dm_TemplateController : ApiManagerBaseController
    {

        public override ObjectBase GetManagerListing()
        {
            return null;
        }

        public override void PrintRecord(Guid prkeyoutput, Guid prkey, string pdfile, string excelfile, string wordfile)
        {
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(decimal idvalue)
        {
            try
            {
                Dm_TemplateManager dmTemplateManager = new Dm_TemplateManager(this.FTSMain, "TEMPLATE");
                dmTemplateManager.LoadRecordWithPrkey(idvalue);
                if (dmTemplateManager.mDmTemplate.IsValidRow(0))
                {
                    dmTemplateManager.TranId = dmTemplateManager.mDmTemplate.DataTable.Rows[0]["TRAN_ID"].ToString();
                    ManagerObjectInfoBase managerObjectInfoBase = dmTemplateManager.GetDataObject();
                    dmTemplateManager.Dispose();
                    return Ok(managerObjectInfoBase);
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
        public IHttpActionResult UpdateData(Dm_TemplateManagerObject dmtemplatemanagerobject)
        {
            try
            {
                if (dmtemplatemanagerobject.dmTemplate != null)
                {
                    Dm_TemplateManager dmTemplateManager = new Dm_TemplateManager(this.FTSMain, dmtemplatemanagerobject.dmTemplate.TRAN_ID);
                    dmTemplateManager.LoadRecordWithPrkey(dmtemplatemanagerobject.dmTemplate.PR_KEY);
                    dmTemplateManager.SyncObjectToTable(dmtemplatemanagerobject);
                    dmTemplateManager.UpdateData();
                    ManagerObjectInfoBase managerObjectInfoBase = dmTemplateManager.GetDataObject();
                    dmTemplateManager.Dispose();
                    return Ok(managerObjectInfoBase);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_INVALID_PARAMETER")));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }


        [HttpDelete]
        public IHttpActionResult Delete(decimal prKey)
        {
            try
            {
                Dm_TemplateManager dmTemplateManager = new Dm_TemplateManager(this.FTSMain, "TEMPLATE");
                dmTemplateManager.LoadRecordWithPrkey(prKey);
                if (dmTemplateManager.mDmTemplate.IsValidRow(0))
                {
                    dmTemplateManager.TranId = dmTemplateManager.mDmTemplate.DataTable.Rows[0]["TRAN_ID"].ToString();
                    dmTemplateManager.DeleteData();
                    return Ok(prKey);
                }
                else
                {
                    return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_NOT_EXISTS")));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
    }
}
