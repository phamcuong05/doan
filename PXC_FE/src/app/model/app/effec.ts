import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { LocalStorage } from 'src/app/common/local-storage';
import {clearTokenAction, restoreAuthAction} from '../login/action';
import { AuthService } from '../login/service';

@Injectable()
export class AppEffects {
  constructor(
    private actions: Actions,
    private router: Router,
    private authService: AuthService,
    private localStorage: LocalStorage
  ) {}


  loadAuthInfo$ = createEffect(() => {
    return of(true).pipe(
      map(() => {
        const auth = this.localStorage.AuthState();
        if (auth != null) {
          const targetDate = new Date();
          if (auth.expiredAt != null && auth.expiredAt >= targetDate) {
            return restoreAuthAction({
              token: auth.token || '',
              tokenType: auth.tokenType ||  'Bearer',
              expiredAt: new Date(auth.expiredAt),
            });
          }
        }
        return restoreAuthAction({
            token: '',
            tokenType: 'Bearer',
            expiredAt: new Date(1990,1,1)
          });
      })
    );
  });

  clearAuthInfo$ = createEffect(() => {
    return this.actions.pipe(
      ofType(clearTokenAction),
      map(() => {
        this.localStorage.SetAuthState(undefined);
        this.router.navigateByUrl('/login');
        return restoreAuthAction({
            token: '',
            tokenType: 'Bearer',
            expiredAt: new Date(1990,1,1)
          });
      })
    );
  });
}
