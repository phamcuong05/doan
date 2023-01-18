import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmCashbankLimitService } from 'src/app/model/dictionary/dm-cashbank-limit/dm-cashbank-limit.service';

@Component({
  selector: 'dm-cashbank-limit-detail',
  templateUrl: './dm-cashbank-limit-detail.component.html',
  styleUrls: ['./dm-cashbank-limit-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmCashbankLimitDetailComponent),
    },
  ],
})
export class DmCashbankLimitDetailComponent extends FTSDictBaseDetail {
  width: number = 700;

  constructor(
    private fb: FormBuilder,
    cashbankLimitService: DmCashbankLimitService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(cashbankLimitService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'PR_KEY';
  nameField : string = 'ORGANIZATION_ID';
  get formTitle(): string {
    return this.myInject.resourceManager.DmCashbankLimitResource.MyResource
      .DM_CASHBANK_LIMIT;
  }

  override getNewRecord(): object {
    return { ...this.defaultRecord, PR_KEY: commonFunction.newGuid() };
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ORGANIZATION_ID: ['', [Validators.required, Validators.maxLength(20)]],
      ORGANIZATION_NAME: [''],
      ACCOUNT_ID: ['', [Validators.required, Validators.maxLength(20)]],
      ACCOUNT_NAME: [''],
      BANK_ID: [''],
      BANK_NAME: [''],
      VALID_DATE: [''],
      LIMIT: [0],
      NOTES: [''],
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

  /**
   * Chọn tài khoản
   * @param state
   */
  accountId_SelectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ACCOUNT_NAME'].setValue(state?.item?.ACCOUNT_NAME || '');
  }

  /**
   * Chọn ngân hàng
   * @param state
   */
  bankId_SelectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['BANK_NAME'].setValue(state?.item?.BANK_NAME || '');
  }

}
