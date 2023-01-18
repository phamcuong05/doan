import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmItem } from './dm-item';

@Injectable({
  providedIn: 'root',
})
export class DmItemService extends BaseService<DmItem> {
  readonly serviceUrl = 'api/Dm_Item/';

  constructor(_http: HttpServiceModule) {
    super(_http);
  }

  /* override loadData(): Promise<DmItem[]> {
    return this.http.get<DmItem[]>(`${this.rootAPI}/api/Dm/GetAllData`, {
      tableName: 'Dm_item',
    });
  } */
}
