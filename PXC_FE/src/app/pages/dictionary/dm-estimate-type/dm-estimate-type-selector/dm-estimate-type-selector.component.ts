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
import { DmEstimateTypeService } from 'src/app/model/dictionary/dm-estimate-type/dm-estimate-type-service';

@Component({
  selector: 'dm-estimate-type-selector',
  templateUrl: './dm-estimate-type-selector.component.html',
  styleUrls: ['./dm-estimate-type-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmEstimateTypeSelectorComponent),
    },
  ],
})
export class DmEstimateTypeSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = "DM_ESTIMATE_TYPE";
  searchFields: string[] = ['ESTIMATE_TYPE_ID', 'ESTIMATE_TYPE_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmEstimateTypeResource.MyResource
      .DM_ESTIMATE_TYPE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'ESTIMATE_TYPE_ID', Width: 150 },
    { FieldId: 'ESTIMATE_TYPE_NAME', Width: 250 },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmProvinceService: DmEstimateTypeService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmProvinceService,
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

      param.FilterFields = ['ESTIMATE_TYPE_ID', 'ESTIMATE_TYPE_NAME'];
    }
    return param;
  }
}
