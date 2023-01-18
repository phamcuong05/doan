import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { ListPackage } from './list-package';

@Injectable({
  providedIn: 'root'
})
export class ListPackageService extends BaseService<ListPackage>{

  readonly serviceUrl = 'api/ListPackage/';
  http: HttpServiceModule;
  constructor(_http: HttpServiceModule) { 
    super(_http);
    this.http = _http;
  }
}
