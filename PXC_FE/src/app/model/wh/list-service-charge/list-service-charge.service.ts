import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListServiceCharge } from './list-service-charge';

@Injectable({
  providedIn: 'root'
})
export class ListServiceChargeService extends BaseService<ListServiceCharge>{
  readonly serviceUrl = 'api/ListServiceCharge/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
