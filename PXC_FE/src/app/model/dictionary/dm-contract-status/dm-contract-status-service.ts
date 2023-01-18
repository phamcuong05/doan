import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmContractStatus } from './dm-contract-status';

@Injectable({
  providedIn: 'root',
})
export class DmContractStatusService extends BaseService<DmContractStatus> {
  readonly serviceUrl = 'api/Dm_Contract_Status/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
