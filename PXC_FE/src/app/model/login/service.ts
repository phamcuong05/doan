import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { environment } from 'src/environments/environment';

export interface LoginResponse {
  access_token: string;
  token_type: string;
  expires_in: number;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private rootAPI: string = environment.defaultAPI;

  constructor(private http: HttpServiceModule) {}

  login(username: string, password: string, workingYear: number): Promise<LoginResponse> {
    const body = new HttpParams({
      fromObject: {
        grant_type: 'password',
        username,
        password,
        workingYear
      },
    });
    return this.http.post<LoginResponse>(
      `${this.rootAPI}/token`,
      body.toString()
    );
  }
}
