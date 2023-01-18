import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmPrDetailClass } from './dm-pr-detail-class';

@Injectable({
  providedIn: 'root',
})
export class DmPrDetailClassService extends BaseService<DmPrDetailClass> {
  readonly serviceUrl = 'api/Dm_Pr_Detail_Class/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
  //  public LoadDataType<K>(typeName: string, param?: any): Promise<K[]> {
  //   return this.http.get<K[]>(`${this.rootAPI}/api/Dm_Pr_Detail_Class/GetType`, {
  //     ...param,
  //     typename: typeName,
  //   });
  //}
  public LoadDataPrDetailType(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetPrDetailTypeList`
    );
  }
}
