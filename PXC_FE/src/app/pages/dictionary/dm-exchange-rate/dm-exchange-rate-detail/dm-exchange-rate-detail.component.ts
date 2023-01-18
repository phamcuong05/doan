import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmExchangeRateService } from 'src/app/model/dictionary/dm-exchange-rate/dm-exchange-rate-service';

@Component({
  selector: 'dm-exchange-rate-detail',
  templateUrl: './dm-exchange-rate-detail.component.html',
  styleUrls: ['./dm-exchange-rate-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmExchangeRateDetailComponent),
    },
  ],
})
export class DmExchangeRateDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  formGroup!: FormGroup;
  idField: string = 'PR_KEY';
  nameField: string = 'CURRENCY_ID';
  width: number = 450;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      CURRENCY_ID: ['', [Validators.required, Validators.maxLength(20)]],
      TO_CURRENCY_ID: ['', [Validators.required, Validators.maxLength(20)]],
      EXCHANGE_RATE: [1, [Validators.required, Validators.min(0)]],
      VALID_DATE: [new Date(), [Validators.required]],
    });
  }

  constructor(
    private fb: FormBuilder,
    dmExchangeRateService: DmExchangeRateService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmExchangeRateService, myInject);
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
