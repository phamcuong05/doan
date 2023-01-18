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
import { DmUnitService } from 'src/app/model/dictionary/dm-unit/dm-unit-service';

@Component({
  selector: 'dm-unit-selector',
  templateUrl: './dm-unit-selector.component.html',
  styleUrls: ['./dm-unit-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmUnitSelectorComponent),
    },
  ],
})
export class DmUnitSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_UNIT';
  searchFields: string[] = ['UNIT_ID', 'UNIT_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmUnitResource.MyResource.DM_UNIT;
  }
  columns: FtsColumn[] = [
    { FieldId: 'UNIT_ID', Length: 20 },
    { FieldId: 'UNIT_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmUnitService: DmUnitService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmUnitService,
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

      param.FilterFields = ['UNIT_ID', 'UNIT_NAME'];
    }
    return param;
  }
}
