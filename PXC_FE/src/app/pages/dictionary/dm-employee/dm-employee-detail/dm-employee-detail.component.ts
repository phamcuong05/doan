import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmEmployeeService } from 'src/app/model/dictionary/dm-employee/dm-employee-service';

@Component({
  selector: 'dm-employee-detail',
  templateUrl: './dm-employee-detail.component.html',
  styleUrls: ['./dm-employee-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmEmployeeDetailComponent),
    },
  ],
})
export class DmEmployeeDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmEmployeeResource.MyResource
      .DM_EMPLOYEE;
  }
  formGroup!: FormGroup;
  idField: string = 'EMPLOYEE_ID';
  nameField: string = 'EMPLOYEE_NAME';
  width: number = 500;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      EMPLOYEE_ID: ['', [Validators.required, Validators.maxLength(20)]],
      EMPLOYEE_NAME: ['', [Validators.required, Validators.maxLength(100)]],
      DEPARTMENT_ID: ['', [Validators.required, Validators.maxLength(20)]],
      DEPARTMENT_NAME: [''],
      ADDRESS: ['', [Validators.maxLength(200)]],
      PHONE: ['', [Validators.maxLength(20)]],
      EMAIL: ['', [Validators.maxLength(100), Validators.email]],
      IDENTITY_NO: ['', [Validators.maxLength(30)]],
      ACTIVE: [true],
    });
  }

  constructor(
    private fb: FormBuilder,
    dmEmployeeService: DmEmployeeService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmEmployeeService, myInject);
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

  departmentID_selectionChange(state: { item: any; form: FormGroup }) {
    this.formGroup.controls['DEPARTMENT_NAME'].setValue(
      state?.item?.DEPARTMENT_NAME || ''
    );
  }
}
