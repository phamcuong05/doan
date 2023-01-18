import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmExchangeRate } from './dm-exchange-rate';

@Injectable({
  providedIn: 'root',
})
export class DmExchangeRateService extends BaseService<DmExchangeRate> {
  readonly serviceUrl = 'api/Dm_Exchange_Rate/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
