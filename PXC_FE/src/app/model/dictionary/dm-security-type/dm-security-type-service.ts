import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmSecurityType } from './dm-security-type';

@Injectable({
  providedIn: 'root',
})
export class DmSecurityTypeService extends BaseService<DmSecurityType> {
  readonly serviceUrl = 'api/Dm_Security_Type/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
