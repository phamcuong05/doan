#region

using System;
using System.Collections.Generic;
using FTS.Base.Systems;

#endregion

namespace FTS.Base.Report {
    public class ReportFieldInfo {
        private string mReport_Id = string.Empty;
        private string mField_Id = string.Empty;
        private string mField_Group_Id = string.Empty;
        private string mField_Type = string.Empty;
        private Int16 mVisible = 0;
        private Int32 mDecimal_Digit = 0;
        private Int16 mIs_Sum = 0;
        private string mFormula = string.Empty;
        private Int16 mShow_In_Group = 0;
        private Int16 mHide_Detail = 0;
        private Int32 mWidth = 0;
        private Int32 mPart = 0;
        private Int32 mAngle = 0;
        private FTSMain mFTSMain;

        public ReportFieldInfo(FTSMain ftsmain, string report_id, string field_id, string field_group_id, string field_type, Int16 visible,
            Int32 decimal_digit, Int16 is_sum, string formula, Int16 show_in_group, Int16 hide_detail, Int32 width, Int32 part, Int32 angle) {
            this.mReport_Id = report_id;
            this.mField_Id = field_id;
            this.mField_Group_Id = field_group_id;
            this.mField_Type = field_type;
            this.mVisible = visible;
            this.mDecimal_Digit = decimal_digit;
            this.mIs_Sum = is_sum;
            this.mFormula = formula;
            this.mShow_In_Group = show_in_group;
            this.mHide_Detail = hide_detail;
            this.mFTSMain = ftsmain;
            this.mWidth = width;
            this.mPart = part;
            this.mAngle = angle;
        }

        public string Field_Id {
            get { return this.mField_Id; }
            set { this.mField_Id = value; }
        }

        public string Field_Group_Id {
            get { return this.mField_Group_Id; }
            set { this.mField_Group_Id = value; }
        }

        public string Field_Group_Name {
            get { return this.mFTSMain.ResourceManager.GetFieldGroupDisplayName(this.mReport_Id, this.Field_Group_Id); }
            set { this.mFTSMain.ResourceManager.SetFieldGroupDisplayName(this.mReport_Id, this.Field_Group_Id, value); }
        }

        public string Field_Type {
            get { return this.mField_Type; }
            set { this.mField_Type = value; }
        }

        public string Display_Name {
            get { return this.mFTSMain.ResourceManager.GetFieldDisplayName(this.mReport_Id, this.Field_Id); }
            set { this.mFTSMain.ResourceManager.SetFieldDisplayName(this.mReport_Id, this.Field_Id, value); }
        }

        public Int16 Visible {
            get { return this.mVisible; }
            set { this.mVisible = value; }
        }

        public Int32 Decimal_Digit {
            get { return this.mDecimal_Digit; }
            set { this.mDecimal_Digit = value; }
        }

        public Int32 Angle {
            get { return this.mAngle; }
            set { this.mAngle = value; }
        }

        public Int32 Part_Num {
            get { return this.mPart; }
            set { this.mPart = value; }
        }

        public Int32 Width {
            get { return this.mWidth; }
            set { this.mWidth = value; }
        }

        public Int16 Is_Sum {
            get { return this.mIs_Sum; }
            set { this.mIs_Sum = value; }
        }

        public string Formula {
            get { return this.mFormula; }
            set { this.mFormula = value; }
        }

        public Int16 Show_In_Group {
            get { return this.mShow_In_Group; }
            set { this.mShow_In_Group = value; }
        }

        public Int16 Hide_Detail {
            get { return this.mHide_Detail; }
            set { this.mHide_Detail = value; }
        }

        public override string ToString() {
            if (this.mField_Group_Id != string.Empty && this.Field_Group_Name != string.Empty) {
                return this.mField_Id + " - " + this.Display_Name + "(" + this.Field_Group_Name + ")";
            } else {
                return this.mField_Id + " - " + this.Display_Name;
            }
        }
    }

    public class ReportGroupInfo {
        private string mField_Group_Id = string.Empty;
        private string mReport_Id = string.Empty;
        private FTSMain mFTSMain;
        private List<ReportFieldInfo> mFields = new List<ReportFieldInfo>();

        public ReportGroupInfo(FTSMain ftsmain, string report_id, string field_group_id) {
            this.mFTSMain = ftsmain;
            this.mField_Group_Id = field_group_id;
            this.mReport_Id = report_id;
        }

        public ReportGroupInfo(FTSMain ftsmain, string report_id, string field_group_id, ReportFieldInfo field) : this(ftsmain, report_id, field_group_id) {
            this.mFields.Add(field);
        }

        public string Field_Group_Id {
            get { return this.mField_Group_Id; }
            set { this.mField_Group_Id = value; }
        }

        public string Field_Group_Name {
            get { return this.mFTSMain.ResourceManager.GetFieldGroupDisplayName(this.mReport_Id, this.Field_Group_Id); }
            set { this.mFTSMain.ResourceManager.SetFieldGroupDisplayName(this.mReport_Id, this.Field_Group_Id, value); }
        }

        public List<ReportFieldInfo> Fields {
            get { return this.mFields; }
        }

        public override string ToString() {
            return this.Field_Group_Name;
        }
    }
}