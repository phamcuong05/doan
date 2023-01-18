import { Component, ElementRef, forwardRef, ViewContainerRef } from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmSecurityTypeService } from 'src/app/model/dictionary/dm-security-type/dm-security-type-service';

@Component({
  selector: 'dm-security-type-selector',
  templateUrl: './dm-security-type-selector.component.html',
  styleUrls: ['./dm-security-type-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecurityTypeSelectorComponent),
    },
  ],
})
export class DmSecurityTypeSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_SECURITY_TYPE';
  searchFields: string[] = ['SECURITY_TYPE_ID', 'SECURITY_TYPE_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmSecurityTypeResource.MyResource.DM_SECURITY_TYPE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'SECURITY_TYPE_ID', Length: 20 },
    { FieldId: 'SECURITY_TYPE_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmSecurityTypeService: DmSecurityTypeService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmSecurityTypeService,
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

      param.FilterFields = ['SECURITY_TYPE_ID', 'SECURITY_TYPE_NAME'];
    }
    return param;
  }
}
