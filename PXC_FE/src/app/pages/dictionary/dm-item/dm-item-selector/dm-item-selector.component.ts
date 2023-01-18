import {
  Component,
  forwardRef,
  ViewContainerRef,
  ElementRef,
  Input,
} from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { Filter } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmItemService } from 'src/app/model/dictionary/dm-item/dm-item-service';

@Component({
  selector: 'dm-item-selector',
  templateUrl: './dm-item-selector.component.html',
  styleUrls: ['./dm-item-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmItemSelectorComponent),
    },
  ],
})
export class DmItemSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_ITEM';
  /**
   * Danh sách các ITEM CLASS ID chấp nhận lấy dữ liệu
   */
  @Input() itemClassIdAllow: string = '';

  searchFields: string[] = ['ITEM_ID', 'ITEM_NAME', 'UNIT_ID'];
  get formTitle(): string {
    return this.resourceManager.DmItemResource.MyResource.DM_ITEM;
  }
  columns: FtsColumn[] = [
    { FieldId: 'ITEM_ID', Width: 120 },
    { FieldId: 'ITEM_NAME', Width: 300 },
    { FieldId: 'UNIT_ID', Width: 120 },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmItemService: DmItemService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmItemService,
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

      //#region ITEM_CLAS_ID
      if (this.itemClassIdAllow.length > 0) {
        var itemClassList: string[] = this.itemClassIdAllow.split(',');

        let itemFilters: Filter[] = [];

        itemClassList.forEach((itemClassId) => {
          itemFilters.push({
            Field: 'ITEM_CLASS_ID',
            Operator: 'eq',
            Value: itemClassId,
          });
        });

        param.FilterGroups?.push({
          Filters: itemFilters,
          Logic: 'or',
        });
      }
      //#endregion

      //#region ACTIVE
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
      //#endregion

      param.FilterFields = ['ITEM_ID', 'ITEM_NAME', 'UNIT_ID'];
    }
    return param;
  }
}
