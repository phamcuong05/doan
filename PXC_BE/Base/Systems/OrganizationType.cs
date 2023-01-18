#region

using System.Collections.Generic;
using FTS.Base.Business;
using FTS.Base.Model;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Systems {
    public static class OrganizationType {
        public static string CORP = "01";
        public static string COMPANY = "02";
        public static string INDEPENDANT_ORGANIZATION = "03";
        public static string DEPENDANT_ORGANIZATION = "04";

        //public static List<ItemCombobox> GetOrganizationTypeList(FTSMain ftsmain) {
        //    List<ItemCombobox> list = new List<ItemCombobox>();
        //    list.Add(new ItemCombobox(CORP, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + CORP)));
        //    list.Add(new ItemCombobox(COMPANY, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + COMPANY)));
        //    list.Add(new ItemCombobox(INDEPENDANT_ORGANIZATION, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + INDEPENDANT_ORGANIZATION)));
        //    list.Add(new ItemCombobox(DEPENDANT_ORGANIZATION, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + DEPENDANT_ORGANIZATION)));
        //    return list;
        //}

        public static List<OrganizarionTypeObject> GetOrganizationTypeList(FTSMain ftsmain)
        {
            List<OrganizarionTypeObject> list = new List<OrganizarionTypeObject>();
            list.Add(new OrganizarionTypeObject(CORP, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + CORP)));
            list.Add(new OrganizarionTypeObject(COMPANY, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + COMPANY)));
            list.Add(new OrganizarionTypeObject(INDEPENDANT_ORGANIZATION, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + INDEPENDANT_ORGANIZATION)));
            list.Add(new OrganizarionTypeObject(DEPENDANT_ORGANIZATION, ftsmain.MsgManager.GetMessage("ORGANIZATION_TYPE_" + DEPENDANT_ORGANIZATION)));
            return list;
        }
    }
}