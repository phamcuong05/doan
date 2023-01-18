import { Component, forwardRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmAgentService } from 'src/app/model/dictionary/dm-agent/dm-agent-service';
import { DmDistrict } from 'src/app/model/dictionary/dm-district/dm-district';
import { DmProvince } from 'src/app/model/dictionary/dm-province/dm-province';
import { DmDistrictSelectorComponent } from '../../dm-district/dm-district-selector/dm-district-selector.component';

@Component({
  selector: 'dm-agent-detail',
  templateUrl: './dm-agent-detail.component.html',
  styleUrls: ['./dm-agent-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmAgentDetailComponent),
    },
  ],
})
export class DmAgentDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmAgentResource.MyResource.AGENT;
  }
  formGroup!: FormGroup;
  idField: string = 'AGENT_ID';
  nameField: string = 'AGENT_NAME';
  width: number = 700;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      AGENT_ID: ['', [Validators.required, Validators.maxLength(20)]],
      AGENT_NAME: ['', [Validators.required, Validators.maxLength(100)]],
      ADDRESS: ['', [Validators.maxLength(200)]],
      PHONE: ['', [Validators.maxLength(20)]],
      EMAIL: ['', [Validators.maxLength(100), Validators.email]],
      TAX_FILE_NUMBER: ['', [Validators.maxLength(30)]],
      IDENTITY_NO: ['', [Validators.maxLength(30)]],
      BANK_NAME: ['', [Validators.maxLength(100)]],
      BANK_ACCOUNT: ['', [Validators.maxLength(100)]],
      BANK_BRANCH: ['', [Validators.maxLength(100)]],
      BANK_CODE: ['', [Validators.maxLength(20)]],
      PROVINCE_ID: ['', [Validators.maxLength(20)]],
      PROVINCE_NAME: [''],
      DISTRICT_ID: ['', [Validators.maxLength(20)]],
      DISTRICT_NAME: [''],
      ACTIVE: [true],
    });
  }

  @ViewChild('districtSelector')
  districtSelectorComponent!: DmDistrictSelectorComponent;

  constructor(
    private fb: FormBuilder,
    dmAgentService: DmAgentService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmAgentService, myInject);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  districtId_SelectionChange({
    item,
    form,
  }: {
    item: DmDistrict;
    form: FormGroup;
  }) {
    form?.get('DISTRICT_NAME')?.patchValue(item.DISTRICT_NAME);
  }

  provinceId_SelectionChange({
    item,
    form,
  }: {
    item: DmProvince;
    form: FormGroup;
  }) {
    form?.get('PROVINCE_NAME')?.patchValue(item.PROVINCE_NAME);
    form?.get('DISTRICT_ID')?.setValue('');
    form?.get('DISTRICT_NAME')?.setValue('');
    this.districtSelectorComponent.provinceId = item.PROVINCE_ID;
  }
}
