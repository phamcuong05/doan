import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmOrganizarion } from './dm-organizarion';

@Injectable({
  providedIn: 'root',
})
export class DmOrganizarionService extends BaseService<DmOrganizarion> {
  readonly serviceUrl = 'api/Dm_Organizarion/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
