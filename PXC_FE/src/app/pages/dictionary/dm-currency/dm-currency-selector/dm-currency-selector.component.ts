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
import { DmCurrencyService } from 'src/app/model/dictionary/dm-currency/dm-currency-service';

@Component({
  selector: 'dm-currency-selector',
  templateUrl: './dm-currency-selector.component.html',
  styleUrls: ['./dm-currency-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmCurrencySelectorComponent),
    },
  ],
})
export class DmCurrencySelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CURRENCY';
  searchFields: string[] = ['CURRENCY_ID', 'CURRENCY_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmCurrencyResource.MyResource.DM_CURRENCY;
  }
  columns: FtsColumn[] = [
    { FieldId: 'CURRENCY_ID', Length: 20 },
    { FieldId: 'CURRENCY_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmCurrencyService: DmCurrencyService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmCurrencyService,
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

      param.FilterFields = ['CURRENCY_ID', 'CURRENCY_NAME'];

      if (!param.Sorts?.length) {
        param.Sorts = [
          {
            Dir: 'ASC',
            Field: 'CURRENCY_ID',
          },
        ];
      }
    }
    return param;
  }
}
