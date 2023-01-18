import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmDistrictService } from 'src/app/model/dictionary/dm-district/dm-district-service';
import { DmPeriodService } from 'src/app/model/dictionary/dm-period/dm-period-service';
import { PeriodType } from 'src/app/model/dictionary/dm-period/period-type';

@Component({
  selector: 'dm-period-detail',
  templateUrl: './dm-period-detail.component.html',
  styleUrls: ['./dm-period-detail.component.scss'],
  providers: [DictBaseDetailStore,
    DmDistrictService,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPeriodDetailComponent),
    },
  ]
})
export class DmPeriodDetailComponent extends FTSDictBaseDetail {

  width: number = 550;
  constructor(
    private fb: FormBuilder,
    dmItemOpService: DmPeriodService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmItemOpService, myInject);
  }

  formGroup !: FormGroup;
  idField = 'PERIOD_ID';
  nameField = 'PERIOD_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmPeriodResource.MyResource.DM_PERIOD;
  }

  state$: { PeriodTypeDatas: PeriodType[]; }
    = { PeriodTypeDatas: [], };

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      PERIOD_ID: ['', [Validators.required]],
      PERIOD_NAME: ['', [Validators.required]],
      PERIOD_NUMBER: [0, [Validators.required]],
      PERIOD_TYPE: [''],
      PERIOD_TYPE_NAME: [''],
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

  tranferItemOpId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['TRANSFER_PERIOD_NAME'].setValue(
      state?.item?.PERIOD_NAME || ''
    );
  }

  /**
  * override load DM,
  */
  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      (<DmPeriodService>this.myService).GetPeriodTypeList()
    ])
      .then(([PeriodTypeDatas]) => {
        this.state$ = { ...this.state$, PeriodTypeDatas: PeriodTypeDatas };
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