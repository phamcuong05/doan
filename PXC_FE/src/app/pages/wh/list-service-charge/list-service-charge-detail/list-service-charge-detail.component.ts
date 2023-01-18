import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListServiceChargeService } from 'src/app/model/wh/list-service-charge/list-service-charge.service';

@Component({
  selector: 'list-service-charge-detail',
  templateUrl: './list-service-charge-detail.component.html',
  styleUrls: ['./list-service-charge-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListServiceChargeDetailComponent),
    },
  ],
})
export class ListServiceChargeDetailComponent extends FTSDictBaseDetail {

  width: number = 700;

  constructor(
    private fb: FormBuilder,
    listServiceChargeService: ListServiceChargeService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listServiceChargeService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'SERVICE_CHARGE_ID';
  nameField : string = 'SERVICE_CHARGE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListServiceChargeResource.MyResource
      .LIST_SERVICE_CHARGE;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      SERVICE_CHARGE_ID: [''],
      SERVICE_CHARGE_NAME: [''],
      DESCRIPTION: [''],
      SHIP_FEE: [0],
      USER_ID: [''],
      REGION: [''],
      CREATE_DATE: [''],
      MODIFY_DATE: [''],
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
