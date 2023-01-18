import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListWH } from '../list-wh/list-wh';

@Injectable({
  providedIn: 'root'
})
export class ListDeliveryService extends BaseService<ListWH>{

  readonly serviceUrl = 'api/ListDelivery/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
