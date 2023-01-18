import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsEditListBase } from 'src/app/controls/fts-edit-list-base/fts-edit-list-base';
import { FtsEditListBaseInject } from 'src/app/controls/fts-edit-list-base/fts-edit-list-base-inject';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { EditListBaseStore } from 'src/app/model/base/edit-list-base/edit-list-base-store';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmAdvanceLimitService } from 'src/app/model/dictionary/dm-advance-limit/dm-advance-limit-service';

@Component({
  selector: 'dm-advance-limit-edit-list',
  templateUrl: './dm-advance-limit-edit-list.component.html',
  styleUrls: ['./dm-advance-limit-edit-list.component.scss'],
  providers: [FtsEditListBaseInject, EditListBaseStore],
})
export class DmAdvanceLimitEditListComponent extends FtsEditListBase {
  createFormGroup(): FormGroup {
    const that = this;
    let form = that.fb.group({
      ORGANIZATION_ID: ['', [Validators.required, Validators.maxLength(20)]],
      ACCOUNT_ID: ['', [Validators.required, Validators.maxLength(20)]],
      VALID_DATE: ['', [Validators.required]],
      ADVANCE_LIMIT: ['', [Validators.required, Validators.min(0)]],
    });
    return form;
  }

  columns: FtsColumn[] = [
    {
      FieldId: 'ORGANIZATION_ID',
      Width: 200,
      ColumnType: 'textlookup',
    } as FtsColumn,
    {
      FieldId: 'ACCOUNT_ID',
      Width: 200,
      ColumnType: 'textlookup',
    } as FtsColumn,
    {
      FieldId: 'VALID_DATE',
      Width: 200,
      ColumnType: 'date',
      Format: this.myInject.FTSMain.dateFormat,
    },
    {
      FieldId: 'ADVANCE_LIMIT',
      Width: 200,
      ColumnType: 'numeric',
      Format: this.myInject.FTSMain.amountFormat,
    },
  ] as FtsColumn[];

  tableName: string = 'DM_ADVANCE_LIMIT';
  idField: string = 'PR_KEY';
  nameField: string = 'ORGANIZATION_ID';

  constructor(
    public resourceManager: ResourceManager,
    public myService: DmAdvanceLimitService,
    public myInject: FtsEditListBaseInject,
    private fb: FormBuilder
  ) {
    super(myService, myInject);
  }

  override getNewRecord(): object {
    return { ...this.defaultRecord, PR_KEY: commonFunction.newGuid() };
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (!param.Sorts) {
      param.Sorts = [
        {
          Field: 'ORGANIZATION_ID',
          Dir: 'ASC',
        },
      ];
    }
    return param;
  }
}
