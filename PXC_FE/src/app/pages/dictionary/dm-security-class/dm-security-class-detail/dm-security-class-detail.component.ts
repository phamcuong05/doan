import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmSecurityClassService } from 'src/app/model/dictionary/dm-security-class/dm-security-class-service';

@Component({
  selector: 'dm-security-class-detail',
  templateUrl: './dm-security-class-detail.component.html',
  styleUrls: ['./dm-security-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecurityClassDetailComponent),
    },
  ],
})
export class DmSecurityClassDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmSecurityClassResource.MyResource
      .DM_SECURITY_CLASS;
  }
  formGroup!: FormGroup;
  idField: string = "SECURITY_CLASS_ID";
  nameField: string = "SECURITY_CLASS_NAME";
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      SECURITY_CLASS_ID: ['', [Validators.required]],
      SECURITY_CLASS_NAME: ['', [Validators.required]],
      ACTIVE: [true],
      USER_ID: [''],
    });
  }
  width: number = 450;

  constructor(
    private fb: FormBuilder,
    dmSecurityClassService: DmSecurityClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmSecurityClassService, myInject);
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
