import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmPeriod } from './dm-period';

@Injectable({
  providedIn: 'root',
})
export class DmPeriodService extends BaseService<DmPeriod> {
  readonly serviceUrl = 'api/Dm_Period/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }

  public GetPeriodTypeList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetPeriodTypeList`
    );
  }
}
