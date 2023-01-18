import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmUnitService } from 'src/app/model/dictionary/dm-unit/dm-unit-service';

@Component({
  selector: 'dm-unit-detail',
  templateUrl: './dm-unit-detail.component.html',
  styleUrls: ['./dm-unit-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmUnitDetailComponent),
    },
  ],
})
export class DmUnitDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmUnitService: DmUnitService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmUnitService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'UNIT_ID';
  nameField = 'UNIT_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmUnitResource.MyResource.DM_UNIT;
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      UNIT_ID: ['', [Validators.required]],
      UNIT_NAME: ['', [Validators.required]],
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
