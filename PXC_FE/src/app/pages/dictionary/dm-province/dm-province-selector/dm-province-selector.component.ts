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
import { DmProvinceService } from 'src/app/model/dictionary/dm-province/dm-province-service';

@Component({
  selector: 'dm-province-selector',
  templateUrl: './dm-province-selector.component.html',
  styleUrls: ['./dm-province-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmProvinceSelectorComponent),
    },
  ],
})
export class DmProvinceSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_PROVINCE';
  searchFields: string[] = ['PROVINCE_ID', 'PROVINCE_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmProvinceResource.MyResource.DM_PROVINCE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'PROVINCE_ID', Length: 20 },
    { FieldId: 'PROVINCE_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmProvinceService: DmProvinceService,
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

      param.FilterFields = ['PROVINCE_ID', 'PROVINCE_NAME'];
    }
    return param;
  }
}
