using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Systems;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.Base.Business;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_Pr_Detail_ClassController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Pr_Detail_Class(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetailClass.LoadDataByID(idvalue);
                return Ok(dmPrDetailClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_Pr_Detail_ClassObject dmprdetailobject)
        {
            try
            {
                Dm_Pr_Detail_Class dmPrDetail = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetail.LoadDataByID(dmprdetailobject.PR_DETAIL_CLASS_ID);
                if (dmPrDetail.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    dmPrDetail.AddNew();
                    dmPrDetail.SyncObjectToTable(dmprdetailobject);
                    dmPrDetail.UpdateData();
                    return Ok(dmprdetailobject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }
        [HttpGet]
        public IHttpActionResult GetPrDetailTypeList()
        {
            try
            {
                List<Dm_Pr_Class_TypeObject> organizationTypeList = PrDetailType.GetPrDetailTypeList(this.FTSMain);
                return Ok(organizationTypeList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }
        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_Pr_Detail_ClassObject dmprdetailclassobject)
        {
            try
            {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetailClass.LoadDataByID(dmprdetailclassobject.PR_DETAIL_CLASS_ID);
                if (dmPrDetailClass.IsValidRow(0))
                {
                    dmPrDetailClass.SyncObjectToTable(dmprdetailclassobject);
                    dmPrDetailClass.UpdateData();
                    return Ok(dmprdetailclassobject);
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



        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_Pr_DetailObject dmprdetailclassobject) {
            try {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                return Ok(dmPrDetailClass.IsDataChanged(dmprdetailclassobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                DataRow row = dmPrDetailClass.AddNew();
                return Ok(dmPrDetailClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetailClass.LoadDataByID(idvalue);
                if (dmPrDetailClass.IsValidRow(0)) {
                    DataRow newrow = dmPrDetailClass.CopyRecord(0);
                } else {
                    dmPrDetailClass.AddNew();
                }

                return Ok(dmPrDetailClass.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateData(Dm_Pr_Detail_ClassObject dmprdetailclassobject) {
            try {
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetailClass.LoadDataByID(dmprdetailclassobject.PR_DETAIL_CLASS_ID);
                dmPrDetailClass.SyncObjectToTable(dmprdetailclassobject);
                dmPrDetailClass.UpdateData();
                return Ok(dmprdetailclassobject);
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
                Dm_Pr_Detail_Class dmPrDetailClass = new Dm_Pr_Detail_Class(this.FTSMain);
                dmPrDetailClass.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}