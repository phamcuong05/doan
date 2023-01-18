using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Systems;

namespace FTS.Base.Security
{
    public class FTSFunctionCollection {
        //HETHONG
        public static FTSFunction REPORT_MANAGEMENT = new FTSFunction("REPORT_MANAGEMENT", "ACC", false, true, true, true, true, false);
        public static FTSFunction TRAN_OUTPUT_EDIT = new FTSFunction("TRAN_OUTPUT_EDIT", "ACC", false, false, false, false, true, false);

        //DANH MUC
        public static FTSFunction DM_JOB = new FTSFunction("DM_JOB", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ORGANIZATION = new FTSFunction("DM_ORGANIZATION", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ACCOUNT = new FTSFunction("DM_ACCOUNT", "SHARE", false, true, true, true, false,false);
        public static FTSFunction DM_AGENT = new FTSFunction("DM_AGENT", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_BANK = new FTSFunction("DM_BANK", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CAPITAL_SOURCE = new FTSFunction("DM_CAPITAL_SOURCE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CONTRACT_CLASS = new FTSFunction("DM_CONTRACT_CLASS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CONTRACT_LIMIT = new FTSFunction("DM_CONTRACT_LIMIT", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CONTRACT_STATUS = new FTSFunction("DM_CONTRACT_STATUS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CURRENCY = new FTSFunction("DM_CURRENCY", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_DEPARTMENT = new FTSFunction("DM_DEPARTMENT", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_DISTRICT = new FTSFunction("DM_DISTRICT", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_EMPLOYEE = new FTSFunction("DM_EMPLOYEE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ESTIMATE_TYPE = new FTSFunction("DM_ESTIMATE_TYPE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_EXCHANGE_RATE = new FTSFunction("DM_EXCHANGE_RATE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_EXPENSE = new FTSFunction("DM_EXPENSE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_EXPENSE_CLASS = new FTSFunction("DM_EXPENSE_CLASS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ITEM = new FTSFunction("DM_ITEM", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ITEM_CLASS = new FTSFunction("DM_ITEM_CLASS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ITEM_OP = new FTSFunction("DM_ITEM_OP", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_PR_DETAIL = new FTSFunction("DM_PR_DETAIL", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_PR_DETAIL_CLASS = new FTSFunction("DM_PR_DETAIL_CLASS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_PROVINCE = new FTSFunction("DM_PROVINCE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_RISK_CLASS = new FTSFunction("DM_RISK_CLASS", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_UNIT = new FTSFunction("DM_UNIT", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_VAT_TAX = new FTSFunction("DM_VAT_TAX", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_WAREHOUSE = new FTSFunction("DM_WAREHOUSE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_PERIOD = new FTSFunction("DM_PERIOD", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_ADVANCE_LIMIT = new FTSFunction("DM_TAX_LIMIT_BY_ORG", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_SECURITY_CLASS = new FTSFunction("DM_SECURITY_CLASS", "SHARE", true, true, true, true, false, false);
        public static FTSFunction DM_SECURITY_TYPE = new FTSFunction("DM_SECURITY_TYPE", "SHARE", true, true, true, true, false, false);
        public static FTSFunction DM_SECURITY = new FTSFunction("DM_SECURITY", "SHARE", true, true, true, true, false, false);
        public static FTSFunction DM_VAT_PURCHASE = new FTSFunction("DM_VAT_PURCHASE", "SHARE", false, true, true, true, false, false);
        public static FTSFunction DM_CASHBANK_LIMIT = new FTSFunction("DM_CASHBANK_LIMIT", "SHARE", false, true, true, true, true, false);

        //

        public static FTSFunction LIST_SERVICE_CHARGE = new FTSFunction("LIST_SERVICE_CHARGE", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_PACKAGE = new FTSFunction("LIST_PACKAGE", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_CONTAINER = new FTSFunction("LIST_CONTAINER", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_ORDER = new FTSFunction("LIST_ORDER", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_WH = new FTSFunction("LIST_WH", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_MAWB = new FTSFunction("LIST_MAWB", "ACC", false, true, true, true, true, false);
        public static FTSFunction LIST_DELIVERY = new FTSFunction("LIST_DELIVERY", "ACC", false, true, true, true, true, false);



        public static List<FTSFunction> GetFunctionList(FTSMain ftsmain) {
            List<FTSFunction> list = new List<FTSFunction>();
            
            //HE THONG
            list.Add(FTSFunctionCollection.REPORT_MANAGEMENT);
            list.Add(FTSFunctionCollection.TRAN_OUTPUT_EDIT);

            //DANH MUC
            list.Add(FTSFunctionCollection.DM_JOB);
            list.Add(FTSFunctionCollection.DM_ORGANIZATION);
            list.Add(FTSFunctionCollection.DM_ACCOUNT);
            list.Add(FTSFunctionCollection.DM_AGENT);
            list.Add(FTSFunctionCollection.DM_BANK);
            list.Add(FTSFunctionCollection.DM_CAPITAL_SOURCE);
            list.Add(FTSFunctionCollection.DM_CONTRACT_CLASS);
            list.Add(FTSFunctionCollection.DM_CONTRACT_LIMIT);
            list.Add(FTSFunctionCollection.DM_CONTRACT_STATUS);
            list.Add(FTSFunctionCollection.DM_CURRENCY);
            list.Add(FTSFunctionCollection.DM_DEPARTMENT);
            list.Add(FTSFunctionCollection.DM_DISTRICT);
            list.Add(FTSFunctionCollection.DM_EMPLOYEE);
            list.Add(FTSFunctionCollection.DM_ESTIMATE_TYPE);
            list.Add(FTSFunctionCollection.DM_EXCHANGE_RATE);
            list.Add(FTSFunctionCollection.DM_EXPENSE);
            list.Add(FTSFunctionCollection.DM_EXPENSE_CLASS);
            list.Add(FTSFunctionCollection.DM_ITEM);
            list.Add(FTSFunctionCollection.DM_ITEM_CLASS);
            list.Add(FTSFunctionCollection.DM_ITEM_OP);
            list.Add(FTSFunctionCollection.DM_PR_DETAIL);
            list.Add(FTSFunctionCollection.DM_PR_DETAIL_CLASS);
            list.Add(FTSFunctionCollection.DM_PROVINCE);
            list.Add(FTSFunctionCollection.DM_RISK_CLASS);
            list.Add(FTSFunctionCollection.DM_UNIT);
            list.Add(FTSFunctionCollection.DM_VAT_TAX);
            list.Add(FTSFunctionCollection.DM_WAREHOUSE);
            list.Add(FTSFunctionCollection.DM_VAT_PURCHASE);
            list.Add(FTSFunctionCollection.DM_CASHBANK_LIMIT);

            //
            list.Add(FTSFunctionCollection.LIST_SERVICE_CHARGE);
            list.Add(FTSFunctionCollection.LIST_PACKAGE);
            list.Add(FTSFunctionCollection.LIST_CONTAINER); 
            list.Add(FTSFunctionCollection.LIST_ORDER); 
            list.Add(FTSFunctionCollection.LIST_WH); 
            list.Add(FTSFunctionCollection.LIST_MAWB);
            list.Add(FTSFunctionCollection.LIST_DELIVERY);


            return list;
        }
    }
}