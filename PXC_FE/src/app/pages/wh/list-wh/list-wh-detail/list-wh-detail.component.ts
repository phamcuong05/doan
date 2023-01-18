import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListOrderService } from 'src/app/model/wh/list-order/list-order.service';
import { ListWhService } from 'src/app/model/wh/list-wh/list-wh.service';

@Component({
  selector: 'list-wh-detail',
  templateUrl: './list-wh-detail.component.html',
  styleUrls: ['./list-wh-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListWhDetailComponent),
    },
  ],
})
export class ListWhDetailComponent extends FTSDictBaseDetail {

  width: number = 700;

  constructor(
    private fb: FormBuilder,
    listWhService: ListWhService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listWhService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'WARE_HOUSE_ID';
  nameField : string = 'WARE_HOUSE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListWHResource.MyResource
      .LIST_WH;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      WARE_HOUSE_ID: [''],
      WARE_HOUSE_NAME: ['PCS Mỹ Đình'],
      MAWB_ID: [''],
      MAWB_NAME: [''],
      TO_DATE: [''],
      TOTAL_ORDER: [''],
      WEIGHT: [0],
      CONTAINER_ID: [''],
      CONTAINER_NAME: [''],
      ORGANIZATION_ID: [''],
      ORGANIZATION_NAME: [''],
      NOTE: [''],
      USER_ID: [''],
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

  organizationId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ORGANIZATION_NAME'].setValue(
      state?.item?.ORGANIZATION_NAME || ''
    );
  }
}
