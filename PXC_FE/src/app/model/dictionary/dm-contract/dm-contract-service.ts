import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmContract } from './dm-contract';

@Injectable({
  providedIn: 'root',
})
export class DmContractService extends BaseService<DmContract> {
  readonly serviceUrl = 'api/Dm_Contract/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
