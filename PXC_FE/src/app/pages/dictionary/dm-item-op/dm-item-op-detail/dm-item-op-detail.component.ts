import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmDistrictService } from 'src/app/model/dictionary/dm-district/dm-district-service';
import { DmItemOpService } from 'src/app/model/dictionary/dm-item-op/dm-item-op-service';
import { IssueReceiptType } from 'src/app/model/dictionary/dm-item-op/issue-receipt-type';


@Component({
  selector: 'dm-item-op-detail',
  templateUrl: './dm-item-op-detail.component.html',
  styleUrls: ['./dm-item-op-detail.component.scss'],
  providers: [DictBaseDetailStore,
    DmDistrictService,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemOpDetailComponent),
    },
  ]
})
export class DmItemOpDetailComponent extends FTSDictBaseDetail {

  width: number = 550;
  constructor(
    private fb: FormBuilder,
    dmItemOpService: DmItemOpService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmItemOpService, myInject);
  }

  formGroup !: FormGroup;
  idField = 'ITEM_OP_ID';
  nameField = 'ITEM_OP_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmItemOpResource.MyResource.DM_ITEM_OP;
  }

  state$: { IssueReceiptTypeDatas: IssueReceiptType[]; }
    = { IssueReceiptTypeDatas: [], };

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ITEM_OP_ID: ['', [Validators.required]],
      ITEM_OP_NAME: ['', [Validators.required]],
      ISSUE_RECEIPT: ['', [Validators.required]],
      TRANSFER_ITEM_OP_ID: [''],
      TRANSFER_ITEM_OP_NAME: [''],
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
    this.formGroup.controls['TRANSFER_ITEM_OP_NAME'].setValue(
      state?.item?.ITEM_OP_NAME || ''
    );
  }

  /**
  * override load DM,
  */
  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      (<DmItemOpService>this.myService).GetIssueReceiptList()
    ])
      .then(([IssueReceiveTypeDatas]) => {
        this.state$ = { ...this.state$, IssueReceiptTypeDatas: IssueReceiveTypeDatas };
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