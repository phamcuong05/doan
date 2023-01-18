#region

using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Systems;
using FTS.ShareBusiness.Model;

#endregion

namespace FTS.ShareBusiness.Acc {
    public static class PrDetailType {
        public static string CUSTOMER = "00";
        public static string DEPARTMENT = "01";
        public static string EMPLOYEE = "02";
        public static string AGENT = "03";

        //public static List<ItemCombobox> GetPrDetailTypeList(FTSMain ftsmain) {
        //    List<ItemCombobox> list = new List<ItemCombobox>();
        //    list.Add(new ItemCombobox(CUSTOMER, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + CUSTOMER)));
        //    list.Add(new ItemCombobox(DEPARTMENT, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + DEPARTMENT)));
        //    list.Add(new ItemCombobox(EMPLOYEE, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + EMPLOYEE)));
        //    list.Add(new ItemCombobox(AGENT, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + AGENT)));
        //    return list;
        //}
        public static List<Dm_Pr_Class_TypeObject> GetPrDetailTypeList(FTSMain ftsmain)
        {
            List<Dm_Pr_Class_TypeObject> list = new List<Dm_Pr_Class_TypeObject>();
            list.Add(new Dm_Pr_Class_TypeObject(CUSTOMER, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + CUSTOMER)));
            list.Add(new Dm_Pr_Class_TypeObject(DEPARTMENT, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + DEPARTMENT)));
            list.Add(new Dm_Pr_Class_TypeObject(EMPLOYEE, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + EMPLOYEE)));
            list.Add(new Dm_Pr_Class_TypeObject(AGENT, ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + AGENT)));
            return list;
        }
        public static string GetPrDetailTypeName(FTSMain ftsmain, string typeid)
        {
            return ftsmain.MsgManager.GetMessage("PR_DETAIL_TYPE_" + typeid);
        }
    }
}