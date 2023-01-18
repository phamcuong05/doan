import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmItemClass } from './dm-item-class';

@Injectable({
  providedIn: 'root',
})
export class DmItemClassService extends BaseService<DmItemClass> {
  readonly serviceUrl = 'api/Dm_Item_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
