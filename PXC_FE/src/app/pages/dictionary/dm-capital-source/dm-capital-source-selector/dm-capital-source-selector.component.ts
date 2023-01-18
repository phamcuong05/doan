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
import { DmCapitalSourceService } from 'src/app/model/dictionary/dm-capital-source/dm-capital-source-service';

@Component({
  selector: 'dm-capital-source-selector',
  templateUrl: './dm-capital-source-selector.component.html',
  styleUrls: ['./dm-capital-source-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmCapitalSourceSelectorComponent),
    },
  ],
})
export class DmCapitalSourceSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CAPITAL_SOURCE';
  searchFields: string[] = ['CAPITAL_SOURCE_ID', 'CAPITAL_SOURCE_NAME'];
  columns: FtsColumn[] = [
    { FieldId: 'CAPITAL_SOURCE_ID', Length: 20 },
    { FieldId: 'CAPITAL_SOURCE_NAME' },
  ] as FtsColumn[];
  get formTitle(): string {
    return this.resourceManager.DmCapitalSourceResource.MyResource
      .CAPITAL_SOURCE;
  }

  constructor(
    resourceManage: ResourceManager,
    dmCapitalSourceService: DmCapitalSourceService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmCapitalSourceService,
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

      param.FilterFields = ['CAPITAL_SOURCE_ID', 'CAPITAL_SOURCE_NAME'];
    }
    return param;
  }
}
