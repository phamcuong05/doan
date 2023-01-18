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
import { DmRiskClassService } from 'src/app/model/dictionary/dm-risk-class/dm-risk-class-service';

@Component({
  selector: 'dm-risk-class-selector',
  templateUrl: './dm-risk-class-selector.component.html',
  styleUrls: ['./dm-risk-class-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmRiskClassSelectorComponent),
    },
  ],
})
export class DmRiskClassSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = "DM_RISK_CLASS";
  searchFields: string[] = ['RISK_CLASS_ID', 'RISK_CLASS_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmRiskClassResource.MyResource.DM_RISK_CLASS;
  }
  columns: FtsColumn[] = [
    { FieldId: 'RISK_CLASS_ID', Length: 20 },
    { FieldId: 'RISK_CLASS_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmRiskClassService: DmRiskClassService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmRiskClassService,
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

      param.FilterFields = ['RISK_CLASS_ID', 'RISK_CLASS_NAME'];
    }
    return param;
  }
}
