import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListOrderService } from 'src/app/model/wh/list-order/list-order.service';

@Component({
  selector: 'list-order-detail',
  templateUrl: './list-order-detail.component.html',
  styleUrls: ['./list-order-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListOrderDetailComponent),
    },
  ],
})
export class ListOrderDetailComponent extends FTSDictBaseDetail {

  width: number = 980;

  constructor(
    private fb: FormBuilder,
    listOrderService: ListOrderService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listOrderService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'ORDER_ID';
  nameField : string = 'PACKAGE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListOrderResource.MyResource
      .LIST_ORDER;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ORDER_ID: [''],
      ORDER_DATE: [''],
      PACKAGE_CODE: [''],
      PACKAGE_NAME: [''],
      SERVICE_CHARGE_ID: [''],
      SERVICE_CHARGE_NAME: [''],
      TOTAL: [0],
      BUY_FEE: [0],
      SHIP_FEE: [0],
      CUSTOMER_NAME: [''],
      PHONE: [''],
      ADDRESS: [''],
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

}
