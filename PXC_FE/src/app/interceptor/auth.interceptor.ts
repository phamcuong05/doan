import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, throwError } from 'rxjs';
import { catchError, mergeMap, take } from 'rxjs/operators';
import { FTSMain } from '../base/ftsmain';
import { LocalStorage } from '../common/local-storage';
import { ResourceManager } from '../common/resource-manager';
import { FtsDialogService } from '../controls/fts-dialog/fts-dialog.service';
import { clearTokenAction } from '../model/login/action';
import { selectAuthState } from '../model/login/selector';

@Injectable()
/**
 * Interceptor xử lý gán token vào các  request gọi API
 */
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private FTSMain: FTSMain,
    private router: Router,
    private store: Store,
    private localStorage: LocalStorage,
    private ftsDialog: FtsDialogService,
    private resourceManager: ResourceManager
  ) {}

  showDialogError: boolean = true;

  private handleClearToken() {
    if (this.localStorage.LoadedGetStarted) {
      if (this.showDialogError) {
        this.showDialogError = false;
        this.ftsDialog.confirm
          .show({
            icon: 'warning',
            maxWidth: 300,
            content:
              this.resourceManager.CommonResource.MyResource
                .MessageSessionTimeout,
          })
          .pipe(take(1))
          .subscribe((state) => {
            this.showDialogError = true;
            if (state === 'yes') {
              this.store.dispatch(clearTokenAction());
            }
          });
      }
    } else {
      this.store.dispatch(clearTokenAction());
    }
  }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    //Nếu không phải gọi để lấy token hoặc là không phải lấy file tĩnh (json) thì check và gán token
    if (!request.url.includes('token') && !request.url.endsWith('.json')) {
      if (this.localStorage.AuthState()?.token) {
        return this.store.select(selectAuthState).pipe(
          take(1),
          mergeMap((state) => {
            if (
              state &&
              state.token &&
              state.expiredAt &&
              state.expiredAt > new Date()
            ) {
              //Gán token vào header
              const cloned = request.clone({
                setHeaders: {
                  Authorization: `${state.tokenType} ${state.token}`,
                  OrganizationID: this.FTSMain.UserInfo.OrganizationID,
                  OrganizationName: this.FTSMain.UserInfo.OrganizationID,
                },
              });

              return next.handle(cloned).pipe(
                catchError((err: any) => {
                  if (err && err.status == 401) {
                    this.handleClearToken();
                    //cancel request
                    //không bắn ra nội dung lỗi để không phải xử lý lỗi 401 bên ngoài nữa
                    return throwError(undefined);
                  }
                  return throwError(err);
                })
              );
            }
            this.store.dispatch(clearTokenAction());
            return next.handle(request);
          })
        );
      } else {
        this.handleClearToken();
        //cancel request
        //không bắn ra nội dung lỗi để không phải xử lý lỗi 401 bên ngoài nữa
        return throwError(undefined);
      }
    }
    return next.handle(request);
  }
}
