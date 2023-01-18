import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmContractClassService } from 'src/app/model/dictionary/dm-contract-class/dm-contract-class-service';

@Component({
  selector: 'dm-contract-class-detail',
  templateUrl: './dm-contract-class-detail.component.html',
  styleUrls: ['./dm-contract-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractClassDetailComponent),
    },
  ],
})
export class DmContractClassDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmContractClassService: DmContractClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmContractClassService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'CONTRACT_CLASS_ID';
  nameField = 'CONTRACT_CLASS_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      CONTRACT_CLASS_ID: ['', [Validators.required]],
      CONTRACT_CLASS_NAME: ['', [Validators.required]],
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
