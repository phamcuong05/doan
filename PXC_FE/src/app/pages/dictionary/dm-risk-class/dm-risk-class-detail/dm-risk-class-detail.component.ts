import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmRiskClassService } from 'src/app/model/dictionary/dm-risk-class/dm-risk-class-service';

@Component({
  selector: 'dm-risk-class-detail',
  templateUrl: './dm-risk-class-detail.component.html',
  styleUrls: ['./dm-risk-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmRiskClassDetailComponent),
    },
  ],
})
export class DmRiskClassDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    DmRiskClassService: DmRiskClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(DmRiskClassService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'RISK_CLASS_ID';
  nameField = 'RISK_CLASS_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      RISK_CLASS_ID: ['', [Validators.required]],
      RISK_CLASS_NAME: ['', [Validators.required]],
      RISK_CLASS_CATEGORY: 0,
      ACTIVE: [true],
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
