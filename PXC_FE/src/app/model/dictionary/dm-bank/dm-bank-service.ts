import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmBank } from './dm-bank';

@Injectable({
  providedIn: 'root',
})
export class DmBankService extends BaseService<DmBank> {
  readonly serviceUrl = 'api/Dm_Bank/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
