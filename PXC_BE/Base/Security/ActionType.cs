#region

using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Business;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Security {
    public static class ActionType {
        public static string LOGIN = "01";
        public static string LOGOUT = "02";
        public static string ADD = "03";
        public static string EDIT = "04";
        public static string DELETE = "05";
        public static string CLOSING_ENTRY = "06";
        public static string COST_METHOD = "07";
        public static string TOOL_ALLOCATION = "08";
        public static string ASSET_DEP = "09";
        public static string CCDC_DEP = "10";
        public static string CA_EXPENSE = "11";
        public static string CA_PRODUCT_COST = "12";
        public static string SEND_EMAIL = "13";

        public static List<ItemCombobox> GetActionTypeList(FTSMain ftsmain) {
            List<ItemCombobox> list = new List<ItemCombobox>();
            list.Add(new ItemCombobox(LOGIN, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + LOGIN)));
            list.Add(new ItemCombobox(LOGOUT, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + LOGOUT)));
            list.Add(new ItemCombobox(ADD, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + ADD)));
            list.Add(new ItemCombobox(EDIT, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + EDIT)));
            list.Add(new ItemCombobox(DELETE, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + DELETE)));
            list.Add(new ItemCombobox(CLOSING_ENTRY, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + CLOSING_ENTRY)));
            list.Add(new ItemCombobox(COST_METHOD, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + COST_METHOD)));
            list.Add(new ItemCombobox(TOOL_ALLOCATION, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + TOOL_ALLOCATION)));
            list.Add(new ItemCombobox(ASSET_DEP, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + ASSET_DEP)));
            list.Add(new ItemCombobox(CCDC_DEP, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + CCDC_DEP)));
            list.Add(new ItemCombobox(CA_EXPENSE, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + CA_EXPENSE)));
            list.Add(new ItemCombobox(CA_PRODUCT_COST, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + CA_PRODUCT_COST)));
            list.Add(new ItemCombobox(SEND_EMAIL, ftsmain.MsgManager.GetMessage("ACTION_TYPE_" + SEND_EMAIL)));

            return list;
        }
    }
}