// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/29/2006
// Time:                      15:51
// Project Name:              ReportBase
// Project Filename:          ReportBase.csproj
// Project Item Name:         CustomField.cs
// Project Item Filename:     CustomField.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

namespace FTS.Base.Report {
    public class CustomField {
        public string Section = "ReportHeader";
        public string Name = "DON_VI";
        public string Text = "Hello World";
        public string Para = "Hello World";
        public int top = -1;
        public int left = 0;
        public int width = 200;
        public int box = 0;
        public string font_name;
        public int font_size;
        public string font_style;
        public string font_color;
        public string textalignment = "LEFT";
        public string beforeafter = "AFTER";

        public CustomField() {}
    }
}