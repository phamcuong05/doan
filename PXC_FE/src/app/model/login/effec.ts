import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { exhaustMap, tap } from 'rxjs/operators';
import { LocalStorage } from 'src/app/common/local-storage';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { loginAction, loginActionFail, loginActionSuccess } from './action';
import { AuthState } from './auth-state';
import { AuthService } from './service';

@Injectable()
export class AuthEffects {
  constructor(
    private actions: Actions,
    private authService: AuthService,
    private router: Router,
    private localStorage: LocalStorage,
    private ftsDialogService: FtsDialogService
  ) {}

  loginEffect$ = createEffect(() => {
    return this.actions.pipe(
      ofType(loginAction),
      exhaustMap((action) => {
        return this.authService
          .login(action.userName, action.password, action.workingYear)
          .then((response) => {
            const expiredAt = new Date();
            expiredAt.setSeconds(expiredAt.getSeconds() + response.expires_in);
            const payload = {
              token: response.access_token,
              tokenType: response.token_type,
              expiredAt,
            };
            return loginActionSuccess(payload);
          })
          .catch((err: any) => {
            return loginActionFail(err);
          });
      })
    );
  });

  loginSuccessEffect$ = createEffect(
    () => {
      return this.actions.pipe(
        ofType(loginActionSuccess),
        tap((action) => {
          this.localStorage.SetAuthState({
            token: action.token,
            tokenType: action.tokenType,
            expiredAt: action.expiredAt,
          } as AuthState);
          void this.router.navigateByUrl('/dashboard');
        })
      );
    },
    {
      dispatch: false,
    }
  );
}
