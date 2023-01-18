import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmDepartment } from './dm-department';

@Injectable({
  providedIn: 'root',
})
export class DmDepartmentService extends BaseService<DmDepartment> {
  readonly serviceUrl = 'api/Dm_Department/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
