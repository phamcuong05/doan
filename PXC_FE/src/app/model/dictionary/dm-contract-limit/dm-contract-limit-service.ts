import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmContractLimit } from './dm-contract-limit';

@Injectable({
  providedIn: 'root',
})
export class DmContractLimitService extends BaseService<DmContractLimit> {
  readonly serviceUrl = 'api/Dm_Contract_Limit/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
