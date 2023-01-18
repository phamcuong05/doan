import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmSecurityService } from 'src/app/model/dictionary/dm-security/dm-security-service';

@Component({
  selector: 'dm-security-detail',
  templateUrl: './dm-security-detail.component.html',
  styleUrls: ['./dm-security-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecurityDetailComponent),
    },
  ],
})
export class DmSecurityDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmSecurityResource.MyResource
      .DM_SECURITY;
  }
  formGroup!: FormGroup;
  idField: string = 'SECURITY_ID';
  nameField: string = 'SECURITY_NAME';
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      SECURITY_ID: ['', [Validators.required]],
      SECURITY_NAME: ['', [Validators.required]],
      SECURITY_CLASS_ID: [''],
      SECURITY_CLASS_NAME: [''],
      BOOK_UNIT_PRICE_ORIG: [0],
      CURRENCY_ID: [this.myInject.FTSMain.MainCurrency],
      PR_DETAIL_ID: [''],
      PR_DETAIL_NAME: [''],
      PERIOD_ID: [''],
      PERIOD_NAME: [''],
      ISSUE_DATE: [new Date()],
      SHORT_TERM_COST_ACCOUNT_ID: [''],
      SHORT_TERM_PROFIT_ACCOUNT_ID: [''],
      SHORT_TERM_LOSS_ACCOUNT_ID: [''],
      SHORT_TERM_RESERVE_ACCOUNT_ID: [''],
      LONG_TERM_COST_ACCOUNT_ID: [''],
      LONG_TERM_PROFIT_ACCOUNT_ID: [''],
      LONG_TERM_LOSS_ACCOUNT_ID: [''],
      LONG_TERM_RESERVE_ACCOUNT_ID: [''],
      MATURITY_DATE: [new Date()],

      ACTIVE: [true],
      USER_ID: [''],
    });
  }
  width: number = 650;

  constructor(
    private fb: FormBuilder,
    dmSecurityService: DmSecurityService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmSecurityService, myInject);
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

  prDetail_onSelectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PR_DETAIL_NAME'].setValue(
      state?.item?.PR_DETAIL_NAME || ''
    );
  }

  securityClass_onSelectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['SECURITY_CLASS_NAME'].setValue(
      state?.item?.SECURITY_CLASS_NAME || ''
    );
  }

  period_onSelectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PERIOD_NAME'].setValue(
      state?.item?.PERIOD_NAME || ''
    );
  }

}
