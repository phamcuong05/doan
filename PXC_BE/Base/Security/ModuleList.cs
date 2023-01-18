#region
using FTS.Base.Model;
using FTS.Base.Systems;
using System.Collections.Generic;
#endregion

namespace FTS.Base.Security
{
    public static class ModuleList {
        public static string FIN_CASHBANK = "FIN_CASHBANK";
        public static string FIN_AR = "FIN_AR";
        public static string FIN_AP = "FIN_AP";
        public static string FIN_ARAP = "FIN_ARAP"; //CONG NO KHAC
        public static string FIN_OM = "FIN_OM";
        public static string FIN_PO = "FIN_PO";
        public static string FIN_INV = "FIN_INV";
        public static string FIN_TAX = "FIN_TAX";
        public static string FIN_CONTRACT = "FIN_CONTRACT";
        public static string FIN_FA = "FIN_FA";
        public static string FIN_CA = "FIN_CA";
        public static string FIN_GL = "FIN_GL";
        public static string FIN_DICT = "FIN_DICT";
        public static string FIN_SYSTEM = "FIN_SYSTEM";
        public static string FIN_SAMPLE = "FIN_SAMPLE";
        //public static string TP_ROUTE = "TP_ROUTE";
        //public static string TP_VEHICLE = "TP_VEHICLE";
        //public static string QUOTA = "TP_QUOTA";
        //public static string TP_ORDER = "TP_ORDER";
        //public static string HT_REC = "HT_REC";
        //public static string HT_BOO = "HT_BOO";
        //public static string HT_ROMA = "HT_ROMA";
        //public static string HT_LAUN = "HT_LAUN";
        //public static string HT_INV = "HT_INV";
        //public static string HT_RES = "HT_RES";
        //public static string HT_ADMI = "HT_ADMI";
        //public static string SYS = "SYS_PER";
        //public static string POS_BUY = "POS_BUY";
        //public static string POS_ADMI = "POS_ADMI";
        //public static string POS_OTHER = "POS_OTHER";
        //public static string POS_PAYMENT = "POS_PAYMENT";
        //public static string POS_PRINT = "POS_PRINT";
        //public static string POS_SALE = "POS_SALE";
        //public static string POS_CASHBANK = "POS_CASHBANK";
        //public static string HRM_ADMI = "HRM_ADMI";
        //public static string HRM_REC = "HRM_REC";
        //public static string HRM_INFO = "HRM_INFO";
        //public static string HRM_TRAINING = "HRM_TRAINING";
        //public static string HRM_INS = "HRM_INS";
        //public static string HRM_TIME = "HRM_TIME";
        //public static string HRM_SA = "HRM_SA";
        //public static string HRM_SA_ITEM = "HRM_SA_ITEM";
        //public static string HRM_EVALUATION = "HRM_EVALUATION";
        public static string FIN_SCR = "FIN_SCR";
        //public static string FIN_REINS = "FIN_REINS";
        //public static string FIN_REINSBUSI = "FIN_REINSBUSI";
        //public static string FIN_BR = "FIN_BR";
        //public static string BT_WEB = "BT_WEB";
        //public static string DM_WEB = "DM_WEB";
        //public static string SYS_DM = "SYS_DM";
        //public static string BT_XCG = "BT_XCG";
        //public static string ADJ = "ADJ";
        //public static string FIN_PRINT = "FIN_PRINT";

        public static List<ModuleObject> GetModuleList(FTSMain ftsmain)
        {
            List<ModuleObject> list = new List<ModuleObject>();
            list.Add(new ModuleObject(FIN_CASHBANK, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_CASHBANK)));
            list.Add(new ModuleObject(FIN_AR, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_AR)));
            list.Add(new ModuleObject(FIN_AP, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_AP)));
            list.Add(new ModuleObject(FIN_ARAP, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_ARAP)));
            list.Add(new ModuleObject(FIN_OM, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_OM)));
            list.Add(new ModuleObject(FIN_PO, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_PO)));
            list.Add(new ModuleObject(FIN_INV, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_INV)));
            list.Add(new ModuleObject(FIN_TAX, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_TAX)));
            list.Add(new ModuleObject(FIN_CONTRACT, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_CONTRACT)));
            list.Add(new ModuleObject(FIN_FA, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_FA)));
            list.Add(new ModuleObject(FIN_CA, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_CA)));
            list.Add(new ModuleObject(FIN_SCR, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_SCR)));
            list.Add(new ModuleObject(FIN_GL, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_GL)));
            list.Add(new ModuleObject(FIN_DICT, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_DICT)));
            list.Add(new ModuleObject(FIN_SYSTEM, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_SYSTEM)));
            list.Add(new ModuleObject(FIN_SAMPLE, ftsmain.MsgManager.GetMessage("MODULE_LIST_" + FIN_SAMPLE)));
            return list;
        }
    }
}