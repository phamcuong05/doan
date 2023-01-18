#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class PeriodType
    {
        public static string DAY = "DAY";
        public static string WEEK = "WEEK";
        public static string MONTH = "MONTH";
        public static string QUARTER = "QUARTER";
        public static string YEAR = "YEAR";

        public static List<PeriodTypeObject> GetPeriodTypeList(FTSMain ftsmain)
        {
            List<PeriodTypeObject> list = new List<PeriodTypeObject>();
            list.Add(new PeriodTypeObject(DAY, ftsmain.MsgManager.GetMessage("PERIOD_TYPE_" + DAY)));
            list.Add(new PeriodTypeObject(WEEK, ftsmain.MsgManager.GetMessage("PERIOD_TYPE_" + WEEK)));
            list.Add(new PeriodTypeObject(MONTH, ftsmain.MsgManager.GetMessage("PERIOD_TYPE_" + MONTH)));
            list.Add(new PeriodTypeObject(QUARTER, ftsmain.MsgManager.GetMessage("PERIOD_TYPE_" + QUARTER)));
            list.Add(new PeriodTypeObject(YEAR, ftsmain.MsgManager.GetMessage("PERIOD_TYPE_" + YEAR)));
            return list;
        }
    }
}