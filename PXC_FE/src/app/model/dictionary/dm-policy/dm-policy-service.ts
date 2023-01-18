import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmPolicy } from './dm-policy';

@Injectable({
  providedIn: 'root',
})
export class DmPolicyService extends BaseService<DmPolicy> {
  readonly serviceUrl = 'api/Dm_Policy/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
