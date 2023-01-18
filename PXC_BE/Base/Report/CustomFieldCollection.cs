// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:51
// Project Name:              ReportBase
// Project Filename:          ReportBase.csproj
// Project Item Name:         CustomFieldCollection.cs
// Project Item Filename:     CustomFieldCollection.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System.Xml.Serialization;

#endregion

namespace FTS.Base.Report {
    public class CustomFieldCollection {
        [XmlArray("Items")] public CustomField[] CustomFields;

        public CustomFieldCollection() {}
    }
}