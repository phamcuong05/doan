import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModuleList } from 'src/app/base/module-list';
import { ProjectList } from 'src/app/base/project-list';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { SysSystemVarService } from 'src/app/model/system/system-var/sys-systemvar-service';

@Component({
  selector: 'sys-systemvar-detail',
  templateUrl: './sys-systemvar-detail.component.html',
  styleUrls: ['./sys-systemvar-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => SysSystemVarDetailComponent),
    },
  ],
})
export class SysSystemVarDetailComponent extends FTSDictBaseDetail {
  width: number = 600;

  constructor(
    private fb: FormBuilder,
    sysMenuService: SysSystemVarService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(sysMenuService, myInject);
  }

  state$: {
    ModuleListDatas: ModuleList[],
    ProjectListDatas: ProjectList[]
  }
    = {
      ModuleListDatas: [],
      ProjectListDatas: [],
    };

  formGroup!: FormGroup;
  idField = 'VAR_NAME';
  nameField = 'VAR_VALUE';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      VAR_NAME: ['', [Validators.required]],
      VAR_VALUE: ['', [Validators.required]],
      DESCRIPTION: [''],
      VAR_TYPE: [''],
      VAR_GROUP: [''],
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
