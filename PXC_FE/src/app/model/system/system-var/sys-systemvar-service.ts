import { Injectable } from '@angular/core';
import { SystemVars } from 'src/app/base/system-vars';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';

@Injectable({
  providedIn: 'root',
})
export class SysSystemVarService extends BaseService<SystemVars> {
  readonly serviceUrl = 'api/Sys_SystemVar/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
