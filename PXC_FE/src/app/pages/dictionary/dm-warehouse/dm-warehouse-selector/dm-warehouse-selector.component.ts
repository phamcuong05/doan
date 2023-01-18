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
import { DmWarehouseService } from 'src/app/model/dictionary/dm-warehouse/dm-warehouse-service';

@Component({
  selector: 'dm-warehouse-selector',
  templateUrl: './dm-warehouse-selector.component.html',
  styleUrls: ['./dm-warehouse-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmWarehouseSelectorComponent),
    },
  ],
})
export class DmWarehouseSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_WAREHOUSE';
  searchFields: string[] = ['WAREHOUSE_ID', 'WAREHOUSE_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmWarehouseResource.MyResource.DM_WAREHOUSE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'WAREHOUSE_ID', Length: 20 },
    { FieldId: 'WAREHOUSE_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmWarehouseService: DmWarehouseService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmWarehouseService,
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

      param.FilterFields = ['WAREHOUSE_ID', 'WAREHOUSE_NAME'];
    }
    return param;
  }
}
