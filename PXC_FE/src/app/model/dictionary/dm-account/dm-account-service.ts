import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmAccount } from './dm-account';

@Injectable({
  providedIn: 'root',
})
export class DmAccountService extends BaseService<DmAccount> {
  readonly serviceUrl = 'api/Dm_Account/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
  public GetBalanceTypeList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetBalanceTypeList`
    );
  }

  public GetRateMethodList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetRateMethodList`
    );
  }
}
