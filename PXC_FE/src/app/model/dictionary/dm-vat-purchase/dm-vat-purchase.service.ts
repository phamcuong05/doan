import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmVatPurchase } from './dm-vat-purchase';

@Injectable({
  providedIn: 'root'
})
export class DmVatPurchaseService extends BaseService<DmVatPurchase>{
  readonly serviceUrl = 'api/Dm_Vat_Purchase/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
   }
}
