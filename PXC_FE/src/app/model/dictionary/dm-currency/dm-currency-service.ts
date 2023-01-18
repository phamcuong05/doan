import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmCurrency } from './dm-currency';

@Injectable({
  providedIn: 'root',
})
export class DmCurrencyService extends BaseService<DmCurrency> {
  readonly serviceUrl = 'api/Dm_Currency/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }

  /**
   * Lấy exchange rate theo ngày chứng từ và mã tiền tệ
   * @param tranDate
   * @param currencyId
   * @returns
   */
  public getExchangeRate(tranDate: Date, currencyId: string): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}GetExchangeRate`,
      {
        tranDate: tranDate.toJSON(),
        currencyId: currencyId,
      }
    );
  }
}
