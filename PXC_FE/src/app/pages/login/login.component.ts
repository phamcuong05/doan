import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { Constaints } from 'src/app/common/constaints';
import { EnumLoadingState } from 'src/app/common/enum';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { loginAction } from 'src/app/model/login/action';
import { AuthState } from 'src/app/model/login/auth-state';
import {
  selectAuthError,
  selectAuthFeature,
} from 'src/app/model/login/selector';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild(TooltipDirective) tooltipDir!: TooltipDirective;
  loginForm!: FormGroup;
  errorCode!: string;
  LOGIN_ERROR_CODE = Constaints.LoginErrorCode;

  private onDestroy$ = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    private fb: FormBuilder,
    private store: Store,
    private maskLoad: MaskLoadService,
    private viewContainerRef: ViewContainerRef,
    private _ftsMain: FTSMain,
    private _ftsDialog: FtsDialogService
  ) {
    this.loginForm = this.fb.group({
      workingYear: this.fb.control(new Date().getFullYear(), [
        Validators.required,
      ]),
      userName: this.fb.control('', [Validators.required]),
      password: this.fb.control('', []),
    });
  }
  ngOnDestroy(): void {
    this.onDestroy$.next();
  }

  ngOnInit(): void {
    this.store
      .select(selectAuthFeature)
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((state: AuthState) => {
        switch (state.loading) {
          case EnumLoadingState.Loading:
            this.maskLoad.show(this.viewContainerRef);
            break;
          case EnumLoadingState.Complete:
            this.errorCode = state.error?.error_description || '';
            this.maskLoad.hide();
            break;
        }
      });

    const appLoadingEl = document.getElementById('appLoading');
    if (appLoadingEl) {
      appLoadingEl.hidden = true;
    }

    this.store
      .select(selectAuthError)
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((error) => {
        if (error) {
          if (typeof error == 'string') {
            try {
              error = JSON.parse(error);
            } catch (err) {}
          }
          let msgError = error;
          if (error.error_description) {
            msgError = error.error_description;
          }

          this._ftsDialog.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content: msgError,
          });
        }
      });
  }

  login() {
    if (this.validateFrom()) {
      const { userName, password, workingYear } = this.loginForm.value;
      this.store.dispatch(
        loginAction({
          userName,
          password,
          workingYear,
        })
      );
    }
  }

  private validateFrom(): boolean {
    let valid = true;
    const crtKeys = Object.keys(this.loginForm.controls);
    this.errorCode = '';
    if (crtKeys && crtKeys.length > 0) {
      let inputErrorFirst: any = null;
      for (const key in this.loginForm.controls) {
        if (
          Object.prototype.hasOwnProperty.call(this.loginForm.controls, key)
        ) {
          const control = this.loginForm.controls[key];
          const el = document.getElementById(key);
          const classList = el?.closest('.form-control')?.classList;
          if (control?.errors) {
            classList?.add('invalid');
            if (!inputErrorFirst) {
              inputErrorFirst = el;
            }
            if (!this.errorCode) {
              this.errorCode = this.getErrorCodeByControl(key, control?.errors);
            }
            valid = false;
          } else {
            classList?.remove('invalid');
          }
        }
      }

      if (inputErrorFirst) {
        inputErrorFirst.focus();
        this.tooltipDir?.hide();
        setTimeout(() => {
          this.tooltipDir?.toggle(inputErrorFirst, true);
        }, 100);
      }
    }
    return valid;
  }

  private getErrorCodeByControl(controlName: string, errors: any): string {
    if (errors) {
      for (const key in errors) {
        if (Object.prototype.hasOwnProperty.call(errors, key)) {
          if (key === 'required') {
            if (controlName === 'userName') {
              return this.LOGIN_ERROR_CODE.UserNameRequired;
            } else if (controlName === 'password') {
              return this.LOGIN_ERROR_CODE.PasswordRequired;
            }
          }
        }
      }
    }
    return '';
  }

  validateControl(name: string): void {
    const control = this.loginForm.controls[name];
    const el: any = document.getElementById(name);
    const classList = el?.closest('.form-control')?.classList;
    if (control?.errors) {
      classList?.add('invalid');
    } else {
      classList?.remove('invalid');
      this.tooltipDir?.toggle(el, false);
    }
  }
}
