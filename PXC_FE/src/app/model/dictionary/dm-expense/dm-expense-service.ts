import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmExpense } from './dm-expense';

@Injectable({ providedIn: 'root' })
export class DmExpenseService extends BaseService<DmExpense> {
  readonly serviceUrl = 'api/Dm_Expense/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
