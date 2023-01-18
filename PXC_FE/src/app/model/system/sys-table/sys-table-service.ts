import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { SysTable } from './sys-table';

@Injectable({
  providedIn: 'root',
})
export class SysTableService extends BaseService<SysTable> {
  readonly serviceUrl = 'api/Sys_Table/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }

}
