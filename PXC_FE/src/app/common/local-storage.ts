import { EventEmitter, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthState } from '../model/login/auth-state';
import { Constaints } from './constaints';
import { EnumLangID } from './enum';
import { mode, AES, enc } from 'crypto-js';
import { Period } from '../model/other/period';

@Injectable({
  providedIn: 'root',
})
/**
 * Object xử lý việc đọc ghi dữ liệu từ localStorage
 * Tất cả data set vào storage đề gọi qua đây dễ quản lý
 */
export class LocalStorage {
  //#region Ngôn ngữ

  /**
   * Get Language Id
   */
  public get LangID(): EnumLangID {
    let langId: EnumLangID = EnumLangID.VIE;
    let _langId = localStorage.getItem(Constaints.LocalStorageKey.LANG_ID);
    if (_langId) {
      langId = EnumLangID[_langId as keyof typeof EnumLangID];
    }
    return langId;
  }

  /**
   * Set Language Id
   */
  public set LangID(v: EnumLangID) {
    localStorage.setItem(Constaints.LocalStorageKey.LANG_ID, EnumLangID[v]);
    this.EventChangeLanguage.next();
  }

  /**
   * Event Thay đổi ngôn ngữ
   */
  EventChangeLanguage = new EventEmitter();

  //#endregion

  //#region Token
  /**
   * Lấy auth State trong localstorage
   */
  public AuthState(): AuthState | null {
    const strAuthState = localStorage.getItem(
      Constaints.LocalStorageKey.AUTH_STATE
    );
    if (strAuthState) {
      const authState = JSON.parse(strAuthState);
      authState.expiredAt = new Date(authState.expiredAt);
      return {
        ...authState,
        token: AES.decrypt(authState.token || '', environment.crypKey, {
          mode: mode.CBC,
        }).toString(enc.Utf8),
      };
    }
    return null;
  }

  /**
   * Set AuthState vào LocalStorage
   * @param v AuthState
   */
  public SetAuthState(v?: AuthState) {
    if (v) {
      let token = AES.encrypt(v.token || '', environment.crypKey, {
        mode: mode.CBC,
      }).toString();
      localStorage.setItem(
        Constaints.LocalStorageKey.AUTH_STATE,
        JSON.stringify({
          ...v,
          token: token,
        })
      );
    } else {
      localStorage.removeItem(Constaints.LocalStorageKey.AUTH_STATE);
    }
  }

  //#endregion

  //#region Periods
  public getPeriod(formName: string): Period | undefined {
    const strPeriods = localStorage.getItem(Constaints.PERIODS);
    if (strPeriods) {
      const period: Period = JSON.parse(strPeriods)?.[formName];
      if (period) {
        return {
          ...period,
          FromDate: new Date(period.FromDate),
          ToDate: new Date(period.ToDate),
        };
      }
    }

    return undefined;
  }

  public setPeriod(v: Period, formName: string) {
    const strPeriods = localStorage.getItem(Constaints.PERIODS);
    let periods: any = {};
    if (strPeriods) {
      periods = JSON.parse(strPeriods);
    }
    if (!periods) {
      periods = {};
    }
    periods[formName] = v;

    localStorage.setItem(Constaints.PERIODS, JSON.stringify(periods));
  }

  //#endregion

  //#region fontsize
  public get FontSize(): number {
    const strFontSize = localStorage.getItem(
      Constaints.LocalStorageKey.FONTSIZE
    );
    if (strFontSize) {
      return Number.parseInt(strFontSize) || 13;
    }
    return 13;
  }

  public set FontSize(v: number | undefined) {
    if (v) {
      localStorage.setItem(Constaints.LocalStorageKey.FONTSIZE, v.toString());
    } else {
      localStorage.removeItem(Constaints.LocalStorageKey.FONTSIZE);
    }
  }

  //#endregion

  //#region darkMode
  public get DarkMode(): boolean {
    if (localStorage.getItem(Constaints.LocalStorageKey.DARKMODE)) {
      return true;
    }
    return false;
  }

  public set DarkMode(v: boolean) {
    if (v) {
      localStorage.setItem(Constaints.LocalStorageKey.DARKMODE, v.toString());
    } else {
      localStorage.removeItem(Constaints.LocalStorageKey.DARKMODE);
    }
  }

  //#endregion
  
  public LoadedGetStarted : boolean = false;
  
}

@Injectable({
  providedIn: 'root',
})
export class LocaleService {
  constructor() {}

  private _locale: string = 'vi-VN';

  set locale(value: string) {
    this._locale = value;
  }
  get locale(): string {
    return this._locale || 'vi-VN';
  }

  public registerCulture(culture: string) {
    if (!culture) {
      return;
    }
    switch (culture) {
      case 'en-US': {
        this._locale = 'en-US';
        break;
      }
      default: {
        this._locale = 'vi-VN';
        break;
      }
    }
  }
}
