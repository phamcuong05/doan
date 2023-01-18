import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmEstimateTypeService } from 'src/app/model/dictionary/dm-estimate-type/dm-estimate-type-service';

@Component({
  selector: 'dm-estimate-type-detail',
  templateUrl: './dm-estimate-type-detail.component.html',
  styleUrls: ['./dm-estimate-type-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmEstimateTypeDetailComponent),
    },
  ],
})
export class DmEstimateTypeDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmDistrictService: DmEstimateTypeService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmDistrictService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'ESTIMATE_TYPE_ID';
  nameField = 'ESTIMATE_TYPE_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ESTIMATE_TYPE_ID: ['', [Validators.required]],
      ESTIMATE_TYPE_NAME: ['', [Validators.required]],
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
