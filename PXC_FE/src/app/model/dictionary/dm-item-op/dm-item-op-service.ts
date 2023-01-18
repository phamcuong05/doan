import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmItemOp } from './dm-item-op';

@Injectable({
  providedIn: 'root',
})
export class DmItemOpService extends BaseService<DmItemOp> {
  readonly serviceUrl = 'api/Dm_Item_Op/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }

  /**
   * GetIssueReceiptList
   * @returns GetIssueReceiptList
   */
  public GetIssueReceiptList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetIssueReceiptList`
    );
  }
}
