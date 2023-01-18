import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { SysResource } from './sys-resource';

@Injectable({
  providedIn: 'root',
})
export class SysResoureService extends BaseService<SysResource> {
  readonly serviceUrl = 'api/Sys_Resource/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
