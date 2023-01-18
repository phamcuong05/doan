import { Component, forwardRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FtsException } from 'src/app/base/fts-exception';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmVatPurchaseService } from 'src/app/model/dictionary/dm-vat-purchase/dm-vat-purchase.service';

@Component({
  selector: 'dm-vat-purchase-detail',
  templateUrl: './dm-vat-purchase-detail.component.html',
  styleUrls: ['./dm-vat-purchase-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmVatPurchaseDetailComponent),
    },
  ],
})
export class DmVatPurchaseDetailComponent extends FTSDictBaseDetail {
  width: number = 500;
  constructor(
    private fb: FormBuilder,
    vatPurchaseService: DmVatPurchaseService,
    myInject: FtsDictBaseDetailInject
  ) { 
    super(vatPurchaseService, myInject)

  }

  formGroup!: FormGroup;
  idField = 'VAT_PURCHASE_ID';
  nameField = 'VAT_PURCHASE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmVatPurchaseResource.MyResource
      .DM_VAT_PURCHASE;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      VAT_PURCHASE_ID: ['', [Validators.required]],
      VAT_PURCHASE_NAME: ['', [Validators.required]],
      ACTIVE: [true],
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
