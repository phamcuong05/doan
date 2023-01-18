import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChangeOrganizarionService {
  readonly rootAPI: string = environment.defaultAPI;
  readonly serviceUrl = 'api/Dm_Organizarion/';
  http: HttpServiceModule;

  constructor(_http: HttpServiceModule) {
    this.http = _http;
  }

  changeOrganizarion(data: any): Promise<any> {
    return this.http.post<any>(
      `${this.rootAPI}/${this.serviceUrl}ChangeOrganizarion`,
      data
    );
  }
}
