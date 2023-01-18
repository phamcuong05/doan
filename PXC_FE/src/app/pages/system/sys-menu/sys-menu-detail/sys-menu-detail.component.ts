import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModuleList } from 'src/app/base/module-list';
import { ProjectList } from 'src/app/base/project-list';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { SysMenuService } from 'src/app/model/system/sys-menu/sys-menu-service';

@Component({
  selector: 'sys-menu-detail',
  templateUrl: './sys-menu-detail.component.html',
  styleUrls: ['./sys-menu-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => SysMenuDetailComponent),
    },
  ],
})
export class SysMenuDetailComponent extends FTSDictBaseDetail {
  width: number = 700;

  constructor(
    private fb: FormBuilder,
    sysMenuService: SysMenuService,
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
  idField = 'MENU_ID';
  nameField = 'MENU_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      MENU_ID: ['', [Validators.required]],
      MENU_NAME: ['', [Validators.required]],
      MODULE_ID: [''],
      MENU_GROUP_ID: [''],
      ICON_CLS: [''],
      HREF: [''],
      MAP_PATH: [''],
      MENU_ORDER: [1],
      EXPAND_TYPE: [''],
      ACTIVE: [true],
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

  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      (<SysMenuService>this.myService).GetModuleList(),
      (<SysMenuService>this.myService).GetProjectList(),
    ])
      .then(([ModuleListDatas, ProjectListDatas]) => {
        this.state$ = { ...this.state$, ModuleListDatas, ProjectListDatas };
        this.myInject.detailStore.loadDataComplete(undefined);
      })
      .catch((err) => {
        this.myInject.detailStore.loadDataComplete(err);
        this.myInject.ftsDialogService.alert.show({
          content: this.myInject.FTSMain.ExceptionManager.processException(err),
          icon: 'warning',
        });
      });
  }
}
