import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmDistrictService } from 'src/app/model/dictionary/dm-district/dm-district-service';

@Component({
  selector: 'dm-district-detail',
  templateUrl: './dm-district-detail.component.html',
  styleUrls: ['./dm-district-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmDistrictDetailComponent),
    },
  ],
})
export class DmDistrictDetailComponent extends FTSDictBaseDetail {
  width: number = 550;
  constructor(
    private fb: FormBuilder,
    dmDistrictService: DmDistrictService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmDistrictService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'DISTRICT_ID';
  nameField = 'DISTRICT_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      DISTRICT_ID: ['', [Validators.required]],
      DISTRICT_NAME: ['', [Validators.required]],
      PROVINCE_ID: ['', [Validators.required]],
      PROVINCE_NAME: [''],
      ACTIVE: [true],
      USER_ID: [''],
    });
  }

  /**
   * chọn tỉnh/thành phố
   * @param state
   */
  provinceId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PROVINCE_NAME'].setValue(
      state?.item?.PROVINCE_NAME || ''
    );
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
