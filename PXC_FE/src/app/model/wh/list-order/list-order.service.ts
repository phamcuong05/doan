import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListOrder } from './list-order';

@Injectable({
  providedIn: 'root'
})
export class ListOrderService extends BaseService<ListOrder>{
  readonly serviceUrl = 'api/ListOrder/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
