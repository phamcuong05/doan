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
import { DmSecurityService } from 'src/app/model/dictionary/dm-security/dm-security-service';

@Component({
  selector: 'dm-security-selector',
  templateUrl: './dm-security-selector.component.html',
  styleUrls: ['./dm-security-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecuritySelectorComponent),
    },
  ],
})
export class DmSecuritySelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_SECURITY';
  searchFields: string[] = ['SECURITY_ID', 'SECURITY_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmSecurityResource.MyResource.DM_SECURITY;
  }
  columns: FtsColumn[] = [
    { FieldId: 'SECURITY_ID', Length: 20 },
    { FieldId: 'SECURITY_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmSecurityService: DmSecurityService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmSecurityService,
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

      param.FilterFields = ['SECURITY_ID', 'SECURITY_NAME','BOOK_UNIT_PRICE_ORIG'];
    }
    return param;
  }
}
