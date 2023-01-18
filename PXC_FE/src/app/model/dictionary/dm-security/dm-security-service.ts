import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmSecurity } from './dm-security';

@Injectable({
  providedIn: 'root',
})
export class DmSecurityService extends BaseService<DmSecurity> {
  readonly serviceUrl = 'api/Dm_Security/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
