import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmCashbankLimit } from './dm-cashbank-limit';

@Injectable({
  providedIn: 'root'
})
export class DmCashbankLimitService extends BaseService<DmCashbankLimit>{
  readonly serviceUrl = 'api/Dm_Cashbank_Limit/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
   }
}
