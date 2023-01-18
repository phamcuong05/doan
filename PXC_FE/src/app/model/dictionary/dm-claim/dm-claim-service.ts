import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmClaim } from './dm-claim';

@Injectable({
  providedIn: 'root',
})
export class DmClaimService extends BaseService<DmClaim> {
  readonly serviceUrl = 'api/Dm_Claim/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
