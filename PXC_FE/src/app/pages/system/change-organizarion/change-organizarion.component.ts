import { Component, forwardRef, OnInit, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FTSMain } from 'src/app/base/ftsmain';
import { MyReference } from 'src/app/common/MyReference';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { ChangeOrganizarionService } from 'src/app/model/system/change-organizarion/change-password-service';

@Component({
  selector: 'change-organizarion',
  templateUrl: './change-organizarion.component.html',
  styleUrls: ['./change-organizarion.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => ChangeOrganizarionComponent),
    },
  ],
})
export class ChangeOrganizarionComponent implements OnInit {
  frmChangeOrganization: FormGroup;

  constructor(
    private FTSMain: FTSMain,
    fb: FormBuilder,
    private changeOrganizarionService: ChangeOrganizarionService,
    private viewContainerRef: ViewContainerRef,
    private maskLoadService: MaskLoadService,
    private ftsDialogService: FtsDialogService,
    private router: Router
  ) {
    this.frmChangeOrganization = fb.group({
      ORGANIZATION_ID: ['', Validators.required],
      ORGANIZATION_NAME: [''],
    });
  }

  title: string = 'Đổi đơn vị làm việc';

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.frmChangeOrganization.controls['ORGANIZATION_ID'].setValue(
      this.FTSMain.UserInfo.OrganizationID
    );
    this.frmChangeOrganization.controls['ORGANIZATION_NAME'].setValue(
      this.FTSMain.UserInfo.OrganizationName
    );
  }

  CheckBusinessRules() {
    let organizationId: string =
      this.frmChangeOrganization.get('ORGANIZATION_ID')?.value;

    if (organizationId == '') {
      throw 'Bạn chưa chọn đơn vị làm việc...';
    }
  }

  /**
   * Khi thay đổi đơn vị làm việc
   * @param e
   */
  changeOrganization(state: { item: any; form: FormGroup }): void {
    this.frmChangeOrganization.controls['ORGANIZATION_NAME'].setValue(
      state?.item?.ORGANIZATION_NAME || ''
    );
  }

  btnChangeOrganization_Click() {
    try {
      this.CheckBusinessRules();
      let organizationID =
        this.frmChangeOrganization.get('ORGANIZATION_ID')?.value;
      const data = {
        ORGANIZATION_ID: organizationID,
      };

      this.maskLoadService.show(this.viewContainerRef);
      this.changeOrganizarionService
        .changeOrganizarion(data)
        .then(() => {
          this.maskLoadService.hide();
          this.FTSMain.UserInfo.OrganizationID = organizationID;
          window.location.href = '/dashboard';
          //this.router.navigateByUrl('/dashboard');

          /*  this.ftsDialogService.alert.show({
             content: 'Đổi đơn vị làm việc thành công <br> Bạn cần đăng nhập lại để chuyển sang đơn vị làm việc mới.',
             icon: 'notification',
           }); */
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
