import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListWH } from './list-wh';

@Injectable({
  providedIn: 'root'
})
export class ListWhService extends BaseService<ListWH>{

  readonly serviceUrl = 'api/ListWH/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
