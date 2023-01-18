import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { take } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { clearTokenAction } from 'src/app/model/login/action';
import { ChangePasswordService } from 'src/app/model/system/change-password/change-password-service';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {
  frmChangePassword: FormGroup;

  constructor(
    private FTSMain: FTSMain,
    private store: Store,
    fb: FormBuilder,
    private changePasswordService: ChangePasswordService,
    private viewContainerRef: ViewContainerRef,
    private maskLoadService: MaskLoadService,
    private ftsDialogService: FtsDialogService,
    private router: Router
  ) {
    this.frmChangePassword = fb.group({
      oldPwd: [''],
      newPwd: ['', Validators.required],
      confirmPwd: ['', Validators.required],
    });
  }

  title: string = 'Đổi mật khẩu';

  ngOnInit(): void {}

  CheckPasword(password: string) {
    var strength = 0;
    if (password.match(/[a-z]+/)) {
      strength += 1;
    }
    if (password.match(/[A-Z]+/)) {
      strength += 1;
    }
    if (password.match(/[0-9]+/)) {
      strength += 1;
    }
    if (password.match(/[$@#&!]+/)) {
      strength += 1;
    }
    if (password.length >= 6) {
      strength += 1;
    }

    if (strength < 5) {
      return false;
    }
    return true;
  }

  CheckBusinessRules() {
    let oldPwd: string = this.frmChangePassword.get('oldPwd')?.value;
    let newPwd: string = this.frmChangePassword.get('newPwd')?.value;
    let confirmPwd: string = this.frmChangePassword.get('confirmPwd')?.value;

    /* if (this.oldPwd?.value == '' || this.oldPwd?.value.length < 6) {
    throw 'Mật khẩu cũ không hợp lệ';
    } */

    if (oldPwd == newPwd) {
      throw 'Mật khẩu mới không hợp lệ, mật khẩu mới đang trùng với mật khẩu cũ';
    }

    if (newPwd == '' || this.CheckPasword(newPwd) == false) {
      throw 'Mật khẩu mới không hợp lệ';
    }

    if (newPwd !== confirmPwd) {
      throw 'Confirm Pwd not match new Pwd';
    }
  }

  ChangePassword() {
    try {
      this.CheckBusinessRules();
      const data = {
        oldPwd: this.frmChangePassword.get('oldPwd')?.value,
        newPwd: this.frmChangePassword.get('newPwd')?.value,
      };

      this.maskLoadService.show(this.viewContainerRef);
      this.changePasswordService
        .changePassword(data)
        .then(() => {
          this.maskLoadService.hide();
          /*    this.ftsDialogService.alert.show({
               content: 'Đổi mật khẩu thành công',
               icon: 'notification',
             }); */
          this.ftsDialogService.confirm
            .show({
              icon: 'question',
              content:
                'Đổi mật khẩu thành công.<br>Bạn muốn đăng xuất để đăng nhập lại không?',
            })
            .pipe(take(1))
            .subscribe((state) => {
              if (state == 'yes') {
                this.store.dispatch(clearTokenAction());
              } else {
                this.router.navigateByUrl('/dashboard');
              }
            });
        })
        .catch((err) => {
          this.maskLoadService.hide();
          this.ftsDialogService.alert.show({
            content: this.FTSMain.ExceptionManager.processException(err),
            icon: 'warning',
          });
        });
    } catch (err) {
      this.ftsDialogService.alert.show({
        content: err as string,
        icon: 'warning',
      });
    }
  }
}
