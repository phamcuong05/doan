import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmAdvanceLimit } from './dm-advance-limit';

@Injectable({
  providedIn: 'root',
})
export class DmAdvanceLimitService extends BaseService<DmAdvanceLimit> {
  readonly serviceUrl = 'api/Dm_Advance_Limit/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
