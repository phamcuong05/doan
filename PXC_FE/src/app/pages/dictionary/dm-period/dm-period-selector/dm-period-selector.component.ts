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
import { DmPeriodService } from 'src/app/model/dictionary/dm-period/dm-period-service';

@Component({
  selector: 'dm-period-selector',
  templateUrl: './dm-period-selector.component.html',
  styleUrls: ['./dm-period-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPeriodSelectorComponent),
    },
  ],
})
export class DmPeriodSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_PERIOD';
  searchFields: string[] = ['PERIOD_ID', 'PERIOD_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmPeriodResource.MyResource.DM_PERIOD;
  }
  columns: FtsColumn[] = [
    { FieldId: 'PERIOD_ID', Width: 120 },
    { FieldId: 'PERIOD_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmItemOpService: DmPeriodService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmItemOpService,
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

      param.FilterFields = ['PERIOD_ID', 'PERIOD_NAME'];
    }
    return param;
  }
}
