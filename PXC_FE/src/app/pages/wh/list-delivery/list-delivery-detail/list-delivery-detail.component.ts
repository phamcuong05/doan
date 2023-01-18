import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListDeliveryService } from 'src/app/model/wh/list-delivery/list-delivery.service';

@Component({
  selector: 'list-delivery-detail',
  templateUrl: './list-delivery-detail.component.html',
  styleUrls: ['./list-delivery-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListDeliveryDetailComponent),
    },
  ],
})
export class ListDeliveryDetailComponent extends FTSDictBaseDetail {

  width: number = 700;

  constructor(
    private fb: FormBuilder,
    listDeliveryService: ListDeliveryService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listDeliveryService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'WARE_HOUSE_ID';
  nameField : string = 'WARE_HOUSE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListWHResource.MyResource
      .LIST_DELIVERY;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      WARE_HOUSE_ID: [''],
      WARE_HOUSE_NAME: ['PCS Mỹ Đình'],
      FROM_DATE: [''],
      TOTAL_ORDER: [''],
      WEIGHT: [0],
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
