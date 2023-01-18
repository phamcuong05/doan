import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmWarehouse } from './dm-warehouse';

@Injectable({
  providedIn: 'root',
})
export class DmWarehouseService extends BaseService<DmWarehouse> {
  readonly serviceUrl = 'api/Dm_Warehouse/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
