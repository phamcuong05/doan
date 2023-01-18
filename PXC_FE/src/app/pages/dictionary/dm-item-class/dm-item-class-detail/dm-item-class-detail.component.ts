import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmAccount } from 'src/app/model/dictionary/dm-account/dm-account';
import { DmItemClassService } from 'src/app/model/dictionary/dm-item-class/dm-item-class-service';

@Component({
  selector: 'dm-item-class-detail',
  templateUrl: './dm-item-class-detail.component.html',
  styleUrls: ['./dm-item-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemClassDetailComponent),
    },
  ],
})

/**
 *
 */
export class DmItemClassDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmItemClassService: DmItemClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmItemClassService, myInject);
  }

  state$: {
    DmAccountDatas: DmAccount[];
  } = {
    DmAccountDatas: [],
  };

  formGroup!: FormGroup;
  idField = 'ITEM_CLASS_ID';
  nameField = 'ITEM_CLASS_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ITEM_CLASS_ID: ['', [Validators.required]],
      ITEM_CLASS_NAME: ['', [Validators.required]],
      INV_ACCOUNT_ID: [''],
      ACCOUNT_NAME: [''],
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

  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([this.myService.loadDm<DmAccount>('Dm_Account')])
      .then(([DmAccountDatas]) => {
        this.state$ = {
          ...this.state$,
          DmAccountDatas,
        };
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
  accountId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ACCOUNT_NAME'].setValue(
      state?.item?.ACCOUNT_NAME || ''
    );
  }
}
