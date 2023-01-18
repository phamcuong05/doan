import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmSecurityClass } from './dm-security-class';

@Injectable({
  providedIn: 'root',
})
export class DmSecurityClassService extends BaseService<DmSecurityClass> {
  readonly serviceUrl = 'api/Dm_Security_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
