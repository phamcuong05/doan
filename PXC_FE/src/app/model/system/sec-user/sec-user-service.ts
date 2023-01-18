import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { SecUser } from './sec-user';

@Injectable({
  providedIn: 'root',
})
export class SecUserService extends BaseService<SecUser> {
  readonly serviceUrl = 'api/Sec_User/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
