#region

using System;

#endregion

namespace FTS.Base.Utilities {
    public class FTSReadNumberVN {
        private string[] ChuSo = new string[10] {" không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín"};

        private string[] Tien = new string[6] {"", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ"};

        public string DocSoLuong(decimal sotien, int tpsl) {
            int numtp = 0;
            switch (tpsl) {
                case 1:
                    numtp = 10;
                    break;
                case 2:
                    numtp = 100;
                    break;
                case 3:
                    numtp = 1000;
                    break;
                case 4:
                    numtp = 10000;
                    break;
            }
            long phannguyen = Convert.ToInt64(Math.Floor(sotien));
            if (sotien < 0) {
                phannguyen = Convert.ToInt64(Math.Ceiling(sotien));
            }
            long phanthapphan = Convert.ToInt64(Math.Floor((sotien - (decimal) phannguyen)*numtp));

            string result = string.Empty;
            if (phannguyen == 0) {
                result = "không";
            } else {
                if (phannguyen < 0) {
                    result = "âm " + this.DocTienBangChu(phannguyen*-1, "");
                } else {
                    result = this.DocTienBangChu(phannguyen, " ");
                }
            }
            if (phanthapphan != 0) {
                if (phanthapphan > 0) {
                    if (tpsl == 2 && phanthapphan < 10) {
                        result += " phẩy không " + this.DocTienBangChu(phanthapphan, "");
                    } else {
                        result += " phẩy " + this.DocTienBangChu(phanthapphan, "");
                    }
                } else {
                    if (phannguyen == 0) {
                        result += " phẩy âm " + this.DocTienBangChu(phanthapphan*-1, "");
                    } else {
                        result += " phẩy " + this.DocTienBangChu(phanthapphan*-1, "");
                    }
                }
            } else {
                result += "";
            }
            result = result.Substring(0, 1).ToUpper() + result.Substring(1);
            return result;
        }

        // Hàm đọc số thành chữ
        public string DocTien(decimal sotien, string currency, string currencytext, int tpst) {
            int numtp = 0;
            switch (tpst) {
                case 1:
                    numtp = 10;
                    break;
                case 2:
                    numtp = 100;
                    break;
                case 3:
                    numtp = 1000;
                    break;
                case 4:
                    numtp = 10000;
                    break;
            }
            long phannguyen = Convert.ToInt64(Math.Floor(sotien));
            if (sotien < 0) {
                phannguyen = Convert.ToInt64(Math.Ceiling(sotien));
            }
            long phanthapphan = Convert.ToInt64(Math.Floor((sotien - (decimal) phannguyen)*numtp));
            if (currency == "VND") {
                if (sotien == 0) {
                    return "Không đồng";
                } else {
                    if (phannguyen > 0) {
                        string KetQua = this.DocTienBangChu(phannguyen, " đồng.");
                        return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
                    } else {
                        return "Âm " + this.DocTienBangChu(phannguyen*-1, " đồng.");
                    }
                }
            } else {
                string result = string.Empty;
                if (phannguyen == 0) {
                    result = "Không " + currencytext;
                } else {
                    if (phannguyen < 0) {
                        result = "Âm " + this.DocTienBangChu(phannguyen*-1, " " + currencytext);
                    } else {
                        result = this.DocTienBangChu(phannguyen, " " + currencytext);
                    }
                }
                if (phanthapphan != 0) {
                    if (phanthapphan > 0) {
                        result += " và " + this.DocTienBangChu(phanthapphan, " cents");
                    } else {
                        if (phannguyen == 0) {
                            result += " và âm " + this.DocTienBangChu(phanthapphan*-1, " cents");
                        } else {
                            result += " và " + this.DocTienBangChu(phanthapphan*-1, " cents");
                        }
                    }
                }
                return result.Substring(0, 1).ToUpper() + result.Substring(1);
            }
        }

        // Hàm đọc số thành chữ
        public string DocTien(decimal sotien, string currency, int tpst) {
            return this.DocTien(sotien, currency, currency, tpst);
        }

        public string DocTienBangChu(long SoTien, string strTail) {
            int lan, i;
            long so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) {
                return "số tiền âm !";
            }
            if (SoTien == 0) {
                return "không " + strTail;
            }
            if (SoTien > 0) {
                so = SoTien;
            } else {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 8999999999999999) {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int) (so/1000000000000000);
            so = so - long.Parse(ViTri[5].ToString())*1000000000000000;
            ViTri[4] = (int) (so/1000000000000);
            so = so - long.Parse(ViTri[4].ToString())*+1000000000000;
            ViTri[3] = (int) (so/1000000000);
            so = so - long.Parse(ViTri[3].ToString())*1000000000;
            ViTri[2] = (int) (so/1000000);
            ViTri[1] = (int) ((so%1000000)/1000);
            ViTri[0] = (int) (so%1000);
            if (ViTri[5] > 0) {
                lan = 5;
            } else if (ViTri[4] > 0) {
                lan = 4;
            } else if (ViTri[3] > 0) {
                lan = 3;
            } else if (ViTri[2] > 0) {
                lan = 2;
            } else if (ViTri[1] > 0) {
                lan = 1;
            } else {
                lan = 0;
            }
            bool first = true;
            for (i = lan; i >= 0; i--) {
                tmp = this.DocSo3ChuSo(ViTri[i], first);
                first = false;
                KetQua += tmp;
                if (ViTri[i] != 0) {
                    KetQua += this.Tien[i];
                }
                //if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += "";//&& (!string.IsNullOrEmpty(tmp))
            }
            //if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim() + strTail;
            return KetQua;
        }

        // Hàm đọc số có 3 chữ số
        private string DocSo3ChuSo(int baso, bool firts) {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int) (baso/100);
            chuc = (int) ((baso%100)/10);
            donvi = baso%10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) {
                return "";
            }
            if (tram != 0) {
                KetQua += this.ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) {
                    KetQua += " linh";
                }
            } else {
                if (!firts) {
                    KetQua += " không trăm";
                    if ((chuc == 0) && (donvi != 0)) {
                        KetQua += " linh";
                    }
                }
            }
            if ((chuc != 0) && (chuc != 1)) {
                KetQua += this.ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) {
                    KetQua = KetQua + " linh";
                }
            }
            if (chuc == 1) {
                KetQua += " mười";
            }
            switch (donvi) {
                case 1:
                    if ((chuc != 0) && (chuc != 1)) {
                        KetQua += " mốt";
                    } else {
                        KetQua += this.ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0) {
                        KetQua += this.ChuSo[donvi];
                    } else {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0) {
                        KetQua += this.ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }
    }
}