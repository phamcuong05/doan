#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public static class RateType {
        public static string MONTH = "MONTH";
        public static string YEAR = "YEAR";
        public static string OVERDUE = "OVERDUE";

        public static List<RateTypeObject> GetRateTypeList(FTSMain ftsmain) {
            List<RateTypeObject> list = new List<RateTypeObject>();
            list.Add(new RateTypeObject(MONTH, ftsmain.MsgManager.GetMessage("RATE_TYPE_" + MONTH)));
            list.Add(new RateTypeObject(YEAR, ftsmain.MsgManager.GetMessage("RATE_TYPE_" + YEAR)));
            //list.Add(new ItemCombobox(OVERDUE, ftsmain.MsgManager.GetMessage("RATE_TYPE_" + OVERDUE)));
            return list;
        }
    }
}