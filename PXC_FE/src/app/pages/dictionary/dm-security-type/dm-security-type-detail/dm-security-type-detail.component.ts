import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmSecurityTypeService } from 'src/app/model/dictionary/dm-security-type/dm-security-type-service';

@Component({
  selector: 'dm-security-type-detail',
  templateUrl: './dm-security-type-detail.component.html',
  styleUrls: ['./dm-security-type-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecurityTypeDetailComponent),
    },
  ],
})
export class DmSecurityTypeDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmSecurityTypeResource.MyResource
      .DM_SECURITY_TYPE;
  }
  formGroup!: FormGroup;
  idField: string = "SECURITY_TYPE_ID";
  nameField: string = "SECURITY_TYPE_NAME";
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      SECURITY_TYPE_ID: ['', [Validators.required]],
      SECURITY_TYPE_NAME: ['', [Validators.required]],
      ACTIVE: [true],
      USER_ID: [''],
    });
  }
  width: number = 450;

  constructor(
    private fb: FormBuilder,
    dmSecurityTypeService: DmSecurityTypeService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmSecurityTypeService, myInject);
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
