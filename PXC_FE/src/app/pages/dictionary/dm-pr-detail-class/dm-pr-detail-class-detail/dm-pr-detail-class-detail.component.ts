import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmPrDetailClassService } from 'src/app/model/dictionary/dm-pr-detail-class/dm-pr-detail-class-service';
import { PrDetailType } from 'src/app/model/dictionary/dm-pr-detail-class/pr-detail-type';

@Component({
  selector: 'dm-pr-detail-class-detail',
  templateUrl: './dm-pr-detail-class-detail.component.html',
  styleUrls: ['./dm-pr-detail-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPrDetailClassDetailComponent),
    },
  ],
})
export class DmPrDetailClassDetailComponent extends FTSDictBaseDetail {
  width: number = 450;
  constructor(
    private fb: FormBuilder,
    dmPrDetailClassService: DmPrDetailClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmPrDetailClassService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'PR_DETAIL_CLASS_ID';
  nameField = 'PR_DETAIL_CLASS_NAME';
  get formTitle(): string {
    return commonFunction.getPageTitle();
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      PR_DETAIL_CLASS_ID: ['', [Validators.required]],
      PR_DETAIL_CLASS_NAME: ['', [Validators.required]],
      PR_DETAIL_TYPE_ID: ['', [Validators.required]],
      ACTIVE: true,
    });
  }

  state$: {
    PrDetailTypeDatas: PrDetailType[];
  } = {
    PrDetailTypeDatas: [],
  };

  loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      (<DmPrDetailClassService>this.myService).LoadDataPrDetailType(),
    ])
      .then(([PrDetailTypeDatas]) => {
        this.state$ = { ...this.state$, PrDetailTypeDatas };
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
