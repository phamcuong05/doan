import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SystemService {
  private rootAPI: string = environment.defaultAPI;
  constructor(private _http: HttpServiceModule) {}

  /**
   * Lấy dữ liệu ban đầu.
   * Created by: MTLUC - 09/12/2021
   */
  getStart(): Promise<any> {
    return this._http.get<any>(`${this.rootAPI}/api/Login/GetStart`);
  }
}
