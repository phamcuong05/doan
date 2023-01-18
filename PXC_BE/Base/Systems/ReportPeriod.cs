// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:54
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         ReportPeriod.cs
// Project Item Filename:     ReportPeriod.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using FTS.Base.Utilities;

#endregion

namespace FTS.Base.Systems {
    /// <summary>Class ReportPeriod dinh nghia mot ky bao cao gom co cac thong tin ngay dau ky, ngay cuoi ky va ten cua ky bao cao.
    ///Class nay duoc dung de dua vao 1 combo box tren man hinh lua chon thong so truoc khi in bao cao de tao tien ich cho
    // nguoi su dung chon lua cac ky co ban.</summary>
    [Serializable] public class ReportPeriod {
        public DateTime DayStartOfFirstYear = DateTime.Today;
        public DateTime DayStartOfCurrentYear = DateTime.Today;
        private DateTime mDayEnd = DateTime.Today;
        private DateTime mDayStart = DateTime.Today;
        public string ReportPeriodName = string.Empty;
        public string ReportPeriodShortName = string.Empty;

        public ReportPeriod(string periodname, DateTime daystartoffirstyear, DateTime daystartofcurrentyear) {
            this.ReportPeriodName = periodname;
            this.DayStartOfFirstYear = daystartoffirstyear;
            this.DayStartOfCurrentYear = daystartofcurrentyear;
        }

        public ReportPeriod(string reportPeriodName, DateTime daystartoffirstyear, DateTime dayStart, DateTime dayEnd, DateTime daystartofcurrentyear) {
            this.ReportPeriodName = reportPeriodName;
            this.mDayStart = dayStart;
            this.mDayEnd = dayEnd;
            this.DayStartOfFirstYear = daystartoffirstyear;
            this.DayStartOfCurrentYear = daystartofcurrentyear;
        }

        public ReportPeriod(string reportPeriodName, string reportperiodshortname, DateTime daystartoffirstyear, DateTime dayStart, DateTime dayEnd,
            DateTime daystartofcurrentyear) {
            this.ReportPeriodName = reportPeriodName;
            this.ReportPeriodShortName = reportperiodshortname;
            this.mDayStart = dayStart;
            this.mDayEnd = dayEnd;
            this.DayStartOfFirstYear = daystartoffirstyear;
            this.DayStartOfCurrentYear = daystartofcurrentyear;
        }

        public static string GetMonth(FTSMain ftsmain, int month) {
            switch (month) {
                case 1:
                    return ftsmain.MsgManager.GetMessage("MSG_January");
                case 2:
                    return ftsmain.MsgManager.GetMessage("MSG_February");
                case 3:
                    return ftsmain.MsgManager.GetMessage("MSG_March");
                case 4:
                    return ftsmain.MsgManager.GetMessage("MSG_April");
                case 5:
                    return ftsmain.MsgManager.GetMessage("MSG_May");
                case 6:
                    return ftsmain.MsgManager.GetMessage("MSG_June");
                case 7:
                    return ftsmain.MsgManager.GetMessage("MSG_July");
                case 8:
                    return ftsmain.MsgManager.GetMessage("MSG_August");
                case 9:
                    return ftsmain.MsgManager.GetMessage("MSG_September");
                case 10:
                    return ftsmain.MsgManager.GetMessage("MSG_October");
                case 11:
                    return ftsmain.MsgManager.GetMessage("MSG_November");
                case 12:
                    return ftsmain.MsgManager.GetMessage("MSG_December");
                default:
                    return string.Empty;
            }
        }

        public static string GetQuarter(FTSMain ftsmain, int i) {
            switch (i) {
                case 0:
                    return ftsmain.MsgManager.GetMessage("MSG_1st_Quarter");
                case 1:
                    return ftsmain.MsgManager.GetMessage("MSG_2nd_Quarter");
                case 2:
                    return ftsmain.MsgManager.GetMessage("MSG_3rd_Quarter");
                case 3:
                    return ftsmain.MsgManager.GetMessage("MSG_4th_Quarter");
                default:
                    return string.Empty;
            }
        }

        public static string GetQuarter(DateTime date) {
            switch (date.Month) {
                case 1:
                case 2:
                case 3:
                    return "Jan - Mar";
                case 4:
                case 5:
                case 6:
                    return "Apr - Jun";
                case 7:
                case 8:
                case 9:
                    return "Jul - Sep";
                default:
                    return "Oct - Dec";
            }
        }

        public static ArrayList LoadReportPeriod(FTSMain ftsMain, bool tuyyrow) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            if (tuyyrow) {
                string tuy_y = ftsMain.MsgManager.GetMessage("MSG_TUYY");
                kybaocao = new ReportPeriod(tuy_y, ftsMain.DayStartOfFirstYear, ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            }
            if (ftsMain.Language == Language.VN) {
                string quy = ftsMain.MsgManager.GetMessage("MSG_QUARTER");
                string lblthang = ftsMain.MsgManager.GetMessage("MSG_MONTH");
                string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");
                string ca_nam = ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR");
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(lblthang + " " + dauthang.Month.ToString() + " " + lblnam + " " + dauthang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(lblthang + " " + cuoithang.Month.ToString() + " " + lblnam + " " + cuoithang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy,
                            cuoiquy, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy,
                            cuoiquy, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                kybaocao = new ReportPeriod("6 " + lblthang + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddMonths(6).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
                kybaocao = new ReportPeriod("9 " + lblthang + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddMonths(9).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
                kybaocao = new ReportPeriod(ca_nam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddYears(1).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            } else {
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, dauthang.Month) + " " + dauthang.Year.ToString(), ftsMain.DayStartOfFirstYear, dauthang,
                            cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, cuoithang.Month) + " " + " " + cuoithang.Year.ToString(), ftsMain.DayStartOfFirstYear,
                            dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Year <= 15) {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy, cuoiquy,
                            ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy, cuoiquy,
                            ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                kybaocao = new ReportPeriod(ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR") + " " + ftsMain.DayStartOfCurrentYear.Year,
                    ftsMain.DayStartOfFirstYear, ftsMain.DayStartOfCurrentYear, ftsMain.DayEndOfCurrentYear, ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            }
            return array;
        }

        public static ArrayList LoadReportPeriod(FTSMain ftsMain) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            string tuy_y = ftsMain.MsgManager.GetMessage("MSG_TUYY");
            kybaocao = new ReportPeriod(tuy_y, ftsMain.DayStartOfFirstYear, ftsMain.DayStartOfCurrentYear);
            array.Add(kybaocao);
            if (ftsMain.Language == Language.VN) {
                string quy = ftsMain.MsgManager.GetMessage("MSG_QUARTER");
                string lblthang = ftsMain.MsgManager.GetMessage("MSG_MONTH");
                string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");
                string ca_nam = ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR");
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(lblthang + " " + dauthang.Month.ToString() + " " + lblnam + " " + dauthang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(lblthang + " " + cuoithang.Month.ToString() + " " + lblnam + " " + cuoithang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy,
                            cuoiquy, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy,
                            cuoiquy, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                kybaocao = new ReportPeriod("6 " + lblthang + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddMonths(6).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
                kybaocao = new ReportPeriod("9 " + lblthang + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddMonths(9).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
                kybaocao = new ReportPeriod(ca_nam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddYears(1).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            } else {
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, dauthang.Month) + " " + dauthang.Year.ToString(), ftsMain.DayStartOfFirstYear, dauthang,
                            cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, cuoithang.Month) + " " + " " + cuoithang.Year.ToString(), ftsMain.DayStartOfFirstYear,
                            dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Year <= 15) {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy, cuoiquy,
                            ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear, dauquy, cuoiquy,
                            ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
                kybaocao = new ReportPeriod(ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR") + " " + ftsMain.DayStartOfCurrentYear.Year,
                    ftsMain.DayStartOfFirstYear, ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddYears(1).AddDays(-1),
                    ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            }
            return array;
        }

        public static ArrayList LoadReportPeriodByYear(FTSMain ftsMain) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            if (ftsMain.Language == Language.VN) {
                string ca_nam = ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR");
                kybaocao = new ReportPeriod(ca_nam + " " + ftsMain.DayStartOfCurrentYear.Year, ftsMain.DayStartOfFirstYear,
                    ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddYears(1).AddDays(-1), ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            } else {
                kybaocao = new ReportPeriod(ftsMain.MsgManager.GetMessage("MSG_ALL_YEAR") + " " + ftsMain.DayStartOfCurrentYear.Year,
                    ftsMain.DayStartOfFirstYear, ftsMain.DayStartOfCurrentYear, ftsMain.DayStartOfCurrentYear.AddYears(1).AddDays(-1),
                    ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            }
            return array;
        }

        public static ArrayList LoadReportPeriodByMonth(FTSMain ftsMain) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            if (ftsMain.Language == Language.VN) {
                string lblthang = ftsMain.MsgManager.GetMessage("MSG_MONTH");
                string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(lblthang + " " + dauthang.Month.ToString() + " " + lblnam + " " + dauthang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(lblthang + " " + cuoithang.Month.ToString() + " " + lblnam + " " + cuoithang.Year.ToString(),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
            } else {
                for (int i = 1; i <= 12; i++) {
                    DateTime dauthang = ftsMain.DayStartOfCurrentYear.AddMonths(i - 1);
                    DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, dauthang.Month) + " " + dauthang.Year.ToString(), ftsMain.DayStartOfFirstYear, dauthang,
                            cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, cuoithang.Month) + " " + " " + cuoithang.Year.ToString(), ftsMain.DayStartOfFirstYear,
                            dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
            }
            return array;
        }

        public static ArrayList LoadReportPeriodByQuarter(FTSMain ftsMain) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            if (ftsMain.Language == Language.VN) {
                string quy = ftsMain.MsgManager.GetMessage("MSG_QUARTER");
                string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, quy + " " + i.ToString(),
                            ftsMain.DayStartOfFirstYear, dauquy, cuoiquy, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(quy + " " + i.ToString() + " " + lblnam + " " + ftsMain.DayStartOfCurrentYear.Year, quy + " " + i.ToString(),
                            ftsMain.DayStartOfFirstYear, dauquy, cuoiquy, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
            } else {
                for (int i = 1; i <= 4; i++) {
                    DateTime dauquy = ftsMain.DayStartOfCurrentYear.AddMonths((i - 1)*3);
                    DateTime cuoiquy = dauquy.AddMonths(3).AddDays(-1);
                    if (ftsMain.DayStartOfCurrentYear.Year <= 15) {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, GetQuarter(ftsMain, i - 1),
                            ftsMain.DayStartOfFirstYear, dauquy, cuoiquy, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetQuarter(ftsMain, i - 1) + " " + ftsMain.DayStartOfCurrentYear.Year, GetQuarter(ftsMain, i - 1),
                            ftsMain.DayStartOfFirstYear, dauquy, cuoiquy, ftsMain.DayStartOfCurrentYear);
                    }
                    array.Add(kybaocao);
                }
            }
            return array;
        }

        public static ArrayList LoadReportPeriodByWeek(FTSMain ftsMain) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;

            string lblweek = ftsMain.MsgManager.GetMessage("MSG_WEEK");
            string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");
            string lbltungay = ftsMain.MsgManager.GetMessage("MSG_DAY_START");
            CultureInfo cultureInfo = ftsMain.FTSCultureInfo;
            Calendar cal = cultureInfo.Calendar;
            CalendarWeekRule myCWR = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            int NumWeekOfYearstart = cal.GetWeekOfYear(ftsMain.DayStartOfCurrentYear, myCWR, myFirstDOW);
            int NumWeekOfYearend = cal.GetWeekOfYear(ftsMain.DayEndOfCurrentYear, myCWR, myFirstDOW);

            int NumWeekOfYearstart1 = cal.GetWeekOfYear(new DateTime(ftsMain.DayStartOfCurrentYear.Year,12,31), myCWR, myFirstDOW);
            int NumWeekOfYearend1 = cal.GetWeekOfYear(new DateTime(ftsMain.DayEndOfCurrentYear.Year,12,31), myCWR, myFirstDOW);


            int NumWeekOfYear = NumWeekOfYearstart1 - NumWeekOfYearstart + NumWeekOfYearend1- NumWeekOfYearend;
            DateTime lastWeek = DateTime.Today;
            DateTime dautuan = DateTime.Today;
            DateTime cuoituan = DateTime.Today;
            for (int i = 1; i <= NumWeekOfYear; i++) {
                if (i == 1) {
                    int numdayofweek = Convert.ToInt16(ftsMain.DayStartOfCurrentYear.DayOfWeek);
                    if (numdayofweek != 0) {
                        dautuan = ftsMain.DayStartOfCurrentYear.AddDays(-1*(Convert.ToInt16(ftsMain.DayStartOfCurrentYear.DayOfWeek) - 1));
                    } else {
                        dautuan = ftsMain.DayStartOfCurrentYear.AddDays(-6);
                    }
                    cuoituan = dautuan.AddDays(6);
                    lastWeek = cuoituan;
                } else {
                    dautuan = lastWeek.AddDays(1);
                    cuoituan = lastWeek.AddDays(7);
                    lastWeek = cuoituan;
                }
                kybaocao =
                    new ReportPeriod(
                        lblweek + " " + i.ToString() + " :" + lbltungay + " " + Convert.ToDateTime(dautuan, ftsMain.FTSCultureInfo).ToString("dd/MM/yyyy") +
                        "->" + Convert.ToDateTime(cuoituan, ftsMain.FTSCultureInfo).ToString("dd/MM/yyyy"), ftsMain.DayStartOfFirstYear, dautuan, cuoituan,
                        ftsMain.DayStartOfCurrentYear);
                array.Add(kybaocao);
            }
            return array;
        }

        public static void GetDateInWeek(FTSMain ftsMain, DateTime dateSelected, out DateTime StartDate, out DateTime EndDate) {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            int numdayofweek = Convert.ToInt16(dateSelected.DayOfWeek);
            if (numdayofweek != 0) {
                StartDate = dateSelected.AddDays(-1*(Convert.ToInt16(dateSelected.DayOfWeek) - 1));
            } else {
                StartDate = dateSelected.AddDays(-6);
            }
            EndDate = StartDate.AddDays(6);
        }

        public override string ToString() {
            return this.ReportPeriodName;
        }

        public string GetId() {
            return Functions.ParseDate(this.mDayStart) + "_" + Functions.ParseDate(this.mDayEnd);
        }

        public DateTime DayEnd {
            get { return this.mDayEnd; }
            set { this.mDayEnd = value; }
        }

        public DateTime DayEndLastPeriod {
            get { return this.mDayStart.AddDays(-1); }
        }

        public DateTime DayEndOfMonth {
            get { return Functions.DayEndOfMonth(this.mDayStart.Month, this.mDayStart.Year); }
        }

        public DateTime DayEndOfQuarter {
            get { return this.DayStartOfQuarter.AddMonths(3).AddDays(-1); }
        }

        public DateTime DayEndOfCurrentYear {
            get { return this.DayStartOfCurrentYear.AddDays(-1).AddYears(1); }
        }

        public DateTime DayStart {
            get { return this.mDayStart; }
            set { this.mDayStart = value; }
        }

        public DateTime DayStartLastPeriod {
            get {
                int sothang = this.mDayEnd.Month - this.mDayStart.Month + 12*(this.mDayEnd.Year - this.mDayStart.Year);
                DateTime thang = this.mDayStart.AddMonths(sothang*(-1) - 1);
                return thang;
            }
        }

        public DateTime DayStartOfMonth {
            get { return Functions.DayStartOfMonth(this.mDayStart.Month, this.mDayStart.Year); }
        }

        public DateTime DayStartOfQuarter {
            get {
                int sothang = this.mDayStart.Month - this.DayStartOfCurrentYear.Month + 12*(this.mDayStart.Year - this.DayStartOfCurrentYear.Year);
                if (sothang < 0) {
                    return DateTime.Today;
                }
                if (sothang < 3) {
                    return this.DayStartOfCurrentYear;
                } else {
                    if (sothang < 6) {
                        return this.DayStartOfCurrentYear.AddMonths(3);
                    } else {
                        if (sothang < 9) {
                            return this.DayStartOfCurrentYear.AddMonths(6);
                        } else {
                            if (sothang < 12) {
                                return this.DayStartOfCurrentYear.AddMonths(9);
                            } else {
                                return DateTime.Today;
                            }
                        }
                    }
                }
            }
        }

        public DateTime DayStart1Quarter {
            get { return this.DayStartOfCurrentYear; }
        }

        public DateTime DayEnd1Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(3).AddDays(-1); }
        }

        public DateTime DayStart2Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(3); }
        }

        public DateTime DayEnd2Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(6).AddDays(-1); }
        }

        public DateTime DayStart3Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(6); }
        }

        public DateTime DayEnd3Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(9).AddDays(-1); }
        }

        public DateTime DayStart4Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(9); }
        }

        public DateTime DayEnd4Quarter {
            get { return this.DayStartOfCurrentYear.AddMonths(12).AddDays(-1); }
        }

        public static ReportPeriod GetReportPeriod(FTSMain ftsmain, string id) {
            ArrayList list = LoadReportPeriod(ftsmain, true);
            foreach (ReportPeriod period in list) {
                if (period.GetId() == id) {
                    return period;
                }
            }
            return null;
        }

        //public static List<ItemCombobox> LoadReportPeriodList(FTSMain ftsMain) {
        //    List<ItemCombobox> array = new List<ItemCombobox>();
        //    ArrayList reportlist = LoadReportPeriod(ftsMain);
        //    foreach (object list in reportlist) {
        //        ReportPeriod kybaocao = (ReportPeriod) list;
        //        array.Add(new ItemCombobox(kybaocao.GetId(), kybaocao.ToString()));
        //    }
        //    return array;
        //}

        public static ArrayList LoadReportPeriodByMonth(FTSMain ftsMain, DateTime start, DateTime end) {
            ArrayList array = new ArrayList();
            ReportPeriod kybaocao;
            DateTime daystart = start;
            int totalmonth = end.Month + end.Year*12 - daystart.Month - daystart.Year*12 + 1;
            string lblthang = ftsMain.MsgManager.GetMessage("MSG_MONTH");
            string lblnam = ftsMain.MsgManager.GetMessage("MSG_YEAR");

            for (int i = 1; i <= totalmonth; i++) {
                DateTime dauthang = daystart.AddMonths(i - 1);
                DateTime cuoithang = dauthang.AddMonths(1).AddDays(-1);
                if (ftsMain.Language == Language.VN) {
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(lblthang + " " + dauthang.Month.ToString() + " " + lblnam + " " + dauthang.Year.ToString(),
                            lblthang + " " + dauthang.Month.ToString(), ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(lblthang + " " + cuoithang.Month.ToString() + " " + lblnam + " " + cuoithang.Year.ToString(),
                            lblthang + " " + cuoithang.Month.ToString(), ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                } else {
                    if (ftsMain.DayStartOfCurrentYear.Day <= 15) {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, dauthang.Month) + " " + dauthang.Year.ToString(), GetMonth(ftsMain, dauthang.Month),
                            ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    } else {
                        kybaocao = new ReportPeriod(GetMonth(ftsMain, cuoithang.Month) + " " + " " + cuoithang.Year.ToString(),
                            GetMonth(ftsMain, cuoithang.Month), ftsMain.DayStartOfFirstYear, dauthang, cuoithang, ftsMain.DayStartOfCurrentYear);
                    }
                }
                array.Add(kybaocao);
            }
            return array;
        }
    }
}