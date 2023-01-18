import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmWarehouseService } from 'src/app/model/dictionary/dm-warehouse/dm-warehouse-service';

@Component({
  selector: 'dm-warehouse-detail',
  templateUrl: './dm-warehouse-detail.component.html',
  styleUrls: ['./dm-warehouse-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmWarehouseDetailComponent),
    },
  ],
})
export class DmWarehouseDetailComponent extends FTSDictBaseDetail {
  width: number = 550;
  constructor(
    private fb: FormBuilder,
    dmWarehouseService: DmWarehouseService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmWarehouseService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'WAREHOUSE_ID';
  nameField = 'WAREHOUSE_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      WAREHOUSE_ID: ['', [Validators.required]],
      WAREHOUSE_NAME: ['', [Validators.required]],
      DEPARTMENT_ID: [''],
      DEPARTMENT_NAME: [''],
      IS_USE_WAREHOUSE: [true],
      ACTIVE: [true],
      USER_ID: [''],
    });
  }

  departmentId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['DEPARTMENT_NAME'].setValue(
      state?.item?.DEPARTMENT_NAME || ''
    );
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
