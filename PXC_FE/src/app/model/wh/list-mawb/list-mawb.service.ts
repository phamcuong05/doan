import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListMawb } from './list-mawb';

@Injectable({
  providedIn: 'root'
})
export class ListMawbService extends BaseService<ListMawb>{

  readonly serviceUrl = 'api/ListMawb/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
