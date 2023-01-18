import {
  Component,
  ElementRef,
  forwardRef,
  ViewContainerRef,
} from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmPrDetailService } from 'src/app/model/dictionary/dm-pr-detail/dm-pr-detail-service';

@Component({
  selector: 'dm-pr-detail-selector',
  templateUrl: './dm-pr-detail-selector.component.html',
  styleUrls: ['./dm-pr-detail-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPrDetailSelectorComponent),
    },
  ],
})
export class DmPrDetailSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_PR_DETAIL';
  searchFields: string[] = [
    'PR_DETAIL_ID',
    'PR_DETAIL_NAME',
    'IDENTITY_NO',
    'TAX_FILE_NUMBER',
  ];
  get formTitle(): string {
    return this.resourceManager.DmPrDetailResource.MyResource.DM_PR_DETAIL;
  }
  columns: FtsColumn[] = [
    { FieldId: 'PR_DETAIL_ID', Length: 20 } as FtsColumn,
    { FieldId: 'PR_DETAIL_NAME', Length: 30 },
    { FieldId: 'IDENTITY_NO', Length: 12 },
    { FieldId: 'TAX_FILE_NUMBER', Length: 15 },
    { FieldId: 'EMAIL', Length: 20 },
    { FieldId: 'ADDRESS', Length: 50 },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    prDetailService: DmPrDetailService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      prDetailService,
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

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (param) {
      if (!param.FilterGroups) {
        param.FilterGroups = [];
      }
      param.FilterGroups.push({
        Filters: [
          {
            Field: 'ACTIVE',
            Operator: 'eq',
            Value: 1,
          },
        ],
        Logic: 'and',
      });

      param.FilterFields = [
        'PR_DETAIL_ID',
        'PR_DETAIL_NAME',
        'IDENTITY_NO',
        'EMAIL',
        'ADDRESS',
        'TAX_FILE_NUMBER',
        'BANK_CODE',
        'BANK_NAME',
        'BANK_BRANCH',
        'BANK_ACCOUNT_NAME',
        'BANK_ACCOUNT_NO',
      ];
    }
    return param;
  }
}
