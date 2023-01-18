import {
  Component,
  ElementRef,
  forwardRef, ViewContainerRef
} from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmClaimService } from 'src/app/model/dictionary/dm-claim/dm-claim-service';
import { Period } from 'src/app/model/other/period';

@Component({
  selector: 'dm-claim-selector',
  templateUrl: './dm-claim-selector.component.html',
  styleUrls: ['./dm-claim-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmClaimSelectorComponent),
    },
  ],
})
export class DmClaimSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CLAIM';
  period: Period = {} as Period;

  searchFields: string[] = ['CLAIM_NO', 'STATUS'];
  get formTitle(): string {
    return this.resourceManager.CommonResource.MyResource.Claims;
  }
  columns: FtsColumn[] = [
    { FieldId: 'CLAIM_NO' },
    { FieldId: 'TRAN_DATE', ColumnType: 'date', Width: 100 },
    { FieldId: 'STATUS' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmClaimService: DmClaimService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmClaimService,
      maskLoad,
      viewContainerRef,
      el,
      ftsDialogService,
      eventManager,
      ftsMain
    );
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
    if (param) {
      param.TranId = 'CLAIM';
      if (!param.FilterGroups) {
        param.FilterGroups = [];
      }

      param.FilterGroups.push({
        Filters: [
          {
            Field: 'TRAN_DATE',
            Operator: 'gte',
            Value: this.period.FromDate,
          },
          {
            Field: 'TRAN_DATE',
            Operator: 'lt',
            Value: commonFunction.addDay(this.period.ToDate, 1),
          },
          {
            Field: 'ORGANIZATION_ID',
            Operator: 'eq',
            Value: this.ftsMain.UserInfo.OrganizationID,
          },
        ],
        Logic: 'and',
      });

      param.FilterFields = ['PR_KEY', 'CLAIM_NO', 'TRAN_DATE', 'STATUS'];

      if (!param.Sorts) {
        param.Sorts = [
          {
            Dir: 'ASC',
            Field: 'CLAIM_NO',
          },
        ];
      }
    }
    return param;
  }
}
