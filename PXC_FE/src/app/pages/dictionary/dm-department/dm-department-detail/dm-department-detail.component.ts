import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmDepartmentService } from 'src/app/model/dictionary/dm-department/dm-department-service';

@Component({
  selector: 'dm-department-detail',
  templateUrl: './dm-department-detail.component.html',
  styleUrls: ['./dm-department-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmDepartmentDetailComponent),
    },
  ],
})
export class DmDepartmentDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmDepartmentResource.MyResource.DM_DEPARTMENT;
  }
  formGroup!: FormGroup;
  idField: string = 'DEPARTMENT_ID';
  nameField: string = 'DEPARTMENT_NAME';
  width: number = 400;
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      DEPARTMENT_ID: ['', [Validators.required, Validators.maxLength(20)]],
      DEPARTMENT_NAME: ['', [Validators.required, Validators.maxLength(50)]],
      ACTIVE: [true],
    });
  }

  constructor(
    private fb: FormBuilder,
    dmDepartmentService: DmDepartmentService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmDepartmentService, myInject);
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
}
