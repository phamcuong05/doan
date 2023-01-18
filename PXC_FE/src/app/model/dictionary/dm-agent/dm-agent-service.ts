import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmAgent } from './dm-agent';

@Injectable({
  providedIn: 'root',
})
export class DmAgentService extends BaseService<DmAgent> {
  readonly serviceUrl = 'api/Dm_Agent/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
