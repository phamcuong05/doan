import { Component, forwardRef } from '@angular/core';
import { MyReference } from 'src/app/common/MyReference';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { SecUserService } from 'src/app/model/system/sec-user/sec-user-service';

@Component({
  selector: 'sec-user-detail',
  templateUrl: './sec-user-detail.component.html',
  styleUrls: ['./sec-user-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => SecUserDetailComponent),
    },
  ],
})
export class SecUserDetailComponent extends FTSDictBaseDetail {
  width: number = 600;

  constructor(
    private fb: FormBuilder,
    public secUserService: SecUserService,
    myInject: FtsDictBaseDetailInject,
  ) {
    super(secUserService, myInject);
  }

  /*   state$: {
      SecUseGroupDatas: SecUserGroup[];
    } = {
      SecUseGroupDatas: [],
    }; */

  formGroup!: FormGroup;
  idField = 'USER_ID';
  nameField = 'USER_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.SecUserResource.MyResource.SecUser;
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      USER_ID: ['', [Validators.required]],
      USER_NAME: ['', [Validators.required]],
      USER_GROUP_ID: ['', [Validators.required]],
      USER_PASSWORD: [''],
      EMPLOYEE_ID: [''],
      EMPLOYEE_NAME: [''],
      ORGANIZATION_ID: [''],
      ORGANIZATION_NAME: [''],
      USER_KEY: [''],
      ACTIVE: [true],
      LOGIN_DATE: [Date.now()],
      QUANTITY_INVALID: [0],
    });
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  OrganizationId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ORGANIZATION_NAME'].setValue(
      state?.item?.ORGANIZATION_NAME || ''
    );
  }

  employeeId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['EMPLOYEE_NAME'].setValue(
      state?.item?.EMPLOYEE_NAME || ''
    );
  }

}
