import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { SysResoureService } from 'src/app/model/system/sys-resource/sys-resource-service';

@Component({
  selector: 'sys-resource-detail',
  templateUrl: './sys-resource-detail.component.html',
  styleUrls: ['./sys-resource-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => SysResourceDetailComponent),
    },
  ],
})
export class SysResourceDetailComponent extends FTSDictBaseDetail {
  width: number = 450;

  constructor(
    private fb: FormBuilder,
    sysResoureService: SysResoureService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(sysResoureService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'RES_ID';
  nameField = 'RES_VALUE';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      RES_ID: ['', [Validators.required]],
      RES_VALUE: ['', [Validators.required]],
    });
  }

  getNewRecord(): object {
    return {
      RES_ID: '',
      RES_VALUE: '',
    };
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
