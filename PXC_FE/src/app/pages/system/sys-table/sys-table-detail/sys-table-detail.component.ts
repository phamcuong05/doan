import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModuleList } from 'src/app/base/module-list';
import { ProjectList } from 'src/app/base/project-list';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { SysTableService } from 'src/app/model/system/sys-table/sys-table-service';

@Component({
  selector: 'sys-table-detail',
  templateUrl: './sys-table-detail.component.html',
  styleUrls: ['./sys-table-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => SysTableDetailComponent),
    },
  ],
})
export class SysTableDetailComponent extends FTSDictBaseDetail {
  width: number = 600;

  constructor(
    private fb: FormBuilder,
    sysMenuService: SysTableService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(sysMenuService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'TABLE_NAME';
  nameField = 'ID_FIELD';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      TABLE_NAME: ['', [Validators.required]],
      ID_FIELD: ['', [Validators.required]],
      NAME_FIELD: [''],
      TABLE_TYPE: [''],
      CAN_GROUP: [false],
      ID_AUTO: [false],
      ID_MASK: [''],
      ID_LENGTH: [0],
      ID_PARTS: [0],
      ID_SPLIT: [''],
      LOAD_BY_SEARCH: [false],
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
