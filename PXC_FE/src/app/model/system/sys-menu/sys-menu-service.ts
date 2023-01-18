import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from 'src/app/model/base/BaseService';
import { SysMenu } from './sys-menu';

@Injectable({
  providedIn: 'root',
})
export class SysMenuService extends BaseService<SysMenu> {
  readonly serviceUrl = 'api/Sys_Menu/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }

  /**
   * Các module trong hệ thống
   */
  public GetModuleList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/api/System/GetModuleList`
    );
  }

  /**
   * 
   * @returns các project trong hệ thống
   */
  public GetProjectList(): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/api/System/GetProjectList`
    );
  }
}
