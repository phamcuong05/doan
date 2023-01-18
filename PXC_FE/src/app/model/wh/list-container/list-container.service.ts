import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListContainer } from './list-container';

@Injectable({
  providedIn: 'root'
})
export class ListContainerService extends BaseService<ListContainer>{

  readonly serviceUrl = 'api/ListContainer/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
