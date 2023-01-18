import { Component, forwardRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { BalanceType } from 'src/app/model/dictionary/dm-account/balance-type';
import { DmAccountService } from 'src/app/model/dictionary/dm-account/dm-account-service';
import { RateMethod } from 'src/app/model/dictionary/dm-account/rate-method';
import { DmAccountSelectorComponent } from '../dm-account-selector/dm-account-selector.component';

@Component({
  selector: 'dm-account-detail',
  templateUrl: './dm-account-detail.component.html',
  styleUrls: ['./dm-account-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    DmAccountService,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmAccountDetailComponent),
    },
  ],
})

/**
 *
 */
export class DmAccountDetailComponent extends FTSDictBaseDetail {
  width: number = 700;
  @ViewChild('accountSelector')
  accountSelectorComponent!: DmAccountSelectorComponent;
  constructor(
    private fb: FormBuilder,
    dmAccountService: DmAccountService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmAccountService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'ACCOUNT_ID';
  nameField = 'ACCOUNT_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }

  state$: { BalanceTypeDatas: BalanceType[]; RateMethodDatas: RateMethod[]; }
    = { BalanceTypeDatas: [], RateMethodDatas: [] };


  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ACCOUNT_ID: ['', [Validators.required]],
      ACCOUNT_NAME: ['', [Validators.required]],
      BALANCE_TYPE: '',
      RATE_METHOD: '',
      CURRENCY_ID: '',
      CURRENCY_NAME: '',
      ACTIVE: 0,
      IS_OOB: 0,
      IS_PR_DETAIL: 0,
      IS_EXPENSE: 0,
      IS_JOB: 0,
      IS_BANK: 0,
      IS_EMPLOYEE: 0,
      IS_DEPARTMENT: 0,
      IS_CONTRACT: 0,
      IS_INSURANCE_SOURCE: 0,
      IS_CAPITAL_SOURCE: 0,
      IS_REINSURANCE_SOURCE: 0,
      IS_AGENT: 0,
      IS_ITEM: 0,
      IS_VAT: 0,
      IS_PARENT: 0,
      PARENT_ACCOUNT_NAME: '',
      PARENT_ACCOUNT_ID: '',
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
    this.accountSelectorComponent.isParent = true;
  }

  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      (<DmAccountService>this.myService).GetBalanceTypeList(), (<DmAccountService>this.myService).GetRateMethodList()
    ])
      .then(([BalanceTypeDatas, RateMethodDatas]) => {
        this.state$ = { ...this.state$, BalanceTypeDatas, RateMethodDatas };
        this.myInject.detailStore.loadDataComplete(undefined);
      })
      .catch((err) => {
        this.myInject.detailStore.loadDataComplete(err);
        this.myInject.ftsDialogService.alert.show({
          content: this.myInject.FTSMain.ExceptionManager.processException(err),
          icon: 'warning',
        });
      });
  }


  currency_id_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['CURRENCY_NAME'].setValue(state?.item?.CURRENCY_NAME || '');
  }
  account_parent_id_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PARENT_ACCOUNT_NAME'].setValue(state?.item?.ACCOUNT_NAME || '');
  }
}
