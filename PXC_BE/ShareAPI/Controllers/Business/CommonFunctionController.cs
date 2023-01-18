using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using FTS.ShareBusiness.Acc;
using FTS.Base.API;
using FTS.ShareBusiness.Model;

namespace FTS.ShareAPI.Controllers
{

    [Authorize]
    public class CommonFunctionController : ApiBaseController
    {

        /// <summary>
        /// List balance type: deb, crd, debcrd
        /// </summary>
        /// <param name="isdebcrd"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetBalanceTypeList(bool isdebitcredit)
        {
            try
            {
                List<DebitCreditObject> balanceType = DebitCredit.GetDebitCreditList(this.FTSMain, isdebitcredit);
                return Ok(balanceType);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }


        [HttpGet]
        public IHttpActionResult GetRateMethodList()
        {
            try
            {
                List<RateMethodObject> balanceType = RateMethodType.GetRateMethodList(this.FTSMain);
                return Ok(balanceType);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }

        /// <summary>
        /// GetPaymentMethodList - cac phuong thuc thanh toan
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetPaymentMethodList()
        {
            try
            {
                List<PaymentMethodObject> paymentMethod = PaymentMethod.GetPaymentMethodList(this.FTSMain);
                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, (this.FTSMain).ExceptionManager.ProcessException(ex));
            }

        }
    }
}