import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmVatTaxService } from 'src/app/model/dictionary/dm-vat-tax/dm-vat-tax-service';

@Component({
  selector: 'dm-vat-tax-detail',
  templateUrl: './dm-vat-tax-detail.component.html',
  styleUrls: ['./dm-vat-tax-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmVatTaxDetailComponent),
    },
  ],
})
export class DmVatTaxDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmVatTaxService: DmVatTaxService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmVatTaxService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'VAT_TAX_ID';
  nameField = 'VAT_TAX_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      VAT_TAX_ID: ['', [Validators.required]],
      VAT_TAX_NAME: ['', [Validators.required]],
      VAT_TAX_RATE: [0, [Validators.min(0)]],
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
