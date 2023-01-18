#region

using System.Collections.Generic;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc
{
    public class RateMethodType
    {
        public static string BQ = "BQ";
        public static string DD = "DD";
        public static string DDLN = "DDLN";
        public static string DDLOT = "DDLOT";
        public static string NTXT = "NTXT";
        public static string BQTT = "BQTT";
        public static string HT = "HT";


        public static List<RateMethodObject> GetRateMethodList(FTSMain ftsmain)
        {
            List<RateMethodObject> list = new List<RateMethodObject>();
            list.Add(new RateMethodObject(BQ, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + BQ)));
            list.Add(new RateMethodObject(DD, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + DD)));
            list.Add(new RateMethodObject(DDLN, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + DDLN)));
            list.Add(new RateMethodObject(DDLOT, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + DDLOT)));
            list.Add(new RateMethodObject(NTXT, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + NTXT)));
            list.Add(new RateMethodObject(BQTT, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + BQTT)));
            list.Add(new RateMethodObject(HT, ftsmain.MsgManager.GetMessage("RATE_METHOD_TYPE" + HT)));
            return list;
        }
    }
}