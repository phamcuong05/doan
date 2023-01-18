import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmContractClass } from './dm-contract-class';

@Injectable({
  providedIn: 'root',
})
export class DmContractClassService extends BaseService<DmContractClass> {
  readonly serviceUrl = 'api/Dm_Contract_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
