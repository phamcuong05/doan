import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmUnit } from './dm-unit';

@Injectable({
  providedIn: 'root',
})
export class DmUnitService extends BaseService<DmUnit> {
  readonly serviceUrl = 'api/Dm_Unit/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
