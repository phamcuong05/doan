import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmExpenseClass } from './dm-expense-class';

@Injectable({
  providedIn: 'root',
})
export class DmExpenseClassService extends BaseService<DmExpenseClass> {
  readonly serviceUrl = 'api/Dm_Expense_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}