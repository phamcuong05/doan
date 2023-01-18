//#region  import
import { Component, forwardRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FtsException } from 'src/app/base/fts-exception';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmPrDetail } from 'src/app/model/dictionary/dm-pr-detail/dm-pr-detail';
import { DmPrDetailService } from 'src/app/model/dictionary/dm-pr-detail/dm-pr-detail-service';
import { DmDistrictSelectorComponent } from '../../dm-district/dm-district-selector/dm-district-selector.component';
//#endregion

@Component({
  selector: 'dm-pr-detail-detail',
  templateUrl: './dm-pr-detail-detail.component.html',
  styleUrls: ['./dm-pr-detail-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPrDetailDetailComponent),
    },
  ],
})
export class DmPrDetailDetailComponent extends FTSDictBaseDetail {
  width: number = 700;
  @ViewChild('districtSelector')
  districtSelectorComponent!: DmDistrictSelectorComponent;

  constructor(
    private fb: FormBuilder,
    prDetailService: DmPrDetailService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(prDetailService, myInject);
  }

  /*  state$: {
    PrDetailClassDatas: PrDetailClass[];
    PrDetailClass1Datas: PrDetailClass1[];
    DmAccountDatas: DmAccount[];
  } = {
      PrDetailClassDatas: [],
      PrDetailClass1Datas: [],
      DmAccountDatas: [],
    }; */

  formGroup!: FormGroup;
  idField = 'PR_DETAIL_ID';
  nameField = 'PR_DETAIL_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmPrDetailResource.MyResource
      .DM_PR_DETAIL;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      PR_DETAIL_ID: ['', [Validators.required]],
      PR_DETAIL_NAME: ['', [Validators.required]],
      PR_DETAIL_CLASS_ID: [''],
      PR_DETAIL_CLASS_NAME: [''],
      PR_DETAIL_CLASS1_ID: [''],
      PR_DETAIL_CLASS1_NAME: [''],
      PR_ACCOUNT_ID: [''],
      PR_ACCOUNT_NAME: [''],
      ADDRESS: [''],
      PHONE: [''],
      EMAIL: ['', [Validators.email]],
      TAX_FILE_NUMBER: [''],
      IDENTITY_NO: [''],
      BANK_NAME: [''],
      BANK_ACCOUNT_NAME: [''],
      BANK_ACCOUNT_NO: [''],
      BANK_BRANCH: [''],
      BANK_CODE: [''],
      PROVINCE_ID: [''],
      PROVINCE_NAME: [''],
      DISTRICT_ID: [''],
      DISTRICT_NAME: [''],
      PASSWORD: [''],
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
    var objPrDetail: DmPrDetail = currentRow;
    if (objPrDetail.PR_DETAIL_ID.length == 0) {
      let ftsException: FtsException = {
        mMessage: 'Mã đối tượng không được để trống',
        mFieldName: 'PR_DETAIL_ID',
      } as FtsException;
      throw ftsException;
    }
  }

  /**
   * chọn tỉnh/thành phố
   * @param state
   */
  provinceId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PROVINCE_NAME'].setValue(
      state?.item?.PROVINCE_NAME || ''
    );

    this.formGroup.controls['DISTRICT_ID'].setValue('');
    this.formGroup.controls['DISTRICT_NAME'].setValue('');
    this.districtSelectorComponent.provinceId = state?.item?.PROVINCE_ID;
  }

  /**
   * Chọn quận huyện
   * @param state
   */
  districtId_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['DISTRICT_NAME'].setValue(
      state?.item?.DISTRICT_NAME || ''
    );
  }

  /**
   * Set PR_DETAIL_CLASS_NAME
   * @param state
   */
  PrDetailClass_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PR_DETAIL_CLASS_NAME'].setValue(
      state?.item?.PR_DETAIL_CLASS_NAME || ''
    );
  }

  /**
   * Set PR_DETAIL_CLASS1_NAME
   * @param state
   */
  PrDetailClass1_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PR_DETAIL_CLASS1_NAME'].setValue(
      state?.item?.PR_DETAIL_CLASS1_NAME || ''
    );
  }

  /**
   * Set PR_DETAIL_CLASS1_NAME
   * @param state
   */
  PrAccount_selectionChange(state: { item: any; form: FormGroup }): void {
    this.formGroup.controls['PR_ACCOUNT_NAME'].setValue(
      state?.item?.ACCOUNT_NAME || ''
    );
  }

  /**
   * override load DM,
   * Thực hiện load danh mục, nếu load 1 lần ở list thì set DM trên list
   */
  public loadDm() {
    /* this.myInject.detailStore.loadData();
    Promise.all([
      this.myService.loadDm<PrDetailClass>('Dm_Pr_Detail_Class'),
      this.myService.loadDm<PrDetailClass1>('Dm_Pr_Detail_Class1'),
      this.myService.loadDm<DmAccount>('Dm_Account'),
    ])
      .then(([PrDetailClassDatas, PrDetailClass1Datas, DmAccountDatas]) => {
        this.state$ = {
          ...this.state$,
          PrDetailClassDatas,
          PrDetailClass1Datas,
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
      }); */
  }
}
