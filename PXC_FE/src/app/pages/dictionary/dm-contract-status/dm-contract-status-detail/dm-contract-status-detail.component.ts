import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmContractStatusService } from 'src/app/model/dictionary/dm-contract-status/dm-contract-status-service';

@Component({
  selector: 'dm-contract-status-detail',
  templateUrl: './dm-contract-status-detail.component.html',
  styleUrls: ['./dm-contract-status-detail.component.scss'],
  providers: [DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractStatusDetailComponent),
    },
  ]
})
export class DmContractStatusDetailComponent extends FTSDictBaseDetail {

  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmContractStatusService: DmContractStatusService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmContractStatusService, myInject);
  }

  formGroup !: FormGroup;
  idField = 'CONTRACT_STATUS_ID';
  nameField = 'CONTRACT_STATUS_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      CONTRACT_STATUS_ID: ['', [Validators.required]],
      CONTRACT_STATUS_NAME: ['', [Validators.required]],
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
