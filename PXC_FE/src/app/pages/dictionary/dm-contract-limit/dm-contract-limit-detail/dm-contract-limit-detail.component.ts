import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { guid } from '@progress/kendo-angular-common';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmContractLimitService } from 'src/app/model/dictionary/dm-contract-limit/dm-contract-limit-service';
import { DmPrDetail } from 'src/app/model/dictionary/dm-pr-detail/dm-pr-detail';

@Component({
  selector: 'dm-contract-limit-detail',
  templateUrl: './dm-contract-limit-detail.component.html',
  styleUrls: ['./dm-contract-limit-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractLimitDetailComponent),
    },
  ],
})
export class DmContractLimitDetailComponent extends FTSDictBaseDetail {
  width: number = 600;
  constructor(
    private fb: FormBuilder,
    dmContractClassService: DmContractLimitService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmContractClassService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'PR_KEY';
  nameField = 'PR_DETAIL_ID';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ORGANIZATION_ID: ['', [Validators.required]],
      PR_DETAIL_ID: ['', [Validators.required]],
      PR_DETAIL_NAME: ['', [Validators.required]],
      VALID_DATE: [new Date()],
      AMOUNT: [0],
      NOTES: [''],
      USER_ID: [''],
      CREATE_DATE: [new Date()],
      MODIFY_DATE: [new Date()],
    });
  }

  setDataDuplicate(currentRow: any) {
    let newRecord: any = this.getNewRecord();
    currentRow = {
      ...currentRow,
      [this.idField]: commonFunction.newGuid(),
    };
    return currentRow;
  }

  prDetailId_SelectionChange({
    item, form,
  }: {
    item: DmPrDetail; form: FormGroup;
  }) {
    this.formGroup.controls['PR_DETAIL_NAME'].setValue(item?.PR_DETAIL_NAME || '');
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
