import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmVatTax } from './dm-vat-tax';

@Injectable({
  providedIn: 'root',
})
export class DmVatTaxService extends BaseService<DmVatTax> {
  readonly serviceUrl = 'api/Dm_Vat_Tax/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
