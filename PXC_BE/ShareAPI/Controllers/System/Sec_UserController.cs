using System;
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
    public class Sec_UserController : ApiObjectBaseController
    {
        public override ObjectBase GetObjectBase()
        {
            return new Sec_User(this.FTSMain);
        }

        [HttpGet]
        public IHttpActionResult GetDataByID(string idvalue)
        {
            try {
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(idvalue);
                return Ok(secUser.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult IsDataChanged(Sec_UserObject secuserobject) {
            try {
                Sec_User secUser = new Sec_User(this.FTSMain);
                return Ok(secUser.IsDataChanged(secuserobject));
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult AddNewData() {
            try {
                Sec_User secUser = new Sec_User(this.FTSMain);
                DataRow row = secUser.AddNew();
                return Ok(secUser.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        [HttpGet]
        public IHttpActionResult CopyData(string idvalue) {
            try {
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(idvalue);
                if (secUser.IsValidRow(0)) {
                    DataRow newrow = secUser.CopyRecord(0);
                } else {
                    secUser.AddNew();
                }

                return Ok(secUser.GetDataObject());
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult UpdateEditData(Sec_UserObject secuserobject)
        {
            try
            {
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(secuserobject.USER_ID);
                if (secUser.IsValidRow(0))
                {
                    secUser.SyncObjectToTable(secuserobject);
                    secUser.UpdateData();
                    return Ok(secuserobject);
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
        public IHttpActionResult UpdateNewData(Sec_UserObject secuserobject)
        {
            try
            {
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(secuserobject.USER_ID);
                if (secUser.IsValidRow(0))
                {
                    return Content(HttpStatusCode.InternalServerError,
                        this.FTSMain.ExceptionManager.ProcessException(new FTSException("MSG_RECORD_ID_EXISTS")));

                }
                else
                {
                    secUser.AddNew();
                    secUser.SyncObjectToTable(secuserobject);
                    secUser.UpdateData();
                    return Ok(secuserobject);
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
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.DeleteInData(idvalue);
                return Ok(idvalue);
            } catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }
        }


        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePasswordObject changePasswordObject)
        {
            try
            {
                Sec_User secUser = new Sec_User(this.FTSMain);
                secUser.LoadDataByID(this.FTSMain.UserInfo.UserID);
                if(secUser.DataTable.Rows.Count == 0)
                {
                    throw new FTSException("MSG_USER_ID_INVALID");
                }
                if(secUser.DataTable.Rows[0]["USER_PASSWORD"].ToString() != FTS.Base.Utilities.FunctionsBase.Encrypt(changePasswordObject.oldPwd))
                {
                    throw new FTSException("MSG_OLD_PASSWORD_INVALID");
                }
                secUser.DataTable.Rows[0]["USER_PASSWORD"] = FTS.Base.Utilities.FunctionsBase.Encrypt(changePasswordObject.newPwd);
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