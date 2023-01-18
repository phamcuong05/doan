using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Http;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.Base.API;
using FTS.Base.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class Dm_OrganizarionController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Dm_Organization(this.FTSMain);
        }


        [HttpGet]
        public IHttpActionResult GetOrganizationTypeList()
        {
            try
            {
                List<OrganizarionTypeObject> organizationTypeList = OrganizationType.GetOrganizationTypeList(this.FTSMain);
                return Ok(organizationTypeList);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Dm_Organization dmOrganization = new Dm_Organization(this.FTSMain);
                dmOrganization.LoadDataByID(idvalue);
                return Ok(dmOrganization.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Dm_OrganizarionObject dmorganizationobject) {
            try {
                Dm_Organization dmOrganization = new Dm_Organization(this.FTSMain);
                return Ok(dmOrganization.IsDataChanged(dmorganizationobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Dm_Organization dmOrganizarion = new Dm_Organization(this.FTSMain);
                DataRow row = dmOrganizarion.AddNew();
                return Ok(dmOrganizarion.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Dm_Organization dmOrganizarion = new Dm_Organization(this.FTSMain);
                dmOrganizarion.LoadDataByID(idvalue);
                if (dmOrganizarion.IsValidRow(0)) {
                    DataRow newrow = dmOrganizarion.CopyRecord(0);
                } else {
                    dmOrganizarion.AddNew();
                }

                return Ok(dmOrganizarion.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateNewData(Dm_OrganizarionObject dmOrganizarionObject)
        {
            try
            {
                Dm_Organization dmOrganization = new Dm_Organization(this.FTSMain);
                dmOrganization.LoadDataByID(dmOrganizarionObject.ORGANIZATION_ID);
                if (dmOrganization.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));
                }
                else
                {
                    dmOrganization.AddNew();
                    dmOrganization.SyncObjectToTable(dmOrganizarionObject);
                    dmOrganization.UpdateData();
                    return Ok(dmOrganizarionObject);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, this.FTSMain.ExceptionManager.ProcessException(ex));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Dm_OrganizarionObject dmcurrencyobject)
        {
            try
            {
                Dm_Organization dmOrganization = new Dm_Organization(this.FTSMain);
                dmOrganization.LoadDataByID(dmcurrencyobject.ORGANIZATION_ID);
                if (dmOrganization.IsValidRow(0))
                {
                    dmOrganization.SyncObjectToTable(dmcurrencyobject);
                    dmOrganization.UpdateData();
                    return Ok(dmcurrencyobject);
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



        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteData(string idvalue)
        {
            try
            {
                Dm_Organization dmOrganizarion = new Dm_Organization(this.FTSMain);
                dmOrganizarion.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }


        [HttpPost]
        public IHttpActionResult ChangeOrganizarion(Dm_OrganizarionObject dmOrganizarionObject)
        {
            try
            {              

                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(this.FTSMain.UserInfo.UserID);
                if (secUser.DataTable.Rows.Count == 0)
                {
                    throw new FTSException("MSG_USER_ID_INVALID");
                }
                secUser.DataTable.Rows[0]["ORGANIZATION_ID"] = dmOrganizarionObject.ORGANIZATION_ID;
                secUser.UpdateData();

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }
    }
}