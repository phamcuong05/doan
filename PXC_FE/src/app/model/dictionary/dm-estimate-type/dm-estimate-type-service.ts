import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmEstimateType } from './dm-estimate-type';

@Injectable({
  providedIn: 'root',
})
export class DmEstimateTypeService extends BaseService<DmEstimateType> {
  readonly serviceUrl = 'api/Dm_Estimate_Type/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
