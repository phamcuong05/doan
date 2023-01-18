import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmDistrict } from './dm-district';

@Injectable({
  providedIn: 'root',
})
export class DmDistrictService extends BaseService<DmDistrict> {
  readonly serviceUrl = 'api/Dm_District/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
