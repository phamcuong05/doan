#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class PaymentMethod
    {
        public static string TM = "TM"; //TIEN_MAT
        public static string CK = "CK"; //CHUYEN_KHOAN
        public static string DT = "DT"; //DOI_TRU
        public static string LC = "DLCT"; //LC
        public static string KHAC = "KHAC"; //KHAC

        /// <summary>
        /// Danh sach phuong thuc thanh toan
        /// </summary>
        /// <param name="ftsmain"></param>
        /// <returns></returns>
        public static List<PaymentMethodObject> GetPaymentMethodList(FTSMain ftsmain)
        {
            List<PaymentMethodObject> list = new List<PaymentMethodObject>();
            list.Add(new PaymentMethodObject(TM, ftsmain.MsgManager.GetMessage("PAYMENT_METHOD_" + TM)));
            list.Add(new PaymentMethodObject(CK, ftsmain.MsgManager.GetMessage("PAYMENT_METHOD_" + CK)));
            list.Add(new PaymentMethodObject(DT, ftsmain.MsgManager.GetMessage("PAYMENT_METHOD_" + DT)));
            list.Add(new PaymentMethodObject(LC, ftsmain.MsgManager.GetMessage("PAYMENT_METHOD_" + LC)));
            list.Add(new PaymentMethodObject(KHAC, ftsmain.MsgManager.GetMessage("PAYMENT_METHOD_" + KHAC)));
            return list;
        }
    }
}