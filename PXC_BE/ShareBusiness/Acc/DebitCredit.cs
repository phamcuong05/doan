#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class DebitCredit {
        public static string DEB = "DEB";
        public static string CRD = "CRD";
        public static string DEBCRD = "DEBCRD";
        

        public static List<DebitCreditObject> GetDebitCreditList(FTSMain ftsmain)
        {
            List<DebitCreditObject> list = new List<DebitCreditObject>();
            list.Add(new DebitCreditObject(DEB, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + DEB)));
            list.Add(new DebitCreditObject(CRD, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + CRD)));
            list.Add(new DebitCreditObject(DEBCRD, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + DEBCRD)));
            return list;
        }

        /// <summary>
        /// GetDebitCreditList
        /// </summary>
        /// <param name="ftsmain"></param>
        /// <param name="isdebcrd">true/false</param>
        /// <returns></returns>
        public static List<DebitCreditObject> GetDebitCreditList(FTSMain ftsmain, bool isdebcrd)
        {
            List<DebitCreditObject> list = new List<DebitCreditObject>();
            list.Add(new DebitCreditObject(DEB, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + DEB)));
            list.Add(new DebitCreditObject(CRD, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + CRD)));
            if (isdebcrd)
            {
                list.Add(new DebitCreditObject(DEBCRD, ftsmain.MsgManager.GetMessage("DEBIT_CREDIT_" + DEBCRD)));
            }
            return list;
        }
    }
}