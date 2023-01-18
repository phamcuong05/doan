import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmJob } from './dm-job';

@Injectable({
  providedIn: 'root'
})
export class DmJobService extends BaseService<DmJob>{
  readonly serviceUrl= 'api/Dm_Job/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
   }
}
