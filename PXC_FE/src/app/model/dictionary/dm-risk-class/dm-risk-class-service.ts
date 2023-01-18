import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmRiskClass } from './dm-risk-class';

@Injectable({
  providedIn: 'root',
})
export class DmRiskClassService extends BaseService<DmRiskClass> {
  readonly serviceUrl = 'api/Dm_Risk_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
