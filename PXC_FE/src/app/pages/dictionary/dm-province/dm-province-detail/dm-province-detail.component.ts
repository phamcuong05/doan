import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmProvinceService } from 'src/app/model/dictionary/dm-province/dm-province-service';

@Component({
  selector: 'dm-province-detail',
  templateUrl: './dm-province-detail.component.html',
  styleUrls: ['./dm-province-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmProvinceDetailComponent),
    },
  ],
})
export class DmProvinceDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmDistrictService: DmProvinceService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmDistrictService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'PROVINCE_ID';
  nameField = 'PROVINCE_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      PROVINCE_ID: ['', [Validators.required]],
      PROVINCE_NAME: ['', [Validators.required]],
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
