import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
/**
 * Xử lý gọi request lên server
 */
export class HttpServiceModule {
  /**
   * ctor
   */
  constructor(private http: HttpClient, private route: Router) {}

  /**
   * Gọi 1 request get lên server
   * @param uri kiểu object {}
   * @returns Promise<T>
   */
  public get<T>(uri: string, param?: any, headers?: HttpHeaders) {
    let params: HttpParams | undefined = undefined;
    if (param) {
      params = new HttpParams({
        fromObject: param,
      });
    }

    return new Promise<T>((resolve, reject) => {
      this.http
        .get<T>(uri, { params: params, headers: headers })
        .pipe(take(1))
        .subscribe(
          (datas: T) => {
            resolve(datas);
          },
          (error) => {
            reject(error);
          }
        );
    });
  }

  /**
   * Gọi 1 request post lên server
   * @param uri
   * @param body
   * @returns
   */
  public post<T>(uri: string, body?: any, headers?: HttpHeaders) {
    return new Promise<T>((resolve, reject) => {
      this.http
        .post<T>(uri, body, { headers: headers })
        .pipe(take(1))
        .subscribe(
          (datas: T) => {
            resolve(datas);
          },
          (error) => {
            reject(error);
          }
        );
    });
  }

  /**
   * Gọi 1 request put lên server
   * @param uri
   * @param body
   * @returns
   */
  public put<T>(uri: string, body?: any, headers?: HttpHeaders) {
    return new Promise<T>((resolve, reject) => {
      this.http
        .put<T>(uri, body, { headers: headers })
        .pipe(take(1))
        .subscribe(
          (datas: T) => {
            resolve(datas);
          },
          (error) => {
            reject(error);
          }
        );
    });
  }

  /**
   * Gọi 1 request delete lên server
   * @param uri
   * @param body
   * @returns
   */
  public delete<T>(uri: string, body?: any, headers?: HttpHeaders) {
    return new Promise<T>((resolve, reject) => {
      this.http
        .delete<T>(uri, { headers: headers, body: body })
        .pipe(take(1))
        .subscribe(
          (datas: T) => {
            resolve(datas);
          },
          (error) => {
            reject(error);
          }
        );
    });
  }
}
