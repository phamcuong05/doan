import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmEmployee } from './dm-employee';

@Injectable({
  providedIn: 'root',
})
export class DmEmployeeService extends BaseService<DmEmployee> {
  readonly serviceUrl = 'api/Dm_Employee/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
