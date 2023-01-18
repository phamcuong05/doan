import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmProvince } from './dm-province';

@Injectable({
  providedIn: 'root',
})
export class DmProvinceService extends BaseService<DmProvince> {
  readonly serviceUrl = 'api/Dm_Province/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
