import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmCapitalSource } from './dm-capital-source';

@Injectable({
  providedIn: 'root',
})
export class DmCapitalSourceService extends BaseService<DmCapitalSource> {
  readonly serviceUrl = 'api/Dm_Capital_Source/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
