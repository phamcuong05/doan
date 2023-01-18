//#region  import
import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FtsException } from 'src/app/base/fts-exception';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmItemClass } from 'src/app/model/dictionary/dm-item-class/dm-item-class';
import { DmUnit } from 'src/app/model/dictionary/dm-unit/dm-unit';
import { DmItem } from 'src/app/model/dictionary/dm-item/dm-item';
import { DmItemService } from 'src/app/model/dictionary/dm-item/dm-item-service';

@Component({
  selector: 'dm-item-detail',
  templateUrl: './dm-item-detail.component.html',
  styleUrls: ['./dm-item-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemDetailComponent),
    },
  ],
})
export class DmItemDetailComponent extends FTSDictBaseDetail {
  width: number = 650;

  constructor(
    private fb: FormBuilder,
    dmItemService: DmItemService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmItemService, myInject);
  }

  state$: {
    DmItemClassDatas: DmItemClass[];
    DmUnitDatas: DmUnit[];
  } = {
    DmItemClassDatas: [],
    DmUnitDatas: [],
  };

  formGroup!: FormGroup;
  idField = 'ITEM_ID';
  nameField = 'ITEM_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmItemResource.MyResource.DM_ITEM;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ITEM_ID: ['', [Validators.required]],
      ITEM_NAME: ['', [Validators.required]],
      ITEM_CLASS_ID: ['', [Validators.required]],
      ITEM_CLASS1_ID: ['', [Validators.required]],
      UNIT_ID: ['', [Validators.required]],
      UNIT_NAME: [''],
      ITEM_CLASS_NAME: [''],
      ITEM_CLASS1_NAME: [''],
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

  public updateInfo(currentRow: any): void {
    var actionType = this.getActionType();
    if (actionType == 'VIEW' || actionType == 'EDIT') {
    }
  }

  public checkBusinessRules(currentRow: any): void {
    var dataMode = this.getActionType();
    var objItem: DmItem = currentRow;
    if (objItem.ITEM_ID.length == 0) {
      let ftsException: FtsException = {
        mMessage: 'Mã hàng hóa được để trống',
        mFieldName: 'ITEM_ID',
      } as FtsException;
      throw ftsException;
    }
  }

  /**
   * chọn đơn vị
   * @param state
   */
  unitId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['UNIT_NAME'].setValue(state?.item?.UNIT_NAME || '');
  }

  /**
   * Chọn itemClass
   * @param state
   */
  itemClassId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ITEM_CLASS_NAME'].setValue(
      state?.item?.ITEM_CLASS_NAME || ''
    );
  }

  // /**
  //  * Set PR_DETAIL_CLASS_NAME
  //  * @param state
  //  */
  // PrDetailClass_selectionChange(state: { item: any; form: FormGroup }): void {
  //   this.formGroup.controls['PR_DETAIL_CLASS_NAME'].setValue(
  //     state?.item?.PR_DETAIL_CLASS_NAME || ''
  //   );
  // }

  /**
   * Set itemclass1
   * @param state
   */
  ItemClass1_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['ITEM_CLASS1_NAME'].setValue(
      state?.item?.ITEM_CLASS1_NAME || ''
    );
  }

  /**
   * override load DM,
   * Thực hiện load danh mục, nếu load 1 lần ở list thì set DM trên list
   * Created by: MTLUC - 03/11/2021
   */
  public loadDm() {
    this.myInject.detailStore.loadData();
    Promise.all([
      this.myService.loadDm<DmItemClass>('Dm_Item_Class'),
      this.myService.loadDm<DmUnit>('Dm_Unit'),
    ])
      .then(([DmItemClassDatas, DmUnitDatas]) => {
        this.state$ = {
          ...this.state$,
          DmItemClassDatas,
          DmUnitDatas,
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
}
